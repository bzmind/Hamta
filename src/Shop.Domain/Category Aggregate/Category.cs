using Common.Domain.BaseClasses;
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

    public Category(long? parentId, string title, string slug, ICategoryDomainService categoryService)
    {
        Validate(title, slug, categoryService);
        Title = title;
        Slug = slug;
        SubCategories = new List<Category>();
        Specifications = new List<CategorySpecification>();
    }

    public void Edit(long? parentId, string title, string slug, ICategoryDomainService categoryService)
    {
        Validate(title, slug, categoryService);
        Title = title;
        Slug = slug;
    }

    public void SetSubCategories(List<Category> subCategories, ICategoryDomainService categoryService)
    {
        if (categoryService.IsThirdCategory(Id))
            throw new OperationNotAllowedDomainException("This category can't have any sub categories");

        subCategories.ForEach(subCategory =>
        {
            subCategory.ParentId = Id;
        });

        SubCategories = subCategories;
    }

    // I probably don't need these edit methods, because I can just edit the whole category object
    // at once, and just call the SetSubCategories method to set the whole thing all together, yeah.
    public void EditSubCategory(long subCategoryId, long? parentId, string title, string slug,
        ICategoryDomainService categoryService)
    {
        Validate(title, slug, categoryService);

        var subcategory = SubCategories.FirstOrDefault(sc => sc.Id == subCategoryId);

        if (subcategory == null)
            throw new InvalidDataDomainException($"No sub category was found with this ID: {subCategoryId}");

        subcategory.Edit(parentId, title, slug, categoryService);
    }

    public void RemoveSubCategory(long subCategoryId)
    {
        var subcategory = SubCategories.FirstOrDefault(sc => sc.Id == subCategoryId);

        if (subcategory == null)
            throw new InvalidDataDomainException($"No sub category was found with this ID: {subCategoryId}");

        SubCategories.Remove(subcategory);
    }

    public void SetSpecifications(List<CategorySpecification> specifications,
        ICategoryDomainService categoryService)
    {
        if (categoryService.IsThirdCategory(Id) || ParentId == null)
            throw new OperationNotAllowedDomainException("This category can't have specifications");

        specifications.ForEach(specification =>
        {
            specification.CategoryId = Id;
        });

        Specifications = specifications;
    }
    
    public void EditSpecification(long specificationId, string specificationTitle)
    {
        var specification = Specifications.FirstOrDefault(sc => sc.Id == specificationId);

        if (specification == null)
            throw new InvalidDataDomainException("No such specification was found for this category");

        specification.Edit(specificationTitle);
    }

    public void RemoveSpecification(long specificationId)
    {
        var specification = Specifications.FirstOrDefault(sc => sc.Id == specificationId);

        if (specification == null)
            throw new InvalidDataDomainException("No such specification was found for this category");

        Specifications.Remove(specification);
    }

    private void Validate(string title, string slug, ICategoryDomainService categoryService)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(slug, nameof(slug));

        if (categoryService.IsDuplicateSlug(slug))
            throw new SlugAlreadyExistsDomainException("Slug is already used, cannot use duplicated slug");
    }
}