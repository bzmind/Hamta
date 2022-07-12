using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Users;

public class SetUserAvatarViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long UserId { get; set; }

    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long AvatarId { get; set; }
}