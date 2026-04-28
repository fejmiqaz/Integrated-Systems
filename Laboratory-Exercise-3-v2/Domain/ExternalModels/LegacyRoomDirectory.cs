namespace Domain.ExternalModels;

public class LegacyRoomDirectory
{
    public int RoomCode { get; set; }
    public string RoomName { get; set; }
    public int MaxCapacity { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}