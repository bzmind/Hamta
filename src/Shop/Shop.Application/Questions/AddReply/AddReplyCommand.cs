using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.AddReply;

public class AddReplyCommand : IBaseCommand
{
    public long QuestionId { get; set; }
    public long UserId { get; set; }
    public string Description { get; set; }

    public AddReplyCommand(long questionId, long userId, string description)
    {
        QuestionId = questionId;
        UserId = userId;
        Description = description;
    }
}

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
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("توضیح"));
    }
}