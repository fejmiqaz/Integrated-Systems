using System.ComponentModel.DataAnnotations;
using HotelApplication.Domain.Enums;

namespace HotelApplication.Web.Request;

public class CreateOrUpdateRoomRequest
{
    [Required] public Status Status;
    [Required] public int RoomNumber;
    [Required] public int Capacity;
    [Required] public double PricePerNight;
    [Required] public Guid HotelId;
}