using System.Text.RegularExpressions;

namespace Common.Application.Utility;

public static class StringCheckers
{
    public static bool IsEmail(this string input)
    {
        return Regex.IsMatch(input, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");
    }

    public static bool IsIranPhone(this string input)
    {
        var r = new Regex(@"^(?:0)?(9\d{9})$");
        return r.IsMatch(input) && input.Length is >= 10 and <= 11;
    }
}