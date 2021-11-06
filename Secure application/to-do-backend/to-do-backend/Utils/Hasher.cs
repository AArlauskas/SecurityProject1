using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace to_do_backend.Utils
{
    public class Hasher
    {
        public static (byte[], string) HashPassword(string password, byte[] salt = null)
        {
            if (salt == null)
            {
                salt = new byte[128 / 8];

                var rng = RandomNumberGenerator.Create();
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return (salt, hashed);
        }

        public static bool CheckPlaintextAgainstHash(string plaintext, string hash, byte[] salt)
        {
            return HashPassword(plaintext, salt).Item2 == hash;
        }
    }
}
