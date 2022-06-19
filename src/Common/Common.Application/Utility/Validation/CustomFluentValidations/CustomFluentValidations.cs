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
                context.AddFailure(ValidationMessages.InvalidPhoneNumber);

            if (!phoneNumber.IsPhone())
                context.AddFailure(ValidationMessages.InvalidPhoneNumber);
        });
    }

    public static IRuleBuilderOptionsConditions<T, string> ValidEmailOrPhoneNumber<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((emailOrPhone, context) =>
        {
            if (emailOrPhone == null || string.IsNullOrWhiteSpace(emailOrPhone))
                context.AddFailure(ValidationMessages.EmailOrPhoneRequired);

            if (!emailOrPhone.IsPhone() || !emailOrPhone.IsEmail())
                context.AddFailure(ValidationMessages.InvalidEmailOrPhone);
        });
    }
}