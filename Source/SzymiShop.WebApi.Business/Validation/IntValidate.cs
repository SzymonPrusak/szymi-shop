using System.Runtime.CompilerServices;

namespace SzymiShop.WebApi.Business.Validation
{
    internal static class IntValidate
    {
        public static void MinMax(int n, int min, int max, [CallerArgumentExpression(nameof(n))] string? name = null)
        {
            if (n < min)
                throw new ArgumentException($"{name} cannot be lower than {min}");
            if (n > max)
                throw new ArgumentException($"{name} cannot be greater than {max}");
        }
    }
}
