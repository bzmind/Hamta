using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.Addresses.ActivateAddress;

public record ActivateUserAddressCommand(long UserId, long AddressId) : IBaseCommand;

public class ActivateUserAddressCommandHandler : IBaseCommandHandler<ActivateUserAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public ActivateUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(ActivateUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound();

        user.Addresses.ToList().ForEach(address =>
        {
            user.SetAddressActivation(address.Id, false);
        });

        user.SetAddressActivation(request.AddressId, true);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}