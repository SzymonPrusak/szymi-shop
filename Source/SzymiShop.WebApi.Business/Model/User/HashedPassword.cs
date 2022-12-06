using System.Security.Cryptography;
using System.Text;
using SzymiShop.WebApi.Business.Util;

namespace SzymiShop.WebApi.Business.Model.User
{
    public class HashedPassword
    {
        private readonly byte[] _passwordHash;

        public HashedPassword(string plainPassword)
        {
            string salt = RandomString.Generate(16);
            _passwordHash = HashPassword(plainPassword, salt);
            PasswordHashB64 = Convert.ToBase64String(_passwordHash);
            Salt = salt;
        }

        public HashedPassword(string passwordHashB64, string salt)
        {
            _passwordHash = Convert.FromBase64String(passwordHashB64);
            PasswordHashB64 = passwordHashB64;
            Salt = salt;
        }


        public string PasswordHashB64 { get; }
        public string Salt { get; }


        public bool CheckPassword(string plain)
        {
            var hashed = HashPassword(plain, Salt);
            return _passwordHash.SequenceEqual(hashed);
        }

        private static byte[] HashPassword(string password, string salt)
        {
            var plainBytes = Encoding.UTF8.GetBytes(password + salt);
            var hashed = SHA256.HashData(plainBytes);
            return hashed;
        }
    }
}
