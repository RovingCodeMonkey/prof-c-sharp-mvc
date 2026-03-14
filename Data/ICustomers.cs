using Web.Models;

namespace Web.Data
{
    public interface ICustomers
    {
        Task<Customer> Create(Customer customer);
        Task Delete(int id);
        Task<IEnumerable<Customer>> Get();
        Task<Customer?> Get(int id);
        Task Update(Customer customer);
    }
}