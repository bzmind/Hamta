using System.Collections;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Common.Api.Attributes;

public class ListMembersCharactersMinLengthAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly int _charactersMinLength;

    public ListMembersCharactersMinLengthAttribute(int charactersMinLength)
    {
        _charactersMinLength = charactersMinLength;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not ICollection list)
            return new ValidationResult(ValidationMessages.InvalidList);

        if (list.Count == 0)
            return ValidationResult.Success;

        foreach (var item in list)
        {
            var str = item.ToString();
            if (str == null)
                return new ValidationResult(ValidationMessages.InvalidList);

            if (str.Length < _charactersMinLength)
                return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-listMembersCharactersMinLength", _charactersMinLength.ToString());
        context.Attributes.Add("data-val-listMembersCharactersMinLength", ErrorMessage);
    }
}

public class ListMembersCharactersMaxLengthAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly int _charactersMaxLength;

    public ListMembersCharactersMaxLengthAttribute(int charactersMaxLength)
    {
        _charactersMaxLength = charactersMaxLength;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not ICollection list)
            return new ValidationResult(ValidationMessages.InvalidList);

        if (list.Count == 0)
            return ValidationResult.Success;

        foreach (var item in list)
        {
            var str = item.ToString();
            if (str == null)
                return new ValidationResult(ValidationMessages.InvalidList);

            if (str.Length > _charactersMaxLength)
                return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-listMembersCharactersMaxLength", _charactersMaxLength.ToString());
        context.Attributes.Add("data-val-listMembersCharactersMaxLength", ErrorMessage);
    }
}