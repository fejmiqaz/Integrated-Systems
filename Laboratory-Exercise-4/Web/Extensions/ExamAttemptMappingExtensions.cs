using Domain.DTO;
using Domain.Models;
using Web.Response;

namespace Web.Extensions;

public static class ExamAttemptMappingExtensions
{
    public static ExamAttemptResponse ToResponse(this ExamAttempt e,List<AttemptQuestionDto> questions)
    {
        return new ExamAttemptResponse(
            e.Id,
            e.ExamId,
            e.StudentId,
            e.StartedAt,
            e.FinishedAt,
            questions
        );
    }
}