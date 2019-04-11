using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <param name="text">The text to highlight.</param>
        /// <param name="color">The color to use for the highlight.</param>
        /// <param name="alpha">The transparency value of the indicator.</param>
        /// <param name="outlineAlpha">The transparency value of the indicator outline.</param>
        /// <note>(C): https://github.com/jacobslusser/ScintillaNET/wiki/Find-and-Highlight-Words</note>
        public static void HighlightWord(Scintilla scintilla, string text, Color color, byte alpha = 255, byte outlineAlpha = 255)
        {
            if (string.IsNullOrEmpty(text))
                return;

            // Indicators 0-7 could be in use by a lexer
            // so we'll use indicator 8 to highlight words.
            const int NUM = 8;

            // Remove all uses of our indicator
            scintilla.IndicatorCurrent = NUM;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);

            // Update indicator appearance
            scintilla.Indicators[NUM].Style = IndicatorStyle.StraightBox;
            scintilla.Indicators[NUM].Under = true;
            scintilla.Indicators[NUM].ForeColor = color;
            scintilla.Indicators[NUM].OutlineAlpha = alpha;
            scintilla.Indicators[NUM].Alpha = outlineAlpha;

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
    }
}
