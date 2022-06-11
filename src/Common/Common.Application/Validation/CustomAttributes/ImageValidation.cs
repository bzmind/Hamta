using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace Common.Application.Validation.CustomAttributes;

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