using CoursesApplication.Domain.Dto;

namespace CoursesApplication.Web.Response;

public class ExamAttemptResponse
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    
}