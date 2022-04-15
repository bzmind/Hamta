using System.Collections.ObjectModel;
using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Shop.Domain.ProductAggregate.Services;
using Shop.Domain.ProductAggregate.Value_Objects;
using Shop.Domain.QuestionAggregate;

namespace Shop.Domain.ProductAggregate;

public class Product : BaseAggregateRoot
{
    public long CategoryId { get; private set; }
    public string Name { get; private set; }
    public string? EnglishName { get; private set; }
    public string Slug { get; private set; }
    public string Description { get; private set; }
    public Score Score { get; private set; }
    public ProductImage MainImage { get; private set; }

    private List<ProductImage> _galleryImages = new List<ProductImage>();
    public ReadOnlyCollection<ProductImage> GalleryImages => _galleryImages.AsReadOnly();

    private List<ProductSpecification> _customSpecifications = new List<ProductSpecification>();
    public ReadOnlyCollection<ProductSpecification> CustomSpecifications => _customSpecifications.AsReadOnly();

    private List<ProductExtraDescription> _extraDescriptions = new List<ProductExtraDescription>();
    public ReadOnlyCollection<ProductExtraDescription> ExtraDescriptions => _extraDescriptions.AsReadOnly();

    public Product(long categoryId, string name, string? englishName, string slug, string description,
        IProductDomainService productService)
    {
        Guard(name, slug, description, productService);
        CategoryId = categoryId;
        Name = name;
        EnglishName = englishName;
        Description = description;
        Slug = slug;
        Score = new(0);
    }

    public void Edit(long categoryId, string name, string slug, string description, ProductImage mainImage,
        List<ProductImage> images, IProductDomainService productService)
    {
        Guard(name, slug, description, productService);
        CategoryId = categoryId;
        Name = name;
        Slug = slug;
        Description = description;
        MainImage = mainImage;
        _galleryImages = images;
    }

    public void AddMainImage(string mainImage)
    {
        NullOrEmptyDataDomainException.CheckString(mainImage, nameof(mainImage));
        MainImage = new ProductImage(Id, mainImage);
    }

    public void AddGalleryImages(List<string> galleryImages)
    {
        galleryImages.ForEach(galleryImage =>
        {
            NullOrEmptyDataDomainException.CheckString(galleryImage, nameof(galleryImage));
            _galleryImages.Add(new ProductImage(Id, galleryImage));
        });
    }

    public void RemoveImage(string imageName)
    {
        var image = GalleryImages.FirstOrDefault(i => i.Name == imageName);

        if (image == null)
            throw new InvalidDataDomainException("Image not found for this product");

        _galleryImages.Remove(image);
    }

    public void SetCustomSpecifications(List<ProductSpecification> customSpecifications)
    {
        _customSpecifications = customSpecifications;
    }

    public void SetExtraDescriptions(List<ProductExtraDescription> extraDescriptions)
    {
        _extraDescriptions = extraDescriptions;
    }

    public void SetScore(Score score)
    {
        Score = score;
    }

    public void SetEnglishName(string englishName)
    {
        NullOrEmptyDataDomainException.CheckString(englishName, nameof(englishName));
        EnglishName = englishName;
    }

    private void Guard(string name, string slug, string description, IProductDomainService productService)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        NullOrEmptyDataDomainException.CheckString(slug, nameof(slug));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));

        if (productService.IsDuplicateSlug(slug))
            throw new SlugAlreadyExistsDomainException("Slug is already used, cannot use duplicated slug");
    }
}