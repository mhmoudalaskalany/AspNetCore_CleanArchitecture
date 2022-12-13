using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Template.Common.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CryptoHasher
    {
        private const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
        private const int Pbkdf2SubKeyLength = 256 / 8; // 256 bits
        private const int SaltSize = 128 / 8; // 128 bits
        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            // Produce a version 0 (see comment above) text hash.
            byte[] salt;
            byte[] subKey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Pbkdf2IterCount , HashAlgorithmName.SHA256))
            {
                salt = deriveBytes.Salt;
                subKey = deriveBytes.GetBytes(Pbkdf2SubKeyLength);
            }

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubKeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subKey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubKeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        // hashedPassword must be of the format of HashWithPassword (salt + Hash(salt+input)
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            // Verify a version 0 (see comment above) text hash.

            if (hashedPasswordBytes.Length != (1 + SaltSize + Pbkdf2SubKeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                // Wrong length or version header.
                return false;
            }

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
            var storedSubKey = new byte[Pbkdf2SubKeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubKey, 0, Pbkdf2SubKeyLength);

            byte[] generatedSubKey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Pbkdf2IterCount , HashAlgorithmName.SHA256))
            {
                generatedSubKey = deriveBytes.GetBytes(Pbkdf2SubKeyLength);
            }
            return ByteArraysEqual(storedSubKey, generatedSubKey);
        }

        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}
