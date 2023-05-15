using System.Net;

namespace FTI.PartnerMiddle.Shared.Extensions
{
    public static class StringExtensions
    {

        public static string HtmlEncoded(this string value)
        {
            return WebUtility.HtmlEncode(value);
        }

        public static string Base64Encode(this string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            Span<byte> buffer = new(new byte[base64EncodedData.Length]);
            bool isBase64String = Convert.TryFromBase64String(base64EncodedData, buffer, out int _);

            if (!isBase64String)
            {
                return string.Empty;
            }

            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        
    }
}