using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class CourseService : ICourseService
{
    public IRepository<Course> _repository;

    public CourseService(IRepository<Course> repository)
    {
        _repository = repository;
    }

    public async Task<Course> GetByIdNotNullAsync(Guid id)
    {
        var result = await _repository.Get(
            selector: x => x,
            predicate: x => x.Id == id);
        if (result == null)
        {
            throw new InvalidOperationException($"Course with id {id} was not found");
        }

        return result;
    }

    public async Task<List<Course>> GetAllAsync(string? category) // if category is not null apply filter by category.
    {
        if (!string.IsNullOrWhiteSpace(category))
        {
            return (await _repository.GetAllAsync(
                x => x,
                c => c.Category.ToString().ToLower() == category.ToLower(),
                include: q => q.Include(c => c.Semester)
            )).ToList();
        }

        return (await _repository.GetAllAsync(
            x => x,
            include: q => q.Include(c => c.Semester)
        )).ToList();
    }

    public async Task<Course> CreateAsync(CourseDto dto)
    {
        var courseToAdd = new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            Ects = dto.Ects,
            SemesterId = dto.SemesterId
        };

        return await _repository.InsertAsync(courseToAdd);
    }

    public async Task<Course> UpdateAsync(Guid id, CourseDto dto)
    {
        var courseToUpdate = await GetByIdNotNullAsync(id);

        courseToUpdate.Title = dto.Title;
        courseToUpdate.Description = dto.Description;
        courseToUpdate.Category = dto.Category;
        courseToUpdate.Ects = dto.Ects;
        courseToUpdate.SemesterId = dto.SemesterId;

        return await _repository.UpdateAsync(courseToUpdate);
    }

    public async Task<Course> DeleteAsync(Guid id)
    {
        var courseToDelete = await GetByIdNotNullAsync(id);
        return await _repository.DeleteAsync(courseToDelete);
    }

    public async Task<PaginatedResult<Course>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetPagedAsync(pageNumber, pageSize,
        include: q => q.
                Include(c => c.Semester)
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.User));
    }
}