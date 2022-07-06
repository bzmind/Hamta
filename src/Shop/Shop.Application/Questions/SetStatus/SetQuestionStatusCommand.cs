using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.QuestionAggregate;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.SetStatus;

public record SetQuestionStatusCommand(long QuestionId, Question.QuestionStatus Status) : IBaseCommand;

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

        question.SetStatus(request.Status);

        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class SetQuestionStatusCommandValidator : AbstractValidator<SetQuestionStatusCommand>
{
    public SetQuestionStatusCommandValidator()
    {
        RuleFor(r => r.Status)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("وضعیت سوال"))
            .IsInEnum().WithMessage(ValidationMessages.FieldInvalid("وضعیت سوال"));
    }
}