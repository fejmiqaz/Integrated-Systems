using CoursesApplication.Domain.Dto;
using CoursesApplication.Domain.Models;

namespace CoursesApplication.Service.Interface;

public interface ISemesterService
{
    Task<List<Semester>> GetAllAsync();
    Task<Semester?> GetByIdAsync(Guid id);
    Task<Semester> InsertAsync(SemesterDto dto);
    Task<Semester> UpdateAsync(Guid id, SemesterDto dto);
    Task<Semester> DeleteAsync(Guid id);
    Task<PaginatedResult<Semester>> GetAllPagedAsync(int pageNumber, int pageSize);
}
