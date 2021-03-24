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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Editor.EntityHelpers.DataHolders;
using ScriptNotepad.UtilityClasses.Encodings;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.LinesAndBinary;
using VPKSoft.LangLib;

namespace ScriptNotepad.Editor.EntityHelpers
{
    /// <summary>
    /// Helper methods for the <see cref="FileSave"/> entity.
    /// </summary>
    public static class FileSaveHelper
    {
        /// <summary>
        /// Gets the data indexer fot the <see cref="FileSaveData"/> instances.
        /// </summary>
        /// <value>The data indexer fot the <see cref="FileSaveData"/> instances.</value>
        public static DataHolderIndexer<FileSaveData> DataIndexer { get; } = new();

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
            fileSave.DatabaseModified = fileSave.GetPreviousDbModified();
        }

        /// <summary>
        /// Sets the random file name for this <see cref="FileSave"/> instance to to be used as a file system cache for changed file contents. The <see cref="FileSave.TemporaryFileSaveName"/> must be true for this to work.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <returns>System.String.</returns>
        public static string? SetRandomFile(this FileSave fileSave)
        {
            if (fileSave.TemporaryFileSaveName == null && fileSave.UseFileSystemOnContents == true)
            {
                fileSave.Session.SetRandomPath();
                fileSave.TemporaryFileSaveName = Path.Combine(fileSave.Session.TemporaryFilePath ?? string.Empty,
                    Path.GetRandomFileName());
            }
            else if (!fileSave.UseFileSystemOnContents == false)
            {
                fileSave.TemporaryFileSaveName = null;
            }

            return fileSave.TemporaryFileSaveName;
        }

        /// <summary>
        /// Sets the file contents of this <see cref="FileSave"/> class instance.
        /// The contents are either saved to the file system or to the database depending on the <see cref="FileSave.UseFileSystemOnContents"/> property value.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <param name="fileContents">The file contents.</param>
        /// <param name="commit">A value indicating whether to commit the changes to the
        /// database or to the file system cache depending on the <see cref="FileSave.UseFileSystemOnContents"/> property value.</param>
        /// <param name="saveToFileSystem">A value indicating whether to override existing copy of the file in the file system.</param>
        /// <param name="contentChanged">A value indicating whether the file contents have been changed.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool SetFileContents(this FileSave fileSave, byte[] fileContents, bool commit,
            bool saveToFileSystem,
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
            if (DataIndexer[fileSave.Id].PreviousEncodings.Count > 0)
            {
                // get the last index of the list..
                int idx = DataIndexer[fileSave.Id].PreviousEncodings.Count - 1;

                // set the previous encoding value..
                fileSave.SetEncoding(DataIndexer[fileSave.Id].PreviousEncodings[idx]);

                // remove the last encoding from the list..
                DataIndexer[fileSave.Id].PreviousEncodings.RemoveAt(idx);
            }
        }

        /// <summary>
        /// Resets the previous database modified property, so it can be set again.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        public static void ResetPreviousDbModified(this FileSave fileSave)
        {
            DataIndexer[fileSave.Id].PreviousDbModifiedIsSet = false;
            DataIndexer[fileSave.Id].PreviousDbModified = DateTime.MinValue;
        }

        /// <summary>
        /// Sets the value indicating when the file was previously modified in the database.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <param name="value">The <see cref="DateTime"/> to set as previously modified time.</param>
        public static void SetPreviousDbModified(this FileSave fileSave, DateTime value)
        {
            if (DataIndexer[fileSave.Id].PreviousDbModified.CompareTo(value) != 0 && !DataIndexer[fileSave.Id].PreviousDbModifiedIsSet)
            {
                DataIndexer[fileSave.Id].PreviousDbModifiedIsSet = true;
                DataIndexer[fileSave.Id].PreviousDbModified = value;
            }
        }

        /// <summary>
        /// Gets the value indicating when the file was previously modified in the database.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        public static DateTime GetPreviousDbModified(this FileSave fileSave)
        {
            return DataIndexer[fileSave.Id].PreviousDbModified;
        }

        /// <summary>
        /// Gets the file contents as a memory stream.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <returns>The file contents as a memory stream.</returns>
        public static MemoryStream GetFileContentsAsMemoryStream(this FileSave fileSave)
        {
            var fileContents = fileSave.GetFileContents();
            if (fileContents == null || fileContents.Length == 0)
            {
                return new MemoryStream();
            }

            return new MemoryStream(fileContents);
        }

        /// <summary>
        /// Gets the file contents as a memory stream.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <param name="value">The file's contents in a memory stream.</param>
        /// <returns>The file contents as a memory stream.</returns>
        public static void SetFileContentsAsMemoryStream(this FileSave fileSave, MemoryStream value)
        {
            fileSave.SetFileContents(value.ToArray(), true, false, false);
        }

        /// <summary>
        /// Gets a value indicating whether the user should be queried of to reload the changed document from the file system.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <returns>The value indicating whether the user should be queried of to reload the changed document from the file system.</returns>
        public static bool GetShouldQueryDiskReload(this FileSave fileSave)
        {
            var fileSysModified = new FileInfo(fileSave.FileNameFull).LastWriteTime;

            // get the last time the file was written into..
            DateTime dtUpdated = fileSysModified;

            // get the result to be returned..
            bool result = DataIndexer[fileSave.Id].ShouldQueryDiskReload && dtUpdated > fileSave.FileSystemModified;

            return result;
        }

        /// <summary>
        /// Sets a value indicating whether the user should be queried of to reload the changed document from the file system.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <param name="value">The value indicating whether the user should be queried of to reload the changed document from the file system.</param>
        public static void SetShouldQueryDiskReload(this FileSave fileSave, bool value)
        {
            DataIndexer[fileSave.Id].ShouldQueryDiskReload = value;
        }

        /// <summary>
        /// Gets a value indicating whether a software should query the user if the deleted file should be kept in the editor.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <remarks>A value indicating whether a software should query the user if the deleted file should be kept in the editor.</remarks>
        public static bool ShouldQueryKeepFile(this FileSave fileSave)
        {
            return fileSave.ExistsInFileSystem && !File.Exists(fileSave.FileNameFull);
        }

        /// <summary>
        /// Gets a value indicating whether a software should query the user if a file reappeared in the file system should be reloaded.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <returns>A value indicating whether a software should query the user if a file reappeared in the file system should be reloaded.</returns>
        public static bool ShouldQueryFileReappeared(this FileSave fileSave)
        {
            return !fileSave.ExistsInFileSystem && File.Exists(fileSave.FileNameFull);
        }

        /// <summary>
        /// Gets a value indicating whether the document is changed in the editor versus the file system.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <returns>A value indicating whether the document is changed in the editor versus the file system.</returns>
        public static bool IsChangedInEditor(this FileSave fileSave)
        {
            return fileSave.ExistsInFileSystem && fileSave.DatabaseModified > fileSave.FileSystemModified;
        }

        /// <summary>
        /// Adds a previous encoding to the collection for undo possibility.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <param name="encoding">The encoding to add.</param>
        public static void AddPreviousEncoding(this FileSave fileSave, Encoding encoding)
        {
            DataIndexer[fileSave.Id].PreviousEncodings.Add(encoding);
        }

        /// <summary>
        /// Clears the previous encodings data from the <see cref="FileSave"/>.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        public static void ClearPreviousEncodings(this FileSave fileSave)
        {
            DataIndexer[fileSave.Id].PreviousEncodings.Clear();
        }

        /// <summary>
        /// Gets the file line types and their descriptions.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <returns>File line types and their descriptions.</returns>
        public static IEnumerable<KeyValuePair<FileLineTypes, string>> GetFileLineTypes(this FileSave fileSave)
        {
            if (fileSave.FileContents == null)
            {
                var fileLineTypes =
                    ScriptNotepad.UtilityClasses.LinesAndBinary.FileLineType.GetFileLineTypes(fileSave.FileContents);

                var lineTypesInternal = fileLineTypes as KeyValuePair<FileLineTypes, string>[] ??
                                        fileLineTypes.ToArray();

                DataIndexer[fileSave.Id].FileLineTypesInternal = lineTypesInternal;

                return lineTypesInternal;
            }

            return DataIndexer[fileSave.Id].FileLineTypesInternal;
        }

        /// <summary>
        /// Gets the type of the file line ending.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <returns>The type of the file line ending.</returns>
        public static FileLineTypes GetFileLineType(this FileSave fileSave)
        {
            List<KeyValuePair<FileLineTypes, string>> typesList =
                new(fileSave.GetFileLineTypes().ToArray());

            if (typesList.Count == 0 ||
                typesList.Count == 1 && typesList[0].Key.HasFlag(FileLineTypes.Mixed))
            {
                return FileLineTypes.CRLF;
            }

            if (typesList.Count == 1)
            {
                return typesList[0].Key;
            }

            return typesList.FirstOrDefault().Key;
        }

        /// <summary>
        /// Gets the text describing the file line ending type(s) of the document.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> instance.</param>
        /// <returns>The text describing the file line ending type(s) of the document.</returns>
        public static string FileLineEndingText(this FileSave fileSave)
        {
            if (string.IsNullOrEmpty(DataIndexer[fileSave.Id].FileEndingText))
            {
                DataIndexer[fileSave.Id].FileEndingText = DBLangEngine.GetStatMessage("msgLineEndingShort",
                    "LE: |A short message indicating a file line ending type value(s) as a concatenated text");


                var fileLineTypes = fileSave.GetFileLineTypes();

                string endAppend = string.Empty;

                foreach (var fileLineType in fileLineTypes)
                {
                    if (!fileLineType.Key.HasFlag(FileLineTypes.Mixed))
                    {
                        DataIndexer[fileSave.Id].FileEndingText += fileLineType.Value + ", ";
                    }
                    else
                    {
                        endAppend = $" ({fileLineType.Value})";
                    }

                    DataIndexer[fileSave.Id].FileEndingText =
                        DataIndexer[fileSave.Id].FileEndingText.TrimEnd(',', ' ') + endAppend;
                }
            }

            return DataIndexer[fileSave.Id].FileEndingText;
        }

        /// <summary>
        /// Sets the database modified property value along with the <see cref="FileSaveData.PreviousDbModified"/> property value.
        /// </summary>
        /// <param name="fileSave">The file save.</param>
        /// <param name="value">The value.</param>
        public static void SetDatabaseModified(this FileSave fileSave, DateTime value)
        {
            DataIndexer[fileSave.Id].PreviousDbModified = fileSave.DatabaseModified;
            fileSave.DatabaseModified = value;
        }
    }
}
