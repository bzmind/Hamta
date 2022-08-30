using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Common.Api.Attributes;

public class RequiredIfAttribute : ValidationAttribute, IClientModelValidator
{
    private string PropertyName { get; set; }
    private object DesiredValue { get; set; }

    public RequiredIfAttribute(string propertyName, object desiredValue)
    {
        PropertyName = propertyName;
        DesiredValue = desiredValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        object instance = context.ObjectInstance;
        Type type = instance.GetType();
        object propertyValue = type.GetProperty(PropertyName).GetValue(instance, null);

        if ((value == null && propertyValue == DesiredValue)
            || (value == null && propertyValue != null && propertyValue.Equals(DesiredValue)))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        MergeAttribute(context.Attributes, "data-val", "true");
        var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
        MergeAttribute(context.Attributes, "data-val-requiredIf", errorMessage);
        MergeAttribute(context.Attributes, "data-val-requiredIf-otherProperty", PropertyName);
        MergeAttribute(context.Attributes, "data-val-requiredIf-otherPropertyDesiredValue",
            DesiredValue == null ? "" : DesiredValue.ToString());
    }

    private void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
    {
        if (attributes.ContainsKey(key))
        {
            return;
        }
        attributes.Add(key, value);
    }
}