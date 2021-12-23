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
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Editor.EntityHelpers;
using VPKSoft.ScintillaTabbedTextControl;

namespace ScriptNotepad.Editor.Utility.ModelHelpers
{
    /// <summary>
    /// A class to help with <see cref="FileSave"/> entities.
    /// </summary>
    public static class FileSaveHelper
    {
        /// <summary>
        /// Adds the or update file.
        /// </summary>
        /// <param name="fileSave">A <see cref="FileSave"/> class instance to be added or updated into the database.</param>
        /// <param name="document">An instance to a ScintillaTabbedDocument class.</param>
        /// <param name="commit">A value indicating whether to commit the changes to the
        /// database or to the file system cache depending on the setting.</param>
        /// <param name="saveToFileSystem">A value indicating whether to override existing copy of the file in the file system.</param>
        /// <param name="contentChanged">A value indicating whether the file contents have been changed.</param>
        /// <returns>An instance to a <see cref="FileSave"/> modified class.</returns>
        public static FileSave AddOrUpdateFile(this FileSave fileSave, ScintillaTabbedDocument document, bool commit,
            bool saveToFileSystem, bool contentChanged)
        {
            fileSave.SetFileContents(fileSave.GetEncoding().GetBytes(document.Scintilla.Text), commit, saveToFileSystem, contentChanged);
            fileSave.CurrentCaretPosition = document.Scintilla.CurrentPosition;
            fileSave.FilePath = Path.GetDirectoryName(fileSave.FileNameFull);
            ScriptNotepadDbContext.DbContext.SaveChanges();

            if (!ScriptNotepadDbContext.DbContext.FileSaves.Any(f => f.Id == fileSave.Id))
            {
                return ScriptNotepadDbContext.DbContext.FileSaves.Add(fileSave).Entity;
            }

            return ScriptNotepadDbContext.DbContext.FileSaves.FirstOrDefault(f => f.Id == fileSave.Id);
        }

        /// <summary>
        /// Sets the contents of the <see cref="FileSave"/> class instance.
        /// </summary>
        /// <param name="fileSave">The file save of which contents to set.</param>
        /// <param name="contents">The contents as a string.</param>
        /// <param name="commit">A value indicating whether to commit the changes to the
        /// database or to the file system cache depending on the setting.</param>
        /// <param name="saveToFileSystem">A value indicating whether to override existing copy of the file in the file system.</param>
        /// <param name="contentChanged">A value indicating whether the file contents have been changed.</param>
        /// <returns>An instance to a <see cref="FileSave"/> modified class.</returns>
        public static FileSave SetContents(this FileSave fileSave, string contents, bool commit,
            bool saveToFileSystem, bool contentChanged)
        {
            fileSave.SetFileContents(fileSave.GetEncoding().GetBytes(contents), commit, saveToFileSystem, contentChanged);

            return fileSave;
        }

        /// <summary>
        /// Adds the or update file.
        /// </summary>
        /// <param name="fileSave">The file save.</param>
        /// <returns>An instance to a <see cref="FileSave"/> modified class.</returns>
        public static FileSave AddOrUpdateFile(this FileSave fileSave)
        {
            ScriptNotepadDbContext.DbContext.SaveChanges();
            return ScriptNotepadDbContext.DbContext.FileSaves.FirstOrDefault(f => f.Id == fileSave.Id);
        }

        /// <summary>
        /// Updates the file data of the <see cref="FileSave"/> class instance with a given full file name.
        /// </summary>
        /// <param name="fileSave">The file save.</param>
        /// <param name="fileNameFull">The full file name.</param>
        /// <returns>An instance to a <see cref="FileSave"/> modified class.</returns>
        public static FileSave UpdateFileData(this FileSave fileSave, string fileNameFull)
        {
            fileSave.FileName = Path.GetFileName(fileNameFull);
            fileSave.FileNameFull = fileNameFull;
            fileSave.FilePath = Path.GetDirectoryName(fileNameFull);
            return fileSave;
        }

        /// <summary>
        /// Adds the or update file.
        /// </summary>
        /// <param name="fileSave">A <see cref="FileSave"/> class instance to be added or updated into the database.</param>
        /// <param name="document">An instance to a ScintillaTabbedDocument class.</param>
        /// <param name="isHistory">if set to <c>true</c> the file is to be considered as a closed/history file.</param>
        /// <param name="sessionName">Name of the session the file belongs to.</param>
        /// <param name="encoding">The encoding of the file.</param>
        /// <param name="commit">A value indicating whether to commit the changes to the
        /// database or to the file system cache depending on the setting.</param>
        /// <param name="saveToFileSystem">A value indicating whether to override existing copy of the file in the file system.</param>
        /// <param name="contentChanged">A value indicating whether the file contents have been changed.</param>
        /// <returns>An instance to a <see cref="FileSave"/> modified class.</returns>
        public static FileSave AddOrUpdateFile(this FileSave fileSave, ScintillaTabbedDocument document, bool isHistory,
            string sessionName, Encoding encoding, bool commit,
            bool saveToFileSystem, bool contentChanged)
        {
            fileSave.SetFileContents(fileSave.GetEncoding().GetBytes(document.Scintilla.Text), commit, saveToFileSystem, contentChanged);
            fileSave.CurrentCaretPosition = document.Scintilla.CurrentPosition;
            fileSave.FilePath = Path.GetDirectoryName(fileSave.FileNameFull);
            fileSave.IsHistory = isHistory;
            fileSave.Session =
                ScriptNotepadDbContext.DbContext.FileSessions.FirstOrDefault(f => f.SessionName == sessionName);

            fileSave.SetEncoding(encoding);

            ScriptNotepadDbContext.DbContext.SaveChanges();
            return ScriptNotepadDbContext.DbContext.FileSaves.FirstOrDefault(f => f.Id == fileSave.Id);
        }

        // <returns>An instance to a <see cref="FileSave"/> class generated from the <see cref="ScintillaTabbedDocument"/> class instance.</returns>

        /// <summary>
        /// Creates a <see cref="FileSave"/> entity from a given <see cref="ScintillaTabbedDocument"/> document.
        /// </summary>
        /// <param name="document">The document to create a file save from.</param>
        /// <param name="encoding">The encoding of the file save.</param>
        /// <param name="fileSession">The file session.</param>
        /// <param name="isHistory">if set to <c>true</c> the resulting <see cref="FileSave"/> instance is marked as a history file.</param>
        /// <returns>An instance to a <see cref="FileSave"/> modified class.</returns>
        public static FileSave CreateFromTabbedDocument(ScintillaTabbedDocument document, Encoding encoding,
            FileSession fileSession, bool isHistory = false)
        {
            var fileSave = new FileSave
            {
                ExistsInFileSystem = File.Exists(document.FileName),
                FileNameFull = document.FileName,
                FileName = Path.GetFileName(document.FileName),
                FilePath = Path.GetDirectoryName(document.FileName),
                FileSystemModified = File.Exists(document.FileName)
                    ? new FileInfo(document.FileName).LastWriteTime
                    : DateTime.MinValue,
                LexerType = document.LexerType,
                VisibilityOrder = (int) document.FileTabButton.Tag,
                Session = ScriptNotepadDbContext.DbContext.FileSessions.FirstOrDefault(f =>
                    f.SessionName == fileSession.SessionName),
                IsActive = document.FileTabButton.IsActive,
                IsHistory = isHistory,
                CurrentCaretPosition = document.Scintilla.CurrentPosition,
                UseSpellChecking = true,
                EditorZoomPercentage = document.ZoomPercentage,
                UseFileSystemOnContents = fileSession.UseFileSystemOnContents,
            };

            fileSave.SetDatabaseModified(DateTime.Now);

            fileSave.SetEncoding(encoding);

            fileSave.SetFileContents(encoding.GetBytes(document.Scintilla.Text), true, false, true);

            ScriptNotepadDbContext.DbContext.FileSaves.Add(fileSave);
            ScriptNotepadDbContext.DbContext.SaveChanges();
            return fileSave;
        }
    }
}
