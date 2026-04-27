using CoursesApplication.Domain.Models;

namespace CoursesApplication.Repository.Interface;

public interface ICourseRepository
{
    Task BulkInsertOrUpdateCoursesAsync(List<Course> courses);
}