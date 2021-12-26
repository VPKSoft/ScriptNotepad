using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Database.Entity.Enumerations;
using ScriptNotepad.Localization;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.CodeDom;
using ScriptNotepad.UtilityClasses.TextManipulation;
using ScriptNotepad.UtilityClasses.TextManipulation.Interfaces;
using ScriptNotepad.UtilityClasses.TextManipulation.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptNotepad.UtilityClasses.ScintillaUtils;
using ScriptNotepad.UtilityClasses.TextManipulation.base64;
using ScriptNotepad.UtilityClasses.TextManipulation.Xml;
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
        FormMain FormMain => FormMain.Instance;
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

        private void cmbCommands_SelectedValueChanged(object sender, EventArgs e)
        {
            IndicateError(false);
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

            items.Add(new XmlMultilineConvert
            {
                MethodName = Translation.GetMessage("msgUtilTextPrettifyXML",
                    "Prettify XML|A message indicating a function to format XML as human-readable.")
            });

            items.Add(new XmlSingleLineConvert
            {
                MethodName = Translation.GetMessage("msgUtilTextXMLToOneLine",
                    "XML to one line|A message indicating a function to human-readable XML to one line.")
            });

            items.Add(new StringToBase64
            {
                MethodName = Translation.GetMessage("msgUtilTextBase64ToString",
                    "Selected base64 to string|A message indicating a function to convert selected base64 data to string.")
            });

            items.Add(new Base64ToString
            {
                MethodName = Translation.GetMessage("msgUtilStringToBase64",
                    "Selected string to base64|A message indicating a function to convert human-readable text into a base64 encoded data string.")
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

        private void IndicateError(bool error)
        {
            if (cmbCommands.InvokeRequired)
            {
                cmbCommands.Invoke(new MethodInvoker(() =>
                {
                    cmbCommands.Font =
                        error
                            ? new Font(cmbCommands.Font, FontStyle.Bold)
                            : new Font(cmbCommands.Font, FontStyle.Regular);
                    cmbCommands.ForeColor = error ? Color.Red : Color.Black;
                }));
            }
            else
            {
                cmbCommands.Font =
                    error
                        ? new Font(cmbCommands.Font, FontStyle.Bold)
                        : new Font(cmbCommands.Font, FontStyle.Regular);
                cmbCommands.ForeColor = error ? Color.Red : Color.Black;
            }
        }

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

                        IndicateError(!result.Value);
                    }

                    if (codeSnippet.ScriptTextManipulationType == ScriptSnippetType.Lines)
                    {
                        var result = await CsScriptRunnerText.RunScriptLines(codeSnippet.ScriptContents,
                            FormMain.ActiveScintilla.Lines.Select(f => f.Text).ToList());

                        if (result.Value)
                        {
                            FormMain.ActiveScintilla.Text = result.Key;
                        }
                        IndicateError(!result.Value);
                    }
                }

                if (cmbCommands.SelectedItem is ITextManipulationCommand command)
                {
                    if (command.PreferSelectedText)
                    {
                        FormMain.ActiveScintilla.SelectionReplaceWithValue(
                            command.Manipulate(FormMain.ActiveScintilla.SelectedText));
                    }
                    else
                    {
                        FormMain.ActiveScintilla.Text = command.Manipulate(FormMain.ActiveScintilla.Text);
                    }
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
