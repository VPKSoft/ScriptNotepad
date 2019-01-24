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

using ScriptNotepad.Database;
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

namespace ScriptNotepad
{
    /// <summary>
    /// A form for user to select saved script snippets from the database.
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogScriptLoad : DBLangEngineWinforms
    {
        private IEnumerable<CODE_SNIPPETS> codeSnippets;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogScriptLoad"/> class.
        /// </summary>
        public FormDialogScriptLoad()
        {
            // Add this form to be positioned..
            PositionForms.Add(this, PositionCore.SizeChangeMode.MoveTopLeft);

            // add positioning..
            PositionCore.Bind();

            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // get the code snippets in the database..
            codeSnippets = Database.Database.GetCodeSnippets();

            // localize the currently supported script types..
            cmbScriptType.Items.Clear();
            cmbScriptType.Items.Add(
                DBLangEngine.GetMessage("msgScriptTypeText", "Script text|As in the C# script type should be handling the Scintilla's contents as text")
                );

            cmbScriptType.Items.Add(
                DBLangEngine.GetMessage("msgScriptTypeLines", "Script lines|As in the C# script type should be handling the Scintilla's contents as lines")
                );

            cmbScriptType.SelectedIndex = 0;

            cmbScriptType.SelectedItem =
                DBLangEngine.GetMessage("msgScriptTypeText", "Script text|As in the C# script type should be handling the Scintilla's contents as text");

            // set the OK button's state based on the value if any script is selected from the script list box..
            btOK.Enabled = lbScriptList.SelectedIndex != -1;
        }

        /// <summary>
        /// Shows this dialog for a user to select a script.
        /// </summary>
        /// <returns>An instance for the <see cref="CODE_SNIPPETS"/> class if user accepted the dialog; otherwise null.</returns>
        public static CODE_SNIPPETS Execute()
        {
            // create a new instance of the FormDialogScriptLoad..
            FormDialogScriptLoad formDialogScriptLoad = new FormDialogScriptLoad();

            // show the dialog..
            if (formDialogScriptLoad.ShowDialog() == DialogResult.OK)
            {
                // if the OK button was selected then return the selected CODE_SNIPPETS class instance..
                return (CODE_SNIPPETS)formDialogScriptLoad.lbScriptList.SelectedItem;
            }
            else
            {
                // user canceled the dialog so do return null..
                return null;
            }
        }


        /// <summary>
        /// Filters the list box contents based on the script type and a possible search string.
        /// </summary>
        /// <param name="type">The type of the script.</param>
        /// <param name="filterText">The text for filtering the snippets by their names.</param>
        private void FilterSnippets(int type, string filterText)
        {
            // select the snipped based on the given parameter values..
            IEnumerable<CODE_SNIPPETS> selectedSnippets =
                codeSnippets.Where(
                    f => f.SCRIPT_TYPE == type &&
                    (filterText.Trim() == string.Empty || f.SCRIPT_NAME.ToLowerInvariant().Contains(filterText.ToLowerInvariant())));

            // list the script snippets to the list box..
            ListScriptSnippets(selectedSnippets);
        }

        /// <summary>
        /// Filters the list box contents based on the script type and a possible search string.
        /// </summary>
        /// <param name="snippets">The snippets.</param>
        private void ListScriptSnippets(IEnumerable<CODE_SNIPPETS> snippets)
        {
            // clear the previous contents from the list box..
            lbScriptList.Items.Clear();

            // list the code snippets to the list box..
            foreach (CODE_SNIPPETS codeSnippet in snippets)
            {
                lbScriptList.Items.Add(codeSnippet);
            }
        }

        // common handler for both the filter type combo box and the search text box changed event..
        private void common_ScriptChanged(object sender, EventArgs e)
        {
            // filter the list box contents based on the given filters..
            FilterSnippets(cmbScriptType.SelectedIndex, tbFilter.Text);
        }

        private void lbScriptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set the OK button's state based on the value if any script is selected from the script list box..
            btOK.Enabled = lbScriptList.SelectedIndex != -1;
        }
    }
}
