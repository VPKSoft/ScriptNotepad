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
        /// <returns>An instance to a MemoryStream class containing the given text.</returns>
        public static MemoryStream TextToMemoryStream(string text)
        {
            MemoryStream result;
            byte[] streamContents;

            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter streamWriter = new StreamWriter(ms);
                {
                    streamWriter.Write(text.ToArray());
                    streamWriter.Flush();
                    streamContents = ms.ToArray();
                }
            }

            result = new MemoryStream(streamContents)
            {
                Position = 0
            };
            return result;
        }

        /// <summary>
        /// Returns the contents of a give <paramref name="memoryStream"/> as a string.
        /// </summary>
        /// <param name="memoryStream">The memory stream which contents to be returned as a string.</param>
        /// <returns></returns>
        public static string MemoryStreamToText(ref MemoryStream memoryStream)
        {
            byte[] streamContents = memoryStream.ToArray();
            string result = string.Empty;
            using (memoryStream)
            {
                using (StreamReader streamReader = new StreamReader(memoryStream))
                {
                    memoryStream.Position = 0;
                    result = streamReader.ReadToEnd();
                }
            }

            memoryStream = new MemoryStream(streamContents)
            {
                Position = 0
            };
            return result;
        }
    }
}
