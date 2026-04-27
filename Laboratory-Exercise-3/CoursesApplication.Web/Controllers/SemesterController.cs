using System.Security.Claims;
using CoursesApplication.Web.Mapper;
using CoursesApplication.Web.Request;
using CoursesApplication.Web.Response;
using Microsoft.AspNetCore.Mvc;

namespace CoursesApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SemesterController : ControllerBase
{
    private readonly SemesterMapper _mapper;

    public SemesterController(SemesterMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<SemesterResponse>>> GetAll() =>
        Ok(await _mapper.GetAllAsync());

    [HttpGet("paged")]
    public async Task<ActionResult<PaginatedResponse<SemesterResponse>>> GetPaged([FromQuery] PaginatedRequest request) =>
        Ok(await _mapper.GetAllPagedAsync(request.PageNumber, request.PageSize));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SemesterResponse>> GetById(Guid id)
    {
        var response = await _mapper.GetByIdAsync(id);
        if (response is null) return NotFound();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<SemesterResponse>> Create([FromBody] SemesterRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var response = await _mapper.InsertAsync(request, userId);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SemesterResponse>> Update(Guid id, [FromBody] SemesterRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        return Ok(await _mapper.UpdateAsync(id, request, userId));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<SemesterResponse>> Delete(Guid id) =>
        Ok(await _mapper.DeleteAsync(id));
}
