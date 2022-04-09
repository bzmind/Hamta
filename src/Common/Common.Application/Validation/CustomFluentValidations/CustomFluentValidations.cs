using FluentValidation;

namespace Common.Application.Validation.CustomFluentValidations;

public static class CustomFluentValidations
{
    public static IRuleBuilderOptionsConditions<T, string> ValidPhoneNumber<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((phoneNumber, context) =>
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                context.AddFailure(ValidationMessages.FieldRequired("شماره تلفن"));

            if (phoneNumber.Length < 11)
                context.AddFailure(ValidationMessages.FieldMinLength("شماره تلفن", 11));

            if (phoneNumber.Length > 11)
                context.AddFailure(ValidationMessages.FieldMaxLength("شماره تلفن", 11));
        });
    }
}