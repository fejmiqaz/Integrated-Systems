using CoursesApplication.Domain.Dto;
using CoursesApplication.Service.Interface;
using CoursesApplication.Web.Extensions;
using CoursesApplication.Web.Request;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Web.Mapper;

public class EnrollmentMapper
{
    private readonly IEnrollmentService _service;

    public EnrollmentMapper(IEnrollmentService service)
    {
        _service = service;
    }

    public async Task<List<EnrollmentResponse>> GetAllAsync()
    {
        var enrollments = await _service.GetAllAsync();
        return enrollments.Select(e => e.ToResponse()).ToList();
    }

    public async Task<EnrollmentResponse?> GetByIdAsync(Guid id)
    {
        var enrollment = await _service.GetByIdAsync(id);
        return enrollment?.ToResponse();
    }

    public async Task<EnrollmentResponse> InsertAsync(EnrollmentRequest request, string userId)
    {
        var dto = new EnrollmentDto
        {
            CourseId = request.CourseId,
            UserId = request.UserId,
            IsPayed = request.IsPayed,
            CreatedById = userId,
            LastModifiedById = userId
        };
        var enrollment = await _service.InsertAsync(dto);
        return enrollment.ToResponse();
    }

    public async Task<EnrollmentResponse> UpdateAsync(Guid id, EnrollmentRequest request, string userId)
    {
        var dto = new EnrollmentDto
        {
            CourseId = request.CourseId,
            UserId = request.UserId,
            IsPayed = request.IsPayed,
            CreatedById = userId,
            LastModifiedById = userId
        };
        var enrollment = await _service.UpdateAsync(id, dto);
        return enrollment.ToResponse();
    }

    public async Task<EnrollmentResponse> DeleteAsync(Guid id)
    {
        var enrollment = await _service.DeleteAsync(id);
        return enrollment.ToResponse();
    }

    public async Task<PaginatedResponse<EnrollmentResponse>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var result = await _service.GetAllPagedAsync(pageNumber, pageSize);
        return result.ToPaginatedResponse(e => e.ToResponse());
    }
}
