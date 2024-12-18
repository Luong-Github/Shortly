using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Share.Utilities
{
    public static class ByteSegmentExtractor
    {
        public static byte[] Extract(byte[] sourceBytes, int offset = 0, int length = 6)
        {
            // Validate input parameters
            if (sourceBytes == null)
                throw new ArgumentNullException(nameof(sourceBytes), "Source bytes cannot be null.");

            if (offset < 0 || offset >= sourceBytes.Length)
                throw new ArgumentOutOfRangeException(nameof(offset), "Offset must be within the bounds of the source array.");

            if (length <= 0 || offset + length > sourceBytes.Length)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be positive and within array bounds.");

            // Extract the byte segment
            byte[] segment = new byte[length];
            Array.Copy(sourceBytes, offset, segment, 0, length);
            return segment;
        }
    }
}
