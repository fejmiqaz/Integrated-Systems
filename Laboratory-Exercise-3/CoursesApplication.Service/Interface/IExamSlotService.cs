using CoursesApplication.Domain.Dto;
using CoursesApplication.Domain.Models;

namespace CoursesApplication.Service.Interface;

public interface IExamSlotService
{
    Task<List<ExamSlot>> GetAllAsync();
    Task<ExamSlot?> GetByIdAsync(Guid id);
    Task<ExamSlot> InsertAsync(ExamSlotDto dto);
    Task<ExamSlot> UpdateAsync(Guid id, ExamSlotDto dto);
    Task<ExamSlot> DeleteAsync(Guid id);
    Task<PaginatedResult<ExamSlot>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<List<ExamSlot>> GetByCourseAsync(Guid courseId);
}
