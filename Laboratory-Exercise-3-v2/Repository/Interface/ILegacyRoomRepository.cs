using Domain.Models;

namespace Repository.Interface;

public interface ILegacyRoomRepository
{
    Task<List<Room>> GetRoomsModifiedSinceAsync(
        DateTime since);
    Task<List<Consultation>> GetConsultationsModifiedSinceAsync(
        DateTime since);

}