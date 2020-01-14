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
using System.Globalization;
using System.Windows.Forms;
using ScriptNotepad.DialogForms;
using ScriptNotepad.Localization;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.Keyboard;
using ScriptNotepad.UtilityClasses.SearchAndReplace;
using VPKSoft.ErrorLogger;

// ReSharper disable once CheckNamespace
namespace ScriptNotepad
{
    /// <summary>
    /// A class to contain the main form event handlers; Takes too much code to be in the main form's code file.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    partial class FormMain
    {
        // the menu including time formats is about to be opened..
        private void MnuInsertDateAndTime_DropDownOpening(object sender, EventArgs e)
        {
            var toolStripMenuItem = (ToolStripMenuItem) sender;
            var useInvariantCulture = FormSettings.Settings.DateFormatUseInvariantCulture;

            foreach (ToolStripMenuItem toolStripSubMenuItem in toolStripMenuItem.DropDownItems)
            {
                var formatNumber = int.Parse(toolStripSubMenuItem.Tag.ToString());
                string format;
                switch (formatNumber)
                {
                    case 0:
                        format = FormSettings.Settings.DateFormat1;
                        break;
                    case 1:
                        format = FormSettings.Settings.DateFormat2;
                        break;
                    case 2:
                        format = FormSettings.Settings.DateFormat3;
                        break;
                    case 3:
                        format = FormSettings.Settings.DateFormat4;
                        break;
                    case 4:
                        format = FormSettings.Settings.DateFormat5;
                        break;
                    case 5:
                        format = FormSettings.Settings.DateFormat6;
                        break;
                    default:
                        format = FormSettings.Settings.DateFormat1;
                        break;
                }

                // must try in case the user has specified and invalid date-time format..
                try
                {

                    toolStripSubMenuItem.Text = DBLangEngine.GetMessage("msgDateTimeMenuItemText",
                        "Insert date and time type {0}: '{1}'|A message describing a text to insert a date and/or time to a Scintilla instance via a menu strip item.",
                        formatNumber + 1,
                        DateTime.Now.ToString(format,
                            // we need to ensure that an overridden thread locale will not affect the non-invariant culture setting..
                            useInvariantCulture ? CultureInfo.InvariantCulture : CultureInfo.InstalledUICulture));
                }
                catch (Exception ex)
                {
                    toolStripSubMenuItem.Text = DBLangEngine.GetMessage(
                        "msgDateTimeInvalidFormat",
                        "Invalid date and/or time format|The user has issued an non-valid formatted date and/or time formatting string.");

                    // log the exception..
                    ExceptionLogger.LogError(ex);
                }
            }
        }

        private void GotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentScintillaAction(FormDialogQueryJumpLocation.Execute);
        }

        // user wants to insert a date and/or time to the current Scintilla document..
        private void MnuDate1_Click(object sender, EventArgs e)
        {
            CurrentScintillaAction(scintilla =>
            {
                string format;

                int formatNumber = int.Parse(((ToolStripMenuItem) sender).Tag.ToString());

                var useInvariantCulture = FormSettings.Settings.DateFormatUseInvariantCulture;

                switch (formatNumber)
                {
                    case 0:
                        format = FormSettings.Settings.DateFormat1;
                        break;
                    case 1:
                        format = FormSettings.Settings.DateFormat2;
                        break;
                    case 2:
                        format = FormSettings.Settings.DateFormat3;
                        break;
                    case 3:
                        format = FormSettings.Settings.DateFormat4;
                        break;
                    case 4:
                        format = FormSettings.Settings.DateFormat5;
                        break;
                    case 5:
                        format = FormSettings.Settings.DateFormat6;
                        break;
                    default:
                        format = FormSettings.Settings.DateFormat1;
                        break;
                }

                try
                {
                    scintilla.InsertText(scintilla.CurrentPosition,
                        DateTime.Now.ToString(format,
                            // we need to ensure that an overridden thread locale will not affect the non-invariant culture setting..
                            useInvariantCulture ? CultureInfo.InvariantCulture : CultureInfo.InstalledUICulture));
                }
                // must try in case the user has specified and invalid date-time format..
                catch (Exception ex)
                {
                    // TODO:: Show an error dialog..

                    // log the exception..
                    ExceptionLogger.LogError(ex);
                }
            });
        }

        // handles navigation to the next and previous tab and
        // to the next and previous session..
        private void MnuNextPrevious_Click(object sender, EventArgs e)
        {
            if (sender.Equals(mnuNextTab))
            {
                sttcMain.NextTab(true);
            }
            else if (sender.Equals(mnuPreviousTab))
            {
                sttcMain.PreviousTab(true);
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            // a user wishes to navigate within the FormSearchResultTree..
            if (e.OnlyAlt() && e.KeyCodeIn(Keys.Left, Keys.Right, Keys.X))
            {
                // validate that there is an instance of the FormSearchResultTree which is visible..
                if (FormSearchResultTree.PreviousInstance != null && FormSearchResultTree.PreviousInstance.Visible)
                {
                    // Alt+Left navigates to the previous tree node within the form..
                    if (e.KeyCode == Keys.Left) 
                    {
                        FormSearchResultTree.PreviousInstance.PreviousOccurrence();
                    }
                    // Alt+Right navigates to the next tree node within the form..
                    else if (e.KeyCode == Keys.Right)
                    {
                        FormSearchResultTree.PreviousInstance.NextOccurrence();
                    }
                    // Alt+X closes the FormSearchResultTree instance..
                    else
                    {
                        FormSearchResultTree.PreviousInstance.CloseTree();
                    }

                    // this is handled..
                    e.Handled = true;
                }
                return;
            }

            // a user pressed the insert key a of a keyboard, which indicates toggling for
            // insert / override mode for the Scintilla control..
            if (e.KeyCode == Keys.Insert && e.NoModifierKeysDown())
            {
                // only if a document exists..
                if (sttcMain.CurrentDocument != null)
                {
                    // ..set the insert / override text for the status strip..
                    StatusStripTexts.SetInsertOverrideStatusStripText(sttcMain.CurrentDocument, true);
                }
                return;
            }

            if (e.KeyCode == Keys.F3)
            {
                if (e.OnlyShift() || e.NoModifierKeysDown())
                {
                    // find the next result if available..
                    FormSearchAndReplace.Instance.Advance(!e.OnlyShift());

                    // this is handled..
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyCodeIn(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.PageDown, Keys.PageUp))
            {
                // set the flag to suspend the selection update to avoid excess CPU load..
                suspendSelectionUpdate = true;
            }
        }

        // the navigation menu is opening, so set the drop-down items states accordingly..
        private void MnuNavigation_DropDownOpening(object sender, EventArgs e)
        {
            mnuNextTab.Enabled = sttcMain.DocumentsCount > 0;
            mnuPreviousTab.Enabled = sttcMain.DocumentsCount > 0;
        }
    }
}
