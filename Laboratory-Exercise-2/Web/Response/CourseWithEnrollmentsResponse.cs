using Domain.Enums;
using Domain.Models;

namespace Web.Response;

public record CourseWithEnrollmentsResponse(
    Guid Id,
    string Title,
    string Description,
    Category Category,
    int Ects,
    Guid SemesterId,
    string? SemesterName,
    List<EnrollmentResponse> Enrollments
);