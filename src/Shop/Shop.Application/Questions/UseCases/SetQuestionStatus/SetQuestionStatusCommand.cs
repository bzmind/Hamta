using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.QuestionAggregate;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.UseCases.SetQuestionStatus;

public record SetQuestionStatusCommand(long QuestionId, int QuestionStatusId) : IBaseCommand;

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

        var status = (Question.QuestionStatus) request.QuestionStatusId;
        question.SetQuestionStatus(status);

        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}