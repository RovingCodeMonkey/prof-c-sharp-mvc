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

        public async Task<(IEnumerable<Models.SalesPerson> Items, int TotalCount)> Get(int page, int count, string? search, string? phone, string? sortBy, bool ascending)
        {
            var query = _context.SalesPersons.AsQueryable();
            //Using a starts with query so that the index can be used not a table scan
            if (!string.IsNullOrEmpty(search))
                query = query.Where(sp => EF.Functions.Like(sp.FirstName, search + "%") || EF.Functions.Like(sp.LastName, search + "%"));

            if (!string.IsNullOrEmpty(phone))
                query = query.Where(sp => sp.Phone.Contains(phone));

            query = (sortBy?.ToLower()) switch
            {
                "firstname"       => ascending ? query.OrderBy(sp => sp.FirstName)       : query.OrderByDescending(sp => sp.FirstName),
                "lastname"        => ascending ? query.OrderBy(sp => sp.LastName)        : query.OrderByDescending(sp => sp.LastName),
                "address"         => ascending ? query.OrderBy(sp => sp.Address)         : query.OrderByDescending(sp => sp.Address),
                "phone"           => ascending ? query.OrderBy(sp => sp.Phone)           : query.OrderByDescending(sp => sp.Phone),
                "startdate"       => ascending ? query.OrderBy(sp => sp.StartDate)       : query.OrderByDescending(sp => sp.StartDate),
                "terminationdate" => ascending ? query.OrderBy(sp => sp.TerminationDate) : query.OrderByDescending(sp => sp.TerminationDate),
                "manager"         => ascending ? query.OrderBy(sp => sp.Manager)         : query.OrderByDescending(sp => sp.Manager),
                _                 => query.OrderBy(sp => sp.FirstName),
            };

            var totalCount = await query.CountAsync();
            var items = await query.Skip(page * count).Take(count).ToListAsync();
            return (items, totalCount);
        }

        public async Task<bool> ExistsWithNameAndPhone(string firstName, string lastName, string phone, long? excludeId = null)
        {
            var query = _context.SalesPersons.Where(sp =>
                sp.FirstName == firstName &&
                sp.LastName == lastName &&
                sp.Phone == phone);

            if (excludeId.HasValue)
                query = query.Where(sp => sp.SalesPersonId != excludeId.Value);

            return await query.AnyAsync();
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

        public async Task<bool> Update(Models.SalesPerson salesPerson)
        {
            if (!await _context.SalesPersons.AnyAsync(sp => sp.SalesPersonId == salesPerson.SalesPersonId))
                return false;
            _context.SalesPersons.Update(salesPerson);
            await _context.SaveChangesAsync();
            return true;
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
