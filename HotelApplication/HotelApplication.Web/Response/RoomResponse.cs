using HotelApplication.Domain.Enums;

namespace HotelApplication.Web.Response;

public record RoomResponse(
    Guid Id,
    int RoomNumber,
    int Capacity,
    Status Status,
    double PricePerNight,
    Guid? HotelId,
    string HotelName
);