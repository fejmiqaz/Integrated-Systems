using Domain.Models;

namespace Service.Interface;

public interface IExamAttemptService
{
    Task<ExamAttempt> GetByIdNotNullAsync(Guid id);
}