using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.SetStatus;

public record SetOrderStatusCommand(long UserId, string OrderStatus) : IBaseCommand;

public class SetOrderStatusCommandHandler : IBaseCommandHandler<SetOrderStatusCommand>
{
    private readonly IOrderRepository _orderRepository;

    public SetOrderStatusCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(SetOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound();

        if (Enum.TryParse(request.OrderStatus, out Order.OrderStatus status))
            return OperationResult.Error("وضعیت سفارش نامعتبر است");

        order.SetStatus(status);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class SetOrderStatusCommandValidator : AbstractValidator<SetOrderStatusCommand>
{
    public SetOrderStatusCommandValidator()
    {
        RuleFor(o => o.OrderStatus)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("وضعیت سفارش"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("وضعیت سفارش"));
    }
}