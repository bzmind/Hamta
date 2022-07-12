using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Roles;

public class RemoveRoleViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long RoleId { get; set; }
}