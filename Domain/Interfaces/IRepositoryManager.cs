namespace Domain.Interfaces;

public interface IRepositoryManager
{
    // Repositories
    IBlockedCountryRepository BlockedCountry { get; }
    ILogRepository Log { get; }
    ITemporalBlockedRepository TemporalBlocked { get; }
}
