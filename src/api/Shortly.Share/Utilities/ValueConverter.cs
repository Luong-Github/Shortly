using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shortly.Share.Utilities
{
    public static class ValueConverter
    {
        private static readonly Regex HexCleaner = new Regex("[^0-9a-fA-F]+", RegexOptions.Compiled);

        /// <summary>
        /// Converts a hexadecimal byte array to its decimal equivalent.
        /// </summary>
        /// <param name="bytes">The byte array representing the hexadecimal value.</param>
        /// <returns>The decimal value of the hexadecimal byte array.</returns>
        public static long HexByteToDecimal(byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));

            return HexByteToDecimalInternal(bytes);
        }

        /// <summary>
        /// Converts a hexadecimal byte array to its decimal equivalent with an optional offset.
        /// </summary>
        /// <param name="bytes">The byte array representing the hexadecimal value.</param>
        /// <param name="offset">The optional starting position in the byte array.</param>
        /// <returns>The decimal value of the hexadecimal byte array.</returns>
        public static long HexByteToDecimal(byte[] bytes, int? offset)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));

            // Use offset to skip initial bytes (if specified)
            byte[] processedBytes = offset.HasValue && offset.Value < bytes.Length
                ? bytes[offset.Value..]
                : bytes;

            return HexByteToDecimalInternal(processedBytes);
        }

        /// <summary>
        /// Helper method that processes the conversion logic.
        /// </summary>
        /// <param name="bytes">The byte array to process.</param>
        /// <returns>The cleaned and converted decimal value.</returns>
        private static long HexByteToDecimalInternal(byte[] bytes)
        {
            // Remove special characters and convert hex string to decimal
            string hexString = BitConverter.ToString(bytes);
            string cleanedHex = HexCleaner.Replace(hexString, "");
            return Convert.ToInt64(cleanedHex, 16);
        }
    }
}
