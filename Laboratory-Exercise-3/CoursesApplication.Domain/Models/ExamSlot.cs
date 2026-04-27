using CoursesApplication.Domain.Common;

namespace CoursesApplication.Domain.Models;

public class ExamSlot : BaseAuditableEntity<CoursesApplicationUser>
{
    public DateTime ScheduledAt { get; set; }
    public string Location { get; set; } = string.Empty;
    public int Capacity { get; set; }

    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;

    public Guid SemesterId { get; set; }
    public virtual Semester Semester { get; set; } = null!;
}
