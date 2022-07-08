using Common.Domain.Repository;
using Shop.Domain.UserAggregate;

namespace Shop.Domain.AvatarAggregate.Repository;

public interface IAvatarRepository : IBaseRepository<Avatar>
{
    Task<Avatar> GetRandomAvatarNameByUserGender(User.UserGender gender);
    Task<bool> RemoveAvatar(long avatarId);
}