using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class Products : IProducts
    {
        private readonly AppDbContext _context;
        public Products(AppDbContext context)
        {
            this._context = context;
        }    

        public async Task<(IEnumerable<Models.Product> Items, int TotalCount)> Get(int page, int count, string? search, string? sortBy, bool ascending, bool inStockOnly = false)
        {
            var query = _context.Products.AsQueryable();

            //Using a starts with query so that the index can be used not a table scan
            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => EF.Functions.Like(p.Name, search + "%"));

            if (inStockOnly)
                query = query.Where(p => p.QtyOnHand > 0);

            query = (sortBy?.ToLower()) switch
            {
                "name"                => ascending ? query.OrderBy(p => p.Name)                : query.OrderByDescending(p => p.Name),
                "manufacturer"        => ascending ? query.OrderBy(p => p.Manufacturer)        : query.OrderByDescending(p => p.Manufacturer),
                "style"               => ascending ? query.OrderBy(p => p.Style)               : query.OrderByDescending(p => p.Style),
                "purchaseprice"       => ascending ? query.OrderBy(p => p.PurchasePrice)       : query.OrderByDescending(p => p.PurchasePrice),
                "saleprice"           => ascending ? query.OrderBy(p => p.SalePrice)           : query.OrderByDescending(p => p.SalePrice),
                "qtyonhand"           => ascending ? query.OrderBy(p => p.QtyOnHand)           : query.OrderByDescending(p => p.QtyOnHand),
                "commisionpercentage" => ascending ? query.OrderBy(p => p.CommisionPercentage) : query.OrderByDescending(p => p.CommisionPercentage),
                _                     => query.OrderBy(p => p.Name),
            };

            var totalCount = await query.CountAsync();
            var items = await query.Skip(page * count).Take(count).ToListAsync();
            return (items, totalCount);
        }

        public async Task<bool> ExistsWithNameAndManufacturer(string name, string manufacturer, long? excludeId = null)
        {
            var query = _context.Products.Where(p =>
                p.Name == name &&
                p.Manufacturer == manufacturer);

            if (excludeId.HasValue)
                query = query.Where(p => p.ProductId != excludeId.Value);

            return await query.AnyAsync();
        }

        public async Task<Models.Product?> Get(int id)
        {
            return await _context.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task<Models.Product> Create(Models.Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Update(Models.Product product)
        {
            if (!await _context.Products.AnyAsync(p => p.ProductId == product.ProductId))
                return false;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Delete(int id)
        {
            var product = await Get(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
