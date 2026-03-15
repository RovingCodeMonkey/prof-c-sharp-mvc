using Web.Models;

namespace Web.Data
{
    public interface ISalesPersons
    {
        Task<SalesPerson> Create(SalesPerson salesPerson);
        Task Delete(int id);
        Task<(IEnumerable<SalesPerson> Items, int TotalCount)> Get(int page, int count, string? search, string? phone, string? sortBy, bool ascending);
        Task<SalesPerson?> Get(int id);
        Task Update(SalesPerson salesPerson);
    }
}