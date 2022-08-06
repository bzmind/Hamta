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

namespace Shop.Application.Users.Create;

public record CreateUserCommand(string FullName, User.UserGender Gender, string PhoneNumber,
    string Password) : IBaseCommand<long>;

public class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand, long>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;
    private readonly IAvatarRepository _avatarRepository;

    public CreateUserCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService,
        IAvatarRepository avatarRepository)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
        _avatarRepository = avatarRepository;
    }

    public async Task<OperationResult<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var avatar = await _avatarRepository.GetRandomAvatarNameByUserGender(request.Gender);

        var user = new User(request.FullName, request.Gender, request.PhoneNumber, request.Password.ToSHA256(),
            avatar.Id, _userDomainService);

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
            .NotNull().WithMessage(ValidationMessages.FullNameRequired)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired)
            .MaximumLength(30).WithMessage(ValidationMessages.FieldCharactersMaxLength("نام و نام خانوادگی", 30));

        RuleFor(c => c.Gender)
            .NotNull().WithMessage(ValidationMessages.GenderRequired)
            .IsInEnum().WithMessage(ValidationMessages.InvalidGender);

        RuleFor(c => c.PhoneNumber)
            .ValidPhoneNumber();

        RuleFor(c => c.Password)
            .NotNull().WithMessage(ValidationMessages.PasswordRequired)
            .NotEmpty().WithMessage(ValidationMessages.PasswordRequired)
            .MinimumLength(8).WithMessage(ValidationMessages.FieldCharactersMinLength("رمز عبور", 7));
    }
}