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

using ScriptNotepad.UtilityClasses.ExternalProcessInteraction;
using ScriptNotepad.UtilityClasses.Process;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ScriptNotepad.DialogForms;
using ScriptNotepad.Localization.Hunspell;

namespace ScriptNotepad.Test
{
    /// <summary>
    /// A test form for various things (...).
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormTestThings : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormTestThings"/> class.
        /// </summary>
        public FormTestThings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(FormDialogQueryEncoding.Execute().ToString());

//            Printing printer = new Printing(sttcMain.Documents[0].Scintilla);
//            printer.PrintPreview();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowsExplorerInteraction.ShowFileOrPathInExplorer(@"C:\Files\GitHub\ScintillaLexers\obj\Release\TempPE");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ProcessElevation.IsElevated.ToString());
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Regex.Unescape(textBox1.Text));
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                textBox3.Text.LastIndexOf(textBox2.Text, textBox3.TextLength - 1, textBox3.TextLength
                        )
                    .ToString());
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.MinValue.ToLongDateString());
            /*
            MessageBox.Show(
                FormSettings.Settings.FromDataGrid(((FormSettings) Application.OpenForms[1]).dgvEncodings).ToString());
                */
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            var data = HunspellData.FromDictionaryFile(@"C:\Files\GitHub\dictionaries\en\en_US.dic");
            MessageBox.Show(data.HunspellCulture.ToString());

            var result = HunspellDictionaryCrawler.CrawlDirectory(@"C:\Files\GitHub\dictionaries");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBoxExtended.Show(this, "Helevetin helevetin helevetti!", "Testing..",
                MessageBoxButtonsExtended.YesNo, MessageBoxIcon.Hand);
        }
    }
}
