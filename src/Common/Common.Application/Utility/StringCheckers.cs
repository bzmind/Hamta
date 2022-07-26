using System.Text.RegularExpressions;
using Common.Application.Utility.Validation;

namespace Common.Application.Utility;

public static class StringCheckers
{
    public static bool IsEmail(this string input)
    {
        return Regex.IsMatch(input, ValidationMessages.EmailRegex);
    }

    public static bool IsIranPhone(this string input)
    {
        var r = new Regex(ValidationMessages.IranPhoneRegex);
        return r.IsMatch(input) && input.Length is 10 or 11;
    }
}