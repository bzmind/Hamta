using Shop.Domain.AvatarAggregate;
using Shop.Domain.UserAggregate;

namespace Common.Application.Utility.Mappers;

public static class EnumMappers
{
    public static Avatar.AvatarGender MapUserGenderToAvatarGender(this User.UserGender userGender)
    {
        return (Avatar.AvatarGender)Enum.Parse(typeof(Avatar.AvatarGender), userGender.ToString());
    }
}