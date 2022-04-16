using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.QuestionAggregate;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.UseCases.AddAnswer;

public record AddAnswerCommand(long QuestionId, string Description) : IBaseCommand;

public class AddAnswerCommandHandler : IBaseCommandHandler<AddAnswerCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public AddAnswerCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<OperationResult> Handle(AddAnswerCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetAsTrackingAsync(request.QuestionId);

        if (question == null)
            return OperationResult.NotFound();

        question.AddAnswer(new Answer(request.QuestionId, request.Description));

        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class AddAnswerCommandValidator : AbstractValidator<AddAnswerCommand>
{
    public AddAnswerCommandValidator()
    {
        RuleFor(a => a.Description)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("متن پاسخ"));
    }
}