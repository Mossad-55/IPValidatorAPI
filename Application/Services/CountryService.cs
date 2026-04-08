using Application.DTOs;
using Application.Interfaces;
using Application.RequestFeatures;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

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

        var country = new BlockedCountry
        {
            CountryCode = dto.CountryCode.ToUpper(),
            CountryName = dto.CountryCode.ToUpper()
        };

        await _repository.BlockedCountry.AddAsync(country);
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
}
