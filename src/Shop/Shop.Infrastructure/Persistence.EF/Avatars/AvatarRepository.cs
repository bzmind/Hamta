using Common.Application.Utility.Mappers;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.AvatarAggregate;
using Shop.Domain.AvatarAggregate.Repository;
using Shop.Domain.UserAggregate;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Avatars;

public class AvatarRepository : BaseRepository<Avatar>, IAvatarRepository
{
    public AvatarRepository(ShopContext context) : base(context)
    {
    }

    public async Task<Avatar> GetRandomAvatarNameByUserGender(User.UserGender gender)
    {
        var avatars = await Context.Avatars.ToListAsync();
        return avatars.Where(a => a.Gender == gender.MapUserGenderToAvatarGender())
            .OrderBy(_ => new Random().Next())
            .First();
    }
}