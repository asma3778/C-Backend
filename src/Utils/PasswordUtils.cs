using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace sda_3_online_Backend_Teamwork.src.Utils
{
    public class PasswordUtils
    {
        public static void HashPassword(
            string originalPassword,
            out string hashedPassword,
            out byte[] salt
        )
        {
            var hmac = new HMACSHA256();
            salt = hmac.Key;

            hashedPassword = BitConverter.ToString(
                hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword))
            );

            // return hashedPassword;
        }

        //method 2 : Compare
        public static bool VerifyPassword(
            string originalPassword,
            string hashedPassword,
            byte[] salt
        )
        {
            var hmac = new HMACSHA256(salt);
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)))
                == hashedPassword;
        }
    }
}
