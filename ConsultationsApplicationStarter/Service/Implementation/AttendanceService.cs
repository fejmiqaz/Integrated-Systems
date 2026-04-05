using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class AttendanceService : IAttendanceService
{
    private readonly IRepository<Attendance> _repository;
    private readonly IRepository<Consultation> _consultationRepo;

    public AttendanceService(IRepository<Attendance> repository, IRepository<Consultation> consultationRepo)
    {
        _repository = repository;
        _consultationRepo = consultationRepo;
    }

    public async Task<List<Attendance>> GetByConsultationIdAsync(Guid consultationId)
    {
        var result = await _repository.GetAllAsync(
            selector: x => x,
            predicate: x => x.ConsultationId == consultationId
        );
        return result.ToList();
    }

    public async Task<Attendance> MarkAsAbsentAsync(Guid id)
    {
        var attendanceToMarkAsAbsent = await GetByIdNotNullAsync(id);
        attendanceToMarkAsAbsent.Status = Status.Absent;
        return await _repository.UpdateAsync(attendanceToMarkAsAbsent);
    }

    public async Task<Attendance> GetByIdNotNullAsync(Guid id)
    {
        var result = await GetByIdAsync(id);
        if (result == null)
        {
            throw new InvalidOperationException("Attendance not found");
        }

        return result;
    }

    public async Task<Attendance?> GetByIdAsync(Guid id)
    {
        return await _repository.GetAsync(
            selector: x => x,
            predicate: x => x.Id == id
        );
    }

    public async Task<List<Attendance>> GetAllAsync(string? dateAfter)
    {
        var result = await _repository.GetAllAsync(
            selector: x => x);
        return result.ToList();
    }

    public async Task<Attendance> CreateAsync(AttendanceDto dto)
    {
        var consultation = await _consultationRepo.GetAsync(
            selector: x => x,
            predicate: x => x.Id == dto.ConsultationId
        );

        if (consultation == null)
        {
            throw new InvalidOperationException("Consultation not found");
        }

        consultation.RegisteredStudents += 1;
        await _consultationRepo.UpdateAsync(consultation);

        var attendanceToAdd = new Attendance()
        {
            UserId = dto.UserId,
            RoomId = dto.RoomId,
            ConsultationId = dto.ConsultationId,
            Comment = dto.Comment,
            Status = Status.Registered,
            Consultation = consultation
        };

        return await _repository.InsertAsync(attendanceToAdd);
    }

    public async Task<Attendance> UpdateAsync(Guid id, AttendanceDto dto)
    {
        var attendanceToUpdate = await GetByIdNotNullAsync(id);
        attendanceToUpdate.Comment = dto.Comment;
        attendanceToUpdate.UserId = dto.UserId;
        attendanceToUpdate.RoomId = dto.RoomId;
        attendanceToUpdate.ConsultationId = dto.ConsultationId;

        return await _repository.UpdateAsync(attendanceToUpdate);
    }

    public async Task<Attendance> DeleteByIdAsync(Guid id)
    {
        var attendanceToDelete = await GetByIdNotNullAsync(id);

        if (attendanceToDelete.Consultation.StartTime < DateTime.Now.AddHours(1))
        {
            throw new InvalidOperationException("Cannot delete attendance because Start Time Hour is less than 1 hour");
        }

        if (attendanceToDelete.Consultation.RegisteredStudents > 0)
        {
            attendanceToDelete.Consultation.RegisteredStudents -= 1;
        }
        
        return await _repository.DeleteAsync(attendanceToDelete);
    }

    public async Task<PaginatedResult<Attendance>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync(
            selector: x => x,
            pageNumber: pageNumber,
            pageSize: pageSize,
            include: q => q.Include(a => a.Room),
            orderBy: q => q.OrderBy(a => a.RoomId)
        );
    }

    public async Task<Attendance> UpdateReasonPathByIdAsync(Guid id, string path)
    {
        var result = await GetByIdNotNullAsync(id);
        result.CancellationReasonDocumentPath = path;
        return result;
    }
}