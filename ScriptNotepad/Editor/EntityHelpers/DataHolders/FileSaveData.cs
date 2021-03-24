using System;
using System.Collections.Generic;
using System.Text;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.UtilityClasses.LinesAndBinary;

namespace ScriptNotepad.Editor.EntityHelpers.DataHolders
{
    /// <summary>
    /// Additional data for the <see cref="FileSave"/> entity.
    /// </summary>
    public class FileSaveData
    {
        /// <summary>
        /// Gets or sets the value if the <see cref="PreviousDbModified"/> property has been set once.
        /// </summary>
        public bool PreviousDbModifiedIsSet { get; set; }

        /// <summary>
        /// Gets or sets the value indicating when the file was modified in the database.
        /// </summary>
        public DateTime PreviousDbModified { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets a value indicating whether the user should be queried of to reload the changed document from the file system.
        /// </summary>
        public bool ShouldQueryDiskReload { get; set; } = true;

        /// <summary>
        /// Gets or sets the previous encodings of the file save for undo possibility.
        /// <note type="note">Redo possibility does not exist.</note>
        /// </summary>
        public List<Encoding> PreviousEncodings { get; set; } = new();

        /// <summary>
        /// A text describing the file line ending type(s) of the document.
        /// </summary>
        public string FileEndingText { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file line types and their descriptions.
        /// </summary>
        /// <value>The file line types and their descriptions.</value>
        public IEnumerable<KeyValuePair<FileLineTypes, string>> FileLineTypesInternal { get; set; } =
            new List<KeyValuePair<FileLineTypes, string>>();

        /// <summary>
        /// Gets or sets the value indicating when the file was modified in the database.
        /// </summary>
        public DateTime DbModified { get; set; } = DateTime.MinValue;
    }
}
