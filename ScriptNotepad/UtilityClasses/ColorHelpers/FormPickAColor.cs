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

using System;
using System.Drawing;
using System.Windows.Forms;
using ScintillaNET;
using VPKSoft.LangLib;
using ColorMine.ColorSpaces;

namespace ScriptNotepad.UtilityClasses.ColorHelpers
{
    /// <summary>
    /// A class to detect if the <see cref="Scintilla"/> selection contains a color definition.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormPickAColor : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormPickAColor"/> class.
        /// </summary>
        public FormPickAColor()
        {
            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages");
        }

        /// <summary>
        /// A field to hold the current instance of this class.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static FormPickAColor formPickAColor;

        /// <summary>
        /// Displays the dialog with a specified color.
        /// </summary>
        /// <param name="color">The color to use with the dialog.</param>
        /// <returns>Color.</returns>
        public static Color Execute(Color color)
        {
            formPickAColor = new FormPickAColor();
            formPickAColor.UpdateColor(color);
            formPickAColor.ShowDialog();
            return formPickAColor.pnColor.BackColor;
        }

        private Color UpdateColor(Color color)
        {
            pnColor.BackColor = color;
            // ReSharper disable once LocalizableElement
            // ReSharper disable once StringLiteralTypo
            tbColorFromArgb.Text = $"Color.FromArgb({color.A}, {color.R}, {color.G}, {color.B})";

            // ReSharper disable once LocalizableElement
            tbHexRGB.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";

            // ReSharper disable once LocalizableElement
            tbHexARGB.Text = $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
                    // ReSharper disable once StringLiteralTypo

            Hex hex = new Hex($"#{color.R:X2}{color.G:X2}{color.B:X2}");
        
            var hsb = hex.To<Hsb>();

            // ReSharper disable once LocalizableElement
            tbHSB.Text =
                $"hsb({(int) hsb.H}, {(int)hsb.S * 100}%, {(int)hsb.B * 100}%)";

            var hsv = hex.To<Hsv>();
            // ReSharper disable once LocalizableElement
            tbHSV.Text =
                $"hsv({(int) hsv.H}, {(int) hsv.S * 100}%, {(int) hsv.V * 100}%)";

            var hsl = hex.To<Hsl>();
            // ReSharper disable once LocalizableElement
            tbHSL.Text =
                $"hsl({(int) hsl.H}, {(int) hsl.S * 100}%, {(int) hsl.L * 100}%)";

            // ReSharper disable once IdentifierTypo
            var cmyk = hex.To<Cmyk>();

            // ReSharper disable once LocalizableElement
            tbCMYK.Text =
                // ReSharper disable once StringLiteralTypo
                $@"cmyk({(int)cmyk.C * 100}%, {(int) cmyk.M * 100}%, {(int) cmyk.Y * 100}%, {(int)cmyk.K * 100}%)";

            return color;
        }

        /// <summary>
        /// Handles the Click event of the PnColor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PnColor_Click(object sender, EventArgs e)
        {
            if (cdColors.ShowDialog() == DialogResult.OK)
            {
                pnColor.BackColor = UpdateColor(cdColors.Color);
            }
        }
    }
}
