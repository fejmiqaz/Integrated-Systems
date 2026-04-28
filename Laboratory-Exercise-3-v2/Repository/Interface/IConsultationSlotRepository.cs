using Domain.Models;

namespace Repository.Interface;

public interface IConsultationSlotRepository
{
    Task BulkInsertOrUpdateConsultationSlots(List<Consultation> rooms);

}