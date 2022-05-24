using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.QuestionAggregate;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.Create;

public record CreateQuestionCommand(long ProductId, long CustomerId, string Description) : IBaseCommand;

public class CreateQuestionCommandHandler : IBaseCommandHandler<CreateQuestionCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public CreateQuestionCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<OperationResult> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = new Question(request.ProductId, request.CustomerId, request.Description);

        _questionRepository.Add(question);
        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(q => q.Description)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("متن سوال"));
    }
}