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

using ScriptNotepad.Settings;
using System.Linq;
using System.Windows.Forms;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Editor.Utility.ModelHelpers;
using VPKSoft.LangLib;
using VPKSoft.MessageBoxExtended;

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
                DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages");

            ttMain.SetToolTip(pbDeleteSelectedSession,
                DBLangEngine.GetMessage("msgDeleteSession", "Delete selected session|A message indicating an action to delete a session from the database"));

            ttMain.SetToolTip(pbRenameSelectedSession,
                DBLangEngine.GetMessage("msgRenameSession", "Rename selected session|A message indicating an action to rename a session in the database"));

            ttMain.SetToolTip(pbAddNewSessionWithName,
                DBLangEngine.GetMessage("msgAddSessionWithName", "Add a new named session|A message indicating an action to add a new session to the database"));
        }

        /// <summary>
        /// Displays the session management dialog for session management.
        /// </summary>
        public static void Execute()
        {
            // create a new instance of "this" dialog..
            FormDialogSessionManage formDialogSessionManage = new FormDialogSessionManage();

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
            foreach (FileSession session in ScriptNotepadDbContext.DbContext.FileSessions)
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
                ScriptNotepadDbContext.DbContext.FileSessions.Add(new FileSession
                    {SessionName = tbAddNewSessionWithName.Text.Trim(' ')});

                ScriptNotepadDbContext.DbContext.SaveChanges();

                // list the sessions to the combo box..
                ListSessions();

                MessageBoxExtended.Show(
                    DBLangEngine.GetMessage("msgSessionCreateSuccess",
                    "The session was successfully created.|A message indicating a successful session creation."),
                    DBLangEngine.GetMessage("msgSuccess", "Success|A message indicating a successful operation."),
                    MessageBoxButtonsExtended.OK, MessageBoxIcon.Information, ExtendedDefaultButtons.Button1);
            }
            else
            {
                MessageBoxExtended.Show(
                    DBLangEngine.GetMessage("msgWarningInvalidSessionName",
                    "The session name '{0}' is either invalid or already exists in the database|The given session name is invalid (white space or null) or the given session name already exists in the database", tbAddNewSessionWithName.Text),
                    DBLangEngine.GetMessage("msgWarning", "Warning|A message warning of some kind problem."),
                    MessageBoxButtonsExtended.OK, MessageBoxIcon.Warning, ExtendedDefaultButtons.Button1);
            }
        }

        /// <summary>
        /// Validates the name of the session.
        /// </summary>
        /// <param name="textBox">The text box to use for validation.</param>
        /// <param name="canNotExist">If set to <c>true</c> the session name in the given text box may not exists within the current sessions in the database.</param>
        /// <param name="id">An optional ID number for the session to validate.</param>
        /// <returns>True if the session name was validate successfully; otherwise false.</returns>
        private bool ValidateSessionName(TextBox textBox, bool canNotExist, int id = -1)
        {
            bool result =
                !(
                string.IsNullOrWhiteSpace(textBox.Text) ||
                ScriptNotepadDbContext.DbContext.FileSessions.Any(f => f.SessionName.ToLowerInvariant().Trim(' ') == textBox.Text.ToLowerInvariant().Trim(' ')) && canNotExist);

            if (id != -1)
            {
                result &=
                    !ScriptNotepadDbContext.DbContext.FileSessions.Any(f => f.SessionName.ToLowerInvariant().Trim(' ') == textBox.Text.ToLowerInvariant().Trim(' ') && f.Id == id);
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
                var session = (FileSession)cmbSessions.SelectedItem;

                if (ValidateSessionName(tbRenameSession, false, session.Id))
                {
                    // set the session name to the instance..
                    session.SessionName = tbRenameSession.Text.Trim(' ');

                    ScriptNotepadDbContext.DbContext.SaveChanges();

                    // set the session to the settings as it's saved as string using the session name..
                    FormSettings.Settings.CurrentSessionEntity = session;

                    // set the instance back to the combo box..
                    cmbSessions.SelectedItem = session;

                    MessageBoxExtended.Show(
                        DBLangEngine.GetMessage("msgSessionRenameSuccess",
                            "The session was successfully renamed.|A message indicating a successful session rename."),
                        DBLangEngine.GetMessage("msgSuccess", "Success|A message indicating a successful operation."),
                        MessageBoxButtonsExtended.OK, MessageBoxIcon.Information, ExtendedDefaultButtons.Button1);
                }
                else
                {
                    MessageBoxExtended.Show(
                        DBLangEngine.GetMessage("msgWarningInvalidSessionName",
                        "The session name '{0}' is either invalid or already exists in the database|The given session name is invalid (white space or null) or the given session name already exists in the database", tbRenameSession.Text),
                        DBLangEngine.GetMessage("msgWarning", "Warning|A message warning of some kind problem."),
                        MessageBoxButtonsExtended.OK, MessageBoxIcon.Warning, ExtendedDefaultButtons.Button1);
                }
            }
        }

        private void pbDeleteSelectedSession_Click(object sender, EventArgs e)
        {
            if (cmbSessions.SelectedItem != null)
            {
                FileSession session = (FileSession)cmbSessions.SelectedItem;

                if (session.Id == 1) // Id == 1 is the default..
                {
                    MessageBoxExtended.Show(
                        DBLangEngine.GetMessage("msgDefaultSessionCanNotDelete",
                        "The default session can not be deleted.|A message informing that the default session can not be deleted."),
                        DBLangEngine.GetMessage("msgInformation", "Information|A message title describing of some kind information."),
                        MessageBoxButtonsExtended.OK, MessageBoxIcon.Information, ExtendedDefaultButtons.Button1);

                }
                else if (session.SessionName == FormSettings.Settings.CurrentSessionEntity.SessionName)
                {
                    MessageBoxExtended.Show(
                        DBLangEngine.GetMessage("msgCurrentSessionCanNotDelete",
                        "The currently active session can not be deleted.|A message informing that the currently active session can not be deleted."),
                        DBLangEngine.GetMessage("msgInformation", "Information|A message title describing of some kind information."),
                        MessageBoxButtonsExtended.OK, MessageBoxIcon.Information, ExtendedDefaultButtons.Button1);
                }
                else
                {
                    if (MessageBoxExtended.Show(
                    DBLangEngine.GetMessage("msgConfirmDeleteEntireSession",
                    "Please confirm you want to delete an entire session '{0}' from the database. This operation can not be undone. Continue?|A confirmation dialog if the user wishes to delete an entire session from the database. (NO POSSIBILITY TO UNDO!).", session.SessionName),
                    DBLangEngine.GetMessage("msgConfirm", "Confirm|A caption text for a confirm dialog."),
                    MessageBoxButtonsExtended.YesNo, MessageBoxIcon.Question, ExtendedDefaultButtons.Button2) == DialogResultExtended.Yes)
                    {
                        if (FileSessionHelper.DeleteEntireSession(session))
                        {
                            MessageBoxExtended.Show(
                                DBLangEngine.GetMessage("msgSessionDeleteSuccess",
                                "The session was successfully deleted.|A message indicating a successful session delete."),
                                DBLangEngine.GetMessage("msgSuccess", "Success|A message indicating a successful operation."),
                                MessageBoxButtonsExtended.OK, MessageBoxIcon.Information, ExtendedDefaultButtons.Button1);
                        }
                    }
                }
            }
        }
    }
}
