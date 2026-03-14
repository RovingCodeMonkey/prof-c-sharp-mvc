using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class SalesPersons : ISalesPersons
    {
        private readonly AppDbContext _context;
        public SalesPersons(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Models.SalesPerson>> Get()
        {
            return await _context.SalesPersons.ToListAsync();
        }

        public async Task<Models.SalesPerson?> Get(int id)
        {
            return await _context.SalesPersons.Where(sp => sp.SalesPersonId == id).FirstOrDefaultAsync();
        }

        public async Task<Models.SalesPerson> Create(Models.SalesPerson salesPerson)
        {
            _context.SalesPersons.Add(salesPerson);
            await _context.SaveChangesAsync();
            return salesPerson;
        }

        public async Task Update(Models.SalesPerson salesPerson)
        {
            _context.SalesPersons.Update(salesPerson);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var salesPerson = await Get(id);
            if (salesPerson != null)
            {
                _context.SalesPersons.Remove(salesPerson);
                await _context.SaveChangesAsync();
            }
        }
    }
}
