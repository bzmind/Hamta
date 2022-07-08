using Common.Query.BaseClasses;
using Shop.Domain.AvatarAggregate;

namespace Shop.Query.Avatars._DTOs;

public class AvatarDto : BaseDto
{
    public string Name { get; set; }
    public Avatar.AvatarGender Gender { get; set; }
}