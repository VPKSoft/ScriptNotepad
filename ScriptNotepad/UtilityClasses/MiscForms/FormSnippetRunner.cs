using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Database.Entity.Enumerations;
using ScriptNotepad.Localization;
using ScriptNotepad.UtilityClasses.CodeDom;
using ScriptNotepad.UtilityClasses.TextManipulation;
using ScriptNotepad.UtilityClasses.TextManipulation.Json;
using VPKSoft.ErrorLogger;
using VPKSoft.LangLib;
using static ScriptNotepad.UtilityClasses.ApplicationHelpers.ApplicationActivateDeactivate;

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

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages");

            // subscribe the disposed event..
            Disposed += formSnippetRunner_Disposed;
        }

        private static FormSnippetRunner _instance;

        /// <summary>
        /// Gets a singleton instance of this form.
        /// </summary>
        public static FormSnippetRunner Instance
        {
            get
            {
                return _instance ??= new FormSnippetRunner();
            }
        }

        #region WndProc
        /// <summary>Processes Windows messages.</summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        protected override void WndProc(ref Message m)
        {
            WndProcApplicationActivateHelper(this, ref m);

            base.WndProc(ref m);
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
            Close();
        }

        private void FormSnippetRunner_Shown(object sender, EventArgs e)
        {
            cmbCommands.Items.Clear();
            cmbCommands.Items.AddRange(ScriptNotepadDbContext.DbContext.CodeSnippets.Cast<object>().ToArray());
            cmbCommands.Items.Add(new JsonMultilineConvert
            {
                MethodName = Translation.GetMessage("msgUtilTextPrettifyJson",
                    "Prettify Json|A message indicating a function to format Json as human-readable.")
            });

            cmbCommands.Items.Add(new JsonSingleLineConvert
            {
                MethodName = Translation.GetMessage("msgUtilTextJsonToOneLine",
                    "Json to one line|A message indicating a function to human-readable Json to one line.")
            });

            cmbCommands.Items.Add(new DuplicateLines
            {
                MethodName = Translation.GetMessage("msgUtilTextRemoveDuplicateLines",
                    "Remove duplicate lines|A message indicating a function to remove duplicate lines from a file.")
            });
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
            }
        }
        #endregion
    }
}
