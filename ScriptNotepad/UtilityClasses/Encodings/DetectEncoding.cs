#region License
/*
MIT License

Copyright(c) 2019 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScriptNotepad.UtilityClasses.Encodings
{
    /// <summary>
    /// A class to help to detect an encoding of a text.
    /// </summary>
    // (C): https://en.wikipedia.org/wiki/ISO/IEC_8859-1
    // (C): https://en.wikipedia.org/wiki/Byte_order_mar
    // (C): https://en.wikipedia.org/wiki/Specials_(Unicode_block)#Replacement_character
    public class DetectEncoding
    {
        /// <summary>
        /// The UTF8 byte order marks.
        /// </summary>
        internal static readonly List<byte> Utf8Bom = new List<byte>(new byte[] {0xEF, 0xBB, 0xBF});

        // ReSharper disable once IdentifierTypo        
        /// <summary>
        /// The UTF16 big endian byte order marks.
        /// </summary>
        internal static readonly List<byte> Utf16BigEndianBom = new List<byte>(new byte[] {0xFE, 0xFF});

        // ReSharper disable once IdentifierTypo        
        /// <summary>
        /// The UTF16 little endian byte order marks.
        /// </summary>
        internal static readonly List<byte> Utf16LittleEndianBom = new List<byte>(new byte[] {0xFF, 0xFE});

        // ReSharper disable once IdentifierTypo        
        /// <summary>
        /// The UTF23 big endian byte order marks.
        /// </summary>
        internal static readonly List<byte> Utf32BigEndianBom = new List<byte>(new byte[] {0x00, 0x00, 0xFE, 0xFF});

        // ReSharper disable once IdentifierTypo        
        /// <summary>
        /// The UTF23 little endian byte order marks.
        /// </summary>
        internal static readonly List<byte> Utf32LittleEndianBom = new List<byte>(new byte[] {0xFF, 0xFE, 0x00, 0x00});

        /// <summary>
        /// The first variant of the UTF7 byte order marks.
        /// </summary>
        internal static readonly List<byte> Utf7Bom1 = new List<byte>(new byte[] {0x2B, 0x2F, 0x76, 0x38});

        /// <summary>
        /// The second variant of the UTF7 byte order marks.
        /// </summary>
        internal static readonly List<byte> Utf7Bom2 = new List<byte>(new byte[] {0x2B, 0x2F, 0x76, 0x39});

        /// <summary>
        /// The third variant of the UTF7 byte order marks.
        /// </summary>
        internal static readonly List<byte> Utf7Bom3 = new List<byte>(new byte[] {0x2B, 0x2F, 0x76, 0x2B});

        /// <summary>
        /// The fourth variant of the UTF7 byte order marks.
        /// </summary>
        internal static readonly List<byte> Utf7Bom4 = new List<byte>(new byte[] {0x2B, 0x2F, 0x76, 0x2F});

        /// <summary>
        /// The fifth variant of the UTF7 byte order marks.
        /// </summary>
        internal static readonly List<byte> Utf7Bom5 = new List<byte>(new byte[] {0x2B, 0x2F, 0x76, 0x38, 0x2D});

        /// <summary>
        /// The UTF8 replacement character.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        internal static readonly List<byte> Utf8ReplacementChar = new List<byte>(new byte[] {0xFF, 0xFD});

        /// <summary>
        /// Checks if a given byte array matches a given byte order mark.
        /// </summary>
        /// <param name="bytes">The bytes to verify the byte order mark from.</param>
        /// <param name="bom">The byte order mark (BOM).</param>
        /// <returns><c>true</c> if given bytes matches the given byte order mark, <c>false</c> otherwise.</returns>
        internal static bool ByteMatch(byte[] bytes, List<byte> bom)
        {
            // avoid a useless exception..
            if (bytes.Length < bom.Count)
            {
                return false;
            }

            // compare the BOM bytes..
            for (int i = 0; i < bom.Count; i++)
            {
                if (bytes[i] != bom[i])
                {
                    // in-equality, so failure..
                    return false;
                }
            }

            // success..
            return true;
        }

        /// <summary>
        /// Determines whether the specified byte array is a byte-order-mark (BOM).
        /// </summary>
        /// <param name="bom">The byte array to check for a BOM.</param>
        /// <returns><c>true</c> if the specified byte array has a byte-order-mark (BOM); otherwise, <c>false</c>.</returns>
        public static bool HasBom(byte[] bom)
        {
            List<byte> bomBytes = new List<byte>(bom);
            return ByteMatch(Utf8Bom.ToArray(), bomBytes) ||
                   ByteMatch(Utf16BigEndianBom.ToArray(), bomBytes) ||
                   ByteMatch(Utf16LittleEndianBom.ToArray(), bomBytes) ||
                   ByteMatch(Utf32BigEndianBom.ToArray(), bomBytes) ||
                   ByteMatch(Utf32LittleEndianBom.ToArray(), bomBytes);
        }

        /// <summary>
        /// Determines whether the specified byte array is a big-endian byte-order-mark (BOM).
        /// </summary>
        /// <param name="bom">The byte array to check for a byte order.</param>
        /// <returns><c>true</c> if the specified byte array is a big-endian byte-order-mark (BOM); otherwise, <c>false</c>.</returns>
        public static bool IsBigEndian(byte[] bom)
        {
            List<byte> bomBytes = new List<byte>(bom);
            return ByteMatch(Utf16BigEndianBom.ToArray(), bomBytes) ||
                   ByteMatch(Utf32BigEndianBom.ToArray(), bomBytes);
        }

        /// <summary>
        /// Gets the encoding comparison bytes (the first five bytes of the stream).
        /// </summary>
        /// <param name="stream">The stream get the bytes from.</param>
        /// <returns>System.Byte[].</returns>
        internal static byte[] GetEncodingComparisonBytes(MemoryStream stream)
        {
            stream.Position = 0;

            byte[] bytes = new byte[5];

            stream.Read(bytes, 0, 5);

            return bytes;
        }

        /// <summary>
        /// Gets or sets the fall back encoding if no other encodings are detected.
        /// </summary>
//        public static Encoding FallBackEncoding { get; set; } = Encoding.GetEncoding(28591); // ISO-8859-1
        public static Encoding FallBackEncoding { get; set; } = Encoding.Default;

        /// <summary>
        /// Gets an <see cref="Encoding"/> from a given byte array.
        /// </summary>
        /// <param name="bytes">The byte array to get the encoding from.</param>
        /// <returns>The detected <see cref="Encoding"/>.</returns>
        public static Encoding FromBytes(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                return FromStream(memoryStream);
            }
        }

        /// <summary>
        /// Gets an <see cref="Encoding"/> from a given list of bytes.
        /// </summary>
        /// <param name="bytes">The list of bytes to get the encoding from.</param>
        /// <returns>The detected <see cref="Encoding"/>.</returns>
        public static Encoding FromBytes(List<byte> bytes)
        {
            using(var memoryStream = new MemoryStream(bytes.ToArray()))
            {
                return FromStream(memoryStream);
            }
        }

        /// <summary>
        /// Gets an <see cref="Encoding"/> from a given file stream.
        /// </summary>
        /// <param name="stream">The stream to get the encoding from.</param>
        /// <returns>The detected <see cref="Encoding"/>.</returns>
        public static Encoding FromStream(FileStream stream)
        {
            using(var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return FromStream(memoryStream);
            }
        }

        /// <summary>
        /// Gets an <see cref="Encoding"/> from a given memory stream.
        /// </summary>
        /// <param name="stream">The stream to get the encoding from.</param>
        /// <returns>The detected <see cref="Encoding"/>.</returns>
        public static Encoding FromStream(MemoryStream stream)
        {
            byte[] bytes = GetEncodingComparisonBytes(stream);

            if (ByteMatch(bytes, Utf7Bom5) || 
                ByteMatch(bytes, Utf7Bom4) ||
                ByteMatch(bytes, Utf7Bom3) ||
                ByteMatch(bytes, Utf7Bom2) ||
                ByteMatch(bytes, Utf7Bom1))
            {
                return new UTF7Encoding(false);
            }

            if (ByteMatch(bytes, Utf8Bom))
            {
                return new UTF8Encoding(false, true);
            }

            if (ByteMatch(bytes, Utf16BigEndianBom))
            {
                return new UnicodeEncoding(true, true, true);
            }

            if (ByteMatch(bytes, Utf16LittleEndianBom))
            {
                return new UnicodeEncoding(false, true, true);
            }

            if (ByteMatch(bytes, Utf32BigEndianBom))
            {
                return new UTF32Encoding(true, true, true);
            }

            if (ByteMatch(bytes, Utf32LittleEndianBom))
            {
                return new UTF32Encoding(false, true, true);
            }

            return FallBackEncoding;
        }

    }
}
