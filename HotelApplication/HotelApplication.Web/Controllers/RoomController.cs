using HotelApplication.Web.Mappers;
using HotelApplication.Web.Response;
using Microsoft.AspNetCore.Mvc;
using HotelApplication.Web.Request;
using Microsoft.AspNetCore.Authorization;

namespace HotelApplication.Web.Controllers;


[Route("api/rooms")]
[ApiController]

public class RoomController : ControllerBase
{
    private readonly RoomMapper _roomMapper;

    public RoomController(RoomMapper roomMapper)
    {
        _roomMapper = roomMapper;
    }
    
    // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    // [HttpGet]
    // public async Task<List<RoomResponse>> GetAll([FromQuery] int? status)
    // {
    //     return await _roomMapper.GetAll(status);
    // }
    // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? status, [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize)
    {
        if (pageNumber.HasValue && pageSize.HasValue)
        {
            var request = new PaginatedRequest()
            {
                PageNumber = pageNumber.Value,
                PageSize = pageSize.Value
            };
            var pagedResult = await _roomMapper.PaginatedGetAllAsync(request);
            return Ok(pagedResult);
        }

        var result = await _roomMapper.GetAll(status);
        return Ok(result);
    }
    
    // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    // [HttpGet("paged")]
    // public async Task<PaginatedResponse<RoomResponse>> Paged([FromQuery] PaginatedRequest request)
    // {
    //     return await _roomMapper.PaginatedGetAllAsync(request);
    // }
    // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    
    // /api/rooms/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _roomMapper.GetById(id);
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CreateOrUpdateRoomRequest eventRequest)
    {
        var result = await _roomMapper.InsertAsync(eventRequest);
        return Ok(result);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateOrUpdateRoomRequest eventRequest)
    {
        var result = await _roomMapper.UpdateAsync(id, eventRequest);
        return Ok(result);
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _roomMapper.DeleteAsync(id);
        return Ok(result); 
    }
    
    

}