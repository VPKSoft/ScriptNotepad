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
using System.IO;
using System.Text;
using VPKSoft.ScintillaLexers;
using VPKSoft.ScintillaTabbedTextControl;
using ScriptNotepad.UtilityClasses.StreamHelpers;
using System.Collections.Generic;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using static VPKSoft.ScintillaLexers.LexerEnumerations;

namespace ScriptNotepad.Database.Tables
{
    /// <summary>
    /// Represents a file saved to the database.
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase"/>
    public class DBFILE_SAVE: ErrorHandlingBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DBFILE_SAVE"/> class.
        /// </summary>
        public DBFILE_SAVE()
        {
            ENCODING = Encoding.UTF8;                
        }

        /// <summary>
        /// Gets or sets the ID number (database).
        /// </summary>
        public long ID { get; set; } = -1;

        /// <summary>
        /// Gets or sets a value indicating whether the file exists in the file system.
        /// </summary>
        public bool EXISTS_INFILESYS { get; set; } = false;

        /// <summary>
        /// Gets or sets the full file name with path.
        /// </summary>
        public string FILENAME_FULL { get; set; }

        /// <summary>
        /// Gets or sets the file name without path.
        /// </summary>
        public string FILENAME { get; set; }

        /// <summary>
        /// Gets or sets the full path for the file.
        /// </summary>
        public string FILEPATH { get; set; }

        /// <summary>
        /// Gets or sets the value indicating when the file was modified in the file system.
        /// </summary>
        public DateTime FILESYS_MODIFIED { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the value indicating when the file was saved to the file system by the software.
        /// </summary>
        public DateTime FILESYS_SAVED { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the value indicating when the file was modified in the database.
        /// </summary>
        public DateTime DB_MODIFIED { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the lexer number with the ScintillaNET.
        /// </summary>
        public LexerType LEXER_CODE { get; set; }

        /// <summary>
        /// Gets or sets the file contents.
        /// </summary>
        public MemoryStream FILE_CONTENTS { get; set; }

        /// <summary>
        /// Gets or sets the visibility order (in a tabbed control).
        /// </summary>
        public int VISIBILITY_ORDER { get; set; } = -1;

        /// <summary>
        /// Gets or sets the session ID.
        /// </summary>
        public long SESSIONID { get; set; } = 1;

        /// <summary>
        /// Gets or sets the name of the session.
        /// </summary>
        public string SESSIONNAME { get; set; } = "Default";

        /// <summary>
        /// Gets or sets a value indicating whether if the file is activated in the tab control.
        /// </summary>
        public bool ISACTIVE { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this entry is a history entry.
        /// </summary>
        public bool ISHISTORY { get; set; }

        /// <summary>
        /// Gets or sets the encoding of the file save.
        /// </summary>
        public Encoding ENCODING { get; set; }

        /// <summary>
        /// Gets or sets the previous encodings of the file save for undo possibility.
        /// <note type="note">Redo possibility does not exist.</note>
        /// </summary>
        public List<Encoding> PreviousEncodings { get; set; } = new List<Encoding>();

        /// <summary>
        /// Undoes the encoding change.
        /// </summary>
        public void UndoEncodingChange()
        {
            // only if there exists a previous encoding..
            if (PreviousEncodings.Count > 0)
            {
                // get the last index of the list..
                int idx = PreviousEncodings.Count - 1;

                // set the previous encoding value..
                ENCODING = PreviousEncodings[idx];

                // remove the last encoding from the list..
                PreviousEncodings.RemoveAt(idx);
            }
        }

        /// <summary>
        /// Disposes the memory stream containing the document's contents.
        /// </summary>
        public void DisposeMemoryStream()
        {
            if (FILE_CONTENTS != null)
            {
                using (FILE_CONTENTS)
                {
                    FILE_CONTENTS = null;
                }
            }
        }

        /// <summary>
        /// Reloads the contents of the document from the disk.
        /// </summary>
        /// <param name="document">A ScintillaTabbedDocument to which contents should also be updated.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public bool ReloadFromDisk(ScintillaTabbedDocument document)
        {
            try
            {
                // can't reload what doesn't exist..
                if (File.Exists(FILENAME_FULL))
                {
                    // dispose of the previous file contents..
                    DisposeMemoryStream();

                    // read the file contents from the file..
                    using (FileStream fileStream = new FileStream(FILENAME_FULL, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        // create a byte buffer the contain all the bytes if the file with an assumption
                        // no one wishes to open massive binary files..
                        byte[] fileContents = new byte[fileStream.Length];

                        // read the file contents to the buffer..
                        fileStream.Read(fileContents, 0, (int)fileStream.Length);

                        // set the file system's modified flag..
                        FILESYS_MODIFIED = new FileInfo(FILENAME_FULL).LastWriteTime;
                        DB_MODIFIED = FILESYS_MODIFIED; // set the other DateTime flags to indicate the same..
                        FILESYS_SAVED = FILESYS_MODIFIED; // set the other DateTime flags to indicate the same..

                        // create a new memory stream to hold the file contents..
                        MemoryStream memoryStream = new MemoryStream(fileContents); 

                        document.Scintilla.Text = StreamStringHelpers.MemoryStreamToText(ref memoryStream, ENCODING);

                        FILE_CONTENTS = memoryStream;
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
                ExceptionLogAction?.Invoke(ex);

                return false; // an exception occurred, so fail..
            }
        }

        /// <summary>
        /// Compares two DateTime values <paramref name="dt1"/> > <paramref name="dt2"/>.
        /// </summary>
        /// <param name="dt1">The first DateTime to compare.</param>
        /// <param name="dt2">The second DateTime to compare.</param>
        /// <returns>True if the <paramref name="dt1"/> is larger than <paramref name="dt2"/> value; otherwise false.</returns>
        public static bool DateTimeLarger(DateTime dt1, DateTime dt2)
        {
            string s1 = UtilityClasses.DataFormulationHelpers.DateToDBString(dt1);
            string s2 = UtilityClasses.DataFormulationHelpers.DateToDBString(dt2);
            return s1.CompareTo(s2) > 0;
        }

        /// <summary>
        /// Gets a value indicating whether a software should query the user if the deleted file should be kept in the editor.
        /// </summary>
        public bool ShouldQueryKeepFile
        {
            get => EXISTS_INFILESYS && !File.Exists(FILENAME_FULL);
        }

        /// <summary>
        /// Gets a value indicating whether a software should query the user if a file reappeared in the file system should be reloaded.
        /// </summary>
        public bool ShouldQueryFileReappeared
        {
            get => !EXISTS_INFILESYS && File.Exists(FILENAME_FULL);
        }

        /// <summary>
        /// Gets a value indicating whether the document is changed in the editor versus the file system.
        /// </summary>
        public bool IsChangedInEditor {get => EXISTS_INFILESYS ? DB_MODIFIED > FILESYS_MODIFIED : false; }

        // a value indicating if the user want's to reload the changes from the file system if the file has been changed..
        private bool _ShouldQueryDiskReload = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user should be queried of to reload the changed document from the file system.
        /// </summary>
        public bool ShouldQueryDiskReload
        {
            get
            {
                // note to self: "I do hate this logic with date and time!"..

                // get the last time the file was written into..
                DateTime dtUpdated = new FileInfo(FILENAME_FULL).LastWriteTime;

                // get the result to be returned..
                bool result = _ShouldQueryDiskReload && DateTimeLarger(dtUpdated, FILESYS_MODIFIED);// dtUpdated > FILESYS_MODIFIED;

                // reset this flag so the user can be annoyed again with a stupid question of reloading the file..
                // .. after rethinking, don't do this:  _ShouldQueryDiskReload = true;

                // return the result if the file has been changed in the file system..
                return result;
            }

            set
            {
                // set the flag whether the user want's to hear the
                // stupid question of reloading the file on the next time..
                _ShouldQueryDiskReload = value;
            }
        }
    }
}
