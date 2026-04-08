using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

internal sealed class LogService : ILogService
{
    private readonly IRepositoryManager _repsitory;

    public LogService(IRepositoryManager repository)
    {
        _repsitory = repository;
    }

    public async Task<IEnumerable<LogEntryDto>> GetAllLogsAsync()
    {
        var logs =  await _repsitory.Log.GetAllAsync();

        return logs.Select(log => new LogEntryDto
        {
            IpAddess = log.IpAddess,
            CountryCode = log.CountryCode,
            Timestamp = log.Timestamp,
            IsBlocked = log.IsBlocked,
            UserAgent = log.UserAgent
        });
    }

    public async Task LogAttemptyAsync(LogEntryDto dto)
    {
        await _repsitory.Log.AddAsync(new LogEntry
        {
            IpAddess = dto.IpAddess,
            CountryCode = dto.CountryCode,
            Timestamp = dto.Timestamp,
            IsBlocked = dto.IsBlocked,
            UserAgent = dto.UserAgent
        });
    }
}
