using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.Roles.RemoveRole;

public record RemoveUserRoleCommand(long UserId, long RoleId) : IBaseCommand;

public class RemoveUserRoleCommandHandler : IBaseCommandHandler<RemoveUserRoleCommand>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserRoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        user.RemoveRole(request.RoleId);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}