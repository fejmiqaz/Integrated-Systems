using CoursesApplication.Domain.ExternalModels;
using CoursesApplication.Domain.Models;
using CoursesApplication.Repository;
using CoursesApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.Repository.Implementation;

public class LegacyAcademicPeriodsRepository : ILegacyAcademicPeriodsRepository
{
    private readonly LegacyApplicationDbContext _dbContext;

    public LegacyAcademicPeriodsRepository(LegacyApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Semester>> GetSemestersModifiedSinceAsync(DateTime since)
    {
        var legacy = await _dbContext.AcademicPeriods
            .Where(x => x.DateLastModified >= since)
            .ToListAsync();

        return legacy.Select(x => new Semester
        {
            Id = GuidHelper.FromLegacyId("AcademicPeriod", x.PeriodId),
            Name = x.PeriodLabel,
            StartDate = x.StartingDate,
            EndDate = x.ClosingDate,
            CreatedById = "ETL",
            LastModifiedById = "ETL"
        }).ToList();
    }
}