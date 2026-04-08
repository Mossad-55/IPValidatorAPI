namespace Domain.Configuration;

public record IpGeolocationApiResponse
{
    public string? Ip { get; set; }
    public string? Country_name { get; set; }
    public string? Country_code2 { get; set; }
}
