namespace CoursesApplication.Domain.Dto;

public class ExamSlotDto
{
    public DateTime ScheduledAt { get; set; }
    public string Location { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public Guid CourseId { get; set; }
    public Guid SemesterId { get; set; }
    public string CreatedById { get; set; } = string.Empty;
    public string LastModifiedById { get; set; } = string.Empty;
}
