using Service.Interface;
using Web.Extensions;
using Web.Response;

namespace Web.Mapper;

public class ExamAttemptMapper
{
    private readonly IExamAttemptService _examAttemptService;
    private readonly IQuestionApiClient _questionApiClient;


    public ExamAttemptMapper(IExamAttemptService examAttemptService, IQuestionApiClient questionApiClient)
    {
        _examAttemptService = examAttemptService;
        _questionApiClient = questionApiClient;
    }

    public async Task<ExamAttemptResponse> GetByIdAsync(Guid id)
    {
        var attempt = await _examAttemptService.GetByIdNotNullAsync(id);

        var questions = await _questionApiClient.GetFiveFirstQuestionsWithAttemptAsync(id);

        return attempt.ToResponse(questions);
    }
    
}