using Shortly.Share.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Share.Utilities
{
    public static class HashGenerator 
    {
        public static string GenerateHash(string input, HashAlgorithmTypeEnum hashAlgorithm)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input), "Input cannot be null or empty.");

            using (HashAlgorithm algorithm = CreateHashAlgorithm(hashAlgorithm))
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = algorithm.ComputeHash(inputBytes);
                
                StringBuilder str = new StringBuilder(hashBytes.Length * 2);
                foreach (byte b in hashBytes)
                {
                    str.Append(b.ToString("x2"));
                }
                return str.ToString();
            }
        }

        public static byte[] GenerateHashBytes(string input, HashAlgorithmTypeEnum hashAlgorithm)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input), "Can not be empty");

            using (HashAlgorithm algo = CreateHashAlgorithm(hashAlgorithm))
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                return algo.ComputeHash(inputBytes);
            }
        }

        private static HashAlgorithm CreateHashAlgorithm(HashAlgorithmTypeEnum hashAlgorithm) =>
            hashAlgorithm switch
            {
                HashAlgorithmTypeEnum.SHA256 => SHA256.Create(),
                HashAlgorithmTypeEnum.MD5 => MD5.Create(),
                _ => throw new NotSupportedException($"The algorithm '${hashAlgorithm}' is not supported.")
            };
    }
}
