#region License
/*
MIT License

Copyright(c) 2021 Petteri Kautonen

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

using System.Collections.Generic;
using System.Linq;
using ScriptNotepad.UtilityClasses.TextManipulation.Interfaces;

namespace ScriptNotepad.UtilityClasses.TextManipulation.BaseClasses
{
    /// <summary>
    /// A base class for all the simple text manipulation utility classes.
    /// Implements the <see cref="ITextLinesManipulationCommand" />
    /// </summary>
    /// <seealso cref="ITextLinesManipulationCommand" />
    public abstract class TextLinesManipulationCommandBase: ITextLinesManipulationCommand
    {
        /// <summary>
        /// Manipulates the specified text value.
        /// </summary>
        /// <param name="lines">The lines to manipulate.</param>
        /// <param name="textEncoding">The encoding used in the</param>
        /// <returns>A string containing the manipulated text.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <remarks>The <paramref name="lines" /> may contain line separator character(s).</remarks>
        public abstract string Manipulate(IList<string> lines, Encoding textEncoding);

        /// <summary>
        /// Determines whether the <paramref name="lines"/> collection has new line characters at the end of the string.
        /// </summary>
        /// <param name="lines">The lines to check for new line characters.</param>
        /// <returns>bool.</returns>
        public static bool HasLineFeeds(IList<string> lines)
        {
            return lines.Any(f => f.EndsWith('\n') || f.EndsWith('\r'));
        }

        /// <summary>
        /// Gets or sets the name of the method manipulating the text.
        /// </summary>
        /// <value>The name of the method manipulating the text.</value>
        public string MethodName { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public abstract override string ToString();
    }
}
