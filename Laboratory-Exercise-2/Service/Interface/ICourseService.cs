using Domain.DTO;
using Domain.Models;
using Repository.Implementation;

namespace Service.Interface;

public interface ICourseService
{
    Task<Course> GetByIdNotNullAsync(Guid id);
    Task<List<Course>> GetAllAsync(string? category);
    Task<Course> CreateAsync(CourseDto dto);
    Task<Course> UpdateAsync(Guid id, CourseDto dto);
    Task<Course> DeleteAsync(Guid id);
    Task<PaginatedResult<Course>> GetPagedAsync(int pageNumber, int pageSize);

}