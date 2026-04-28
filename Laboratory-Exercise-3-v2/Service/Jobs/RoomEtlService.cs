using Domain.Models;
using Repository.Interface;
using Quartz;

namespace Service.Jobs;

public class RoomEtlService : IJob
{
    private readonly ILegacyRoomRepository _legacyRoomRepository;
    private readonly IRoomDirectoryRepository _roomDirectoryRepository;
    private readonly IConsultationSlotRepository _consultationSlotRepository;
    private readonly IRepository<EtlSyncLog> _etlSyncLogRepository;

    public RoomEtlService(ILegacyRoomRepository legacyRoomRepository, IRoomDirectoryRepository roomDirectoryRepository, IConsultationSlotRepository consultationSlotRepository, IRepository<EtlSyncLog> etlSyncLogRepository)
    {
        _legacyRoomRepository = legacyRoomRepository;
        _roomDirectoryRepository = roomDirectoryRepository;
        _consultationSlotRepository = consultationSlotRepository;
        _etlSyncLogRepository = etlSyncLogRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var syncLog = new EtlSyncLog()
        {
            JobName = "RoomDirectorySync",
            StartedAt = DateTime.UtcNow
        };

        try
        {
            var lastRun = await _etlSyncLogRepository.GetAllAsync(
                selector: x => x,
                predicate: x => x.JobName == "RoomDirectorySync" && x.Success,
                orderBy: x => x.OrderByDescending(v => v.StartedAt));

            var date = lastRun.FirstOrDefault()?.StartedAt ?? DateTime.MinValue;

            var rooms = await _legacyRoomRepository.GetRoomsModifiedSinceAsync(date);
            var consultations = await _legacyRoomRepository.GetConsultationsModifiedSinceAsync(date);

            await _roomDirectoryRepository.BulkInsertOrUpdateRoomDirectories(rooms);
            await _consultationSlotRepository.BulkInsertOrUpdateConsultationSlots(consultations);

            syncLog.Success = true;
            syncLog.CompletedAt = DateTime.UtcNow;

        }
        catch (Exception ex)
        {
            syncLog.Success = false;
            syncLog.ErrorMessage = ex.Message;
            syncLog.CompletedAt = DateTime.UtcNow;
        }
        finally
        {
            await _etlSyncLogRepository.InsertAsync(syncLog);
        }
    }
}