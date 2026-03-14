using Web.Models;

namespace Web.Data
{
    public interface IProducts
    {
        Task<Product> Create(Product product);
        Task Delete(int id);
        Task<IEnumerable<Product>> Get();
        Task<Product?> Get(int id);
        Task Update(Product product);
    }
}