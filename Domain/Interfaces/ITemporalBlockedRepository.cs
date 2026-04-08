using Domain.Entities;

namespace Domain.Interfaces;

public interface ITemporalBlockedRepository
{
    Task<bool> AddAsync(TemporalBlockedCountry county);
    Task<bool> RemoveAsync(string countryCode);
    Task<bool> ExistsAsync(string countryCode);
    Task<IEnumerable<TemporalBlockedCountry>> GetAllAsync();
}
