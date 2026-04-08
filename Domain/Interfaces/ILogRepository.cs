using Domain.Entities;

namespace Domain.Interfaces;

public interface ILogRepository
{
    Task AddAsync(LogEntry log);
    Task<IEnumerable<LogEntry>> GetAllAsync();
}
