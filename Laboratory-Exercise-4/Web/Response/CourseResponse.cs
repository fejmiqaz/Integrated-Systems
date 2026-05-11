using Domain.Enums;

namespace Web.Response;

public record CourseResponse(
    Guid Id,
    string Title,
    string Description,
    Category Category,
    int Ects,
    Guid SemesterId,
    string? SemesterName
);