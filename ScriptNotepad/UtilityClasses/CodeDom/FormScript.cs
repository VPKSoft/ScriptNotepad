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

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ScintillaNET;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Database.Entity.Enumerations;
using ScriptNotepad.DialogForms;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.ScintillaHelpers;
using VPKSoft.LangLib;
using VPKSoft.MessageBoxExtended;
using VPKSoft.PosLib;
using VPKSoft.ScintillaLexers;
using VPKSoft.Utils;
using static VPKSoft.ScintillaLexers.LexerEnumerations;
using Utils = VPKSoft.LangLib.Utils;

namespace ScriptNotepad.UtilityClasses.CodeDom;

/// <summary>
/// A windows form tho run a C# script against a Scintilla document's contents.
/// </summary>
/// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
public partial class FormScript : DBLangEngineWinforms
{
    #region PrivateFields
    // a list to track the instances of this form so the changes can be delegated to each other..
    // ReSharper disable once CollectionNeverQueried.Local
    private static readonly List<FormScript> FormScriptInstances = new List<FormScript>();

    // a CodeDOM provider for executing C# scripts for a list of lines..
    private readonly CsCodeDomScriptRunnerLines scriptRunnerLines = new CsCodeDomScriptRunnerLines();

    // a CodeDOM provider for executing C# scripts for a string..
    private readonly CsCodeDomScriptRunnerText scriptRunnerText = new CsCodeDomScriptRunnerText();

    // an indicator if the Scintilla's text changed event should be disregarded..
    private bool suspendChangedEvent;

    // a field to hold localized name for a script template for manipulating Scintilla contents as text..
    private readonly string defaultNameScriptTemplateText = string.Empty;

    // a field to hold localized name for a script template for manipulating Scintilla contents as lines..
    private readonly string defaultNameScriptTemplateLines = string.Empty;

    // a field to hold the code snippet's contents for saving possibility..
    private CodeSnippet currentCodeSnippet;
    #endregion

    #region PrivateProperties        
    #endregion

    #region MassiveConstructor
    /// <summary>
    /// Initializes a new instance of the <see cref="FormScript"/> class.
    /// </summary>
    public FormScript()
    {
        // Add this form to be positioned..
        PositionForms.Add(this);

        // add positioning..
        PositionCore.Bind(ApplicationType.WinForms);

        InitializeComponent();

        DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

        if (Utils.ShouldLocalize() != null)
        {
            DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
            return; // After localization don't do anything more..
        }

        suspendChangedEvent = true; // suspend the event handler as the contents of the script is about to change..

        // initialize the language/localization database..
        DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages");

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

        tsbComboScriptType.SelectedItem =
            DBLangEngine.GetMessage("msgScriptTypeText", "Script text|As in the C# script type should be handling the Scintilla's contents as text");

        // set the default script for manipulating text..
        scintillaScript.Text = scriptRunnerText.CSharpScriptBase;

        // set the text for the default script snippet..
        tstScriptName.Text = defaultNameScriptTemplateText;
            
        CreateNewCodeSnippet(); // create a new CODE_SNIPPETS class instance..

        // set the lexer as C#..
        ScintillaLexers.CreateLexer(scintillaScript, LexerType.Cs);

        // highlight the braces..
        SetBraceHighlights();
        scintillaScript.Margins[0].Width = 20;

        suspendChangedEvent = false; // "resume" the event handler..

        // track the instances of this form so the changes can be delegated to each other..
        FormScriptInstances.Add(this);
    }
    #endregion

    #region InternalHelpers
    /// <summary>
    /// Gets the type of the selected script in the tool strip's combo box.
    /// </summary>
    private int SelectedScriptType => 
        // the tool strip combo box doesn't seem to remember it's index,
        // so get the index by using another way..
        tsbComboScriptType.Items.IndexOf(tsbComboScriptType.Text);

    /// <summary>
    /// Compiles the script in the view and outputs the compilation results.
    /// </summary>
    /// <returns>True if the compilation was successful; otherwise false.</returns>
    private bool Compile()
    {
        tbCompilerResults.Text = string.Empty; // clear the previous results..

        IScriptRunner scriptRunnerParent;
        if (SelectedScriptType == 0)
        {
            scriptRunnerParent = scriptRunnerText;
        }
        else if (SelectedScriptType == 1)
        {
            scriptRunnerParent = scriptRunnerLines;
        }
        else
        {
            tbCompilerResults.Text +=
                DBLangEngine.GetMessage("msgScriptCompileFailed", "Compile failed.|A text to be shown if a script snippet compilation wasn't successful.") +
                Environment.NewLine;
            return false;
        }

        // set the script code from the Scintilla document contents..
        scriptRunnerParent.ScriptCode = scintillaScript.Text;

        if (scriptRunnerParent.CompileFailed)
        {
            // loop through the compilation results..
            tbCompilerResults.Text += scriptRunnerParent.CompileException?.Message + Environment.NewLine;

            Exception exception;

            while ((exception = scriptRunnerParent.CompileException?.InnerException) != null)
            {
                tbCompilerResults.Text += exception.Message + Environment.NewLine;
            }
        }

        // no need to continue if the script compilation failed..
        if (scriptRunnerParent.CompileFailed)
        {
            tbCompilerResults.Text +=
                DBLangEngine.GetMessage("msgScriptCompileFailed", "Compile failed.|A text to be shown if a script snippet compilation wasn't successful.") +
                Environment.NewLine;
            return false;
        }

        tbCompilerResults.Text +=
            DBLangEngine.GetMessage("msgScriptCompileSuccess", "Compile successful.|A text to be shown if a script snippet compilation was successful.") +
            Environment.NewLine;

        return true;
    }

    /// <summary>
    /// Blinks the name of the script in the tool strip if a user has set a "reserved" name for the script
    /// or a user tries to save a script as with an existing name.
    /// </summary>
    private void ErrorBlink()
    {
        // save the background color to a variable..
        Color backColorSave = tstScriptName.BackColor;

        // loop few times changing the color from red to the original color..
        for (int i = 0; i < 6; i++)
        {
            // a normal remainder operator for two options..
            tstScriptName.BackColor = (i % 2) == 0 ? Color.Red : backColorSave;
            Thread.Sleep(100); // sleep form a hundred milliseconds..
            Application.DoEvents(); // keep the GUI alive..
        }
    }

    /// <summary>
    /// Creates a CODE_SNIPPETS class instance from the GUI items.
    /// </summary>
    /// <returns>A CODE_SNIPPETS class instance.</returns>
    private void CreateNewCodeSnippet()
    {
        // just call the overload..
        CreateNewCodeSnippet(scintillaScript.Text, tstScriptName.Text, SelectedScriptType);
    }

    /// <summary>
    /// Creates a CODE_SNIPPETS class instance from the given parameters.
    /// </summary>
    /// <param name="contents">The contents of the script.</param>
    /// <param name="name">The name of the script.</param>
    /// <param name="scriptType">The type of the script.</param>
    /// <returns>A CODE_SNIPPETS class instance.</returns>
    private CodeSnippet CreateNewCodeSnippet(string contents, string name, int scriptType)
    {
        // return a new CODE_SNIPPETS class instance using the given parameters..
        return currentCodeSnippet = new CodeSnippet
        {
            ScriptContents = contents,
            ScriptName = name,
            ScriptLanguage = CodeSnippetLanguage.Cs,
            ScriptTextManipulationType = (ScriptSnippetType)scriptType, 
            Modified = DateTime.Now,
        };
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

    /// <summary>
    /// Validates the name of the script (i.e. reserved words or existing scripts).
    /// </summary>
    /// <param name="scriptName">A name of the script of which validity to check.</param>
    /// <returns>True if the given script name was valid; otherwise false.</returns>
    private bool ValidateScriptName(string scriptName)
    {
        // no white space allowed..
        scriptName = scriptName.Trim();

        // an unnamed script is not allowed..
        if (scriptName == string.Empty)
        {
            return false;
        }

        var result = !((scriptName == defaultNameScriptTemplateText || // a localized template name for text will not do..
                        scriptName == defaultNameScriptTemplateLines || // a localized template name for lines will not do..
                        scriptName == "Simple line ending change script" || // a non-localized database template for lines will not do..
                        scriptName == "Simple replace script" ||
                        scriptName == "Simple XML manipulation script"));

        // return the result..
        return result;
    }
    #endregion

    #region InternalEvents
    private void FormScript_FormClosing(object sender, FormClosingEventArgs e)
    {
        // this form is no longer going to be an instance for much longer..
        FormScriptInstances.Remove(this);
    }

    // a user wants to run the script against the active Scintilla document on the main form..
    private async void tsbRunScript_Click(object sender, EventArgs e)
    {
        // no need to continue if the script compilation failed..
        if (!Compile())
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
            // a text contents manipulation script was requested..
            if (SelectedScriptType == 0)
            {
                var evaluateSuccess = await scriptRunnerText.Evaluate(args.Scintilla.Text);

                // a reference to a Scintilla document was gotten so do run the code..
                var result = await scriptRunnerText.ExecuteText(args.Scintilla.Text);
                if (result.Value && evaluateSuccess)
                {
                    args.Scintilla.Text = result.Key;
                }
            }
            // a line contents manipulation script was requested..
            else if (SelectedScriptType == 1)
            {
                var evaluateSuccess = await scriptRunnerLines.Evaluate(args.Scintilla.Text);

                // a reference to a Scintilla document was gotten so do run the code..
                var result =
                    await scriptRunnerLines.ExecuteLinesAsync(ScintillaLines.GetLinesAsList(args.Scintilla));

                if (result.Value && evaluateSuccess)
                {
                    args.Scintilla.Text = result.Key;
                }
            }
        }
    }

    // a user wishes to load a script from the database..
    private void tsbOpen_Click(object sender, EventArgs e)
    {
        // display the script dialog..
        var snippet = FormDialogScriptLoad.Execute(false);

        // if a selection was made..
        if (snippet != null)
        {
            suspendChangedEvent = true; // suspend the event handler as the contents of the script is about to change..
            tstScriptName.Text = snippet.ScriptName;
            scintillaScript.Text = snippet.ScriptContents;
            tsbComboScriptType.SelectedIndex = (int)snippet.ScriptTextManipulationType;
            suspendChangedEvent = false; // "resume" the event handler..
            currentCodeSnippet = snippet;
        }
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
                    scintillaScript.Text = scriptRunnerText.CSharpScriptBase;
                    tstScriptName.Text = defaultNameScriptTemplateText;
                    CreateNewCodeSnippet(); // create a new CODE_SNIPPETS class instance..
                }
                else
                {
                    scintillaScript.Text = scriptRunnerLines.CSharpScriptBase;
                    tstScriptName.Text = defaultNameScriptTemplateLines;
                    CreateNewCodeSnippet(); // create a new CODE_SNIPPETS class instance..
                }
                suspendChangedEvent = false; // "resume" the event handler..
                return;
            }

            if (currentCodeSnippet == null)
            {
                CreateNewCodeSnippet(); // create a new CODE_SNIPPETS class instance..
            }

            currentCodeSnippet.ScriptName = tstScriptName.Text;
            currentCodeSnippet.ScriptContents = scintillaScript.Text;

            // don't allow the user to lose one's work on the current script..
            EnableDisableControlsOnChange(false);
        }
    }

    private void tsbDiscardChanges_Click(object sender, EventArgs e)
    {
        // enable the controls as the user chose to discard the changes of the script..
        EnableDisableControlsOnChange(true);
    }

    // an event which occurs when the save or a save as button is clicked..
    private void tsbSaveButtons_Click(object sender, EventArgs e)
    {
        // if the script's name is invalid..
        if (!ValidateScriptName(tstScriptName.Text))
        {
            ErrorBlink(); // ..indicate an error..
            return; // ..and return..
        }

        // if the currentCodeSnippet is null then create a new one..
        if (currentCodeSnippet == null)
        {
            currentCodeSnippet = CreateNewCodeSnippet(scintillaScript.Text, tstScriptName.Text, SelectedScriptType);
        }

        // display an error if the script's name is any of the reserved script names..
        if (ScriptNotepadDbContext.DbContext.CodeSnippets.Any(f => f.ScriptName.In(
                defaultNameScriptTemplateText,
                defaultNameScriptTemplateLines, 
                "Simple line ending change script", 
                "Simple replace script",
                "Simple XML manipulation script")))
        {
            MessageBoxExtended.Show(
                DBLangEngine.GetMessage("msgReservedScriptName", "The given script name is reserved for the script samples. The script wasn't saved.|A message informing that the given script name is reserved for static sample scripts."),
                DBLangEngine.GetMessage("msgWarning",
                    "Warning|A message warning of some kind problem"), MessageBoxButtonsExtended.OK,
                MessageBoxIcon.Warning, ExtendedDefaultButtons.Button1);

            EnableDisableControlsOnChange(true);

            return;
        }

        ScriptNotepadDbContext.DbContext.CodeSnippets.Add(currentCodeSnippet);
        ScriptNotepadDbContext.DbContext.SaveChanges();

        // enable the controls as the user chose to save the changes of the script..
        EnableDisableControlsOnChange(true);
    }

    // a user wants to compile the script to see if it's valid..
    private void tsbTestScript_Click(object sender, EventArgs e)
    {
        // ..so do compile and output the results..
        Compile();
    }
    #endregion

    #region PublicEvents
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
    public class ScintillaRequiredEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the Scintilla document which contents to manipulate via a C# script.
        /// </summary>
        public Scintilla Scintilla { get; set; } = null;
    }

    /// <summary>
    /// Occurs when a user wants to run a script for a Scintilla document contents.
    /// </summary>
    public event OnScintillaRequired ScintillaRequired;
    #endregion

    #region "CodeIndent Handlers"
    // This (C): https://gist.github.com/Ahmad45123/f2910192987a73a52ab4
    // ReSharper disable once InconsistentNaming
    private const int SCI_SETLINEINDENTATION = 2126;
    // ReSharper disable once InconsistentNaming
    private const int SCI_GETLINEINDENTATION = 2127;
    void SetIndent(Scintilla scintilla, int line, int indent)
    {
        scintilla.DirectMessage(SCI_SETLINEINDENTATION, new IntPtr(line), new IntPtr(indent));
    }
    int GetIndent(Scintilla scintilla, int line)
    {
        return (scintilla.DirectMessage(SCI_GETLINEINDENTATION, new IntPtr(line), IntPtr.Zero).ToInt32());
    }

    private void Scintilla_CharAdded(object sender, CharAddedEventArgs e)
    {
        var scintilla = (Scintilla) sender;

        //The '}' char.
        if (e.Char == '}') {
            int curLine = scintilla.LineFromPosition(scintilla.CurrentPosition);
		
            if (scintilla.Lines[curLine].Text.Trim() == "}") { //Check whether the bracket is the only thing on the line.. For cases like "if() { }".
                SetIndent(scintilla, curLine, GetIndent(scintilla, curLine) - FormSettings.Settings.EditorTabWidth);
            }
        }
    }


    private void Scintilla_InsertCheck(object sender, InsertCheckEventArgs e)
    {
        // This (C): https://gist.github.com/Ahmad45123/f2910192987a73a52ab4
        var scintilla = (Scintilla) sender;

        if ((e.Text.EndsWith("\r") || e.Text.EndsWith("\n"))) {
            int startPos = scintilla.Lines[scintilla.LineFromPosition(scintilla.CurrentPosition)].Position;
            int endPos = e.Position;
            string curLineText = scintilla.GetTextRange(startPos, (endPos - startPos)); //Text until the caret so that the whitespace is always equal in every line.
		
            Match indent = Regex.Match(curLineText, "^[ \\t]*");
            e.Text = (e.Text + indent.Value);
            if (Regex.IsMatch(curLineText, "{\\s*$")) {
                e.Text = (e.Text + "\t");
            }
        }
    }
    #endregion

    #region BraceHighLight
    // (C): https://github.com/jacobslusser/ScintillaNET/wiki/Brace-Matching
    private static bool IsBrace(int c)
    {
        switch (c)
        {
            case '(':
            case ')':
            case '[':
            case ']':
            case '{':
            case '}':
            case '<':
            case '>':
                return true;
        }

        return false;
    }

    /// <summary>
    /// Sets the brace highlight colors for a <see cref="Scintilla"/> instance.
    /// </summary>
    private void SetBraceHighlights()
    {
        // not in the settings, so do return..
        if (!FormSettings.Settings.HighlightBraces)
        {
            return;
        }

        scintillaScript.Styles[Style.BraceLight].ForeColor = FormSettings.Settings.BraceHighlightForegroundColor;
        scintillaScript.Styles[Style.BraceLight].BackColor = FormSettings.Settings.BraceHighlightBackgroundColor;
        scintillaScript.Styles[Style.BraceBad].BackColor = FormSettings.Settings.BraceBadHighlightForegroundColor;

        scintillaScript.Styles[Style.BraceLight].Italic = FormSettings.Settings.HighlightBracesItalic;
        scintillaScript.Styles[Style.BraceLight].Bold = FormSettings.Settings.HighlightBracesBold;
    }

    private int lastCaretPos = -1;

    private void Scintilla_UpdateUI(object sender, UpdateUIEventArgs e)
    {
        // (C): https://github.com/jacobslusser/ScintillaNET/wiki/Brace-Matching
        // Has the caret changed position?
        var caretPos = scintillaScript.CurrentPosition;
        if (lastCaretPos != caretPos)
        {
            lastCaretPos = caretPos;
            var bracePos1 = -1;

            // Is there a brace to the left or right?
            if (caretPos > 0 && IsBrace(scintillaScript.GetCharAt(caretPos - 1)))
            {
                bracePos1 = (caretPos - 1);
            }
            else if (IsBrace(scintillaScript.GetCharAt(caretPos)))
            {
                bracePos1 = caretPos;
            }

            if (bracePos1 >= 0)
            {
                // Find the matching brace
                var bracePos2 = scintillaScript.BraceMatch(bracePos1);
                if (bracePos2 == Scintilla.InvalidPosition)
                {
                    scintillaScript.BraceBadLight(bracePos1);
                }
                else
                {
                    scintillaScript.BraceHighlight(bracePos1, bracePos2);
                }
            }
            else
            {
                // Turn off brace matching
                scintillaScript.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
            }
        }
    }
    #endregion
}