using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Security;
using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.CustomerAggregate;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.Create;

public record CreateCustomerCommand(string FullName, string Email, string Password,
    string PhoneNumber) : IBaseCommand;

public class CreateCustomerCommandHandler : IBaseCommandHandler<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.FullName, request.Email, request.Password.ToSHA256(),
            request.PhoneNumber);

        _customerRepository.Add(customer);
        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(c => c.FullName)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام و نام خانوادگی"));

        RuleFor(c => c.Email)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("ایمیل"));

        RuleFor(c => c.Password)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("رمز عبور"));

        RuleFor(c => c.PhoneNumber).ValidPhoneNumber();
    }
}