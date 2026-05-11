using Domain.Common;

namespace Domain.Models;

public class Enrollment : BaseEntity
{
    public DateOnly EnrolledAt { get; set; }
    
    public required Guid CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;

    public required String UserId { get; set; } = null!;
    public virtual CoursesApplicationUser User { get; set; } = null!;

}