using Domain.Category_Aggregate;
using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;
using Domain.Shared.Value_Objects;

namespace Domain.Product_Aggregate;

public class Product : BaseAggregateRoot
{
    public long CategoryId { get; private set; }
    public string Name { get; private set; }
    public string? EnglishName { get; private set; }
    public string Slug { get; private set; }
    public string Description { get; private set; }
    public Score Score { get; private set; } = new(0);
    public List<ProductImage> Images { get; private set; }
    public List<ProductSpecification>? CustomSpecifications { get; private set; }
    public List<ProductExtraDescription>? ExtraDescriptions { get; private set; }

    public Product(long categoryId, string name, string slug, string description, List<ProductImage> images)
    {
        Validate(name, slug, description);
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Slug = slug;
        Images = images;
    }

    public void Edit(long categoryId, string name, string slug, string description, List<ProductImage> images)
    {
        Validate(name, slug, description);
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Images = images;
    }

    public void AddImage(string imageName)
    {
        NullOrEmptyDataDomainException.CheckString(imageName, nameof(imageName));
        Images.Add(new ProductImage(Id, imageName));
    }

    public void RemoveImage(string imageName)
    {
        var image = Images.FirstOrDefault(i => i.Name == imageName);

        if (image == null)
            throw new InvalidDataDomainException($"No image was found with the provided name: {imageName}");

        Images.Remove(image);
    }

    public void SetCustomSpecifications(List<ProductSpecification> customSpecifications)
    {
        CustomSpecifications = customSpecifications;
    }

    public void SetExtraDescriptions(List<ProductExtraDescription> extraDescriptions)
    {
        ExtraDescriptions = extraDescriptions;
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

    private void Validate(string name, string slug, string description)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        NullOrEmptyDataDomainException.CheckString(slug, nameof(slug));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}