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

using System.Drawing;
using ScintillaNET;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using VPKSoft.ScintillaLexers.ScintillaNotepadPlusPlus;


namespace ScriptNotepad.Settings.XmlNotepadPlusMarks
{
    /// <summary>
    /// A class to load the mark colors (highlight) from a Notepad++ style definition file.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    class MarkColorsHelper : ErrorHandlingBase
    {
        /// <summary>
        /// Gets or sets the color for smart highlight for the <see cref="Scintilla"/> document.
        /// </summary>
        public Color SmartHighlight { get; set; }

        /// <summary>
        /// Gets or sets the color for the mark one for the <see cref="Scintilla"/> document.
        /// </summary>
        public Color Mark1Color { get; set; }

        /// <summary>
        /// Gets or sets the color for the mark two for the <see cref="Scintilla"/> document.
        /// </summary>
        public Color Mark2Color { get; set; }

        /// <summary>
        /// Gets or sets the color for the mark three for the <see cref="Scintilla"/> document.
        /// </summary>
        public Color Mark3Color { get; set; }

        /// <summary>
        /// Gets or sets the color for the mark four for the <see cref="Scintilla"/> document.
        /// </summary>
        public Color Mark4Color { get; set; }

        /// <summary>
        /// Gets or sets the color for the mark five for the <see cref="Scintilla"/> document.
        /// </summary>
        public Color Mark5Color { get; set; }

        /// <summary>
        /// Gets or sets the color for the current line background.
        /// </summary>
        public Color CurrentLineBackground { get; set; }

        /// <summary>
        /// Creates a new instance of "this" class from a Notepad++ style definition XML file.
        /// </summary>
        /// <param name="fileName">Name of the XML file.</param>
        /// <returns>MarkColorsHelper.</returns>
        public static MarkColorsHelper FromFile(string fileName)
        {
            try
            {
                var markColors = MarkColors.FromFile(fileName);

                return new MarkColorsHelper
                {
                    SmartHighlight = markColors.SmartHighLightingBackground,
                    Mark1Color = markColors.MarkOneBackground,
                    Mark2Color = markColors.MarkTwoBackground,
                    Mark3Color = markColors.MarkThreeBackground,
                    Mark4Color = markColors.MarkFourBackground,
                    Mark5Color = markColors.MarkFiveBackground,
                    CurrentLineBackground = markColors.CurrentLineBackground
                };
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);

                // ..and return default..
                return default;
            }
        }
    }
}
