using CoursesApplication.Domain.Models;
using CoursesApplication.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace CoursesApplication.Service.Implementation;

public class ETLService
{
    private readonly ILegacyAcademicPeriodsRepository _legacyAcademicPeriodsRepository;
    private readonly ILegacySubjectCatalogRepository _legacySubjectCatalogRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IRepository<EtlSyncLog> _etlSyncLogRepository;
    private readonly ILogger<ETLService> _logger;

    public ETLService(ILegacyAcademicPeriodsRepository legacyAcademicPeriodsRepository, ILegacySubjectCatalogRepository legacySubjectCatalogRepository, ISemesterRepository semesterRepository, ICourseRepository courseRepository, IRepository<EtlSyncLog> etlSyncLogRepository, ILogger<ETLService> logger)
    {
        _legacyAcademicPeriodsRepository = legacyAcademicPeriodsRepository;
        _legacySubjectCatalogRepository = legacySubjectCatalogRepository;
        _semesterRepository = semesterRepository;
        _courseRepository = courseRepository;
        _etlSyncLogRepository = etlSyncLogRepository;
        _logger = logger;
    }

    public async Task SyncAllAsync()
    {
        var syncLog = new EtlSyncLog
        {
            JobName = "SemesterAsync",
            StartedAt = DateTime.UtcNow
        };

        try
        {
            var lastRun = await _etlSyncLogRepository.GetAllAsync(
                selector: x => x,
                predicate: x => x.JobName == "SemesterAsync" && x.Success == true,
                orderBy: x => x.OrderByDescending(v => v.StartedAt));

            var date = lastRun.FirstOrDefault()?.StartedAt ?? DateTime.MinValue;

            _logger.LogInformation("Starting Legacy DB ETL with date last modified {date}", date);

            var semesters = await _legacyAcademicPeriodsRepository.GetSemestersModifiedSinceAsync(date);
            var courses = await _legacySubjectCatalogRepository.GetCoursesModifiedSinceAsync(date);

            // _logger.LogInformation(
            //     "Extracted and transformed total {venues} Venues, {sections}  Sections, {seats} Seats", semesters.Count,
            //     sections.Count, seats.Count);
            
            _logger.LogInformation(
                "Extracted and transformed total {semesters} Semesters, {courses} Courses.", semesters.Count, courses.Count);

            await _semesterRepository.BulkInsertOrUpdateSemestersAsync(semesters);
            await _courseRepository.BulkInsertOrUpdateCoursesAsync(courses);
            
            _logger.LogInformation("Successfully loaded the data");

            syncLog.Success = true;
            syncLog.CompletedAt = DateTime.UtcNow;
            
            
            _logger.LogInformation("Legacy DB ETL finished successfully at {date}", syncLog.CompletedAt);

        }
        catch (Exception ex)
        {
            syncLog.Success = false;
            syncLog.ErrorMessage = ex.Message;
            syncLog.CompletedAt = DateTime.UtcNow;
            _logger.LogError(ex, "An error occured during the ETL process...");
        }
        finally
        {
            await _etlSyncLogRepository.InsertAsync(syncLog);
        }
    }
    
}