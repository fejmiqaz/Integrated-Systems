using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using Web.Mapper;
using Web.Request;
using Web.Response;

namespace Web.Controllers;

[Route("/api/consultation")]
[ApiController]
public class ConsultationController : ControllerBase
{
    private readonly ConsultationMapper _consultationMapper;

    public ConsultationController(ConsultationMapper consultationMapper)
    {
        _consultationMapper = consultationMapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? roomName, [FromQuery] DateOnly? date)
    {
        var result = await _consultationMapper.GetAllAsync(roomName, date);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _consultationMapper.GetByIdNotNullAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ConsultationRequest request)
    {
        var result = await _consultationMapper.InsertAsync(request);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ConsultationRequest request)
    {
        var result = await _consultationMapper.UpdateAsync(id, request);
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _consultationMapper.DeleteAsync(id);
        return Ok(result);
    }

    [HttpGet("paged")]
    public async Task<PaginatedResponse<ConsultationResponse>> Paged(
        [FromQuery] PaginatedRequest request)
    {
        return await _consultationMapper.GetPagedAsync(request);
    }
    
    
    
}