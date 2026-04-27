using CoursesApplication.Domain.Dto;
using CoursesApplication.Domain.Models;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;

namespace CoursesApplication.Service.Implementation;

public class SemesterService : ISemesterService
{
    private readonly IRepository<Semester> _repository;

    public SemesterService(IRepository<Semester> repository)
    {
        _repository = repository;
    }

    public async Task<List<Semester>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync<Semester>(x => x);
        return result.ToList();
    }

    public async Task<Semester?> GetByIdAsync(Guid id)
    {
        return await _repository.Get<Semester>(x => x, x => x.Id == id);
    }

    public async Task<Semester> InsertAsync(SemesterDto dto)
    {
        var semester = new Semester
        {
            Name = dto.Name,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            CreatedById = dto.CreatedById,
            LastModifiedById = dto.LastModifiedById
        };
        return await _repository.InsertAsync(semester);
    }

    public async Task<Semester> UpdateAsync(Guid id, SemesterDto dto)
    {
        var semester = await _repository.Get<Semester>(x => x, x => x.Id == id)
                       ?? throw new KeyNotFoundException($"Semester {id} not found.");
        semester.Name = dto.Name;
        semester.StartDate = dto.StartDate;
        semester.EndDate = dto.EndDate;
        return await _repository.UpdateAsync(semester);
    }

    public async Task<Semester> DeleteAsync(Guid id)
    {
        var semester = await _repository.Get<Semester>(x => x, x => x.Id == id)
                       ?? throw new KeyNotFoundException($"Semester {id} not found.");
        return await _repository.DeleteAsync(semester);
    }

    public async Task<PaginatedResult<Semester>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync<Semester>(x => x, pageNumber, pageSize);
    }
}
