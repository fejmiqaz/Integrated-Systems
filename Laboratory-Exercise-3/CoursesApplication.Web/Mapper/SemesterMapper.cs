using CoursesApplication.Domain.Dto;
using CoursesApplication.Service.Interface;
using CoursesApplication.Web.Extensions;
using CoursesApplication.Web.Request;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Web.Mapper;

public class SemesterMapper
{
    private readonly ISemesterService _service;

    public SemesterMapper(ISemesterService service)
    {
        _service = service;
    }

    public async Task<List<SemesterResponse>> GetAllAsync()
    {
        var semesters = await _service.GetAllAsync();
        return semesters.Select(s => s.ToResponse()).ToList();
    }

    public async Task<SemesterResponse?> GetByIdAsync(Guid id)
    {
        var semester = await _service.GetByIdAsync(id);
        return semester?.ToResponse();
    }

    public async Task<SemesterResponse> InsertAsync(SemesterRequest request, string userId)
    {
        var dto = new SemesterDto
        {
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreatedById = userId,
            LastModifiedById = userId
        };
        var semester = await _service.InsertAsync(dto);
        return semester.ToResponse();
    }

    public async Task<SemesterResponse> UpdateAsync(Guid id, SemesterRequest request, string userId)
    {
        var dto = new SemesterDto
        {
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreatedById = userId,
            LastModifiedById = userId
        };
        var semester = await _service.UpdateAsync(id, dto);
        return semester.ToResponse();
    }

    public async Task<SemesterResponse> DeleteAsync(Guid id)
    {
        var semester = await _service.DeleteAsync(id);
        return semester.ToResponse();
    }

    public async Task<PaginatedResponse<SemesterResponse>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var result = await _service.GetAllPagedAsync(pageNumber, pageSize);
        return result.ToPaginatedResponse(s => s.ToResponse());
    }
}
