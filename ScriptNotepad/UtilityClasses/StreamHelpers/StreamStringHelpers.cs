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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepad.UtilityClasses.StreamHelpers
{
    /// <summary>
    /// A class to help with MemoryStreams and strings.
    /// </summary>
    public static class StreamStringHelpers
    {
        /// <summary>
        /// Saves a given text to a MemoryStream.
        /// </summary>
        /// <param name="text">The text to be saved to a MemoryStream.</param>
        /// <param name="encoding">An encoding to be used with the used with the memory stream conversion.</param>
        /// <returns>An instance to a MemoryStream class containing the given text.</returns>
        public static MemoryStream TextToMemoryStream(string text, System.Text.Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(text).ToArray());
        }

        /// <summary>
        /// Returns the contents of a give <paramref name="memoryStream"/> as a string.
        /// </summary>
        /// <param name="memoryStream">The memory stream which contents to be returned as a string.</param>
        /// <param name="encoding">The encoding to be used to convert a memory stream to text.</param>
        /// <returns></returns>
        public static string MemoryStreamToText(MemoryStream memoryStream, System.Text.Encoding encoding)
        {
            return encoding.GetString(memoryStream.ToArray());
        }

        /// <summary>
        /// Converts the encoding of a given string.
        /// </summary>
        /// <param name="encodingFrom">The encoding to convert from.</param>
        /// <param name="encodingTo">The encoding to convert to.</param>
        /// <param name="contents">The contents which encoding should be changed.</param>
        /// <returns>A string converted to the encoding <paramref name="encodingTo"/>.</returns>
        public static string ConvertEncoding(System.Text.Encoding encodingFrom, System.Text.Encoding encodingTo, string contents)
        {
            byte[] bytes = encodingFrom.GetBytes(contents);
            bytes = System.Text.Encoding.Convert(encodingFrom, encodingTo, bytes);
            return encodingTo.GetString(bytes);
        }
    }
}
