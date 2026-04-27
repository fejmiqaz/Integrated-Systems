using CoursesApplication.Domain.Dto;
using CoursesApplication.Domain.Models;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoursesApplication.Service.Implementation;

public class EnrollmentService : IEnrollmentService
{
    private readonly IRepository<Enrollment> _repository;

    public EnrollmentService(IRepository<Enrollment> repository)
    {
        _repository = repository;
    }

    public async Task<List<Enrollment>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync<Enrollment>(x => x);
        return result.ToList();
    }

    public async Task<Enrollment?> GetByIdAsync(Guid id)
    {
        return await _repository.Get<Enrollment>(x => x, x => x.Id == id);
    }

    public async Task<Enrollment> InsertAsync(EnrollmentDto dto)
    {
        var enrollment = new Enrollment
        {
            CourseId = dto.CourseId,
            UserId = dto.UserId,
            IsPayed = dto.IsPayed,
            EnrolledAt = DateTime.UtcNow,
            CreatedById = dto.CreatedById,
            LastModifiedById = dto.LastModifiedById
        };
        return await _repository.InsertAsync(enrollment);
    }

    public async Task<Enrollment> UpdateAsync(Guid id, EnrollmentDto dto)
    {
        var enrollment = await _repository.Get<Enrollment>(x => x, x => x.Id == id)
                         ?? throw new KeyNotFoundException($"Enrollment {id} not found.");
        enrollment.CourseId = dto.CourseId;
        enrollment.UserId = dto.UserId;
        enrollment.IsPayed = dto.IsPayed;
        return await _repository.UpdateAsync(enrollment);
    }

    public async Task<Enrollment> DeleteAsync(Guid id)
    {
        var enrollment = await _repository.Get<Enrollment>(x => x, x => x.Id == id)
                         ?? throw new KeyNotFoundException($"Enrollment {id} not found.");
        return await _repository.DeleteAsync(enrollment);
    }

    public async Task<PaginatedResult<Enrollment>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync<Enrollment>(x => x, pageNumber, pageSize);
    }
    
    public async Task<List<Enrollment>> DeleteLongerAndNotPaidAsync()
    {
        var result = await _repository.GetAllAsync(
            selector: x => x,
            predicate: x => x.DateCreated < DateTime.Now.AddSeconds(-30) && !x.IsPayed);
        return result.ToList();
    }
}
