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

namespace Shop.Application.Products.Edit;

public class EditProductCommand : IBaseCommand
{
    public long ProductId { get; set; }
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string? EnglishName { get; set; }
    public string Slug { get; set; }
    public string? Introduction { get; set; }
    public string? Review { get; set; }
    public IFormFile? MainImage { get; set; }
    public List<IFormFile>? GalleryImages { get; set; }
    public List<ProductSpecificationDto>? Specifications { get; set; }
    public List<ProductCategorySpecificationDto>? CategorySpecifications { get; set; }
}

public class EditProductCommandHandler : IBaseCommandHandler<EditProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;
    private readonly IProductDomainService _productDomainService;
    private readonly ICategoryRepository _categoryRepository;

    public EditProductCommandHandler(IProductRepository productRepository, IFileService fileService,
        IProductDomainService productDomainService, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _fileService = fileService;
        _productDomainService = productDomainService;
        _categoryRepository = categoryRepository;
    }

    public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsTrackingAsync(request.ProductId);
        if (product == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("محصول"));

        var category = await _categoryRepository.GetAsTrackingAsync(request.CategoryId);
        if (category == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("دسته‌بندی"));

        if (category.Specifications.Any()
            && (request.CategorySpecifications == null || !request.CategorySpecifications.Any()))
            return OperationResult.Error(ValidationMessages.CategorySpecificationRequired);
        
        if (request.CategorySpecifications != null && request.CategorySpecifications.Any())
        {
            var emptyOptionalSpecifications = new List<ProductCategorySpecificationDto>();

            foreach (var spec in request.CategorySpecifications)
            {
                var categorySpec = category.Specifications.FirstOrDefault(s => s.Id == spec.CategorySpecificationId);

                if (categorySpec == null)
                    return OperationResult.Error("مشخصه دسته‌بندی یافت نشد");

                if (categorySpec.IsOptional == false && string.IsNullOrWhiteSpace(spec.Description))
                    return OperationResult.Error("لطفا مشخصات اجباری را وارد کنید");

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

        product.Edit(request.CategoryId, request.Name, request.EnglishName, request.Slug, request.Introduction,
            request.Review, _productDomainService);

        var oldImage = product.MainImage;

        if (request.MainImage != null)
        {
            _fileService.DeleteFile(Directories.ProductMainImages, oldImage);

            var newMainImage =
                await _fileService.SaveFileAndGenerateName(request.MainImage, Directories.ProductMainImages);

            product.SetMainImage(newMainImage);
        }

        if (request.GalleryImages != null && request.GalleryImages.Any())
        {
            var oldGalleryImages = product.GalleryImages.Select(img => img.Name).ToList();

            _fileService.DeleteMultipleFiles(Directories.ProductGalleryImages, oldGalleryImages);

            var galleryImagesNames =
                await _fileService.SaveMultipleFilesAndGenerateNames(request.GalleryImages,
                    Directories.ProductGalleryImages);

            product.SetGalleryImages(galleryImagesNames);
        }

        if (request.Specifications != null && request.Specifications.Any())
        {
            var customSpecifications = new List<ProductSpecification>();

            request.Specifications.ToList().ForEach(specification =>
                customSpecifications.Add(new ProductSpecification(product.Id, specification.Title,
                    specification.Description)));

            product.SetSpecifications(customSpecifications);
        }

        await _productRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(p => p.ProductId)
            .NotEmpty().WithMessage(ValidationMessages.ProductIdRequired);

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
            .JustImageFile();

        RuleForEach(p => p.GalleryImages)
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