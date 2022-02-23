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

    public Category(string title, string slug)
    {
        Validate(title, slug);
        Title = title;
        Slug = slug;
        SubCategories = new List<Category>();
        Specifications = new List<CategorySpecification>();
    }

    public void Edit(string title, string slug)
    {
        Validate(title, slug);
        Title = title;
        Slug = slug;
    }

    public void SetParent(long categoryId, ICategoryService _categoryService)
    {
        if (_categoryService.DoesCategoryExist(categoryId) == false)
            throw new DataNotFoundInDataBaseDomainException
                ($"No category exist in database with this ID: {categoryId}");

        ParentId = categoryId;
    }

    public void AddSubCategory(string title, string slug)
    {
        Validate(title, slug);
        var subcategory = new Category(title, slug);

        SubCategories.Add(subcategory);
    }

    public void EditSubCategory(long subCategoryId, string title, string slug)
    {
        Validate(title, slug);

        var subcategory = SubCategories.FirstOrDefault(sc => sc.Id == subCategoryId);

        if (subcategory == null)
            throw new InvalidDataDomainException($"No SubCategory was found with this ID: {subCategoryId}");

        subcategory.Edit(title, slug);
    }

    public void RemoveSubCategory(long subCategoryId)
    {
        var subcategory = SubCategories.FirstOrDefault(sc => sc.Id == subCategoryId);
        NullOrEmptyDataDomainException.CheckData(subcategory, nameof(subcategory));
        SubCategories.Remove(subcategory);
    }

    public void AddSpecification(CategorySpecification specification)
    {
        NullOrEmptyDataDomainException.CheckData(specification, nameof(specification));
        NullOrEmptyDataDomainException.CheckString(specification.Title, nameof(specification.Title));
        Specifications.Add(specification);
    }
    
    public void EditSpecification(long specificationId, string specificationTitle)
    {
        NullOrEmptyDataDomainException.CheckString(specificationTitle, nameof(specificationTitle));
        var specification = Specifications.FirstOrDefault(sc => sc.Id == specificationId);
        NullOrEmptyDataDomainException.CheckData(specification, nameof(specification));
        specification.Edit(specificationTitle);
    }

    public void DeleteSpecification(long specificationId)
    {
        var specification = Specifications.FirstOrDefault(sc => sc.Id == specificationId);
        NullOrEmptyDataDomainException.CheckData(specification, nameof(specification));
        Specifications.Remove(specification);
    }

    private void Validate(string title, string slug)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(slug, nameof(slug));
    }
}