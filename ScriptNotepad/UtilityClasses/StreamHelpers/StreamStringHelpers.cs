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
