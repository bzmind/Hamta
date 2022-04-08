using System.Collections.ObjectModel;
using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;
using Shop.Domain.Product_Aggregate.Services;
using Shop.Domain.Product_Aggregate.Value_Objects;

namespace Shop.Domain.Product_Aggregate;

public class Product : BaseAggregateRoot
{
    public long CategoryId { get; private set; }
    public string Name { get; private set; }
    public string? EnglishName { get; private set; }
    public string Slug { get; private set; }
    public string Description { get; private set; }
    public Score Score { get; private set; } = new(0);

    private List<ProductImage> _images;
    public ReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

    private List<ProductSpecification> _customSpecifications = new List<ProductSpecification>();
    public ReadOnlyCollection<ProductSpecification> CustomSpecifications => _customSpecifications.AsReadOnly();

    private List<ProductExtraDescription> _extraDescriptions = new List<ProductExtraDescription>();
    public ReadOnlyCollection<ProductExtraDescription> ExtraDescriptions => _extraDescriptions.AsReadOnly();

    private readonly List<ProductQuestion> _questions = new List<ProductQuestion>();
    public ReadOnlyCollection<ProductQuestion> Questions => _questions.AsReadOnly();

    public Product(long categoryId, string name, string slug, string description, List<ProductImage> images,
        IProductDomainService productService)
    {
        Guard(name, slug, description, productService);
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Slug = slug;
        _images = images;
    }

    public void Edit(long categoryId, string name, string slug, string description, List<ProductImage> images,
        IProductDomainService productService)
    {
        Guard(name, slug, description, productService);
        CategoryId = categoryId;
        Name = name;
        Slug = slug;
        Description = description;
        _images = images;
    }

    public void AddImage(string imageName)
    {
        NullOrEmptyDataDomainException.CheckString(imageName, nameof(imageName));
        _images.Add(new ProductImage(Id, imageName));
    }

    public void RemoveImage(string imageName)
    {
        var image = Images.FirstOrDefault(i => i.Name == imageName);

        if (image == null)
            throw new InvalidDataDomainException($"No image was found with the provided name: {imageName}");

        _images.Remove(image);
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


    public void AddQuestion(ProductQuestion question)
    {
        _questions.Add(question);
    }

    public void RemoveQuestion(long questionId)
    {
        var question = Questions.FirstOrDefault(q => q.Id == questionId);

        if (question == null)
            throw new NullOrEmptyDataDomainException("No such answer was found for this question");

        _questions.Remove(question);
    }
    
    public void AddAnswer(long questionId, ProductAnswer answer)
    {
        var question = Questions.FirstOrDefault(q => q.Id == questionId);

        if (question == null)
            throw new NullOrEmptyDataDomainException("No such question was found");

        question.AddAnswer(answer);
    }

    public void RemoveAnswer(long answerParentId, long answerId)
    {
        var question = Questions.FirstOrDefault(q => q.Id == answerParentId);

        if (question == null)
            throw new NullOrEmptyDataDomainException("No such question was found for this product");
        
        question.RemoveAnswer(answerId);
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