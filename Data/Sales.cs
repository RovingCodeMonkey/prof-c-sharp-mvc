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
                "finalprice" => ascending ? query.OrderBy(s => s.FinalPrice) : query.OrderByDescending(s => s.FinalPrice),
                "commision" => ascending ? query.OrderBy(s => s.Commision) : query.OrderByDescending(s => s.Commision),
                "customerid"    => ascending ? query.OrderBy(s => s.CustomerId)    : query.OrderByDescending(s => s.CustomerId),
                _               => query.OrderByDescending(s => s.SalesDate),
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

        public async Task<Models.OperationResult<Models.Sale>> Create(Models.Sale sale)
        {
            var product = await _context.Products.FindAsync(sale.ProductId);
            if (product == null || product.QtyOnHand <= 0)
                return Models.OperationResult<Models.Sale>.Failure("The product is out of stock.");

            product.QtyOnHand--;
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return Models.OperationResult<Models.Sale>.Success(sale);
        }

        public async Task<bool> Update(Models.Sale sales)
        {
            if (!await _context.Sales.AnyAsync(s => s.SalesId == sales.SalesId))
                return false;
            _context.Sales.Update(sales);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Delete(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Models.ReportResult> GetReport(long? productId, long? salesPersonId, long? customerId, DateTime startDate, DateTime endDate)
        {
            var query = _context.Sales.AsQueryable();

            if (productId.HasValue)     query = query.Where(s => s.ProductId == productId.Value);
            if (salesPersonId.HasValue) query = query.Where(s => s.SalesPersonId == salesPersonId.Value);
            if (customerId.HasValue)    query = query.Where(s => s.CustomerId == customerId.Value);

            query = query.Where(s => s.SalesDate >= startDate && s.SalesDate <= endDate);

            var agg = await query
                .GroupBy(_ => 1)
                .Select(g => new
                {
                    Count           = g.Count(),
                    TotalSales      = g.Sum(s => s.FinalPrice),
                    TotalCommission = g.Sum(s => s.Commision)
                })
                .FirstOrDefaultAsync();

            var totalCount      = agg?.Count ?? 0;
            var totalSales      = agg?.TotalSales ?? 0;
            var totalCommission = agg?.TotalCommission ?? 0;

            var items = await query
                .Include(s => s.Product)
                .Include(s => s.SalesPerson)
                .Include(s => s.Customer)
                .OrderByDescending(s => s.SalesDate)
                .ToListAsync();

            return new Models.ReportResult
            {
                Items           = items,
                Count           = totalCount,
                TotalSales      = totalSales,
                TotalCommission = totalCommission
            };
        }
    }
}
