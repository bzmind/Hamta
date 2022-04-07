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
    public List<ProductImage> Images { get; private set; }
    public List<ProductSpecification> CustomSpecifications { get; private set; }
    public List<ProductExtraDescription> ExtraDescriptions { get; private set; }
    public List<ProductQuestion> Questions { get; private set; }

    public Product(long categoryId, string name, string slug, string description, List<ProductImage> images,
        IProductDomainService productService)
    {
        Validate(name, slug, description, productService);
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Slug = slug;
        Images = images;
        CustomSpecifications = new List<ProductSpecification>();
        ExtraDescriptions = new List<ProductExtraDescription>();
        Questions = new List<ProductQuestion>();
    }

    public void Edit(long categoryId, string name, string slug, string description, List<ProductImage> images,
        IProductDomainService productService)
    {
        Validate(name, slug, description, productService);
        CategoryId = categoryId;
        Name = name;
        Slug = slug;
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


    public void AddQuestion(ProductQuestion question)
    {
        Questions.Add(question);
    }

    public void RemoveQuestion(long questionId)
    {
        var question = Questions.FirstOrDefault(q => q.Id == questionId);

        if (question == null)
            throw new NullOrEmptyDataDomainException("No such answer was found for this question");

        Questions.Remove(question);
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

    private void Validate(string name, string slug, string description, IProductDomainService productService)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        NullOrEmptyDataDomainException.CheckString(slug, nameof(slug));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));

        if (productService.IsDuplicateSlug(slug))
            throw new SlugAlreadyExistsDomainException("Slug is already used, cannot use duplicated slug");
    }
}