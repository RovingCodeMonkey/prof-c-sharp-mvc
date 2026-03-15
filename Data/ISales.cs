

namespace Web.Data
{
    public interface ISales
    {
        Task<Models.Sale> Create(Models.Sale sale);
        Task Delete(int id);
        Task<(IEnumerable<Models.Sale> Items, int TotalCount)> Get(int page, int count, long? productId, long? salesPersonId, long? customerId, string? sortBy, bool ascending);
        Task<Models.Sale?> Get(int id);
        Task Update(Models.Sale sales);
    }
}