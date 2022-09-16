using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomAttributes;
using Shop.Domain.AvatarAggregate;

namespace Shop.API.ViewModels.Avatars;

public class CreateAvatarViewModel
{
    [DisplayName("آواتار")]
    [Required(ErrorMessage = ValidationMessages.AvatarRequired)]
    [ImageFile(ErrorMessage = ValidationMessages.InvalidAvatar)]
    public IFormFile AvatarFile { get; set; }

    [Display(Name = "جنسیت")]
    [Required(ErrorMessage = ValidationMessages.ChooseGender)]
    public Avatar.AvatarGender Gender { get; set; }
}