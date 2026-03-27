using Domain.Models;
using Web.Response;

namespace Web.Extensions;

public static class EnrollmentMappingExtensions
{
    public static EnrollmentResponse ToResponse(this Enrollment e)
    {
        return new EnrollmentResponse(
            e.Id,
            e.UserId,
            $"{e.User.FirstName} {e.User.LastName}",
            e.EnrolledAt
        );
    }
    
    public static List<EnrollmentResponse> ToResponse(this IEnumerable<Enrollment> enrollments)
    {
        return enrollments.Select(x => x.ToResponse()).ToList();
    }

}