namespace CoursesApplication.Domain.Dto;

public class AttemptQuestionDto
{
    public Guid Id { get; set; }
    public Guid AttemptId { get; set; }
    public string? QuestionText { get; set; }
    public string? SelectedAnswer { get; set; }
    public bool? IsCorrect { get; set; }
    public int? Points { get; set; }
}