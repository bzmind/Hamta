using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Shop.Domain.RoleAggregate;

namespace Shop.API.ViewModels.Roles;

public class CreateRoleViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public string Title { get; set; }

    [DisplayName("مجوز ها")]
    [ListNotEmpty(ErrorMessage = ValidationMessages.PermissionsRequired)]
    public List<RolePermission.Permissions> Permissions { get; set; }
}