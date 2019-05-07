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

using Ookii.Dialogs.WinForms;
using ScriptNotepad.UtilityClasses.Encoding.CharacterSets;
using ScriptNotepad.UtilityClasses.ExternalProcessInteraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ScintillaNET;
using VPKSoft.ErrorLogger;
using VPKSoft.LangLib;
using TabDrawMode = ScintillaNET.TabDrawMode;

namespace ScriptNotepad.Settings
{
    /// <summary>
    /// A settings/preferences dialog for the application.
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormSettings : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormSettings"/> class.
        /// </summary>
        public FormSettings()
        {
            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages");

            // create a new instance of the CharacterSetComboBuilder class..
            CharacterSetComboBuilder = new CharacterSetComboBuilder(cmbCharacterSet, cmbEncoding, false, "encoding");

            // subscribe the encoding selected event..
            CharacterSetComboBuilder.EncodingSelected += CharacterSetComboBuilder_EncodingSelected;

            // translate the tool tips..
            ttMain.SetToolTip(btUTF8,
                DBLangEngine.GetMessage("msgUTF8Encoding", "Set to Unicode (UTF8)|Set the selected encoding to UTF8 via a button click"));

            // translate the tool tips..
            ttMain.SetToolTip(btSystemDefaultEncoding,
                DBLangEngine.GetMessage("msgSysDefaultEncoding", "Set to system default|Set the selected encoding to system's default encoding via a button click"));

            // translate the tool tips..
            ttMain.SetToolTip(pbDefaultFolder,
                DBLangEngine.GetMessage("msgSetToDefault", "Set to default|A some value is set to default value"));

            // list the translated cultures..
            List<CultureInfo> cultures = DBLangEngine.GetLocalizedCultures();

            // a the translated cultures to the selection combo box..
            // ReSharper disable once CoVariantArrayConversion
            cmbSelectLanguageValue.Items.AddRange(cultures.ToArray());
        }

        /// <summary>
        /// Displays the FormSettings dialog and saves the settings if the user selected OK.
        /// </summary>
        /// <returns>True if the user selected to save the settings; otherwise false.</returns>
        public static bool Execute()
        {
            FormSettings formSettings = new FormSettings();
            if (formSettings.ShowDialog() == DialogResult.OK)
            {
                formSettings.SaveSettings();
                return true;
            }
            return false;
        }

        #region SaveLoad
        /// <summary>
        /// Loads the settings visualized to form.
        /// </summary>
        private void LoadSettings()
        {
            // select the encoding from the settings..
            CharacterSetComboBuilder.SelectItemByEncoding(Settings.DefaultEncoding, false);

            // set the amount of document file names to keep in history..
            nudHistoryDocuments.Value = Settings.HistoryListAmount;

            // get the amount of documents contents to be kept after a document has been closed..
            nudDocumentContentHistory.Value = Settings.SaveFileHistoryContentsCount;

            // set the flag whether to save closed document contents..
            cbDocumentContentHistory.Checked = Settings.SaveFileHistoryContents;

            // set the current culture from the settings..
            cmbSelectLanguageValue.SelectedItem = Settings.Culture;

            // set the current plug-in folder from the settings..
            tbPluginFolder.Text = Settings.PluginFolder;

            // set the value of whether to dock the search tree form in the main form..
            cbDockSearchTree.Checked = Settings.DockSearchTreeForm;

            // get the color values..
            btSmartHighlightColor.BackColor = Settings.SmartHighlight;
            btMarkStyle1Color.BackColor = Settings.Mark1Color;
            btMarkStyle2Color.BackColor = Settings.Mark2Color;
            btMarkStyle3Color.BackColor = Settings.Mark3Color;
            btMarkStyle4Color.BackColor = Settings.Mark4Color;
            btMarkStyle5Color.BackColor = Settings.Mark5Color;
            btCurrentLineBackgroundColor.BackColor = Settings.CurrentLineBackground;
            // END: get the color values..

            // get the value of file's maximum size in megabytes (MB) to include in the file search..
            nudMaximumSearchFileSize.Value = Settings.FileSearchMaxSizeMb;

            // get the amount of how much search history (search text, replace text, directories, paths, etc.) to keep..
            nudHistoryAmount.Value = Settings.FileSearchHistoriesLimit;

            // get the size of the white space dot..
            nudWhiteSpaceSize.Value = Settings.EditorWhiteSpaceSize;

            // get the value whether to use tabs in the editor (tabulator characters)..
            cbUseTabs.Checked = Settings.EditorUseTabs;

            // gets the type of the tab character symbol..
            rbTabSymbolStrikeout.Checked = Settings.EditorTabSymbol != 0;
            rbTabSymbolArrow.Checked = Settings.EditorTabSymbol == 0;

            // get the indent guide value..
            cbIndentGuideOn.Checked = Settings.EditorIndentGuideOn;

            // get the spell checking properties..
            if (File.Exists(Settings.EditorHunspellDictionaryFile) && File.Exists(Settings.EditorHunspellAffixFile) &&
                Settings.EditorUseSpellChecking)
            {
                cbSpellCheckInUse.Checked = true;
            }

            btSpellCheckMarkColor.BackColor = Settings.EditorSpellCheckColor;

            tbHunspellAffixFile.Text = Settings.EditorHunspellAffixFile;
            tbHunspellDictionary.Text = Settings.EditorHunspellDictionaryFile;
            nudEditorSpellRecheckInactivity.Value = (decimal)Settings.EditorSpellCheckInactivity;

        }

        /// <summary>
        /// Saves the settings visualized on the form.
        /// </summary>
        private void SaveSettings()
        {
            // save the default encoding..
            Settings.DefaultEncoding = SelectedEncoding;

            // save the amount of history documents to keep..
            Settings.HistoryListAmount = (int)nudHistoryDocuments.Value;

            // save the amount of documents contents to be kept after a document has been closed..
            Settings.SaveFileHistoryContentsCount = (int)nudDocumentContentHistory.Value;

            // save the flag whether to save closed document contents..
            Settings.SaveFileHistoryContents = cbDocumentContentHistory.Checked;

            // save the selected culture for localization..
            Settings.Culture = (CultureInfo)cmbSelectLanguageValue.SelectedItem;

            // save the selected plug-in folder to the settings..
            Settings.PluginFolder = tbPluginFolder.Text;

            // save the value of whether to dock the search tree form in the main form..
            Settings.DockSearchTreeForm = cbDockSearchTree.Checked;

            // save the color values..
            Settings.SmartHighlight = btSmartHighlightColor.BackColor;
            Settings.Mark1Color = btMarkStyle1Color.BackColor;
            Settings.Mark2Color = btMarkStyle2Color.BackColor;
            Settings.Mark3Color = btMarkStyle3Color.BackColor;
            Settings.Mark4Color = btMarkStyle4Color.BackColor;
            Settings.Mark5Color = btMarkStyle5Color.BackColor;
            Settings.CurrentLineBackground = btCurrentLineBackgroundColor.BackColor;
            // END: save the color values..

            // set the value of file's maximum size in megabytes (MB) to include in the file search..
            Settings.FileSearchMaxSizeMb = (long)nudMaximumSearchFileSize.Value;

            // set the amount of how much search history (search text, replace text, directories, paths, etc.) to keep..
            Settings.FileSearchHistoriesLimit = (int)nudHistoryAmount.Value;

            // set the size of the white space dot..
            Settings.EditorWhiteSpaceSize = (int)nudWhiteSpaceSize.Value;

            // set the value whether to use tabs in the editor (tabulator characters)..
            Settings.EditorUseTabs = cbUseTabs.Checked;

            // set the type of the tab character symbol..
            if (rbTabSymbolArrow.Checked)
            {
                Settings.EditorTabSymbol = (int) TabDrawMode.LongArrow;
            }
            else
            {
                Settings.EditorTabSymbol = (int) TabDrawMode.Strikeout;
            }

            // set the indent guide value..
            Settings.EditorIndentGuideOn = cbIndentGuideOn.Checked;

            // set the spell checking properties..
            Settings.EditorUseSpellChecking = cbSpellCheckInUse.Checked;
            Settings.EditorSpellCheckColor = btSpellCheckMarkColor.BackColor;
            Settings.EditorHunspellAffixFile = tbHunspellAffixFile.Text;
            Settings.EditorHunspellDictionaryFile = tbHunspellDictionary.Text;
            Settings.EditorSpellCheckInactivity = (int)nudEditorSpellRecheckInactivity.Value;

        }
        #endregion

        /// <summary>
        /// Gets or sets the character set combo box builder.
        /// </summary>
        private CharacterSetComboBuilder CharacterSetComboBuilder { get; set; }

        // this event is fired when the encoding is changed from the corresponding combo box..
        private void CharacterSetComboBuilder_EncodingSelected(object sender, OnEncodingSelectedEventArgs e)
        {
            // save the changed value..
            SelectedEncoding = e.Encoding;
        }

        /// <summary>
        /// Gets or sets the settings class instance containing the settings for the software.
        /// </summary>
        public static Settings Settings { get; set; }

        /// <summary>
        /// Sets the editor settings for a given <see cref="Scintilla"/>.
        /// </summary>
        /// <param name="scintilla">The scintilla which settings to set.</param>
        public static void SetEditorSettings(Scintilla scintilla)
        {
            // set the size of the white space dot..
            scintilla.WhitespaceSize = Settings.EditorWhiteSpaceSize;

            // set the value whether to use tabs in the editor (tabulator characters)..
            scintilla.UseTabs = Settings.EditorUseTabs;

            // set the type of the tab character symbol..
            scintilla.TabDrawMode = (TabDrawMode) Settings.EditorTabSymbol;

            // set the value whether to show the indent guides..
            scintilla.IndentationGuides = Settings.EditorIndentGuideOn ? IndentView.Real : IndentView.None;
        }

        /// <summary>
        /// Gets or sets the encoding a user selected from the dialog.
        /// </summary>
        private Encoding SelectedEncoding { get; set; }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (CharacterSetComboBuilder)
            {
                // unsubscribe the encoding selected event..
                CharacterSetComboBuilder.EncodingSelected -= CharacterSetComboBuilder_EncodingSelected;
            }
        }

        private void FormSettings_Shown(object sender, EventArgs e)
        {
            // load the settings visualized on the form..
            LoadSettings();
        }

        private void btDefaultEncodings_Click(object sender, EventArgs e)
        {            
            // select the encoding based on which button the user clicked..
            CharacterSetComboBuilder.SelectItemByEncoding(sender.Equals(btUTF8) ? Encoding.UTF8 : Encoding.Default, false);
        }

        private void cbDocumentContentHistory_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            nudDocumentContentHistory.Enabled = checkBox.Checked;
        }

        /// <summary>
        /// Creates the default plug-in directory for the software.
        /// </summary>
        public static string CreateDefaultPluginDirectory()
        {
            // create a folder for plug-ins if it doesn't exist already.. 
            if (!Directory.Exists(Path.Combine(VPKSoft.Utils.Paths.GetAppSettingsFolder(), "Plugins")))
            {
                try
                {
                    // create the folder..
                    Directory.CreateDirectory(Path.Combine(VPKSoft.Utils.Paths.GetAppSettingsFolder(), "Plugins"));

                    // save the folder in the settings..
                    Settings.PluginFolder = Path.Combine(VPKSoft.Utils.Paths.GetAppSettingsFolder(), "Plugins");
                }
                catch (Exception ex) // a failure so do log it..
                {
                    ExceptionLogger.LogError(ex);
                    return string.Empty;
                }
            }
            return Path.Combine(VPKSoft.Utils.Paths.GetAppSettingsFolder(), "Plugins");
        }

        private void btSelectPluginFolder_Click(object sender, EventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();

            if (Directory.Exists(tbPluginFolder.Text))
            {
                dialog.SelectedPath = tbPluginFolder.Text;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbPluginFolder.Text = dialog.SelectedPath;
            }
        }

        private void pbDefaultFolder_Click(object sender, EventArgs e)
        {
            tbPluginFolder.Text = CreateDefaultPluginDirectory();
        }

        private void tbPluginFolder_TextChanged(object sender, EventArgs e)
        {
            btOK.Enabled = Directory.Exists(tbPluginFolder.Text);
        }

        private void lbPluginFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(tbPluginFolder.Text))
            {
                WindowsExplorerInteraction.OpenFolderInExplorer(tbPluginFolder.Text);
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            if (cdColors.ShowDialog() == DialogResult.OK)
            {
                Button button = (Button) sender;
                button.BackColor = cdColors.Color;
            }
        }

        private void BtDefaults_Click(object sender, EventArgs e)
        {
            btSmartHighlightColor.BackColor = Color.FromArgb(0, 255, 0);
            btMarkStyle1Color.BackColor = Color.FromArgb(0, 255, 255);
            btMarkStyle2Color.BackColor = Color.FromArgb(255, 128, 0);
            btMarkStyle3Color.BackColor = Color.FromArgb(255, 255, 0);
            btMarkStyle4Color.BackColor = Color.FromArgb(128, 0, 255);
            btMarkStyle5Color.BackColor = Color.FromArgb(0, 128, 0);
            btCurrentLineBackgroundColor.BackColor = Color.FromArgb(232, 232, 255);
        }

        private void BtHunspellDictionary_Click(object sender, EventArgs e)
        {
            if (odDictionaryFile.ShowDialog() == DialogResult.OK)
            {
                tbHunspellDictionary.Text = odDictionaryFile.FileName;
            }
        }

        private void BtHunspellAffixFile_Click(object sender, EventArgs e)
        {
            if (odAffixFile.ShowDialog() == DialogResult.OK)
            {
                tbHunspellAffixFile.Text = odAffixFile.FileName;
            }
        }

        private void CbSpellCheckInUse_Click(object sender, EventArgs e)
        {
            if (!File.Exists(tbHunspellDictionary.Text) || !File.Exists(tbHunspellAffixFile.Text))
            {
                cbSpellCheckInUse.Checked = false;
            }
        }
    }
}
