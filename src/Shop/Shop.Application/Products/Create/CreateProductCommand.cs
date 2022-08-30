using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.FileUtility;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Application.Products._DTOs;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.ProductAggregate;
using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.ProductAggregate.Services;

namespace Shop.Application.Products.Create;

public class CreateProductCommand : IBaseCommand<long>
{
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string? EnglishName { get; set; }
    public string Slug { get; set; }
    public string? Introduction { get; set; }
    public string? Review { get; set; }
    public IFormFile MainImage { get; set; }
    public List<IFormFile> GalleryImages { get; set; }
    public List<ProductSpecificationDto>? Specifications { get; set; }
    public List<ProductCategorySpecificationDto>? CategorySpecifications { get; set; }
}

public class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand, long>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IFileService _fileService;

    public CreateProductCommandHandler(IProductRepository productRepository, IFileService fileService,
        IProductDomainService productDomainService, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _productDomainService = productDomainService;
        _fileService = fileService;
        _categoryRepository = categoryRepository;
    }

    public async Task<OperationResult<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.CategoryId, request.Name, request.EnglishName, request.Slug,
            request.Introduction, request.Review, _productDomainService);

        var category = await _categoryRepository.GetAsTrackingAsync(request.CategoryId);
        if (category == null)
            return OperationResult<long>.NotFound(ValidationMessages.FieldNotFound("دسته‌بندی"));

        if (category.Specifications.Any()
            && (request.CategorySpecifications == null || !request.CategorySpecifications.Any()))
            return OperationResult<long>.Error(ValidationMessages.CategorySpecificationRequired);

        await _productRepository.AddAsync(product);

        if (request.CategorySpecifications != null && request.CategorySpecifications.Any())
        {
            var emptyOptionalSpecifications = new List<ProductCategorySpecificationDto>();

            foreach (var spec in request.CategorySpecifications)
            {
                var categorySpec = category.Specifications.FirstOrDefault(s => s.Id == spec.CategorySpecificationId);

                if (categorySpec == null)
                    return OperationResult<long>.Error("مشخصه دسته‌بندی یافت نشد");

                if (categorySpec.IsOptional == false && string.IsNullOrWhiteSpace(spec.Description))
                    return OperationResult<long>.Error("لطفا مشخصات اجباری را وارد کنید");

                if (categorySpec.IsOptional && string.IsNullOrWhiteSpace(spec.Description))
                    emptyOptionalSpecifications.Add(spec);
            }

            if (emptyOptionalSpecifications.Count > 0)
                emptyOptionalSpecifications.ForEach(spec =>
                {
                    request.CategorySpecifications?.Remove(spec);
                });

            var categorySpecifications = new List<ProductCategorySpecification>();

            request.CategorySpecifications.ToList().ForEach(specification =>
                categorySpecifications.Add(new ProductCategorySpecification(product.Id, specification.CategorySpecificationId, specification.Description)));

            product.SetCategorySpecifications(categorySpecifications);
        }

        var mainImage = await _fileService.SaveFileAndGenerateName(request.MainImage,
            Directories.ProductMainImages);
        product.SetMainImage(mainImage);

        var galleryImagesNames =
            await _fileService.SaveMultipleFilesAndGenerateNames(request.GalleryImages,
                Directories.ProductGalleryImages);

        product.SetGalleryImages(galleryImagesNames);

        if (request.Specifications != null)
        {
            var customSpecifications = new List<ProductSpecification>();

            request.Specifications.ToList().ForEach(specification =>
                customSpecifications.Add(new ProductSpecification(product.Id, specification.Title,
                    specification.Description)));

            product.SetSpecifications(customSpecifications);
        }
        
        await _productRepository.SaveAsync();
        return OperationResult<long>.Success(product.Id);
    }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage(ValidationMessages.CategoryIdRequired);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ValidationMessages.ProductNameRequired)
            .MaximumLength(2000).WithMessage(ValidationMessages.FieldCharactersMaxLength("نام محصول", 50));

        RuleFor(p => p.EnglishName)
            .MaximumLength(2000).WithMessage(ValidationMessages.FieldCharactersMaxLength("نام انگلیسی محصول", 50));

        RuleFor(p => p.Slug)
            .NotEmpty().WithMessage(ValidationMessages.SlugRequired)
            .MaximumLength(100).WithMessage(ValidationMessages.FieldCharactersMaxLength("اسلاگ", 100));

        RuleFor(p => p.Introduction)
            .MaximumLength(2000).WithMessage(ValidationMessages.FieldCharactersMaxLength("معرفی", 2000));

        RuleFor(p => p.Review)
            .MaximumLength(10000).WithMessage(ValidationMessages.FieldCharactersMaxLength("بررسی تخصصی", 10000));

        RuleFor(p => p.MainImage)
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس اصلی محصول"))
            .JustImageFile();

        RuleForEach(p => p.GalleryImages)
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس گالری"))
            .JustImageFile();

        RuleForEach(p => p.Specifications).ChildRules(specification =>
        {
            specification.RuleFor(spec => spec.Title)
                .NotEmpty().WithMessage(ValidationMessages.TitleRequired)
                .MaximumLength(100).WithMessage(ValidationMessages.FieldCharactersMaxLength("عنوان", 100));

            specification.RuleFor(spec => spec.Description)
                .NotEmpty().WithMessage(ValidationMessages.DescriptionRequired)
                .MaximumLength(300).WithMessage(ValidationMessages.FieldCharactersMaxLength("توضیحات", 300));
        });

        RuleForEach(p => p.CategorySpecifications).ChildRules(categorySpecification =>
        {
            categorySpecification.RuleFor(p => p.CategorySpecificationId)
                .NotEmpty().WithMessage(ValidationMessages.CategorySpecificationIdRequired);

            categorySpecification.RuleFor(spec => spec.Description)
                .MaximumLength(300).WithMessage(ValidationMessages.FieldCharactersMaxLength("توضیحات", 300));
        });
    }
}