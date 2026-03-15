using Web.Models;

namespace Web.Data
{
    public interface IProducts
    {
        Task<Product> Create(Product product);
        Task Delete(int id);
        Task<(IEnumerable<Product> Items, int TotalCount)> Get(int page, int count, string? search, string? sortBy, bool ascending);
        Task<Product?> Get(int id);
        Task Update(Product product);
    }
}