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

namespace ScriptNotepadOldDatabase.Database
{
    /// <summary>
    /// Some enumerations used by the database classes.
    /// </summary>
    internal static class DatabaseEnumerations
    {
        /// <summary>
        /// An enumeration indicating how the database commands should react to the ISHISTORY flag of the <see cref="DBFILE_SAVE"/> class.
        /// </summary>
        internal enum DatabaseHistoryFlag
        {
            /// <summary>
            /// No history files should be included.
            /// </summary>
            NotHistory,

            /// <summary>
            /// Only history files should be included.
            /// </summary>
            IsHistory,

            /// <summary>
            /// The ISHISTORY flag should be ignored.
            /// </summary>
            DontCare
        }
    }
}
