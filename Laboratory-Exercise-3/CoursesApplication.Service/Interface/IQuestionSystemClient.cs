using CoursesApplication.Domain.Dto;

namespace CoursesApplication.Service.Interface;

public interface IQuestionSystemClient
{
    Task<List<AttemptQuestionDto>> GetFirstFiveQuestionsForAttemptAsync(Guid attemptId);
}