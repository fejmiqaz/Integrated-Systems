using Domain.Common;

namespace Domain.Models;

public class User : BaseEntity
{
    public String? FirstName { get; set; }
    public String? LastName { get; set; }
    public String? Email { get; set; }
    public DateOnly DateOfBirth { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();
}