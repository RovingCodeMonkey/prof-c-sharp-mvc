namespace Web.Models
{
    public class ReportResult
    {
        public IEnumerable<Sale> Items { get; set; } = [];
        public int Count { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalCommission { get; set; }
    }
}
