using System.Text;

namespace SzymiShop.WebApi.Business.Util
{
    internal static class RandomString
    {
        private static readonly string _chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";


        public static string Generate(int length)
        {
            var r = new Random();
            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                char c = _chars[r.Next(_chars.Length)];
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
