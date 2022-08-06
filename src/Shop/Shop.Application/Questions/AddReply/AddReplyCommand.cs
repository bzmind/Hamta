using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.AddReply;

public class AddReplyCommand : IBaseCommand
{
    public long UserId { get; set; }
    public long QuestionId { get; set; }
    public string Description { get; set; }

    private AddReplyCommand()
    {

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
            .NotNull().WithMessage(ValidationMessages.DescriptionRequired)
            .NotEmpty().WithMessage(ValidationMessages.DescriptionRequired)
            .MaximumLength(300).WithMessage(ValidationMessages.FieldCharactersMaxLength("توضیحات", 300));
    }
}