using System.Security.Cryptography;
using System.Text;

namespace Common.Application.Utility.Security;

public static class SHA256Hash
{
    public static string ToSHA256(this string input)
    {
        using var sha256 = SHA256.Create();
        var inputBytes = Encoding.Default.GetBytes(input);
        var hashedOutput = sha256.ComputeHash(inputBytes);
        return Convert.ToBase64String(hashedOutput);
    }

    public static bool Compare(string hashedText, string rawText)
    {
        return rawText.ToSHA256() == hashedText;
    }
}