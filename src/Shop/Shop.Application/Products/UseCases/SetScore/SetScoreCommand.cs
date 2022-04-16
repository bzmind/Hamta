using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.ProductAggregate.Repository;

namespace Shop.Application.Products.UseCases.SetScore;

public record SetScoreCommand(long ProductId, int ScoreAmount) : IBaseCommand;

public class SetScoreCommandHandler : IBaseCommandHandler<SetScoreCommand>
{
    private readonly IProductRepository _productRepository;

    public SetScoreCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(SetScoreCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsTrackingAsync(request.ProductId);

        if (product == null)
            return OperationResult.NotFound();

        product.AddScore(request.ScoreAmount);

        await _productRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class SetScoreCommandValidator : AbstractValidator<SetScoreCommand>
{
    public SetScoreCommandValidator()
    {
        RuleFor(p => p.ScoreAmount)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("امتیاز"))
            .GreaterThan(0).WithMessage(ValidationMessages.FieldMinLength("امتیاز", 0))
            .LessThan(6).WithMessage(ValidationMessages.FieldMaxLength("امتیاز", 6));
    }
}