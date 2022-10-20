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
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.GraphicUtils;

/// <summary>
/// A class with helper methods for the <see cref="FontFamily"/> class.
/// </summary>
public static class FontFamilyHelpers
{
    /// <summary>
    /// Determines whether the given font family is fixed width (mono-space).
    /// </summary>
    /// <param name="fontFamily">The font family to check for.</param>
    // (C): https://social.msdn.microsoft.com/Forums/windows/en-US/5b582b96-ade5-4354-99cf-3fe64cc6b53b/determining-if-font-is-monospaced?forum=winforms
    public static bool IsFixedWidth(this FontFamily fontFamily)
    {
        char[] measureChars = { 'i', 'a', 'Z', '%', '#', 'a', 'B', 'l', 'm', ',', '.', };
        if (Form.ActiveForm != null)
        {
            using (Graphics graphics = Graphics.FromHwnd(Form.ActiveForm.Handle))
            {
                try
                {
                    using (Font font = new Font(fontFamily, 10, FontStyle.Regular, GraphicsUnit.Pixel))
                    {
                        float charWidth = graphics.MeasureString("I", font).Width;
                        foreach (var measureChar in measureChars)
                        {
                            // ReSharper disable once CompareOfFloatsByEqualityOperator
                            if (graphics.MeasureString(measureChar.ToString(), font).Width != charWidth)
                            {
                                return false;
                            }
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        return true;
    }
}