namespace Web.Response;

public record EnrollmentResponse(
    Guid Id,
    string UserId,
    string UserName,
    DateOnly EnrolledAt
);