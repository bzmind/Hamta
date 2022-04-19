using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.EditAddress;

public record EditCustomerAddressCommand(long CustomerId, long AddressId, string FullName, string PhoneNumber,
    string Province, string City, string FullAddress, string PostalCode) : IBaseCommand;

public class EditCustomerAddressCommandHandler : IBaseCommandHandler<EditCustomerAddressCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public EditCustomerAddressCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(EditCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsTrackingAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound();

        customer.EditAddress(request.AddressId, request.FullName, request.PhoneNumber, request.Province,
            request.City, request.FullAddress, request.PostalCode);

        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class EditCustomerAddressCommandValidator : AbstractValidator<EditCustomerAddressCommand>
{
    public EditCustomerAddressCommandValidator()
    {
        RuleFor(a => a.FullName)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام و نام خانوادگی"));

        RuleFor(a => a.PhoneNumber).ValidPhoneNumber();

        RuleFor(a => a.Province)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("استان"));

        RuleFor(a => a.City)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("شهر"));

        RuleFor(a => a.FullAddress)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("آدرس کامل"));

        RuleFor(a => a.PostalCode)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("کد پستی"));
    }
}