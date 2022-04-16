using System.Collections.ObjectModel;
using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Shop.Domain.ProductAggregate.Services;
using Shop.Domain.ProductAggregate.Value_Objects;

namespace Shop.Domain.ProductAggregate;

public class Product : BaseAggregateRoot
{
    public long CategoryId { get; private set; }
    public string Name { get; private set; }
    public string? EnglishName { get; private set; }
    public string Slug { get; private set; }
    public string Description { get; private set; }

    private readonly List<Score> _scores = new List<Score>();
    public ReadOnlyCollection<Score> Scores => _scores.AsReadOnly();
    public ProductImage MainImage { get; private set; }

    private readonly List<ProductImage> _galleryImages = new List<ProductImage>();
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
    }

    public void Edit(long categoryId, string name, string? englishName, string slug, string description,
        IProductDomainService productService)
    {
        Guard(name, slug, description, productService);
        CategoryId = categoryId;
        Name = name;
        EnglishName = englishName;
        Slug = slug;
        Description = description;
    }

    public void SetMainImage(string mainImage)
    {
        MainImage = new ProductImage(Id, mainImage);
    }

    public void SetGalleryImages(List<string> galleryImages)
    {
        galleryImages.ForEach(galleryImage =>
        {
            _galleryImages.Add(new ProductImage(Id, galleryImage));
        });
    }
    
    public void RemoveGalleryImage(long imageId)
    {
        var image = GalleryImages.FirstOrDefault(i => i.Id == imageId);

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

    public void AddScore(int scoreAmount)
    {
        _scores.Add(new Score(scoreAmount));
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