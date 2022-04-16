using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.UseCases.RemoveAnswer;

public record RemoveAnswerCommand(long QuestionId, long AnswerId) : IBaseCommand;

public class RemoveAnswerCommandHandler : IBaseCommandHandler<RemoveAnswerCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public RemoveAnswerCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<OperationResult> Handle(RemoveAnswerCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetAsTrackingAsync(request.QuestionId);

        if (question == null)
            return OperationResult.NotFound();

        question.RemoveAnswer(request.AnswerId);

        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}