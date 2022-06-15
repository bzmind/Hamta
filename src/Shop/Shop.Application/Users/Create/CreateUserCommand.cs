using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Security;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomFluentValidations;
using FluentValidation;
using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;
using Shop.Domain.UserAggregate.Services;

namespace Shop.Application.Users.Create;

public record CreateUserCommand(string PhoneNumber, string Password) : IBaseCommand<long>;

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
        var user = new User(request.PhoneNumber, request.Password.ToSHA256(), request.PhoneNumber,
            _userDomainService);

        _userRepository.Add(user);

        await _userRepository.SaveAsync();
        return OperationResult<long>.Success(user.Id);
    }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.PhoneNumber)
            .ValidPhoneNumber();

        RuleFor(c => c.Password)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("رمز عبور"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("رمز عبور"))
            .MinimumLength(8).WithMessage(ValidationMessages.FieldCharactersMinLength("رمز عبور", 7));
    }
}