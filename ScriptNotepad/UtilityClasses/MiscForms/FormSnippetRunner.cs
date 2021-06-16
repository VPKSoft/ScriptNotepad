using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Database.Entity.Enumerations;
using ScriptNotepad.Localization;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.CodeDom;
using ScriptNotepad.UtilityClasses.TextManipulation;
using ScriptNotepad.UtilityClasses.TextManipulation.Interfaces;
using ScriptNotepad.UtilityClasses.TextManipulation.Json;
using VPKSoft.ErrorLogger;
using VPKSoft.LangLib;

namespace ScriptNotepad.UtilityClasses.MiscForms
{
    /// <summary>
    /// A floating form to run code snippets or internal text manipulation methods.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormSnippetRunner : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormSnippetRunner"/> class.
        /// </summary>
        private FormSnippetRunner()
        {
            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            DBLangEngine.NameSpaces.Add("CustomControls");

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages");

            // subscribe the disposed event..
            Disposed += formSnippetRunner_Disposed;
            FormSettings.Settings.ShowRunSnippetToolbar = true;
        }

        internal static List<ITextManipulationCallback> Callbacks { get; } = new();

        private static FormSnippetRunner _instance;

        #region InternalProperties        
        /// <summary>
        /// Gets or sets the value indicating whether the search combo box is focused.
        /// </summary>
        /// <value>The value indicating whether the search combo box is focused.</value>
        internal bool SearchFocused
        {
            get => cmbCommands.Focused;
            set
            {
                if (value)
                {
                    cmbCommands.Focus();
                }
            }
        }

        /// <summary>
        /// Gets a singleton instance of this form.
        /// </summary>
        internal static FormSnippetRunner Instance
        {
            get
            {
                return _instance ??= new FormSnippetRunner();
            }
        }
        #endregion

        #region PrivateProperties                
        /// <summary>
        /// Gets or sets the <see cref="StaticMessageLocalizationProvider"/> instance.
        /// </summary>
        /// <value>The <see cref="StaticMessageLocalizationProvider"/> instance.</value>
        private static StaticMessageLocalizationProvider Translation { get; } = StaticMessageLocalizationProvider.Instance;

        /// <summary>
        /// Gets the main form instance of the application.
        /// </summary>
        /// <value>The main form instance of the application.</value>
        FormMain FormMain
        {
            get
            {
                try
                {
                    if (Application.OpenForms.Count > 0)
                    {
                        return (FormMain) Application.OpenForms[0];
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogError(ex);
                }

                return null;
            }
        }
        #endregion

        #region InternalEvents
        private async void FormSnippetRunner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.Return)
            {
                await RunCommand();
                e.SuppressKeyPress = true;
            }
        }

        private async void ibRunCommand_Click(object sender, EventArgs e)
        {
            await RunCommand();
        }

        private void formSnippetRunner_Disposed(object sender, EventArgs e)
        {
            // unsubscribe the disposed event..
            Disposed -= formSnippetRunner_Disposed;
            _instance = null;
        }

        private void pnClose_Click(object sender, EventArgs e)
        {
            FormSettings.Settings.ShowRunSnippetToolbar = false;
            Close();
        }

        private void FormSnippetRunner_Shown(object sender, EventArgs e)
        {
            cmbCommands.Items.Clear();

            var items = new List<object>();
            items.AddRange(ScriptNotepadDbContext.DbContext.CodeSnippets.Cast<object>().ToArray());

            items.Add(new JsonMultilineConvert
            {
                MethodName = Translation.GetMessage("msgUtilTextPrettifyJson",
                    "Prettify Json|A message indicating a function to format Json as human-readable.")
            });

            items.Add(new JsonSingleLineConvert
            {
                MethodName = Translation.GetMessage("msgUtilTextJsonToOneLine",
                    "Json to one line|A message indicating a function to human-readable Json to one line.")
            });

            items.Add(new DuplicateLines
            {
                MethodName = Translation.GetMessage("msgUtilTextRemoveDuplicateLines",
                    "Remove duplicate lines|A message indicating a function to remove duplicate lines from a file.")
            });

            foreach (var callback in Callbacks)
            {
                items.Add(callback);
            }

            items = items.OrderBy(f => f.ToString()).ToList();

            cmbCommands.Items.AddRange(items.ToArray());
        }
        #endregion

        #region PrivateMethods
        private async Task RunCommand()
        {
            if (cmbCommands.SelectedItem != null && FormMain.ActiveScintilla != null)
            {
                if (cmbCommands.SelectedItem is CodeSnippet codeSnippet)
                {
                    if (codeSnippet.ScriptTextManipulationType == ScriptSnippetType.Text)
                    {
                        var result = await CsScriptRunnerText.RunScriptText(codeSnippet.ScriptContents,
                            FormMain.ActiveScintilla.Text);

                        if (result.Value)
                        {
                            FormMain.ActiveScintilla.Text = result.Key;
                        }
                    }

                    if (codeSnippet.ScriptTextManipulationType == ScriptSnippetType.Lines)
                    {
                        var result = await CsScriptRunnerText.RunScriptLines(codeSnippet.ScriptContents,
                            FormMain.ActiveScintilla.Lines.Select(f => f.Text).ToList());

                        if (result.Value)
                        {
                            FormMain.ActiveScintilla.Text = result.Key;
                        }
                    }
                }

                if (cmbCommands.SelectedItem is ITextManipulationCommand command)
                {
                    FormMain.ActiveScintilla.Text = command.Manipulate(FormMain.ActiveScintilla.Text);
                }

                if (cmbCommands.SelectedItem is ITextManipulationCallback callback)
                {
                    callback.CallbackAction?.Invoke();
                }
            }
        }
        #endregion
    }
}
