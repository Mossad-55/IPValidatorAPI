namespace Application.DTOs;

public record TemporalBlockDto
{
    public string CountryCode { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
}
