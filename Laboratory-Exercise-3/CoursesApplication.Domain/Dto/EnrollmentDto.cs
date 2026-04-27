namespace CoursesApplication.Domain.Dto;

public class EnrollmentDto
{
    public Guid CourseId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public bool IsPayed { get; set; }
    public string CreatedById { get; set; } = string.Empty;
    public string LastModifiedById { get; set; } = string.Empty;
}
