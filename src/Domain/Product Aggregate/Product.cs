using Domain.Category_Aggregate;
using Domain.Comment_Aggregate;
using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;
using Domain.Shared.Value_Objects;

namespace Domain.Product_Aggregate;

public class Product : BaseAggregateRoot
{
    public long CategoryId { get; private set; }
    public string Name { get; private set; }
    public string EnglishName { get; private set; }
    public string Description { get; private set; }
    public Score Score { get; private set; } = new (0);
    public Money Price { get; private set; }
    public bool IsInStock { get; private set; }
    public bool IsInSale { get; private set; }
    public List<ProductImage> Images { get; private set; }
    public List<Comment> Comments { get; private set; }
    public List<ProductQuestion> Questions { get; private set; }
    public List<ProductAnswer> Answers { get; private set; }
    public List<ProductSpecification> CustomSpecifications { get; private set; }
    public List<CategorySpecification> CategorySpecifications { get; private set; }
    public List<ProductExtraDescription> ExtraDescriptions { get; private set; }
    public List<string> Colors { get; private set; }

    public Product(long categoryId, string name, Money price, bool isInSale, List<ProductImage> images,
        List<CategorySpecification> categorySpecifications)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        CategoryId = categoryId;
        Name = name;
        Price = price;
        IsInStock = true;
        IsInSale = isInSale;
        Images = images;
        CategorySpecifications = categorySpecifications;
    }

    public void Edit(long categoryId, string name, Money price, bool isInSale, List<ProductImage> images,
        List<CategorySpecification> categorySpecifications)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        CategoryId = categoryId;
        Name = name;
        Price = price;
        IsInSale = isInSale;
        Images = images;
        CategorySpecifications = categorySpecifications;
    }

    public void AddImage(string imageName)
    {
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

    public void AddComment(Comment comment)
    {
        NullOrEmptyDataDomainException.CheckData(comment, nameof(comment));
        Comments.Add(comment);
    }

    public void RemoveComment(long commentId)
    {
        var comment = Comments.FirstOrDefault(c => c.Id == commentId);
        NullOrEmptyDataDomainException.CheckData(comment, nameof(comment));
        Comments.Remove(comment);
    }

    public void AddQuestion(ProductQuestion question)
    {
        NullOrEmptyDataDomainException.CheckData(question, nameof(question));
        Questions.Add(question);
    }

    public void RemoveQuestion(long questionId)
    {
        var question = Questions.FirstOrDefault(q => q.Id == questionId);
        NullOrEmptyDataDomainException.CheckData(question, nameof(question));
        Questions.Remove(question);
    }

    public void AddAnswer(ProductAnswer answer)
    {
        NullOrEmptyDataDomainException.CheckData(answer, nameof(answer));
        Answers.Add(answer);
    }

    public void RemoveAnswer(long answerId)
    {
        var answer = Answers.FirstOrDefault(a => a.Id == answerId);
        NullOrEmptyDataDomainException.CheckData(answer, nameof(answer));
        Answers.Remove(answer);
    }
}