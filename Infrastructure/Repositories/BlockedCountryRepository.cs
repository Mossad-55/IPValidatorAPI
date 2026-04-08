using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Concurrent;

namespace Infrastructure.Repositories;

internal sealed class BlockedCountryRepository : IBlockedCountryRepository
{
    private readonly ConcurrentDictionary<string, BlockedCountry> _countries = new();

    public Task<bool> AddAsync(BlockedCountry country) =>
        Task.FromResult(_countries.TryAdd(country.CountryCode.ToUpper(), country));

    public Task<bool> DeleteAsync(string countryCode) =>
        Task.FromResult(_countries.TryRemove(countryCode, out _));

    public Task<bool> ExistsAsync(string countryCode) =>
        Task.FromResult(_countries.ContainsKey(countryCode.ToUpper()));

    public Task<IEnumerable<BlockedCountry>> GetAllAsync() =>
        Task.FromResult(_countries.Values.AsEnumerable());
}
