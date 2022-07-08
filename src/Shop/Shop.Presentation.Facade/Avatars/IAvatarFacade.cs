using Common.Application;
using Shop.Application.Avatars.Create;
using Shop.Domain.AvatarAggregate;
using Shop.Query.Avatars._DTOs;

namespace Shop.Presentation.Facade.Avatars;

public interface IAvatarFacade
{
    Task<OperationResult<long>> Create(CreateAvatarCommand command);
    Task<OperationResult> Remove(long avatarId);

    Task<AvatarDto?> GetById(long avatarId);
    Task<AvatarDto?> GetByGender(Avatar.AvatarGender gender);
    Task<List<AvatarDto>> GetAll();
}