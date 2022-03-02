using Domain.Category_Aggregate.Services;
using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;

namespace Domain.Category_Aggregate;

public class Category : BaseAggregateRoot
{
    public long? ParentId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public List<Category> SubCategories { get; private set; }
    public List<CategorySpecification> Specifications { get; private set; }

    public Category(string title, string slug, ICategoryDomainService categoryService)
    {
        Validate(title, slug, categoryService);
        Title = title;
        Slug = slug;
        SubCategories = new List<Category>();
        Specifications = new List<CategorySpecification>();
    }

    public void Edit(string title, string slug, ICategoryDomainService categoryService)
    {
        Validate(title, slug, categoryService);
        Title = title;
        Slug = slug;
    }

    public void AddSubCategory(string title, string slug, ICategoryDomainService categoryService)
    {
        Validate(title, slug, categoryService);
        var subcategory = new Category(title, slug, categoryService);
        SubCategories.Add(subcategory);
    }

    public void EditSubCategory(long subCategoryId, string title, string slug,
        ICategoryDomainService categoryService)
    {
        Validate(title, slug, categoryService);

        var subcategory = SubCategories.FirstOrDefault(sc => sc.Id == subCategoryId);

        if (subcategory == null)
            throw new InvalidDataDomainException($"No SubCategory was found with this ID: {subCategoryId}");

        subcategory.Edit(title, slug, categoryService);
    }

    public void RemoveSubCategory(long subCategoryId)
    {
        var subcategory = SubCategories.FirstOrDefault(sc => sc.Id == subCategoryId);

        if (subcategory == null)
            throw new InvalidDataDomainException($"No SubCategory was found with this ID: {subCategoryId}");

        SubCategories.Remove(subcategory);
    }

    public void AddSpecification(CategorySpecification specification)
    {
        NullOrEmptyDataDomainException.CheckString(specification.Title, nameof(specification.Title));
        Specifications.Add(specification);
    }
    
    public void EditSpecification(long specificationId, string specificationTitle)
    {
        NullOrEmptyDataDomainException.CheckString(specificationTitle, nameof(specificationTitle));
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

        if (categoryService.DoesSlugAlreadyExist(slug))
            throw new SlugAlreadyExistsDomainException("Slug is already used, cannot use duplicated slug");
    }
}