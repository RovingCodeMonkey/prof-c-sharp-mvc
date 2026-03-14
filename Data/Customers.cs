using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class Customers : ICustomers
    {
        private readonly AppDbContext _context;
        public Customers(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Models.Customer>> Get()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Models.Customer?> Get(int id)
        {
            return await _context.Customers.Where(c => c.CustomerId == id).FirstOrDefaultAsync();
        }

        public async Task<Models.Customer> Create(Models.Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task Update(Models.Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var customer = await Get(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
