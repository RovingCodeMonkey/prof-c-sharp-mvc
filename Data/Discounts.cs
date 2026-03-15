using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class Discounts : IDiscounts
    {
        private readonly AppDbContext _context;
        public Discounts(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<(IEnumerable<Models.Discount> Items, int TotalCount)> Get(int page, int count, string? search, string? sortBy, bool ascending)
        {
            var query = _context.Discounts.Include(d => d.Product).AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(d => EF.Functions.Like(d.Product.Name, search + "%"));

            query = (sortBy?.ToLower()) switch
            {
                "productname"        => ascending ? query.OrderBy(d => d.Product.Name)        : query.OrderByDescending(d => d.Product.Name),
                "begindate"          => ascending ? query.OrderBy(d => d.BeginDate)           : query.OrderByDescending(d => d.BeginDate),
                "enddate"            => ascending ? query.OrderBy(d => d.EndDate)             : query.OrderByDescending(d => d.EndDate),
                "discountpercentage" => ascending ? query.OrderBy(d => d.DiscountPercentage)  : query.OrderByDescending(d => d.DiscountPercentage),
                _                    => query.OrderBy(d => d.Product.Name),
            };

            var totalCount = await query.CountAsync();
            var items = await query.Skip(page * count).Take(count).ToListAsync();
            return (items, totalCount);
        }

        public async Task<IEnumerable<Models.Discount>> GetAvailable(long productId, DateTime date)
        {
            return await _context.Discounts
                .Include(d => d.Product)
                .Where(d => d.ProductId == productId && d.BeginDate <= date && d.EndDate >= date)
                .ToListAsync();
        }

        public async Task<Models.Discount?> Get(int id)
        {
            return await _context.Discounts.Include(d => d.Product).Where(d => d.DiscountId == id).FirstOrDefaultAsync();
        }

        public async Task<Models.Discount> Create(Models.Discount discount)
        {
            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();
            return discount;
        }

        public async Task Update(Models.Discount discount)
        {
            _context.Discounts.Update(discount);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var discount = await Get(id);
            if (discount != null)
            {
                _context.Discounts.Remove(discount);
                await _context.SaveChangesAsync();
            }
        }
    }
}
