using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Shop.Domain.RoleAggregate;

namespace Shop.API.ViewModels.Roles;

public class RemoveRolePermissionViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long RoleId { get; set; }

    [DisplayName("مجوز ها")]
    [ListNotEmpty(ErrorMessage = ValidationMessages.PermissionsRequired)]
    public List<RolePermission.Permissions> Permissions { get; set; }
}