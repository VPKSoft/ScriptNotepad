using System;
using System.Linq;
using System.Windows.Forms;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Database.Entity.Enumerations;
using ScriptNotepad.Localization;
using ScriptNotepad.Settings;
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
        public FormSnippetRunner()
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

            AllowTransparency = true;

            // subscribe to an event which is raised upon application activation..
            ApplicationDeactivated += FormSearchAndReplace_ApplicationDeactivated;
            ApplicationActivated += FormSearchAndReplace_ApplicationActivated;

            // subscribe the disposed event..
            Disposed += formSnippetRunner_Disposed;

            pnRunSnippet.BorderStyle = BorderStyle.Fixed3D;
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
        private static StaticMessageLocalizationProvider Translation { get; set; } = StaticMessageLocalizationProvider.Instance;

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
        // occurs when the application is activated..
        private void FormSearchAndReplace_ApplicationDeactivated(object sender, EventArgs e)
        {
            if (Visible)
            {
                TopMost = false;
            }
        }

        private void FormSearchAndReplace_ApplicationActivated(object sender, EventArgs e)
        {
            if (Visible)
            {
                TopMost = true;
            }
        }

        // run a script or an internal command (i.e. menu item code)..
        private async void lbRunSnippet_Click(object sender, EventArgs e)
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

        // the form is activated, toggle the transparency of the form..
        private void FormSnippetRunner_Activated(object sender, EventArgs e)
        {
            TopMost = true;
            TransparencyToggle();
        }

        private void formSnippetRunner_Disposed(object sender, EventArgs e)
        {
            // unsubscribe from an event which is raised upon application activation..
            ApplicationDeactivated -= FormSearchAndReplace_ApplicationDeactivated;
            ApplicationActivated -= FormSearchAndReplace_ApplicationActivated;

            // unsubscribe the disposed event..
            Disposed -= formSnippetRunner_Disposed;
        }

        private void FormSnippetRunner_Shown(object sender, EventArgs e)
        {
            Top = FormMain.Bottom - Height - 50;
            Left = FormMain.Right - Width - 30;

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
        }
        #endregion

        #region PrivateMethods
        /// <summary>
        /// Toggles the transparency based on the saved transparency settings of the form.
        /// </summary>
        private void TransparencyToggle()
        {
            var activated = ActiveForm != null && ActiveForm.Equals(this);
            Opacity = activated ? 1 : FormSettings.Settings.SearchBoxOpacity;

            // transparency is disabled..
            if (FormSettings.Settings.SearchBoxTransparency == 0)
            {
                // set the opacity value to 100%..
                Opacity = 1;
            }
            // transparency is enabled while active..
            else if (FormSettings.Settings.SearchBoxTransparency == 1)
            {
                // set the opacity value to based on the active property..
                Opacity = activated ? 1 : FormSettings.Settings.SearchBoxOpacity;
            }
            else if (FormSettings.Settings.SearchBoxTransparency == 2)
            {
                Opacity = FormSettings.Settings.SearchBoxOpacity;
            }
        }
        #endregion
    }
}
