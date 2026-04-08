using Application.DTOs;
using Application.Interfaces;
using Application.RequestFeatures;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using ISO3166;

namespace Application.Services;

internal sealed class CountryService : ICountryService
{
    private readonly IRepositoryManager _repository;

    public CountryService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task AddBlockedCountryAsync(BlockCountryDto dto)
    {
        if (await _repository.BlockedCountry.ExistsAsync(dto.CountryCode.ToUpper()))
            throw new ConflictException("Country already blocked");

        var country = Country.List.FirstOrDefault(c => string.Equals(c.TwoLetterCode, dto.CountryCode, StringComparison.OrdinalIgnoreCase));
        if (country == null)
            throw new ConflictException("Country code not found");

        var countryEntitiy = new BlockedCountry
        {
            CountryCode = dto.CountryCode.ToUpper(),
            CountryName = country.Name
        };

        await _repository.BlockedCountry.AddAsync(countryEntitiy);
    }

    public async Task AddTemporalBlockAsync(TemporalBlockDto dto)
    {
        if (dto.DurationInMinutes < 1 || dto.DurationInMinutes > 1440)
            throw new ConflictException("Invalid Duration");

        if (await _repository.TemporalBlocked.ExistsAsync(dto.CountryCode))
            throw new ConflictException("Country already has a temporal block");

        var country = Country.List.FirstOrDefault(c => string.Equals(c.TwoLetterCode, dto.CountryCode, StringComparison.OrdinalIgnoreCase));
        if (country == null)
            throw new ConflictException("Country code not found");

        await _repository.TemporalBlocked.AddAsync(new TemporalBlockedCountry
        {
            CountryCode = dto.CountryCode.ToUpper(),
            CountryName = country.Name,
            ExpiresAt = DateTime.UtcNow.AddMinutes(dto.DurationInMinutes)
        });
    }

    public async Task<PagedResultDto<BlockedCountryDto>> GetBlockedCountreisAsync(PaginationRequest request)
    {
        var result = await _repository.BlockedCountry.GetAllAsync();

        // Search
        var filteredCountires = result.Where(c => string.IsNullOrEmpty(request.SearchTerm) ||
            c.CountryCode.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));

        // Pagination
        var pagedCountries = filteredCountires.Skip((request.Page - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(c => new BlockedCountryDto { CountryCode = c.CountryCode, CountryName = c.CountryName });

        return new PagedResultDto<BlockedCountryDto>
        {
            Items = pagedCountries,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = filteredCountires.Count()
        };
    }

    public async Task RemoveBlockedCountryAsync(string countryCode)
    {
        var exists = await _repository.TemporalBlocked.ExistsAsync(countryCode.ToUpper());
        if(!exists)
            throw new NotFoundException($"Country with code: {countryCode} was not found.");

        await _repository.TemporalBlocked.RemoveAsync(countryCode.ToUpper());
    }
}
