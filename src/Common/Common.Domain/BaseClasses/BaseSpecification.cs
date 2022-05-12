using Common.Domain.Exceptions;

namespace Common.Domain.BaseClasses;

public abstract class BaseSpecification : BaseEntity
{
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public bool IsImportantFeature { get; protected set; }

    protected void Guard(string title, string description)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}