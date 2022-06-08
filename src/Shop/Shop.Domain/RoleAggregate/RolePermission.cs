using System.ComponentModel;
using Common.Domain.BaseClasses;

namespace Shop.Domain.RoleAggregate;

public class RolePermission : BaseEntity
{
    public string Permission { get; private set; }

    public enum Permissions
    {
        CategoryManager,
        ColorManager,
        CommentManager,
        InventoryManager,
        OrderManager,
        ProductManager,
        QuestionManager,
        RoleManager,
        ShippingManager,
        UserManager
    }

    private RolePermission()
    {
        
    }

    public RolePermission(Permissions permission)
    {
        Guard(permission);
        Permission = permission.ToString();
    }

    private void Guard(Permissions permission)
    {
        if (!Enum.TryParse(permission.ToString(), out permission))
            throw new InvalidAsynchronousStateException("Permission is invalid");
    }
}