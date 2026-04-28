using Domain.ExternalModels;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class LegacyRoomDbContext : DbContext
{
    public LegacyRoomDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<LegacyRoomDirectory> RoomDirectories { get; set; }
    public DbSet<LegacyConsultationSlots> ConsultationSlots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LegacyRoomDirectory>(e => e.HasKey(v => v.RoomCode));
        modelBuilder.Entity<LegacyConsultationSlots>(e => e.HasKey(v => v.SlotId));

    }
}