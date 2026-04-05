using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Web.Request;
using Web.Response;

namespace Web.Extensions;

public static class AttendanceExtensions
{
    public static AttendanceResponse ToResponse(this Attendance attendance)
    {
        return new AttendanceResponse(
            Id: attendance.Id,
            UserId: attendance.UserId,
            ConsultationId: attendance.ConsultationId,
            RoomId: attendance.RoomId,
            FirstName: attendance.User?.FirstName ?? string.Empty,
            LastName: attendance.User?.LastName ?? string.Empty,
            Status: attendance.Status.ToString(),
            Comment: attendance.Comment
        );
    }

    public static List<AttendanceResponse> ToResponse(this List<Attendance> attendances)
    {
        return attendances.Select(x => x.ToResponse()).ToList();
    }

    public static AttendanceDto ToDto(this AttendanceRequest request)
    {
        return new AttendanceDto()
        {
            Comment = request.Comment,
            ConsultationId = request.ConsultationId,
            RoomId = request.RoomId,
            UserId = request.UserId
        };
    }

    public static AttendanceBasicResponse ToBasicResponse(this Attendance attendance)
    {
        return new AttendanceBasicResponse(
            Id: attendance.Id,
            FirstName: attendance.User.FirstName,
            LastName: attendance.User.LastName
        );
    }

    public static List<AttendanceBasicResponse> ToBasicResponse(this List<Attendance> attendances)
    {
        return attendances.Select(x => x.ToBasicResponse()).ToList();
    }

    public static PaginatedResponse<AttendanceResponse> ToPaginatedResponse(this PaginatedResult<Attendance> result)
    {
        return new PaginatedResponse<AttendanceResponse>()
        {
            Items = result.Items.ToList().ToResponse(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount,
            TotalPages = result.TotalPages
        };
    }
}