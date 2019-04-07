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
using ScintillaNET;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace.Misc
{
    /// <summary>
    /// A class to help create a multi-selection for <see cref="Scintilla"/> document when user double-clicks a word.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class DoubleClickSelectAllHelper: ErrorHandlingBase
    {
        /// <summary>
        /// Selects all matching texts of the <paramref name="scintilla"/> based on a single selection text.
        /// </summary>
        /// <param name="scintilla">The scintilla to select the matching texts from.</param>
        /// <returns><c>true</c> if the operation was successful; otherwise <c>false</c>.</returns>
        public static bool MultiSelectFromSelection(Scintilla scintilla)
        {
            try
            {
                scintilla.SuspendLayout();
                scintilla.MultipleSelection = true;
                scintilla.TargetStart = 0;
                scintilla.TargetEnd = scintilla.TextLength;
                scintilla.MultipleSelectAddEach();
                scintilla.ResumeLayout();
                return true;
            }
            catch (Exception ex)
            {
                scintilla.ResumeLayout();
                ExceptionLogAction?.Invoke(ex);
                return false;
            }
        }

    }
}
