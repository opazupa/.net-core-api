using System;
using System.Security.Cryptography;
using System.Text;

namespace CoreLibrary.Utils
{
    public static class Cryptography
    {
        private static int SALT_POSITION = 0;
        private static int HASH_POSITION = 1;

        /// <summary>
        /// Hash the given password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 32)
            {
                IterationCount = 10000,
                
            };

            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            byte[] salt = rfc2898DeriveBytes.Salt;

            return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
        }
    }
}