﻿using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Common.Api.Attributes;

public class EnumNotNullOrZeroAttribute : ValidationAttribute, IClientModelValidator
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || (int)value == 0)
            return new ValidationResult(ValidationMessages.InvalidGender);

        var enumMembers = value.GetType().GetEnumNames();

        var valueExistInEnum = enumMembers.ToList().Any(m => m == value.ToString());
        if (valueExistInEnum)
            return ValidationResult.Success;

        return new ValidationResult(ValidationMessages.InvalidGender);
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-val-notNullEnum", ErrorMessage);
    }
}