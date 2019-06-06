using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static ScriptNotepad.UtilityClasses.Encodings.DetectEncoding;

namespace ScriptNotepad.UtilityClasses.Encodings
{
    /// <summary>
    /// A class to help to match a given byte data to an encoded string.
    /// </summary>
    class TryMakeEncoding
    {
        /// <summary>
        /// A delegate for the <see cref="ExceptionOccurred"/> event.
        /// </summary>
        /// <param name="methodName">Name of the method in which the exception was caused.</param>
        /// <param name="e">The <see cref="EncodingExceptionEventArgs"/> instance containing the event data.</param>
        public delegate void OnExceptionOccurred(string methodName, EncodingExceptionEventArgs e);

        /// <summary>
        /// Occurs when exception occurred in one of the methods of this class.
        /// </summary>
        public static event OnExceptionOccurred ExceptionOccurred;

        /// <summary>
        /// Tries to convert a given memory stream to the UTF8 encoding by adding an UTF8 BOM to the stream.
        /// </summary>
        /// <param name="stream">The stream to try to convert to.</param>
        /// <returns>A string converted into the UTF8 encoding if successful; otherwise null.</returns>
        public static string TryUtf8Encoding(MemoryStream stream)
        {
            if (ByteMatch(GetEncodingComparisonBytes(stream), Utf8Bom))
            {
                try
                {
                    return new UTF8Encoding(false, true).GetString(stream.ToArray());
                }
                catch (Exception ex)
                {
                    ExceptionOccurred?.Invoke("TryUtf8Encoding", new EncodingExceptionEventArgs {Exception = ex});
                    // failed..
                    return null;
                }
            }

            try // there is no BOM..
            {
                // get the contents of the memory stream into a list of bytes..
                List<byte> bytes = new List<byte>(stream.ToArray());

                // insert the BOM..
                bytes.InsertRange(0, Utf8Bom);

                // try the UTF8 encoding with the BOM..
                return new UTF8Encoding(false, true).GetString(bytes.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionOccurred?.Invoke("TryUtf8Encoding", new EncodingExceptionEventArgs {Exception = ex});
                // failed..
                return null;
            }
        }

        /// <summary>
        /// Tries to convert a given memory stream to the UTF16 encoding by adding an UTF16 BOM to the stream.
        /// </summary>
        /// <param name="stream">The stream to try to convert to.</param>
        /// <param name="bigEndian"><c>true</c> to use the big endian byte order (most significant byte first); <c>false</c> to use the little endian byte order (least significant byte first).</param>
        /// <returns>A string converted into the UTF16 encoding if successful; otherwise null.</returns>
        public static string TryUtf16Encoding(MemoryStream stream, bool bigEndian)
        {
            if (!bigEndian && ByteMatch(GetEncodingComparisonBytes(stream), Utf16LittleEndianBom) ||
                bigEndian && ByteMatch(GetEncodingComparisonBytes(stream), Utf16BigEndianBom))
            {
                try
                {
                    return new UnicodeEncoding(bigEndian, true, true).GetString(stream.ToArray());
                }
                catch (Exception ex)
                {
                    ExceptionOccurred?.Invoke("TryUtf16Encoding", new EncodingExceptionEventArgs {Exception = ex});
                    // failed..
                    return null;
                }
            }

            try // there is no BOM..
            {
                // get the contents of the memory stream into a list of bytes..
                List<byte> bytes = new List<byte>(stream.ToArray());

                // insert the BOM..
                bytes.InsertRange(0, bigEndian ? Utf16BigEndianBom : Utf16LittleEndianBom);

                // try the UTF16 encoding with the BOM..
                return new UnicodeEncoding(bigEndian, true, true).GetString(bytes.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionOccurred?.Invoke("TryUtf16Encoding", new EncodingExceptionEventArgs {Exception = ex});
                // failed..
                return null;
            }
        }

        /// <summary>
        /// Tries to convert a given memory stream to the UTF32 encoding by adding an UTF32 BOM to the stream.
        /// </summary>
        /// <param name="stream">The stream to try to convert to.</param>
        /// <param name="bigEndian"><c>true</c> to use the big endian byte order (most significant byte first); <c>false</c> to use the little endian byte order (least significant byte first).</param>
        /// <returns>A string converted into the UTF32 encoding if successful; otherwise null.</returns>
        public static string TryUtf32Encoding(MemoryStream stream, bool bigEndian)
        {
            if (!bigEndian && ByteMatch(GetEncodingComparisonBytes(stream), Utf32LittleEndianBom) ||
                bigEndian && ByteMatch(GetEncodingComparisonBytes(stream), Utf32BigEndianBom))
            {
                try
                {
                    return new UTF32Encoding(bigEndian, true, true).GetString(stream.ToArray());
                }
                catch (Exception ex)
                {
                    ExceptionOccurred?.Invoke("TryUtf32Encoding", new EncodingExceptionEventArgs {Exception = ex});
                    // failed..
                    return null;
                }
            }

            try // there is no BOM..
            {
                // get the contents of the memory stream into a list of bytes..
                List<byte> bytes = new List<byte>(stream.ToArray());

                // insert the BOM..
                bytes.InsertRange(0, bigEndian ? Utf32BigEndianBom : Utf32LittleEndianBom);

                // try the UTF32 encoding with the BOM..
                return new UTF32Encoding(bigEndian, true, true).GetString(bytes.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionOccurred?.Invoke("TryUtf32Encoding", new EncodingExceptionEventArgs {Exception = ex});
                // failed..
                return null;
            }
        }
    }
}
