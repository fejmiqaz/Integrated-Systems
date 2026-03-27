using Microsoft.AspNetCore.Mvc;
using Web.Mapper;
using Web.Request;
using Web.Response;

namespace Web.Controllers;

[Route("api/courses")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly CourseMapper _courseMapper;

    public CourseController(CourseMapper courseMapper)
    {
        _courseMapper = courseMapper;
    }
    // GET /{id} - returns a Course by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllById([FromRoute] Guid id)
    {
        var result = await _courseMapper.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
    
    // GET /?category - returns all Courses; supports optional query parameter category for filtering

    [HttpGet]
    public async Task<IActionResult> GetAllByCategory([FromQuery] string category)
    {
        var result = await _courseMapper.GetAll(category);
        
        return Ok(result);
    }
    
    // POST / - creates a new Course from the request body

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdateCourseRequest courseRequest)
    {
        var result = await _courseMapper.InsertAsync(courseRequest);
        return Ok(result);
    }
    
    // PUT /{id} - updates an existing Course with the data from the request body

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateOrUpdateCourseRequest courseRequest)
    {
        var result = await _courseMapper.UpdateAsync(id, courseRequest);
        return Ok(result);
    }
    
    // DELETE /{id} - deletes an existing Course by ID

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _courseMapper.DeleteAsync(id);
        return Ok(result);
    }
    
    // GET /?pageNumber=&pageSize - returns paginated results; required parameters: pageNumber, pageSize (PaginatedRequest)

    [HttpGet("paged")]
    public async Task<PaginatedResponse<CourseWithEnrollmentsResponse>> GetPaged([FromQuery] PaginateRequest request)
    {
        var result = await _courseMapper.GetPaged(request);
        return result;
    }
    
}   