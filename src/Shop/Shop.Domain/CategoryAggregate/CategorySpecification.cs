﻿using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.CategoryAggregate;

public class CategorySpecification : BaseEntity
{
    public long CategoryId { get; internal set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public CategorySpecification(string title, string description)
    {
        Guard(title, description);
        Title = title;
        Description = description;
    }

    public void Edit(string title, string description)
    {
        Guard(title, description);
        Title = title;
        Description = description;
    }

    private void Guard(string title, string description)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}