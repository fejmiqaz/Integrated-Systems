using CoursesApplication.Domain.Models;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Web.Extensions;

public static class SemesterExtensions
{
    public static SemesterResponse ToResponse(this Semester semester) =>
        new(semester.Id, semester.Name, semester.StartDate, semester.EndDate);
}
