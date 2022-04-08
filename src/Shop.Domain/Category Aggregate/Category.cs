using System.Collections.ObjectModel;
using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;
using Shop.Domain.Category_Aggregate.Services;

namespace Shop.Domain.Category_Aggregate;

public class Category : BaseAggregateRoot
{
    public long? ParentId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }

    private List<Category> _subCategories = new List<Category>();
    public ReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();

    private List<CategorySpecification> _specifications = new List<CategorySpecification>();
    public ReadOnlyCollection<CategorySpecification> Specifications => _specifications.AsReadOnly();

    private readonly ICategoryDomainService _categoryDomainService;

    public Category(long? parentId, string title, string slug, ICategoryDomainService categoryDomainService)
    {
        Guard(title, slug);
        Title = title;
        Slug = slug;
        _categoryDomainService = categoryDomainService;
    }

    public void Edit(long? parentId, string title, string slug)
    {
        Guard(title, slug);
        Title = title;
        Slug = slug;
    }

    public void SetSubCategories(List<Category> subCategories)
    {
        if (_categoryDomainService.IsThirdCategory(Id))
            throw new OperationNotAllowedDomainException("This category can't have sub categories");

        subCategories.ForEach(subCategory =>
        {
            subCategory.ParentId = Id;
        });

        _subCategories = subCategories;
    }

    public void SetSpecifications(List<CategorySpecification> specifications)
    {
        if (_categoryDomainService.IsThirdCategory(Id) || ParentId == null)
            throw new OperationNotAllowedDomainException("This category can't have specifications");

        specifications.ForEach(specification =>
        {
            specification.CategoryId = Id;
        });

        _specifications = specifications;
    }

    private void Guard(string title, string slug)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(slug, nameof(slug));

        if (_categoryDomainService.IsDuplicateSlug(slug))
            throw new SlugAlreadyExistsDomainException("Slug is already used, cannot use duplicated slug");
    }
}