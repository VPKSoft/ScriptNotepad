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
using VPKSoft.ScintillaLexers;

namespace ScriptNotepad.Database
{
    /// <summary>
    /// Represents a file saved to the database.
    /// </summary>
    public class DBFILE_SAVE
    {
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
        public int SESSIONID { get; set; } = 1;

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
    }
}
