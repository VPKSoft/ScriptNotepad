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

using System.Reflection;

namespace ScriptNotepad.UtilityClasses.Encodings
{
    /// <summary>
    /// A helper class for the <see cref="Encoding"/> class.
    /// </summary>
    public static class EncodingHelpers
    {
        /// <summary>
        /// Gets a value indicating whether the encoding uses identifier / byte-order-mark.
        /// </summary>
        /// <param name="encoding">The encoding from which to get the value from.</param>
        /// <returns><c>true</c> if the encoding uses identifier / byte-order-mark, <c>false</c> otherwise.</returns>
        public static bool EmitsBom(this Encoding encoding)
        {
            // special case for UTF8; use reflection..
            if (encoding is UTF8Encoding)
            {
                FieldInfo infoEmitUtf8Identifier = encoding.GetType().GetField("emitUTF8Identifier",
                    BindingFlags.Instance | BindingFlags.NonPublic);

                return infoEmitUtf8Identifier != null && (bool) infoEmitUtf8Identifier.GetValue(encoding);
            }            

            // special case for Unicode; use reflection..
            if (encoding is UnicodeEncoding)
            {
                FieldInfo infoByteOrderMark = encoding.GetType()
                    .GetField("byteOrderMark", BindingFlags.Instance | BindingFlags.NonPublic);


                return infoByteOrderMark != null && (bool) infoByteOrderMark.GetValue(encoding);
            }

            // special case for UTF32; use reflection..
            if (encoding is UTF32Encoding)
            {
                FieldInfo infoEmitUtf32ByteOrderMark = encoding.GetType().GetField("emitUTF32ByteOrderMark",
                    BindingFlags.Instance | BindingFlags.NonPublic);

                return infoEmitUtf32ByteOrderMark != null &&
                                              (bool) infoEmitUtf32ByteOrderMark.GetValue(encoding);
            }

            return false;
        }

        /// <summary>
        /// Determines whether the encoding is big endian.
        /// </summary>
        /// <param name="encoding">The encoding to check for little/big endian..</param>
        /// <returns><c>true</c> if the encoding is big endian; otherwise, <c>false</c>.</returns>
        public static bool IsBigEndian(this Encoding encoding)
        {
            // special case for Unicode; use reflection..
            if (encoding is UnicodeEncoding)
            {
                FieldInfo infoBigEndian = encoding.GetType()
                    .GetField("bigEndian", BindingFlags.Instance | BindingFlags.NonPublic);


                return infoBigEndian != null && (bool) infoBigEndian.GetValue(encoding);
            }

            // special case for UTF32; use reflection..
            if (encoding is UTF32Encoding)
            {
                FieldInfo infoBigEndian = encoding.GetType()
                    .GetField("bigEndian", BindingFlags.Instance | BindingFlags.NonPublic);


                return infoBigEndian != null && (bool) infoBigEndian.GetValue(encoding);
            }

            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the Encoding raises an exception in case of an invalid character.
        /// </summary>
        /// <param name="encoding">The encoding to check the value for.</param>
        /// <returns><c>true</c> if the Encoding raises an exception in case of an invalid character, <c>false</c> otherwise.</returns>
        public static bool ThrowOnInvalidCharacters(this Encoding encoding)
        {
            // special case for UTF8; use reflection..
            if (encoding is UTF8Encoding)
            {
                FieldInfo infoIsThrowException = encoding.GetType().GetField("isThrowException",
                    BindingFlags.Instance | BindingFlags.NonPublic);


                return infoIsThrowException != null && (bool) infoIsThrowException.GetValue(encoding);
            }

            // special case for Unicode; use reflection..
            if (encoding is UnicodeEncoding)
            {
                FieldInfo infoIsThrowException = encoding.GetType().GetField("isThrowException",
                    BindingFlags.Instance | BindingFlags.NonPublic);

                return 
                    infoIsThrowException != null && (bool) infoIsThrowException.GetValue(encoding);
            }

            // special case for UTF32; use reflection..
            if (encoding is UTF32Encoding)
            {
                FieldInfo infoIsThrowException = encoding.GetType().GetField("isThrowException",
                    BindingFlags.Instance | BindingFlags.NonPublic);

                return
                    infoIsThrowException != null && (bool) infoIsThrowException.GetValue(encoding);
            }

            return false;
        }
    }
}
