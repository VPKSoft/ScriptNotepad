using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using ScriptNotepad.UtilityClasses.Encodings;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.LinesAndBinary;
using ScriptNotepad.UtilityClasses.StreamHelpers;
using SQLite.CodeFirst;
using VPKSoft.LangLib;
using VPKSoft.ScintillaLexers;
using VPKSoft.ScintillaTabbedTextControl;
using static ScriptNotepad.UtilityClasses.LinesAndBinary.FileLineTypes;


namespace ScriptNotepad.Database.Entity.Entities
{
    /// <summary>
    /// A class representing a single file save entry in the database.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// Implements the <see cref="ScriptNotepad.Database.Entity.IEntity" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// <seealso cref="ScriptNotepad.Database.Entity.IEntity" />
    public class FileSave: ErrorHandlingBase, IEntity
    {
        /// <summary>
        /// Gets or sets the ID number (database).
        /// </summary>
        public int Id { get; set; } = -1;

        /// <summary>
        /// Gets or sets a string representing the encoding of the file save.
        /// </summary>
        [SqlDefaultValue(DefaultValue = "'utf-8;65001;True;False;False'")]
        public string EncodingAsString { get; set; } = "utf-8;65001;True;False;False";

        /// <summary>
        /// Gets or sets the encoding of the file save.
        /// </summary>
        [NotMapped]
        public Encoding Encoding
        {
            get => EncodingAsString == null ? Encoding.UTF8 : EncodingData.EncodingFromString(EncodingAsString);
            set => EncodingAsString = EncodingData.EncodingToString(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the file exists in the file system.
        /// </summary>
        public bool ExistsInFileSystem { get; set; }

        /// <summary>
        /// Gets or sets the full file name with path.
        /// </summary>
        [Required]
        public string FileNameFull { get; set; }

        /// <summary>
        /// Gets or sets the file name without path.
        /// </summary>
        [Required]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the full path for the file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the value indicating when the file was modified in the file system.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [SqlDefaultValue(DefaultValue = "DATETIME('0001-01-01 00:00:00', 'localtime')")]
        public DateTime FileSystemModified { get; set; }

        /// <summary>
        /// Gets or sets the value indicating when the file was saved to the file system by the software.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [SqlDefaultValue(DefaultValue = "DATETIME('0001-01-01 00:00:00', 'localtime')")]
        public DateTime FileSystemSaved { get; set; }

        /// <summary>
        /// Resets the previous database modified property, so it can be set again.
        /// </summary>
        public void ResetPreviousDbModified()
        {
            previousDbModifiedIsSet = false;
            previousDbModified = DateTime.MinValue;
        }

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
        [NotMapped]
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
        /// A field to hold the <see cref="DatabaseModified"/> property value.
        /// </summary>
        private DateTime dbModified = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the value indicating when the file was modified in the database.
        /// </summary>
        public DateTime DatabaseModified
        {
            get => dbModified;

            set 
            { 
                PreviousDbModified = dbModified;
                dbModified = value;
            }
        }

        /// <summary>
        /// Restores the previous time stamp for the <see cref="DatabaseModified"/> property value.
        /// </summary>
        public void PopPreviousDbModified()
        {
            DatabaseModified = PreviousDbModified;
        }

        /// <summary>
        /// Gets or sets the lexer number with the ScintillaNET.
        /// </summary>
        public LexerEnumerations.LexerType LexerType { get; set; }

        /// <summary>
        /// Gets or sets the file contents.
        /// </summary>
        //[NotMapped]
        public byte [] FileContents { get; set; }

        /// <summary>
        /// Gets or sets the visibility order (in a tabbed control).
        /// </summary>
        [SqlDefaultValue(DefaultValue = "-1")] 
        public int VisibilityOrder { get; set; } = -1;

        /// <summary>
        /// Gets or sets the session ID.
        /// </summary>
        [SqlDefaultValue(DefaultValue = "1")] 
        public int SessionId { get; set; } = 1;

        /// <summary>
        /// Gets or sets the name of the session.
        /// </summary>
        [SqlDefaultValue(DefaultValue = "'Default'")] 
        public string SessionName { get; set; } = "Default";

        /// <summary>
        /// Gets or sets a value indicating whether the file is activated in the tab control.
        /// </summary>
        public bool IsActive { get; set; } 

        /// <summary>
        /// Gets or sets a value indicating whether this entry is a history entry.
        /// </summary>
        public bool IsHistory { get; set; }

        /// <summary>
        /// Gets or sets the current position (cursor / caret) of the file.
        /// </summary>
        public int CurrentCaretPosition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use spell check with this document.
        /// </summary>
        public bool UseSpellChecking { get; set; }

        /// <summary>
        /// Gets or sets the editor zoom value in percentage.
        /// </summary>
        [SqlDefaultValue(DefaultValue = "100")] 
        public int EditorZoomPercentage { get; set; } = 100;

        /// <summary>
        /// Gets or sets the previous encodings of the file save for undo possibility.
        /// <note type="note">Redo possibility does not exist.</note>
        /// </summary>
        [NotMapped]
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
                Encoding = PreviousEncodings[idx];

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
                if (File.Exists(FileNameFull))
                {
                    // read the file contents from the file..
                    using (FileStream fileStream = new FileStream(FileNameFull, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        // create a byte buffer the contain all the bytes if the file with an assumption
                        // no one wishes to open massive binary files..
                        byte[] fileContents = new byte[fileStream.Length];

                        // read the file contents to the buffer..
                        fileStream.Read(fileContents, 0, (int)fileStream.Length);

                        // set the file system's modified flag..
                        FileSystemModified = new FileInfo(FileNameFull).LastWriteTime;
                        DatabaseModified = FileSystemModified; // set the other DateTime flags to indicate the same..
                        FileSystemSaved = FileSystemModified; // set the other DateTime flags to indicate the same..

                        // create a new memory stream to hold the file contents..
                        MemoryStream memoryStream = new MemoryStream(fileContents); 

                        document.Scintilla.Text = StreamStringHelpers.MemoryStreamToText(memoryStream, Encoding);

                        // a reload doesn't need to be undone..
                        document.Scintilla.EmptyUndoBuffer();

                        FileContents = memoryStream.ToArray();

                        // set the saved position of the document's caret..
                        if (CurrentCaretPosition > 0 && CurrentCaretPosition < document.Scintilla.TextLength)
                        {
                            document.Scintilla.CurrentPosition = CurrentCaretPosition;
                            document.Scintilla.SelectionStart = CurrentCaretPosition;
                            document.Scintilla.SelectionEnd = CurrentCaretPosition;
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
        [NotMapped]
        public bool ShouldQueryKeepFile => ExistsInFileSystem && !File.Exists(FileNameFull);

        /// <summary>
        /// Gets a value indicating whether a software should query the user if a file reappeared in the file system should be reloaded.
        /// </summary>
        [NotMapped]
        public bool ShouldQueryFileReappeared => !ExistsInFileSystem && File.Exists(FileNameFull);

        /// <summary>
        /// Gets a value indicating whether the document is changed in the editor versus the file system.
        /// </summary>
        [NotMapped]
        public bool IsChangedInEditor => ExistsInFileSystem && DatabaseModified > FileSystemModified;

        // a value indicating if the user wants to reload the changes from the file system if the file has been changed..
        private bool shouldQueryDiskReload = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user should be queried of to reload the changed document from the file system.
        /// </summary>
        [NotMapped]
        public bool ShouldQueryDiskReload
        {
            get
            {
                // note to self: "I do hate this logic with date and time!"..

                // get the last time the file was written into..
                DateTime dtUpdated = new FileInfo(FileNameFull).LastWriteTime;

                // get the result to be returned..
                bool result = shouldQueryDiskReload && DateTimeLarger(dtUpdated, FileSystemModified);// dtUpdated > FileSystemModified;

                // reset this flag so the user can be annoyed again with a stupid question of reloading the file..
                // .. after rethinking, don't do this:  _ShouldQueryDiskReload = true;

                // return the result if the file has been changed in the file system..
                return result;
            }

            // set the flag whether the user wants to hear the
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
        [NotMapped]
        public IEnumerable<KeyValuePair<FileLineTypes, string>> FileLineTypes
        {
            get
            {
                if (fileLineTypesInternal == null)
                {
                    var fileLineTypes = ScriptNotepad.UtilityClasses.LinesAndBinary.
                        FileLineType.GetFileLineTypes(FileContents);

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
        [NotMapped]
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
        [NotMapped]
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
