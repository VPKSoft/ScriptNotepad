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

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using VPKSoft.LangLib;
using VPKSoft.SearchText;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    /// <summary>
    /// A dialog to report various length process states.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogSearchReplaceProgress : DBLangEngineWinforms
    {
        private Action Action { get; set; }

        private TextSearcherAndReplacer TextSearcher { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogSearchReplaceProgress"/> class.
        /// </summary>
        /// <param name="action">The action to perform in a <see cref="BackgroundWorker"/>.</param>
        /// <param name="textSearcher">The text searcher class intance.</param>
        public FormDialogSearchReplaceProgress(Action action, TextSearcherAndReplacer textSearcher)
        {
            if (!ConstructorHelper())
            {
                return;
            }

            textSearcher.SearchProgress += SearchOpenDocuments_SearchProgress;
            TextSearcher = textSearcher;

            Action = action;

            ShowDialog();
        }

        private bool ConstructorHelper()
        {
            InitializeComponent();
            
            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return false; // After localization don't do anything more..
            }

            return true;
        }

        private void SearchOpenDocuments_SearchProgress(object sender, TextSearcherEventArgs e)
        {
            pbMain.Invoke(new MethodInvoker(delegate { pbMain.Value = e.Percentage; }));
            lbProgressDesc.Invoke(new MethodInvoker(delegate
            {
                lbProgressDesc.Text = DBLangEngine.GetMessage("msgSearchProgress",
                    "File: {0}, Progress: {1}|A message describing a search or replace progress with a file name and a progress percentage",
                    e.FileName, e.Percentage);
            }));
        }

        private void BwMain_DoWork(object sender, DoWorkEventArgs e)
        {
            Action();
        }

        private void FormDialogCommonProgress_Shown(object sender, EventArgs e)
        {
            bwMain.RunWorkerAsync();
        }

        private void BwMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate { DialogResult = DialogResult.OK; }));
        }

        private void FormDialogSearchReplaceProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            TextSearcher.Cancelled = true;
        }
    }
}
