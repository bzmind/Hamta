using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.QuestionAggregate;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.SetReplyStatus;

public record SetQuestionReplyStatusCommand(long QuestionId, long ReplyId, int ReplyStatusId) : IBaseCommand;

public class SetReplyStatusCommandHandler : IBaseCommandHandler<SetQuestionReplyStatusCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public SetReplyStatusCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<OperationResult> Handle(SetQuestionReplyStatusCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetAsTrackingAsync(request.QuestionId);

        if (question == null)
            return OperationResult.NotFound();

        var status = (Reply.ReplyStatus) request.ReplyStatusId;
        question.SetReplyStatus(request.ReplyId, status);

        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}