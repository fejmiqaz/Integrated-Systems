using CoursesApplication.Domain.Dto;
using CoursesApplication.Service.Interface;
using CoursesApplication.Web.Extensions;
using CoursesApplication.Web.Request;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Web.Mapper;

public class CourseMapper
{
    private readonly ICourseService _service;

    public CourseMapper(ICourseService service)
    {
        _service = service;
    }

    public async Task<List<CourseResponse>> GetAllAsync()
    {
        var courses = await _service.GetAllAsync();
        return courses.Select(c => c.ToResponse()).ToList();
    }

    public async Task<CourseResponse?> GetByIdAsync(Guid id)
    {
        var course = await _service.GetByIdAsync(id);
        return course?.ToResponse();
    }

    public async Task<CourseResponse> InsertAsync(CourseRequest request, string userId)
    {
        var dto = new CourseDto
        {
            Title = request.Title,
            Description = request.Description,
            Ects = request.Ects,
            Category = request.Category,
            SemesterId = request.SemesterId
        };
        var course = await _service.InsertAsync(dto);
        return course.ToResponse();
    }

    public async Task<CourseResponse> UpdateAsync(Guid id, CourseRequest request, string userId)
    {
        var dto = new CourseDto
        {
            Title = request.Title,
            Description = request.Description,
            Ects = request.Ects,
            Category = request.Category,
            SemesterId = request.SemesterId
        };
        var course = await _service.UpdateAsync(id, dto);
        return course.ToResponse();
    }

    public async Task<CourseResponse> DeleteAsync(Guid id)
    {
        var course = await _service.DeleteAsync(id);
        return course.ToResponse();
    }

    public async Task<PaginatedResponse<CourseResponse>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var result = await _service.GetAllPagedAsync(pageNumber, pageSize);
        return result.ToPaginatedResponse(c => c.ToResponse());
    }
}
