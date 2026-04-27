using CoursesApplication.Domain.Enums;

namespace CoursesApplication.Web.Response;

public record CourseResponse(Guid Id, string Title, string Description, int Ects, Category Category, Guid SemesterId);
