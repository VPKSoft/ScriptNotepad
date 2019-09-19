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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ScriptNotepad.UtilityClasses.Keyboard;
using VPKSoft.ErrorLogger;
using VPKSoft.LangLib;
using VPKSoft.ScintillaTabbedTextControl;

namespace ScriptNotepad.DialogForms
{
    /// <summary>
    /// A dialog to select a tab from the tabbed <see cref="ScintillaNET.Scintilla"/> control.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogSelectFileTab : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogSelectFileTab"/> class.
        /// </summary>
        public FormDialogSelectFileTab()
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

        // a list of image keys and ScintillaTabbedDocument class instances to display on the dialog..
        private List<(string imageKey, ScintillaTabbedDocument document)> OpenDocuments { get; } =
            new List<(string imageKey, ScintillaTabbedDocument document)>();

        /// <summary>
        /// Gets the cached images for file types.
        /// </summary>
        private static ImageList CachedImages { get; } = new ImageList();

        // the ScintillaTabbedTextControl class instance given via the ShowDialog(...) method..
        private ScintillaTabbedTextControl scintillaTabbedTextControl;

        /// <summary>
        /// Shows the form as a modal dialog box for user to select an opened file within a a <see cref="ScintillaTabbedTextControl"/> control.
        /// </summary>
        /// <param name="scintillaTabbedTextControl">An instance to a <see cref="ScintillaTabbedTextControl"/> instance to select a tab from.</param>
        /// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
        public static DialogResult ShowDialog(ScintillaTabbedTextControl scintillaTabbedTextControl)
        {
            var form = new FormDialogSelectFileTab();

            form.dgvOpenFiles.SuspendLayout();

            form.scintillaTabbedTextControl = scintillaTabbedTextControl;

            foreach (var scintillaTabbedDocument in scintillaTabbedTextControl.Documents)
            {
                var iconForFile = SystemIcons.WinLogo;

                if (!CachedImages.Images.ContainsKey(":unknown:"))
                {
                    iconForFile.ToBitmap();
                    CachedImages.Images.Add(":unknown:", iconForFile);
                }

                try
                {
                    if (File.Exists(scintillaTabbedDocument.FileName))
                    {
                        FileInfo fileInfo = new FileInfo(scintillaTabbedDocument.FileName);

                        // Check to see if the image collection contains an image
                        // for this extension, using the extension as a key.
                        if (!CachedImages.Images.ContainsKey(fileInfo.Extension))
                        {
                            // If not, add the image to the image list.
                            iconForFile = Icon.ExtractAssociatedIcon(fileInfo.FullName);
                            if (iconForFile != null)
                            {
                                CachedImages.Images.Add(fileInfo.Extension, iconForFile);
                            }
                        }

                        form.OpenDocuments.Add((fileInfo.Extension, scintillaTabbedDocument));
                        form.dgvOpenFiles.Rows.Add(CachedImages.Images[fileInfo.Extension], fileInfo.FullName);
                    }
                    else
                    {
                        form.OpenDocuments.Add((":unknown:", scintillaTabbedDocument));
                        form.dgvOpenFiles.Rows.Add(CachedImages.Images[":unknown:"], scintillaTabbedDocument.FileName);
                    }
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionLogger.LogError(ex);
                }
            }
            form.dgvOpenFiles.ResumeLayout();

            return form.ShowDialog();
        }

        /// <summary>
        /// Filters the open tab list with a given filter text.
        /// </summary>
        /// <param name="filterText">The string to be used for the filtering.</param>
        private void RefreshTabs(string filterText)
        {
            dgvOpenFiles.SuspendLayout();
            dgvOpenFiles.Rows.Clear();
            foreach (var document in OpenDocuments)
            {
                if (filterText.Trim() == string.Empty || document.document.FileName.IndexOf(filterText, StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    dgvOpenFiles.Rows.Add(CachedImages.Images[document.imageKey], document.document.FileName);
                }
            }
            dgvOpenFiles.ResumeLayout();
        }

        /// <summary>
        /// Gets a value indicating if the user selected a valid file entry from the dialog and if successful, selects the tab from the <see cref="ScintillaTabbedTextControl"/>.
        /// </summary>
        /// <returns><c>true</c> if the tab was successfully selected, <c>false</c> otherwise.</returns>
        private bool DialogOk()
        {
            var row = dgvOpenFiles.CurrentRow;
            if (row != null)
            {
                var fileName = row.Cells[colFileName.Index].Value.ToString();
                var index = scintillaTabbedTextControl.Documents.FindIndex(f => f.FileName == fileName);
                if (index != -1)
                {
                    scintillaTabbedTextControl.ActivateDocument(index);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Changes the selection of the <see cref="DataGridView"/> instance in case of the given key is <see cref="Keys.Up"/> or <see cref="Keys.Down"/> value.
        /// </summary>
        /// <param name="keys">A <see cref="Keys"/> enumeration value.</param>
        /// <param name="focusControl">A control to focus after the selection change. Set the value to null to not to change the focus.</param>
        /// <returns><c>true</c> if the selection was successfully changed, <c>false</c> otherwise.</returns>
        private bool MoveSelection(Keys keys, Control focusControl)
        {
            if (keys != Keys.Up && keys != Keys.Down)
            {
                return false;
            }

            if (dgvOpenFiles.Rows.Count > 0)
            {
                int rowIndex = dgvOpenFiles.CurrentRow?.Index ?? -1;

                if (keys == Keys.Down)
                {
                    rowIndex++;
                    if (rowIndex >= dgvOpenFiles.Rows.Count)
                    {
                        rowIndex = 0;
                    }

                    dgvOpenFiles.CurrentCell = dgvOpenFiles.Rows[rowIndex].Cells[0];

                    dgvOpenFiles.FirstDisplayedScrollingRowIndex = rowIndex;

                    focusControl?.Focus();
                    return true;
                }

                if (keys == Keys.Up)
                {
                    rowIndex--;
                    if (rowIndex < 0)
                    {
                        rowIndex = dgvOpenFiles.Rows.Count - 1;
                    }

                    dgvOpenFiles.CurrentCell = dgvOpenFiles.Rows[rowIndex].Cells[0];

                    dgvOpenFiles.FirstDisplayedScrollingRowIndex = rowIndex;

                    focusControl?.Focus();
                    return true;
                }
            }

            return false;
        }

        // the keyboard handler for everything else than the filter text box..
        private void FormDialogSelectFileTab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                DialogResult = DialogResult.Cancel;
            }

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                if (DialogOk())
                {
                    DialogResult = DialogResult.OK;
                }
                return;
            }

            if (MoveSelection(e.KeyCode, null))
            {
                e.SuppressKeyPress = true;
                return;
            }

            // delegate normal key presses to the filter open tabs text box..
            if (char.IsLetterOrDigit((char)e.KeyValue) || KeySendList.HasKey(e.KeyCode) && !tbFilterFiles.Focused)
            {
                tbFilterFiles.SelectAll();
                tbFilterFiles.Focus();
                char key = (char)e.KeyValue;

                SendKeys.Send(
                    char.IsLetterOrDigit(key) ? key.ToString().ToLower() : KeySendList.GetKeyString(e.KeyCode));

                e.SuppressKeyPress = true;
            }
        }

        // filters the open tab list when the text with the filtered text box changes..
        private void TbFilterFiles_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox) sender;
            RefreshTabs(textBox.Text);
        }

        // the keyboard handler for the filter text box..
        private void TbFilterFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                DialogResult = DialogResult.Cancel;
            }

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                if (DialogOk())
                {
                    DialogResult = DialogResult.OK;
                }
                return;
            }

            if (MoveSelection(e.KeyCode, tbFilterFiles))
            {
                e.SuppressKeyPress = true;
            }
        }

        // a user clicked an open tab file entry within the list of open tabs, so handle the click..
        private void DgvOpenFiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // ..validate the index..
            if (e.RowIndex >= 0 && e.RowIndex < dgvOpenFiles.RowCount)
            {
                // ..check if a valid entry was clicked..
                if (DialogOk())
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
