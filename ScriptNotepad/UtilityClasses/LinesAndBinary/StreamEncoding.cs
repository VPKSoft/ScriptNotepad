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

using System.IO;
using System.Text;

namespace ScriptNotepad.UtilityClasses.LinesAndBinary
{
    /// <summary>
    /// A class for encoding detection of a stream or byte buffer.
    /// <note type="note">Based on the MSDN article: https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader.currentencoding. </note>
    /// </summary>
    public static class StreamEncoding
    {
        /// <summary>
        /// Gets or sets a value of how many characters to peek from the stream.
        /// </summary>
        public static int StreamPeekCount { get; set; } = 16;

        /// <summary>
        /// Gets the encoding of a byte buffer.
        /// </summary>
        /// <param name="buffer">The byte buffer to get the encoding from.</param>
        /// <param name="useBOM">A flag indicating whether to use byte order marks at the beginning of the stream to detect the encoding.</param>
        /// <returns>The current encoding detected using a <see cref="StreamReader"/> class.</returns>
        public static System.Text.Encoding GetEncoding(byte[] buffer, bool useBOM)
        {
            // this memory stream will be disposed..
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                // get the encoding with the given parameters from the byte buffer..
                return GetEncoding(memoryStream, useBOM);
            }
        }

        /// <summary>
        /// Gets the encoding of stream.
        /// <note type="note">The stream will not be disposed of.</note>
        /// </summary>
        /// <param name="stream">A stream to get the encoding from.</param>
        /// <param name="useBOM">A flag indicating whether to use byte order marks at the beginning of the stream to detect the encoding.</param>
        /// <returns>The current encoding detected using a <see cref="StreamReader"/> class.</returns>
        public static System.Text.Encoding GetEncoding(Stream stream, bool useBOM)
        {
            // save the current stream position..
            long saveStreamPos = stream.Position; 

            // set the stream position to the start (0)..
            stream.Position = 0;

            // use a stream reader with default parameters to detect the stream's encoding,
            // the only variable is the value whether to use byte order marks
            // at the beginning of the stream to detect the encoding..
            using (StreamReader streamReader = new StreamReader(stream, System.Text.Encoding.UTF8, useBOM, 1024, true))
            {
                // lets peed for about 24 bytes..
                int iCountPeeks = 0;

                // peek the stream from 0 to StreamPeekCount property value..
                while (streamReader.Peek() >= 0 && iCountPeeks < StreamPeekCount)
                {
                    // count the peeks..
                    iCountPeeks++;
                }

                // set the stream's position to the previously saved position..
                stream.Position = saveStreamPos;

                // return the detected encoding..
                return streamReader.CurrentEncoding;
            }
        }
    }
}
