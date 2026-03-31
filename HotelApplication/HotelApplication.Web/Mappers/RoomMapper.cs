using HotelApplication.Service.Interface;
using HotelApplication.Web.Extensions;
using HotelApplication.Web.Request;
using HotelApplication.Web.Response;

namespace HotelApplication.Web.Mappers;

public class RoomMapper
{
    private readonly IRoomService _roomService;

    public RoomMapper(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    public async Task<RoomResponse?> GetById(Guid id)
    {
        var result = await _roomService.GetByIdNotNullAsync(id);
        return result.ToResponse();
    }

    public async Task<List<RoomResponse>> GetAll(int? status)
    {
        var result = await _roomService.GetAllAsync(status);
        return result.ToResponse();
    }

    public async Task<PaginatedResponse<RoomResponse>> PaginatedGetAllAsync(PaginatedRequest request)
    {
        var result = await _roomService.GetAllPagedAsync(request.PageNumber, request.PageSize);
        return result.ToPaginatedResponse(e => e.ToResponse());
    }

    public async Task<RoomResponse> InsertAsync(CreateOrUpdateRoomRequest request)
    {
        var dto = request.ToDto();
        
        var result = await _roomService.InsertAsync(dto);
        
        return result.ToResponse();
    }

    public async Task<RoomResponse> UpdateAsync(Guid id, CreateOrUpdateRoomRequest request)
    {
        var dto = request.ToDto();
        var result = await _roomService.UpdateAsync(id, dto);
        return result.ToResponse();
    }

    public async Task<RoomResponse> DeleteAsync(Guid id)
    {
        var resut = await _roomService.DeleteAsync(id);
        return resut?.ToResponse();
    }

}