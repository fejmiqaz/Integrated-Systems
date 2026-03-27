using Domain.Common;
using Domain.Enums;

namespace Domain.Models;

public class Course : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int Ects { get; set; }
    public Category Category { get; set; }
    
    public Guid SemesterId { get; set; }
    public virtual Semester Semester { get; set; } = null!;

    public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}