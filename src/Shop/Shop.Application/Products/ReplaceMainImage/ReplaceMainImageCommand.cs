using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.FileUtility;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ProductAggregate.Repository;

namespace Shop.Application.Products.ReplaceMainImage;

public record ReplaceMainImageCommand(long ProductId, IFormFile MainImage) : IBaseCommand;

public class ReplaceMainImageCommandHandler : IBaseCommandHandler<ReplaceMainImageCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;

    public ReplaceMainImageCommandHandler(IProductRepository productRepository, IFileService fileService)
    {
        _productRepository = productRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(ReplaceMainImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsTrackingAsync(request.ProductId);

        if (product == null)
            return OperationResult.NotFound();
        
        _fileService.DeleteFile(Directories.ProductMainImages, product.MainImage.Name);

        var newImage = await _fileService
            .SaveFileAndGenerateName(request.MainImage, Directories.ProductMainImages);
        product.SetMainImage(newImage);

        await _productRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class ReplaceMainImageCommandValidator : AbstractValidator<ReplaceMainImageCommand>
{
    public ReplaceMainImageCommandValidator()
    {
        RuleFor(p => p.MainImage)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس اصلی محصول"));
    }
}