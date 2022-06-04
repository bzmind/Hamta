using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Security;
using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;
using Shop.Domain.UserAggregate.Services;

namespace Shop.Application.Users.Create;

public record CreateUserCommand(string FullName, string Email, string Password,
    string PhoneNumber) : IBaseCommand<long>;

public class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand, long>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;

    public CreateUserCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
    }

    public async Task<OperationResult<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.FullName, request.Email, request.Password.ToSHA256(),
            request.PhoneNumber, _userDomainService);

        _userRepository.Add(user);
        await _userRepository.SaveAsync();
        return OperationResult<long>.Success(user.Id);
    }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.FullName)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("نام و نام خانوادگی"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام و نام خانوادگی"));

        RuleFor(c => c.Email)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("ایمیل"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("ایمیل"))
            .EmailAddress().WithMessage(ValidationMessages.FieldInvalid("ایمیل"));

        RuleFor(c => c.Password)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("رمز عبور"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("رمز عبور"))
            .MinimumLength(8).WithMessage(ValidationMessages.FieldCharactersMinLength("رمز عبور", 7));

        RuleFor(c => c.PhoneNumber)
            .ValidPhoneNumber();
    }
}