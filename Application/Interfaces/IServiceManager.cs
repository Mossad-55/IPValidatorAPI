namespace Application.Interfaces;

public interface IServiceManager
{
    ICountryService CountryService { get; }
    ILogService LogService { get; }
}
