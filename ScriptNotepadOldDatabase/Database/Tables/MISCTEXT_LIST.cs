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

namespace ScriptNotepadOldDatabase.Database.Tables
{
    /// <summary>
    /// An enumeration for the <see cref="MISCTEXT_LIST.TYPE"/> field in the database.
    /// </summary>
    internal enum MiscTextType
    {
        /// <summary>
        /// Indicates a path/directory in the file system.
        /// </summary>
        Path = 0,

        /// <summary>
        /// Indicates a file extension list delimited with semicolon (;); I.e. *.txt;*.cs.
        /// </summary>
        FileExtensionList = 1,
    }

    /// <summary>
    /// A class representing a database entry in the MISCTEXT_LIST database table.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    internal class MISCTEXT_LIST
    {
        /// <summary>
        /// Gets or sets the ID number (database).
        /// </summary>
        internal long ID { get; set; } = -1;

        /// <summary>
        /// Gets or sets the text value of the entry.
        /// </summary>
        internal string TEXTVALUE { get; set; }

        /// <summary>
        /// Gets or sets the type of the entry where 0 = directory/path, 1 = file filter, 2 = Regular expression, 3 = Simple extended.
        /// </summary>
        internal MiscTextType TYPE { get; set; }

        /// <summary>
        /// Gets or sets the added date and time when the entry was added or updated to the database.
        /// </summary>
        internal DateTime ADDED { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the session ID the entry belongs to. This can be null.
        /// </summary>
        internal long? SESSIONID { get; set; } = null;

        /// <summary>
        /// Gets or sets the name of a session.
        /// </summary>
        internal string SESSIONNAME { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        internal new string ToString()
        {
            return TEXTVALUE;
        }
    }
}
