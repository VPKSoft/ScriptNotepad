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

namespace ScriptNotepadOldDatabase.Database.Tables
{
    /// <summary>
    /// A class for indicating for code snippets in the database.
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase"/>
    internal class CODE_SNIPPETS
    {
        /// <summary>
        /// Gets or sets the ID number of the entry in the database.
        /// </summary>
        internal long ID { get; set; } = -1;

        /// <summary>
        /// Gets or sets the script's contents.
        /// </summary>
        internal string SCRIPT_CONTENTS { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the script.
        /// </summary>
        internal string SCRIPT_NAME { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the script was previously modified.
        /// </summary>
        internal DateTime MODIFIED { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the type of the script where a value of 0 means text manipulation and a value of 1 means lines manipulation.
        /// </summary>
        internal int SCRIPT_TYPE { get; set; } = 0; // currently only 

        /// <summary>
        /// Gets or sets the language type of the script snippet. Currently only C# is supported with a value of 0.
        /// </summary>
        internal int SCRIPT_LANGUAGE { get; set; } = 0;
    }
}
