using CoursesApplication.Domain.Dto;
using CoursesApplication.Domain.Models;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;

namespace CoursesApplication.Service.Implementation;

public class ExamSlotService : IExamSlotService
{
    private readonly IRepository<ExamSlot> _repository;

    public ExamSlotService(IRepository<ExamSlot> repository)
    {
        _repository = repository;
    }

    public async Task<List<ExamSlot>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync<ExamSlot>(x => x);
        return result.ToList();
    }

    public async Task<ExamSlot?> GetByIdAsync(Guid id)
    {
        return await _repository.Get<ExamSlot>(x => x, x => x.Id == id);
    }

    public async Task<ExamSlot> InsertAsync(ExamSlotDto dto)
    {
        var examSlot = new ExamSlot
        {
            ScheduledAt = dto.ScheduledAt,
            Location = dto.Location,
            Capacity = dto.Capacity,
            CourseId = dto.CourseId,
            SemesterId = dto.SemesterId,
            CreatedById = dto.CreatedById,
            LastModifiedById = dto.LastModifiedById
        };
        return await _repository.InsertAsync(examSlot);
    }

    public async Task<ExamSlot> UpdateAsync(Guid id, ExamSlotDto dto)
    {
        var examSlot = await _repository.Get<ExamSlot>(x => x, x => x.Id == id)
                       ?? throw new KeyNotFoundException($"ExamSlot {id} not found.");
        examSlot.ScheduledAt = dto.ScheduledAt;
        examSlot.Location = dto.Location;
        examSlot.Capacity = dto.Capacity;
        examSlot.CourseId = dto.CourseId;
        examSlot.SemesterId = dto.SemesterId;
        return await _repository.UpdateAsync(examSlot);
    }

    public async Task<ExamSlot> DeleteAsync(Guid id)
    {
        var examSlot = await _repository.Get<ExamSlot>(x => x, x => x.Id == id)
                       ?? throw new KeyNotFoundException($"ExamSlot {id} not found.");
        return await _repository.DeleteAsync(examSlot);
    }

    public async Task<PaginatedResult<ExamSlot>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync<ExamSlot>(x => x, pageNumber, pageSize);
    }

    public async Task<List<ExamSlot>> GetByCourseAsync(Guid courseId)
    {
        var result = await _repository.GetAllAsync<ExamSlot>(x => x, x => x.CourseId == courseId);
        return result.ToList();
    }
}
