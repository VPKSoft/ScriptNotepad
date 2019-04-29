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
using Ookii.Dialogs.WinForms;
using ScriptNotepad.Database.TableMethods;
using ScriptNotepad.Database.Tables;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.IO;
using ScriptNotepad.UtilityClasses.Keyboard;
using VPKSoft.LangLib;
using VPKSoft.PosLib;
using VPKSoft.SearchText;
using static ScriptNotepad.UtilityClasses.ApplicationHelpers.ApplicationActivateDeactivate;

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

            // get the open documents on the main form..
            Documents = GetDocuments(true);

            // get the current document..
            CurrentDocument = GetCurrentDocument();

            // localize the tool tips..
            ttMain.SetToolTip(rbSimpleExtended, DBLangEngine.GetMessage("msgSimpleExtendedDesc", "? = one character, * = multiple characters, # = digit, % = single digit|A message describing the usage of the simple extended search algorithm (meant for demented persons, such as me)."));
            ttMain.SetToolTip(rbSimpleExtended2, DBLangEngine.GetMessage("msgSimpleExtendedDesc", "? = one character, * = multiple characters, # = digit, % = single digit|A message describing the usage of the simple extended search algorithm (meant for demented persons, such as me)."));
            ttMain.SetToolTip(rbSimpleExtended3, DBLangEngine.GetMessage("msgSimpleExtendedDesc", "? = one character, * = multiple characters, # = digit, % = single digit|A message describing the usage of the simple extended search algorithm (meant for demented persons, such as me)."));

            // subscribe to an event which is raised upon application activation..
            ApplicationDeactivated += FormSearchAndReplace_ApplicationDeactivated;

            // get the search text history from the database..
            SearchHistory = DatabaseSearchAndReplace.GetSearchesAndReplaces(
                FormSettings.Settings.CurrentSession,
                "SEARCH_HISTORY",
                FormSettings.Settings.FileSearchHistoriesLimit);

            // get the replace text history from the database..
            ReplaceHistory = DatabaseSearchAndReplace.GetSearchesAndReplaces(
                FormSettings.Settings.CurrentSession,
                "REPLACE_HISTORY",
                FormSettings.Settings.FileSearchHistoriesLimit);

            // get the filter used in the search and/or replace in files..
            FilterHistory = DatabaseMiscText.GetMiscTexts(MiscTextType.FileExtensionList, FormSettings.Settings.FileSearchHistoriesLimit);

            // get the path(s) used in the search and/or replace in files..
            PathHistory = DatabaseMiscText.GetMiscTexts(MiscTextType.Path, FormSettings.Settings.FileSearchHistoriesLimit);
        }
        #endregion

        #region WndProc
         /// <summary>Processes Windows messages.</summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        protected override void WndProc(ref Message m)
        {
            WndProcApplicationActivateHelper(this, ref m);

            base.WndProc(ref m);
        }
        #endregion

        #region PublicProperties
        // the search history texts..
        private List<SEARCH_AND_REPLACE_HISTORY> searchHistory = new List<SEARCH_AND_REPLACE_HISTORY>();

        /// <summary>
        /// Gets or sets the search history texts.
        /// </summary>
        public List<SEARCH_AND_REPLACE_HISTORY> SearchHistory
        {
            get => searchHistory;

            set
            {
                // sort the list as sorted in the database..
                value = value.OrderByDescending(f => f.ADDED).ThenBy(f => f.SEARCH_OR_REPLACE_TEXT.ToLowerInvariant())
                    .ToList();

                // save the value..
                searchHistory = value;

                // fill the combo boxes..
                ReListSearchHistory();
            }
        }

        // the search filter history values..
        private List<MISCTEXT_LIST> filterHistory = new List<MISCTEXT_LIST>();

        /// <summary>
        /// Gets or sets the search filter history values.
        /// </summary>
        public List<MISCTEXT_LIST> FilterHistory
        {
            get => filterHistory;

            set
            {
                // sort the list as sorted in the database..
                value = value.OrderByDescending(f => f.ADDED).ThenBy(f => f.TEXTVALUE.ToLowerInvariant())
                    .ToList();

                // save the value..
                filterHistory = value;

                // fill the combo boxes..
                ReListFilterHistory();
            }
        }

        // the search and/or replace path values..
        private List<MISCTEXT_LIST> pathHistory = new List<MISCTEXT_LIST>();

        /// <summary>
        /// Gets or sets the search and/or replace path values.
        /// </summary>
        public List<MISCTEXT_LIST> PathHistory
        {
            get => pathHistory;

            set
            {
                // sort the list as sorted in the database..
                value = value.OrderByDescending(f => f.ADDED).ThenBy(f => f.TEXTVALUE.ToLowerInvariant())
                    .ToList();

                // save the value..
                pathHistory = value;

                // fill the combo boxes..
                ReListPathHistory();
            }
        }

        // the replace history texts..
        private List<SEARCH_AND_REPLACE_HISTORY> replaceHistory = new List<SEARCH_AND_REPLACE_HISTORY>();

        /// <summary>
        /// Gets or sets the replace history texts.
        /// </summary>
        public List<SEARCH_AND_REPLACE_HISTORY> ReplaceHistory
        {
            get => replaceHistory;

            set
            {
                // sort the list as sorted in the database..
                value = value.OrderByDescending(f => f.ADDED).ThenBy(f => f.SEARCH_OR_REPLACE_TEXT.ToLowerInvariant())
                    .ToList();

                // save the value..
                replaceHistory = value;

                // fill the combo boxes..
                ReListReplaceHistory();
            }
        }


        // value for the SelectionChangedFromMainForm property..
        private bool selectionChangedFromMainForm = true;

        /// <summary>
        /// Gets or set a value indicating whether the active document's selection was changed from the application main form.
        /// </summary>
        public bool SelectionChangedFromMainForm
        {
            get => selectionChangedFromMainForm;
            set
            {
                cbInSelection.Enabled = value;
                selectionChangedFromMainForm = value;

                if (!SelectionChangedFromMainForm)
                {
                    cbInSelection.Enabled = false;
                    cbInSelection.Checked = false;
                }
            }
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
        /// Shows the form with the find (and replace) in files tab page opened.
        /// </summary>
        public static void ShowFindInFiles()
        {
            Instance.Show();
            Instance.tcMain.SelectTab(2);
            Instance.cmbFind3.Focus();
        }

        /// <summary>
        /// Creates an instance of the dialog for localization.
        /// </summary>
        // ReSharper disable once ObjectCreationAsStatement
        public static void CreateLocalizationInstance() => new FormSearchAndReplace();
        #endregion

        #region PublicMethods        
        /// <summary>
        /// Re-lists the search history combo boxes contents.
        /// </summary>
        public void ReListSearchHistory()
        {
            // clear the combo boxes..
            cmbFind.Items.Clear();
            cmbFind2.Items.Clear();
            cmbFind3.Items.Clear();

            // fill the combo boxes..
            // ReSharper disable once CoVariantArrayConversion
            cmbFind.Items.AddRange(searchHistory.ToArray());
            // ReSharper disable once CoVariantArrayConversion
            cmbFind2.Items.AddRange(searchHistory.ToArray());
            // ReSharper disable once CoVariantArrayConversion
            cmbFind3.Items.AddRange(searchHistory.ToArray());
        }

        /// <summary>
        /// Re-lists the search and/or replace history filter combo boxes contents.
        /// </summary>
        public void ReListFilterHistory()
        {
            // clear the combo boxes..
            cmbFilters3.Items.Clear();

            // fill the combo boxes..
            // ReSharper disable once CoVariantArrayConversion
            cmbFilters3.Items.AddRange(filterHistory.ToArray());
        }

        /// <summary>
        /// Re-lists the search and/or replace directory combo boxes contents.
        /// </summary>
        public void ReListPathHistory()
        {
            // clear the combo boxes..
            cmbDirectory3.Items.Clear();

            // fill the combo boxes..
            // ReSharper disable once CoVariantArrayConversion
            cmbDirectory3.Items.AddRange(pathHistory.ToArray());
        }

        /// <summary>
        /// Re-lists the replace history combo boxes contents.
        /// </summary>
        public void ReListReplaceHistory()
        {
            // clear the combo boxes..
            cmbReplace.Items.Clear();
            cmbReplace3.Items.Clear();

            // fill the combo boxes..
            // ReSharper disable once CoVariantArrayConversion
            cmbReplace.Items.AddRange(replaceHistory.ToArray());
            // ReSharper disable once CoVariantArrayConversion
            cmbReplace3.Items.AddRange(replaceHistory.ToArray());
        }

        /// <summary>
        /// Toggles the visible state of this form.
        /// </summary>
        /// <param name="shouldShow">if set to <c>true</c> the form should be shown.</param>
        public void ToggleVisible(bool shouldShow)
        {
            if (!Visible && shouldShow && !UserClosed)
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
                else if (tcMain.SelectedTab.Equals(tabFindInFiles))
                {
                    cmbFind3.Focus();
                }
            }
            else if (Visible && !shouldShow)
            {
                Close();
                UserClosed = false;
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
                SendToBack();
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
        /// Gets or sets the value whether this form was previously closed by a user.
        /// </summary>
        private bool UserClosed { get; set; }

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

        /// <summary>
        /// Gets or sets the <see cref="TextSearcherAndReplacer"/> instance used to search and replace with either open or closed documents.
        /// </summary>
        /// <value>The search open documents.</value>
        private TextSearcherAndReplacer SearchReplaceDocuments { get; set; }

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
                    rbNormal2.Checked && tcMain.SelectedTab.Equals(tabReplace) ||
                    rbNormal3.Checked && tcMain.SelectedTab.Equals(tabFindInFiles))
                {
                    return TextSearcherEnums.SearchType.Normal;
                }
                
                // based on the currently active tab, return the correct value..
                if (rbExtented.Checked && tcMain.SelectedTab.Equals(tabFind) ||
                    rbExtented2.Checked && tcMain.SelectedTab.Equals(tabReplace) ||
                    rbExtented3.Checked && tcMain.SelectedTab.Equals(tabFindInFiles))
                {
                    return TextSearcherEnums.SearchType.Extended;
                }

                // based on the currently active tab, return the correct value..
                if (rbRegEx.Checked && tcMain.SelectedTab.Equals(tabFind) ||
                    rbRegEx2.Checked && tcMain.SelectedTab.Equals(tabReplace) ||
                    rbRegEx3.Checked && tcMain.SelectedTab.Equals(tabFindInFiles))
                {
                    return TextSearcherEnums.SearchType.RegularExpression;
                }

                // based on the currently active tab, return the correct value..
                if (rbSimpleExtended.Checked && tcMain.SelectedTab.Equals(tabFind) ||
                    rbSimpleExtended2.Checked && tcMain.SelectedTab.Equals(tabReplace) ||
                    rbSimpleExtended3.Checked && tcMain.SelectedTab.Equals(tabFindInFiles))
                {
                    return TextSearcherEnums.SearchType.SimpleExtended;
                }

                // return the default value (NOTE: the code should never reach this point!)..
                return TextSearcherEnums.SearchType.Normal;
            }
        }

        /// <summary>
        /// Gets the search text.
        /// </summary>
        private string SearchText
        {
            get
            {
                if (tcMain.SelectedTab.Equals(tabFind))
                {
                    return cmbFind.Text;
                } 
                
                if (tcMain.SelectedTab.Equals(tabReplace))
                {
                    return cmbFind2.Text;
                }

                if (tcMain.SelectedTab.Equals(tabFindInFiles))
                {
                    return cmbFind3.Text;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the replace text.
        /// </summary>
        private string ReplaceText
        {
            get
            {
                if (tcMain.SelectedTab.Equals(tabFind))
                {
                    return string.Empty;
                } 
                
                if (tcMain.SelectedTab.Equals(tabReplace))
                {
                    return cmbReplace.Text;
                }

                if (tcMain.SelectedTab.Equals(tabFindInFiles))
                {
                    return cmbReplace3.Text;
                }

                return string.Empty;
            }
        }


        /// <summary>
        /// A value indicating whether the <see cref="TransparencySettings_Changed"/> should suspend it self from executing any code.
        /// </summary>
        private bool SuspendTransparencyChangeEvent { get; set; }
        #endregion

        #region PrivateMethods        
        /// <summary>
        /// Gets ta value indicating whether the search and/or replace algorithm is correctly set.
        /// </summary>
        /// <param name="replacing">A value indicating whether the current method is a search method or a replace method.-</param>
        /// <returns>True if the search and/or replace algorithm is correctly set; otherwise false.</returns>
        private bool ValidSearchAndReplace(bool replacing)
        {
            if (CurrentDocument.scintilla == null)
            {
                return false;
            }

            if (tcMain.SelectedTab.Equals(tabFind))
            {
                return cmbFind.Text != string.Empty;
            }

            if (tcMain.SelectedTab.Equals(tabReplace))
            {
                if (replacing && (cmbReplace.Text == string.Empty | cmbFind2.Text == string.Empty))
                {
                    return false;
                }

                return cmbFind2.Text != string.Empty;
            }

            if (tcMain.SelectedTab.Equals(tabFindInFiles))
            {
                return ((cmbFind3.Text != string.Empty && !replacing) ||
                       (cmbReplace3.Text != string.Empty && replacing)) &&
                       cmbFilters3.Text.Trim() != string.Empty && cmbDirectory3.Text != string.Empty;
            }

            return false;
        }


        /// <summary>
        /// Saves the search text(s) to the database.
        /// </summary>
        private void SaveSearchText()
        {
            if (SearchText == string.Empty) // no empty strings..
            {
                return;
            }

            var inserted = DatabaseSearchAndReplace.AddOrUpdateSearchAndReplace(
                new SEARCH_AND_REPLACE_HISTORY
                {
                    SEARCH_OR_REPLACE_TEXT = SearchText, CASE_SENSITIVE = cbMatchCase.Checked, TYPE = (int) SearchType
                },
                FormSettings.Settings.CurrentSession, "SEARCH_HISTORY");

            // conditional insert to the list..
            if (inserted != null && !SearchHistory.Exists(f =>
                    f.TYPE == inserted.TYPE && f.SEARCH_OR_REPLACE_TEXT == inserted.SEARCH_OR_REPLACE_TEXT))
            {
                SearchHistory.Add(inserted);
                ReListSearchHistory();
            }
        }

        /// <summary>
        /// Saves the search text(s) to the database.
        /// </summary>
        private void SaveFilters()
        {
            if (cmbFilters3.Text.Trim() == string.Empty) // no empty strings..
            {
                return;
            }

            var inserted = DatabaseMiscText.AddOrUpdateMiscText(
                new MISCTEXT_LIST
                {
                    TEXTVALUE = cmbFilters3.Text.Trim(), TYPE = MiscTextType.FileExtensionList
                });

            // conditional insert to the list..
            if (inserted != null && !FilterHistory.Exists(f =>
                    f.TYPE == inserted.TYPE && f.TEXTVALUE == inserted.TEXTVALUE))
            {
                FilterHistory.Add(inserted);
                ReListFilterHistory();
            }
        }

        /// <summary>
        /// Saves the search and/or replace paths to the database.
        /// </summary>
        private void SavePaths()
        {
            if (cmbDirectory3.Text.Trim() == string.Empty) // no empty strings..
            {
                return;
            }

            var inserted = DatabaseMiscText.AddOrUpdateMiscText(
                new MISCTEXT_LIST
                {
                    TEXTVALUE = cmbDirectory3.Text, TYPE = MiscTextType.Path
                });

            // conditional insert to the list..
            if (inserted != null && !FilterHistory.Exists(f =>
                    f.TYPE == inserted.TYPE && f.TEXTVALUE == inserted.TEXTVALUE))
            {
                PathHistory.Add(inserted);
                ReListPathHistory();
            }
        }

        /// <summary>
        /// Saves the replace text(s) to the database.
        /// </summary>
        private void SaveReplaceText()
        {
            if (ReplaceText == string.Empty) // no empty strings..
            {
                return;
            }

            var inserted = DatabaseSearchAndReplace.AddOrUpdateSearchAndReplace(
                new SEARCH_AND_REPLACE_HISTORY
                {
                    SEARCH_OR_REPLACE_TEXT = ReplaceText, CASE_SENSITIVE = cbMatchCase.Checked, TYPE = (int) SearchType
                },
                FormSettings.Settings.CurrentSession, "REPLACE_HISTORY");

            // conditional insert to the list..
            if (inserted != null && !ReplaceHistory.Exists(f =>
                    f.TYPE == inserted.TYPE && f.SEARCH_OR_REPLACE_TEXT == inserted.SEARCH_OR_REPLACE_TEXT))
            {
                ReplaceHistory.Add(inserted);
                ReListReplaceHistory();
            }
        }


        /// <summary>
        /// Creates the single search and/or replace algorithm for a given contents and file name.
        /// </summary>
        /// <param name="contents">A contents of a document of file and a file name of the document to create the search algorithm from.</param>
        private void CreateSingleSearchReplaceAlgorithm((string contents, string fileName) contents)
        {
            // only create the algorithm if the the passed scintilla actually contains a ScintillaNET control..
            if (contents.contents != null)
            {
                if (SearchReplaceDocuments != null)
                {
                    using (SearchReplaceDocuments) // dispose of the previous algorithm..
                    {
                        // ..and unsubscribe the search/replace progress event..
                        SearchReplaceDocuments.SearchProgress -= SearchOpenDocuments_SearchProgress;
                    }
                }

                // invoke as this method might be called from a thread..
                Invoke(new MethodInvoker(delegate
                {
                    // get the default controls for the search algorithm..
                    string  cmbFindString = cmbFind.Text;
                    bool cbWrapAroundChecked = cbWrapAround.Checked;
                    bool cbMatchCaseChecked = cbMatchCase.Checked;
                    bool cbMatchWholeWordChecked = cbMatchWholeWord.Checked;

                    // get the controls matching the active tab..
                    if (tcMain.SelectedTab.Equals(tabReplace))
                    {
                        cmbFindString = cmbFind2.Text;
                        cbWrapAroundChecked = cbWrapAround2.Checked;
                        cbMatchCaseChecked = cbMatchCase2.Checked;
                        cbMatchWholeWordChecked = cbMatchWholeWord2.Checked;
                    }
                    else if (tcMain.SelectedTab.Equals(tabFindInFiles))
                    {
                        cmbFindString = cmbFind3.Text;
                        cbWrapAroundChecked = false; // no wrap-around with files..
                        cbMatchCaseChecked = cbMatchCase3.Checked;
                        cbMatchWholeWordChecked = cbMatchWholeWord3.Checked;
                    }

                    // create the search and replace algorithm..
                    SearchReplaceDocuments =
                        new TextSearcherAndReplacer(
                            contents.contents, cmbFindString,
                            SearchType)
                        {
                            WrapAround = cbWrapAroundChecked,
                            IgnoreCase = !cbMatchCaseChecked,
                            FileName = contents.fileName,
                            WholeWord = cbMatchWholeWordChecked,
                            FileNameNoPath = Path.GetFileName(contents.fileName),
                        };

                    // subscribe the search progress event..
                    SearchReplaceDocuments.SearchProgress += SearchOpenDocuments_SearchProgress;
                }));
            }
        }


        /// <summary>
        /// Creates the single search and/or replace algorithm for a given <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="scintilla">The scintilla and its file name to create the search algorithm from.</param>
        private void CreateSingleSearchReplaceAlgorithm((Scintilla scintilla, string fileName) scintilla)
        {
            // get a value if only the selection is required to be used as text for the algorithm..
            bool selection = cbInSelection.Checked && tcMain.SelectedTab.Equals(tabReplace);

            if (scintilla.scintilla != null)
            {
                CreateSingleSearchReplaceAlgorithm((
                    selection ? scintilla.scintilla.SelectedText : scintilla.scintilla.Text,
                    scintilla.fileName));
            }
        }

        /// <summary>
        /// Searches to backward direction.
        /// </summary>
        private void Backward()
        {
            SaveSearchText(); // save the used search text to the database..
            var result = SearchReplaceDocuments?.Backward();
            if (result.HasValue)
            {
                GetCurrentDocument().scintilla.SelectionStart = result.Value.position;
                GetCurrentDocument().scintilla.SelectionEnd = result.Value.position + result.Value.length;
                GetCurrentDocument().scintilla.ScrollCaret();

                // set the flag to indicate a the selection was changed from this form..
                SelectionChangedFromMainForm = false;
            }
        }

        /// <summary>
        /// Searches to forward direction.
        /// </summary>
        private void Forward()
        {
            SaveSearchText(); // save the used search text to the database..
            var result = SearchReplaceDocuments?.Forward();
            if (result.HasValue)
            {
                GetCurrentDocument().scintilla.SelectionStart = result.Value.position;
                GetCurrentDocument().scintilla.SelectionEnd = result.Value.position + result.Value.length;
                GetCurrentDocument().scintilla.ScrollCaret();

                // set the flag to indicate a the selection was changed from this form..
                SelectionChangedFromMainForm = false;
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
            gpTransparency3.Enabled = true;

            // transparency is disabled..
            if (FormSettings.Settings.SearchBoxTransparency == 0) 
            {
                // set the enabled value of the transparency setting group boxes to disabled..
                cbTransparency.Checked = false;
                cbTransparency2.Checked = false;
                cbTransparency3.Checked = false;

                gpTransparency.Enabled = false;
                gpTransparency2.Enabled = false;
                gpTransparency3.Enabled = false;

                // set the opacity value to 100%..
                Opacity = 1;
            }
            // transparency is enabled while active..
            else if (FormSettings.Settings.SearchBoxTransparency == 1)
            {
                rbTransparencyOnLosingFocus.Checked = true;
                rbTransparencyOnLosingFocus2.Checked = true;
                rbTransparencyOnLosingFocus3.Checked = true;
                // set the opacity value to based on the active property..
                Opacity = activated ? 1 : FormSettings.Settings.SearchBoxOpacity;
            }
            else if (FormSettings.Settings.SearchBoxTransparency == 2)
            {
                rbTransparencyAlways.Checked = true;
                rbTransparencyAlways2.Checked = true;
                rbTransparencyAlways3.Checked = true;
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
        // occurs when the application is activated..
        private void FormSearchAndReplace_ApplicationDeactivated(object sender, EventArgs e)
        {
            TopMost = false;
            SendToBack();
        }

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

            // save the flag indicating whether the form was closed by the user..
            UserClosed = e.CloseReason == CloseReason.UserClosing;

            e.Cancel = !AllowInstanceDispose;
            if (!AllowInstanceDispose)
            {
                Hide();
            }
        }

        // the form was allowed to close, so do unsubscribe any events subscribed manually..
        private void FormSearchAndReplace_FormClosed(object sender, FormClosedEventArgs e)
        {
            // unsubscribe from an event which is raised upon application activation..
            ApplicationDeactivated += FormSearchAndReplace_ApplicationDeactivated;
        }

        // a new search class is constructed if the search or replace conditions have changed..
        private void SearchAndReplaceCondition_Changed(object sender, EventArgs e)
        {
            if (CurrentDocument.scintilla != null)
            {
                CreateSingleSearchReplaceAlgorithm(CurrentDocument);
            }

            // set the navigation control states to enabled/disabled depending on the search condition..
            AppendValidation();
        }

        /// <summary>
        /// Set the navigation control states to enabled/disabled depending on the search condition.
        /// </summary>
        private void AppendValidation()
        {
            btFindNext.Enabled = ValidSearchAndReplace(false);
            btFindPrevious.Enabled = ValidSearchAndReplace(false);
            btCount.Enabled = ValidSearchAndReplace(false);
            btFindAllInAll.Enabled = ValidSearchAndReplace(false);
            btFindAllCurrent.Enabled = ValidSearchAndReplace(false);

            btFindPrevious2.Enabled = ValidSearchAndReplace(false);
            btFindNext2.Enabled = ValidSearchAndReplace(false);
            btReplace.Enabled = ValidSearchAndReplace(true);
            btReplaceAll.Enabled = ValidSearchAndReplace(true);
            btReplaceAllInAll.Enabled = ValidSearchAndReplace(true);

            btFindAllInFiles.Enabled = ValidSearchAndReplace(false);
            btReplaceAllInFiles.Enabled = ValidSearchAndReplace(true);
        }

        // a user wishes to count all the matching strings of the currently active document..
        private void BtCount_Click(object sender, EventArgs e)
        {
            SaveSearchText(); // save the used search text to the database..

            int count = 0; // set the occurrence count variable..

            // ReSharper disable once ObjectCreationAsStatement
            new FormDialogSearchReplaceProgress(delegate
            {
                // search all the occurrences..
                var result = SearchReplaceDocuments?.FindAll(100);

                Invoke(new MethodInvoker(delegate { CreateSingleSearchReplaceAlgorithm(CurrentDocument); }));

                SearchReplaceDocuments?.ResetSearch();

                // save the count value..
                if (result != null)
                {
                    count = result.Count();
                }
            }, SearchReplaceDocuments);

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
            else if (tcMain.SelectedTab.Equals(tabFindInFiles))
            {
                cmbFind3.Focus();
            }

            // set the navigation control states to enabled/disabled depending on the search condition..
            AppendValidation();
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

            // set the navigation control states to enabled/disabled depending on the search condition..
            AppendValidation();
        }

        // the form is deactivated so set the transparency value..
        private void FormSearchAndReplace_Deactivate(object sender, EventArgs e)
        {
            TransparencyToggle(false);

            // set the navigation control states to enabled/disabled depending on the search condition..
            AppendValidation();
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
            SaveSearchText(); // save the used search text to the database..

            // make a list suitable for the FormSearchResultTree class instance..            
            List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                isFileOpen)> results =
                new List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                    isFileOpen)>();

            // a necessary null check..
            if (CurrentDocument.scintilla != null)
            {
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogSearchReplaceProgress(delegate
                {
                    // create a new search algorithm for the current document..
                    Invoke(new MethodInvoker(delegate { CreateSingleSearchReplaceAlgorithm(CurrentDocument); }));

                    // search for all the matches in the current document..
                    var result = SearchReplaceDocuments?.FindAll(100);

                    // reset the search algorithm..
                    SearchReplaceDocuments?.ResetSearch();

                    // get the results in a suitable format for the FormSearchResultTree class instance..
                    results.AddRange(ToTreeResult(result, CurrentDocument.scintilla, CurrentDocument.fileName, true));

                }, SearchReplaceDocuments);
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
            SaveSearchText(); // save the used search text to the database..

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
                    Invoke(new MethodInvoker(delegate { CreateSingleSearchReplaceAlgorithm(document); }));

                    // search for all the matches in the document..
                    var result = SearchReplaceDocuments?.FindAll(100);

                    // reset the search algorithm..
                    SearchReplaceDocuments?.ResetSearch();

                    // get the results in a suitable format for the FormSearchResultTree class instance..
                    results.AddRange(ToTreeResult(result, document.scintilla, document.fileName, true));

                }, SearchReplaceDocuments);

                // if the user canceled then break the loop..
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
            SaveSearchText(); // save the used search text to the database..
            SaveReplaceText(); // save the used replace text to the database..

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
                    var result = SearchReplaceDocuments?.ReplaceAll(replaceString, 100);

                    if (result != null)
                    {
                        // invoke as running in another thread..
                        document.scintilla.Invoke(new MethodInvoker(delegate
                        {
                            document.scintilla.Text = result.Value.newContents;
                        }));

                        results.Add((result.Value.newContents, result.Value.count, SearchReplaceDocuments.FileName, SearchReplaceDocuments.FileNameNoPath));

                        if (result.Value.count > 0)
                        {
                            fileCount++;
                        }
                    }

                    // reset the search algorithm..
                    SearchReplaceDocuments?.ResetSearch();

                }, SearchReplaceDocuments);

                // if the user canceled then break the loop..
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
            SaveSearchText(); // save the used search text to the database..
            SaveReplaceText(); // save the used replace text to the database..

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
            SaveSearchText(); // save the used search text to the database..
            SaveReplaceText(); // save the used replace text to the database..

            // get the text to replace the occurrences with..
            string replaceStr = cmbReplace.Text; 

            // create a variable for the result returned from the
            // ReplaceAll() call..
            (string newContents, int count)? result = (string.Empty, 0);


            // ReSharper disable once ObjectCreationAsStatement
            new FormDialogSearchReplaceProgress(delegate
            {
                // create a new search algorithm for the current document..
                Invoke(new MethodInvoker(delegate { CreateSingleSearchReplaceAlgorithm(CurrentDocument); }));

                // get the replace results..
                result = SearchReplaceDocuments?.ReplaceAll(replaceStr, 100);
                
                // reset the search algorithm..
                SearchReplaceDocuments?.ResetSearch();
            }, SearchReplaceDocuments);

            // validate that there is a result..
            if (result.HasValue)
            {
                // get a value if only the selection is required to be used as text for the algorithm..
                bool selection = cbInSelection.Checked && tcMain.SelectedTab.Equals(tabReplace);

                // set the new contents for the current document..
                if (selection)
                {
                    CurrentDocument.scintilla.ReplaceSelection(result?.newContents);
                }
                else
                {
                    CurrentDocument.scintilla.Text = result?.newContents;
                }

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

            // ReSharper disable once InconsistentNaming
            RadioButton _rbTransparencyAlways = rbTransparencyAlways;
            // ReSharper disable once InconsistentNaming
            TrackBar _tbOpacity = tbOpacity;
            // ReSharper disable once InconsistentNaming
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
            TransparencyToggle(ActiveForm != null && ActiveForm.Equals(this));

            // resume "self"..
            SuspendTransparencyChangeEvent = false;            
        }

        // searches for a text occurrences in multiple files on the file system..
        private void BtFindAllInFiles_Click(object sender, EventArgs e)
        {
            SaveSearchText(); // save the used search text to the database..
            SaveReplaceText(); // save the used replace text to the database..
            SaveFilters(); // save the used file extension filter to the database..
            SavePaths(); // save the path used in the search into the database..

            // make a list suitable for the FormSearchResultTree class instance..            
            List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                isFileOpen)> results =
                new List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                    isFileOpen)>();

            // create a new instance of the DirectoryCrawler class by user given "arguments"..
            DirectoryCrawler crawler = new DirectoryCrawler(cmbDirectory3.Text, DirectoryCrawler.SearchTypeMatch.Regex,
                cmbFilters3.Text, cbInSubFolders.Checked);

            // create a invisible scintilla control for the process..
            Scintilla contents = new Scintilla {Visible = false};

            // invocations to the control can't be made if the control has no handle..
            Controls.Add(contents);

            // ReSharper disable once ObjectCreationAsStatement

            // initialize a new FormDialogSearchReplaceProgressFiles dialog form with the
            // DirectoryCrawler class instance as parameter and a delegate for the
            // FormDialogSearchReplaceProgressFiles.RequestNextAction..
            new FormDialogSearchReplaceProgressFiles(crawler,
                // no name for this delegate..
                (o, args) =>
                {
                    // check the settings for a maximum search file size..
                    if ((new FileInfo(args.FileName)).Length > FormSettings.Settings.FileSearchMaxSizeMb * 1000000)
                    {
                        // .. and if exceeded, skip the file..
                        args.SkipFile = true;
                    }
                    else // ..otherwise do continue..
                    {
                        // invoke the Scintilla to get new contents of the new file..
                        contents.Invoke(new MethodInvoker(delegate
                        {
                            // just read all the text in the file and set it to the Scintilla..
                            contents.Text = File.ReadAllText(args.FileName, FormSettings.Settings.DefaultEncoding);

                            // by not doing this the memory consumption might get HIGH..
                            contents.EmptyUndoBuffer();

                            // create a new search algorithm for the file and it's contents..
                            CreateSingleSearchReplaceAlgorithm((contents, args.FileName));
                        }));

                        // set the new TextSearcherAndReplacer class instance to the event's argument..
                        args.SearchAndReplacer = SearchReplaceDocuments;
                        
                        // set the action to run for the file in the FormDialogSearchReplaceProgressFiles class instance..
                        args.Action = () =>
                        {
                            results.AddRange(ToTreeResult(SearchReplaceDocuments?.FindAll(100), contents,
                                args.FileName, false));
                        };
                    }
                });
            
            // create the FormSearchResultTree class instance..
            var tree = new FormSearchResultTree();

            // display the tree..
            tree.Show();

            // set the search results for the FormSearchResultTree class instance..
            tree.SearchResults = results;

            // set the Scintilla free (!)..
            using (contents)
            {
                Controls.Remove(contents);
            }
        }

        // replaces a text occurrences in multiple files on the file system..
        private void BtReplaceAllInFiles_Click(object sender, EventArgs e)
        {
            SaveSearchText(); // save the used search text to the database..
            SaveReplaceText(); // save the used replace text to the database..
            SaveFilters(); // save the used file extension filter to the database..
            SavePaths(); // save the path used in the search into the database..

            string toReplaceWith = cmbReplace3.Text;

            // create a new instance of the DirectoryCrawler class by user given "arguments"..
            DirectoryCrawler crawler = new DirectoryCrawler(cmbDirectory3.Text, DirectoryCrawler.SearchTypeMatch.Regex,
                cmbFilters3.Text, cbInSubFolders.Checked);

            // create a invisible scintilla control for the process..
            Scintilla contents = new Scintilla {Visible = false};

            // invocations to the control can't be made if the control has no handle..
            Controls.Add(contents);

            int replaceCount = 0;
            int filesAffected = 0;

            // ReSharper disable once ObjectCreationAsStatement

            // initialize a new FormDialogSearchReplaceProgressFiles dialog form with the
            // DirectoryCrawler class instance as parameter and a delegate for the
            // FormDialogSearchReplaceProgressFiles.RequestNextAction..
            new FormDialogSearchReplaceProgressFiles(crawler,
                // no name for this delegate..
                (o, args) =>
                {
                    // check the settings for a maximum search file size..
                    if ((new FileInfo(args.FileName)).Length > FormSettings.Settings.FileSearchMaxSizeMb * 1000000)
                    {
                        // .. and if exceeded, skip the file..
                        args.SkipFile = true;
                    }
                    else // ..otherwise do continue..
                    {
                        // invoke the Scintilla to get new contents of the new file..
                        contents.Invoke(new MethodInvoker(delegate
                        {
                            // just read all the text in the file and set it to the Scintilla..
                            contents.Text = File.ReadAllText(args.FileName, FormSettings.Settings.DefaultEncoding);

                            // by not doing this the memory consumption might get HIGH..
                            contents.EmptyUndoBuffer();

                            // create a new search algorithm for the file and it's contents..
                            CreateSingleSearchReplaceAlgorithm((contents, args.FileName));
                        }));

                        // set the new TextSearcherAndReplacer class instance to the event's argument..
                        args.SearchAndReplacer = SearchReplaceDocuments;
                        
                        // set the action to run for the file in the FormDialogSearchReplaceProgressFiles class instance..
                        args.Action = () =>
                        {
                            var replacements = SearchReplaceDocuments?.ReplaceAll(toReplaceWith, 100);
                            if (replacements.HasValue)
                            {
                                if (replacements.Value.count > 0)
                                {
                                    File.WriteAllText(args.FileName, replacements.Value.newContents,
                                        FormSettings.Settings.DefaultEncoding);
                                    filesAffected++;

                                    replaceCount += replacements.Value.count;
                                }                               
                            }
                        };
                    }
                });            

            // set the Scintilla free (!)..
            using (contents)
            {
                Controls.Remove(contents);
            }

            // indicate the replacement count..
            ssLbStatus.Text = DBLangEngine.GetMessage("msgReplacementsMadeFilesAmount",
                "Replaced {0} occurrences in {1} files.|A message indicating and amount of replacements made to files in the files system and how many files were affected.",
                replaceCount, filesAffected);
        }

        // set the folder via the Ookii.Dialogs.WinForms.VistaFolderBrowserDialog class..
        private void BtSelectFolder_Click(object sender, EventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();

            if (Directory.Exists(cmbDirectory3.Text))
            {
                dialog.SelectedPath = cmbDirectory3.Text;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                cmbDirectory3.Text = dialog.SelectedPath;
                SavePaths(); // save the path used in the search into the database..
            }
        }

        /// <summary>
        /// Some keyboard shortcuts for the search and replace dialog.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void FormSearchAndReplace_KeyDown(object sender, KeyEventArgs e)
        {
            // escape key was pressed, so the dialog can be closed..
            if (e.KeyCode == Keys.Escape && e.NoModifierKeysDown())
            {
                e.Handled = true;
                Close();
                return;
            }

            // use the return key to search either to forwards or to backwards direction..
            if (e.KeyCode == Keys.Return)
            {
                // if the find tab is active..
                if (tcMain.SelectedTab.Equals(tabFind))
                {
                    if (btFindNext.Enabled && e.NoModifierKeysDown())
                    {
                        // the return key was pressed with no modifiers, so search forwards..
                        Forward();
                    }
                    else if (btFindPrevious.Enabled && e.ModifierKeysDown(false, true, false))
                    {
                        // the return key was pressed with the Control key down, so search backwards..
                        Backward();
                    }

                    // flag the event as handled..
                    e.Handled = true;
                }
                // if the search and replace tab is active..
                else if (tcMain.SelectedTab.Equals(tabReplace))
                {
                    if (btFindNext2.Enabled && e.NoModifierKeysDown())
                    {
                        // the return key was pressed with no modifiers, so search forwards..
                        Forward();
                    }
                    else if (btFindPrevious2.Enabled && e.ModifierKeysDown(false, true, false))
                    {
                        // the return key was pressed with the Control key down, so search backwards..
                        Backward();
                    }

                    // flag the event as handled..
                    e.Handled = true;
                }
            }
        }
        #endregion
    }
}
