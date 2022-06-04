using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.RemoveAddress;

public record RemoveUserAddressCommand(long UserId, long AddressId) : IBaseCommand;

public class RemoveUserAddressCommandHandler : IBaseCommandHandler<RemoveUserAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(RemoveUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound();

        user.RemoveAddress(request.AddressId);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}