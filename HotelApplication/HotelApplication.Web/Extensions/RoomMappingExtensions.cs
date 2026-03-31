using HotelApplication.Domain.Dto;
using HotelApplication.Domain.Models;
using HotelApplication.Web.Request;
using HotelApplication.Web.Response;

namespace HotelApplication.Web.Extensions;

public static class RoomMappingExtensions
{
    public static RoomResponse? ToResponse(this Room room)
    {
        return new RoomResponse(
            room.Id,
            room.RoomNumber,
            room.Capacity,
            room.Status,
            room.PricePerNight,
            room.HotelId,
            room.Hotel.Name
        );
    }

    public static RoomWithReservationsResponse ToRoomWithReservationsResponse(this Room room)
    {
        return new RoomWithReservationsResponse(
            room.Id,
            room.RoomNumber,
            room.Capacity,
            room.Status,
            room.PricePerNight,
            room.HotelId,
            room.Hotel.Name,
            room.Reservations.ToList().ToResponse()
        );
    }

    public static RoomDto ToDto(this CreateOrUpdateRoomRequest request)
    {
        return new RoomDto()
        {
            Status = request.Status,
            Capacity = request.Capacity,
            RoomNumber = request.RoomNumber,
            PricePerNight = request.PricePerNight,
            HotelId = request.HotelId
        };
    }

    public static List<RoomResponse?> ToResponse(this List<Room> rooms)
    {
        return rooms.Select(x => x.ToResponse()).ToList();
    }

    public static List<RoomWithReservationsResponse> ToRoomWithReservationsResponse(this List<Room> rooms)
    {
        return rooms.Select(x => x.ToRoomWithReservationsResponse()).ToList();
    }

    public static PaginatedResponse<RoomResponse> ToPaginatedResponse(this PaginatedResult<Room> paginatedResult)
    {
        return paginatedResult.ToPaginatedResponse();
    }
    
}