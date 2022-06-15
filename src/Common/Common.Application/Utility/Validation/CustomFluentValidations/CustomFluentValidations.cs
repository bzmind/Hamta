using System.Text.RegularExpressions;
using FluentValidation;

namespace Common.Application.Utility.Validation.CustomFluentValidations;

public static class CustomFluentValidations
{
    public static IRuleBuilderOptionsConditions<T, string> ValidPhoneNumber<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((phoneNumber, context) =>
        {
            if (phoneNumber == null || string.IsNullOrWhiteSpace(phoneNumber))
                context.AddFailure(ValidationMessages.FieldRequired("شماره موبایل"));

            if (!Regex.IsMatch(phoneNumber, "09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}") ||
                phoneNumber.Length != 11)
                context.AddFailure(ValidationMessages.FieldInvalid("شماره موبایل"));
        });
    }

    public static IRuleBuilderOptionsConditions<T, string> ValidEmailOrPhoneNumber<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((emailOrPhone, context) =>
        {
            if (emailOrPhone == null || string.IsNullOrWhiteSpace(emailOrPhone))
                context.AddFailure(ValidationMessages.FieldRequired("ایمیل یا شماره موبایل"));

            if (!Regex.IsMatch(emailOrPhone, "09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}") ||
                emailOrPhone.Length != 11)
                context.AddFailure(ValidationMessages.FieldInvalid("شماره موبایل"));

            if (!Regex.IsMatch(emailOrPhone, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"))
            {
                context.AddFailure(ValidationMessages.FieldInvalid("ایمیل"));
            }
        });
    }
}