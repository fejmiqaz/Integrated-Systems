namespace CoursesApplication.Domain.Dto;

public class SemesterDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string CreatedById { get; set; } = string.Empty;
    public string LastModifiedById { get; set; } = string.Empty;
}
