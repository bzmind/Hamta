using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using Common.Query.BaseClasses;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Users._DTOs;
using Shop.Query.Users._Mappers;

namespace Shop.Query.Users.GetByEmailOrPhone;

public record GetUserByEmailOrPhoneQuery(string EmailOrPhoneNumber) : IBaseQuery<UserDto?>;

public class GetUserByEmailOrPhoneQueryHandler : IBaseQueryHandler<GetUserByEmailOrPhoneQuery, UserDto?>
{
    private readonly ShopContext _shopContext;

    public GetUserByEmailOrPhoneQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<UserDto?> Handle(GetUserByEmailOrPhoneQuery request, CancellationToken cancellationToken)
    {
        var user = await _shopContext.Users
            .FirstOrDefaultAsync(c => c.PhoneNumber.Value == request.EmailOrPhoneNumber
                                      || c.Email == request.EmailOrPhoneNumber, cancellationToken);

        return user.MapToUserDto();
    }
}

public class GetUserByEmailOrPhoneQueryValidator : AbstractValidator<GetUserByEmailOrPhoneQuery>
{
    public GetUserByEmailOrPhoneQueryValidator()
    {
        RuleFor(u => u.EmailOrPhoneNumber)
            .NotNull().WithMessage(ValidationMessages.PhoneNumberRequired)
            .NotEmpty().WithMessage(ValidationMessages.PhoneNumberRequired)
            .ValidEmailOrPhoneNumber();
    }
}