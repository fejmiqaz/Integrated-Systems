using CoursesApplication.Web.Mapper;
using CoursesApplication.Web.Request;
using CoursesApplication.Web.Response;
using Microsoft.AspNetCore.Mvc;

namespace CoursesApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachingController : ControllerBase
{
    private readonly TeachingMapper _mapper;

    public TeachingController(TeachingMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<TeachingResponse>>> GetAll() =>
        Ok(await _mapper.GetAllAsync());

    [HttpGet("paged")]
    public async Task<ActionResult<PaginatedResponse<TeachingResponse>>> GetPaged([FromQuery] PaginatedRequest request) =>
        Ok(await _mapper.GetAllPagedAsync(request.PageNumber, request.PageSize));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TeachingResponse>> GetById(Guid id)
    {
        var response = await _mapper.GetByIdAsync(id);
        if (response is null) return NotFound();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<TeachingResponse>> Create([FromBody] TeachingRequest request)
    {
        var response = await _mapper.InsertAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TeachingResponse>> Update(Guid id, [FromBody] TeachingRequest request) =>
        Ok(await _mapper.UpdateAsync(id, request));

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<TeachingResponse>> Delete(Guid id) =>
        Ok(await _mapper.DeleteAsync(id));
}
