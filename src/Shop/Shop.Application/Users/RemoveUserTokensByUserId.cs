using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users;

public record RemoveUserTokensByUserId(long UserId) : IBaseCommand;

public class RemoveUserTokensByUserIdHandler : IBaseCommandHandler<RemoveUserTokensByUserId>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserTokensByUserIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(RemoveUserTokensByUserId request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        user.RemoveAllTokens();

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}