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
using System.Linq;
using ScintillaNET;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.UtilityClasses.TextManipulation
{
    /// <summary>
    /// A class for sorting lines within a <see cref="Scintilla"/> control.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class SortLines: ErrorHandlingBase
    {
        public static void Sort(Scintilla scintilla, StringComparison stringComparison)
        {
            try
            {
                // if text is selected, do the ordering with a bit more complex algorithm..
                if (scintilla.SelectedText.Length > 0)
                {
                    // save the selection start into a variable..
                    int selStart = scintilla.SelectionStart;

                    // save the start line index of the selection into a variable..
                    int startLine = scintilla.LineFromPosition(selStart);

                    // adjust the selection start to match the actual line start of the selection..
                    selStart = scintilla.Lines[startLine].Position;

                    // save the selection end into a variable..
                    int selEnd = scintilla.SelectionEnd;

                    // save the end line index of the selection into a variable..
                    int endLine = scintilla.LineFromPosition(selEnd);

                    // adjust the selection end to match the actual line end of the selection..
                    selEnd = scintilla.Lines[endLine].EndPosition;

                    // reset the selection with the "corrected" values..
                    scintilla.SelectionStart = selStart;
                    scintilla.SelectionEnd = selEnd;

                    // get the lines in the selection and order the lines alphabetically with LINQ..
                    var lines = scintilla.Lines.Where(f => f.Index >= startLine && f.Index <= endLine)
                        .OrderBy(f => f.Text.ToComparisonVariant(stringComparison)).Select(f => f.Text);

                    // replace the modified selection with the sorted lines..
                    scintilla.ReplaceSelection(string.Join("", lines));

                    // get the "new" selection start..
                    selStart = scintilla.Lines[startLine].Position;

                    // get the "new" selection end..
                    selEnd = scintilla.Lines[endLine].EndPosition;

                    // set the "new" selection..
                    scintilla.SelectionStart = selStart;
                    scintilla.SelectionEnd = selEnd;
                }
                // somehow the whole document is easier..
                else
                {
                    // just LINQ it to sorted list..
                    var lines = scintilla.Lines.OrderBy(f => f.Text.ToComparisonVariant(stringComparison)).Select(f => f.Text);

                    // set the text..
                    scintilla.Text = string.Concat(lines);                    
                }
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
            }
        }
    }
}
