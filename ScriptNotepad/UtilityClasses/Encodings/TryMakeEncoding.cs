#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScriptNotepad.Settings;
using static ScriptNotepad.UtilityClasses.Encodings.DetectEncoding;

namespace ScriptNotepad.UtilityClasses.Encodings
{
    /// <summary>
    /// A class to help to match a given byte data to an encoded string.
    /// </summary>
    public class TryMakeEncoding
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
        /// Tries different unicode encodings for a given file (with or without a BOM).
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="encoding">The detected encoding if any.</param>
        /// <param name="bigEndian">A value indicating whether the detected encoding is big-endian.</param>
        /// <param name="noBom">A value indicating whether the detected unicode file contains a byte-order-mark.</param>
        /// <returns>The contents of the file as a string if an unicode encoding variant was found for it.</returns>
        public static string TryEncodings(string fileName, out Encoding encoding, out bool bigEndian, out bool noBom)
        {
            noBom = false;
            bigEndian = false;
            try
            {
                using FileStream fileStream = File.OpenRead(fileName);
                using MemoryStream memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);

                // try the UTF8 encoding..
                var result = TryUtf8Encoding(memoryStream, out encoding, out noBom);

                if (result != null)
                {
                    bigEndian = false;
                    return result;
                }

                // try the UTF16 encoding with LittleEndian..
                result = TryUtf16Encoding(memoryStream, false, out encoding, out noBom);

                if (result != null)
                {
                    bigEndian = false;
                    return result;
                }

                // try the UTF16 encoding with BigEndian..
                result = TryUtf16Encoding(memoryStream, true, out encoding, out noBom);

                if (result != null)
                {
                    bigEndian = true;
                    return result;
                }

                // try the UTF32 encoding with LittleEndian..
                result = TryUtf32Encoding(memoryStream, false, out encoding, out noBom);

                if (result != null)
                {
                    bigEndian = false;
                    return result;
                }

                // try the UTF32 encoding with BigEndian..
                result = TryUtf32Encoding(memoryStream, true, out encoding, out noBom);

                if (result != null)
                {
                    bigEndian = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                ExceptionOccurred?.Invoke("TryEncodings", new EncodingExceptionEventArgs {Exception = ex});

                // failed..
                encoding = null;
                return null;
            }

            return null;
        }

        /// <summary>
        /// Tries to convert a given memory stream to the UTF8 encoding by adding an UTF8 BOM to the stream.
        /// </summary>
        /// <param name="stream">The stream to try to convert to.</param>
        /// <param name="encoding">The encoding successfully used with the stream data.</param>
        /// <param name="noBom">A value indicating whether the detected unicode file contains a byte-order-mark.</param>
        /// <returns>A string converted into the UTF8 encoding if successful; otherwise null.</returns>
        public static string TryUtf8Encoding(MemoryStream stream, out Encoding encoding, out bool noBom)
        {
            noBom = false;
            if (ByteMatch(GetEncodingComparisonBytes(stream), Utf8Bom))
            {
                try
                {
                    encoding = new UTF8Encoding(true, true);
                    return encoding.GetString(stream.ToArray());
                }
                catch (Exception ex)
                {
                    ExceptionOccurred?.Invoke("TryUtf8Encoding", new EncodingExceptionEventArgs {Exception = ex});

                    // failed..
                    encoding = null;
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
                encoding = new UTF8Encoding(false, true);
                noBom = true;
                return encoding.GetString(bytes.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionOccurred?.Invoke("TryUtf8Encoding", new EncodingExceptionEventArgs {Exception = ex});

                // failed..
                encoding = null;
                return null;
            }
        }

        /// <summary>
        /// Tries to convert a given memory stream to the UTF16 encoding by adding an UTF16 BOM to the stream.
        /// </summary>
        /// <param name="stream">The stream to try to convert to.</param>
        /// <param name="encoding">The encoding successfully used with the stream data.</param>
        /// <param name="bigEndian"><c>true</c> to use the big endian byte order (most significant byte first); <c>false</c> to use the little endian byte order (least significant byte first).</param>
        /// <param name="noBom">A value indicating whether the detected unicode file contains a byte-order-mark.</param>
        /// <returns>A string converted into the UTF16 encoding if successful; otherwise null.</returns>
        public static string TryUtf16Encoding(MemoryStream stream, bool bigEndian, out Encoding encoding, out bool noBom)
        {
            noBom = false;

            if (!FormSettings.Settings.SkipUnicodeDetectLe && !bigEndian &&
                ByteMatch(GetEncodingComparisonBytes(stream), Utf16LittleEndianBom) ||
                !FormSettings.Settings.SkipUnicodeDetectBe && bigEndian &&
                ByteMatch(GetEncodingComparisonBytes(stream), Utf16BigEndianBom))
            {
                try
                {
                    encoding = new UnicodeEncoding(bigEndian, true, true);
                    return encoding.GetString(stream.ToArray());
                }
                catch (Exception ex)
                {
                    ExceptionOccurred?.Invoke("TryUtf16Encoding", new EncodingExceptionEventArgs {Exception = ex});

                    // failed..
                    encoding = null;
                    return null;
                }
            }

            // the user doesn't want this detection..
            if (FormSettings.Settings.SkipUnicodeDetectLe && !bigEndian ||
                FormSettings.Settings.SkipUnicodeDetectBe && bigEndian)
            {
                // so just return..
                encoding = null;
                return null;
            }

            try // there is no BOM..
            {
                // get the contents of the memory stream into a list of bytes..
                List<byte> bytes = new List<byte>(stream.ToArray());

                // insert the BOM..
//                bytes.InsertRange(0, bigEndian ? Utf16BigEndianBom : Utf16LittleEndianBom);

                // try the UTF16 encoding with the BOM..
                encoding = new UnicodeEncoding(bigEndian, false, true);
                noBom = true;
                return encoding.GetString(bytes.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionOccurred?.Invoke("TryUtf16Encoding", new EncodingExceptionEventArgs {Exception = ex});

                // failed..
                encoding = null;
                return null;
            }
        }

        /// <summary>
        /// Tries to convert a given memory stream to the UTF32 encoding by adding an UTF32 BOM to the stream.
        /// </summary>
        /// <param name="stream">The stream to try to convert to.</param>
        /// <param name="encoding">The encoding successfully used with the stream data.</param>
        /// <param name="bigEndian"><c>true</c> to use the big endian byte order (most significant byte first); <c>false</c> to use the little endian byte order (least significant byte first).</param>
        /// <param name="noBom">A value indicating whether the detected unicode file contains a byte-order-mark.</param>
        /// <returns>A string converted into the UTF32 encoding if successful; otherwise null.</returns>
        public static string TryUtf32Encoding(MemoryStream stream, bool bigEndian, out Encoding encoding, out bool noBom)
        {
            noBom = false;

            if (!FormSettings.Settings.SkipUtf32Le && !bigEndian && ByteMatch(GetEncodingComparisonBytes(stream), Utf32LittleEndianBom) ||
                !FormSettings.Settings.SkipUtf32Be && bigEndian && ByteMatch(GetEncodingComparisonBytes(stream), Utf32BigEndianBom))
            {
                try
                {
                    encoding = new UTF32Encoding(bigEndian, true, true);
                    return encoding.GetString(stream.ToArray());
                }
                catch (Exception ex)
                {
                    ExceptionOccurred?.Invoke("TryUtf32Encoding", new EncodingExceptionEventArgs {Exception = ex});

                    // failed..
                    encoding = null;
                    return null;
                }
            }

            // the user doesn't want this detection..
            if (FormSettings.Settings.SkipUtf32Le && !bigEndian ||
                FormSettings.Settings.SkipUtf32Be && bigEndian)
            {
                // ..so just return..
                encoding = null;
                return null;
            }

            try // there is no BOM..
            {
                // get the contents of the memory stream into a list of bytes..
                List<byte> bytes = new List<byte>(stream.ToArray());

                // insert the BOM..
                //bytes.InsertRange(0, bigEndian ? Utf32BigEndianBom : Utf32LittleEndianBom);

                // try the UTF32 encoding with the BOM..
                encoding = new UTF32Encoding(bigEndian, false, true);
                noBom = true;
                return encoding.GetString(bytes.ToArray());
            }
            catch (Exception ex)
            {
                ExceptionOccurred?.Invoke("TryUtf32Encoding", new EncodingExceptionEventArgs {Exception = ex});

                // failed..
                encoding = null;
                return null;
            }
        }
    }
}
