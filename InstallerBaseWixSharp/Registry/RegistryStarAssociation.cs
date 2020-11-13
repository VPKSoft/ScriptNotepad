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

using System;

namespace InstallerBaseWixSharp.Registry
{
    /// <summary>
    /// A class to register file associations for a software to open any file.
    /// </summary>
    public class RegistryStarAssociation
    {
        /// <summary>
        /// Registers the star association (open any file with the software).
        /// </summary>
        /// <param name="applicationExecutableFile">The application executable file.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="associationText">The text to display in the shell context menu.</param>
        /// <param name="iconIndex">Index of the icon to use from the <see paramref="applicationExecutableFile"/>.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool RegisterStarAssociation(string applicationExecutableFile, string applicationName, string associationText, int iconIndex = 0)
        {
            try
            {
                using (var key = CommonCalls.OpenOrCreateKeyHKCR(@$"*\Shell\{associationText}"))
                {
                    key.SetValue("Icon", string.Concat(applicationExecutableFile, $",{iconIndex}"));
                }

                using (var key = CommonCalls.OpenOrCreateKeyHKCR(@$"*\Shell\{associationText}\command"))
                {
                    key.SetValue("", string.Concat(applicationExecutableFile, " \"%1\""));
                }

                using (var key = CommonCalls.OpenOrCreateKeyHKLM(@$"SOFTWARE\Classes\*\Shell\{associationText}"))
                {
                    key.SetValue("Icon", string.Concat(applicationExecutableFile, $",{iconIndex}"));
                }

                using (var key = CommonCalls.OpenOrCreateKeyHKLM(@$"SOFTWARE\Classes\*\Shell\{associationText}\command"))
                {
                    key.SetValue("", string.Concat(applicationExecutableFile, " \"%1\""));
                }

                RegistryFileAssociation.ShellChangeNotify();

                return true;
            }
            catch (Exception ex)
            {
                // report the exception..
                ReportExceptionAction?.Invoke(ex);
                return false;
            }
        }

        /// <summary>
        /// Removes a star association from the registry (open any file with the software).
        /// </summary>
        /// <param name="associationText">The text, which was displayed in the shell context menu.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool UnRegisterStarAssociation(string associationText)
        {
            try
            {
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree(@$"*\Shell\{associationText}");
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree(@$"SOFTWARE\Classes\*\Shell\{associationText}");
                RegistryFileAssociation.ShellChangeNotify();
                return true;
            }
            catch (Exception ex)
            {
                // report the exception..
                ReportExceptionAction?.Invoke(ex);
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the action which is called in case of an exception within the class code.
        /// </summary>
        /// <value>The report exception action.</value>
        public static Action<Exception> ReportExceptionAction { get; set; }
    }
}
