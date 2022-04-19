using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.QuestionAggregate;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.SetAnswerStatus;

public record SetAnswerStatusCommand(long QuestionId, long AnswerId, int AnswerStatusId) : IBaseCommand;

public class SetAnswerStatusCommandHandler : IBaseCommandHandler<SetAnswerStatusCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public SetAnswerStatusCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<OperationResult> Handle(SetAnswerStatusCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetAsTrackingAsync(request.QuestionId);

        if (question == null)
            return OperationResult.NotFound();

        var status = (Answer.AnswerStatus) request.AnswerStatusId;
        question.SetAnswerStatus(request.AnswerId, status);

        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}