using Common.Application.Utility.Validation.CustomAttributes;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Common.Application.Utility.Validation.CustomFluentValidations;

public static class CustomFluentValidations
{
    public static IRuleBuilderOptionsConditions<T, string> ValidPhoneNumber<T>
        (this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((phoneNumber, context) =>
        {
            if (phoneNumber == null || string.IsNullOrWhiteSpace(phoneNumber))
                context.AddFailure(ValidationMessages.InvalidPhoneNumber);

            if (!phoneNumber.IsIranPhone())
                context.AddFailure(ValidationMessages.InvalidPhoneNumber);
        });
    }

    public static IRuleBuilderOptionsConditions<T, string> ValidEmailOrPhoneNumber<T>
        (this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((emailOrPhone, context) =>
        {
            if (emailOrPhone == null || string.IsNullOrWhiteSpace(emailOrPhone))
                context.AddFailure(ValidationMessages.EmailOrPhoneRequired);

            if (!emailOrPhone.IsIranPhone() || !emailOrPhone.IsEmail())
                context.AddFailure(ValidationMessages.InvalidEmailOrPhone);
        });
    }

    public static IRuleBuilderOptionsConditions<T, TProperty> JustImageFile<T, TProperty>
        (this IRuleBuilder<T, TProperty> ruleBuilder,
            string errorMessage = ValidationMessages.InvalidImage)
        where TProperty : IFormFile?
    {
        return ruleBuilder.Custom((file, context) =>
        {
            if (file == null)
                return;

            if (!file.IsImage())
                context.AddFailure(errorMessage);
        });
    }
}