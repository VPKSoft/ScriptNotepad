#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

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

using System.Diagnostics;
using System.Reflection;
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

        // ReSharper disable once InconsistentNaming
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

                var assemblyName = Path.Combine(Path.GetDirectoryName(xmlDefinitionFile) ?? string.Empty, data.lib);

                var version = "1.0.0.0";

                try
                {
                    var assembly = Assembly.LoadFile(assemblyName);
                    version = assembly.GetName().Version.ToString();
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionLogger.LogError(ex);
                }


                var form = new FormDialogCustomSpellCheckerInfo
                {
                    tbName = {Text = data.name},
                    tbLibrary = {Text = data.lib},
                    tbCompany = {Text = data.company},
                    tbCopyright = {Text = data.copyright},
                    tbCulture = {Text = data.cultureName},
                    tbCultureDescription = {Text = data.cultureDescription},
                    tbCultureDescriptionNative = {Text = data.cultureDescriptionNative},
                    lbUrlValue = {Text = data.url},
                    lbSpdxLicenseLinkValue = {Text = data.spdxLicenseId, Tag = NuGetLicenseUrl + data.spdxLicenseId},
                    tbAssemblyVersion = {Text = version},
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

        private void lbUrlValue_Click(object sender, EventArgs e)
        {
            try
            {
                var label = (Label) sender;
                Process.Start(label.Text);
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);
            }
        }
    }
}
