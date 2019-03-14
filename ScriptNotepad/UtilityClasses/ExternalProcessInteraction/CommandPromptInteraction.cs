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
using System.Diagnostics;

namespace ScriptNotepad.UtilityClasses.ExternalProcessInteraction
{
    /// <summary>
    /// A class for interacting with the Command Prompt (cmd.exe) and with Windows PowerShell (powershell.exe).
    /// </summary>
    public static class CommandPromptInteraction
    {
        /// <summary>
        /// Gets or sets the action to be used to log an exception.
        /// </summary>
        public static Action<Exception> ExceptionLogAction { get; set; } = null;

        /// <summary>
        /// Shows the Command Prompt (cmd.exe) with a given specific path.
        /// </summary>
        /// <param name="path">The path to start the Command Prompt with.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public static bool OpenCmdWithPath(string path)
        {
            try
            {
                var processStartInfo = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    WorkingDirectory = path,
                    UseShellExecute = false,
                    LoadUserProfile = true,
                };

                System.Diagnostics.Process.Start(processStartInfo);
                
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
        /// Opens the Windows PowerShell (powershell.exe) with a given specific path.
        /// </summary>
        /// <param name="path">The path to start the Windows PowerShell with.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public static bool OpenPowerShellWithPath(string path)
        {
            try
            {
                var processStartInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    WorkingDirectory = path,
                    UseShellExecute = false,
                    LoadUserProfile = true,
                };

                System.Diagnostics.Process.Start(processStartInfo);

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

