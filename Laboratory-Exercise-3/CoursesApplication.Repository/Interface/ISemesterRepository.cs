using CoursesApplication.Domain.Models;

namespace CoursesApplication.Repository.Interface;

public interface ISemesterRepository
{
    Task BulkInsertOrUpdateSemestersAsync(List<Semester> semesters);
}