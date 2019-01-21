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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPKSoft.LangLib;
using VPKSoft.PosLib;
using VPKSoft.ScintillaLexers;
using ScriptNotepad.UtilityClasses.CodeDom;
using ScintillaNET;
using ScriptNotepad.UtilityClasses.ScintillaHelpers;
using ScriptNotepad.Database;

namespace ScriptNotepad
{
    public partial class FormScript : DBLangEngineWinforms
    {
        // a list to track the instances of this form so the changes can be delegated to each other..
        private static List<FormScript> formScriptInstances = new List<FormScript>();

        // a CodeDOM provider for executing C# scripts for a list of lines..
        private CSCodeDOMeScriptRunnerLines scriptRunnerLines = new CSCodeDOMeScriptRunnerLines();

        // a CodeDOM provider for executing C# scripts for a string..
        private CSCodeDOMScriptRunnerText scriptRunnerText = new CSCodeDOMScriptRunnerText();

        /// <summary>
        /// A delegate for the ScintillaRequired event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="ScintillaRequiredEventArgs"/> instance containing the event data.</param>
        public delegate void OnScintillaRequired(object sender, ScintillaRequiredEventArgs e);

        /// <summary>
        /// Event arguments for the ScintillaRequired event.
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public class ScintillaRequiredEventArgs: EventArgs
        {
            /// <summary>
            /// Gets or sets the Scintilla document which contents to manipulate via a C# script.
            /// </summary>
            public Scintilla Scintilla { get; set; } = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormScript"/> class.
        /// </summary>
        public FormScript()
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

            // set the main form's reference so that the active document can be reached..

            tsbComboScriptType.Items.Clear();
            tsbComboScriptType.Items.Add(
                DBLangEngine.GetMessage("msgScriptTypeText", "Script text|As in the C# script type should be handling the Scintilla's contents as text")
                );

            tsbComboScriptType.Items.Add(
                DBLangEngine.GetMessage("msgScriptTypeLines", "Script lines|As in the C# script type should be handling the Scintilla's contents as lines")
                );

            tsbComboScriptType.SelectedItem =
                DBLangEngine.GetMessage("msgScriptTypeText", "Script text|As in the C# script type should be handling the Scintilla's contents as text");

            // set the lexer as C#..
            ScintillaLexers.CreateLexer(scintilla, LexerType.Cs);

            // track the instances of this form so the changes can be delegated to each other..
            formScriptInstances.Add(this);

            // load the saved code snippets to the combo box on the tool strip..
            ReloadCodeSnippets(true);
        }

        private void FormScript_FormClosing(object sender, FormClosingEventArgs e)
        {
            // this form is no longer going to be an instance for much longer..
            formScriptInstances.Remove(this);
        }

        private int previousCmbIndex = -1;

        // a user selected the C# script type so set the C# script base accordingly..
        private void tsbComboScriptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the tool strip's combo box..
            ToolStripComboBox comboBox = (ToolStripComboBox)sender;

            // a text contents manipulation script was requested..
            if (comboBox.SelectedIndex == 0)
            {
                // set the C# script skeleton to the Scintilla..
                scintilla.Text = scriptRunnerText.ScriptCode;                
            }
            // a line contents manipulation script was requested..
            else if (comboBox.SelectedIndex == 1)
            {
                // set the C# script skeleton to the Scintilla..
                scintilla.Text = scriptRunnerLines.ScriptCode;
            }

            // this needs to be saved as it doesn't seem to last..
            previousCmbIndex = comboBox.SelectedIndex;
        }

        // a user wants to run the script against the active Scintilla document on the main form..
        private void tsbRunScript_Click(object sender, EventArgs e)
        {
            // a text contents manipulation script was requested..
            if (previousCmbIndex == 0)
            {
                // set the script code from the Scintilla document contents..
                scriptRunnerText.ScriptCode = scintilla.Text;

                tbCompilerResults.Text = string.Empty; // clear the previous results..

                // loop through the compilation results..
                for (int i = 0; i < scriptRunnerText.CompilerResults.Output.Count; i++)
                {
                    tbCompilerResults.Text += scriptRunnerText.CompilerResults.Output[i] + Environment.NewLine;
                }

                // no need to continue if the script compilation failed..
                if (scriptRunnerText.CompileFailed)
                {
                    return;
                }

                // create a new instance of a ScintillaRequiredEventArgs class..
                ScintillaRequiredEventArgs args = new ScintillaRequiredEventArgs();

                // if the ScintillaRequired event is subscribed do raise the event..
                ScintillaRequired?.Invoke(this, args);

                // we really don't care if the event was subscribed or not,
                // only the value of the ScintillaRequiredEventArgs needs checking..
                if (args.Scintilla != null)
                {
                    // a reference to a Scintilla document was gotten so do run the code..
                    args.Scintilla.Text = scriptRunnerText.ExecuteText(args.Scintilla.Text);
                }
            }
            // a line contents manipulation script was requested..
            else if (previousCmbIndex == 1)
            {
                // set the script code from the Scintilla document contents,
                // this also pre-compiles the code so do list the compilation results
                // to the text box..
                scriptRunnerLines.ScriptCode = scintilla.Text;

                tbCompilerResults.Text = string.Empty; // clear the previous results..

                // loop through the compilation results..
                for (int i = 0; i < scriptRunnerLines.CompilerResults.Errors.Count; i++)
                {
                    tbCompilerResults.Text += scriptRunnerLines.CompilerResults.Errors[i].ErrorText + Environment.NewLine;
                }

                // no need to continue if the script compilation failed..
                if (scriptRunnerLines.CompileFailed)
                {
                    return;
                }

                // create a new instance of a ScintillaRequiredEventArgs class..
                ScintillaRequiredEventArgs args = new ScintillaRequiredEventArgs();

                // if the ScintillaRequired event is subscribed do raise the event..
                ScintillaRequired?.Invoke(this, args);

                // we really don't care if the event was subscribed or not,
                // only the value of the ScintillaRequiredEventArgs needs checking..
                if (args.Scintilla != null)
                {
                    // a reference to a Scintilla document was gotten so do run the code..
                    args.Scintilla.Text = scriptRunnerLines.ExecuteLines(ScintillaLines.GetLinesAsList(args.Scintilla));
                }
            }
        }

        /// <summary>
        /// Reloads the code snippets for the current instance of the <see cref="FormScript"/> class or to all instances based on the <paramref name="thisInstance"/> value.
        /// </summary>
        /// <param name="thisInstance">if set to <c>true</c> only this instances code snippet combo's contents are reloaded; otherwise all.</param>
        private void ReloadCodeSnippets(bool thisInstance)
        {
            IEnumerable<CODE_SNIPPETS> codeSnippets = Database.Database.GetCodeSnippets();
            foreach (FormScript formScript in formScriptInstances)
            {
                if (thisInstance && !formScript.Equals(this))
                {
                    continue;
                }
                formScript.tsbComboSavedScripts.Items.Clear();
                foreach (CODE_SNIPPETS codeSnippet in codeSnippets)
                {
                    formScript.tsbComboSavedScripts.Items.Add(codeSnippet);
                }
            }
        }

        /// <summary>
        /// Occurs when a user wants to run a script for a Scintilla document contents.
        /// </summary>
        public event OnScintillaRequired ScintillaRequired = null;

        private void tsbComboSavedScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox comboBox = (ToolStripComboBox)sender;
            if (comboBox.SelectedIndex != -1)
            {
                CODE_SNIPPETS codeSnippet = (CODE_SNIPPETS)comboBox.SelectedItem;
                scintilla.Text = codeSnippet.SCRIPT_CONTENTS;
                tsbTextScriptName.Text = codeSnippet.SCRIPT_NAME;
            }
        }
    }
}
