using System.ComponentModel.DataAnnotations;

namespace Web.Request;

public record AttendanceRequest(
    [Required] Guid ConsultationId,
    [Required] string UserId,
    [Required] Guid RoomId,
    [Required] string? Comment
    );