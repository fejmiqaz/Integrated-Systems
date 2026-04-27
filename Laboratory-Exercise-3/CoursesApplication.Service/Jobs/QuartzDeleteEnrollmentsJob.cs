using CoursesApplication.Service.Interface;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CoursesApplication.Service.Jobs;

[DisallowConcurrentExecution]
public class QuartzDeleteEnrollmentsJob : IJob
{
    private readonly IEnrollmentService _enrollmentService;
    private readonly ILogger<QuartzDeleteEnrollmentsJob> _logger;

    public QuartzDeleteEnrollmentsJob(IEnrollmentService enrollmentService, ILogger<QuartzDeleteEnrollmentsJob> logger)
    {
        _enrollmentService = enrollmentService;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Deletion of Enrollments started...");

        try
        {
            var enrollments = await _enrollmentService.DeleteLongerAndNotPaidAsync();

            _logger.LogInformation($"Fetched enrollments to be deleted {enrollments.Count}");

            foreach (var enrollment in enrollments)
            {
                await _enrollmentService.DeleteAsync(enrollment.Id);
                _logger.LogInformation($"Deleted Enrollment with Id: {enrollment.Id}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Exception {ex.Message}");
        }
        
        _logger.LogInformation("Deletion job finished...");
        
    }
}