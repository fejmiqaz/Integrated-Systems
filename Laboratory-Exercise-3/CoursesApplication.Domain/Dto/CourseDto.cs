using CoursesApplication.Domain.Enums;

namespace CoursesApplication.Domain.Dto;

public class CourseDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Ects { get; set; }
    public Category Category { get; set; }
    public Guid SemesterId { get; set; }
}