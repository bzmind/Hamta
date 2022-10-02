using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Shop.Domain.ProductAggregate.Services;

namespace Shop.Domain.ProductAggregate;

public class Product : BaseAggregateRoot
{
    public long CategoryId { get; private set; }
    public string Name { get; private set; }
    public string? EnglishName { get; private set; }
    public string Slug { get; private set; }
    public string? Introduction { get; private set; }
    public string? Review { get; private set; }
    public string MainImage { get; private set; }

    private readonly List<ProductGalleryImage> _galleryImages = new();
    public IEnumerable<ProductGalleryImage> GalleryImages => _galleryImages.ToList();

    private List<ProductSpecification> _specifications = new();
    public IEnumerable<ProductSpecification> Specifications => _specifications.ToList();

    private List<ProductCategorySpecification> _categorySpecifications = new();
    public IEnumerable<ProductCategorySpecification> CategorySpecifications => _categorySpecifications.ToList();

    private Product()
    {

    }

    public Product(long categoryId, string name, string? englishName, string slug, string? introduction,
        string? review, IProductDomainService productService)
    {
        Guard(name, slug, productService);
        CategoryId = categoryId;
        Name = name;
        EnglishName = englishName;
        Introduction = introduction;
        Slug = slug;
        Review = review;
    }

    public void Edit(long categoryId, string name, string? englishName, string slug, string? description,
        string? review, IProductDomainService productService)
    {
        Guard(name, slug, productService);
        CategoryId = categoryId;
        Name = name;
        EnglishName = englishName;
        Slug = slug;
        Introduction = description;
        Review = review;
    }

    public void SetMainImage(string mainImage)
    {
        NullOrEmptyDataDomainException.CheckString(mainImage, nameof(mainImage));
        MainImage = mainImage;
    }

    public void SetGalleryImages(List<string> galleryImagesNames)
    {
        galleryImagesNames.ForEach(imageName =>
        {
            NullOrEmptyDataDomainException.CheckString(imageName, "Gallery image name");
        });

        _galleryImages.Clear();

        for (var i = 0; i < galleryImagesNames.Count; i++)
        {
            _galleryImages.Add(new ProductGalleryImage(Id, galleryImagesNames[i], i + 1));
        }
    }
    
    public void SetSpecifications(List<ProductSpecification> specifications)
    {
        _specifications = specifications;
    }
    
    public void SetCategorySpecifications(List<ProductCategorySpecification> categorySpecifications)
    {
        _categorySpecifications = categorySpecifications;
    }

    private void Guard(string name, string slug, IProductDomainService productService)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        NullOrEmptyDataDomainException.CheckString(slug, nameof(slug));

        if (productService.IsDuplicateSlug(Id, slug))
            throw new SlugAlreadyExistsDomainException("Slug is already used, cannot use duplicated slug");
    }
}