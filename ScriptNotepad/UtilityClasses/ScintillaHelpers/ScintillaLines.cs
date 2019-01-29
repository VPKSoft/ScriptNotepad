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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScintillaNET;

namespace ScriptNotepad.UtilityClasses.ScintillaHelpers
{
    /// <summary>
    /// A helper class to manipulate a Scintilla document lines as a list of strings.
    /// </summary>
    public static class ScintillaLines
    {
        /// <summary>
        /// Gets the lines of a Scintilla as list of strings.
        /// </summary>
        /// <param name="scintilla">The Scintilla to get the line contents from.</param>
        /// <returns>A list of strings of the lines of a Scintilla.</returns>
        public static List<string> GetLinesAsList(Scintilla scintilla)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < scintilla.Lines.Count; i++)
            {
                result.Add(scintilla.Lines[i].Text);
            }
            return result;
        }

        /// <summary>
        /// Sets the lines of a Scintilla document from a given list of strings.
        /// </summary>
        /// <param name="scintilla">A Scintilla document of which lines to set.</param>
        /// <param name="lines">A list of strings to be used to set the Scintilla document's contents from.</param>
        /// <param name="lineEnding">A line ending string to append to a string with no line ending.</param>
        public static void SetLinesFromList(Scintilla scintilla, List<string> lines, string lineEnding)
        {
            // ensure that the lines have a line ending..
            for (int i = 0; i < lines.Count; i++)
            {
                // check for a possible line endings (not sure if the "\r\n" is valid)..
                if (lines[i].EndsWith("\n") ||
                    lines[i].EndsWith("\r") ||
                    lines[i].EndsWith("\n\r") ||
                    lines[i].EndsWith("\r\n"))
                {
                    continue; // a line ending was found so do continue..
                }

                // a line ending wasn't found so give it a line ending..
                lines[i] = lines[i] + lineEnding;
            }

            // concatenate the lines and set the contents of the Scintilla document..
            scintilla.Text = string.Concat(lines);
        }
    }
}
