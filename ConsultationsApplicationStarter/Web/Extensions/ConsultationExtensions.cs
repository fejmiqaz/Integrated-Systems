using Domain.Dto;
using Domain.Models;
using Web.Request;
using Web.Response;

namespace Web.Extensions;

public static class ConsultationExtensions
{
    public static ConsultationBasicResponse ToBasicResponse(this Consultation consultation)
    {
        return new ConsultationBasicResponse(
            Id: consultation.Id,
            RoomId: consultation.RoomId,
            Start: consultation.StartTime,
            End: consultation.EndTime
        );
    }

    public static List<ConsultationBasicResponse> ToBasicResponse(this List<Consultation> consultations)
    {
        return consultations.Select(x => x.ToBasicResponse()).ToList();
    }

    public static ConsultationResponse ToResponse(
        this Consultation consultation)
    {
        return new ConsultationResponse(
            Id: consultation.Id,
            Date: DateOnly.FromDateTime(consultation.StartTime),
            RoomId: consultation.RoomId,
            RoomName: consultation.Room?.Name ?? string.Empty,
            RegisteredStudents: consultation.RegisteredStudents,
            Attendances: consultation.Attendances?.ToList().ToBasicResponse() ?? new List<AttendanceBasicResponse>()
        );
    }

    public static List<ConsultationResponse> ToResponse(
        this List<Consultation> consultation)
    {
        return consultation.Select(x => x.ToResponse()).ToList();
    }

    public static AttendanceDto ToDto(this Attendance attendance)
    {
        return new AttendanceDto()
        {
            UserId = attendance.UserId,
            Comment = attendance.Comment,
            ConsultationId = attendance.ConsultationId,
            RoomId = attendance.RoomId
        };
    }

    public static ConsultationDto ToDto(this ConsultationRequest request)
    {
        return new ConsultationDto()
        {
            EndTime = request.EndTime,
            StartTime = request.StartTime,
            RoomId = request.RoomId
        };
    }

    public static PaginatedResponse<ConsultationResponse> ToPaginatedResponse(
        this PaginatedResult<Consultation> result)
    {
        return new PaginatedResponse<ConsultationResponse>()
        {
            Items = result.Items.Select(x => x.ToResponse()).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages
        };
    }
    
}