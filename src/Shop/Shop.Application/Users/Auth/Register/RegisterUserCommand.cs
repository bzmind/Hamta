using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Security;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.AvatarAggregate.Repository;
using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;
using Shop.Domain.UserAggregate.Services;

namespace Shop.Application.Users.Auth.Register;

public record RegisterUserCommand(string FullName, User.UserGender Gender, string PhoneNumber,
    string Password) : IBaseCommand;

public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;
    private readonly IAvatarRepository _avatarRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService,
        IAvatarRepository avatarRepository)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
        _avatarRepository = avatarRepository;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var avatar = await _avatarRepository.GetRandomAvatarNameByUserGender(request.Gender);

        var user = User.Register(request.FullName, request.Gender, request.PhoneNumber, request.Password.ToSHA256(),
            avatar.Id, _userDomainService);

        _userRepository.Add(user);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u => u.FullName)
            .NotNull().WithMessage(ValidationMessages.FullNameRequired)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired);

        RuleFor(u => u.Gender)
            .NotNull().WithMessage(ValidationMessages.ChooseGender)
            .IsInEnum().WithMessage(ValidationMessages.InvalidGender);

        RuleFor(u => u.PhoneNumber)
            .NotNull().WithMessage(ValidationMessages.PhoneNumberRequired)
            .NotEmpty().WithMessage(ValidationMessages.PhoneNumberRequired)
            .ValidPhoneNumber();

        RuleFor(u => u.Password)
            .NotNull().WithMessage(ValidationMessages.PasswordRequired)
            .NotEmpty().WithMessage(ValidationMessages.PasswordRequired)
            .MinimumLength(8).WithMessage(ValidationMessages.FieldCharactersMinLength("رمز عبور", 7));
    }
}