using System.Net.Http.Json;
using CoursesApplication.Domain.Configuration;
using CoursesApplication.Domain.Dto;
using CoursesApplication.Service.Interface;

namespace CoursesApplication.Service.Implementation;

public class QuestionSystemClient : IQuestionSystemClient
{
    private readonly HttpClient _httpClient;
    private readonly QuestionSystemSettings _settings;

    public QuestionSystemClient(HttpClient httpClient, QuestionSystemSettings settings)
    {
        _httpClient = httpClient;
        _settings = settings;
    }

    public async Task<List<AttemptQuestionDto>> GetFirstFiveQuestionsForAttemptAsync(Guid attemptId)
    {
        var url = $"api/attemptquestions/byattempt/{attemptId}/paged?page=1&pageSize=5";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return new List<AttemptQuestionDto>();
        }

        var pagedResponse = await response.Content.ReadFromJsonAsync<PaginatedResult<AttemptQuestionDto>>();

        return pagedResponse?.Items ?? new List<AttemptQuestionDto>();
    }
}