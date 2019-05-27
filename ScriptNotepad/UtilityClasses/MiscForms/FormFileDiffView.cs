using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPKSoft.LangLib;
using VPKSoft.PosLib;
using static ScintillaDiff.ScintillaDiffStyles;

namespace ScriptNotepad.UtilityClasses.MiscForms
{
    /// <summary>
    /// A form to display differences between two files.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormFileDiffView : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormFileDiffView"/> class.
        /// </summary>
        public FormFileDiffView()
        {
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

            ThisInstance = this;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the application is going to be closed.
        /// </summary>
        public static bool ApplicationClosing { get; set; }

        /// <summary>
        /// Keep this class a singleton.
        /// </summary>
        private static FormFileDiffView ThisInstance { get; set; }

        /// <summary>
        /// Displays the form with given file contents.
        /// </summary>
        /// <param name="diffOne">The first file's contents to use with the diff.</param>
        /// <param name="diffTwo">The second file's contents to use with the diff.</param>
        public static void Execute(string diffOne, string diffTwo)
        {
            if (ThisInstance == null)
            {
                ThisInstance = new FormFileDiffView();
            }

            ThisInstance.diffControl.TextLeft = diffOne;
            ThisInstance.diffControl.TextRight = diffTwo;

            ThisInstance.tsbPreviousDiff.Enabled = ThisInstance.diffControl.CanGoPrevious;
            ThisInstance.tsbNextDiff.Enabled = ThisInstance.diffControl.CanGoNext;

            if (!ThisInstance.Visible)
            {
                ThisInstance.Show();
            }
        }

        // set the mode of the diff viewer..
        private void TsbSplitView_Click(object sender, EventArgs e)
        {
            var button = (ToolStripButton) sender;
            diffControl.DiffStyle = button.Checked ? DiffStyle.DiffSideBySide : DiffStyle.DiffList;
        }

        // navigate to the previous difference..
        private void TsbPreviousDiff_Click(object sender, EventArgs e)
        {
            var button = (ToolStripButton) sender;
            diffControl.Previous();
            tsbPreviousDiff.Enabled = diffControl.CanGoPrevious;
            tsbNextDiff.Enabled = diffControl.CanGoNext;
        }

        // navigate to the next difference..
        private void TsbNextDiff_Click(object sender, EventArgs e)
        {
            var button = (ToolStripButton) sender;
            diffControl.Next();
            tsbPreviousDiff.Enabled = diffControl.CanGoPrevious;
            tsbNextDiff.Enabled = diffControl.CanGoNext;
        }

        private void FormFileDiffView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ApplicationClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void TsbSwapContents_Click(object sender, EventArgs e)
        {
            var button = (ToolStripButton) sender;
            diffControl.SwapDiff();
        }
    }
}
