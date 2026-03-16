

namespace Web.Data
{
    public interface ISales
    {
        Task<Models.OperationResult<Models.Sale>> Create(Models.Sale sale);
        Task Delete(int id);
        Task<(IEnumerable<Models.Sale> Items, int TotalCount)> Get(int page, int count, long? productId, long? salesPersonId, long? customerId, DateTime? startDate, DateTime? endDate, string? sortBy, bool ascending);
        Task<Models.Sale?> Get(int id);
        Task<bool> Update(Models.Sale sales);
        Task<Models.ReportResult> GetReport(long? productId, long? salesPersonId, long? customerId, DateTime startDate, DateTime endDate);
    }
}