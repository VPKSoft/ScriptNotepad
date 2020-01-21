﻿#region License
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VPKSoft.LangLib;
using VPKSoft.ScintillaLexers;

namespace ScriptNotepadOldDatabase.Database.Tables
{
    /// <summary>
    /// Represents a file saved to the database.
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase"/>
    internal class DBFILE_SAVE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DBFILE_SAVE"/> class.
        /// </summary>
        internal DBFILE_SAVE()
        {
            ENCODING = Encoding.UTF8;                
        }

        private long id = -1;

        /// <summary>
        /// Gets or sets the ID number (database).
        /// </summary>
        internal long ID
        {
            get => id; 
            set
            {
                if (value <= 0)
                {
                    // TODO::After LINQ:throw new Exception("Catch the bug");
                }

                id = value;
            }

        }

        /// <summary>
        /// Gets or sets a value indicating whether the file exists in the file system.
        /// </summary>
        internal bool EXISTS_INFILESYS { get; set; } = false;

        /// <summary>
        /// Gets or sets the full file name with path.
        /// </summary>
        internal string FILENAME_FULL { get; set; }

        /// <summary>
        /// Gets or sets the file name without path.
        /// </summary>
        internal string FILENAME { get; set; }

        /// <summary>
        /// Gets or sets the full path for the file.
        /// </summary>
        internal string FILEPATH { get; set; }

        /// <summary>
        /// Gets or sets the value indicating when the file was modified in the file system.
        /// </summary>
        internal DateTime FILESYS_MODIFIED { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the value indicating when the file was saved to the file system by the software.
        /// </summary>
        internal DateTime FILESYS_SAVED { get; set; } = DateTime.MinValue;

        /// <summary>
        /// The field to hold a value if the <see cref="PreviousDbModified"/> property has been set once.
        /// </summary>
        internal bool previousDbModifiedIsSet;

        /// <summary>
        /// A field to hold the <see cref="PreviousDbModified"/> property value.
        /// </summary>
        internal DateTime previousDbModified = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the value indicating when the file was previously modified in the database.
        /// </summary>
        internal DateTime PreviousDbModified
        {
            get => previousDbModified;

            set
            {
                if (previousDbModified.CompareTo(value) != 0 && !previousDbModifiedIsSet)
                {
                    previousDbModified = value;
                    previousDbModifiedIsSet = true;
                }
            }
        }

        /// <summary>
        /// A field to hold the <see cref="DB_MODIFIED"/> property value.
        /// </summary>
        internal DateTime dbModified = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the value indicating when the file was modified in the database.
        /// </summary>
        internal DateTime DB_MODIFIED
        {
            get => dbModified;

            set 
            { 
                PreviousDbModified = dbModified;
                dbModified = value;
            }
        }

        /// <summary>
        /// Gets or sets the lexer number with the ScintillaNET.
        /// </summary>
        internal LexerEnumerations.LexerType LEXER_CODE { get; set; }

        /// <summary>
        /// Gets or sets the file contents.
        /// </summary>
        internal string FILE_CONTENTS { get; set; }

        /// <summary>
        /// Gets or sets the visibility order (in a tabbed control).
        /// </summary>
        internal int VISIBILITY_ORDER { get; set; } = -1;


        internal long sessionId = 1;
        /// <summary>
        /// Gets or sets the session ID.
        /// </summary>
        internal long SESSIONID { get => sessionId;
            set
            {
                if (value < 1)
                {
                    sessionId = 1;
                }
                else
                {
                    sessionId = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the session.
        /// </summary>
        internal string SESSIONNAME { get; set; } = "Default";

        /// <summary>
        /// Gets or sets a value indicating whether if the file is activated in the tab control.
        /// </summary>
        internal bool ISACTIVE { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this entry is a history entry.
        /// </summary>
        internal bool ISHISTORY { get; set; }

        // a field for the ENCODING property..
        private Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// Gets or sets the encoding of the file save.
        /// </summary>
        internal Encoding ENCODING
        {
            get => encoding;

            set => encoding = value;
        }

        /// <summary>
        /// Gets or sets the current position (cursor / caret) of the file.
        /// </summary>
        internal int CURRENT_POSITION { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use spell check with this document.
        /// </summary>
        internal bool USESPELL_CHECK { get; set; }

        /// <summary>
        /// Gets or sets the editor zoom value in percentage.
        /// </summary>
        internal int EDITOR_ZOOM { get; set; } = 100;

        /// <summary>
        /// Gets or sets the previous encodings of the file save for undo possibility.
        /// <note type="note">Redo possibility does not exist.</note>
        /// </summary>
        internal List<Encoding> PreviousEncodings { get; set; } = new List<Encoding>();

        /// <summary>
        /// Restores the previous time stamp for the <see cref="DB_MODIFIED"/> field.
        /// </summary>
        internal void PopPreviousDbModified()
        {
            DB_MODIFIED = PreviousDbModified;
        }

        /// <summary>
        /// Undoes the encoding change.
        /// </summary>
        internal void UndoEncodingChange()
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
        /// Compares two DateTime values <paramref name="dt1"/> > <paramref name="dt2"/>.
        /// </summary>
        /// <param name="dt1">The first DateTime to compare.</param>
        /// <param name="dt2">The second DateTime to compare.</param>
        /// <returns>True if the <paramref name="dt1"/> is larger than <paramref name="dt2"/> value; otherwise false.</returns>
        internal static bool DateTimeLarger(DateTime dt1, DateTime dt2)
        {
            string s1 = UtilityClasses.DataFormulationHelpers.DateToDBString(dt1);
            string s2 = UtilityClasses.DataFormulationHelpers.DateToDBString(dt2);
            return String.Compare(s1, s2, StringComparison.Ordinal) > 0;
        }

        /// <summary>
        /// Gets a value indicating whether a software should query the user if the deleted file should be kept in the editor.
        /// </summary>
        internal bool ShouldQueryKeepFile
        {
            get => EXISTS_INFILESYS && !File.Exists(FILENAME_FULL);
        }

        /// <summary>
        /// Gets a value indicating whether a software should query the user if a file reappeared in the file system should be reloaded.
        /// </summary>
        internal bool ShouldQueryFileReappeared
        {
            get => !EXISTS_INFILESYS && File.Exists(FILENAME_FULL);
        }

        /// <summary>
        /// Gets a value indicating whether the document is changed in the editor versus the file system.
        /// </summary>
        internal bool IsChangedInEditor => EXISTS_INFILESYS && DB_MODIFIED > FILESYS_MODIFIED;

        // a value indicating if the user wants to reload the changes from the file system if the file has been changed..
        private bool shouldQueryDiskReload = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user should be queried of to reload the changed document from the file system.
        /// </summary>
        internal bool ShouldQueryDiskReload
        {
            get
            {
                // note to self: "I do hate this logic with date and time!"..

                // get the last time the file was written into..
                DateTime dtUpdated = new FileInfo(FILENAME_FULL).LastWriteTime;

                // get the result to be returned..
                bool result = shouldQueryDiskReload && DateTimeLarger(dtUpdated, FILESYS_MODIFIED);// dtUpdated > FILESYS_MODIFIED;

                // reset this flag so the user can be annoyed again with a stupid question of reloading the file..
                // .. after rethinking, don't do this:  _ShouldQueryDiskReload = true;

                // return the result if the file has been changed in the file system..
                return result;
            }

            // set the flag whether the user want's to hear the
            // stupid question of reloading the file on the next time..
            set => shouldQueryDiskReload = value;
        }
    }
}