using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Common.Api.Attributes;

public class DictionaryMembersCharactersMinLengthAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly int _keyMinLength;
    private readonly int _valueMinLength;

    public DictionaryMembersCharactersMinLengthAttribute(int keyMinlength, int valueMinLength)
    {
        _keyMinLength = keyMinlength;
        _valueMinLength = valueMinLength;
    }

    public override bool IsValid(object? value)
    {
        if (value is not IDictionary<string, string> dictionary)
            return false;

        foreach (var keyValuePair in dictionary)
        {
            if (keyValuePair.Key.Length < _keyMinLength ||
                keyValuePair.Value.Length < _valueMinLength)
                return false;
        }

        return true;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-val-dictionaryMembersCharactersMinLength", ErrorMessage);
    }
}

public class DictionaryMembersCharactersMaxLengthAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly int _keyMaxLength;
    private readonly int _valueMaxLength;

    public DictionaryMembersCharactersMaxLengthAttribute(int keyMaxlength, int valueMaxLength)
    {
        _keyMaxLength = keyMaxlength;
        _valueMaxLength = valueMaxLength;
    }

    public override bool IsValid(object? value)
    {
        if (value is not IDictionary<string, string> dictionary)
            return false;

        foreach (var keyValuePair in dictionary)
        {
            if (keyValuePair.Key.Length > _keyMaxLength ||
                keyValuePair.Value.Length > _valueMaxLength)
                return false;
        }

        return true;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-val-dictionaryMembersCharactersMaxLength", ErrorMessage);
    }
}