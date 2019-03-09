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
using ScriptNotepad.UtilityClasses.Encoding.CharacterSets;

namespace ScriptNotepad
{
    /// <summary>
    /// A helper form to localize some other class properties, etc.
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormLocalizationHelper : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormLocalizationHelper"/> class.
        /// </summary>
        public FormLocalizationHelper()
        {
            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            LocalizeCharacterSets();

            // localize the names which are used to display line ending types of the document..
            LocalizeLineEndingTypeNames();
        }

        /// <summary>
        /// Localizes some other class properties, etc.
        /// </summary>
        public static void LocalizeMisc()
        {
            // just make a new instance of this form and the forget about it..
            new FormLocalizationHelper();
        }

        /// <summary>
        /// Localizes the line ending type names.
        /// </summary>
        private void LocalizeLineEndingTypeNames()
        {
            UtilityClasses.LinesAndBinary.FileLineType.CRLF_Description =
                DBLangEngine.GetMessage("msgLineEndingCRLF", "CR+LF|A description for a line ending sequence for CR+LF.");

            UtilityClasses.LinesAndBinary.FileLineType.LF_Description =
                DBLangEngine.GetMessage("msgLineEndingLF", "LF|A description for a line ending sequence for LF.");

            UtilityClasses.LinesAndBinary.FileLineType.CR_Description =
                DBLangEngine.GetMessage("msgLineEndingCR", "CR|A description for a line ending sequence for CR.");

            UtilityClasses.LinesAndBinary.FileLineType.RS_Description =
                DBLangEngine.GetMessage("msgLineEndingRS", "RS|A description for a line ending sequence for RS.");

            UtilityClasses.LinesAndBinary.FileLineType.LFCR_Description =
                DBLangEngine.GetMessage("msgLineEndingLFCR", "LF+CR|A description for a line ending sequence for LF+CR.");

            UtilityClasses.LinesAndBinary.FileLineType.NL_Description =
                DBLangEngine.GetMessage("msgLineEndingNL", "NL|A description for a line ending sequence for NL.");

            UtilityClasses.LinesAndBinary.FileLineType.ATASCII_Description =
                DBLangEngine.GetMessage("msgLineEndingATASCII", "ATASCII|A description for a line ending sequence for ATASCII.");

            UtilityClasses.LinesAndBinary.FileLineType.NEWLINE_Description =
                DBLangEngine.GetMessage("msgLineEndingNEWLINE", "NEWLINE|A description for a line ending sequence for NEWLINE.");

            UtilityClasses.LinesAndBinary.FileLineType.Unknown_Description =
                DBLangEngine.GetMessage("msgLineEndingUnknown", "Unknown|A description for a line ending sequence for Unknown / non-existent line ending.");

            UtilityClasses.LinesAndBinary.FileLineType.Mixed_Description =
                DBLangEngine.GetMessage("msgLineEndingMixed", "Mixed|A description for a line ending sequence for Mixed (multiple types of line endings).");

            UtilityClasses.LinesAndBinary.FileLineType.UCRLF_Description =
                DBLangEngine.GetMessage("msgLineEndingUCRLF", "Unicode CR+LF|A description for a line ending sequence for Unicode CR+LF.");

            UtilityClasses.LinesAndBinary.FileLineType.ULF_Description =
                DBLangEngine.GetMessage("msgLineEndingULF", "Unicode LF|A description for a line ending sequence for Unicode LF.");

            UtilityClasses.LinesAndBinary.FileLineType.UCR_Description =
                DBLangEngine.GetMessage("msgLineEndingUCR", "Unicode CR|A description for a line ending sequence for Unicode CR.");

            UtilityClasses.LinesAndBinary.FileLineType.ULFCR_Description =
                DBLangEngine.GetMessage("msgLineEndingULFCR", "Unicode LF+CR|A description for a line ending sequence for Unicode LF+CR.");
        }

        /// <summary>
        /// Localizes the character sets of the <see cref="EncodingCharacterSet"/> class.
        /// </summary>
        private void LocalizeCharacterSets()
        {
            EncodingCharacterSet encodingCharacterSet = new EncodingCharacterSet();
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Arabic, DBLangEngine.GetMessage("msgCharSetArabic", "Arabic|A message describing Arabic character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Baltic, DBLangEngine.GetMessage("msgCharSetBaltic", "Baltic|A message describing Arabic character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Canada, DBLangEngine.GetMessage("msgCharSetCanada", "Canada|A message describing Canada character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Cyrillic, DBLangEngine.GetMessage("msgCharSetCyrillic", "Cyrillic|A message describing Cyrillic character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.CentralEuropean, DBLangEngine.GetMessage("msgCharSetCentralEuropean", "Central European|A message describing Central European character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Chinese, DBLangEngine.GetMessage("msgCharSetChinese", "Chinese|A message describing Chinese character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.DenmarkNorway, DBLangEngine.GetMessage("msgCharSetDenmarkNorway", "Denmark-Norway|A message describing Denmark-Norway character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.FinlandSweden, DBLangEngine.GetMessage("msgCharSetFinlandSweden", "Finland-Sweden|A message describing Finland-Sweden character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.France, DBLangEngine.GetMessage("msgCharSetFrance", "France|A message describing France character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.German, DBLangEngine.GetMessage("msgCharSetGerman", "German|A message describing German character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Greek, DBLangEngine.GetMessage("msgCharSetGreek", "Greek|A message describing Greek character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Hebrew, DBLangEngine.GetMessage("msgCharSetHebrew", "Hebrew|A message describing Hebrew character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Icelandic, DBLangEngine.GetMessage("msgCharSetIcelandic", "Icelandic|A message describing Icelandic character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Italy, DBLangEngine.GetMessage("msgCharSetItaly", "Italy|A message describing Italy character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Japanese, DBLangEngine.GetMessage("msgCharSetJapanese", "Japanese|A message describing Japanese character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Korean, DBLangEngine.GetMessage("msgCharSetKorean", "Korean|A message describing Korean character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Latin, DBLangEngine.GetMessage("msgCharSetLatin", "Latin|A message describing Latin character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Miscellaneous, DBLangEngine.GetMessage("msgCharSetMiscellaneous", "Miscellaneous|A message describing Miscellaneous character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Norwegian, DBLangEngine.GetMessage("msgCharSetNorwegian", "Norwegian|A message describing Norwegian character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.WesternEuropean, DBLangEngine.GetMessage("msgCharSetWesternEuropean", "Western European|A message describing Western European character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Spain, DBLangEngine.GetMessage("msgCharSetSpain", "Spain|A message describing Spain character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Swedish, DBLangEngine.GetMessage("msgCharSetSwedish", "Swedish|A message describing Swedish character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Taiwan, DBLangEngine.GetMessage("msgCharSetTaiwan", "Taiwan|A message describing Taiwan character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Thai, DBLangEngine.GetMessage("msgCharSetThai", "Thai|A message describing Thai character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Turkish, DBLangEngine.GetMessage("msgCharSetTurkish", "Turkish|A message describing Turkish character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Unicode, DBLangEngine.GetMessage("msgCharSetUnicode", "Unicode|A message describing Unicode character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Assamese, DBLangEngine.GetMessage("msgCharSetAssamese", "Assamese|A message describing Assamese character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Bengali, DBLangEngine.GetMessage("msgCharSetBengali", "Bengali|A message describing Bengali character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Devanagari, DBLangEngine.GetMessage("msgCharSetDevanagari", "Devanagari|A message describing Devanagari character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Estonian, DBLangEngine.GetMessage("msgCharSetEstonian", "Estonian|A message describing Estonian character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Kannada, DBLangEngine.GetMessage("msgCharSetKannada", "Kannada|A message describing Kannada character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Malayalam, DBLangEngine.GetMessage("msgCharSetMalayalam", "Malayalam|A message describing Arabic character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Oriya, DBLangEngine.GetMessage("msgCharSetOriya", "Oriya|A message describing Oriya character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Punjabi, DBLangEngine.GetMessage("msgCharSetPunjabi", "Punjabi|A message describing Punjabi character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Tamil, DBLangEngine.GetMessage("msgCharSetTamil", "Tamil|A message describing Tamil character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Telugu, DBLangEngine.GetMessage("msgCharSetTelugu", "Telugu|A message describing Telugu character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.Vietnamese, DBLangEngine.GetMessage("msgCharSetVietnamese", "Vietnamese|A message describing Vietnamese character set(s)."));
            encodingCharacterSet.LocalizeCharacterSetName(CharacterSets.SingleCharacterSets, DBLangEngine.GetMessage("msgCharSetSingleCharacterSets", "Single Character Sets|A message describing Single Character Sets character set(s)."));
        }
    }
}
