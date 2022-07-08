using Shop.Domain.AvatarAggregate;
using Shop.Query.Avatars._DTOs;

namespace Shop.Query.Avatars._Mappers;

internal static class AvatarMapper
{
    public static AvatarDto MapToAvatarDto(this Avatar? avatar)
    {
        if (avatar == null)
            return null;

        return new AvatarDto
        {
            Id = avatar.Id,
            CreationDate = avatar.CreationDate,
            Name = avatar.Name,
            Gender = avatar.Gender
        };
    }

    public static List<AvatarDto> MapToAvatarDto(this List<Avatar> avatars)
    {
        var dtoAvatars = new List<AvatarDto>();

        avatars.ForEach(avatar =>
        {
            dtoAvatars.Add(new AvatarDto
            {
                Id = avatar.Id,
                CreationDate = avatar.CreationDate,
                Name = avatar.Name,
                Gender = avatar.Gender
            });
        });

        return dtoAvatars;
    }
}