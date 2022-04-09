using System.Security.Cryptography;
using System.Text;

namespace Common.Application.Security;

public static class Hash
{
    public static string ToSHA256(this string input)
    {
        using var sha256 = SHA256.Create();
        var inputBytes = Encoding.Default.GetBytes(input);
        var hashedOutput = sha256.ComputeHash(inputBytes);
        return hashedOutput.ToString();
    }
}