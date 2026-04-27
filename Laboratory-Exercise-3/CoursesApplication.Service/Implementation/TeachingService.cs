using CoursesApplication.Domain.Dto;
using CoursesApplication.Domain.Models;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;

namespace CoursesApplication.Service.Implementation;

public class TeachingService : ITeachingService
{
    private readonly IRepository<Teaching> _repository;

    public TeachingService(IRepository<Teaching> repository)
    {
        _repository = repository;
    }

    public async Task<List<Teaching>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync<Teaching>(x => x);
        return result.ToList();
    }

    public async Task<Teaching?> GetByIdAsync(Guid id)
    {
        return await _repository.Get<Teaching>(x => x, x => x.Id == id);
    }

    public async Task<Teaching> InsertAsync(TeachingDto dto)
    {
        var teaching = new Teaching
        {
            Role = dto.Role,
            CourseId = dto.CourseId,
            UserId = dto.UserId,
            SemesterId = dto.SemesterId
        };
        return await _repository.InsertAsync(teaching);
    }

    public async Task<Teaching> UpdateAsync(Guid id, TeachingDto dto)
    {
        var teaching = await _repository.Get<Teaching>(x => x, x => x.Id == id)
                       ?? throw new KeyNotFoundException($"Teaching {id} not found.");
        teaching.Role = dto.Role;
        teaching.CourseId = dto.CourseId;
        teaching.UserId = dto.UserId;
        teaching.SemesterId = dto.SemesterId;
        return await _repository.UpdateAsync(teaching);
    }

    public async Task<Teaching> DeleteAsync(Guid id)
    {
        var teaching = await _repository.Get<Teaching>(x => x, x => x.Id == id)
                       ?? throw new KeyNotFoundException($"Teaching {id} not found.");
        return await _repository.DeleteAsync(teaching);
    }

    public async Task<PaginatedResult<Teaching>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync<Teaching>(x => x, pageNumber, pageSize);
    }
}
