using Common.Application.Utility.Mappers;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.AvatarAggregate;
using Shop.Domain.AvatarAggregate.Repository;
using Shop.Domain.UserAggregate;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Avatars;

public class AvatarRepository : BaseRepository<Avatar>, IAvatarRepository
{
    public AvatarRepository(ShopContext shopContext) : base(shopContext)
    {
    }

    public async Task<Avatar> GetRandomAvatarNameByUserGender(User.UserGender gender)
    {
        var avatars = await ShopContext.Avatars.ToListAsync();
        return avatars.Where(a => a.Gender == gender.MapUserGenderToAvatarGender())
            .OrderBy(_ => new Random().Next())
            .First();
    }

    public bool RemoveAvatar(Avatar avatar)
    {
        ShopContext.Avatars.Remove(avatar);
        return true;
    }
}