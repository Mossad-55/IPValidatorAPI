namespace Application.DTOs;

public record BlockedCountryDto
{
    public string CountryCode { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
}
