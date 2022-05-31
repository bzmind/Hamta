using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.QuestionAggregate;
using Shop.Domain.QuestionAggregate.Repository;

namespace Shop.Application.Questions.Create;

public record CreateQuestionCommand(long ProductId, long UserId, string Description) : IBaseCommand<long>;

public class CreateQuestionCommandHandler : IBaseCommandHandler<CreateQuestionCommand, long>
{
    private readonly IQuestionRepository _questionRepository;

    public CreateQuestionCommandHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<OperationResult<long>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = new Question(request.ProductId, request.UserId, request.Description);

        _questionRepository.Add(question);

        await _questionRepository.SaveAsync();
        return OperationResult<long>.Success(question.Id);
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