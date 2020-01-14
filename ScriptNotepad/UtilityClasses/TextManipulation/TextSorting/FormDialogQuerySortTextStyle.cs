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
using System.Drawing;
using VPKSoft.LangLib;
using System.Windows.Forms;
using ScintillaNET;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.Keyboard;


namespace ScriptNotepad.UtilityClasses.TextManipulation.TextSorting
{
    /// <summary>
    /// A dialog to query advanced text sorting parameters.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogQuerySortTextStyle : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogQuerySortTextStyle"/> class.
        /// </summary>
        public FormDialogQuerySortTextStyle()
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

            ListSortStyles();

            btSort.Enabled = listCheckSortStyles.Items.Count > 0;

            #region NoDesignerVisibility
            pictureBox1.AllowDrop = true;
            pictureBox2.AllowDrop = true;
            #endregion
        }

        #region PrivateMethods
        /// <summary>
        /// Undo the document changes if possible.
        /// </summary>
        private void Undo()
        {
            if (Scintilla.CanUndo)
            {
                FormMain.Undo();
            }

            btUndo.Enabled = Scintilla.CanUndo;
        }

        /// <summary>
        /// Lists the current text sorting "styles".
        /// </summary>
        private void ListSortStyles()
        {
            foreach (var value in Enum.GetValues(typeof(SortTextStyle)))
            {
                listSortStylesAvailable.Items.Add(new SortText {SortTextStyle = (SortTextStyle) value,});
            }
        }

        /// <summary>
        /// Checks if a give <see cref="SortText"/> style exists in the check list box.
        /// </summary>
        /// <param name="sortText">An instance to a <see cref="SortText"/> class to check for.</param>
        /// <returns><c>true</c> if the given <see cref="SortText"/> style exists in the check list box, <c>false</c> otherwise.</returns>
        private bool SortStyleExists(SortText sortText)
        {
            foreach (var item in listCheckSortStyles.Items)
            {
                var itemData = (SortText) item;
                if (itemData.SortTextStyle == sortText.SortTextStyle && itemData.Descending == sortText.Descending)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a <see cref="DragEventArgs.Data"/> is valid to be dropped to the check list box.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
        /// <returns><c>true</c> if the <see cref="DragEventArgs.Data"/> is valid to be dropped to the check list box, <c>false</c> otherwise.</returns>
        private bool CheckListCanDrop(DragEventArgs e)
        {
            var data = (SortText) e.Data.GetData(typeof(SortText));

            if (DragDropOrigin != null && DragDropOrigin.Equals(listCheckSortStyles))
            {
                var checkListBox = listCheckSortStyles;
                int index = checkListBox.IndexFromPoint(checkListBox.PointToClient(new Point(e.X, e.Y)));
                if (checkListBox.Items.IndexOf(data) == index)
                {
                    return false;
                }
                return true;
            }

            if (e.Data.GetDataPresent(typeof(SortText)))
            {
                if (!e.Data.GetDataPresent(typeof(SortText)))
                {
                    return false;
                }

                if (SortStyleExists(data))
                {
                    return false;

                }
            }

            return true;
        }

        /// <summary>
        /// Moves an available sorting item to the check list box of sorting styles to use.
        /// </summary>
        private void MoveToSortCheckListBox()
        {
            if (listSortStylesAvailable.SelectedItem == null)
            {
                return;
            }

            var sortText = (SortText)listSortStylesAvailable.SelectedItem;
            if (SortStyleExists(sortText))
            {
                return;
            }

            if (listCheckSortStyles.SelectedIndex != -1)
            {
                listCheckSortStyles.Items.Insert(listCheckSortStyles.SelectedIndex, sortText);
            }
            else
            {
                listCheckSortStyles.Items.Add(sortText);
            }
            btSort.Enabled = listCheckSortStyles.Items.Count > 0;
        }
        #endregion

        #region EventHandlers
        private void listCheckSortStyles_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var checkListBox = (CheckedListBox) sender;
            var sortText = (SortText) checkListBox.Items[e.Index];
            sortText.Descending = e.NewValue == CheckState.Checked;
        }

        private void listSortStylesAvailable_MouseDown(object sender, MouseEventArgs e)
        {
            StartDragAvailableListBox = true;
            MouseDownPointAvailableListBox = e.Location;
        }

        private void listCheckSortStyles_MouseDown(object sender, MouseEventArgs e)
        {
            StartDragCheckListBox = true;
            MouseDownPointCheckListBox = e.Location;
        }

        private void FormDialogQuerySortTextStyle_MouseUp(object sender, MouseEventArgs e)
        {
            DragDropOrigin = null;
            StartDragCheckListBox = false;
            StartDragAvailableListBox = false;
        }

        private void listCheckSortStyles_DragEnterOver(object sender, DragEventArgs e)
        {
            e.Effect = CheckListCanDrop(e) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void FormDialogQuerySortTextStyle_DragEnterOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listCheckSortStyles_DragDrop(object sender, DragEventArgs e)
        {
            var checkListBox = (CheckedListBox) sender;
            int index = checkListBox.IndexFromPoint(checkListBox.PointToClient(new Point(e.X, e.Y)));
            if (CheckListCanDrop(e))
            {
                var sortText = (SortText) e.Data.GetData(typeof(SortText));
                if (checkListBox.Equals(DragDropOrigin))
                {
                    checkListBox.Items.Remove(sortText);
                }

                if (index != -1)
                {
                    checkListBox.Items.Insert(index, sortText);
                }
                else
                {
                    checkListBox.Items.Add(sortText);
                }
                btSort.Enabled = listCheckSortStyles.Items.Count > 0;
            }

            DragDropOrigin = null;
            StartDragCheckListBox = false;
            StartDragAvailableListBox = false;
        }

        private void FormDialogQuerySortTextStyle_DragDrop(object sender, DragEventArgs e)
        {
            if (listCheckSortStyles.Equals(DragDropOrigin))
            {
                var data = (SortText) e.Data.GetData(typeof(SortText));
                listCheckSortStyles.Items.Remove(data);
            }

            DragDropOrigin = null;
            StartDragCheckListBox = false;
            StartDragAvailableListBox = false;
        }

        private void listCheckSortStyles_Leave(object sender, EventArgs e)
        {
            StartDragCheckListBox = false;
            StartDragAvailableListBox = false;
        }

        private void listCheckSortStyles_MouseMove(object sender, MouseEventArgs e)
        {
            if (!StartDragCheckListBox || MouseDownPointCheckListBox.X == e.X && MouseDownPointCheckListBox.Y == e.Y)
            {
                return;
            }

            var checkListBox = (CheckedListBox) sender;
            int index = checkListBox.IndexFromPoint(new Point(e.X, e.Y));
            if (index != -1 && checkListBox.Items.Count > 0)
            {
                DragDropOrigin = checkListBox;
                checkListBox.DoDragDrop(checkListBox.Items[index], DragDropEffects.All);
            }
        }

        private void listSortStylesAvailable_MouseMove(object sender, MouseEventArgs e)
        {
            if (!StartDragAvailableListBox || MouseDownPointAvailableListBox.X == e.X && MouseDownPointAvailableListBox.Y == e.Y)
            {
                return;
            }

            var listBox = (ListBox) sender;
            int index = listBox.IndexFromPoint(new Point(e.X, e.Y));
            if (index != -1)
            {
                DragDropOrigin = listBox;
                listBox.DoDragDrop(listBox.Items[index], DragDropEffects.Copy);
            }
        }

        private void btSort_Click(object sender, EventArgs e)
        {
            foreach (var item in listCheckSortStyles.Items)
            {
                var itemData = (SortText) item;
                SortLines.Sort(Scintilla, itemData, FormSettings.Settings.TextCurrentComparison);
            }

            btUndo.Enabled = Scintilla.CanUndo;
            StartDragCheckListBox = false;
            StartDragAvailableListBox = false;
        }

        private void btUndo_Click(object sender, EventArgs e)
        {
            Undo();
            StartDragCheckListBox = false;
            StartDragAvailableListBox = false;
        }

        private void FormDialogQuerySortTextStyle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.ModifierKeysDown(false, true, false) && e.KeyCode == Keys.Z)
            {
                Undo();
                e.SuppressKeyPress = true;
            }
            StartDragCheckListBox = false;
            StartDragAvailableListBox = false;
        }

        private void listSortStylesAvailable_DoubleClick(object sender, EventArgs e)
        {
            MoveToSortCheckListBox();
        }


        private void listSortStylesAvailable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && e.NoModifierKeysDown())
            {
                e.SuppressKeyPress = true;
                MoveToSortCheckListBox();
            }
            StartDragCheckListBox = false;
            StartDragAvailableListBox = false;
        }

        private void listCheckSortStyles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.NoModifierKeysDown())
            {
                e.SuppressKeyPress = true;
                if (listCheckSortStyles.SelectedItem != null)
                {
                    listCheckSortStyles.Items.Remove(listCheckSortStyles.SelectedItem);
                }
            }
            StartDragCheckListBox = false;
            StartDragAvailableListBox = false;
        }
        #endregion

        #region PrivateProperties
        private object DragDropOrigin { get; set; }

        private bool StartDragCheckListBox { get; set; }

        private Point MouseDownPointCheckListBox { get; set; }

        private bool StartDragAvailableListBox { get; set; }

        private Point MouseDownPointAvailableListBox { get; set; }

        private Scintilla Scintilla { get; set; }

        private FormMain FormMain { get; set; }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Shows the form as a modal dialog box.
        /// </summary>
        /// <param name="scintilla">The scintilla control which contents to sort.</param>
        /// <param name="formMain">An instance to the <see cref="FormMain"/> Form for an undo operation call.</param>
        /// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
        /// <exception cref="T:System.InvalidOperationException">The form being shown is already visible.-or- The form being shown is disabled.-or- The form being shown is not a top-level window.-or- The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" />).</exception>
        // ReSharper disable once UnusedMember.Global
        public static DialogResult ShowDialog(Scintilla scintilla, FormMain formMain)
        {
            return ShowDialog(null, scintilla, formMain);
        }

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box. </param>
        /// <param name="scintilla">The scintilla control which contents to sort.</param>
        /// <param name="formMain">An instance to the <see cref="FormMain"/> Form for an undo operation call.</param>
        /// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
        /// <exception cref="T:System.InvalidOperationException">The form being shown is already visible.-or- The form being shown is disabled.-or- The form being shown is not a top-level window.-or- The form being shown as a dialog box is already a modal form.-or-The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" />).</exception>
        public static DialogResult ShowDialog(IWin32Window owner, Scintilla scintilla, FormMain formMain)
        {
            var form = new FormDialogQuerySortTextStyle
                {Scintilla = scintilla, btUndo = {Enabled = scintilla.CanUndo}, FormMain = formMain};
            return form.ShowDialog(owner);
        }
        #endregion
    }
}
