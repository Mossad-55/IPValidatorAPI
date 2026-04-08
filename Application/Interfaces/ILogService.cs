using Application.DTOs;

namespace Application.Interfaces;

public interface ILogService
{
    Task LogAttemptyAsync(LogEntryDto dto);
    Task<IEnumerable<LogEntryDto>> GetAllLogsAsync();
}
