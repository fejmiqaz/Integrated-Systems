using CoursesApplication.Domain.Models;
using CoursesApplication.Repository;
using CoursesApplication.Repository.Interface;
using EFCore.BulkExtensions;

namespace EventsManagement.Repository.Implementation;

public class SemesterRepository : Repository<Semester>, ISemesterRepository
{
    public SemesterRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task BulkInsertOrUpdateSemestersAsync(List<Semester> semesters)
    {
        _context.BulkInsertOrUpdate(semesters);
    }
}