using Domain.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.HostedServices;

public class TemporalCleanupService : BackgroundService
{
    private readonly IRepositoryManager _repositoryManager;

    public TemporalCleanupService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var expired = (await _repositoryManager.TemporalBlocked.GetAllAsync())
                .Where(x => x.ExpiresAt <= DateTime.UtcNow)
                .ToList();

            foreach (var item in expired)
                await _repositoryManager.TemporalBlocked.RemoveAsync(item.CountryCode);

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
