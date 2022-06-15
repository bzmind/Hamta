using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.AddRole;

public record AddUserRoleCommand(long UserId, long RoleId) : IBaseCommand;

public class AddUserRoleCommandHandler : IBaseCommandHandler<AddUserRoleCommand>
{
    private readonly IUserRepository _userRepository;

    public AddUserRoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        user.AddRole(new UserRole(user.Id, request.RoleId));

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}