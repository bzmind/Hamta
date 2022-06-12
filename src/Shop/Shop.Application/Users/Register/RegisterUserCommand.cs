using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Security;
using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;
using Shop.Domain.UserAggregate.Services;

namespace Shop.Application.Users.Register;

public record RegisterUserCommand(string PhoneNumber, string Password) : IBaseCommand;

public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Register(request.PhoneNumber, request.Password.ToSHA256(), _userDomainService);

        _userRepository.Add(user);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u => u.PhoneNumber)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("شماره موبایل"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("شماره موبایل"))
            .ValidPhoneNumber();

        RuleFor(u => u.Password)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("رمز عبور"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("رمز عبور"))
            .MinimumLength(8).WithMessage(ValidationMessages.FieldCharactersMinLength("رمز عبور", 7));
    }
}