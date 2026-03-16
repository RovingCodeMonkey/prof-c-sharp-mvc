namespace Web.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int Count { get; set; }
        public int? Cursor { get; set; }

        public static PagedResult<T> Create(IEnumerable<T> items, int totalCount, int page, int count) =>
            new()
            {
                Items  = items,
                Count  = totalCount,
                Cursor = (page + 1) * count < totalCount ? page + 1 : null
            };
    }
}
