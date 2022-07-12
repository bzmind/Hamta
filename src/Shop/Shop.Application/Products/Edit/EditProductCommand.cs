using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ProductAggregate;
using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.ProductAggregate.Services;

namespace Shop.Application.Products.Edit;

public record EditProductCommand(long ProductId, long CategoryId, string Name, string? EnglishName,
    string Slug, string Description, IFormFile? MainImage, List<IFormFile>? GalleryImages,
    List<SpecificationDto>? CustomSpecifications, Dictionary<string, string>? ExtraDescriptions) : IBaseCommand;

public class EditProductCommandHandler : IBaseCommandHandler<EditProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IFileService _fileService;

    public EditProductCommandHandler(IProductRepository productRepository,
        IProductDomainService productDomainService, IFileService fileService)
    {
        _productRepository = productRepository;
        _productDomainService = productDomainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsTrackingAsync(request.ProductId);

        if (product == null)
            return OperationResult.NotFound();

        product.Edit(request.CategoryId, request.Name, request.EnglishName, request.Slug, request.Description,
            _productDomainService);

        var oldImage = product.MainImage;

        if (request.MainImage != null)
        {
            _fileService.DeleteFile(Directories.ProductMainImages, oldImage.Name);

            var newMainImage =
                await _fileService.SaveFileAndGenerateName(request.MainImage, Directories.ProductMainImages);

            product.SetMainImage(newMainImage);
        }

        var oldGalleryImages = product.GalleryImages.Select(img => img.Name).ToList();

        if (request.GalleryImages != null)
        {
            _fileService.DeleteMultipleFiles(Directories.ProductGalleryImages, oldGalleryImages);

            var newGalleryImages =
                await _fileService.SaveMultipleFilesAndGenerateNames(request.GalleryImages,
                    Directories.ProductGalleryImages);

            product.SetGalleryImages(newGalleryImages);
        }

        if (request.CustomSpecifications != null)
        {
            var customSpecifications = new List<ProductSpecification>();

            request.CustomSpecifications.ToList().ForEach(specification =>
                customSpecifications.Add(new ProductSpecification(product.Id, specification.Title,
                    specification.Description, specification.IsImportantFeature)));

            product.SetCustomSpecifications(customSpecifications);
        }

        if (request.ExtraDescriptions != null)
        {
            var extraDescriptions = new List<ProductExtraDescription>();

            request.ExtraDescriptions.ToList().ForEach(extraDescription =>
            {
                extraDescriptions.Add(new ProductExtraDescription(product.Id, extraDescription.Key,
                    extraDescription.Value));
            });

            product.SetExtraDescriptions(extraDescriptions);
        }

        await _productRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("نام محصول"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام محصول"));

        RuleFor(p => p.Slug)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("اسلاگ"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("اسلاگ"));

        RuleFor(p => p.MainImage)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("عکس اصلی محصول"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس اصلی محصول"));

        RuleFor(p => p.GalleryImages)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("عکس های گالری"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس های گالری"));
    }
}