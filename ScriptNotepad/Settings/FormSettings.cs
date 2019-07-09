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
using ScriptNotepad.UtilityClasses.ExternalProcessInteraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNET;
using ScriptNotepad.Localization;
using ScriptNotepad.Localization.Hunspell;
using ScriptNotepad.Settings.XmlNotepadPlusMarks;
using ScriptNotepad.UtilityClasses.Encodings.CharacterSets;
using ScriptNotepad.UtilityClasses.GraphicUtils;
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

            foreach (var fontFamily in FontFamily.Families)
            {
                // validate that the font is fixed of width type..
                if (fontFamily.IsFixedWidth())
                {
                    cmbFont.Items.Add(new FontFamilyHolder {FontFamily = fontFamily});
                }
            }

            // on my keyboard the AltGr+(2|3|4) keys somehow aren't registered by the active Scintilla tab,
            // so make a way to bypass this weirdness..
            cmbSimulateKeyboard.Items.Add(DBLangEngine.GetMessage("msgAltGrFinnish",
                "Finnish AltGr simulation (@, £, $)|A message describing that the AltGr and some key would simulate a keypress for an active Scintilla control."));

            // localize the dialog filters..
            StaticLocalizeFileDialog.InitOpenHunspellDictionaryDialog(odDictionaryFile);
            StaticLocalizeFileDialog.InitOpenHunspellAffixFileDialog(odAffixFile);
        }

        private class FontFamilyHolder
        {
            public static FontFamilyHolder FromFontFamilyName(string fontFamilyName)
            {
                List<FontFamily> families = new List<FontFamily>(FontFamily.Families);
                var family = families.Find(f => f.Name == fontFamilyName);
                return new FontFamilyHolder {FontFamily = family};
            }

            public FontFamily FontFamily { get; set; }

            public override string ToString()
            {
                return FontFamily.Name;
            }
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

            // get the value whether to use auto-detection on a file encoding on opening a new file..
            cbEncodingAutoDetect.Checked = Settings.AutoDetectEncoding;

            // get the amount of document file names to keep in history..
            nudHistoryDocuments.Value = Settings.HistoryListAmount;

            // get the amount of documents contents to be kept after a document has been closed..
            nudDocumentContentHistory.Value = Settings.SaveFileHistoryContentsCount;

            // get the flag whether to save closed document contents..
            cbDocumentContentHistory.Checked = Settings.SaveFileHistoryContents;

            // get the current culture from the settings..
            cmbSelectLanguageValue.SelectedItem = Settings.Culture;

            // get the current plug-in folder from the settings..
            tbPluginFolder.Text = Settings.PluginFolder;

            // get the value of whether to dock the search tree form in the main form..
            cbDockSearchTree.Checked = Settings.DockSearchTreeForm;

            // get the value whether to categorize the programming language menu with the language name starting character..
            cbCategorizeProgrammingLanguages.Checked = Settings.CategorizeStartCharacterProgrammingLanguage;

            // get the color values..
            btSmartHighlightColor.BackColor = Settings.SmartHighlight;
            btMarkStyle1Color.BackColor = Settings.Mark1Color;
            btMarkStyle2Color.BackColor = Settings.Mark2Color;
            btMarkStyle3Color.BackColor = Settings.Mark3Color;
            btMarkStyle4Color.BackColor = Settings.Mark4Color;
            btMarkStyle5Color.BackColor = Settings.Mark5Color;
            btCurrentLineBackgroundColor.BackColor = Settings.CurrentLineBackground;

            btBraceHighlightForegroundColor.BackColor = Settings.BraceHighlightForegroundColor;
            btBraceHighlightBackgroundColor.BackColor = Settings.BraceHighlightBackgroundColor;
            btBadBraceColor.BackColor = Settings.BraceBadHighlightForegroundColor;
            cbUseBraceMatching.Checked = Settings.HighlightBraces;
            rbBraceStyleItalic.Checked = Settings.HighlightBracesItalic;
            rbBraceStyleBold.Checked = Settings.HighlightBracesBold;
            // END: get the color values..

            // get the value of file's maximum size in megabytes (MB) to include in the file search..
            nudMaximumSearchFileSize.Value = Settings.FileSearchMaxSizeMb;

            // get the amount of how much search history (search text, replace text, directories, paths, etc.) to keep..
            nudHistoryAmount.Value = Settings.FileSearchHistoriesLimit;

            // get the size of the white space dot..
            nudWhiteSpaceSize.Value = Settings.EditorWhiteSpaceSize;

            // get the value whether to use tabs in the editor (tabulator characters)..
            cbUseTabs.Checked = Settings.EditorUseTabs;

            // get the value whether to use individual zoom value for all
            // the open documents..
            cbIndividualZoom.Checked = Settings.EditorIndividualZoom;

            // get the value whether the zoom value of the document(s)
            // should be saved into the database..
            cbSaveDocumentZoom.Checked = Settings.EditorSaveZoom;

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

            tbDictionaryPath.Text = Settings.EditorHunspellDictionaryPath;
            btSpellCheckMarkColor.BackColor = Settings.EditorSpellCheckColor;

            tbHunspellAffixFile.Text = Settings.EditorHunspellAffixFile;
            tbHunspellDictionary.Text = Settings.EditorHunspellDictionaryFile;
            nudEditorSpellRecheckInactivity.Value = Settings.EditorSpellCheckInactivity;

            // get the editor font settings..
            nudFontSize.Value = Settings.EditorFontSize;

            var item = FontFamilyHolder.FromFontFamilyName(Settings.EditorFontName);

            for (int i = 0; i < cmbFont.Items.Count; i++)
            {
                if (Equals(((FontFamilyHolder)cmbFont.Items[i]).FontFamily, item.FontFamily))
                {
                    cmbFont.SelectedIndex = i;
                    break;
                }
            }

            nudFontSize.Value = Settings.EditorFontSize;

            // get the Notepad++ them path..
            tbNotepadPlusPlusThemePath.Text = Settings.NotepadPlusPlusThemePath;

            // list the Notepad++ then files if any..
            ListNotepadPlusPLusThemes();

            // get the Notepad++ theme settings..
            cbUseNotepadPlusPlusTheme.Checked = Settings.UseNotepadPlusPlusTheme;

            if (Settings.NotepadPlusPlusThemeFile != null)
            {
                cmbNotepadPlusPlusTheme.SelectedIndex = cmbNotepadPlusPlusTheme.Items.IndexOf(
                    Path.GetFileNameWithoutExtension(Settings.NotepadPlusPlusThemeFile));
            }

            // get the AltGr capture bypass method if set..
            if (Settings.SimulateAltGrKeyIndex != -1)
            {
                cbSimulateKeyboard.Checked = true;
                cmbSimulateKeyboard.SelectedIndex = Settings.SimulateAltGrKeyIndex;
            }

            // get the code indentation and editor tab width values..
            cbUseCodeIndentation.Checked = Settings.EditorUseCodeIndentation;
            nudTabWidth.Value = Settings.EditorTabWidth;

            // get the no-BOM detection value..
            cbDetectNoBomUnicode.Checked = Settings.DetectNoBom;

            // get the auto-save settings..
            cbUseAutoSave.Checked = Settings.ProgramAutoSave;
            nudAutoSaveInterval.Value = Settings.ProgramAutoSaveInterval;

            // get the tread locale value..
            cbSetThreadLocale.Checked = Settings.LocalizeThread;

            // get the value whether to check for a new application version upon startup..
            cbUpdateAutoCheck.Checked = Settings.UpdateAutoCheck;

            #region DateTime
            // get the date and time formats..
            tbDateTimeFormat1.Text = Settings.DateFormat1;
            tbDateTimeFormat2.Text = Settings.DateFormat2;
            tbDateTimeFormat3.Text = Settings.DateFormat3;
            tbDateTimeFormat4.Text = Settings.DateFormat4;
            tbDateTimeFormat5.Text = Settings.DateFormat5;
            tbDateTimeFormat6.Text = Settings.DateFormat6;
            cbDateTimeUseInvarianCulture.Checked = Settings.DateFormatUseInvariantCulture;
            #endregion
        }

        /// <summary>
        /// Saves the settings visualized on the form.
        /// </summary>
        private void SaveSettings()
        {
            // save the default encoding..
            Settings.DefaultEncoding = SelectedEncoding;

            // save the value whether to use auto-detection on a file encoding on opening a new file..
            Settings.AutoDetectEncoding = cbEncodingAutoDetect.Checked;

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

            // save the value whether to categorize the programming language menu with the language name starting character..
            Settings.CategorizeStartCharacterProgrammingLanguage = cbCategorizeProgrammingLanguages.Checked;

            // save the color values..
            Settings.SmartHighlight = btSmartHighlightColor.BackColor;
            Settings.Mark1Color = btMarkStyle1Color.BackColor;
            Settings.Mark2Color = btMarkStyle2Color.BackColor;
            Settings.Mark3Color = btMarkStyle3Color.BackColor;
            Settings.Mark4Color = btMarkStyle4Color.BackColor;
            Settings.Mark5Color = btMarkStyle5Color.BackColor;
            Settings.CurrentLineBackground = btCurrentLineBackgroundColor.BackColor;

            Settings.BraceHighlightForegroundColor = btBraceHighlightForegroundColor.BackColor;
            Settings.BraceHighlightBackgroundColor = btBraceHighlightBackgroundColor.BackColor;
            Settings.BraceBadHighlightForegroundColor = btBadBraceColor.BackColor;
            Settings.HighlightBraces = cbUseBraceMatching.Checked;
            Settings.HighlightBracesItalic = rbBraceStyleItalic.Checked;
            Settings.HighlightBracesBold = rbBraceStyleBold.Checked;
            // END: save the color values..

            // set the value of file's maximum size in megabytes (MB) to include in the file search..
            Settings.FileSearchMaxSizeMb = (long)nudMaximumSearchFileSize.Value;

            // set the amount of how much search history (search text, replace text, directories, paths, etc.) to keep..
            Settings.FileSearchHistoriesLimit = (int)nudHistoryAmount.Value;

            // set the size of the white space dot..
            Settings.EditorWhiteSpaceSize = (int)nudWhiteSpaceSize.Value;

            // set the value whether to use tabs in the editor (tabulator characters)..
            Settings.EditorUseTabs = cbUseTabs.Checked;

            // set the value whether to use individual zoom value for all
            // the open documents..
            Settings.EditorIndividualZoom = cbIndividualZoom.Checked;

            // set the value whether the zoom value of the document(s)
            // should be saved into the database..
            Settings.EditorSaveZoom = cbSaveDocumentZoom.Checked;

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
            Settings.EditorHunspellDictionaryPath = tbDictionaryPath.Text;

            // set the editor font settings..
            Settings.EditorFontSize = (int) nudFontSize.Value;
            Settings.EditorFontName = ((FontFamilyHolder) cmbFont.SelectedItem).ToString();

            // set the Notepad++ them settings..
            Settings.NotepadPlusPlusThemePath = tbNotepadPlusPlusThemePath.Text;

            Settings.UseNotepadPlusPlusTheme = cbUseNotepadPlusPlusTheme.Checked;

            if (cmbNotepadPlusPlusTheme.SelectedIndex != -1 && cbUseNotepadPlusPlusTheme.Checked)
            {
                var file = cmbNotepadPlusPlusTheme.Items[cmbNotepadPlusPlusTheme.SelectedIndex].ToString();
                file += ".xml";

                Settings.NotepadPlusPlusThemeFile = file;

                file = Path.Combine(tbNotepadPlusPlusThemePath.Text, file);
                if (File.Exists(file))
                {
                    Settings.UseNotepadPlusPlusTheme = cbUseNotepadPlusPlusTheme.Checked;
                }
            }
            else // no deal here on the theme..
            {
                Settings.UseNotepadPlusPlusTheme = false;
                Settings.NotepadPlusPlusThemeFile = string.Empty;
            }

            // set the AltGr capture bypass method if set..
            if (cmbSimulateKeyboard.SelectedIndex != -1 && cbSimulateKeyboard.Checked)
            {
                Settings.SimulateAltGrKey = true;
                Settings.SimulateAltGrKeyIndex = cmbSimulateKeyboard.SelectedIndex;
            }
            else // no deal here on the AltGr simulation..
            {
                Settings.SimulateAltGrKey = false;
                Settings.SimulateAltGrKeyIndex = -1;
            }

            // set the code indentation and editor tab width values..
            Settings.EditorUseCodeIndentation = cbUseCodeIndentation.Checked;
            Settings.EditorTabWidth = (int)nudTabWidth.Value;

            // set the no-BOM detection value..
            Settings.DetectNoBom = cbDetectNoBomUnicode.Checked;

            // set the auto-save settings..
            Settings.ProgramAutoSave = cbUseAutoSave.Checked;
            Settings.ProgramAutoSaveInterval = (int)nudAutoSaveInterval.Value;

            // set the tread locale value..
            Settings.LocalizeThread = cbSetThreadLocale.Checked;

            // set the value whether to check for a new application version upon startup..
            Settings.UpdateAutoCheck = cbUpdateAutoCheck.Checked;

            #region DateTime
            // set the date and time formats..
            Settings.DateFormat1 = tbDateTimeFormat1.Text;
            Settings.DateFormat2 = tbDateTimeFormat2.Text;
            Settings.DateFormat3 = tbDateTimeFormat3.Text;
            Settings.DateFormat4 = tbDateTimeFormat4.Text;
            Settings.DateFormat5 = tbDateTimeFormat5.Text;
            Settings.DateFormat6 = tbDateTimeFormat6.Text;
            Settings.DateFormatUseInvariantCulture = cbDateTimeUseInvarianCulture.Checked;
            #endregion
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
        /// Gets the Notepad++ style definition file if assigned in the settings.
        /// </summary>
        public static string NotepadPlusPlusStyleFile
        {
            get
            {
                if (Settings != null)
                {
                    var file = Path.Combine(Settings.NotepadPlusPlusThemePath, Settings.NotepadPlusPlusThemeFile);
                    if (File.Exists(file))
                    {
                        return file;
                    }

                    return string.Empty;
                }

                return string.Empty;
            }
        }

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

            // ReSharper disable once CoVariantArrayConversion
            cmbInstalledDictionaries.Items.AddRange(HunspellDictionaryCrawler
                .CrawlDirectory(Settings.EditorHunspellDictionaryPath).OrderBy(f => f.ToString().ToLowerInvariant())
                .ToArray());
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

        private void btCommonSelectFolder_Click(object sender, EventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog {UseDescriptionForTitle = true};

            var button = (Button) sender;
            TextBox textBox = default;
            if (button.Tag.ToString() == "tbPluginFolder")
            {
                dialog.Description = DBLangEngine.GetMessage("msgDirectoryDialogPlugin",
                    "Select the plugin folder|A message describing that the user should select a plugin folder from a browse folder dialog");
                textBox = tbPluginFolder;
            }
            else if (button.Tag.ToString() == "tbDictionaryPath")
            {
                dialog.Description = DBLangEngine.GetMessage("msgDirectoryDialogDictionary",
                    "Select the dictionary folder|A message describing that the user should select a folder where the Hunspell dictionaries reside");
                textBox = tbDictionaryPath;
            }
            else if (button.Tag.ToString() == "tbNotepadPlusPlusThemePath")
            {
                dialog.Description = DBLangEngine.GetMessage("msgDirectoryDialogNotepadPlusPlusThemes",
                    "Select a theme folder|A message describing that the user should select a folder where the Notepad++ theme files");
                textBox = tbNotepadPlusPlusThemePath;
            }

            if (textBox != null)
            {
                if (Directory.Exists(tbPluginFolder.Text))
                {
                    dialog.SelectedPath = textBox.Text;
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = dialog.SelectedPath;
                }
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

        /// <summary>
        /// Lists the found Notepad++ themes to the selection combo box.
        /// </summary>
        private void ListNotepadPlusPLusThemes()
        {
            try
            {
                cmbNotepadPlusPlusTheme.Items.Clear();
                if (Directory.Exists(tbNotepadPlusPlusThemePath.Text))
                {
                    var files = Directory.GetFiles(tbNotepadPlusPlusThemePath.Text, "*.xml");
                    foreach (var file in files)
                    {
                        cmbNotepadPlusPlusTheme.Items.Add(Path.GetFileNameWithoutExtension(file));
                    }
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);
            }
        }

        private void BtHunspellDictionary_Click(object sender, EventArgs e)
        {
            odDictionaryFile.InitialDirectory = Settings.FileLocationOpenDictionary;

            odDictionaryFile.Title = DBLangEngine.GetMessage("msgDialogSelectDicFile",
                "Select a dictionary file|A title for an open file dialog to indicate user that the user is selecting a dictionary file for the spell checking");

            if (odDictionaryFile.ShowDialog() == DialogResult.OK)
            {
                Settings.FileLocationOpenDictionary = Path.GetDirectoryName(odDictionaryFile.FileName);
                tbHunspellDictionary.Text = odDictionaryFile.FileName;
            }
        }

        private void BtHunspellAffixFile_Click(object sender, EventArgs e)
        {
            odAffixFile.InitialDirectory = Settings.FileLocationOpenAffix;

            odAffixFile.Title = DBLangEngine.GetMessage("msgDialogSelectAffixFile",
                "Select an affix file|A title for an open file dialog to indicate user that the user is selecting a Hunspell affix file for the spell checking");

            if (odAffixFile.ShowDialog() == DialogResult.OK)
            {
                Settings.FileLocationOpenAffix = Path.GetDirectoryName(odAffixFile.FileName);
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

        private void CmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFont.SelectedItem != null)
            {
                FontFamilyHolder holder = (FontFamilyHolder) cmbFont.SelectedItem;
                scintilla.Styles[Style.Default].Font = holder.ToString();
                scintilla.Styles[Style.Default].Size = (int) nudFontSize.Value;
                btOK.Enabled = true;
            }
            else
            {
                btOK.Enabled = false;
            }
        }

        private void CmbInstalledDictionaries_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (ComboBox) sender;
            if (comboBox.SelectedIndex != -1)
            {
                HunspellData data = (HunspellData) comboBox.Items[comboBox.SelectedIndex];
                tbHunspellDictionary.Text = data.DictionaryFile;
                tbHunspellAffixFile.Text = data.AffixFile;
            }
        }

        private void CmbNotepadPlusPlusTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNotepadPlusPlusTheme.SelectedIndex != -1 && cbUseNotepadPlusPlusTheme.Checked)
            {
                var file = cmbNotepadPlusPlusTheme.Items[cmbNotepadPlusPlusTheme.SelectedIndex].ToString();
                file += ".xml";

                file = Path.Combine(tbNotepadPlusPlusThemePath.Text, file);
                if (File.Exists(file))
                {
                    var colors = MarkColorsHelper.FromFile(file);
                    btSmartHighlightColor.BackColor = colors.SmartHighlight;
                    btMarkStyle1Color.BackColor = colors.Mark1Color;
                    btMarkStyle2Color.BackColor = colors.Mark2Color;
                    btMarkStyle3Color.BackColor = colors.Mark3Color;
                    btMarkStyle4Color.BackColor = colors.Mark4Color;
                    btMarkStyle5Color.BackColor = colors.Mark5Color;
                    btCurrentLineBackgroundColor.BackColor = colors.CurrentLineBackground;
                }
            }
        }

        private string lastFocusedDateTime = string.Empty;

        private void TbDateTimeFormat1_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = null;
            try
            {
                if (sender is TextBox)
                {
                    textBox = (TextBox) sender;
                    lastFocusedDateTime = textBox.Text;

                    lbDateTimeFormatDescriptionValue.Text = DateTime.Now.ToString(lastFocusedDateTime,
                        // we need to ensure that an overridden thread locale will not affect the non-invariant culture setting..
                        cbDateTimeUseInvarianCulture.Checked
                            ? CultureInfo.InvariantCulture
                            : CultureInfo.InstalledUICulture);
                }
                else if (sender is CheckBox)
                {
                    lbDateTimeFormatDescriptionValue.Text = DateTime.Now.ToString(lastFocusedDateTime,
                        // we need to ensure that an overridden thread locale will not affect the non-invariant culture setting..
                        cbDateTimeUseInvarianCulture.Checked
                            ? CultureInfo.InvariantCulture
                            : CultureInfo.InstalledUICulture);
                }

                if (textBox != null)
                {
                    textBox.BackColor = SystemColors.Window;
                }
            }
            catch (Exception ex)
            {
                if (textBox != null)
                {
                    textBox.BackColor = Color.Red;
                }

                lbDateTimeFormatDescriptionValue.Text = DBLangEngine.GetMessage(
                    "msgDateTimeInvalidFormat", 
                    "Invalid date and/or time format|The user has issued an non-valid formatted date and/or time formatting string.");

                ExceptionLogger.LogError(ex);
            }
        }

        // try to open a web browser for further instructions of how to specify a time and/or date..
        private void LbDateTimeInstructionLink_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(
                    "https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings");
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);
            }
        }

        // the user wants to reset the date and/or time format strings to default values..
        private void BtDateTimeDefaults_Click(object sender, EventArgs e)
        {
            tbDateTimeFormat1.Text = @"yyyy'/'MM'/'dd"; // default to american..
            tbDateTimeFormat2.Text = @"dd'.'MM'.'yyyy"; // default to european..
            tbDateTimeFormat3.Text = @"yyyy'/'MM'/'dd hh'.'mm tt"; // default to american..
            tbDateTimeFormat4.Text = @"dd'.'MM'.'yyyy HH':'mm':'ss"; // default to european..
            tbDateTimeFormat5.Text = @"hh'.'mm tt"; // default to american..
            tbDateTimeFormat6.Text = @"HH':'mm':'ss"; // default to european..
        }
    }
}
