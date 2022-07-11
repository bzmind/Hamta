using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Security;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.UserAggregate.Repository;

namespace Shop.Application.Users.Auth.ResetPassword;

public record ResetUserPasswordCommand(long UserId, string CurrentPassword, string NewPassword) : IBaseCommand;

public class ResetUserPasswordCommandHandler : IBaseCommandHandler<ResetUserPasswordCommand>
{
    private readonly IUserRepository _userRepository;

    public ResetUserPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsTrackingAsync(request.UserId);

        if (user == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        var isSamePassword = SHA256Hash.Compare(user.Password, request.CurrentPassword);

        if (!isSamePassword)
            return OperationResult.Error(ValidationMessages.InvalidCurrentPassword);

        user.ResetPassword(request.NewPassword.ToSHA256());

        await _userRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class ResetUserPasswordCommandValidator : AbstractValidator<ResetUserPasswordCommand>
{
    public ResetUserPasswordCommandValidator()
    {
        RuleFor(r => r.CurrentPassword)
            .NotNull().WithMessage(ValidationMessages.CurrentPasswordRequired)
            .NotEmpty().WithMessage(ValidationMessages.CurrentPasswordRequired)
            .MinimumLength(8).WithMessage(ValidationMessages.FieldCharactersMinLength("رمز عبور فعلی", 7));

        RuleFor(r => r.NewPassword)
            .NotNull().WithMessage(ValidationMessages.PasswordRequired)
            .NotEmpty().WithMessage(ValidationMessages.PasswordRequired)
            .MinimumLength(8).WithMessage(ValidationMessages.FieldCharactersMinLength("رمز عبور", 7));
    }
}