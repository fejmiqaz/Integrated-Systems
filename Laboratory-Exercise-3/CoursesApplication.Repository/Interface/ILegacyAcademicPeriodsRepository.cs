using CoursesApplication.Domain.Models;

namespace CoursesApplication.Repository.Interface;

public interface ILegacyAcademicPeriodsRepository
{
    Task<List<Semester>> GetSemestersModifiedSinceAsync(DateTime since);

}