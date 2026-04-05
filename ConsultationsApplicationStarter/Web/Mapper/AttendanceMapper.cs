using Domain.Dto;
using Domain.Models;
using Service.Interface;
using Web.Extensions;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class AttendanceMapper
{
    private readonly IAttendanceService _attendanceService;
    private readonly IFileUploadService _fileUploadService;

    public AttendanceMapper(IAttendanceService attendanceService, IFileUploadService fileUploadService)
    {
        _attendanceService = attendanceService;
        _fileUploadService = fileUploadService;
    }

    public async Task<AttendanceResponse> GetByIdNotNullAsync(Guid id)
    {
        var result = await _attendanceService.GetByIdNotNullAsync(id);
        return result.ToResponse();
    }

    public async Task<List<AttendanceResponse>> GetAllByConsultationIdAsync(Guid consultationId)
    {
        var result = await _attendanceService.GetByConsultationIdAsync(consultationId);
        return result.ToResponse();
    }

    public async Task<AttendanceResponse?> GetByIdAsync(Guid id)
    {
        var result = await _attendanceService.GetByIdAsync(id);
        return result?.ToResponse();
    }

    public async Task<List<AttendanceResponse>> GetAllAsync(string? dateAfter)
    {
        var result = await _attendanceService.GetAllAsync(dateAfter);
        return result.ToResponse();
    }

    public async Task<AttendanceResponse> RegisterAsync(AttendanceRequest request)
    {
        var dto = request.ToDto();
        var result = await _attendanceService.CreateAsync(dto);
        return result.ToResponse();
    }

    public async Task<AttendanceResponse> UpdateAsync(Guid id, AttendanceDto dto)
    {
        var result = await _attendanceService.UpdateAsync(id, dto);
        return result.ToResponse();
    }

    public async Task<AttendanceResponse> DeleteByIdAsync(Guid id)
    {
        var result = await _attendanceService.DeleteByIdAsync(id);
        return result.ToResponse();
    }

    public async Task<PaginatedResponse<AttendanceResponse>> GetPagedAsync(PaginatedRequest request)
    {
        var result = await _attendanceService.GetPagedAsync(request.PageNumber, request.PageSize);
        return result.ToPaginatedResponse();
    }

    public async Task<AttendanceResponse> MarkAsAbsentAsync(Guid id)
    {
        var result = await _attendanceService.MarkAsAbsentAsync(id);
        return result.ToResponse();
    }

    public async Task<AttendanceResponse> UploadReasonByIdInFileSystemAsync(Guid id, IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);

        var path = await _fileUploadService.UploadFileAsync(
            ms.ToArray(),
            file.FileName
        );
        
        var result = await _attendanceService.UpdateReasonPathByIdAsync(id, path);
        
        return result.ToResponse();
    }
}