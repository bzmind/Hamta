using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;
using Shop.Domain.UserAggregate.Services;

namespace Shop.Application.Users.Edit;

public record EditUserCommand(long UserId, string FullName, User.UserGender Gender, string Email,
    string PhoneNumber) : IBaseCommand;

public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomainService _userDomainService;

    public EditUserCommandHandler(IUserRepository userRepository, IUserDomainService userDomainService)
    {
        _userRepository = userRepository;
        _userDomainService = userDomainService;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound();

        user.Edit(request.FullName, request.Gender, request.Email, request.PhoneNumber, _userDomainService);

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(c => c.FullName)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("نام و نام خانوادگی"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام و نام خانوادگی"));

        RuleFor(c => c.Email)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("ایمیل"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("ایمیل"));

        RuleFor(c => c.PhoneNumber)
            .ValidPhoneNumber();
    }
}