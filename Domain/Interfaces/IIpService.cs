namespace Application.Interfaces;

public interface IIpService
{
    Task<(string CountryCode, string CountryName)>? GetCountryByIpAsync(string ip);
}
