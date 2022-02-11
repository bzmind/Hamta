using Domain.Comment_Aggregate;
using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;
using Domain.Shared.Value_Objects;

namespace Domain.Product_Aggregate;

public class Product : BaseAggregateRoot
{
    public long CategoryId { get; private set; }
    public string Name { get; private set; }
    public Score Score { get; private set; } = new Score(0);
    public Money Price { get; private set; }
    public bool IsInStock { get; private set; }
    public List<ProductImage> Images { get; private set; }
    public List<Comment> Comments { get; private set; }
    public List<ProductSpecification> Specifications { get; private set; }

    public Product(long categoryId, string name, Money price)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        CategoryId = categoryId;
        Name = name;
        Price = price;
        IsInStock = true;
        Images = new List<ProductImage>();
        Comments = new List<Comment>();
    }

    public void Edit(string name, Money price, Score score, bool inStock)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        Name = name;
        Price = price;
        Score = score;
        IsInStock = inStock;
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
}