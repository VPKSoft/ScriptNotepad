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
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.Database.Tables
{
    /// <summary>
    /// A class representing either an database entry from the SEARCH_HISTORY or SEARCH_HISTORY table.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class SEARCH_AND_REPLACE_HISTORY: ErrorHandlingBase
    {
        /// <summary>
        /// Gets or sets the ID number (database).
        /// </summary>
        public long ID { get; set; } = -1;

        /// <summary>
        /// Gets or sets the search or replace text.
        /// </summary>
        public string SEARCH_OR_REPLACE_TEXT { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether search or replace was case sensitive.
        /// </summary>
        public bool CASE_SENSITIVE { get; set; }

        /// <summary>
        /// Gets or sets the type of the search where 0 = Normal, 1 = Extended, 2 = Regular expression, 3 = Simple extended.
        /// </summary>
        public int TYPE { get; set; }

        /// <summary>
        /// Gets or sets the added date and time when the entry was added to the database or created.
        /// </summary>
        public DateTime ADDED { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the session ID.
        /// </summary>
        public long SESSIONID { get; set; } = 1;

        /// <summary>
        /// Gets or sets the name of a session.
        /// </summary>
        public string SESSIONNAME { get; set; }
    }
}
