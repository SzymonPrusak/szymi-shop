using System.Runtime.CompilerServices;

namespace SzymiShop.WebApi.Business.Validation
{
    internal static class StringValidate
    {
        public static void NotNullOrWhitespace(string s, [CallerArgumentExpression(nameof(s))] string? name = null)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException($"{name} cannot be null or whitespace");
        }

        public static void Length(string s, int min, int max, [CallerArgumentExpression(nameof(s))] string? name = null)
        {
            if (s.Length < min)
                throw new ArgumentException($"{name} has bo be at least {min} characters long");
            if (s.Length > max)
                throw new ArgumentException($"{name} has bo be at least {max} characters long");
        }
    }
}
