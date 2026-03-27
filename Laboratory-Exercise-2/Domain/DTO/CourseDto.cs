using Domain.Enums;
using Domain.Models;

namespace Domain.DTO;

public class CourseDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int Ects { get; set; }
    public Category Category { get; set; }
    public Guid SemesterId { get; set; }
}