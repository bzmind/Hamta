using System.Collections.ObjectModel;
using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Domain.CategoryAggregate;

public class Category : BaseAggregateRoot
{
    public long? ParentId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }

    private readonly List<Category> _subCategories = new List<Category>();
    public ReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();

    private List<CategorySpecification> _specifications = new List<CategorySpecification>();
    public ReadOnlyCollection<CategorySpecification> Specifications => _specifications.AsReadOnly();

    public Category(long? parentId, string title, string slug, ICategoryDomainService categoryDomainService)
    {
        Guard(title, slug, categoryDomainService);
        Title = title;
        Slug = slug;
        ParentId = parentId;
    }

    public void Edit(long? parentId, string title, string slug, ICategoryDomainService categoryDomainService)
    {
        Guard(title, slug, categoryDomainService);
        Title = title;
        Slug = slug;
        ParentId = parentId;
    }

    public void AddSubCategory(Category subCategory)
    {
        _subCategories.Add(subCategory);
    }

    public void RemoveSubCategory(long subCategoryId)
    {
        var subCategory = _subCategories.FirstOrDefault(sc => sc.Id == subCategoryId);

        if (subCategory == null)
            throw new NullOrEmptyDataDomainException("Sub category doesn't exists in this category");

        _subCategories.Remove(subCategory);
    }

    public void SetSpecifications(List<CategorySpecification> specifications)
    {
        specifications.ForEach(specification =>
        {
            specification.CategoryId = Id;
        });

        _specifications = specifications;
    }

    private void Guard(string title, string slug, ICategoryDomainService categoryDomainService)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(slug, nameof(slug));

        if (categoryDomainService.IsDuplicateSlug(slug))
            throw new SlugAlreadyExistsDomainException("Slug is already used, cannot use duplicated slug");
    }
}