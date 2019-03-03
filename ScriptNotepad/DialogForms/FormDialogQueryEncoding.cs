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
using System.Text;
using System.Windows.Forms;
using VPKSoft.LangLib;

namespace ScriptNotepad.DialogForms
{
    /// <summary>
    /// A dialog to query for an encoding to a file while opening a file.
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogQueryEncoding : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogQueryEncoding"/> class.
        /// </summary>
        public FormDialogQueryEncoding()
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
        }

        /// <summary>
        /// Displays a new instance of this dialog.
        /// </summary>
        /// <returns>An <see cref="Encoding"/> if the user accepted the dialog; otherwise null.</returns>
        public static Encoding Execute()
        {
            // create a new instance of this dialog..
            FormDialogQueryEncoding form = new FormDialogQueryEncoding();

            // show the dialog and if the dialog result was OK..
            if (form.ShowDialog() == DialogResult.OK)
            {
                // ..so return the encoding..
                return form.Encoding;
            }
            else // the user didn't accept the dialog..
            {
                // .. so return null..
                return null;
            }
        }

        // this event is fired when the encoding is changed from the corresponding combo box..
        private void CaracterSetComboBuilder_EncodingSelected(object sender, OnEncodingSelectedEventArgs e)
        {
            // save the changed value..
            Encoding = e.Encoding;
        }

        /// <summary>
        /// Gets or sets the encoding a user selected from the dialog.
        /// </summary>
        private Encoding Encoding { get; set; }

        /// <summary>
        /// Gets or sets the character set combo box builder.
        /// </summary>
        private CharacterSetComboBuilder CharacterSetComboBuilder { get; set; }

        // dispose of all the resourced used..
        private void FormDialogQueryEncoding_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (CharacterSetComboBuilder)
            {
                // unsubscribe the encoding selected event..
                CharacterSetComboBuilder.EncodingSelected -= CaracterSetComboBuilder_EncodingSelected;
            }
        }

        private void btDefaultEncodings_Click(object sender, System.EventArgs e)
        {
            // select the encoding based on which button the user clicked..
            CharacterSetComboBuilder.SelectItemByEncoding(sender.Equals(btUTF8) ? Encoding.UTF8 : Encoding.Default, false);
        }
    }
}
