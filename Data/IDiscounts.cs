using Web.Models;

namespace Web.Data
{
    public interface IDiscounts
    {
        Task<Discount> Create(Discount discount);
        Task Delete(int id);
        Task<(IEnumerable<Discount> Items, int TotalCount)> Get(int page, int count, string? search, string? sortBy, bool ascending);
        Task<Discount?> Get(int id);
        Task<IEnumerable<Discount>> GetAvailable(long productId, DateTime date);
        Task Update(Discount discount);
    }
}