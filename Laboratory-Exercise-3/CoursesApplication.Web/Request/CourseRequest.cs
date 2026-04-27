using CoursesApplication.Domain.Enums;

namespace CoursesApplication.Web.Request;

public record CourseRequest(string Title, string Description, int Ects, Category Category, Guid SemesterId);
