using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<ICountryService> _countryService;
    private readonly Lazy<ILogService> _logService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _countryService = new Lazy<ICountryService>(() => new CountryService(repositoryManager));
        _logService = new Lazy<ILogService>(() => new LogService(repositoryManager));
    }


    public ICountryService CountryService => _countryService.Value;

    public ILogService LogService => _logService.Value;
}
