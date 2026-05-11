using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Web.Request;

public record CreateOrUpdateCourseRequest(
    [Required] string Title,
    [Required] string Description,
    [Required] int Ects,
    [Required] Category Category,
    [Required] Guid SemesterId
);