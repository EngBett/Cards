using System.Security.Cryptography;
using System.Text;
using Agc.GoodShepherd.Common.Hashing;

namespace Agc.GoodShepherd.Common.Extensions;

public static class HelperExtensions
{
    public static string ToBase64(this string str)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
    }

    public static string ToReferenceNumber(this string value)
    {
        using var sha256Managed = SHA256.Create();
        var stringBuilder = new StringBuilder();
        var hashedBytes = sha256Managed.ComputeHash(Encoding.ASCII.GetBytes(value));
        foreach (var b in hashedBytes)
            stringBuilder.Append(b.ToString("x2"));

        var hashString = stringBuilder.ToString();
        return Base36.Encode(Convert.ToInt64(hashString[..12], 16));
    }
}