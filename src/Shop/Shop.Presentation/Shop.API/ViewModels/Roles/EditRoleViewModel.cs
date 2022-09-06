using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Shop.Domain.RoleAggregate;

namespace Shop.API.ViewModels.Roles;

public class EditRoleViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseRole)]
    public long Id { get; set; }

    [DisplayName("عنوان")]
    [Required(ErrorMessage = ValidationMessages.TitleRequired)]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxCharactersLength)]
    public string Title { get; set; }

    [DisplayName("مجوز ها")]
    [Required(ErrorMessage = ValidationMessages.PermissionsRequired)]
    [ListNotEmpty(ErrorMessage = ValidationMessages.PermissionsRequired)]
    public List<RolePermission.Permissions> Permissions { get; set; }
}