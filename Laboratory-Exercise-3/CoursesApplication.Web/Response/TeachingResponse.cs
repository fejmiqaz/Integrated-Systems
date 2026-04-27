using CoursesApplication.Domain.Enums;

namespace CoursesApplication.Web.Response;

public record TeachingResponse(Guid Id, Role Role, Guid CourseId, string UserId, Guid SemesterId);
