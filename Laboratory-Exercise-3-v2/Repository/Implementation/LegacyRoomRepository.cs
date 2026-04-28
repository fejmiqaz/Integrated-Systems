using Domain.ExternalModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementation;

public class LegacyRoomRepository : ILegacyRoomRepository
{
    private readonly LegacyRoomDbContext _context;

    public LegacyRoomRepository(LegacyRoomDbContext context)
    {
        _context = context;
    }

    public async Task<List<Room>> GetRoomsModifiedSinceAsync(DateTime since)
    {
        var legacy = await _context.RoomDirectories.Where(x => x.UpdatedAt >= since).ToListAsync();

        return legacy.Select(x => new Room()
        {
            Id = GuidHelper.FromLegacyId("Room", x.RoomCode),
            Name = x.RoomName,
            Capacity = x.MaxCapacity,
            
        }).ToList();

    }

    public async Task<List<Consultation>> GetConsultationsModifiedSinceAsync(DateTime since)
    {
        var legacy = await _context.ConsultationSlots.Where(x => x.CreatedAt >= since).ToListAsync();

        return legacy.Select(x => new Consultation()
        {
            Id = GuidHelper.FromLegacyId("Consultation", x.SlotId),
            StartTime = x.SlotStart,
            EndTime = x.SlotEnd,
            RoomId = GuidHelper.FromLegacyId("RoomId", x.RoomCode)
            
        }).ToList();
    }
}