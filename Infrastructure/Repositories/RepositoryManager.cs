using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IBlockedCountryRepository> _blockedCountryRepository;
    private readonly Lazy<ITemporalBlockedRepository> _temporalBlockedRepository;
    private readonly Lazy<ILogRepository> _logRepository;

    public RepositoryManager()
    {
        _blockedCountryRepository = new Lazy<IBlockedCountryRepository>(() => new BlockedCountryRepository());
        _temporalBlockedRepository = new Lazy<ITemporalBlockedRepository>(() => new TemporalBlockedRepository());
        _logRepository = new Lazy<ILogRepository>(() => new LogRepostiory());
    }

    public IBlockedCountryRepository BlockedCountry => _blockedCountryRepository.Value;
    public ITemporalBlockedRepository TemporalBlocked => _temporalBlockedRepository.Value;
    public ILogRepository Log => _logRepository.Value;
}
