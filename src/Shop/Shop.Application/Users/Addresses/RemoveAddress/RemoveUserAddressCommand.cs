using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.Addresses.RemoveAddress;

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
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        var address = user.Addresses.FirstOrDefault(a => a.IsActive);
        if (address != null && address.Id == request.AddressId)
        {
            var addresses = user.Addresses.OrderByDescending(a => a.Id);
            addresses.First(a => a.Id != request.AddressId).SetAddressActivation(true);
        }

        user.RemoveAddress(request.AddressId);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}