using HotelApplication.Domain.Models;
using HotelApplication.Web.Response;

namespace HotelApplication.Web.Extensions;

public static class ReservationMappingExtensions
{
    public static ReservationResponse ToResponse(this Reservation reservation)
    {
        return new ReservationResponse(
            reservation.Id,
            reservation.StartDate,
            reservation.EndDate,
            reservation.ServiceDateTime,
            reservation.UserId,
            reservation.User.FirstName,
            reservation.HotelServiceId,
            reservation.HotelService.Price
        );
    }

    public static List<ReservationResponse> ToResponse(this List<Reservation> reservations)
    {
        return reservations.Select(x => x.ToResponse()).ToList();
    }
}