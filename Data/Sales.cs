using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class Sales : ISales
    {
        private readonly AppDbContext _context;
        public Sales(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<(IEnumerable<Models.Sale> Items, int TotalCount)> Get(int page, int count, long? productId, long? salesPersonId, long? customerId, DateTime? startDate, DateTime? endDate, string? sortBy, bool ascending)
        {
            var query = _context.Sales
                .Include(s => s.Product)
                .Include(s => s.SalesPerson)
                .Include(s => s.Customer)
                .AsQueryable();

            if (productId.HasValue)
                query = query.Where(s => s.ProductId == productId.Value);

            if (salesPersonId.HasValue)
                query = query.Where(s => s.SalesPersonId == salesPersonId.Value);

            if (customerId.HasValue)
                query = query.Where(s => s.CustomerId == customerId.Value);

            if (startDate.HasValue)
                query = query.Where(s => s.SalesDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(s => s.SalesDate <= endDate.Value);

            query = (sortBy?.ToLower()) switch
            {
                "salesdate"     => ascending ? query.OrderBy(s => s.SalesDate)     : query.OrderByDescending(s => s.SalesDate),
                "productid"     => ascending ? query.OrderBy(s => s.ProductId)     : query.OrderByDescending(s => s.ProductId),
                "salespersonid" => ascending ? query.OrderBy(s => s.SalesPersonId) : query.OrderByDescending(s => s.SalesPersonId),
                "customerid"    => ascending ? query.OrderBy(s => s.CustomerId)    : query.OrderByDescending(s => s.CustomerId),
                _               => query.OrderBy(s => s.SalesId),
            };

            var totalCount = await query.CountAsync();
            var items = await query.Skip(page * count).Take(count).ToListAsync();
            return (items, totalCount);
        }

        public async Task<Models.Sale?> Get(int id)
        {
            return await _context.Sales
                .Include(s => s.Product)
                .Include(s => s.SalesPerson)
                .Include(s => s.Customer)
                .Where(s => s.SalesId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Models.Sale> Create(Models.Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task Update(Models.Sale sales)
        {
            _context.Sales.Update(sales);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var sale = await Get(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }
    }
}
