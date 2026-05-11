using Domain.Common;
using Domain.Enums;

namespace Domain.Models;

public class Teaching : BaseEntity
{
   public Role? Role { get; set; }
   
   public Guid SemesterId { get; set; }
   public virtual Semester Semester { get; set; } = null!;
   
   public required Guid CourseId { get; set; }
   public virtual Course Course { get; set; } = null!;
   
   public required String UserId { get; set; }
   public virtual CoursesApplicationUser User { get; set; } = null!;
}