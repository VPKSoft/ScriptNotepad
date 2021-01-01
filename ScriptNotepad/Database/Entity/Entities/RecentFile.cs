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

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ScriptNotepad.UtilityClasses.Encodings;

namespace ScriptNotepad.Database.Entity.Entities
{
    /// <summary>
    /// A class for storing recent file data into the database.
    /// Implements the <see cref="ScriptNotepad.Database.Entity.IEntity" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.Database.Entity.IEntity" />
    [Table("RecentFiles")]
    public class RecentFile: IEntity
    {
        /// <summary>
        /// Gets or sets the identifier for the entity.
        /// </summary>
        [Column("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full file name with path.
        /// </summary>
        [Required]
        [Column("FileNameFull")]
        public string FileNameFull { get; set; }

        /// <summary>
        /// Gets or sets the file name without path.
        /// </summary>
        [Required]
        [Column("FileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the full path for the file.
        /// </summary>
        [Column("FilePath")]
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the file was closed in the editor.
        /// </summary>
        [Required]
        [Column("ClosedDateTime")]
        public DateTime ClosedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the session the recent file belongs to.
        /// </summary>
        [Required]
        [Column("Session")]
        public FileSession Session { get; set; }

        /// <summary>
        /// Gets or sets the encoding of the recent file.
        /// </summary>
        [NotMapped]
        public Encoding Encoding
        {
            get => EncodingAsString == null ? Encoding.UTF8 : EncodingData.EncodingFromString(EncodingAsString);
            set => EncodingAsString = EncodingData.EncodingToString(value);
        }

        /// <summary>
        /// Gets or sets a string representing the encoding of the file save.
        /// </summary>
//        [SqlDefaultValue(DefaultValue = "'utf-8;65001;True;False;False'")]
        [Column("EncodingAsString")]
        public string EncodingAsString { get; set; } = "utf-8;65001;True;False;False";

        /// <summary>
        /// Gets a value indicating whether a snapshot of the file in question exists in the database.
        /// </summary>
        public bool ExistsInDatabase(DbSet<FileSave> fileSaves)
        {
            return fileSaves.Count(f => f.FileNameFull == FileNameFull && f.Session.SessionName == Session.SessionName && f.IsHistory) > 0;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return FileNameFull;
        }
    }
}
