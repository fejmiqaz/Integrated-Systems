using HotelApplication.Domain.Dto;
using HotelApplication.Domain.Models;
using HotelApplication.Repository.Interface;
using HotelApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace HotelApplication.Service.Implementation;

public class RoomService : IRoomService
{
    private readonly IRepository<Room> _repository;

    public RoomService(IRepository<Room> repository)
    {
        _repository = repository;
    }

    public async Task<List<Room>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync(x => x);
        return result.ToList();
    }
    
    public async Task<List<Room>> GetAllAsync(int? status)
    {
        if (status != null)
        {
            var result = await _repository.GetAllAsync(
                selector: x => x,
                predicate: x => x.Status.CompareTo(status) == status
            );
            return result.ToList();
        }
        else
        {
            var result = await _repository.GetAllAsync(x => x);
            return result.ToList();
        }
    }
    
    public async Task<Room?> GetByIdAsync(Guid id)
    {
        return await _repository.Get(
            selector: x=> x,
            predicate: x => x.Id == id
        );
    }
    
    public async Task<Room> GetByIdNotNullAsync(Guid id)
    {
        var result = await GetByIdAsync(id);
    
        if (result == null)
        {
            throw new InvalidOperationException($"Room with id {id} not found");
        }
    
        return result;
    }
    
    public async Task<Room> InsertAsync(RoomDto dto)
    {
        var roomToInsert = new Room()
        {
            Capacity = dto.Capacity,
            HotelId = dto.HotelId,
            PricePerNight = dto.PricePerNight,
            RoomNumber = dto.RoomNumber,
            Status = dto.Status
        };
        return await _repository.InsertAsync(roomToInsert);
    }
    
    public async Task<Room> UpdateAsync(Guid id, RoomDto dto)
    {
        var roomToUpdate = await GetByIdNotNullAsync(id);
        roomToUpdate.Capacity = dto.Capacity;
        roomToUpdate.HotelId = dto.HotelId;
        roomToUpdate.PricePerNight = dto.PricePerNight;
        roomToUpdate.RoomNumber = dto.RoomNumber;
        roomToUpdate.Status = dto.Status;
    
        return await _repository.UpdateAsync(roomToUpdate);
    }
    
    public async Task<Room> DeleteAsync(Guid id)
    {
        var roomToDelete = await GetByIdNotNullAsync(id);
        return await _repository.DeleteAsync(roomToDelete);
    }
    
    public async Task<PaginatedResult<Room>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync(
            selector: x=> x,
            pageNumber: pageNumber,
            pageSize: pageSize,
            include: x=> x.Include(y => y.Reservations),
            orderBy: x=> x.OrderBy(e => e.RoomNumber)
        );
    }
}