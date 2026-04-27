using CoursesApplication.Domain.Dto;
using CoursesApplication.Domain.Models;

namespace CoursesApplication.Service.Interface;

public interface IEnrollmentService
{
    Task<List<Enrollment>> GetAllAsync();
    Task<Enrollment?> GetByIdAsync(Guid id);
    Task<Enrollment> InsertAsync(EnrollmentDto dto);
    Task<Enrollment> UpdateAsync(Guid id, EnrollmentDto dto);
    Task<Enrollment> DeleteAsync(Guid id);
    Task<PaginatedResult<Enrollment>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<List<Enrollment>> DeleteLongerAndNotPaidAsync();
}
