namespace Application.DTOs;

public record LogEntryDto
{
    public string IpAddess { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public bool IsBlocked { get; set; }
    public string UserAgent { get; set; } = string.Empty;
}
