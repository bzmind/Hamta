using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.AddAddress;

public record AddCustomerAddressCommand(long CustomerId, string FullName, string PhoneNumber,
    string Province, string City, string FullAddress, string PostalCode) : IBaseCommand;

public class AddCustomerAddressCommandHandler : IBaseCommandHandler<AddCustomerAddressCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public AddCustomerAddressCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(AddCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsTrackingAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound();

        customer.AddAddress(request.CustomerId, request.FullName, request.PhoneNumber, request.Province,
            request.City, request.FullAddress, request.PostalCode);

        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class AddCustomerAddressCommandValidator : AbstractValidator<AddCustomerAddressCommand>
{
    public AddCustomerAddressCommandValidator()
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