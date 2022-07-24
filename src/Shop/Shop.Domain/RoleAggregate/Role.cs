using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.RoleAggregate;

public class Role : BaseAggregateRoot
{
    public string Title { get; private set; }

    private List<RolePermission> _permissions = new();
    public IEnumerable<RolePermission> Permissions => _permissions.ToList();

    private Role()
    {
        
    }

    public Role(string title, List<RolePermission> permissions)
    {
        Guard(title);
        Title = title;
        _permissions = permissions;
    }

    public void Edit(string title, List<RolePermission> permissions)
    {
        Guard(title);
        Title = title;
        _permissions = permissions;
    }

    private void Guard(string title)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
    }
}