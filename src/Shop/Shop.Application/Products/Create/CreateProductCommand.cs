using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ProductAggregate;
using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.ProductAggregate.Services;

namespace Shop.Application.Products.Create;

public record CreateProductCommand(long CategoryId, string Name, string? EnglishName, string Slug,
    string? Description, IFormFile MainImage, List<IFormFile> GalleryImages,
    List<SpecificationDto>? CustomSpecifications, Dictionary<string, string>? ExtraDescriptions) : IBaseCommand<long>;

public class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand, long>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IFileService _fileService;

    public CreateProductCommandHandler(IProductRepository productRepository,
        IProductDomainService productDomainService, IFileService fileService)
    {
        _productRepository = productRepository;
        _productDomainService = productDomainService;
        _fileService = fileService;
    }

    public async Task<OperationResult<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.CategoryId, request.Name, request.EnglishName, request.Slug,
            request.Description, _productDomainService);

        await _productRepository.AddAsync(product);

        var mainImage = await _fileService.SaveFileAndGenerateName(request.MainImage,
            Directories.ProductMainImages);
        product.SetMainImage(mainImage);

        var galleryImages =
            await _fileService.SaveMultipleFilesAndGenerateNames(request.GalleryImages,
                Directories.ProductGalleryImages);
        product.SetGalleryImages(galleryImages);

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
        return OperationResult<long>.Success(product.Id);
    }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("نام محصول"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام محصول"));

        RuleFor(p => p.Slug)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("اسلاگ"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("اسلاگ"));

        RuleFor(p => p.MainImage)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("عکس اصلی محصول"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس اصلی محصول"))
            .JustImageFile();

        RuleFor(p => p.GalleryImages)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("عکس های گالری"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس های گالری"));

        RuleForEach(p => p.GalleryImages)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("عکس های گالری"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس های گالری"))
            .JustImageFile();
    }
}