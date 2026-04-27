using CoursesApplication.Domain.ExternalModels;
using CoursesApplication.Domain.Models;
using CoursesApplication.Repository;
using CoursesApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EventsManagement.Repository.Implementation;

public class LegacySubjectCatalogRepository : ILegacySubjectCatalogRepository
{
    private readonly LegacyApplicationDbContext _dbContext;

    public LegacySubjectCatalogRepository(LegacyApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Course>> GetCoursesModifiedSinceAsync(DateTime since)
    {
        var legacy = await _dbContext.SubjectCatalogs
            .Where(x => x.DateLastModified >= since)
            .ToListAsync();

        return legacy.Select(x => new Course()
        {
            Id = GuidHelper.FromLegacyId("SubjectCatalogs", x.SubjectCode),
            Title = x.SubjectTitle,
            Description = x.SubjectSummary,
            Ects = x.CreditUnits
        }).ToList();
    }
}