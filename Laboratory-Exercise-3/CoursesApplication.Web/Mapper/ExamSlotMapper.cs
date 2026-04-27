using CoursesApplication.Domain.Dto;
using CoursesApplication.Service.Interface;
using CoursesApplication.Web.Extensions;
using CoursesApplication.Web.Request;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Web.Mapper;

public class ExamSlotMapper
{
    private readonly IExamSlotService _service;

    public ExamSlotMapper(IExamSlotService service)
    {
        _service = service;
    }

    public async Task<List<ExamSlotResponse>> GetAllAsync()
    {
        var slots = await _service.GetAllAsync();
        return slots.Select(s => s.ToResponse()).ToList();
    }

    public async Task<ExamSlotResponse?> GetByIdAsync(Guid id)
    {
        var slot = await _service.GetByIdAsync(id);
        return slot?.ToResponse();
    }

    public async Task<List<ExamSlotResponse>> GetByCourseAsync(Guid courseId)
    {
        var slots = await _service.GetByCourseAsync(courseId);
        return slots.Select(s => s.ToResponse()).ToList();
    }

    public async Task<ExamSlotResponse> InsertAsync(ExamSlotRequest request, string userId)
    {
        var dto = new ExamSlotDto
        {
            ScheduledAt = request.ScheduledAt,
            Location = request.Location,
            Capacity = request.Capacity,
            CourseId = request.CourseId,
            SemesterId = request.SemesterId,
            CreatedById = userId,
            LastModifiedById = userId
        };
        var slot = await _service.InsertAsync(dto);
        return slot.ToResponse();
    }

    public async Task<ExamSlotResponse> UpdateAsync(Guid id, ExamSlotRequest request, string userId)
    {
        var dto = new ExamSlotDto
        {
            ScheduledAt = request.ScheduledAt,
            Location = request.Location,
            Capacity = request.Capacity,
            CourseId = request.CourseId,
            SemesterId = request.SemesterId,
            CreatedById = userId,
            LastModifiedById = userId
        };
        var slot = await _service.UpdateAsync(id, dto);
        return slot.ToResponse();
    }

    public async Task<ExamSlotResponse> DeleteAsync(Guid id)
    {
        var slot = await _service.DeleteAsync(id);
        return slot.ToResponse();
    }

    public async Task<PaginatedResponse<ExamSlotResponse>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var result = await _service.GetAllPagedAsync(pageNumber, pageSize);
        return result.ToPaginatedResponse(s => s.ToResponse());
    }
}
