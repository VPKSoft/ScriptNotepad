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
using VPKSoft.SearchText;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    /// <summary>
    /// A class for the <see cref="FormDialogSearchReplaceProgressFiles"/> class to request an action for the next file.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ProgressRequestActionEventArgs: EventArgs
    {
        /// <summary>
        /// Gets or sets the action to process next.
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip processing of the file.
        /// </summary>
        public bool SkipFile { get; set; } = false;

        /// <summary>
        /// Gets or sets the name of the file for to be processed next.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ProgressRequestActionEventArgs"/> is canceled;
        /// I.e. no more processing is being done within the <see cref="FormDialogSearchReplaceProgressFiles"/> form instance.
        /// </summary>
        public bool Canceled { get; set; }

        /// <summary>
        /// Gets or sets the current instance of the <see cref="TextSearcherAndReplacer"/> class.
        /// </summary>
        public TextSearcherAndReplacer SearchAndReplacer { get; set; }
    }
}
