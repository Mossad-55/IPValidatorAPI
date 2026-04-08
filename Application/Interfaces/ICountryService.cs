using Application.DTOs;
using Application.RequestFeatures;

namespace Application.Interfaces;

public interface ICountryService
{
    Task AddBlockedCountryAsync(BlockCountryDto dto);
    Task<PagedResultDto<BlockedCountryDto>> GetBlockedCountreisAsync(PaginationRequest request);
    Task RemoveBlockedCountryAsync(string countryCode);
    Task AddTemporalBlockAsync(TemporalBlockDto dto);
}
