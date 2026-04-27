using CoursesApplication.Domain.Models;

namespace CoursesApplication.Repository.Interface;

public interface ILegacySubjectCatalogRepository
{
    Task<List<Course>> GetCoursesModifiedSinceAsync(DateTime since);

}