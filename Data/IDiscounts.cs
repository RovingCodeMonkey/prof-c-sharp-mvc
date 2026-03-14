using Web.Models;

namespace Web.Data
{
    public interface IDiscounts
    {
        Task<Discount> Create(Discount discount);
        Task Delete(int id);
        Task<IEnumerable<Discount>> Get();
        Task<Discount?> Get(int id);
        Task Update(Discount discount);
    }
}