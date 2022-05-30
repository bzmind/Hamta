﻿using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Shop.Domain.CategoryAggregate.Services;

namespace Shop.Domain.CategoryAggregate;

public class Category : BaseAggregateRoot
{
    public long? ParentId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }

    private readonly List<Category> _subCategories = new();
    public IEnumerable<Category> SubCategories => _subCategories.ToList();

    private List<CategorySpecification> _specifications = new();
    public IEnumerable<CategorySpecification> Specifications => _specifications.ToList();

    private Category()
    {

    }

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

    public void SetSpecifications(List<CategorySpecification> specifications)
    {
        _specifications = specifications;
    }

    private void Guard(string title, string slug, ICategoryDomainService categoryDomainService)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(slug, nameof(slug));

        if (categoryDomainService.IsDuplicateSlug(slug))
            throw new SlugAlreadyExistsDomainException("Slug is already used");
    }
}