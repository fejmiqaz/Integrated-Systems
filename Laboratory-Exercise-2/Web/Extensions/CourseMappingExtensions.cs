using Domain.DTO;
using Domain.Models;
using Web.Request;
using Web.Response;

namespace Web.Extensions;

public static class CourseMappingExtensions
{
    public static CourseResponse ToResponse(this Course c)
    {
        return new CourseResponse(
            c.Id,
            c.Title,
            c.Description,
            c.Category,
            c.Ects,
            c.SemesterId,
            c.Semester.Name
        );
    }
    
    public static CourseWithEnrollmentsResponse ToCourseWithEnrollmentsResponse(this Course c)
    {
        return new CourseWithEnrollmentsResponse(
            c.Id,
            c.Title,
            c.Description,
            c.Category,
            c.Ects,
            c.SemesterId,
            c.Semester?.Name,
            c.Enrollments.ToResponse()
        );
    }
    
    public static List<CourseResponse> ToResponse(this List<Course> courses)
    {
        // return courses.Select(ToResponse).ToList();
        return courses.Select(x => x.ToResponse()).ToList();
    }
    
    public static List<CourseWithEnrollmentsResponse> ToCourseWithEnrollmentsResponse(this IEnumerable<Course> courses)
    {
        return courses.Select(x => x.ToCourseWithEnrollmentsResponse()).ToList();
    }

    public static PaginatedResponse<CourseWithEnrollmentsResponse> ToPaginatedResponse(this PaginatedResult<Course> result)
    {
        return result.ToPaginatedResponse(course => course.ToCourseWithEnrollmentsResponse());
    }
    
    public static CourseDto ToDto(this CreateOrUpdateCourseRequest request)
    {
        return new CourseDto
        {
            Title = request.Title,
            Description = request.Description,
            Ects = request.Ects,
            Category = request.Category,
            SemesterId = request.SemesterId
        };
    }
    
    public static CourseDto ToDto(this CourseRequest request)
    {
        return new CourseDto
        {
            Title = request.Title,
            Description = request.Description,
            Ects = request.Ects,
            Category = request.Category,
            SemesterId = request.SemesterId
        };
    }



}