using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.Remove;

public record RemoveQuestionCommand(long QuestionId) : IBaseCommand;

public class RemoveQuestionCommandHandler : IBaseCommandHandler<RemoveQuestionCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public RemoveQuestionCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<OperationResult> Handle(RemoveQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetAsync(request.QuestionId);

        if (question == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("سوال"));

        _questionRepository.Delete(question);

        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}