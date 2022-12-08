using SzymiShop.WebApi.Business.Validation;

namespace SzymiShop.WebApi.Business.Model
{
    public struct Price
    {
        private int _value;

        public Price(int major, int minor)
        {
            IntValidate.MinMax(minor, 0, 99);

            _value = major * 100 + minor;
        }

        public readonly int Value => _value;
        public readonly int Major => _value / 100;
        public readonly int Minor => _value % 100;

        public static Price FromValue(int value) => new Price { _value = value };
    }
}
