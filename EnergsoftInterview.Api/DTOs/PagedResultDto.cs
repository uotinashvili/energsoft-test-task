namespace EnergsoftInterview.Api.DTOs
{
    public class PagedResultDto<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public string? ContinuationToken { get; set; }
    }
}