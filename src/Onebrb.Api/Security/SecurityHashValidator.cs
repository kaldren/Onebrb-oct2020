using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Api.Security
{
    public static class SecurityHashValidator
    {
        public static bool IsValidSecurityHash(string userId, string securityHash)
        {
            // App key
            byte[] salt = Encoding.ASCII.GetBytes("app8cdf44fc-f815-4751-82a5-43751470a1c8salt");

            string generatedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userId,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));


            return generatedHash == securityHash;
        }
    }
}
