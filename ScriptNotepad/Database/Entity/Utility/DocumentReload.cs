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
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.StreamHelpers;
using VPKSoft.ScintillaTabbedTextControl;

namespace ScriptNotepad.Database.Entity.Utility
{
    /// <summary>
    /// A helper class to reload a document from the file system.
    /// </summary>
    public static class DocumentReload
    {
        /// <summary>
        /// Reloads the contents of the document from the disk.
        /// </summary>
        /// <param name="fileSave">An instance to a <see cref="FileSave"/> class.</param>
        /// <param name="document">A ScintillaTabbedDocument to which contents should also be updated.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public static bool ReloadFromDisk(this FileSave fileSave, ScintillaTabbedDocument document)
        {
            try
            {
                // can't reload what doesn't exist..
                if (File.Exists(fileSave.FileNameFull))
                {
                    // read the file contents from the file..
                    using (FileStream fileStream = new FileStream(fileSave.FileNameFull, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        // create a byte buffer the contain all the bytes if the file with an assumption
                        // no one wishes to open massive binary files..
                        byte[] fileContents = new byte[fileStream.Length];

                        // read the file contents to the buffer..
                        fileStream.Read(fileContents, 0, (int)fileStream.Length);

                        // set the file system's modified flag..
                        fileSave.FileSystemModified = new FileInfo(fileSave.FileNameFull).LastWriteTime;
                        fileSave.DatabaseModified = fileSave.FileSystemModified; // set the other DateTime flags to indicate the same..
                        fileSave.FileSystemSaved = fileSave.FileSystemModified; // set the other DateTime flags to indicate the same..

                        // create a new memory stream to hold the file contents..
                        MemoryStream memoryStream = new MemoryStream(fileContents); 

                        document.Scintilla.Text = StreamStringHelpers.MemoryStreamToText(memoryStream, fileSave.Encoding);

                        // a reload doesn't need to be undone..
                        document.Scintilla.EmptyUndoBuffer();

                        fileSave.FileContents = memoryStream.ToArray();

                        // set the saved position of the document's caret..
                        if (fileSave.CurrentCaretPosition > 0 && fileSave.CurrentCaretPosition < document.Scintilla.TextLength)
                        {
                            document.Scintilla.CurrentPosition = fileSave.CurrentCaretPosition;
                            document.Scintilla.SelectionStart = fileSave.CurrentCaretPosition;
                            document.Scintilla.SelectionEnd = fileSave.CurrentCaretPosition;
                            document.Scintilla.ScrollCaret();
                        }

                    }
                    return true; // success..
                }
                else
                {
                    return false; // the file didn't exists, so fail..
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);

                return false; // an exception occurred, so fail..
            }
        }
    }
}
