using Domain.DTO;

namespace Service.Interface;

public interface IQuestionApiClient
{
    Task<List<AttemptQuestionDto>> GetFiveFirstQuestionsWithAttemptAsync(Guid attemptId);
}