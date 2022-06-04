using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.CreateAddress;

public class CreateUserAddressCommand : IBaseCommand
{
    public long UserId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Province { get; set; }
    public string City { get; set; }
    public string FullAddress { get; set; }
    public string PostalCode { get; set; }

    public CreateUserAddressCommand(long userId, string fullName, string phoneNumber,
        string province, string city, string fullAddress, string postalCode)
    {
        UserId = userId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Province = province;
        City = city;
        FullAddress = fullAddress;
        PostalCode = postalCode;
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

        user.AddAddress(request.UserId, request.FullName, request.PhoneNumber, request.Province,
            request.City, request.FullAddress, request.PostalCode);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class CreateUserAddressCommandValidator : AbstractValidator<CreateUserAddressCommand>
{
    public CreateUserAddressCommandValidator()
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