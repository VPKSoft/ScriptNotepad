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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using ScriptNotepad.UtilityClasses.Encodings;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.LinesAndBinary;
using SQLite.CodeFirst;
using VPKSoft.LangLib;
using VPKSoft.ScintillaLexers;
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
        /// Gets or sets the identifier for the entity.
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
//        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
//        [SqlDefaultValue(DefaultValue = "DATETIME('0001-01-01 00:00:00', 'localtime')")]
        public DateTime FileSystemModified { get; set; }

        /// <summary>
        /// Gets or sets the value indicating when the file was saved to the file system by the software.
        /// </summary>
//        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
//        [SqlDefaultValue(DefaultValue = "DATETIME('0001-01-01 00:00:00', 'localtime')")]
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

        private bool useFileSystemOnContents;

        /// <summary>
        /// Gets or sets a value indicating whether to use the file system to store the contents of the file instead of a database BLOB.
        /// </summary>
        public bool UseFileSystemOnContents
        {
            get => useFileSystemOnContents;

            set
            {
                if (value != useFileSystemOnContents)
                {
                    if (value) // a switch logic is required to perform an "easy" change..
                    {
                        SetFileContents(FileContents, true, false, false);
                        FileContents = null;
                    }
                    else
                    {
                        FileContents = GetFileContents();
                        File.Delete(TemporaryFileSaveName);
                        TemporaryFileSaveName = null;
                    }

                    useFileSystemOnContents = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the location of the temporary file save in case the file changes are cached into the file system.
        /// </summary>
        public string TemporaryFileSaveName { get; set; }

        /// <summary>
        /// Sets the random file name for this <see cref="FileSave"/> instance to to be used as a file system cache for changed file contents. The <see cref="TemporaryFileSaveName"/> must be true for this to work.
        /// </summary>
        /// <returns>System.String.</returns>
        public string SetRandomFile()
        {
            if (TemporaryFileSaveName == null && UseFileSystemOnContents)
            {
                Session.SetRandomPath();
                TemporaryFileSaveName = Path.Combine(Session.TemporaryFilePath, Path.GetRandomFileName());
            }
            else if (!UseFileSystemOnContents)
            {
                TemporaryFileSaveName = null;
            }

            return TemporaryFileSaveName;
        }

        /// <summary>
        /// Gets or sets the file contents.
        /// </summary>
        //[NotMapped]
        public byte[] FileContents
        {
            get
            {
                if (useFileSystemOnContents) 
                {
                    // this will prevent the Entity Framework from saving the contents when file
                    // system cache is used..
                    return null;
                }

                return fileContentsMemory;
            }

            set => fileContentsMemory = value;
        }

        // the "file" contents..
        private byte[] fileContentsMemory;

        /// <summary>
        /// Sets the file contents of this <see cref="FileSave"/> class instance.
        /// The contents are either saved to the file system or to the database depending on the <see cref="UseFileSystemOnContents"/> property value.
        /// </summary>
        /// <param name="fileContents">The file contents.</param>
        /// <param name="commit">A value indicating whether to commit the changes to the
        /// database or to the file system cache depending on the <see cref="UseFileSystemOnContents"/> property value.</param>
        /// <param name="saveToFileSystem">A value indicating whether to override existing copy of the file in the file system.</param>
        /// <param name="contentChanged">A value indicating whether the file contents have been changed.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public bool SetFileContents(byte[] fileContents, bool commit, bool saveToFileSystem, 
            bool contentChanged)
        {
            try
            {
                FileContents = fileContents;

                if (UseFileSystemOnContents)
                {
                    if (commit && !saveToFileSystem)
                    {
                        File.WriteAllBytes(SetRandomFile(), fileContents);
                    }

                    if (saveToFileSystem)
                    {
                        File.WriteAllBytes(SetRandomFile(), fileContents);

                        if (ExistsInFileSystem)
                        {
                            File.WriteAllBytes(FileNameFull, fileContents);
                            FileSystemSaved = DateTime.Now;
                        }
                    }
                }
                else
                {
                    FileContents = fileContents;
                }

                if (contentChanged)
                {
                    DatabaseModified = DateTime.Now;
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionLogAction?.Invoke(ex);
                return false;
            }
        }



        /// <summary>
        /// Gets the cached file contents of this <see cref="FileSave"/> class instance.
        /// </summary>
        /// <returns>A byte array with the file contents.</returns>
        public byte[] GetFileContents()
        {
            try
            {
                if (UseFileSystemOnContents)
                {
                    return File.ReadAllBytes(TemporaryFileSaveName);
                }
                else
                {
                    return FileContents;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets or sets the file contents as a memory stream.
        /// </summary>
        /// <value>The file contents as a memory stream.</value>
        [NotMapped]
        public MemoryStream FileContentsAsMemoryStream
        {
            get
            {
                var fileContents = GetFileContents();
                if (fileContents == null || fileContents.Length == 0)
                {
                    return new MemoryStream();
                }

                return new MemoryStream(fileContents);
            }

            set => SetFileContents(value.ToArray(), true, false, false);
        }

        /// <summary>
        /// Gets or sets the visibility order (in a tabbed control).
        /// </summary>
        [SqlDefaultValue(DefaultValue = "-1")] 
        public int VisibilityOrder { get; set; } = -1;

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
                var fileSysModified = new FileInfo(FileNameFull).LastWriteTime;

                // get the last time the file was written into..
                DateTime dtUpdated = fileSysModified;

                // get the result to be returned..
                bool result = shouldQueryDiskReload && dtUpdated > FileSystemModified;

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
                if (fileLineTypesInternal == null && FileContents == null)
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

        /// <summary>
        /// Gets or sets the session the <see cref="FileSave"/> belongs to.
        /// </summary>
        [Required]
        public FileSession Session { get; set; }
    }
}
