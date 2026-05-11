using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Web.Mapper;
using Web.Response;

namespace Web.Controllers;

[Route("api/examattempt")]
[ApiController]
public class ExamAttemptController : ControllerBase
{
    private readonly ExamAttemptMapper _examAttemptMapper;

    public ExamAttemptController(ExamAttemptMapper examAttemptMapper)
    {
        _examAttemptMapper = examAttemptMapper;
    }

    // API LAB 4
    [EnableRateLimiting("external-api")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ExamAttemptResponse>> GetById(Guid id)
    {
        var response = await _examAttemptMapper.GetByIdAsync(id);
        return Ok(response);
    }
}