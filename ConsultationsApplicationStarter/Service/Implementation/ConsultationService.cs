using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class ConsultationService : IConsultationService
{
    private readonly IRepository<Consultation> _repository;

    public ConsultationService(IRepository<Consultation> repository)
    {
        _repository = repository;
    }

    public async Task<Consultation> GetByIdNotNullAsync(Guid id)
    {
        var result = await GetByIdAsync(id);
        return result ?? throw new InvalidOperationException("Consultation not found");
    }

    public async Task<Consultation?> GetByIdAsync(Guid id)
    {
        return await _repository.GetAsync(
            selector: x => x,
            predicate: x => x.Id == id
        );
    }

    public async Task<List<Consultation>> GetAllAsync(string? roomName, DateOnly? date)
    {
        var result = await _repository.GetAllAsync(
            selector: x => x,
            predicate: x => (roomName == null || x.Room.Name == roomName) && (date == null || DateOnly.FromDateTime(x.StartTime.Date) == date)
        );
        return result.ToList();
    }

    public async Task<Consultation> CreateAsync(DateTime startTime, DateTime endTime, Guid roomId)
    {
        var consultationToAdd = new Consultation()
        {
            StartTime = startTime,
            EndTime = endTime,
            RoomId = roomId,
            RegisteredStudents = 0
        };
        return await _repository.InsertAsync(consultationToAdd);
    }

    public async Task<Consultation> UpdateAsync(Guid id, DateTime startTime, DateTime endTime, Guid roomId)
    {
        var consultationToUpdate = await GetByIdNotNullAsync(id);
        consultationToUpdate.StartTime = startTime;
        consultationToUpdate.EndTime = endTime;
        consultationToUpdate.RoomId = roomId;

        return await _repository.UpdateAsync(consultationToUpdate);
    }

    public async Task<Consultation> DeleteByIdAsync(Guid id)
    {
        var consultationToDelete = await GetByIdNotNullAsync(id);
        
        if (consultationToDelete.RegisteredStudents > 0)
        {
            throw new InvalidOperationException(
                "Consultation cannot be deleted because there are registered students.");
        }
        
        return await _repository.DeleteAsync(consultationToDelete);
    }

    public async Task<PaginatedResult<Consultation>> GetPagedAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber < 0 ? 0 : pageNumber;
        pageSize = pageSize < 0 ? 10 : pageSize;
        pageSize = pageSize > 100 ? 100 : pageSize;
        
        return await _repository.GetAllPagedAsync(
            selector: x=> x,
            pageNumber: pageNumber,
             pageSize: pageSize,
            include: q => q.Include(c => c.Attendances),
            orderBy: q => q.OrderBy(c => c.RoomId)
        );
    }
}