using System.ComponentModel.DataAnnotations;

namespace Common.Api.Attributes;

public class UriAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var valid = Uri.TryCreate(Convert.ToString(value), UriKind.Absolute, out var uri);

        if (!valid)
        {
            return new ValidationResult(ErrorMessage);
        }
        return ValidationResult.Success;
    }
}