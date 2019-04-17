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

namespace ScriptNotepad.UtilityClasses.SearchAndReplace.Misc
{
    /// <summary>
    /// Event arguments for the search and replace methods to report the search progress.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class SearchAndReplaceProgressEventArgs: EventArgs
    {
        /// <summary>
        /// Gets or sets the total file count of a search or a replace process.
        /// </summary>
        public int FileCount { get; set; } = 0;

        /// <summary>
        /// Gets or sets the current file name of a search or a replace process.
        /// </summary>
        public string CurrentFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the size of the current in characters of a search or a replace process.
        /// </summary>
        public long CurrentFileLength { get; set; } = 0;

        /// <summary>
        /// Gets or sets the current result count a search or a replace process.
        /// </summary>
        public long ResultCount { get; set; } = 0;

        /// <summary>
        /// Gets or sets the current position in a file of a search or a replace process.
        /// </summary>
        public long CurrentFileSearchPosition { get; set; } = 0;

        /// <summary>
        /// Gets the progress percentage of the current search or replace operation.
        /// </summary>
        public int ProgressPercentage
        {
            get
            {
                if (CurrentFileSearchPosition == 0)
                {
                    return 0;
                }
                
                if ((int) CurrentFileLength * 100 / (int) CurrentFileSearchPosition > 100)
                {
                    return 100;
                }

                return (int) CurrentFileLength * 100 / (int) CurrentFileSearchPosition;
            }
        }
    }
}
