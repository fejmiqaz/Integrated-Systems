using CoursesApplication.Domain.Dto;
using CoursesApplication.Service.Interface;
using CoursesApplication.Web.Extensions;
using CoursesApplication.Web.Request;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Web.Mapper;

public class TeachingMapper
{
    private readonly ITeachingService _service;

    public TeachingMapper(ITeachingService service)
    {
        _service = service;
    }

    public async Task<List<TeachingResponse>> GetAllAsync()
    {
        var teachings = await _service.GetAllAsync();
        return teachings.Select(t => t.ToResponse()).ToList();
    }

    public async Task<TeachingResponse?> GetByIdAsync(Guid id)
    {
        var teaching = await _service.GetByIdAsync(id);
        return teaching?.ToResponse();
    }

    public async Task<TeachingResponse> InsertAsync(TeachingRequest request)
    {
        var dto = new TeachingDto
        {
            Role = request.Role,
            CourseId = request.CourseId,
            UserId = request.UserId,
            SemesterId = request.SemesterId
        };
        var teaching = await _service.InsertAsync(dto);
        return teaching.ToResponse();
    }

    public async Task<TeachingResponse> UpdateAsync(Guid id, TeachingRequest request)
    {
        var dto = new TeachingDto
        {
            Role = request.Role,
            CourseId = request.CourseId,
            UserId = request.UserId,
            SemesterId = request.SemesterId
        };
        var teaching = await _service.UpdateAsync(id, dto);
        return teaching.ToResponse();
    }

    public async Task<TeachingResponse> DeleteAsync(Guid id)
    {
        var teaching = await _service.DeleteAsync(id);
        return teaching.ToResponse();
    }

    public async Task<PaginatedResponse<TeachingResponse>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var result = await _service.GetAllPagedAsync(pageNumber, pageSize);
        return result.ToPaginatedResponse(t => t.ToResponse());
    }
}
