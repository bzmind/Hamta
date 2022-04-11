using Common.Application;
using Common.Application.Base_Classes;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.Use_Cases.SetAvatar;

public record SetCustomerAvatarCommand(long CustomerId, string AvatarName) : IBaseCommand;

public class SetCustomerAvatarCommandHandler : IBaseCommandHandler<SetCustomerAvatarCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public SetCustomerAvatarCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(SetCustomerAvatarCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsTrackingAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound();

        customer.SetAvatar(request.AvatarName);

        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class SetCustomerAvatarCommandValidator : AbstractValidator<SetCustomerAvatarCommand>
{
    public SetCustomerAvatarCommandValidator()
    {
        RuleFor(a => a.AvatarName)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldInvalid("نام آواتار"));
    }
}