using CoursesApplication.Domain.Dto;
using CoursesApplication.Domain.Models;

namespace CoursesApplication.Service.Interface;

public interface ITeachingService
{
    Task<List<Teaching>> GetAllAsync();
    Task<Teaching?> GetByIdAsync(Guid id);
    Task<Teaching> InsertAsync(TeachingDto dto);
    Task<Teaching> UpdateAsync(Guid id, TeachingDto dto);
    Task<Teaching> DeleteAsync(Guid id);
    Task<PaginatedResult<Teaching>> GetAllPagedAsync(int pageNumber, int pageSize);
}
