using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Application.Products._DTOs;
using Shop.Domain.ProductAggregate;
using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.ProductAggregate.Services;

namespace Shop.Application.Products.Create;

public record CreateProductCommand(long CategoryId, string Name, string? EnglishName, string Slug,
    string? Description, IFormFile MainImage, List<IFormFile> GalleryImages,
    List<ProductSpecificationDto>? CustomSpecifications,
    List<ProductExtraDescriptionDto>? ExtraDescriptions) : IBaseCommand<long>;

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
                    specification.Description)));

            product.SetCustomSpecifications(customSpecifications);
        }

        if (request.ExtraDescriptions != null)
        {
            var extraDescriptions = new List<ProductExtraDescription>();

            request.ExtraDescriptions.ToList().ForEach(extraDescription =>
                extraDescriptions.Add(new ProductExtraDescription(product.Id, extraDescription.Title,
                    extraDescription.Description)));

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
            .NotNull().WithMessage(ValidationMessages.ProductNameRequired)
            .NotEmpty().WithMessage(ValidationMessages.ProductNameRequired)
            .MaximumLength(2000).WithMessage(ValidationMessages.FieldCharactersMaxLength("نام محصول", 50));

        RuleFor(p => p.Name)
            .MaximumLength(2000).WithMessage(ValidationMessages.FieldCharactersMaxLength("نام انگلیسی محصول", 50));

        RuleFor(p => p.Slug)
            .NotNull().WithMessage(ValidationMessages.SlugRequired)
            .NotEmpty().WithMessage(ValidationMessages.SlugRequired)
            .MaximumLength(100).WithMessage(ValidationMessages.FieldCharactersMaxLength("اسلاگ", 100));

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

        RuleForEach(p => p.CustomSpecifications).ChildRules(specification =>
        {
            specification.RuleFor(spec => spec.Title)
                .NotNull().WithMessage(ValidationMessages.TitleRequired)
                .NotEmpty().WithMessage(ValidationMessages.TitleRequired)
                .MaximumLength(100).WithMessage(ValidationMessages.FieldCharactersMaxLength("عنوان", 100));

            specification.RuleFor(spec => spec.Description)
                .NotNull().WithMessage(ValidationMessages.TitleRequired)
                .NotEmpty().WithMessage(ValidationMessages.TitleRequired)
                .MaximumLength(300).WithMessage(ValidationMessages.FieldCharactersMaxLength("توضیحات", 300));
        });

        RuleForEach(p => p.ExtraDescriptions).ChildRules(specification =>
        {
            specification.RuleFor(description => description.Title)
                .NotNull().WithMessage(ValidationMessages.TitleRequired)
                .NotEmpty().WithMessage(ValidationMessages.TitleRequired)
                .MaximumLength(100).WithMessage(ValidationMessages.FieldCharactersMaxLength("عنوان", 100));

            specification.RuleFor(description => description.Description)
                .NotNull().WithMessage(ValidationMessages.DescriptionRequired)
                .NotEmpty().WithMessage(ValidationMessages.DescriptionRequired)
                .MaximumLength(2000).WithMessage(ValidationMessages.FieldCharactersMaxLength("توضیحات", 2000));
        });
    }
}