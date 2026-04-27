namespace CoursesApplication.Web.Request;

public record EnrollmentRequest(Guid CourseId, string UserId, bool IsPayed);
