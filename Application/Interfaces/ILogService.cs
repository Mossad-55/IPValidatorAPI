using Application.DTOs;
using Application.RequestFeatures;

namespace Application.Interfaces;

public interface ILogService
{
    Task LogAttemptyAsync(LogEntryDto dto);
    Task<PagedResultDto<LogEntryDto>> GetAllLogsAsync(PaginationRequest request);
}
