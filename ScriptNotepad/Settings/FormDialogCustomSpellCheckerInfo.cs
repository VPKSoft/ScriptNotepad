using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPKSoft.ErrorLogger;
using VPKSoft.ExternalDictionaryPackage;
using VPKSoft.LangLib;

namespace ScriptNotepad.Settings
{
    /// <summary>
    /// A dialog to display information about a custom spell checker library.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogCustomSpellCheckerInfo : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogCustomSpellCheckerInfo"/> class.
        /// </summary>
        public FormDialogCustomSpellCheckerInfo()
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
        }

        private const string NuGetLicenseUrl = "https://licenses.nuget.org/";

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box. </param>
        /// <param name="xmlDefinitionFile">The XML definition file name.</param>
        public static void ShowDialog(IWin32Window owner, string xmlDefinitionFile)
        {
            try
            {
                var data =
                    DictionaryPackage.GetXmlDefinitionDataFromDefinitionFile(xmlDefinitionFile);
                var form = new FormDialogCustomSpellCheckerInfo
                {
                    tbName = {Text = data.name},
                    tbLibrary = {Text = data.lib},
                    tbCompany = {Text = data.company},
                    tbCopyright = {Text = data.copyright},
                    tbCulture = {Text = data.cultureName},
                    tbCultureDescription = {Text = data.cultureDescription},
                    tbCultureDescriptionNative = {Text = data.cultureDescriptionNative},
                    tbUrl = {Text = data.url},
                    lbSpdxLicenseLinkValue = {Text = data.spdxLicenseId, Tag = NuGetLicenseUrl + data.spdxLicenseId},
                };

                using (form)
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lbSpdxLicenseLinkValue_Click(object sender, EventArgs e)
        {
            var label = (Label) sender;
            if (label.Tag.ToString().StartsWith(NuGetLicenseUrl))
            {
                try
                {
                    Process.Start(label.Tag.ToString());
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionLogger.LogError(ex);
                }
            }
        }
    }
}
