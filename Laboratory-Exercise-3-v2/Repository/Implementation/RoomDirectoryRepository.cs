using Domain.Models;
using EFCore.BulkExtensions;
using Repository.Interface;

namespace Repository.Implementation;

public class RoomDirectoryRepository : Repository<Room>,IRoomDirectoryRepository
{
    public RoomDirectoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task BulkInsertOrUpdateRoomDirectories(List<Room> rooms)
    {
        await _context.BulkInsertOrUpdateAsync(rooms);
    }
}