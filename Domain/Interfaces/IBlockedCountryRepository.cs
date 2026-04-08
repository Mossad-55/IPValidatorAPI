using Domain.Entities;

namespace Domain.Interfaces;

public interface IBlockedCountryRepository
{
    Task<bool> AddAsync(BlockedCountry country);
    Task<bool> DeleteAsync(string countryCode);
    Task<bool> ExistsAsync(string countryCode);
    Task<IEnumerable<BlockedCountry>> GetAllAsync();
}
