using System.Collections;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Common.Api.Attributes;

public class ListMinLengthAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly int _minLength;

    public ListMinLengthAttribute(int minLength)
    {
        _minLength = minLength;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not ICollection list)
            return new ValidationResult(ValidationMessages.InvalidList);

        var length = list.Count;
        if (length < _minLength)
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-listMinLength", _minLength.ToString());
        context.Attributes.Add("data-val-listMinLength", ErrorMessage);
    }
}

public class ListMaxLengthAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly int _maxLength;

    public ListMaxLengthAttribute(int maxLength)
    {
        _maxLength = maxLength;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not ICollection list)
            return new ValidationResult(ValidationMessages.InvalidList);

        var length = list.Count;
        if (length > _maxLength)
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-listMaxLength", _maxLength.ToString());
        context.Attributes.Add("data-val-listMaxLength", ErrorMessage);
    }
}