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

using ScintillaNET;
using ScriptNotepad.UtilityClasses.TextManipulation.Interfaces;

namespace ScriptNotepad.UtilityClasses.TextManipulation.BaseClasses
{
    /// <summary>
    /// A base class for all the simple text manipulation utility classes.
    /// Implements the <see cref="ITextManipulationCommand" />
    /// </summary>
    /// <seealso cref="ITextManipulationCommand" />
    public abstract class TextManipulationCommandBase: ITextManipulationCommand
    {
        /// <summary>
        /// Manipulates the specified text value.
        /// </summary>
        /// <param name="value">The value to manipulate.</param>
        /// <returns>A string containing the manipulated text.</returns>
        public abstract string Manipulate(string value);

        /// <summary>
        /// Gets or sets a value indicating whether prefer selected text of the <see cref="Scintilla" /> control for the command.
        /// </summary>
        /// <value><c>true</c> if to prefer selected text of the <see cref="Scintilla" /> control for the command; otherwise, <c>false</c>.</value>
        public abstract bool PreferSelectedText { get; set; }

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
