using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Avatars;

public class RemoveAvatarViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long AvatarId { get; set; }
}