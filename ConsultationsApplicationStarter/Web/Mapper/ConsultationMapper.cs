using Domain.Dto;
using Domain.Models;
using Service.Interface;
using Web.Extensions;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class ConsultationMapper
{
    private readonly IConsultationService _consultationService;

    public ConsultationMapper(IConsultationService consultationService)
    {
        _consultationService = consultationService;
    }

    public async Task<ConsultationBasicResponse> GetByIdNotNullAsync(Guid id)
    {
        var result = await _consultationService.GetByIdNotNullAsync(id);
        return result.ToBasicResponse();
    }
    
    
    public async Task<ConsultationBasicResponse?> GetByIdAsync(Guid id)
    {
        var result = await _consultationService.GetByIdAsync(id);
        return result?.ToBasicResponse();
    }

    public async Task<List<ConsultationResponse>> GetAllAsync(string? roomName, DateOnly? date)
    {
        var result = await _consultationService.GetAllAsync(roomName, date);
        return result.ToResponse();
    }

    public async Task<ConsultationBasicResponse> InsertAsync(ConsultationRequest request)
    {
        var dto = request.ToDto();
        var result = await _consultationService.CreateAsync(dto.StartTime, dto.EndTime, dto.RoomId);
        return result.ToBasicResponse();
    }

    public async Task<ConsultationBasicResponse> UpdateAsync(Guid id, ConsultationRequest request)
    {
        var dto = request.ToDto();
        var result = await _consultationService.UpdateAsync(id, dto.StartTime, dto.EndTime, dto.RoomId);
        return result.ToBasicResponse();
    }

    public async Task<ConsultationBasicResponse> DeleteAsync(Guid id)
    {
        var result = await _consultationService.DeleteByIdAsync(id);
        var response = result.ToBasicResponse();
        return response;
    }

    public async Task<PaginatedResponse<ConsultationResponse>> GetPagedAsync(PaginatedRequest request)
    {
        var result = await _consultationService.GetPagedAsync(request.PageNumber, request.PageSize);
        return result.ToPaginatedResponse();
    }
}