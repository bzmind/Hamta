using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Common.Application.Utility.Validation.CustomAttributes;

public class ImageFileAttribute : ValidationAttribute, IClientModelValidator
{
    public override bool IsValid(object? value)
    {
        if (value is not IFormFile fileInput)
            return false;

        return fileInput.IsImage();
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");

        context.Attributes.Add("accept", "image/*");
        context.Attributes.Add("data-val-imageFile", ErrorMessage);
    }
}

public class ImageFileListAttribute : ValidationAttribute, IClientModelValidator
{
    public override bool IsValid(object? value)
    {
        if (value is not ICollection<IFormFile> formFiles)
            return false;

        foreach (var formFile in formFiles)
        {
            if (formFile.IsImage() == false)
                return false;
        }
        
        return true;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");

        context.Attributes.Add("accept", "image/*");
        context.Attributes.Add("data-val-imageFileList", ErrorMessage);
    }
}