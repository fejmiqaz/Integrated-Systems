using System.Security.Claims;
using CoursesApplication.Web.Mapper;
using CoursesApplication.Web.Request;
using CoursesApplication.Web.Response;
using Microsoft.AspNetCore.Mvc;

namespace CoursesApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamSlotController : ControllerBase
{
    private readonly ExamSlotMapper _mapper;

    public ExamSlotController(ExamSlotMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ExamSlotResponse>>> GetAll() =>
        Ok(await _mapper.GetAllAsync());

    [HttpGet("paged")]
    public async Task<ActionResult<PaginatedResponse<ExamSlotResponse>>> GetPaged([FromQuery] PaginatedRequest request) =>
        Ok(await _mapper.GetAllPagedAsync(request.PageNumber, request.PageSize));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ExamSlotResponse>> GetById(Guid id)
    {
        var response = await _mapper.GetByIdAsync(id);
        if (response is null) return NotFound();
        return Ok(response);
    }

    [HttpGet("course/{courseId:guid}")]
    public async Task<ActionResult<List<ExamSlotResponse>>> GetByCourse(Guid courseId) =>
        Ok(await _mapper.GetByCourseAsync(courseId));

    [HttpPost]
    public async Task<ActionResult<ExamSlotResponse>> Create([FromBody] ExamSlotRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var response = await _mapper.InsertAsync(request, userId);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ExamSlotResponse>> Update(Guid id, [FromBody] ExamSlotRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        return Ok(await _mapper.UpdateAsync(id, request, userId));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ExamSlotResponse>> Delete(Guid id) =>
        Ok(await _mapper.DeleteAsync(id));
}
