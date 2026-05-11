using Domain.Models;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class ExamAttemptService : IExamAttemptService
{
    private readonly IRepository<ExamAttempt> _repository;

    public ExamAttemptService(IRepository<ExamAttempt> repository)
    {
        _repository = repository;
    }

    public async Task<ExamAttempt> GetByIdNotNullAsync(Guid id)
    {
        var result = await _repository.Get(
            selector: x => x,
            predicate: x => x.Id == id);

        if (result == null)
        {
            throw new Exception();
        }

        return result;
    }
}