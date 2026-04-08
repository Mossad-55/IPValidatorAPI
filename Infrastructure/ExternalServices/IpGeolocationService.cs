using Application.Interfaces;
using Domain.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Infrastructure.ExternalServices;

public class IpGeolocationService : IIpService
{
    private readonly HttpClient _httpClient;
    private readonly IpGeolocationSettings _settings;

    public IpGeolocationService(HttpClient httpClient,
        IOptions<IpGeolocationSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }
    public async Task<(string CountryCode, string CountryName)?> GetCountryByIpAsync(string ip)
    {
        try
        {
            var url = $"{_settings.BaseUrl}?apiKey={_settings.ApiKey}&ip={ip}";

            var response = await _httpClient.GetFromJsonAsync<IpGeolocationApiResponse>(url);

            if (response == null || string.IsNullOrWhiteSpace(response.Country_code2))
                return null;

            return (response.Country_code2, response.Country_name!);
        }
        catch
        {
            return null; // graceful failure
        }
    }
}
