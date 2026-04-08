using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Concurrent;

namespace Infrastructure.Repositories;

internal sealed class LogRepostiory : ILogRepository
{
    private readonly ConcurrentBag<LogEntry> _logs = new();
    public Task AddAsync(LogEntry log)
    {
        _logs.Add(log);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<LogEntry>> GetAllAsync() =>
        Task.FromResult(_logs.AsEnumerable());
}
