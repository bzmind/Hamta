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
        _permissions.Clear();
        _permissions = permissions;
    }

    public void AddPermissions(List<RolePermission> permissions)
    {
        permissions.ForEach(rp =>
        {
            if (_permissions.Select(p => p.Permission).ToList().Contains(rp.Permission))
                throw new OperationNotAllowedDomainException("Permission already exists");

            _permissions.Add(rp);
        });
    }

    public void RemovePermissions(List<RolePermission> permissions)
    {
        var permissionsToDelete = _permissions
            .Where(p => permissions.Select(rp => rp.Permission).ToList().Contains(p.Permission)).ToList();

        permissionsToDelete.ForEach(p => _permissions.Remove(p));
    }
    
    private void Guard(string title)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
    }
}