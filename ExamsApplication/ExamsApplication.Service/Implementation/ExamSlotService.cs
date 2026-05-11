using ExamsApplication.Domain.Dto;
using ExamsApplication.Domain.Models;
using ExamsApplication.Repository.Interfaces;
using ExamsApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExamsApplication.Service.Implementation;

public class ExamSlotService : IExamSlotService
{
    private readonly IRepository<ExamSlot> _repository;

    public ExamSlotService(IRepository<ExamSlot> repository)
    {
        _repository = repository;
    }

    public async Task<ExamSlot> GetByIdNotNullAsync(Guid id)
    {
        var result = await _repository.GetAsync(
            selector: x => x,
            predicate: x => x.Id == id
        );
        
        if (result == null)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public async Task<List<ExamSlot>> GetAllAsync(string? sessionType)
    {
        var result = await _repository.GetAllAsync(
            selector: x=> x,
            predicate: x=> x.SessionType.GetType() == sessionType.GetType(),
            include: e => e.Include(s => s.SessionType.GetType())
            );
        return result.ToList();
    }

    public async Task<ExamSlot> CreateAsync(ExamSlotDto dto)
    {
        var examToAdd = new ExamSlot(
            StartTime = 
            );
    }
    
    public Task<ExamSlot> UpdateAsync(Guid id, ExamSlotDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ExamSlot> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<ExamSlot>> GetPagedAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
}