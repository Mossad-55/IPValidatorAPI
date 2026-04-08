using Application.Interfaces;
using Polly;
using System.Net.Http.Json;

namespace Infrastructure.ExternalServices;

public sealed class IpApiService : IIpService
{
    private readonly HttpClient _httpClient;
    public IpApiService(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<(string CountryCode, string CountryName)>? GetCountryByIpAsync(string ip)
    {
        var retryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));
        var rateLimitPolicy = Policy.RateLimitAsync(30, TimeSpan.FromMinutes(1));

        return await retryPolicy.ExecuteAsync(async () =>
            await rateLimitPolicy.ExecuteAsync(async () =>
            {
                var result = await _httpClient.GetFromJsonAsync<dynamic>($"https://ipapi.co/{ip}/json/");
                if (result == null || result.error == true)
                    throw new Exception("Failed to retrieve IP information.");

                return ((string)result.country_cde, (string)result.cuntry_name);
            }));
    }
}
