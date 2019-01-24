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
using System.Threading;

namespace ScriptNotepad
{
    public partial class FormScript : DBLangEngineWinforms
    {
        // a list to track the instances of this form so the changes can be delegated to each other..
        private static List<FormScript> formScriptInstances = new List<FormScript>();

        // a CodeDOM provider for executing C# scripts for a list of lines..
        private CSCodeDOMScriptRunnerLines scriptRunnerLines = new CSCodeDOMScriptRunnerLines();

        // a CodeDOM provider for executing C# scripts for a string..
        private CSCodeDOMScriptRunnerText scriptRunnerText = new CSCodeDOMScriptRunnerText();

        // an indicator if the Scintilla's text changed event should be disregarded..
        private bool suspendChangedEvent = false;

        // a field to hold localized name for a script template for manipulating Scintilla contents as text..
        string defaultNameScriptTemplateText = string.Empty;

        // a field to hold localized name for a script template for manipulating Scintilla contents as lines..
        string defaultNameScriptTemplateLines = string.Empty;

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

            // localize the script type default names..
            defaultNameScriptTemplateText = 
                DBLangEngine.GetMessage("msgDefaultScriptSnippetText", "A text script snippet|As in a script for manipulating Scintilla contents as text");

            defaultNameScriptTemplateLines =
                DBLangEngine.GetMessage("msgDefaultScriptSnippetLines", "A line script snippet|As in a script for manipulating Scintilla contents as lines");

            // localize the currently supported script types..
            tsbComboScriptType.Items.Clear();
            tsbComboScriptType.Items.Add(
                DBLangEngine.GetMessage("msgScriptTypeText", "Script text|As in the C# script type should be handling the Scintilla's contents as text")
                );

            tsbComboScriptType.Items.Add(
                DBLangEngine.GetMessage("msgScriptTypeLines", "Script lines|As in the C# script type should be handling the Scintilla's contents as lines")
                );

            suspendChangedEvent = true; // suspend the event handler as the contents of the script is about to change..
            tsbComboScriptType.SelectedItem =
                DBLangEngine.GetMessage("msgScriptTypeText", "Script text|As in the C# script type should be handling the Scintilla's contents as text");

            // set the default script for manipulating text..
            scintilla.Text = scriptRunnerText.CSharpScriptBase;

            // set the text for the default script snippet..
            tstScriptName.Text = defaultNameScriptTemplateText;

            suspendChangedEvent = false; // "resume" the event handler..

            // set the lexer as C#..
            ScintillaLexers.CreateLexer(scintilla, LexerType.Cs);

            // track the instances of this form so the changes can be delegated to each other..
            formScriptInstances.Add(this);
        }

        private void FormScript_FormClosing(object sender, FormClosingEventArgs e)
        {
            // this form is no longer going to be an instance for much longer..
            formScriptInstances.Remove(this);
        }

        /// <summary>
        /// Gets the type of the selected script in the tool strip's combo box.
        /// </summary>
        private int SelectedScriptType
        {
            get
            {
                // the tool strip combo box doesn't seem to remember it's index,
                // so get the index by using another way..
                return tsbComboScriptType.Items.IndexOf(tsbComboScriptType.Text);
            }
        }

        // a user wants to run the script against the active Scintilla document on the main form..
        private void tsbRunScript_Click(object sender, EventArgs e)
        {
            // a text contents manipulation script was requested..
            if (SelectedScriptType == 0)
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
            else if (SelectedScriptType == 1)
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
        /// Occurs when a user wants to run a script for a Scintilla document contents.
        /// </summary>
        public event OnScintillaRequired ScintillaRequired = null;


        // a user wishes to load a script from the database..
        private void tsbOpen_Click(object sender, EventArgs e)
        {
            // display the script dialog..
            CODE_SNIPPETS snippet = FormDialogScriptLoad.Execute();

            // if a selection was made..
            if (snippet != null)
            {
                suspendChangedEvent = true; // suspend the event handler as the contents of the script is about to change..
                tstScriptName.Text = snippet.SCRIPT_NAME;
                scintilla.Text = snippet.SCRIPT_CONTENTS;
                tsbComboScriptType.SelectedIndex = snippet.SCRIPT_TYPE;
                suspendChangedEvent = false; // "resume" the event handler..
            }
        }

        /// <summary>
        /// Enables or disabled the controls that might end up for the user to lose his work.
        /// </summary>
        /// <param name="enable">A flag indicating if the controls should be enabled or disabled.</param>
        private void EnableDisableControlsOnChange(bool enable)
        {
            tsbComboScriptType.Enabled = enable; // set the value..
            tsbOpen.Enabled = enable;
        }


        // the text was changed either in the scintilla or in the script's
        // name text box or the script type combo box selected item was changed..
        private void common_Changed(object sender, EventArgs e)
        {
            if (!suspendChangedEvent) // only do something if "listening" flag is set to enabled..
            {
                if (sender.Equals(tsbComboScriptType))
                {
                    suspendChangedEvent = true; // suspend the event handler as the contents of the script is about to change..
                    if (tsbComboScriptType.SelectedIndex == 0)
                    {
                        scintilla.Text = scriptRunnerText.CSharpScriptBase;
                        tstScriptName.Text = defaultNameScriptTemplateText;
                    }
                    else
                    {
                        scintilla.Text = scriptRunnerLines.CSharpScriptBase;
                        tstScriptName.Text = defaultNameScriptTemplateLines;
                    }
                    suspendChangedEvent = false; // "resume" the event handler..
                    return;
                }

                // don't allow the user to lose one's work on the current script..
                EnableDisableControlsOnChange(false);
            }
        }

        private void tsbDiscardChanges_Click(object sender, EventArgs e)
        {
            // enable the controls as the user chose to discard the changes of the script..
            EnableDisableControlsOnChange(true);
        }

        private void ErrorBlink()
        {
            Color backColorSave = tstScriptName.BackColor;
            for (int i = 0; i < 6; i++)
            {
                tstScriptName.BackColor = (i % 2) == 0 ? Color.Red : backColorSave;
                Thread.Sleep(100);
                Application.DoEvents();
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            ErrorBlink();
        }
    }
}
