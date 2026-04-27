using CoursesApplication.Domain.Dto;
using CoursesApplication.Domain.Models;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;

namespace CoursesApplication.Service.Implementation;

public class CourseService : ICourseService
{
    private readonly IRepository<Course> _repository;

    public CourseService(IRepository<Course> repository)
    {
        _repository = repository;
    }

    public async Task<List<Course>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync<Course>(x => x);
        return result.ToList();
    }

    public async Task<Course?> GetByIdAsync(Guid id)
    {
        return await _repository.Get<Course>(x => x, x => x.Id == id);
    }

    public async Task<Course> InsertAsync(CourseDto dto)
    {
        var course = new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            Ects = dto.Ects,
            Category = dto.Category,
            SemesterId = dto.SemesterId
        };
        return await _repository.InsertAsync(course);
    }

    public async Task<Course> UpdateAsync(Guid id, CourseDto dto)
    {
        var course = await _repository.Get<Course>(x => x, x => x.Id == id)
                     ?? throw new KeyNotFoundException($"Course {id} not found.");
        course.Title = dto.Title;
        course.Description = dto.Description;
        course.Ects = dto.Ects;
        course.Category = dto.Category;
        course.SemesterId = dto.SemesterId;
        return await _repository.UpdateAsync(course);
    }

    public async Task<Course> DeleteAsync(Guid id)
    {
        var course = await _repository.Get<Course>(x => x, x => x.Id == id)
                     ?? throw new KeyNotFoundException($"Course {id} not found.");
        return await _repository.DeleteAsync(course);
    }

    public async Task<PaginatedResult<Course>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync<Course>(x => x, pageNumber, pageSize);
    }
}
