using Domain.Models;
using EFCore.BulkExtensions;
using Repository.Interface;

namespace Repository.Implementation;

public class ConsultationSlotRepository : Repository<Consultation>, IConsultationSlotRepository
{
    public ConsultationSlotRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task BulkInsertOrUpdateConsultationSlots(List<Consultation> rooms)
    {
        await _context.BulkInsertOrUpdateAsync(rooms);
    }
}