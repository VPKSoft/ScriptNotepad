#region License
/*
MIT License

Copyright(c) 2021 Petteri Kautonen

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

#nullable enable
using System;
using System.IO;
using System.Text;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.UtilityClasses.Encodings;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.Database.Entity.EntityHelpers
{
    /// <summary>
    /// Helper methods for the <see cref="FileSave"/> entity.
    /// </summary>
    public static class FileSaveHelper
    {
        /// <summary>
        /// Gets the encoding of the file save.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <returns>The encoding used in the file save.</returns>
        public static Encoding GetEncoding(this FileSave fileSave)
        {
            return EncodingData.EncodingFromString(fileSave.EncodingAsString);
        }

        /// <summary>
        /// Sets the encoding for the file save.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <param name="encoding">The encoding.</param>
        public static void SetEncoding(this FileSave fileSave, Encoding encoding)
        {
            fileSave.EncodingAsString = EncodingData.EncodingToString(encoding);
        }

        /// <summary>
        /// Restores the previous time stamp for the <see cref="FileSave.DatabaseModified"/> property value.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        public static void PopPreviousDbModified(this FileSave fileSave)
        {
            fileSave.DatabaseModified = fileSave.PreviousDbModified;
        }

        /// <summary>
        /// Sets the random file name for this <see cref="FileSave"/> instance to to be used as a file system cache for changed file contents. The <see cref="TemporaryFileSaveName"/> must be true for this to work.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <returns>System.String.</returns>
        public static string? SetRandomFile(this FileSave fileSave)
        {
            if (fileSave.TemporaryFileSaveName == null && fileSave.UseFileSystemOnContents == true)
            {
                fileSave.Session.SetRandomPath();
                fileSave.TemporaryFileSaveName = Path.Combine(fileSave.Session.TemporaryFilePath ?? string.Empty, Path.GetRandomFileName());
            }
            else if (!fileSave.UseFileSystemOnContents == false)
            {
                fileSave.TemporaryFileSaveName = null;
            }

            return fileSave.TemporaryFileSaveName;
        }

        /// <summary>
        /// Sets the file contents of this <see cref="FileSave"/> class instance.
        /// The contents are either saved to the file system or to the database depending on the <see cref="UseFileSystemOnContents"/> property value.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <param name="fileContents">The file contents.</param>
        /// <param name="commit">A value indicating whether to commit the changes to the
        /// database or to the file system cache depending on the <see cref="UseFileSystemOnContents"/> property value.</param>
        /// <param name="saveToFileSystem">A value indicating whether to override existing copy of the file in the file system.</param>
        /// <param name="contentChanged">A value indicating whether the file contents have been changed.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool SetFileContents(this FileSave fileSave, byte[] fileContents, bool commit, bool saveToFileSystem, 
            bool contentChanged)
        {
            try
            {
                fileSave.FileContents = fileContents;

                if (fileSave.UseFileSystemOnContents == true)
                {
                    if (commit && !saveToFileSystem)
                    {
                        var randomFile = fileSave.SetRandomFile();
                        if (randomFile != null)
                        {
                            File.WriteAllBytes(randomFile, fileContents);
                        }
                    }

                    if (saveToFileSystem)
                    {
                        var randomFile = fileSave.SetRandomFile();
                        if (randomFile != null)
                        {
                            File.WriteAllBytes(randomFile, fileContents);
                        }

                        if (fileSave.ExistsInFileSystem)
                        {
                            File.WriteAllBytes(fileSave.FileNameFull, fileContents);
                            fileSave.FileSystemSaved = DateTime.Now;
                        }
                    }
                }
                else
                {
                    fileSave.FileContents = fileContents;
                }

                if (contentChanged)
                {
                    fileSave.DatabaseModified = DateTime.Now;
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
                return false;
            }
        }

        /// <summary>
        /// Gets the cached file contents of this <see cref="FileSave"/> class instance.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <returns>A byte array with the file contents.</returns>
        public static byte[]? GetFileContents(this FileSave fileSave)
        {
            try
            {
                if (fileSave.UseFileSystemOnContents == true && fileSave.TemporaryFileSaveName != null)
                {
                    return File.ReadAllBytes(fileSave.TemporaryFileSaveName);
                }

                return fileSave.FileContents;
            }
            catch (Exception ex)
            {
                ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Undoes the encoding change.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        public static void UndoEncodingChange(this FileSave fileSave)
        {
            // only if there exists a previous encoding..
            if (fileSave.PreviousEncodings.Count > 0)
            {
                // get the last index of the list..
                int idx = fileSave.PreviousEncodings.Count - 1;

                // set the previous encoding value..
                fileSave.SetEncoding(fileSave.PreviousEncodings[idx]);

                // remove the last encoding from the list..
                fileSave.PreviousEncodings.RemoveAt(idx);
            }
        }
    }
}
