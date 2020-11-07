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

using InstallerBaseWixSharp.Files.Localization;
using WixSharp;
using WixSharp.CommonTasks;

namespace InstallerBaseWixSharp
{
    // Sample (C): https://github.com/oleg-shilo/wixsharp/blob/master/Source/src/WixSharp.Samples/Wix%23%20Samples/Managed%20Setup/MultiLanguageUI/setup.cs

    /// <summary>
    /// A class containing helper methods to create a single multi-lingual MSI installer file.
    /// </summary>
    public static class ProjectLocalizationHelper
    {
        static SupportedLanguages DetectLanguage()
        {
            return FormDialogSelectLanguage.SelectLanguage();
        }

        /// <summary>
        /// Gets the <see cref="LocalizationDataAttribute"/> attribute from a specified <see cref="SupportedLanguages"/> enumeration value.
        /// </summary>
        /// <param name="language">The <see cref="SupportedLanguages"/> enumeration value.</param>
        /// <returns>A <see cref="LocalizationDataAttribute"/> for the specified <see cref="SupportedLanguages"/> enumeration value.</returns>
        internal static LocalizationDataAttribute GetAttribute(SupportedLanguages language)
        {
            var type = language.GetType();
            var info = type.GetMember(language.ToString());
            var localizationDataAttribute = (LocalizationDataAttribute)info[0].GetCustomAttributes(typeof(LocalizationDataAttribute), false)[0];
            return localizationDataAttribute;
        }


        /// <summary>
        /// Localizes the specified <see cref="ManagedProject"/> project.
        /// </summary>
        /// <param name="project">The <see cref="ManagedProject"/> project to localize.</param>
        public static void Localize(this ManagedProject project)
        {
            project.AddBinary(new Binary(new Id("fi_FI_xsl"), @"Files\Localization\WixUI_fi-FI.wxl"));
            project.AddBinary(new Binary(new Id("en_US_xsl"), @"Files\Localization\WixUI_en-US.wxl"));

            project.UIInitialized += e =>
            {
                
                MsiRuntime runtime = e.ManagedUI.Shell.MsiRuntime();


                var language = DetectLanguage();
                e.Session["LANGNAME"] = GetAttribute(language).Code;

                switch (language)
                {
                    case SupportedLanguages.FinnishFinland:
                        runtime.UIText.InitFromWxl(e.Session.ReadBinary("fi_FI_xsl"));
                        break;
                    
                    case SupportedLanguages.EnglishUnitedStates:
                        runtime.UIText.InitFromWxl(e.Session.ReadBinary("en_US_xsl"));
                        break;

                    default: // default to English (US)..
                        runtime.UIText.InitFromWxl(e.Session.ReadBinary("en_US_xsl"));
                        break;
                }
            };
        }
    }
}

