
using Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Web.Mapper;
using Web.Request;
using Web.Response;

namespace Web.Controllers;

[Route("api/attendance")]
[ApiController]
public class AttendanceController : ControllerBase
{
    private readonly AttendanceMapper _attendanceMapper;

    public AttendanceController(AttendanceMapper attendanceMapper)
    {
        _attendanceMapper = attendanceMapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? dateAfter)
    {
        var result = await _attendanceMapper.GetAllAsync(dateAfter);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AttendanceRequest request)
    {
        var result = await _attendanceMapper.RegisterAsync(request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _attendanceMapper.DeleteByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("consultation/{consultationId}")]
    public async Task<IActionResult> GetByConsultation([FromRoute] Guid consultationId)
    {
        var result = await _attendanceMapper.GetAllByConsultationIdAsync(consultationId);
        return Ok(result);
    }

    [HttpPatch("{id}/mark-as-absent")]
    public async Task<IActionResult> MarkAbsent([FromRoute] Guid id)
    {
        var result = await _attendanceMapper.MarkAsAbsentAsync(id);
        return Ok(result);
    }

    [HttpPost("{id}/cancelation-reason")]
    public async Task<IActionResult> UploadDocumentById([FromRoute] Guid id, [FromForm] IFormFile file)
    {
        var result = await _attendanceMapper.UploadReasonByIdInFileSystemAsync(id, file);
        return Ok(result);
    }
    
}