using System.ComponentModel;
using Common.Domain.BaseClasses;

namespace Shop.Domain.RoleAggregate;

public class RolePermission : BaseEntity
{
    public Permissions Permission { get; private set; }

    public enum Permissions
    {
        AvatarManager,
        BannerManager,
        SliderManager,
        CategoryManager,
        ColorManager,
        CommentManager,
        SellerManager,
        OrderManager,
        ProductManager,
        QuestionManager,
        RoleManager,
        ShippingManager,
        UserAddressManager,
        UserManager
    }

    private RolePermission()
    {
        
    }

    public RolePermission(Permissions permission)
    {
        Guard(permission);
        Permission = permission;
    }

    private void Guard(Permissions permission)
    {
        if (!Enum.TryParse(permission.ToString(), out permission))
            throw new InvalidAsynchronousStateException("Permission is invalid");
    }
}