namespace HotelApplication.Web.Response;

public record ReservationResponse(
    Guid Id,
    DateTime StartDate,
    DateTime EndDate,
    DateTime? ServiceDateTime,
    string? UserId,
    string UserFullName,
    Guid? HotelServiceId,
    double HotelServicePrice
);