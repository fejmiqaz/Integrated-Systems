using CoursesApplication.Domain.Models;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Web.Extensions;

public static class EnrollmentExtensions
{
    public static EnrollmentResponse ToResponse(this Enrollment enrollment) =>
        new(enrollment.Id, enrollment.CourseId, enrollment.UserId, enrollment.IsPayed, enrollment.EnrolledAt);
}
