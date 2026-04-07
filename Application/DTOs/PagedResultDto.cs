namespace Application.DTOs;

public record PagedResultDto<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
}
