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

using ScriptNotepad.UtilityClasses.Encoding.CharacterSets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPKSoft.LangLib;

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
            CharacterSetComboBuilder.EncodingSelected += CaracterSetComboBuilder_EncodingSelected;

            // translate the tool tips..
            ttMain.SetToolTip(btUTF8,
                DBLangEngine.GetMessage("msgUTF8Encoding", "Set to Unicode (UTF8)|Set the selected encoding to UTF8 via a button click"));

            // translate the tool tips..
            ttMain.SetToolTip(btSystemDefaultEncoding,
                DBLangEngine.GetMessage("msgSysDefaultEncoding", "Set to system default|Set the selected encoding to system's default encoding via a button click"));

            // list the translated cultures..
            List<CultureInfo> cultures = DBLangEngine.GetLocalizedCultures();

            // a the translated cultures to the selection combo box..
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
        }
        #endregion

        /// <summary>
        /// Gets or sets the character set combo box builder.
        /// </summary>
        private CharacterSetComboBuilder CharacterSetComboBuilder { get; set; }

        // this event is fired when the encoding is changed from the corresponding combo box..
        private void CaracterSetComboBuilder_EncodingSelected(object sender, OnEncodingSelectedEventArgs e)
        {
            // save the changed value..
            SelectedEncoding = e.Encoding;
        }

        /// <summary>
        /// Gets or sets the settings class instance containing the settings for the software.
        /// </summary>
        public static Settings Settings { get; set; }

        /// <summary>
        /// Gets or sets the encoding a user selected from the dialog.
        /// </summary>
        private Encoding SelectedEncoding { get; set; }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (CharacterSetComboBuilder)
            {
                // unsubscribe the encoding selected event..
                CharacterSetComboBuilder.EncodingSelected -= CaracterSetComboBuilder_EncodingSelected;
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
    }
}
