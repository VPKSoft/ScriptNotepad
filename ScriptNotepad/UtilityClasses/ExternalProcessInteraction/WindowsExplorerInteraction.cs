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

namespace ScriptNotepad.UtilityClasses.ExternalProcessInteraction
{
    /// <summary>
    /// A class to interact with Windows explorer.
    /// </summary>
    public static class WindowsExplorerInteraction
    {
        /// <summary>
        /// Gets or sets the action to be used to log an exception.
        /// </summary>
        public static Action<Exception> ExceptionLogAction { get; set; } = null;

        /// <summary>
        /// Shows the file or path in Windows explorer.
        /// </summary>
        /// <param name="fileOrPath">The file or path to show in the Windows explorer.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public static bool ShowFileOrPathInExplorer(string fileOrPath)
        {
            try
            {
                // (C): https://social.msdn.microsoft.com/Forums/vstudio/en-US/a6e1458a-20d0-48b4-8e3a-0a00c8618d75/opening-folder-in-explorer-by-c-code?forum=netfxbcl
                System.Diagnostics.Process.Start(@"explorer.exe", $"/e,/select,{fileOrPath}");
                return true;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return false;
            }
        }

        /// <summary>
        /// Opens the file with associated program.
        /// </summary>
        /// <param name="fileName">The name of the file to open.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public static bool OpenWithAssociatedProgram(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
                return true;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return false;
            }
        }
    }
}
