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
using System.Reflection;
using System.Text;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.UtilityClasses.Encodings
{
    /// <summary>
    /// A class to get <see cref="Encoding"/> data as a string format and vice versa.
    /// </summary>
    public class EncodingData: ErrorHandlingBase
    {
        /// <summary>
        /// Converts to given encoding to a semicolon-delimited string containing the web name, code page, byte order mark,
        /// little/big endian and a value whether the encoding will throw an exception on invalid characters.
        /// I.e: utf-32BE;True;True;True.
        /// </summary>
        /// <param name="encoding">The encoding of which data to parse into a string.</param>
        /// <returns>A string representing the the <see cref="Encoding"/>.</returns>
        public static string EncodingToString(Encoding encoding)
        {
            // avoid null reference exception..
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }

            try
            {
                // special case for UTF8; use reflection..
                if (encoding is UTF8Encoding)
                {
                    FieldInfo infoEmitUtf8Identifier = encoding.GetType().GetField("emitUTF8Identifier",
                        BindingFlags.Instance | BindingFlags.NonPublic);
                    FieldInfo infoIsThrowException = encoding.GetType().GetField("isThrowException",
                        BindingFlags.Instance | BindingFlags.NonPublic);

                    bool emitUtf8 = infoEmitUtf8Identifier != null && (bool) infoEmitUtf8Identifier.GetValue(encoding);

                    bool throwInvalidChars =
                        infoIsThrowException != null && (bool) infoIsThrowException.GetValue(encoding);

                    return encoding.WebName + $";{encoding.CodePage};{emitUtf8};{false};{throwInvalidChars}";
                }

                // special case for Unicode; use reflection..
                if (encoding is UnicodeEncoding)
                {
                    FieldInfo infoByteOrderMark = encoding.GetType()
                        .GetField("byteOrderMark", BindingFlags.Instance | BindingFlags.NonPublic);

                    FieldInfo infoIsThrowException = encoding.GetType().GetField("isThrowException",
                        BindingFlags.Instance | BindingFlags.NonPublic);

                    FieldInfo infoBigEndian = encoding.GetType()
                        .GetField("bigEndian", BindingFlags.Instance | BindingFlags.NonPublic);

                    bool byteOrderMark = infoByteOrderMark != null && (bool) infoByteOrderMark.GetValue(encoding);

                    bool throwInvalidChars =
                        infoIsThrowException != null && (bool) infoIsThrowException.GetValue(encoding);

                    bool bigEndian = infoBigEndian != null && (bool) infoBigEndian.GetValue(encoding);

                    return encoding.WebName + $";{encoding.CodePage};{byteOrderMark};{bigEndian};{throwInvalidChars}";
                }

                // special case for UTF32; use reflection..
                if (encoding is UTF32Encoding)
                {
                    FieldInfo infoEmitUtf32ByteOrderMark = encoding.GetType().GetField("emitUTF32ByteOrderMark",
                        BindingFlags.Instance | BindingFlags.NonPublic);
                    FieldInfo infoIsThrowException = encoding.GetType().GetField("isThrowException",
                        BindingFlags.Instance | BindingFlags.NonPublic);
                    FieldInfo infoBigEndian = encoding.GetType()
                        .GetField("bigEndian", BindingFlags.Instance | BindingFlags.NonPublic);

                    bool emitUtf32ByteOrderMark = infoEmitUtf32ByteOrderMark != null &&
                        (bool) infoEmitUtf32ByteOrderMark.GetValue(encoding);

                    bool throwInvalidChars =
                        infoIsThrowException != null && (bool) infoIsThrowException.GetValue(encoding);

                    bool bigEndian = infoBigEndian != null && (bool) infoBigEndian.GetValue(encoding);

                    return encoding.WebName +
                           $";{encoding.CodePage};{emitUtf32ByteOrderMark};{bigEndian};{throwInvalidChars}";
                }
            }
            catch
            {
                encoding = Encoding.Default;
            }

            // the normal case.. there are no byte order marks, etc..
            return encoding.WebName + $";{encoding.CodePage};{false};{false};{false}";
        }

        /// <summary>
        /// Gets an <see cref="Encoding"/> class instance from string describing the encoding.
        /// </summary>
        /// <param name="encodingString">The string describing the <see cref="Encoding"/>. I.e: utf-32BE;True;True;True.</param>
        /// <returns>Returns the <see cref="Encoding"/> created from the given string instance.</returns>
        public static Encoding EncodingFromString(string encodingString)
        {
            try
            {
                string[] encodingValues = encodingString.Split(';');

                string webName = encodingValues.Length >= 1 ? encodingValues[0] : Encoding.Default.WebName;
                bool byteOrderMark = encodingValues.Length >= 3 && bool.Parse(encodingValues[2]);
                bool throwInvalidChars = encodingValues.Length >= 5 && bool.Parse(encodingValues[4]);

                if (webName == "utf-8")
                {
                    return new UTF8Encoding(byteOrderMark, throwInvalidChars);
                }

                if (webName == "utf-16" || webName == "utf-16BE")
                {
                    return new UnicodeEncoding(webName == "utf-16BE", byteOrderMark, throwInvalidChars);
                }

                if (webName == "utf-32" || webName == "utf-32BE")
                {
                    return new UTF32Encoding(webName == "utf-32BE", byteOrderMark, throwInvalidChars);
                }

                try
                {
                    return Encoding.GetEncoding(webName);
                }
                catch (Exception ex)
                {
                    ExceptionLogAction?.Invoke(ex);
                    try
                    {
                        return CodePagesEncodingProvider.Instance.GetEncoding(webName);
                    }
                    catch (Exception exInner)
                    {
                        ExceptionLogAction?.Invoke(exInner);
                        return Encoding.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogAction?.Invoke(ex);
                return Encoding.Default;
            }
        }
    }
}
