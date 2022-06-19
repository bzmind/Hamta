using Common.Application;
using Common.Application.Utility;
using Common.Application.Utility.Validation;
using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Users._DTOs;

namespace Shop.Query.Users.SearchByEmailOrPhone;

public record SearchUserByEmailOrPhoneQuery(string EmailOrPhone) : IBaseQuery<OperationResult<LoginResult>>;

public class SearchUserByEmailOrPhoneQueryHandler : IBaseQueryHandler<SearchUserByEmailOrPhoneQuery, OperationResult<LoginResult>>
{
    private readonly ShopContext _shopContext;

    public SearchUserByEmailOrPhoneQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<OperationResult<LoginResult>> Handle(SearchUserByEmailOrPhoneQuery request, CancellationToken cancellationToken)
    {
        var userExists = await _shopContext.Users
            .AnyAsync(c => c.PhoneNumber.Value == request.EmailOrPhone
                                      || c.Email == request.EmailOrPhone, cancellationToken);

        if (userExists)
            return OperationResult<LoginResult>.Success(new LoginResult
            {
                UserExists = true,
                NextStep = LoginResult.NextSteps.Password
            });

        if (request.EmailOrPhone.IsPhone())
            return OperationResult<LoginResult>.Success(new LoginResult
            {
                UserExists = false,
                NextStep = LoginResult.NextSteps.Register
            });

        if (request.EmailOrPhone.IsEmail())
            return OperationResult<LoginResult>.Error(new LoginResult
            {
                UserExists = false,
                NextStep = LoginResult.NextSteps.RegisterWithPhone
            }, "حساب کاربری با مشخصات وارد شده وجود ندارد. " +
               "لطفا از شماره تلفن همراه برای ساخت حساب کاربری استفاده نمایید.");

        return OperationResult<LoginResult>.Error(null, ValidationMessages.InvalidEmailOrPhone);
    }
}