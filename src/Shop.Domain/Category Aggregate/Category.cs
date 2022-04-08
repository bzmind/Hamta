using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;
using Shop.Domain.Category_Aggregate.Services;

namespace Shop.Domain.Category_Aggregate;

public class Category : BaseAggregateRoot
{
    public long? ParentId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public List<Category> SubCategories { get; private set; }
    public List<CategorySpecification> Specifications { get; private set; }

    private readonly ICategoryDomainService _categoryDomainService;

    public Category(long? parentId, string title, string slug, ICategoryDomainService categoryDomainService)
    {
        Validate(title, slug);
        Title = title;
        Slug = slug;
        SubCategories = new List<Category>();
        Specifications = new List<CategorySpecification>();
        _categoryDomainService = categoryDomainService;
    }

    public void Edit(long? parentId, string title, string slug)
    {
        Validate(title, slug);
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

        SubCategories = subCategories;
    }

    public void SetSpecifications(List<CategorySpecification> specifications)
    {
        if (_categoryDomainService.IsThirdCategory(Id) || ParentId == null)
            throw new OperationNotAllowedDomainException("This category can't have specifications");

        specifications.ForEach(specification =>
        {
            specification.CategoryId = Id;
        });

        Specifications = specifications;
    }

    private void Validate(string title, string slug)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(slug, nameof(slug));

        if (_categoryDomainService.IsDuplicateSlug(slug))
            throw new SlugAlreadyExistsDomainException("Slug is already used, cannot use duplicated slug");
    }
}