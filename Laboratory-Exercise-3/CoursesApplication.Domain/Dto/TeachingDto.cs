using CoursesApplication.Domain.Enums;

namespace CoursesApplication.Domain.Dto;

public class TeachingDto
{
    public Role Role { get; set; }
    public Guid CourseId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid SemesterId { get; set; }
}
