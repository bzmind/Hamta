using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Security;
using Common.Application.Utility.Validation;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.AddToken;

public record AddUserTokenCommand(long UserId, string JwtToken, string RefreshToken,
    DateTime JwtTokenExpireDate, DateTime RefreshTokenExpireDate, string Device) : IBaseCommand;

public class AddUserTokenCommandHandler : IBaseCommandHandler<AddUserTokenCommand>
{
    private readonly IUserRepository _userRepository;

    public AddUserTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(AddUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        user.AddToken(request.JwtToken.ToSHA256(), request.RefreshToken.ToSHA256(), request.JwtTokenExpireDate,
            request.RefreshTokenExpireDate, request.Device);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}