using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Common.Application.Validation;

namespace Common.Api.Validation;

public class EmailOrPhoneAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return new ValidationResult(ValidationMessages.FieldRequired("ایمیل یا شماره موبایل"));

        if (Regex.IsMatch(value.ToString(), "09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}") &&
            value.ToString().Length == 11)
        {
            return ValidationResult.Success;
        }

        if (Regex.IsMatch(value.ToString(), @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ValidationMessages.FieldInvalid("ایمیل یا شماره موبایل"));
    }
}

public class IranPhoneNumber : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return new ValidationResult(ValidationMessages.FieldRequired("شماره موبایل"));

        if (Regex.IsMatch(value.ToString(), "09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}") &&
            value.ToString().Length == 11)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ValidationMessages.FieldInvalid("شماره موبایل"));
    }
}