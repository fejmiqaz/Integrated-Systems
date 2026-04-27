using CoursesApplication.Service.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoursesApplication.Service.Jobs;

public class LegacyDbETLBackgroundService : BackgroundService
{

    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<LegacyDbETLBackgroundService> _logger;

    public LegacyDbETLBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<LegacyDbETLBackgroundService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<ETLService>();

            try
            {
                _logger.LogInformation("Starting Legacy DB ETL job");

                await service.SyncAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during legacy DB ETL job");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}