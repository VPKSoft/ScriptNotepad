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
        /// <summary>
        /// Gets or sets the action to run in the background.
        /// </summary>
        private Action Action { get; set; }

        /// <summary>
        /// Gets or sets the text searcher to be used with the search.
        /// </summary>
        private TextSearcherAndReplacer TextSearcher { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogSearchReplaceProgress"/> class.
        /// </summary>
        /// <param name="action">The action to perform in a <see cref="BackgroundWorker"/>.</param>
        /// <param name="textSearcher">The text searcher class instance.</param>
        public FormDialogSearchReplaceProgress(Action action, TextSearcherAndReplacer textSearcher)
        {
            if (!ConstructorHelper())
            {
                return;
            }

            // initialize the language/localization database..
            DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages");

            textSearcher.SearchProgress += SearchOpenDocuments_SearchProgress;
            TextSearcher = textSearcher;

            Action = action;

            ShowDialog();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogSearchReplaceProgress"/> class.
        /// </summary>
        public FormDialogSearchReplaceProgress()
        {
            // a constructor for localization purposes only..
            ConstructorHelper();
        }

        /// <summary>
        /// A helper for multiple constructors.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the execution can continue after this call (no localization process), <c>false</c> otherwise.</returns>
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


        /// <summary>
        /// Handles the SearchProgress event of the SearchOpenDocuments control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextSearcherEventArgs"/> instance containing the event data.</param>
        private void SearchOpenDocuments_SearchProgress(object sender, TextSearcherEventArgs e)
        {
            // invocation is required as this is coming from another thread..
            pbMain.Invoke(new MethodInvoker(delegate { pbMain.Value = e.Percentage; }));
            lbProgressDesc.Invoke(new MethodInvoker(delegate
            {
                lbProgressDesc.Text = DBLangEngine.GetMessage("msgSearchProgress",
                    "File: {0}, Progress: {1}|A message describing a search or replace progress with a file name and a progress percentage",
                    e.FileName, e.Percentage);
            }));
        }

        // just run the action with the BackgroundWorker's DoWork event..
        private void BwMain_DoWork(object sender, DoWorkEventArgs e)
        {
            
            Action();
        }

        // run the BackgroundWorker on the dialog shown event..
        private void FormDialogCommonProgress_Shown(object sender, EventArgs e)
        {
            bwMain.RunWorkerAsync();
        }

        // the BackgroundWorker worker has completed running the action, so allow the dialog to be closed..
        private void BwMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            NoReactOnClosing = true;
            Invoke(new MethodInvoker(delegate { DialogResult = DialogResult.OK; }));
        }

        // the dialog is closing..
        private void FormDialogSearchReplaceProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if the flag is set, allow the dialog to close..
            if (NoReactOnClosing) 
            {
                return;
            }

            // set the flag for the next round..
            NoReactOnClosing = true;

            // cancel the Close(); ..
            e.Cancel = true;

            // cancel the TextSearcherAndReplacer processing..
            TextSearcher.Cancelled = true;

            // indicate that the dialogs background processing has been cancelled..
            Cancelled = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the code in the form's closing event should be run.
        /// </summary>
        private bool NoReactOnClosing { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FormDialogSearchReplaceProgress"/> search or replace <see cref="Action"/> was cancelled.
        /// </summary>
        /// <value><c>true</c> if cancelled; otherwise, <c>false</c>.</value>
        public bool Cancelled { get; set; }
    }
}
