namespace CoursesApplication.Web.Response;

public record EnrollmentResponse(Guid Id, Guid CourseId, string UserId, bool IsPayed, DateTime EnrolledAt);
