using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.ProductAggregate.Repository;

namespace Shop.Application.Products.Remove;

public record RemoveProductCommand(long ProductId) : IBaseCommand;

public class RemoveProductCommandHandler : IBaseCommandHandler<RemoveProductCommand>
{
    private readonly IProductRepository _productRepository;

    public RemoveProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<OperationResult> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.RemoveProduct(request.ProductId);

        if (!product)
            return OperationResult.Error("امکان حذف این محصول وجود ندارد");

        await _productRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
{
    public RemoveProductCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotEmpty().WithMessage(ValidationMessages.ProductIdRequired);
    }
}