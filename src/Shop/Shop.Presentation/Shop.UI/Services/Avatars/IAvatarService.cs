using Common.Api;
using Shop.API.ViewModels.Avatars;
using Shop.Domain.AvatarAggregate;
using Shop.Query.Avatars._DTOs;

namespace Shop.UI.Services.Avatars;

public interface IAvatarService
{
    Task<ApiResult> Create(CreateAvatarViewModel model);
    Task<ApiResult> Remove(long avatarId);

    Task<AvatarDto?> GetById(long avatarId);
    Task<AvatarDto?> GetByGender(Avatar.AvatarGender gender);
    Task<List<AvatarDto>> GetAll();
}