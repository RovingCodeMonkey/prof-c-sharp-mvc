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

        public async Task<IEnumerable<Models.Sale>> Get()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Models.Sale?> Get(int id)
        {
            return await _context.Sales.Where(s => s.SalesId == id).FirstOrDefaultAsync();
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
