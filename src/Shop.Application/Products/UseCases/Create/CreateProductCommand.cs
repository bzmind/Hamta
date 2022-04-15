﻿using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.FileUtility;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ProductAggregate;
using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.ProductAggregate.Services;

namespace Shop.Application.Products.UseCases.Create;

public record CreateProductCommand(long CategoryId, string Name, string? EnglishName, string Slug,
    string Description, IFormFile MainImage, List<IFormFile> GalleryImages,
    Dictionary<string, string>? CustomSpecifications, List<bool>? ImportantFeatures,
    Dictionary<string, string>? ExtraDescriptions) : IBaseCommand;

public class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
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

    public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.CategoryId, request.Name, request.EnglishName, request.Slug,
            request.Description, _productDomainService);

        await _productRepository.AddAsync(product);

        var mainImage = await _fileService.SaveFileAndGenerateName(request.MainImage,
            Directories.ProductMainImages);
        product.AddMainImage(mainImage);

        var galleryImages =
            await _fileService.SaveMultipleFilesAndGenerateNames(request.GalleryImages,
                Directories.ProductGalleryImages);
        product.AddGalleryImages(galleryImages);

        var customSpecifications = new List<ProductSpecification>();
        if (request.CustomSpecifications != null)
        {
            for (var i = 0; i < request.CustomSpecifications.Count; i++)
            {
                customSpecifications.Add(new ProductSpecification(product.Id,
                    request.CustomSpecifications.Keys.ElementAt(i),
                    request.CustomSpecifications.Values.ElementAt(i), request.ImportantFeatures[i]));
            }
        }
        product.SetCustomSpecifications(customSpecifications);

        var extraDescriptions = new List<ProductExtraDescription>();
        if (request.ExtraDescriptions != null)
        {
            request.ExtraDescriptions.ToList().ForEach(extraDescription =>
            {
                extraDescriptions.Add(new ProductExtraDescription(product.Id, extraDescription.Key,
                    extraDescription.Value));
            });
        }
        product.SetExtraDescriptions(extraDescriptions);

        await _productRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام محصول"));

        RuleFor(p => p.Slug)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("Slug"));

        RuleFor(p => p.Description)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("توضیحات"));

        RuleFor(p => p.MainImage)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس اصلی محصول"));

        RuleFor(p => p.GalleryImages)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("عکس های گالری"));
    }
}