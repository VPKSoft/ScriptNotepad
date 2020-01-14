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
using System.Windows.Forms;
using ScintillaNET;
using VPKSoft.LangLib;

namespace ScriptNotepad.DialogForms
{
    /// <summary>
    /// A simple goto dialog for the <see cref="Scintilla"/>.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogQueryJumpLocation : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogQueryJumpLocation"/> class.
        /// </summary>
        public FormDialogQueryJumpLocation()
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

        private Scintilla Scintilla { get; set; }

        /// <summary>
        /// Displays the goto dialog with a specified <see cref="Scintilla"/> instance.
        /// </summary>
        /// <param name="scintilla">The Scintilla instance to use with the dialog.</param>
        public static void Execute(Scintilla scintilla)
        {
            if (1 >= scintilla.Lines.Count)
            {
                return;
            }

            var form = new FormDialogQueryJumpLocation
            {
                Scintilla = scintilla,
                nudGoto =
                {
                    Minimum = 1,
                    Maximum = scintilla.Lines.Count,
                    Value = scintilla.LineFromPosition(scintilla.CurrentPosition)
                },
                lbEnterLineNumber =
                {
                    Text = DBLangEngine.GetStatMessage("msgEnterALineNumber",
                        "Enter a line number ({0} - {1}):|A message for a label describing an input control to select a line number",
                        1, scintilla.Lines.Count)
                }
            };
            form.ShowDialog();
        }

        // occurs when a user selects a line number to jump to..
        private void BtGo_Click(object sender, EventArgs e)
        {
            Scintilla.GotoPosition(Scintilla.Lines[(int) nudGoto.Value - 1].Position);
            DialogResult = DialogResult.OK;
        }

        // occurs when a user selects a line number to jump to..
        private void FormDialogQueryJumpLocation_Shown(object sender, EventArgs e)
        {
            nudGoto.Select(0, nudGoto.Text.Length);
        }
    }
}
