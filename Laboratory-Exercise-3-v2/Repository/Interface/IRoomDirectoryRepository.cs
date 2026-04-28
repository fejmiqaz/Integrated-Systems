using Domain.Models;

namespace Repository.Interface;

public interface IRoomDirectoryRepository
{
    Task BulkInsertOrUpdateRoomDirectories(List<Room> rooms);
}