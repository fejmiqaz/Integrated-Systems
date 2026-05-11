using System.Net.Http.Json;
using Domain.Configuration;
using Domain.DTO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Interface;
using Web.Response;

namespace Service.Implementation;

public class QuestionApiClient : IQuestionApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _settings;
    private readonly ILogger<QuestionApiClient> _logger;
    private readonly IMemoryCache _memoryCache;

    public QuestionApiClient(HttpClient httpClient, IOptions<ApiSettings> settings, ILogger<QuestionApiClient> logger, IMemoryCache memoryCache)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public async Task<List<AttemptQuestionDto>> GetFiveFirstQuestionsWithAttemptAsync(Guid attemptId)
    {
        var cacheKey = $"attempt-questions:{attemptId}";

        if (_memoryCache.TryGetValue(cacheKey, out List<AttemptQuestionDto>? cached))
        {
            _logger.LogInformation($"Cache hit for attempt: {attemptId}");
            return cached!;
        }

        if (cached != null)
        {
            return cached;
        }
        
        var url = $"api/attemptquestions/byattempt/{attemptId}/paged?page=1&pageSize=5";
        _logger.LogInformation("Calling external API: {Url}", url);
        
        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning(
                "External API returned status code {StatusCode} for attempt {AttemptId}",
                response.StatusCode,
                attemptId);

            return new List<AttemptQuestionDto>();
        }
        

        var pagedResponse = await response.Content.ReadFromJsonAsync<PaginatedResult<AttemptQuestionDto>>();

        var questions = pagedResponse?.Items ?? new List<AttemptQuestionDto>();

        _memoryCache.Set(
            cacheKey,
            questions,
            TimeSpan.FromMinutes(_settings.CacheExpirationMinutes));
        return questions;

    }
}