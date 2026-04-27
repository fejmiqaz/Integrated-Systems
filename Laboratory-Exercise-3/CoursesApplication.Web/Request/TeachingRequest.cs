using CoursesApplication.Domain.Enums;

namespace CoursesApplication.Web.Request;

public record TeachingRequest(Role Role, Guid CourseId, string UserId, Guid SemesterId);
