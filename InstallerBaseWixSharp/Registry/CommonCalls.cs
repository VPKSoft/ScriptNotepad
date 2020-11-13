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

using Microsoft.Win32;

namespace InstallerBaseWixSharp.Registry
{
    /// <summary>
    /// A class for common registry methods.
    /// </summary>
    public class CommonCalls
    {
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once IdentifierTypo        
        // ReSharper disable once CommentTypo
        /// <summary>
        /// Opens the or creates a <see cref="Registry"/> key to HKLM (HKey Local Machine).
        /// </summary>
        /// <param name="path">The registry path.</param>
        /// <returns>The opened or created <see cref="RegistryKey"/> instance.</returns>
        public static RegistryKey OpenOrCreateKeyHKLM(string path)
        {
            var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(path, true);
            if (key == null)
            {
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(path);
                key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(path, true);
            }
            return key;
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once IdentifierTypo        
        // ReSharper disable once CommentTypo
        /// <summary>
        /// Opens the or creates a <see cref="Registry"/> key to HKCS (HKey Classes Root).
        /// </summary>
        /// <param name="path">The registry path.</param>
        /// <returns>The opened or created <see cref="RegistryKey"/> instance.</returns>
        public static RegistryKey OpenOrCreateKeyHKCR(string path)
        {
            var key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(path, true);
            if (key == null)
            {
                Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(path);
                key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(path, true);
            }
            return key;
        }

        /// <summary>
        /// Gets the registry key valueName.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <returns>The valueName as a string.</returns>
        public static string GetKeyValue(string company, string applicationName, string valueName)
        {
            using (var key = OpenOrCreateKeyHKLM($@"SOFTWARE\{company}\{applicationName}"))
            {
                return key.GetValue(valueName).ToString();
            }
        }

        /// <summary>
        /// Sets the registry key value.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="value">The value.</param>
        public static void SetKeyValue(string company, string applicationName, string valueName, string value)
        {
            using (var key = OpenOrCreateKeyHKLM($@"SOFTWARE\{company}\{applicationName}"))
            {
                key.SetValue(valueName, value);
            }
        }

        /// <summary>
        /// Deletes the registry key value.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="valueName">Name of the value.</param>
        public static void DeleteValue(string company, string applicationName, string valueName)
        {
            using (var key = OpenOrCreateKeyHKLM($@"SOFTWARE\{company}\{applicationName}"))
            {
                key.DeleteValue(valueName);
            }
        }

        /// <summary>
        /// Deletes the company key from the <see cref="Registry"/> if there are no keys defined.
        /// </summary>
        /// <param name="company">The company name.</param>
        /// <returns><c>true</c> if the key was successfully deleted, <c>false</c> otherwise.</returns>
        public static bool DeleteCompanyKeyIfEmpty(string company)
        {
            try
            {
                var companyRegistryTree = @"SOFTWARE\" +
                                          company;

                var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(companyRegistryTree);
                if (key?.ValueCount == 0)
                {
                    Microsoft.Win32.Registry.LocalMachine.DeleteSubKeyTree(companyRegistryTree);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        // ReSharper disable once CommentTypo
    }
}
