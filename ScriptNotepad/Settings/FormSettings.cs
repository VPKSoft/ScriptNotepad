using ScriptNotepad.UtilityClasses.Encoding.CharacterSets;
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

        /// <summary>
        /// Loads the settings visualized to form.
        /// </summary>
        private void LoadSettings()
        {
            // select the encoding from the settings..
            CharacterSetComboBuilder.SelectItemByEncoding(Settings.DefaultEncoding, false);

            // set the amount of document file names to keep in history..
            nudHistoryDocuments.Value = Settings.HistoryListAmount;
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
        }

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
    }
}
