using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ProductAggregate.Repository;

namespace Shop.Application.Products.ReplaceMainImage;

public record ReplaceProductMainImageCommand(long ProductId, IFormFile MainImage) : IBaseCommand;

public class ReplaceProductMainImageCommandHandler : IBaseCommandHandler<ReplaceProductMainImageCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;

    public ReplaceProductMainImageCommandHandler(IProductRepository productRepository, IFileService fileService)
    {
        _productRepository = productRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(ReplaceProductMainImageCommand request, CancellationToken cancellationToken)
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

public class ReplaceMainImageCommandValidator : AbstractValidator<ReplaceProductMainImageCommand>
{
    public ReplaceMainImageCommandValidator()
    {
        RuleFor(p => p.MainImage)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("عکس اصلی محصول"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس اصلی محصول"));
    }
}