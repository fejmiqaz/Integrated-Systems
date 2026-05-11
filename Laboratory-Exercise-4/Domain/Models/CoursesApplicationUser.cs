using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class CoursesApplicationUser : IdentityUser
{
    public required String? FirstName { get; set; }
    public required String? LastName { get; set; }
    public required DateOnly DateOfBirth { get; set; }
}