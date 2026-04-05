using System.ComponentModel.DataAnnotations;

namespace Web.Request;

public record ConsultationRequest(
    [Required] Guid RoomId,
    [Required] DateTime StartTime,
    [Required] DateTime EndTime
    );