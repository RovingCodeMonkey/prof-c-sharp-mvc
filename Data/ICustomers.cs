using Web.Models;

namespace Web.Data
{
    public interface ICustomers
    {
        Task<Customer> Create(Customer customer);
        Task Delete(int id);
        Task<(IEnumerable<Customer> Items, int TotalCount)> Get(int page, int count, string? search, string? phone, string? sortBy, bool ascending);
        Task<Customer?> Get(int id);
        Task<bool> Update(Customer customer);
    }
}