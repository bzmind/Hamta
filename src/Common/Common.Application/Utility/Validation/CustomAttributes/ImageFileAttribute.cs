using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Common.Application.Utility.Validation.CustomAttributes;

public class ImageFileAttribute : ValidationAttribute, IClientModelValidator
{
    public override bool IsValid(object? value)
    {
        if (value == null)
            return true;

        if (value is ICollection valueCollection)
        {
            if (valueCollection.Count == 0)
                return true;

            foreach (var v in valueCollection)
            {
                return v is IFormFile iFormFile && iFormFile.IsImage();
            }
        }
        return value is IFormFile fileInput && fileInput.IsImage();
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");

        context.Attributes.Add("accept", "image/*");
        context.Attributes.Add("data-val-imageFile", ErrorMessage);
    }
}