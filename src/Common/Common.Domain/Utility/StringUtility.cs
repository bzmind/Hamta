using System.Globalization;

namespace Common.Domain.Utility;

public static class StringUtility
{
    public static string ReplaceFarsiDigits(this string input)
    {
        var cultureInfo = new CultureInfo("fa");
        var nativeDigits = cultureInfo.NumberFormat.NativeDigits;
        return input
            .Replace(cultureInfo.NumberFormat.NumberDecimalSeparator, ".")
            .Replace(cultureInfo.NumberFormat.NumberGroupSeparator, ",")
            .Replace(cultureInfo.NumberFormat.NegativeSign, "-")
            .Replace(cultureInfo.NumberFormat.PositiveSign, "+")
            .Replace(nativeDigits[0], "0")
            .Replace(nativeDigits[1], "1")
            .Replace(nativeDigits[2], "2")
            .Replace(nativeDigits[3], "3")
            .Replace(nativeDigits[4], "4")
            .Replace(nativeDigits[5], "5")
            .Replace(nativeDigits[6], "6")
            .Replace(nativeDigits[7], "7")
            .Replace(nativeDigits[8], "8")
            .Replace(nativeDigits[9], "9");
    }
}