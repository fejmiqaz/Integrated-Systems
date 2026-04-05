namespace Web.Response;

public record AttendanceResponse(
    Guid Id,
    string UserId,
    string FirstName,
    Guid ConsultationId,
    Guid RoomId,
    string LastName,
    string Status,
    string? Comment
);