using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomFluentValidations;
using Common.Domain.ValueObjects;
using FluentValidation;
using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.Addresses.CreateAddress;

public class CreateUserAddressCommand : IBaseCommand
{
    public long UserId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Province { get; set; }
    public string City { get; set; }
    public string FullAddress { get; set; }
    public string PostalCode { get; set; }

    private CreateUserAddressCommand()
    {
        
    }
}

public class CreateUserAddressCommandHandler : IBaseCommandHandler<CreateUserAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound();

        var newAddress = new UserAddress(request.UserId, request.FullName, new PhoneNumber(request.PhoneNumber),
            request.Province, request.City, request.FullAddress, request.PostalCode);

        if (user.Addresses.ToList().Count == 0 || !user.Addresses.Any(a => a.IsActive))
            newAddress.SetAddressActivation(true);

        user.AddAddress(newAddress);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class CreateUserAddressCommandValidator : AbstractValidator<CreateUserAddressCommand>
{
    public CreateUserAddressCommandValidator()
    {
        RuleFor(a => a.FullName)
            .NotNull().WithMessage(ValidationMessages.FullNameRequired)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired)
            .MaximumLength(30).WithMessage(ValidationMessages.FieldCharactersMaxLength("نام و نام خانوادگی", 30));

        RuleFor(a => a.PhoneNumber)
            .ValidPhoneNumber();

        RuleFor(a => a.Province)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("استان"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("استان"))
            .MaximumLength(30).WithMessage(ValidationMessages.FieldCharactersMaxLength("استان", 30));

        RuleFor(a => a.City)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("شهر"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("شهر"))
            .MaximumLength(30).WithMessage(ValidationMessages.FieldCharactersMaxLength("شهر", 30));

        RuleFor(a => a.FullAddress)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("آدرس کامل"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("آدرس کامل"))
            .MaximumLength(300).WithMessage(ValidationMessages.FieldCharactersMaxLength("آدرس کامل", 300));

        RuleFor(a => a.PostalCode)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("کد پستی"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("کد پستی"))
            .Length(10).WithMessage(ValidationMessages.FieldCharactersStaticLength("کد پستی", 10));
    }
}