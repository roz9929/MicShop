﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MicShop.Core.Helpers
{
    public static class Pbkdf2Hasher
    {
        public static string ComputeHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(
              KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
              )
            );
        }

        public static byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            return salt;
        }
    }
}
