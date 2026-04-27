using CoursesApplication.Domain.Models;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Web.Extensions;

public static class TeachingExtensions
{
    public static TeachingResponse ToResponse(this Teaching teaching) =>
        new(teaching.Id, teaching.Role, teaching.CourseId, teaching.UserId, teaching.SemesterId);
}
