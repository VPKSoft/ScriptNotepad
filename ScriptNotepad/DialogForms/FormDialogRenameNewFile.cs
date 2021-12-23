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

using System.Windows.Forms;
using VPKSoft.LangLib;
using VPKSoft.ScintillaTabbedTextControl;

namespace ScriptNotepad.DialogForms
{
    /// <summary>
    /// A dialog to rename new files (files which aren't saved to the file system).
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogRenameNewFile : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogRenameNewFile"/> class.
        /// </summary>
        public FormDialogRenameNewFile()
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
        /// Gets or sets the <see cref="ScintillaTabbedTextControl"/> control.
        /// </summary>
        private ScintillaTabbedTextControl TabbedTextControl { get; set; }

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box. </param>
        /// <param name="tabbedTextControl">An instance of the <see cref="ScintillaTabbedTextControl"/> control in which the current new document should be renamed.</param>
        /// <returns>A new file name for a new file if successful; otherwise null.</returns>
        /// <exception cref="T:System.ArgumentException">The form specified in the <paramref name="owner" /> parameter is the same as the form being shown.</exception>
        /// <exception cref="T:System.InvalidOperationException">The form being shown is already visible.-or- The form being shown is disabled.-or- The form being shown is not a top-level window.-or- The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" />).</exception>
        public static string ShowDialog(IWin32Window owner, ScintillaTabbedTextControl tabbedTextControl)
        {
            if (tabbedTextControl.CurrentDocument == null)
            {
                return null;
            }

            using (var dialog = new FormDialogRenameNewFile())
            {
                dialog.TabbedTextControl = tabbedTextControl;
                dialog.tbCurrentName.Text = tabbedTextControl.CurrentDocument.FileName;
                dialog.tbNewName.Text = tabbedTextControl.CurrentDocument.FileName;
                if (dialog.ShowDialog(owner) == DialogResult.OK)
                {
                    return dialog.tbNewName.Text;
                }
            }

            return null;
        }

        // a text is changed with the new file name text box, so do validation..
        private void TbNewName_TextChanged(object sender, EventArgs e)
        {
            bool enableOk = true;
            var newText = ((TextBox) sender).Text;

            // first validate the file name..
            if (newText.Trim() != string.Empty && // empty string is not allowed..
                newText.IndexOfAny(Path.GetInvalidFileNameChars()) == -1 && // no invalid path characters are allowed..
                Path.GetFileName(newText) == newText) // not paths allowed with the unsaved file..
            {
                // now validate that the file doesn't already "exist"..
                foreach (var document in TabbedTextControl.Documents)
                {
                    if (document.FileName.Equals(newText, StringComparison.InvariantCultureIgnoreCase))
                    {
                        enableOk = false; // a file with the name exists, so disable the button..
                        break;
                    }
                }
            }
            else
            {
                enableOk = false; // validation failed..
            }

            // set the OK button's enabled state..
            btOK.Enabled = enableOk;
        }

        // focus to the right control..
        private void FormDialogRenameNewFile_Shown(object sender, EventArgs e)
        {
            tbNewName.Focus();
            tbNewName.SelectAll();
        }
    }
}
