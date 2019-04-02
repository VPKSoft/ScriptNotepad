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

using ScriptNotepad.Database.TableMethods;
using ScriptNotepad.Database.Tables;
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

namespace ScriptNotepad.UtilityClasses.Session
{
    /// <summary>
    /// A dialog to manage sessions within the software.
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogSessionManage : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogSessionManage"/> class.
        /// </summary>
        public FormDialogSessionManage()
        {
            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            ttMain.SetToolTip(pbDeleteSelectedSession,
                DBLangEngine.GetMessage("msgDeleteSession", "Delete selected session|A message indicating an action to delete a session from the database"));

            ttMain.SetToolTip(pbRenameSelectedSession,
                DBLangEngine.GetMessage("msgRenameSession", "Rename selected session|A message indicating an action to rename a session in the database"));

            ttMain.SetToolTip(pbAddNewSessionWithName,
                DBLangEngine.GetMessage("msgAddSessionWithName", "Add a new named session|A message indicating an action to add a new session to the database"));
        }

        /// <summary>
        /// An internal list of the sessions in the database.
        /// </summary>
        private List<SESSION_NAME> sessions = new List<SESSION_NAME>();

        /// <summary>
        /// Displays the session management dialog for session management.
        /// </summary>
        public static void Execute()
        {
            // create a new instance of "this" dialog..
            FormDialogSessionManage formDialogSessionManage = new FormDialogSessionManage
            {
                // get the session list from the database..
                sessions = DatabaseSessionName.GetSessions()
            };

            // display the dialog..
            formDialogSessionManage.ShowDialog();
        }

        /// <summary>
        /// Handles the Shown event of the FormDialogSessionManage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FormDialogSessionManage_Shown(object sender, EventArgs e)
        {
            // list the sessions to the combo box..
            ListSessions();
        }

        /// <summary>
        /// Lists the sessions in the database.
        /// </summary>
        private void ListSessions()
        {
            cmbSessions.Items.Clear();

            // list the sessions to the combo box..
            foreach (SESSION_NAME session in sessions)
            {
                cmbSessions.Items.Add(session);
            }

            // set the combo box index if there are any sessions in the list..
            if (cmbSessions.Items.Count > 0)
            {
                cmbSessions.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Handles the Click event of the pbAddNewSessionWithName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pbAddNewSessionWithName_Click(object sender, EventArgs e)
        {
            // a user wishes to add a new session with a name..
            if (ValidateSessionName(tbAddNewSessionWithName, true))
            {
                DatabaseSessionName.InsertSession(tbAddNewSessionWithName.Text.Trim(' '));

                // get the session list from the database..
                sessions = DatabaseSessionName.GetSessions();

                // list the sessions to the combo box..
                ListSessions();
            }
            else
            {
                MessageBox.Show(
                    DBLangEngine.GetMessage("msgWarningInvalidSessionName",
                    "The session name '{0}' is either invalid or already exists in the database|The given session name is invalid (white space or null) or the given session name already exists in the database", tbAddNewSessionWithName.Text),
                    DBLangEngine.GetMessage("msgWarning", "Warning|A message warning of some kind problem."),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Validates the name of the session.
        /// </summary>
        /// <param name="textBox">The text box to use for validation.</param>
        /// <param name="canNotExist">If set to <c>true</c> the session name in the given text box may not exists within the current sessions in the database.</param>
        /// <param name="ID">An optional ID number for the session to validate.</param>
        /// <returns>True if the session name was validate successfully; otherwise false.</returns>
        private bool ValidateSessionName(TextBox textBox, bool canNotExist, long ID = -1)
        {
            bool result =
                !(
                string.IsNullOrWhiteSpace(textBox.Text) ||
                (sessions.Exists(f => f.SESSIONNAME.ToLowerInvariant().Trim(' ') == textBox.Text.ToLowerInvariant().Trim(' ')) && canNotExist));

            if (ID != -1)
            {
                result &=
                    !sessions.Exists(f => f.SESSIONNAME.ToLowerInvariant().Trim(' ') == textBox.Text.ToLowerInvariant().Trim(' ') && f.SESSIONID == ID);
            }

            return result;
        }

        /// <summary>
        /// Handles the Click event of the pbRenameSelectedSession control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pbRenameSelectedSession_Click(object sender, EventArgs e)
        {
            if (cmbSessions.SelectedItem != null)
            {
                SESSION_NAME session = (SESSION_NAME)cmbSessions.SelectedItem;

                if (ValidateSessionName(tbRenameSession, false, session.SESSIONID))
                {
                    if (DatabaseSessionName.UpdateSessionName(session, tbRenameSession.Text.Trim(' ')))
                    {
                        // set the session name to the instance..
                        session.SESSIONNAME = tbRenameSession.Text.Trim(' ');

                        // set the instance back to the combo box..
                        cmbSessions.SelectedItem = session;
                    }
                    else
                    {
                        MessageBox.Show(
                            DBLangEngine.GetMessage("msgWarningInvalidSessionName",
                            "The session name '{0}' is either invalid or already exists in the database|The given session name is invalid (white space or null) or the given session name already exists in the database", tbRenameSession.Text),
                            DBLangEngine.GetMessage("msgWarning", "Warning|A message warning of some kind problem."),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBox.Show(
                        DBLangEngine.GetMessage("msgWarningInvalidSessionName",
                        "The session name '{0}' is either invalid or already exists in the database|The given session name is invalid (white space or null) or the given session name already exists in the database", tbRenameSession.Text),
                        DBLangEngine.GetMessage("msgWarning", "Warning|A message warning of some kind problem."),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
        }
    }
}
