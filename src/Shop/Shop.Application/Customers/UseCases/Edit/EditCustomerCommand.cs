using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.UseCases.Edit;

public record EditCustomerCommand(long CustomerId, string FullName, string Email, string PhoneNumber)
    : IBaseCommand;

public class EditCustomerCommandHandler : IBaseCommandHandler<EditCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public EditCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsTrackingAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound();

        customer.Edit(request.FullName, request.Email, request.PhoneNumber);

        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class EditCustomerCommandValidator : AbstractValidator<EditCustomerCommand>
{
    public EditCustomerCommandValidator()
    {
        RuleFor(c => c.FullName)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام و نام خانوادگی"));

        RuleFor(c => c.Email)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("ایمیل"));
        
        RuleFor(c => c.PhoneNumber).ValidPhoneNumber();
    }
}