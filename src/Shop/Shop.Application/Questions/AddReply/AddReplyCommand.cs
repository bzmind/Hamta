using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.AddReply;

public record AddReplyCommand(long UserId, long QuestionId, string Description) : IBaseCommand;

public class AddReplyCommandHandler : IBaseCommandHandler<AddReplyCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public AddReplyCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<OperationResult> Handle(AddReplyCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetAsTrackingAsync(request.QuestionId);

        if (question == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("سوال"));

        question.AddReply(request.UserId, request.Description);

        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class AddReplyCommandValidator : AbstractValidator<AddReplyCommand>
{
    public AddReplyCommandValidator()
    {
        RuleFor(r => r.Description)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("توضیح"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("توضیح"));
    }
}