using Web.Models;

namespace Web.Data
{
    public interface ISalesPersons
    {
        Task<SalesPerson> Create(SalesPerson salesPerson);
        Task Delete(int id);
        Task<IEnumerable<SalesPerson>> Get();
        Task<SalesPerson?> Get(int id);
        Task Update(SalesPerson salesPerson);
    }
}