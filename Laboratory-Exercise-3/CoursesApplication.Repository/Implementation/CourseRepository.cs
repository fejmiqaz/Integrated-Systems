using CoursesApplication.Domain.Models;
using CoursesApplication.Repository;
using CoursesApplication.Repository.Interface;
using EFCore.BulkExtensions;

namespace EventsManagement.Repository.Implementation;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task BulkInsertOrUpdateCoursesAsync(List<Course> courses)
    { 
        _context.BulkInsertOrUpdate(courses);
    }
}