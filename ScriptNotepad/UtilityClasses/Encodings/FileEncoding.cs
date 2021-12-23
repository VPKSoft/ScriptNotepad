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

using System.Linq;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Editor.EntityHelpers;
using ScriptNotepad.Settings;
using VPKSoft.ErrorLogger;
using static ScriptNotepad.UtilityClasses.Encodings.TryMakeEncoding;

namespace ScriptNotepad.UtilityClasses.Encodings
{
    /// <summary>
    /// A class to help with file encoding within the software.
    /// </summary>
    public class FileEncoding
    {
        /// <summary>
        /// Gets the file encoding.
        /// </summary>
        /// <param name="sessionName">Name of the session.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="encoding">The default encoding for the file.</param>
        /// <param name="reloadContents">if set to <c>true</c> the file contents should be reloaded from the file system.</param>
        /// <param name="encodingOverridden">if set to <c>true</c> the encoding should be reassigned.</param>
        /// <param name="overrideDetectBom">if set to <c>true</c> the setting value whether to detect unicode file with no byte-order-mark (BOM) is overridden.</param>
        /// <param name="noBom">A value indicating if the encoding is reconstructed and unicode is used, should the byte-order-mark be excluded from encoding.</param>
        /// <param name="bigEndian">A value indicating whether to use big-endian or little-endian byte order with unicode encoding.</param>
        /// <param name="existsInDatabase">A value indicating whether a snapshot of the file exists in the database.</param>
        /// <returns>Encoding.</returns>
        public static Encoding GetFileEncoding(string sessionName, string fileName, Encoding encoding, bool reloadContents,
            bool encodingOverridden, bool overrideDetectBom, out bool noBom, out bool bigEndian, out bool existsInDatabase)
        {
            try
            {
                // the encoding shouldn't change based on the file's contents if a snapshot of the file already exists in the database..
                existsInDatabase = ScriptNotepadDbContext.DbContext.FileSaves.Any(f => f.Session.SessionName == sessionName && f.FileNameFull == fileName);

                noBom = false;
                bigEndian = false;

                if (FormSettings.Settings.AutoDetectEncoding && !encodingOverridden &&
                    (!existsInDatabase || reloadContents))
                {
                    using FileStream fileStream = File.OpenRead(fileName);
                    encoding = DetectEncoding.FromStream(fileStream);
                    return encoding;
                }

                if ((FormSettings.Settings.DetectNoBom || overrideDetectBom) && !encodingOverridden &&
                    (!existsInDatabase || reloadContents))
                {
                    string contents = TryEncodings(fileName, out var detectedEncoding, out bigEndian, out noBom);

                    if (contents != null)
                    {
                        encoding = detectedEncoding;
                        return encoding;
                    }
                }

                if (existsInDatabase && !reloadContents && !encodingOverridden && !File.Exists(fileName))
                {
                    encoding = ScriptNotepadDbContext.DbContext.FileSaves.FirstOrDefault(f => f.Session.SessionName == sessionName && f.FileNameFull == fileName)?.GetEncoding();
                    return encoding;
                }

                // the last check..
                if (File.Exists(fileName))
                {
                    File.ReadAllText(fileName, encoding);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError(ex);
                existsInDatabase = false;
                noBom = false;
                bigEndian = false;
                encoding = null;
            }

            return encoding;
        }
    }
}
