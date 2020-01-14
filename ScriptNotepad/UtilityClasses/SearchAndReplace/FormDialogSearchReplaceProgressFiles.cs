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
using System.ComponentModel;
using System.Windows.Forms;
using ScriptNotepad.UtilityClasses.IO;
using VPKSoft.ErrorLogger;
using VPKSoft.LangLib;
using VPKSoft.SearchText;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    /// <summary>
    /// A dialog to display search results of files and the search and/or replace progress being done to the files.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogSearchReplaceProgressFiles : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogSearchReplaceProgressFiles"/> class. This is just for localization.
        /// </summary>
        public FormDialogSearchReplaceProgressFiles()
        {
            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
            }
        }

        /// <summary>
        /// A delegate for the <see cref="FormDialogSearchReplaceProgressFiles.RequestNextAction"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="ProgressRequestActionEventArgs"/> instance containing the event data.</param>
        public delegate void OnRequestNextAction(object sender, ProgressRequestActionEventArgs e);

        /// <summary>
        /// Occurs when the form requests for a next action for the next file processing.
        /// </summary>
        public event OnRequestNextAction RequestNextAction;

        /// <summary>
        /// A field to save the delegate for the <see cref="RequestNextAction"/> event as this form also unsubscribes the event handler on disposal.
        /// </summary>
        private readonly OnRequestNextAction requestNextAction;

        /// <summary>
        /// Gets or sets an instance to the <see cref="DirectoryCrawler"/>.
        /// </summary>
        private DirectoryCrawler Crawler { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogSearchReplaceProgressFiles"/> class.
        /// </summary>
        /// <param name="crawler">An instance for the <see cref="DirectoryCrawler"/> class initialized elsewhere.</param>
        /// <param name="requestNextAction">A delegate for the <see cref="RequestNextAction"/> event as this class also unsubscribes the event handler.</param>
        public FormDialogSearchReplaceProgressFiles(DirectoryCrawler crawler, OnRequestNextAction requestNextAction)
        {            
            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            // initialize the language/localization database..
            DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages");

            // subscribe the disposed event..
            Disposed += FormDialogSearchReplaceProgressFiles_Disposed;

            // save the delegate for the RequestNextAction so it can be unsubscribed on this form's disposal..
            this.requestNextAction = requestNextAction;

            // subscribe the event with the delegate given in the parameter..
            RequestNextAction += requestNextAction;

            // subscribe to the ReportProgress event of the DirectoryCrawler instance..
            crawler.ReportProgress += Crawler_ReportProgress;

            // save the directory crawler instance..
            Crawler = crawler;

            // show this form as a dialog..
            ShowDialog();
        }

        /// <summary>
        /// Gets or sets the either the previous or the current instance of <see cref="TextSearcherAndReplacer"/> class.
        /// </summary>
        private TextSearcherAndReplacer SearchAndReplacer { get; set; }

        /// <summary>
        /// Handles the ReportProgress event of the <see cref="DirectoryCrawler"/> class.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DirectoryCrawlerEventArgs"/> instance containing the event data.</param>
        private void Crawler_ReportProgress(object sender, DirectoryCrawlerEventArgs e)
        {
            // try as program should go down with this..
            try
            {
                // there is a 99.999xx% change of the event to be raised from a another thread..
                lbStatus.Invoke(new MethodInvoker(delegate
                {
                    // check if a directory is being processed..
                    if (!string.IsNullOrEmpty(e.CurrentDirectory))
                    {
                        // ..and set the status text accordingly..
                        lbStatus.Text = DBLangEngine.GetMessage("msgProcessingDirectory",
                            "Processing directory: '{0}'...|A message indicating that a directory with a given name is being processed somehow",
                            e.CurrentDirectory);
                    }
                }));

                // AGAIN: there is a 99.999xx% change of the event to be raised from a another thread..
                lbStatus2.Invoke(new MethodInvoker(delegate
                {
                    // check if a file is currently being processed..
                    if (!string.IsNullOrEmpty(e.FileName))
                    {
                        // ..and set the status text accordingly..
                        lbStatus2.Text = DBLangEngine.GetMessage("msgProcessingFile",
                            "Processing file: '{0}'...|A message indicating that a file with a given name is being processed somehow",
                            e.FileName);
                    }
                    else
                    {
                        // ..no file is currently being processed..
                        lbStatus2.Text = string.Empty;
                    }
                }));
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);
            }

            // try as program should go down with this..
            try
            {
                // if the DirectoryCrawler instance has found a file name to process..
                if (!string.IsNullOrEmpty(e.FileName))
                {
                    // create a new instance of the ProgressRequestActionEventArgs class..
                    var args = new ProgressRequestActionEventArgs
                        {Canceled = false, FileName = e.FileName, SearchAndReplacer = SearchAndReplacer};

                    // raise the event if subscribed.. actually an exception should be raised if not subscribed in this case..
                    RequestNextAction?.Invoke(this, args);

                    // if the file has requested to be skipped to just continue the "crawling"..
                    if (args.SkipFile)
                    {
                        return;
                    }

                    // first unsubscribe the SearchProgress event of the previous TextSearcherAndReplacer class instance..
                    if (SearchAndReplacer != null)
                    {
                        // ..after a null check..
                        SearchAndReplacer.SearchProgress -= SearchOpenDocuments_SearchProgress;
                    }

                    // save the TextSearcherAndReplacer class instance from the event arguments..
                    SearchAndReplacer = args.SearchAndReplacer;

                    // subscribe the SearchProgress event for the TextSearcherAndReplacer class instance..
                    if (SearchAndReplacer != null)
                    {
                        // ..after a null check..
                        SearchAndReplacer.SearchProgress += SearchOpenDocuments_SearchProgress;
                    }

                    // invoke the action to "this thread" from the event arguments..
                    args.Action?.Invoke();
                }
                else
                {
                    // no file is currently being processed so do set the progress bar value to zero..
                    pbMain.Invoke(new MethodInvoker(delegate { pbMain.Value = 0; }));
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);
            }
        }

        /// <summary>
        /// This event is fired when the <see cref="TextSearcherAndReplacer"/> class instance reports internal progress.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="T:VPKSoft.SearchText.TextSearcherEventArgs" /> instance containing the event data.</param>
        public void SearchOpenDocuments_SearchProgress(object sender, TextSearcherEventArgs e)
        {
            // as this event is fired from another thread, do invoke..
            pbMain.Invoke(new MethodInvoker(delegate
            {
                // ..set the progress bar value to the value given from the TextSearcherAndReplacer class instance..
                pbMain.Value = e.Percentage;
            }));
        }

        /// <summary>
        /// Handles the Disposed event of the FormDialogSearchReplaceProgressFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FormDialogSearchReplaceProgressFiles_Disposed(object sender, EventArgs e)
        {
            // unsubscribe the events which subscriptions where made in the "code" (does not include *.Designer.cs)..
            RequestNextAction -= requestNextAction;
            Disposed -= FormDialogSearchReplaceProgressFiles_Disposed;
            Crawler.ReportProgress -= Crawler_ReportProgress;
            if (SearchAndReplacer != null) // this one requires a null check..
            {
                SearchAndReplacer.SearchProgress -= SearchOpenDocuments_SearchProgress;
            }
        }

        /// <summary>
        /// Handles the Shown event of the FormDialogSearchReplaceProgressFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FormDialogSearchReplaceProgressFiles_Shown(object sender, EventArgs e)
        {
            // this form needs to be the top most as it is a dialog..
            TopMost = true;
            BringToFront();
            
            // run the BackgroundWorker so the directory "crawling" can start..
            bwMain.RunWorkerAsync();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this form is allowed to close.
        /// </summary>
        private bool AllowClose { get; set; }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bwMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BwMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // all the work is done, so do allow this dialog to close..
            AllowClose = true; 
            // ..and close the dialog..
            Close(); 
        }

        /// <summary>
        /// Handles the DoWork event of the bwMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void BwMain_DoWork(object sender, DoWorkEventArgs e)
        {
            // search the directory and possibly it's subdirectories for files..
            Crawler.GetCrawlResult();
        }

        /// <summary>
        /// Handles the FormClosing event of the FormDialogSearchReplaceProgressFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void FormDialogSearchReplaceProgressFiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            // don't allow the user to close the form..
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // ..instead indicate classes working in the background worker to stop..
                if (SearchAndReplacer != null)
                {
                    SearchAndReplacer.Canceled = true;
                }
                Crawler.Canceled = true;

                // sometimes this false accuses of the user..
                e.Cancel = !AllowClose;
            }
        }
    }
}
