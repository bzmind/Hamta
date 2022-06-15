using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.RemoveReply;

public record RemoveReplyCommand(long QuestionId, long ReplyId) : IBaseCommand;

public class RemoveReplyCommandHandler : IBaseCommandHandler<RemoveReplyCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public RemoveReplyCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<OperationResult> Handle(RemoveReplyCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetAsTrackingAsync(request.QuestionId);

        if (question == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("سوال"));

        question.RemoveReply(request.ReplyId);

        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}