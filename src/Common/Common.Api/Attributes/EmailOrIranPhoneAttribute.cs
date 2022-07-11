using Common.Application.Utility;
using Common.Application.Utility.Validation;
using System.ComponentModel.DataAnnotations;

namespace Common.Api.Attributes;

public class EmailOrIranPhoneAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return new ValidationResult(ValidationMessages.EmailOrPhoneRequired);

        if (value.ToString()!.IsIranPhone() || value.ToString()!.IsEmail())
            return ValidationResult.Success;

        return new ValidationResult(ValidationMessages.InvalidEmailOrPhone);
    }
}

public class IranPhone : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return new ValidationResult(ValidationMessages.PhoneNumberRequired);

        if (value.ToString()!.IsIranPhone())
            return ValidationResult.Success;

        return new ValidationResult(ValidationMessages.InvalidPhoneNumber);
    }
}