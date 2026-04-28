using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Interface;

namespace Service.Jobs;

public class DeleteAttendanceBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DeleteAttendanceBackgroundService> _logger;

    public DeleteAttendanceBackgroundService(IServiceScopeFactory scopeFactory,
        ILogger<DeleteAttendanceBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Deletion of Attendances service starting...");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var service = scope.ServiceProvider.GetRequiredService<IAttendanceService>();

                var attendances = await service.GetNotConfirmedAndOlderThan7Days();
                
                _logger.LogInformation($"Total {attendances.Count} attendances were fetched.");
                
                if (attendances.Count > 0)
                {
                    foreach (var attendance in attendances)
                    {
                        await service.DeleteByIdAsync(attendance.Id);
                        _logger.LogInformation($"Attendance with id {attendance.Id} was deleted.");
                    }
                }

            }
            catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Deletion of attendances failed.");
            }
            _logger.LogInformation("Deletion of Attendances job finished.");
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}