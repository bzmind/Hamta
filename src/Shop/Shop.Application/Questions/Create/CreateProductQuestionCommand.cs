using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.QuestionAggregate;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.Create;

public record CreateProductQuestionCommand(long ProductId, long CustomerId, string Description) : IBaseCommand;

public class CreateProductQuestionCommandHandler : IBaseCommandHandler<CreateProductQuestionCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public CreateProductQuestionCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<OperationResult> Handle(CreateProductQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = new Question(request.ProductId, request.CustomerId, request.Description);

        _questionRepository.Add(question);
        await _questionRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class CreateProductQuestionCommandValidator : AbstractValidator<CreateProductQuestionCommand>
{
    public CreateProductQuestionCommandValidator()
    {
        RuleFor(q => q.Description)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("متن سوال"));
    }
}