using System.Text.RegularExpressions;
using Common.Application;
using Common.Application.Validation;
using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;

namespace Shop.Query.Users.SearchByEmailOrPhone;

public record SearchUserByEmailOrPhoneQuery(string EmailOrPhone) : IBaseQuery<OperationResult>;

public class SearchUserByEmailOrPhoneQueryHandler : IBaseQueryHandler<SearchUserByEmailOrPhoneQuery, OperationResult>
{
    private readonly ShopContext _shopContext;

    public SearchUserByEmailOrPhoneQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<OperationResult> Handle(SearchUserByEmailOrPhoneQuery request, CancellationToken cancellationToken)
    {
        var user = await _shopContext.Users
            .FirstOrDefaultAsync(c => c.PhoneNumber.Value == request.EmailOrPhone
                                      || c.Email == request.EmailOrPhone, cancellationToken);

        if (user != null)
            return OperationResult.Success();

        if (IsThisEmailOrPhone(request.EmailOrPhone) == "phone")
            return OperationResult.NotFound("User was not be found by phone");

        if (IsThisEmailOrPhone(request.EmailOrPhone) == "email")
            return OperationResult.NotFound("User was not be found by email");

        return OperationResult.Error(ValidationMessages.FieldInvalid("شماره موبایل یا ایمیل"));
    }

    private string IsThisEmailOrPhone(string input)
    {
        if (Regex.IsMatch(input, "09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}") && input.Length == 11)
            return "phone";

        if (Regex.IsMatch(input, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"))
            return "email";

        return "none";
    }
}