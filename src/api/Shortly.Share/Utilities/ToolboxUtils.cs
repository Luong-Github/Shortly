using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Share.Utilities
{
    public static class ToolboxUtils
    {
        /// <summary>
        /// Encodes a positive integer value to a custom Base-X string, using a specified character set.
        /// </summary>
        /// <param name="value">The number to encode. Must be non-negative.</param>
        /// <param name="characterSet">A character array defining the encoding base. Example: Base62 uses "0-9A-Za-z".</param>
        /// <returns>A string representation of the number encoded in the provided Base-X format.</returns>
        public static string EncodeToBase(long value, char[] characterSet)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be non-negative.");

            if (characterSet == null || characterSet.Length < 2)
                throw new ArgumentException("Character set must contain at least 2 unique characters.", nameof(characterSet));

            // Handle zero case immediately
            if (value == 0)
                return characterSet[0].ToString();

            StringBuilder result = new StringBuilder();
            int baseLength = characterSet.Length;

            // Main encoding logic: Reduce the value by dividing it with the base length
            while (value > 0)
            {
                int remainder = (int)(value % baseLength);
                result.Insert(0, characterSet[remainder]); // Add remainder symbol at the beginning
                value /= baseLength;
            }

            return result.ToString();
        }

        /// <summary>
        /// Predefined character set for Base62 encoding (0-9, A-Z, a-z).
        /// </summary>
        public static readonly char[] Base62Chars =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

    }
}
