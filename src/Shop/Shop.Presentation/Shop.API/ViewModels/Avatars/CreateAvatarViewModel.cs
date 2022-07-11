using Shop.Domain.AvatarAggregate;

namespace Shop.API.ViewModels.Avatars;

public class CreateAvatarViewModel
{
    public IFormFile AvatarFile { get; set; }
    public Avatar.AvatarGender Gender { get; set; }
}