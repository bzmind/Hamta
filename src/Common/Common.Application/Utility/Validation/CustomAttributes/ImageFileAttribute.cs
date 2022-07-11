using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Common.Application.Utility.Validation.CustomAttributes;

public class ImageFileAttribute : ValidationAttribute, IClientModelValidator
{
    public override bool IsValid(object? value)
    {
        if (value is not IFormFile fileInput)
            return true;

        return fileInput.IsImage();
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");

        context.Attributes.Add("accept", "image/*");
        context.Attributes.Add("data-val-fileImage", ErrorMessage);
    }
}