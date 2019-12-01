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
using VPKSoft.ScintillaTabbedTextControl;
using ScriptNotepad.UtilityClasses.StreamHelpers;
using System.Collections.Generic;
using System.Linq;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.LinesAndBinary;
using static VPKSoft.ScintillaLexers.LexerEnumerations;
using VPKSoft.LangLib;
using static ScriptNotepad.UtilityClasses.LinesAndBinary.FileLineTypes;

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

        private long id = -1;

        /// <summary>
        /// Gets or sets the ID number (database).
        /// </summary>
        public long ID
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
        /// The field to hold a value if the <see cref="PreviousDbModified"/> property has been set once.
        /// </summary>
        private bool previousDbModifiedIsSet;

        /// <summary>
        /// A field to hold the <see cref="PreviousDbModified"/> property value.
        /// </summary>
        private DateTime previousDbModified = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the value indicating when the file was previously modified in the database.
        /// </summary>
        public DateTime PreviousDbModified
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
        private DateTime dbModified = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the value indicating when the file was modified in the database.
        /// </summary>
        public DateTime DB_MODIFIED
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
        public LexerType LEXER_CODE { get; set; }

        /// <summary>
        /// Gets or sets the file contents.
        /// </summary>
        public string FILE_CONTENTS { get; set; }

        /// <summary>
        /// Gets or sets the visibility order (in a tabbed control).
        /// </summary>
        public int VISIBILITY_ORDER { get; set; } = -1;


        private long sessionId = 1;
        /// <summary>
        /// Gets or sets the session ID.
        /// </summary>
        public long SESSIONID { get => sessionId;
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
        public string SESSIONNAME { get; set; } = "Default";

        /// <summary>
        /// Gets or sets a value indicating whether if the file is activated in the tab control.
        /// </summary>
        public bool ISACTIVE { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this entry is a history entry.
        /// </summary>
        public bool ISHISTORY { get; set; }

        // a field for the ENCODING property..
        private Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// Gets or sets the encoding of the file save.
        /// </summary>
        public Encoding ENCODING
        {
            get => encoding;

            set => encoding = value;
        }

        /// <summary>
        /// Gets or sets the current position (cursor / caret) of the file.
        /// </summary>
        public int CURRENT_POSITION { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use spell check with this document.
        /// </summary>
        public bool USESPELL_CHECK { get; set; }

        /// <summary>
        /// Gets or sets the editor zoom value in percentage.
        /// </summary>
        public int EDITOR_ZOOM { get; set; } = 100;

        /// <summary>
        /// Gets or sets the previous encodings of the file save for undo possibility.
        /// <note type="note">Redo possibility does not exist.</note>
        /// </summary>
        public List<Encoding> PreviousEncodings { get; set; } = new List<Encoding>();

        /// <summary>
        /// Restores the previous time stamp for the <see cref="DB_MODIFIED"/> field.
        /// </summary>
        public void PopPreviousDbModified()
        {
            DB_MODIFIED = PreviousDbModified;
        }

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

                        document.Scintilla.Text = StreamStringHelpers.MemoryStreamToText(memoryStream, ENCODING);

                        // a reload doesn't need to be undone..
                        document.Scintilla.EmptyUndoBuffer(); 

                        FILE_CONTENTS = document.Scintilla.Text;

                        // set the saved position of the document's caret..
                        if (CURRENT_POSITION > 0 && CURRENT_POSITION < document.Scintilla.TextLength)
                        {
                            document.Scintilla.CurrentPosition = CURRENT_POSITION;
                            document.Scintilla.SelectionStart = CURRENT_POSITION;
                            document.Scintilla.SelectionEnd = CURRENT_POSITION;
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
            return String.Compare(s1, s2, StringComparison.Ordinal) > 0;
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
        public bool IsChangedInEditor => EXISTS_INFILESYS && DB_MODIFIED > FILESYS_MODIFIED;

        // a value indicating if the user wants to reload the changes from the file system if the file has been changed..
        private bool shouldQueryDiskReload = true;

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

        /// <summary>
        /// A text describing the file line ending type(s) of the document.
        /// </summary>
        private string fileEndingText = string.Empty;

        // the file line types and their descriptions..
        private IEnumerable<KeyValuePair<FileLineTypes, string>> fileLineTypesInternal;

        /// <summary>
        /// Gets the file line types and their descriptions.
        /// </summary>
        public IEnumerable<KeyValuePair<FileLineTypes, string>> FileLineTypes
        {
            get
            {
                if (fileLineTypesInternal == null)
                {
                    var fileLineTypes = ScriptNotepad.UtilityClasses.LinesAndBinary.
                        FileLineType.GetFileLineTypes(FILE_CONTENTS,
                            ENCODING);

                    var lineTypesInternal = fileLineTypes as KeyValuePair<FileLineTypes, string>[] ??
                                            fileLineTypes.ToArray();

                    fileLineTypesInternal = lineTypesInternal;

                    return lineTypesInternal;
                }

                return fileLineTypesInternal;
            }
        }

        /// <summary>
        /// Gets the type of the file line ending.
        /// </summary>
        public FileLineTypes FileLineType
        {
            get
            {
                List<KeyValuePair<FileLineTypes, string>> typesList =
                    new List<KeyValuePair<FileLineTypes, string>>(FileLineTypes.ToArray());

                if (typesList.Count == 0 ||
                    typesList.Count == 1 && typesList[0].Key.HasFlag(Mixed)) 
                {
                    return CRLF;
                }

                if (typesList.Count == 1)
                {
                    return typesList[0].Key;
                }

                return typesList.FirstOrDefault().Key;
            }
        }

        /// <summary>
        /// Gets the text describing the file line ending type(s) of the document.
        /// </summary>
        public string FileLineEndingText
        {
            get
            {
                if (fileEndingText == string.Empty)
                {
                    fileEndingText = DBLangEngine.GetStatMessage("msgLineEndingShort",
                        "LE: |A short message indicating a file line ending type value(s) as a concatenated text");


                    var fileLineTypes = FileLineTypes;

                    string endAppend = string.Empty;

                    foreach (var fileLineType in fileLineTypes)
                    {
                        if (!fileLineType.Key.HasFlag(Mixed))
                        {
                            fileEndingText += fileLineType.Value + ", ";
                        }
                        else
                        {
                            endAppend = $" ({fileLineType.Value})";
                        }

                        fileEndingText = fileEndingText.TrimEnd(',', ' ') + endAppend;
                    }
                }

                return fileEndingText;
            }
        }
    }
}
