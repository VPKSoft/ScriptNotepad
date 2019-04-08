using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VPKSoft.LangLib;
using VPKSoft.PosLib;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    /// <summary>
    /// A search and replace dialog for the ScriptNotepad software.
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormSearchAndReplace : DBLangEngineWinforms
    {
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
        }

        /// <summary>
        /// Gets or sets a value indicating whether to reset the search area upon instance creation or not.
        /// </summary>
        public static bool ResetSearchArea { get; set; } = true;

        /// <summary>
        /// The form search and replace instance holder.
        /// </summary>
        private static FormSearchAndReplace _formSearchAndReplace = null;

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
        /// Creates an instance of the dialog for localization.
        /// </summary>
        public static void CreateLocalizationInstance()
        {
            new FormSearchAndReplace();
        }

        /// <summary>
        /// A delegate for the <see cref="FormSearchAndReplace.RequestDocuments"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="ScintillaDocumentEventArgs"/> instance containing the event data.</param>
        public delegate void OnRequestDocuments(object sender, ScintillaDocumentEventArgs e);

        private bool PreviousVisible { get; set; } = false;

        /// <summary>
        /// Toggles the visible state of this form.
        /// </summary>
        /// <param name="shouldShow">if set to <c>true</c> the form should be shown.</param>
        public void ToggleVisible(bool shouldShow)
        {
            if (PreviousVisible && shouldShow)
            {
                PreviousVisible = Visible;
                Show();
                if (tcMain.SelectedTab.Equals(tabFind))
                {
                    cmbFind.Focus();
                }
            }
            else if (Visible && !shouldShow)
            {
                PreviousVisible = Visible;
                Close();
            }
        }

        /// <summary>
        /// Occurs when the search and replace dialog requests access to the open documents on the main form.
        /// </summary>
        public event OnRequestDocuments RequestDocuments;

        /// <summary>
        /// Gets or sets the documents containing in the main form.
        /// </summary>
        private List<Scintilla> Documents { get; set; } = new List<Scintilla>();

        private Scintilla currentDocument = null;

        /// <summary>
        /// Gets or sets the currently active document in the main form.
        /// </summary>
        private Scintilla CurrentDocument
        {
            get => currentDocument;

            set
            {
                if (currentDocument != value && value != null)
                {
                    CreateSingleSearchAlgorithm(value);
                }

                currentDocument = value;
            }
        }

        /// <summary>
        /// Creates the single search algorithm for a given <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="scintilla">The scintilla.</param>
        private void CreateSingleSearchAlgorithm(Scintilla scintilla)
        {
            if (scintilla != null)
            {
                SearchOpenDocuments =
                    new SearchOpenDocuments(
                        scintilla, rbRegEx.Checked, cbMatchCase.Checked, cbMatchWholeWord.Checked,
                        !cbMatchWholeWord.Checked, ResetSearchArea, cbWrapAround.Checked, rbExtented.Checked,
                        cmbFind.Text);
            }
        }

        private SearchOpenDocuments SearchOpenDocuments { get; set; } = null;

        /// <summary>
        /// Gets the documents open in the main form.
        /// </summary>
        /// <param name="allDocuments">If set to <c>true</c> all the open documents are returned.</param>
        /// <returns>The document(s) currently opened on the main form.</returns>
        private List<Scintilla> GetDocuments(bool allDocuments)
        {
            ScintillaDocumentEventArgs scintillaDocumentEventArgs =
                new ScintillaDocumentEventArgs
                {
                    RequestAllDocuments = allDocuments
                };
            RequestDocuments?.Invoke(this, scintillaDocumentEventArgs);

            return scintillaDocumentEventArgs.Documents;
        }

        /// <summary>
        /// Gets the current document active in the main form.
        /// </summary>
        /// <returns>A Scintilla document currently active in the main form.</returns>
        private Scintilla GetCurrentDocument()
        {
            return GetDocuments(false).Count > 0 ?
                GetDocuments(false)[0] : null;
        }
        
        private void btFindPrevious_Click(object sender, EventArgs e)
        {
            SearchOpenDocuments?.Search(true);
        }

        private void btFindNext_Click(object sender, EventArgs e)
        {
            SearchOpenDocuments?.Search(false);
        }

        private void FormSearchAndReplace_NeedReloadDocuments(object sender, EventArgs e)
        {
            Documents = GetDocuments(true);
            CurrentDocument = GetCurrentDocument();
            if (cbTransparency.Checked && rbTransparencyOnLosingFocus.Checked)
            {
                Opacity = 1;
            }
            else
            {
                Opacity = tbOpacity.Value / 100.0;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow the singleton instance to be disposed of.
        /// </summary>
        public static bool AllowInstanceDispose { get; set; } = false;

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

        // a new search class is constructed if the search conditions have changed..
        private void SearchCondition_Changed(object sender, EventArgs e)
        {
            if (CurrentDocument != null)
            {
                CreateSingleSearchAlgorithm(CurrentDocument);
            }
        }

        private void FormSearchAndReplace_Deactivate(object sender, EventArgs e)
        {
            if (cbTransparency.Checked)
            {
                Opacity = tbOpacity.Value / 100.0;
            }
        }

        private void BtCount_Click(object sender, EventArgs e)
        {
            CreateSingleSearchAlgorithm(CurrentDocument);
            SearchOpenDocuments?.ResetSearchArea();
            int count = 0;
            CurrentDocument.SuspendLayout();

            if (rbExtented.Checked)
            {
                count = SearchOpenDocuments.CountExtended();
            }
            else
            {
                while (SearchOpenDocuments != null && SearchOpenDocuments.Search(false, true).success)
                {
                    count++;
                }
            }

            CurrentDocument.ResumeLayout();

            MessageBox.Show(count.ToString());
        }

        // indicate the current function => active tab..
        private void TcMain_TabIndexChanged(object sender, EventArgs e)
        {
            Text = tcMain.SelectedTab.Text;
            if (tcMain.SelectedTab.Equals(tabFind))
            {
                cmbFind.Focus();
            }
        }
    }
}
