﻿#region License
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

using ScriptNotepad.UtilityClasses.ErrorHandling;
using System;
using System.IO;
using System.Text;

namespace ScriptNotepad.Database.Tables
{
    /// <summary>
    /// A class indicating a recent file in the database.
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase"/>
    public class RECENT_FILES : ErrorHandlingBase
    {
        /// <summary>
        /// Creates a <see cref="RECENT_FILES"/> class instance from a given file name.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="encoding">The encoding of the recent file.</param>
        /// <param name="ID">An optional database identifier for file.</param>
        /// <param name="referenceID">An optional database reference identifier for the file.</param>
        /// <returns>An instance to <see cref="RECENT_FILES"/> class created based on the given arguments.</returns>
        public static RECENT_FILES FromFilename(string fileName, Encoding encoding, long ID = -1, long? referenceID = null)
        {
            return new RECENT_FILES()
            {
                ID = ID,
                FILENAME_FULL = fileName,
                FILENAME = Path.GetFileName(fileName),
                FILEPATH = Path.GetDirectoryName(fileName),
                CLOSED_DATETIME = DateTime.Now,
                REFERENCEID = referenceID,
                ENCODING = encoding
            };
        }

        /// <summary>
        /// Gets or sets the ID number of the entry in the document history.
        /// </summary>
        public long ID { get; set; } = -1;

        /// <summary>
        /// Gets or sets the full file name with path.
        /// </summary>
        public string FILENAME_FULL { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file name without path.
        /// </summary>
        public string FILENAME { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full path for the file.
        /// </summary>
        public string FILEPATH { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the file was closed in the editor.
        /// </summary>
        public DateTime CLOSED_DATETIME { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the session ID.
        /// </summary>
        public long SESSIONID { get; set; } = 1;

        /// <summary>
        /// Gets or sets the name of the session.
        /// </summary>
        public string SESSIONNAME { get; set; } = "Default";

        /// <summary>
        /// Gets or sets the encoding of the recent file.
        /// </summary>
        public Encoding ENCODING { get; set; }

        /// <summary>
        /// Gets or sets a reference to a file ID in the DBFILE_SAVE table.
        /// <note type="note">A null value indicates a DB null.</note>
        /// </summary>
        public long? REFERENCEID { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether a snapshot of the file in question exists in the <see cref="DBFILE_SAVE"/> table in the database.
        /// </summary>
        public bool EXISTSINDB { get; set; } = false;

        /// <summary>
        /// Gets a value indicating whether this <see cref="RECENT_FILES"/> property <see cref="FILENAME_FULL"/> exists in the file system.
        /// </summary>
        public bool EXISTSINFILESYS { get => File.Exists(FILENAME_FULL); }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return FILENAME_FULL;
        }
    }
}
