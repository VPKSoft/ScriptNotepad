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

using ScriptNotepad.UtilityClasses.Encodings.CharacterSets;
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
                DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages");

            // create a new instance of the CharacterSetComboBuilder class..
            CharacterSetComboBuilder = new CharacterSetComboBuilder(cmbCharacterSet, cmbEncoding, tbFilterEncodings, false, "encoding");

            Encoding = CharacterSetComboBuilder.CurrentEncoding;

            // subscribe the encoding selected event..
            CharacterSetComboBuilder.EncodingSelected += CharacterSetComboBuilder_EncodingSelected;

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
        /// <param name="unicodeBom">In case of Unicode (UTF8, Unicode or UTF32) whether to use the Unicode byte order mark.</param>
        /// <param name="unicodeFailInvalidCharacters">In case of Unicode (UTF8, Unicode or UTF32) whether to fail on invalid characters.</param>
        /// <returns>An <see cref="Encoding"/> if the user accepted the dialog; otherwise null.</returns>
        public static Encoding Execute(out bool unicodeBom, out bool unicodeFailInvalidCharacters)
        {
            // create a new instance of this dialog..
            FormDialogQueryEncoding form = new FormDialogQueryEncoding();

            // show the dialog and if the dialog result was OK..
            if (form.ShowDialog() == DialogResult.OK)
            {
                unicodeBom = form.cbUseUnicodeBOM.Checked && form.cbUseUnicodeBOM.Enabled;
                unicodeFailInvalidCharacters = form.cbUnicodeFailInvalidCharacters.Checked &&
                                               form.cbUnicodeFailInvalidCharacters.Enabled;

                if (form.Encoding is UTF8Encoding)
                {
                    return new UTF8Encoding(unicodeBom, unicodeFailInvalidCharacters);
                }

                if (form.Encoding is UnicodeEncoding)
                {
                    return new UnicodeEncoding(form.Encoding.CodePage == 1201, unicodeBom,
                        unicodeFailInvalidCharacters);
                }

                if (form.Encoding is UTF32Encoding)
                {
                    return new UTF32Encoding(form.Encoding.CodePage == 12001, unicodeBom,
                        unicodeFailInvalidCharacters);
                }

                // ..so return the encoding..
                return form.Encoding;
            }

            // .. so return null..
            unicodeBom = false;
            unicodeFailInvalidCharacters = false;
            return null;
        }

        // this event is fired when the encoding is changed from the corresponding combo box..
        private void CharacterSetComboBuilder_EncodingSelected(object sender, OnEncodingSelectedEventArgs e)
        {
            // save the changed value..
            Encoding = e.Encoding;
        }

        private Encoding _Encoding = null;

        /// <summary>
        /// Gets or sets the encoding a user selected from the dialog.
        /// </summary>
        private Encoding Encoding
        {
            get => _Encoding;

            set
            {
                _Encoding = value;
                btOK.Enabled = value != null;
            }
        }

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
                CharacterSetComboBuilder.EncodingSelected -= CharacterSetComboBuilder_EncodingSelected;
            }
        }

        private void btDefaultEncodings_Click(object sender, System.EventArgs e)
        {
            // select the encoding based on which button the user clicked..
            CharacterSetComboBuilder.SelectItemByEncoding(sender.Equals(btUTF8) ? Encoding.UTF8 : Encoding.Default, false);
        }

        private void FormDialogQueryEncoding_Shown(object sender, System.EventArgs e)
        {
            tbFilterEncodings.Focus();
        }

        private void cmbCharacterSet_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            cbUseUnicodeBOM.Enabled = false;
            cbUnicodeFailInvalidCharacters.Enabled = false;

            tbFilterEncodings.Focus();

            // the lower combo box on the dialog..
            if (sender.Equals(cmbEncoding))
            {
                ComboBox combo = (ComboBox) sender;
                if (combo.SelectedItem != null)
                {
                    var item = (CharacterSetComboItem) combo.SelectedItem;
                    if (item.Encoding.CodePage == 65001 || // UTF8..
                        item.Encoding.CodePage == 1200 || // Unicode Little Endian..
                        item.Encoding.CodePage == 1201 || // Unicode Big Endian..
                        item.Encoding.CodePage == 12000 || // UTF32 Little Endian..
                        item.Encoding.CodePage == 12001) // UTF32 Big Endian..
                    {
                        cbUseUnicodeBOM.Enabled = true;
                        cbUnicodeFailInvalidCharacters.Enabled = true;
                    }
                }
            }
        }

        private void pbClearFilterText_Click(object sender, System.EventArgs e)
        {
            tbFilterEncodings.Text = string.Empty;
        }
    }
}
