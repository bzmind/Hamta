using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.EditAddress;

public record EditUserAddressCommand(long UserId, long AddressId, string FullName, string PhoneNumber,
    string Province, string City, string FullAddress, string PostalCode) : IBaseCommand;

public class EditUserAddressCommandHandler : IBaseCommandHandler<EditUserAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public EditUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(EditUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound();

        user.EditAddress(request.AddressId, request.FullName, request.PhoneNumber, request.Province,
            request.City, request.FullAddress, request.PostalCode);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditUserAddressCommandValidator : AbstractValidator<EditUserAddressCommand>
{
    public EditUserAddressCommandValidator()
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