using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Common.Application.Utility.Validation.CustomAttributes;

public static class ImageValidation
{
    public static bool IsImage(this IFormFile file)
    {
        try
        {
            var img = Image.FromStream(file.OpenReadStream());
            return true;
        }
        catch
        {
            return false;
        }
    }
}