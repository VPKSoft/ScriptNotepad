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

using ScintillaNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ScriptNotepad.Settings;
using VPKSoft.LangLib;
using VPKSoft.PosLib;
using VPKSoft.SearchText;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    /// <summary>
    /// A search and replace dialog for the ScriptNotepad software.
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormSearchAndReplace : DBLangEngineWinforms
    {
        #region MassiveConstructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FormSearchAndReplace"/> class.
        /// </summary>
        private FormSearchAndReplace()
        {
            // don't allow other instances..
            if (_formSearchAndReplace != null)
            {
                return;
            }

            // Add this form to be positioned..
            PositionForms.Add(this);

            // add positioning..
            PositionCore.Bind();

            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages");

            AllowTransparency = true;

            Documents = GetDocuments(true);

            CurrentDocument = GetCurrentDocument();

            ttMain.SetToolTip(rbSimpleExtended, DBLangEngine.GetMessage("msgSimpleExtendedDesc", "? = one character, * = multiple characters, # = digit, % = single digit|A message describing the usage of the simple extended search algorithm (meant for demented persons, such as me)."));
            ttMain.SetToolTip(rbSimpleExtended2, DBLangEngine.GetMessage("msgSimpleExtendedDesc", "? = one character, * = multiple characters, # = digit, % = single digit|A message describing the usage of the simple extended search algorithm (meant for demented persons, such as me)."));
        }
        #endregion

        #region PublicStaticProperties
        /// <summary>
        /// Gets or sets a value indicating whether to reset the search area upon instance creation or not.
        /// </summary>
        public static bool ResetSearchArea { get; set; } = true;

        /// <summary>
        /// The form search and replace instance holder.
        /// </summary>
        private static FormSearchAndReplace _formSearchAndReplace;

        /// <summary>
        /// Gets the instance of a <see cref="FormSearchAndReplace"/> dialog.
        /// </summary>
        public static FormSearchAndReplace Instance
        {
            get
            {
                if (_formSearchAndReplace == null)
                {
                    var form = new FormSearchAndReplace();
                    _formSearchAndReplace = form;
                }

                return _formSearchAndReplace;
            }
        }
        #endregion

        #region PublicStaticMethods
        /// <summary>
        /// Shows the form with the search tab page opened.
        /// </summary>
        public static void ShowSearch()
        {
            Instance.Show();
            Instance.tcMain.SelectTab(0);
            Instance.cmbFind.Focus();
        }

        /// <summary>
        /// Shows the form with the replace tab page opened.
        /// </summary>
        public static void ShowReplace()
        {
            Instance.Show();
            Instance.tcMain.SelectTab(1);
            Instance.cmbFind2.Focus();
        }

        /// <summary>
        /// Creates an instance of the dialog for localization.
        /// </summary>
        public static void CreateLocalizationInstance() => new FormSearchAndReplace();
        #endregion

        #region PublicMethods
        /// <summary>
        /// Toggles the visible state of this form.
        /// </summary>
        /// <param name="shouldShow">if set to <c>true</c> the form should be shown.</param>
        public void ToggleVisible(bool shouldShow)
        {
            if (!Visible && shouldShow)
            {
                Show();
                if (tcMain.SelectedTab.Equals(tabFind))
                {
                    cmbFind.Focus();
                }
                else if (tcMain.SelectedTab.Equals(tabReplace))
                {
                    cmbFind2.Focus();
                }
            }
            else if (Visible && !shouldShow)
            {
                Close();
            }
        }

        /// <summary>
        /// Toggles the TopMost property of this form.
        /// </summary>
        /// <param name="shouldStayOnTop">If set to <c>true</c> this form should be the top-most form of the application.</param>
        public void ToggleStayTop(bool shouldStayOnTop)
        {
            if (!shouldStayOnTop && TopMost)
            {
                TopMost = false;
            }
            else if (shouldStayOnTop && !TopMost)
            {
                TopMost = true;
                BringToFront();
            }
        }            
        #endregion

        #region PublicEvents
        /// <summary>
        /// A delegate for the <see cref="FormSearchAndReplace.RequestDocuments"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="ScintillaDocumentEventArgs"/> instance containing the event data.</param>
        public delegate void OnRequestDocuments(object sender, ScintillaDocumentEventArgs e);

        /// <summary>
        /// Occurs when the search and replace dialog requests access to the open documents on the main form.
        /// </summary>
        public event OnRequestDocuments RequestDocuments;
        #endregion

        #region PrivateAndInternalProperties
        /// <summary>
        /// Gets or sets the documents containing in the main form.
        /// </summary>
        internal List<(Scintilla scintilla, string fileName)> Documents { get; set; } = new List<(Scintilla scintilla, string fileName)>();

        private (Scintilla scintilla, string fileName) currentDocument;

        /// <summary>
        /// Gets or sets the currently active document in the main form.
        /// </summary>
        private (Scintilla scintilla, string fileName) CurrentDocument
        {
            get => currentDocument;

            set
            {
                if (currentDocument != value && currentDocument.scintilla != null)
                {
                    CreateSingleSearchReplaceAlgorithm(value);
                }

                currentDocument = value;
            }
        }

        private TextSearcherAndReplacer SearchOpenDocuments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to allow the singleton instance to be disposed of.
        /// </summary>
        public static bool AllowInstanceDispose { get; set; } = false;

        /// <summary>
        /// Gets the current search type.
        /// </summary>
        private TextSearcherEnums.SearchType SearchType
        {
            get
            {
                // based on the currently active tab, return the correct value..
                if (rbNormal.Checked && tcMain.SelectedTab.Equals(tabFind) ||
                    rbNormal2.Checked && tcMain.SelectedTab.Equals(tabReplace))
                {
                    return TextSearcherEnums.SearchType.Normal;
                }
                
                // based on the currently active tab, return the correct value..
                if (rbExtented.Checked && tcMain.SelectedTab.Equals(tabFind) ||
                    rbExtented2.Checked && tcMain.SelectedTab.Equals(tabReplace))
                {
                    return TextSearcherEnums.SearchType.Extended;
                }

                // based on the currently active tab, return the correct value..
                if (rbRegEx.Checked && tcMain.SelectedTab.Equals(tabFind) ||
                    rbRegEx2.Checked && tcMain.SelectedTab.Equals(tabReplace))
                {
                    return TextSearcherEnums.SearchType.RegularExpression;
                }

                // based on the currently active tab, return the correct value..
                if (rbSimpleExtended.Checked && tcMain.SelectedTab.Equals(tabFind) ||
                    rbSimpleExtended2.Checked && tcMain.SelectedTab.Equals(tabReplace))
                {
                    return TextSearcherEnums.SearchType.SimpleExtended;
                }

                // return the default value (NOTE: the code should never reach this point!)..
                return TextSearcherEnums.SearchType.Normal;
            }
        }

        /// <summary>
        /// A value indicating whether the <see cref="TransparencySettings_Changed"/> should suspend it self from executing any code.
        /// </summary>
        private bool SuspendTransparencyChangeEvent { get; set; } = false;
        #endregion

        #region PrivateMethods
        /// <summary>
        /// Creates the single search and/or replace algorithm for a given <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="scintilla">The scintilla and its file name to create the search algorithm from.</param>
        private void CreateSingleSearchReplaceAlgorithm((Scintilla scintilla, string fileName) scintilla)
        {
            // only create the algorithm if the the passed scintilla actually contains a ScintillaNET control..
            if (scintilla.scintilla != null)
            {
                if (SearchOpenDocuments != null)
                {
                    using (SearchOpenDocuments) // dispose of the previous algorithm..
                    {
                        // ..and unsubscribe the search/replace progress event..
                        SearchOpenDocuments.SearchProgress -= SearchOpenDocuments_SearchProgress;
                    }
                }

                // invoke as this method might be called from a thread..
                Invoke(new MethodInvoker(delegate
                {
                    // get the default controls for the search algorithm..
                    ComboBox _cmbFind = cmbFind;
                    CheckBox _cbWrapAround = cbWrapAround;
                    CheckBox _cbMatchCase = cbMatchCase;
                    CheckBox _cbMatchWholeWord = cbMatchWholeWord;

                    // get the controls matching the active tab..
                    if (tcMain.SelectedTab.Equals(tabReplace))
                    {
                        _cmbFind = cmbFind2;
                        _cbWrapAround = cbWrapAround2;
                        _cbMatchCase = cbMatchCase2;
                        _cbMatchWholeWord = cbMatchWholeWord2;
                    }

                    // create the search and replace algorithm..
                    SearchOpenDocuments =
                        new TextSearcherAndReplacer(scintilla.scintilla.Text, _cmbFind.Text, SearchType)
                        {
                            WrapAround = _cbWrapAround.Checked,
                            IgnoreCase = !_cbMatchCase.Checked,
                            FileName = scintilla.fileName,
                            WholeWord = _cbMatchWholeWord.Checked,
                            FileNameNoPath = Path.GetFileName(scintilla.fileName),
                        };

                    // subscribe the search progress event..
                    SearchOpenDocuments.SearchProgress += SearchOpenDocuments_SearchProgress;
                }));
            }
        }

        /// <summary>
        /// Searches to backward direction.
        /// </summary>
        private void Backward()
        {
            var result = SearchOpenDocuments?.Backward();
            if (result.HasValue)
            {
                GetCurrentDocument().scintilla.SelectionStart = result.Value.position;
                GetCurrentDocument().scintilla.SelectionEnd = result.Value.position + result.Value.length;
                GetCurrentDocument().scintilla.ScrollCaret();
            }
        }

        /// <summary>
        /// Searches to forward direction.
        /// </summary>
        private void Forward()
        {
            var result = SearchOpenDocuments?.Forward();
            if (result.HasValue)
            {
                GetCurrentDocument().scintilla.SelectionStart = result.Value.position;
                GetCurrentDocument().scintilla.SelectionEnd = result.Value.position + result.Value.length;
                GetCurrentDocument().scintilla.ScrollCaret();
            }
        }

        /// <summary>
        /// Gets the documents open in the main form.
        /// </summary>
        /// <param name="allDocuments">If set to <c>true</c> all the open documents are returned.</param>
        /// <returns>The document(s) currently opened on the main form.</returns>
        private List<(Scintilla scintilla, string fileName)> GetDocuments(bool allDocuments)
        {
            // create a ScintillaDocumentEventArgs class instance..
            ScintillaDocumentEventArgs scintillaDocumentEventArgs =
                new ScintillaDocumentEventArgs
                {
                    // set the value whether all the open documents are requested..
                    RequestAllDocuments = allDocuments
                };

            // if the event is subscribed, raise the event..
            RequestDocuments?.Invoke(this, scintillaDocumentEventArgs);

            // return the result..
            return scintillaDocumentEventArgs.Documents;
        }

        /// <summary>
        /// Gets the current document active in the main form.
        /// </summary>
        /// <returns>A Scintilla document currently active in the main form.</returns>
        private (Scintilla scintilla, string fileName) GetCurrentDocument()
        {
            return GetDocuments(false).Count > 0 ?
                GetDocuments(false)[0] : (null, null);
        }

        /// <summary>
        /// Toggles the transparency based on the saved transparency settings of the form.
        /// </summary>
        /// <param name="activated">A value indicating whether the form is activated.</param>
        private void TransparencyToggle(bool activated)
        {
            // suspend the change event..
            SuspendTransparencyChangeEvent = true;

            // set the enabled value of the transparency setting group boxes to enabled..
            gpTransparency.Enabled = true;
            gpTransparency2.Enabled = true;

            // transparency is disabled..
            if (FormSettings.Settings.SearchBoxTransparency == 0) 
            {
                // set the enabled value of the transparency setting group boxes to disabled..
                cbTransparency.Checked = false;
                cbTransparency2.Checked = false;

                gpTransparency.Enabled = false;
                gpTransparency2.Enabled = false;

                // set the opacity value to 100%..
                Opacity = 1;
            }
            // transparency is enabled while active..
            else if (FormSettings.Settings.SearchBoxTransparency == 1)
            {
                rbTransparencyOnLosingFocus.Checked = true;
                rbTransparencyOnLosingFocus2.Checked = true;
                // set the opacity value to based on the active property..
                Opacity = activated ? 1 : FormSettings.Settings.SearchBoxOpacity;
            }
            else if (FormSettings.Settings.SearchBoxTransparency == 2)
            {
                rbTransparencyAlways.Checked = true;
                rbTransparencyAlways2.Checked = true;
                Opacity = FormSettings.Settings.SearchBoxOpacity;               
            }
            SuspendTransparencyChangeEvent = false;
        }

        /// <summary>
        /// Creates a result list for the <see cref="FormSearchResultTree"/> to display the results.
        /// </summary>
        /// <param name="searchResults">A collection of search result created by the <see cref="TextSearcherAndReplacer"/> class instance.</param>
        /// <param name="scintilla">A <see cref="Scintilla"/> class instance to be used to fill the return value with.</param>
        /// <param name="fileName">The file name to be used with the result value with.</param>
        /// <param name="isFileOpen">A flag indicating whether the <paramref name="fileName"/> is an opened file in the application.</param>
        /// <returns>A list of results created with the given parameters for the <see cref="FormSearchResultTree"/> class instance.</returns>
        private
            List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                isFileOpen)> ToTreeResult(IEnumerable<(int posion, int length, string foundString)> searchResults,
                Scintilla scintilla, string fileName, bool isFileOpen)
        {
            List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                isFileOpen)> result =
                new List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                    isFileOpen)>();

            // loop trough the search result to create a little more complex list of them ;-) with the given parameters..
            foreach (var searchResult in searchResults)
            {
                Invoke(new MethodInvoker(delegate
                {
                    result.Add((fileName, scintilla.LineFromPosition(searchResult.posion), searchResult.posion,
                        searchResult.length, scintilla.Lines[scintilla.LineFromPosition(searchResult.posion)].Text,
                        isFileOpen));
                }));
            }

            // return the result..
            return result;
        }
        #endregion

        #region PrivateEvents
        // a user wishes to search to the backward direction..
        private void btFindPrevious_Click(object sender, EventArgs e)
        {
            Backward();
        }

        // a user wishes to search to the forward direction..
        private void btFindNext_Click(object sender, EventArgs e)
        {
            Forward();
        }

        /// <summary>
        /// This event is fired when the <see cref="TextSearcherAndReplacer"/> class instance reports internal progress.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="T:VPKSoft.SearchText.TextSearcherEventArgs" /> instance containing the event data.</param>
        private void SearchOpenDocuments_SearchProgress(object sender, TextSearcherEventArgs e)
        {
            // as this event is fired from another thread, do invoke..
            Invoke(new MethodInvoker(delegate
            {
                // set the status message..
                ssLbStatus.Text = DBLangEngine.GetMessage("msgSearchProgress",
                    "File: {0}, Progress: {1}|A message describing a search or replace progress with a file name and a progress percentage",
                    e.FileNameNoPath, e.Percentage);

                // set the progress bar value..
                pbSearchProgress.Value = e.Percentage;
            }));
        }

        // prevent the singleton instance of disposing it self if not allowed by AllowInstanceDispose property..
        private void FormSearchAndReplace_FormClosing(object sender, FormClosingEventArgs e)
        {
            // as this form acts a bit different, do save the positioning here..
            PositionForms.SavePosition(this);

            e.Cancel = !AllowInstanceDispose;
            if (!AllowInstanceDispose)
            {
                Hide();
            }
        }

        // a new search class is constructed if the search or replace conditions have changed..
        private void SearchAndReplaceCondition_Changed(object sender, EventArgs e)
        {
            if (CurrentDocument.scintilla != null)
            {
                CreateSingleSearchReplaceAlgorithm(CurrentDocument);
            }
        }

        // a user wishes to count all the matching strings of the currently active document..
        private void BtCount_Click(object sender, EventArgs e)
        {
            int count = 0; // set the occurrence count variable..

            new FormDialogSearchReplaceProgress(delegate
            {
                // search all the occurrences..
                var result = SearchOpenDocuments?.FindAll(100);

                CreateSingleSearchReplaceAlgorithm(CurrentDocument);
                SearchOpenDocuments?.ResetSearch();

                // save the count value..
                count = result.Count();
            }, SearchOpenDocuments);

            // indicate the count value to the user..
            ssLbStatus.Text = DBLangEngine.GetMessage("msgSearchFoundCount",
                "Found: {0}|A message describing a count of search or replace results", count);
        }

        // indicate the current search or replace function => active tab..
        private void TcMain_TabIndexChanged(object sender, EventArgs e)
        {
            Text = tcMain.SelectedTab.Text;
            if (tcMain.SelectedTab.Equals(tabFind))
            {
                cmbFind.Focus();
            } 
            else if (tcMain.SelectedTab.Equals(tabReplace))
            {
                cmbFind2.Focus();
            }
        }

        // the form is activated..
        private void FormSearchAndReplace_NeedReloadDocuments(object sender, EventArgs e)
        {
            // get the documents as they might have changed..
            Documents = GetDocuments(true);
            CurrentDocument = GetCurrentDocument();

            // set the transparency value..
            TransparencyToggle(true);

            // for some reason this seems to be required..
            TopMost = true;
            BringToFront();
        }

        // the form is deactivated so set the transparency value..
        private void FormSearchAndReplace_Deactivate(object sender, EventArgs e)
        {
            TransparencyToggle(false);
        }

        // a user wishes to close the form..
        private void BtClose_Click(object sender, EventArgs e)
        {
            // ..so lets obey..
            Close();
        }

        // A users wishes to find all occurrences within the active document..
        private void BtFindAllCurrent_Click(object sender, EventArgs e)
        {
            // make a list suitable for the FormSearchResultTree class instance..            
            List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                isFileOpen)> results =
                new List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                    isFileOpen)>();

            // a necessary null check..
            if (CurrentDocument.scintilla != null)
            {
                new FormDialogSearchReplaceProgress(delegate
                {
                    // create a new search algorithm for the current document..
                    CreateSingleSearchReplaceAlgorithm(CurrentDocument);

                    // search for all the matches in the current document..
                    var result = SearchOpenDocuments?.FindAll(100);

                    // reset the search algorithm..
                    SearchOpenDocuments?.ResetSearch();

                    // get the results in a suitable format for the FormSearchResultTree class instance..
                    results.AddRange(ToTreeResult(result, CurrentDocument.scintilla, CurrentDocument.fileName, true));

                }, SearchOpenDocuments);
            }

            // create the FormSearchResultTree class instance..
            var tree = new FormSearchResultTree();

            // display the tree..
            tree.Show();

            // set the search results for the FormSearchResultTree class instance..
            tree.SearchResults = results;
        }

        // A users wishes to find all occurrences within all the opened documents..
        private void BtFindAllInAll_Click(object sender, EventArgs e)
        {
            // make a list suitable for the FormSearchResultTree class instance..            
            List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                isFileOpen)> results =
                new List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                    isFileOpen)>();

            // loop through all the open documents..
            foreach (var document in Documents)
            {
                // an instance reference is required in case the user wishes to cancel the search process
                // before completion..
                var form = new FormDialogSearchReplaceProgress(delegate
                {
                    // create a new search algorithm for the document..
                    CreateSingleSearchReplaceAlgorithm(document);

                    // search for all the matches in the document..
                    var result = SearchOpenDocuments?.FindAll(100);

                    // reset the search algorithm..
                    SearchOpenDocuments?.ResetSearch();

                    // get the results in a suitable format for the FormSearchResultTree class instance..
                    results.AddRange(ToTreeResult(result, document.scintilla, document.fileName, true));

                }, SearchOpenDocuments);

                // if the user cancelled then break the loop..
                if (form.Cancelled)
                {
                    break;
                }
            }

            // create the FormSearchResultTree class instance..
            var tree = new FormSearchResultTree();

            // display the tree..
            tree.Show();

            // set the search results for the FormSearchResultTree class instance..
            tree.SearchResults = results;
        }

        // A users wishes to replace all occurrences within all the opened documents..
        private void BtReplaceAllInAll_Click(object sender, EventArgs e)
        {
            // make a suitable for the results..
            List<(string newContents, int count, string fileName, string fileNameNoPath)> results =
                new List<(string newContents, int count, string fileName, string fileNameNoPath)>();

            int fileCount = 0;

            string replaceString = cmbReplace.Text;

            // loop through all the open documents..
            foreach (var document in Documents)
            {
                // an instance reference is required in case the user wishes to cancel the search process
                // before completion..
                var form = new FormDialogSearchReplaceProgress(delegate
                {
                    // create a new search algorithm for the document..
                    CreateSingleSearchReplaceAlgorithm(document);

                    // search for all the matches in the document..
                    var result = SearchOpenDocuments?.ReplaceAll(replaceString, 100);

                    if (result != null)
                    {
                        // invoke as running in another thread..
                        document.scintilla.Invoke(new MethodInvoker(delegate
                        {
                            document.scintilla.Text = result.Value.newContents;
                        }));

                        results.Add((result.Value.newContents, result.Value.count, SearchOpenDocuments.FileName, SearchOpenDocuments.FileNameNoPath));

                        if (result.Value.count > 0)
                        {
                            fileCount++;
                        }
                    }

                    // reset the search algorithm..
                    SearchOpenDocuments?.ResetSearch();

                }, SearchOpenDocuments);

                // if the user cancelled then break the loop..
                if (form.Cancelled)
                {
                    break;
                }
            }

            // set the count value of replaced occurrences and file count on the status strip..
            ssLbStatus.Text = DBLangEngine.GetMessage("msgSearchReplaceCountWithFiles",
                "Replaced {0} occurrences from {1} files|A message describing a count of occurrences replaced and in how many files",
                results.Sum(f => f.count), fileCount);

        }

        // a user wishes to replace the current search result in the current document..
        private void BtReplace_Click(object sender, EventArgs e)
        {                        
            // ..so do replace the selection gotten via call to Forward() or Backward() method..
            CurrentDocument.scintilla?.ReplaceSelection(cmbReplace.Text);

            // a necessary null check..
            if (CurrentDocument.scintilla != null)
            {
                // re-create the search algorithm as its internal contents have not changed..
                CreateSingleSearchReplaceAlgorithm(CurrentDocument);
            }
        }

        // a user wishes to replace all occurrences in the current document..
        private void BtReplaceAll_Click(object sender, EventArgs e)
        {
            // get the text to replace the occurrences with..
            string replaceStr = cmbReplace.Text; 

            // create a variable for the result returned from the
            // ReplaceAll() call..
            (string newContents, int count)? result = (string.Empty, 0);


            new FormDialogSearchReplaceProgress(delegate
            {
                // create a new search algorithm for the current document..
                CreateSingleSearchReplaceAlgorithm(CurrentDocument);

                // get the replace results..
                result = SearchOpenDocuments?.ReplaceAll(replaceStr, 100);
                
                // reset the search algorithm..
                SearchOpenDocuments?.ResetSearch();
            }, SearchOpenDocuments);

            // validate that there is a result..
            if (result.HasValue)
            {
                // set the new contents for the current document..
                CurrentDocument.scintilla.Text = result?.newContents;

                // set the count value of replaced occurrences on the status strip..
                ssLbStatus.Text = DBLangEngine.GetMessage("msgSearchReplaceCount",
                    "Replaced: {0}|A message describing a count of occurrences replaced", result?.count);

            }
        }

        private void TransparencySettings_Changed(object sender, EventArgs e)
        {
            // if suspended, return..
            if (SuspendTransparencyChangeEvent)
            {
                return;
            }

            // suspend "self"..
            SuspendTransparencyChangeEvent = true;

            RadioButton _rbTransparencyAlways = rbTransparencyAlways;
            TrackBar _tbOpacity = tbOpacity;
            CheckBox _cbTransparency = cbTransparency;
            if (sender.Equals(cbTransparency2) || 
                sender.Equals(rbTransparencyAlways2) || 
                sender.Equals(rbTransparencyOnLosingFocus2) || 
                sender.Equals(tbOpacity2))
            {
                _rbTransparencyAlways = rbTransparencyAlways2;
                _tbOpacity = tbOpacity2;
                _cbTransparency = cbTransparency2;
            }
                
            if (_cbTransparency.Checked)
            {
                if (_rbTransparencyAlways.Checked)
                {
                    FormSettings.Settings.SearchBoxTransparency = 2;
                }
                else
                {
                    FormSettings.Settings.SearchBoxTransparency = 1;                        
                }
            }
            else
            {
                FormSettings.Settings.SearchBoxTransparency = 0;
            }

            FormSettings.Settings.SearchBoxOpacity = _tbOpacity.Value / 100.0;

            // toggle the transparency control states..
            TransparencyToggle(Form.ActiveForm != null && Form.ActiveForm.Equals(this));

            // resume "self"..
            SuspendTransparencyChangeEvent = false;            
        }
        #endregion
    }
}
