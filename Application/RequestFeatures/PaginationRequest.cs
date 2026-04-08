using Microsoft.AspNetCore.Mvc;

namespace Application.RequestFeatures;

public record PaginationRequest
{
    [FromQuery(Name = "page")]
    public int Page { get; set; } = 1;
    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; } = 10;
    [FromQuery(Name = "searchTerm")]
    public string? SearchTerm { get; set; }
}
