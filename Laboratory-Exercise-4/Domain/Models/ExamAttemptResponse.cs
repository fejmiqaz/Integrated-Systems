using Domain.DTO;

namespace Web.Response;

public record ExamAttemptResponse(
    Guid Id,
    Guid ExamId,
    Guid StudentId,
    DateTime StartedAt,
    DateTime? FinishedAt,

    List<AttemptQuestionDto> Questions
);

    // public record EnrollmentResponse(
    //     Guid Id,
    //     string UserId,
    //     string UserName,
    //     DateOnly EnrolledAt
    // );