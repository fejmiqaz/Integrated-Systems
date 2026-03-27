using Service.Interface;
using Web.Response;
using System.CodeDom;
using Web.Extensions;
using Web.Request;

namespace Web.Mapper;

public class CourseMapper
{
    private readonly ICourseService _courseService;

    public CourseMapper(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    public async Task<CourseResponse?> GetById(Guid id)
    {
        var result = await _courseService.GetByIdNotNullAsync(id);
        return result.ToResponse();
    }
    
    public async Task<List<CourseResponse>> GetAll(string? category)
    {
        var result = await _courseService.GetAllAsync(category);
        return result.ToResponse();
    }
    
    public async Task<CourseResponse> InsertAsync(CreateOrUpdateCourseRequest request)
    {
        var dto = request.ToDto();
        
        var result = await _courseService.CreateAsync(dto);
        
        return result.ToResponse();
    }
    
    public async Task<CourseResponse> UpdateAsync(Guid id, CreateOrUpdateCourseRequest request)
    {
        var dto = request.ToDto();
        var result = await _courseService.UpdateAsync(id, dto);
        return result?.ToResponse();
    }

    public async Task<CourseResponse> DeleteAsync(Guid id)
    {
        var result = await _courseService.DeleteAsync(id);
        return result?.ToResponse();
    }

    public async Task<PaginatedResponse<CourseWithEnrollmentsResponse>> GetPaged(PaginateRequest request)
    {
        var result = await _courseService.GetPagedAsync(request.PageNumber, request.PageSize);
        return result.ToPaginatedResponse();
    }



    
}