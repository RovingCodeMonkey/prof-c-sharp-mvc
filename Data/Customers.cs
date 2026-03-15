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

        public async Task<(IEnumerable<Models.Customer> Items, int TotalCount)> Get(int page, int count, string? search, string? phone, string? sortBy, bool ascending)
        {
            var query = _context.Customers.AsQueryable();
            //Using a starts with query so that the index can be used not a table scan
            if (!string.IsNullOrEmpty(search))
                query = query.Where(c => EF.Functions.Like(c.FirstName, search + "%") || EF.Functions.Like(c.LastName, search + "%"));

            if (!string.IsNullOrEmpty(phone))
                query = query.Where(c => c.Phone.Contains(phone));

            query = (sortBy?.ToLower()) switch
            {
                "firstname" => ascending ? query.OrderBy(c => c.FirstName) : query.OrderByDescending(c => c.FirstName),
                "lastname"  => ascending ? query.OrderBy(c => c.LastName)  : query.OrderByDescending(c => c.LastName),
                "address"   => ascending ? query.OrderBy(c => c.Address)   : query.OrderByDescending(c => c.Address),
                "phone"     => ascending ? query.OrderBy(c => c.Phone)     : query.OrderByDescending(c => c.Phone),
                "startdate" => ascending ? query.OrderBy(c => c.StartDate) : query.OrderByDescending(c => c.StartDate),
                _           => query.OrderBy(c => c.FirstName),
            };

            var totalCount = await query.CountAsync();
            var items = await query.Skip(page * count).Take(count).ToListAsync();
            return (items, totalCount);
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
