using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.QuestionAggregate;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.SetStatus;

public record SetQuestionStatusCommand(long QuestionId, string Status) : IBaseCommand;

public class SetQuestionStatusCommandHandler : IBaseCommandHandler<SetQuestionStatusCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public SetQuestionStatusCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<OperationResult> Handle(SetQuestionStatusCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetAsTrackingAsync(request.QuestionId);

        if (question == null)
            return OperationResult.NotFound();

        if (!Enum.TryParse(request.Status, out Question.QuestionStatus status)) 
            return OperationResult.Error(ValidationMessages.FieldInvalid("وضعیت سوال"));

        question.SetStatus(status);

        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}