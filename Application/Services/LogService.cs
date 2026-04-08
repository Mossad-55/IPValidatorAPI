using Application.DTOs;
using Application.Interfaces;
using Application.RequestFeatures;
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

    public async Task<PagedResultDto<LogEntryDto>> GetAllLogsAsync(PaginationRequest request)
    {
        var logs =  await _repsitory.Log.GetAllAsync();

        // Sort 
        var sortedLogs = logs.OrderByDescending(x => x.Timestamp);

        // Pagination
        var pagedLogs = sortedLogs.Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new LogEntryDto
            {
                IpAddess = x.IpAddess,
                CountryCode = x.CountryCode,
                Timestamp = x.Timestamp,
                IsBlocked = x.IsBlocked,
                UserAgent = x.UserAgent
            });

        return new PagedResultDto<LogEntryDto>
        {
            Items = pagedLogs,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = logs.Count()
        };
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
