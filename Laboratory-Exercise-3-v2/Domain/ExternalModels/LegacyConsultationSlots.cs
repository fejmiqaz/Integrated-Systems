namespace Domain.ExternalModels;

public class LegacyConsultationSlots
{
    public int SlotId { get; set; }
    public DateTime SlotStart { get; set; }
    public DateTime SlotEnd { get; set; }
    public int RoomCode { get; set; }
    public DateTime CreatedAt { get; set; }
}