using CoursesApplication.Domain.Models;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Web.Extensions;

public static class CourseExtensions
{
    public static CourseResponse ToResponse(this Course course) =>
        new(course.Id, course.Title, course.Description, course.Ects, course.Category, course.SemesterId);
}
