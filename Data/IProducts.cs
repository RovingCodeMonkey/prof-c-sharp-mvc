using Web.Models;

namespace Web.Data
{
    public interface IProducts
    {
        Task<Product> Create(Product product);
        Task Delete(int id);
        Task<bool> ExistsWithNameAndManufacturer(string name, string manufacturer, long? excludeId = null);
        Task<(IEnumerable<Product> Items, int TotalCount)> Get(int page, int count, string? search, string? sortBy, bool ascending, bool inStockOnly = false);
        Task<Product?> Get(int id);
        Task<bool> Update(Product product);
    }
}