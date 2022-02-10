using Domain.Product_Aggregate;
using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;

namespace Domain.Category_Aggregate;

public class Category : BaseAggregateRoot
{
    public string Title { get; private set; }
    public long? ParentId { get; private set; }
    public List<Category> SubCategories { get; private set; }
    //public List<long> ProductIds { get; private set; }

    public Category(string title)
    {
        Validate(title);
        Title = title;
        SubCategories = new List<Category>();
        //ProductIds = new List<long>();
    }

    public void AddSubCategory(string title)
    {
        Validate(title);
        SubCategories.Add(new Category(title));
    }

    public void RemoveSubCategory(long subCategoryId)
    {
        var subCategory = SubCategories.FirstOrDefault(sc => sc.Id == subCategoryId);
        if (subCategory != null)
            SubCategories.Remove(subCategory);
    }

    public void Edit(string title)
    {
        Validate(title);
        Title = title;
    }

    private void Validate(string title)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
    }
}