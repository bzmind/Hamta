using Common.Application.Utility;
using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Users._DTOs;

namespace Shop.Query.Users.SearchByEmailOrPhone;

public record SearchUserByEmailOrPhoneQuery(string EmailOrPhone) : IBaseQuery<LoginNextStep>;

public class SearchUserByEmailOrPhoneQueryHandler : IBaseQueryHandler<SearchUserByEmailOrPhoneQuery, LoginNextStep>
{
    private readonly ShopContext _shopContext;

    public SearchUserByEmailOrPhoneQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<LoginNextStep> Handle(SearchUserByEmailOrPhoneQuery request, CancellationToken cancellationToken)
    {
        var userExists = await _shopContext.Users
            .AnyAsync(c => c.PhoneNumber.Value == request.EmailOrPhone
                                      || c.Email == request.EmailOrPhone, cancellationToken);

        if (userExists)
            return new LoginNextStep
            {
                UserExists = true,
                NextStep = LoginNextStep.NextSteps.Password
            };

        if (request.EmailOrPhone.IsPhone())
            return new LoginNextStep
            {
                UserExists = false,
                NextStep = LoginNextStep.NextSteps.Register
            };

        return new LoginNextStep
        {
            UserExists = false,
            NextStep = LoginNextStep.NextSteps.RegisterWithPhone
        };
    }
}