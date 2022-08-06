using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.AvatarAggregate.Repository;
using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;
using Shop.Domain.UserAggregate.Services;

namespace Shop.Application.Users.Edit;

public class EditUserCommand : IBaseCommand
{
    public long UserId { get; set; }
    public string FullName { get; set; }
    public User.UserGender Gender { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    private EditUserCommand()
    {

    }
}

public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;
    private readonly IAvatarRepository _avatarRepository;

    public EditUserCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService,
        IAvatarRepository avatarRepository)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
        _avatarRepository = avatarRepository;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        user.Edit(request.FullName, request.Gender, request.Email, request.PhoneNumber,
            _userDomainService);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(c => c.FullName)
            .NotNull().WithMessage(ValidationMessages.FullNameRequired)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired)
            .MaximumLength(30).WithMessage(ValidationMessages.FieldCharactersMaxLength("نام و نام خانوادگی", 30));
        
        RuleFor(r => r.Gender)
            .NotNull().WithMessage(ValidationMessages.GenderRequired)
            .IsInEnum().WithMessage(ValidationMessages.InvalidGender);

        RuleFor(c => c.Email)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("ایمیل"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("ایمیل"))
            .MaximumLength(250).WithMessage(ValidationMessages.FieldCharactersMaxLength("ایمیل", 250));

        RuleFor(c => c.PhoneNumber)
            .ValidPhoneNumber();
    }
}