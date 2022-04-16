using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.UseCases.SetStatus;

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

        Order.OrderStatus status;
        
        if (Enum.TryParse(request.OrderStatus, out status))
            return OperationResult.Error("وضعیت سفارش نامعتبر است");

        order.SetStatus(status);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class SetOrderStatusCommandValidator : AbstractValidator<SetOrderStatusCommand>
{
    public SetOrderStatusCommandValidator()
    {
        RuleFor(o => o.OrderStatus)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("وضعیت سفارش"));
    }
}