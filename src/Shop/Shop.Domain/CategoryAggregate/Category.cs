using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Domain.CategoryAggregate;

public class Category : BaseAggregateRoot
{
    public long? ParentId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public bool ShowInMenu { get; set; }

    private readonly List<Category> _subCategories = new();
    public IEnumerable<Category> SubCategories => _subCategories.ToList();

    private List<CategorySpecification> _specifications = new();
    public IEnumerable<CategorySpecification> Specifications => _specifications.ToList();

    private Category()
    {

    }

    public Category(long? parentId, string title, string slug, bool showInMenu,
        ICategoryDomainService categoryDomainService)
    {
        Guard(title, slug, categoryDomainService);
        ParentId = parentId;
        Title = title;
        Slug = slug;
        ShowInMenu = showInMenu;
    }

    public void Edit(long? parentId, string title, string slug, bool showInMenu,
        ICategoryDomainService categoryDomainService)
    {
        Guard(title, slug, categoryDomainService);
        ParentId = parentId;
        Title = title;
        Slug = slug;
        ShowInMenu = showInMenu;
    }

    public void AddSubCategory(Category subCategory)
    {
        _subCategories.Add(subCategory);
    }

    public void EditSpecification(long? id, string title, bool isImportant, bool isOptional, bool isFilterable,
        List<long?> ids)
    {
        if (id == null)
        {
            _specifications.Add(new CategorySpecification(Id, title, isImportant, isOptional, isFilterable));
            return;
        }
        var spec = _specifications.FirstOrDefault(spec => spec.Id == id);
        if (spec == null)
            throw new DataNotFoundDomainException("Specification not found");
        spec.Edit(title, isImportant, isOptional, isFilterable);

        var existingIds = _specifications.Select(s => s.Id).ToList();
        existingIds.ForEach(existingId =>
        {
            var newId = ids.FirstOrDefault(newId => newId == existingId);
            if (newId == null)
                _specifications.Remove(_specifications.First(s => s.Id == existingId));
        });
    }

    public void SetSpecifications(List<CategorySpecification> specifications)
    {
        _specifications = specifications;
    }

    private void Guard(string title, string slug, ICategoryDomainService categoryDomainService)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(slug, nameof(slug));

        if (categoryDomainService.IsDuplicateSlug(Id, slug))
            throw new SlugAlreadyExistsDomainException("Slug is already used");
    }
}