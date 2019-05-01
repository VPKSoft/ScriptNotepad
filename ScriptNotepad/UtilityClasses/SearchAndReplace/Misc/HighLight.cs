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

using System.Drawing;
using ScintillaNET;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace.Misc
{
    /// <summary>
    /// A helper class to highlight words with a given color.
    /// </summary>
    public class Highlight
    {
        /// <summary>
        /// Highlights a given word (string) with a given color and given alpha values.
        /// </summary>
        /// <param name="scintilla">The scintilla of which words to highlight.</param>
        /// <param name="num">The indicator number for the <paramref name="scintilla"/>.Indicators 0-7 could be in use by a lexer so use a higher value.</param>
        /// <param name="text">The text to highlight.</param>
        /// <param name="color">The color to use for the highlight.</param>
        /// <param name="alpha">The transparency value of the indicator.</param>
        /// <param name="outlineAlpha">The transparency value of the indicator outline.</param>
        /// <note>(C): https://github.com/jacobslusser/ScintillaNET/wiki/Find-and-Highlight-Words</note>
        public static void HighlightWord(Scintilla scintilla, int num, string text, Color color, byte alpha = 255, byte outlineAlpha = 255)
        {
            if (string.IsNullOrEmpty(text))
                return;

            // Remove all uses of our indicator
            scintilla.IndicatorCurrent = num;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);

            // Update indicator appearance
            scintilla.Indicators[num].Style = IndicatorStyle.StraightBox;
            scintilla.Indicators[num].Under = true;
            scintilla.Indicators[num].ForeColor = color;
            scintilla.Indicators[num].OutlineAlpha = alpha;
            scintilla.Indicators[num].Alpha = outlineAlpha;

            // Search the document
            scintilla.TargetStart = 0;
            scintilla.TargetEnd = scintilla.TextLength;
            scintilla.SearchFlags = SearchFlags.None;
            while (scintilla.SearchInTarget(text) != -1)
            {
                // Mark the search results with the current indicator
                scintilla.IndicatorFillRange(scintilla.TargetStart, scintilla.TargetEnd - scintilla.TargetStart);

                // Search the remainder of the document
                scintilla.TargetStart = scintilla.TargetEnd;
                scintilla.TargetEnd = scintilla.TextLength;
            }
        }

        /// <summary>
        /// Clears the given style form a <see cref="Scintilla"/>.
        /// </summary>
        /// <param name="scintilla">The scintilla to clear the style from.</param>
        /// <param name="num">The number of the style to clear.</param>
        public static void ClearStyle(Scintilla scintilla, int num)
        {
            scintilla.IndicatorCurrent = num;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);
        }
    }
}
