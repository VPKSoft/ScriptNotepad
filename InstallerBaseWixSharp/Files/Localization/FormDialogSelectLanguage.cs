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
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using WixSharp;

namespace InstallerBaseWixSharp.Files.Localization
{
    /// <summary>
    /// A language selection dialog for the installer.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormDialogSelectLanguage : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogSelectLanguage"/> class.
        /// </summary>
        public FormDialogSelectLanguage()
        {
            InitializeComponent();

            var selectedItem = LanguageList.FirstOrDefault(f => f.Key == SupportedLanguages.EnglishUnitedStates);
            
            var values = Enum.GetValues(typeof(SupportedLanguages));
            foreach (var value in values)
            {
                var type = value.GetType();
                var info = type.GetMember(value.ToString());
                var localizationDataAttribute = (LocalizationDataAttribute)info[0].GetCustomAttributes(typeof(LocalizationDataAttribute), false)[0];

                if (localizationDataAttribute.Localized)
                {
                    if (localizationDataAttribute.Code == CultureInfo.CurrentUICulture.Name)
                    {
                        selectedItem = new KeyValuePair<SupportedLanguages, string>((SupportedLanguages) value,
                            localizationDataAttribute.Description);
                    }

                    LanguageList.Add(new KeyValuePair<SupportedLanguages, string>((SupportedLanguages) value,
                        localizationDataAttribute.Description));
                }
            }
            
            LanguageList = LanguageList.OrderBy(f => f.Value).ToList();

            foreach (var language in LanguageList)
            {
                cmbLanguage.Items.Add(language);
            }

            cmbLanguage.SelectedItem = selectedItem;
        }

        private List<KeyValuePair<SupportedLanguages, string>> LanguageList { get; } = new List<KeyValuePair<SupportedLanguages, string>>();

        /// <summary>
        /// Displays the dialog for language selection for the user.
        /// </summary>
        /// <returns>A value representing the user selection of the <see cref="SupportedLanguages"/> enumeration.</returns>
        public static SupportedLanguages SelectLanguage()
        {
            using (var form = new FormDialogSelectLanguage())
            {
                form.ShowDialog();
                return ((KeyValuePair<SupportedLanguages, string>)form.cmbLanguage.SelectedItem).Key;
            }
        }

        private void FormDialogSelectLanguage_Shown(object sender, EventArgs e)
        {
            Activate(); // this seems to be deactivated at this point already..
        }
    }
}























