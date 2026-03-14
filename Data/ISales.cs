

namespace Web.Data
{
    public interface ISales
    {
        Task<Models.Sale> Create(Models.Sale sale);
        Task Delete(int id);
        Task<IEnumerable<Models.Sale>> Get();
        Task<Models.Sale?> Get(int id);
        Task Update(Models.Sale sales);
    }
}