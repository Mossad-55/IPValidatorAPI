using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Concurrent;

namespace Infrastructure.Repositories;

internal sealed class TemporalBlockedRepository : ITemporalBlockedRepository
{
    private readonly ConcurrentDictionary<string, TemporalBlockedCountry> _tempCountries = new();
    public Task<bool> AddAsync(TemporalBlockedCountry county) =>
        Task.FromResult(_tempCountries.TryAdd(county.CountryCode.ToUpper(), county));

    public Task<bool> ExistsAsync(string countryCode) =>
        Task.FromResult(_tempCountries.ContainsKey(countryCode.ToUpper()));

    public Task<IEnumerable<TemporalBlockedCountry>> GetAllAsync() =>
        Task.FromResult(_tempCountries.Values.AsEnumerable());

    public Task<bool> RemoveAsync(string countryCode) =>
        Task.FromResult(_tempCountries.TryRemove(countryCode.ToUpper(), out _));
}
