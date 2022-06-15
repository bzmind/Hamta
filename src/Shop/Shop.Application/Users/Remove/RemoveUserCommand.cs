using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.Remove;

public record RemoveUserCommand(long UserId) : IBaseCommand;

public class RemoveUserCommandHandler : IBaseCommandHandler<RemoveUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IOrderRepository _orderRepository;

    public RemoveUserCommandHandler(IUserRepository userRepository, IOrderRepository orderRepository)
    {
        _userRepository = userRepository;
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        var userOrder = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);

        if (userOrder != null && userOrder.Status != Order.OrderStatus.Pending.ToString())
        {
            return OperationResult
                .Error("امکان حذف حساب کاربری وجود ندارد، زیرا کاربر دارای سفارشِ در حال پردازش است");
        }

        _userRepository.Delete(user);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}