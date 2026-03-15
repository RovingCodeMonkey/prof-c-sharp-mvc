namespace Web.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int Count { get; set; }
        public int? Cursor { get; set; }
    }
}
