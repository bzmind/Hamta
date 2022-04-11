using System.Collections.ObjectModel;
using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Domain.CategoryAggregate;

public class Category : BaseAggregateRoot
{
    public long? ParentId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }

    private List<Category> _subCategories = new List<Category>();
    public ReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();

    private List<CategorySpecification> _specifications = new List<CategorySpecification>();
    public ReadOnlyCollection<CategorySpecification> Specifications => _specifications.AsReadOnly();

    public Category(long? parentId, string title, string slug, ICategoryDomainService categoryDomainService)
    {
        Guard(title, slug, categoryDomainService);
        Title = title;
        Slug = slug;
    }

    public void Edit(long? parentId, string title, string slug, ICategoryDomainService categoryDomainService)
    {
        Guard(title, slug, categoryDomainService);
        Title = title;
        Slug = slug;
    }

    public void SetSubCategories(List<Category> subCategories)
    {
        subCategories.ForEach(subCategory =>
        {
            subCategory.ParentId = Id;
        });

        _subCategories = subCategories;
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