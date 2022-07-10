using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.AvatarAggregate.Repository;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.SetAvatar;

public record SetUserAvatarCommand(long UserId, long AvatarId) : IBaseCommand;

public class SetUserAvatarCommandHandler : IBaseCommandHandler<SetUserAvatarCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IAvatarRepository _avatarRepository;

    public SetUserAvatarCommandHandler(IAvatarRepository avatarRepository, IUserRepository userRepository)
    {
        _avatarRepository = avatarRepository;
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(SetUserAvatarCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);
        if (user == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        var avatar = await _avatarRepository.GetAsync(request.AvatarId);
        if (avatar == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("آواتار"));

        user.SetAvatar(avatar.Id);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}