using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace CookieMVC.Security
{
    public class HashUtility
    {
        public static string GenerateSalt(int size = 64)
        {
            var crypto = new RNGCryptoServiceProvider();
            byte[] rbytes = new byte[size / 2];
            crypto.GetNonZeroBytes(rbytes);

            return ToHexString(rbytes);
        }

        public static string GenerateToken(int size = 64)
        {
            var crypto = new RNGCryptoServiceProvider();
            byte[] rbytes = new byte[size / 2];
            crypto.GetNonZeroBytes(rbytes);

            return ToHexString(rbytes, true);
        }

        private static string ToHexString(byte[] bytes, bool useLowerCase = false)
        {
            var hex = string.Concat(bytes.Select(b => b.ToString(useLowerCase ? "x2" : "X2")));

            return hex;
        }


        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="hashMode">Type of hash algorithm used to hash the text.</param>
        /// <param name="forceUpperCase">if set to <c>true</c>, forces uppercase characters in the hash-string.</param>
        /// <param name="unicode">if set to <c>true</c>, UTF-8 encoded bytes are extracted from the source string; otherwise, bytes are ASCII encoded.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid hash type; hashMode</exception>
        /// <remarks>
        /// This method uses UTF-8 encoding by default.
        /// </remarks>
        public static string GetHash(string text, HashMode hashMode, bool forceUpperCase = false, bool unicode = true)
        {
            HashAlgorithm algorithm;
            switch (hashMode)
            {
                case HashMode.MD5:
                    algorithm = MD5Cng.Create();
                    break;
                case HashMode.SHA1:
                    algorithm = SHA1Cng.Create();
                    break;
                case HashMode.SHA256:
                    algorithm = SHA256Cng.Create();
                    break;
                case HashMode.SHA512:
                    algorithm = SHA512Cng.Create();
                    break;
                case HashMode.HMACMD5:
                    algorithm = HMACMD5.Create();
                    break;
                case HashMode.HMACSHA1:
                    algorithm = HMACSHA1.Create();
                    break;
                case HashMode.HMACSHA256:
                    algorithm = HMACSHA256.Create();
                    break;
                case HashMode.HMACSHA512:
                    algorithm = HMACSHA512.Create();
                    break;
                default:
                    throw new ArgumentException("Invalid HashMode", "hashMode");
            }
            byte[] bytes = unicode ? Encoding.UTF8.GetBytes(text) : Encoding.ASCII.GetBytes(text);
            byte[] hash = algorithm.ComputeHash(bytes);

            return ToHexString(hash, !forceUpperCase);
        }
    }
}
