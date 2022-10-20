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

using ScintillaNET;
using ScriptNotepad.Settings;

namespace ScriptNotepad.UtilityClasses.ScintillaUtils;

/// <summary>
/// A helper class to set the <see cref="Scintilla"/> brace highlighting.
/// </summary>
public class SetStyleBraceMatch
{
    /// <summary>
    /// Set the brace highlight style if set in the <see cref="Settings"/> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla"/> class instance of which brace highlighting to set.</param>
    public static void SetStyle(Scintilla scintilla)
    {
        // failure is not accepted within this class..
        if (scintilla == null)
        {
            return;
        }

        // not in the settings, so do return..
        if (!FormSettings.Settings.HighlightBraces)
        {
            return;
        }

        scintilla.Styles[Style.BraceLight].ForeColor = FormSettings.Settings.BraceHighlightForegroundColor;
        scintilla.Styles[Style.BraceLight].BackColor = FormSettings.Settings.BraceHighlightBackgroundColor;
        scintilla.Styles[Style.BraceBad].BackColor = FormSettings.Settings.BraceBadHighlightForegroundColor;

        scintilla.Styles[Style.BraceLight].Italic = FormSettings.Settings.HighlightBracesItalic;
        scintilla.Styles[Style.BraceLight].Bold = FormSettings.Settings.HighlightBracesBold;
    }
}