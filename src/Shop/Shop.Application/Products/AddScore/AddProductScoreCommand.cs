using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.ProductAggregate.Repository;

namespace Shop.Application.Products.AddScore;

public record AddProductScoreCommand(long ProductId, int Score) : IBaseCommand;

public class AddProductScoreCommandHandler : IBaseCommandHandler<AddProductScoreCommand>
{
    private readonly IProductRepository _productRepository;

    public AddProductScoreCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(AddProductScoreCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsTrackingAsync(request.ProductId);

        if (product == null)
            return OperationResult.NotFound();

        product.AddScore(request.Score);

        await _productRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class AddScoreCommandValidator : AbstractValidator<AddProductScoreCommand>
{
    public AddScoreCommandValidator()
    {
        RuleFor(p => p.Score)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("امتیاز"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("امتیاز"))
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.FieldGreaterThanOrEqualTo("امتیاز", 0))
            .LessThanOrEqualTo(5).WithMessage(ValidationMessages.FieldLessThanOrEqualTo("امتیاز", 5));
    }
}