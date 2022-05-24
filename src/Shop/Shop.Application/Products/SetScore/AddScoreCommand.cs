using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.ProductAggregate.Repository;

namespace Shop.Application.Products.SetScore;

public record AddScoreCommand(long ProductId, float ScoreAmount) : IBaseCommand;

public class AddScoreCommandHandler : IBaseCommandHandler<AddScoreCommand>
{
    private readonly IProductRepository _productRepository;

    public AddScoreCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(AddScoreCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsTrackingAsync(request.ProductId);

        if (product == null)
            return OperationResult.NotFound();

        product.AddScore(request.ScoreAmount);

        await _productRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class AddScoreCommandValidator : AbstractValidator<AddScoreCommand>
{
    public AddScoreCommandValidator()
    {
        RuleFor(p => p.ScoreAmount)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("امتیاز"))
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.FieldGreaterThanOrEqualTo("امتیاز", 0))
            .LessThanOrEqualTo(5).WithMessage(ValidationMessages.FieldLessThanOrEqualTo("امتیاز", 5));
    }
}