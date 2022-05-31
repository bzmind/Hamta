using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.Remove;

public record RemoveUserCommand(long UserId) : IBaseCommand;

public class RemoveUserCommandHandler : IBaseCommandHandler<RemoveUserCommand>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        _userRepository.Delete(user);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}