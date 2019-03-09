﻿#region License
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

using ScriptNotepad.UtilityClasses.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ScriptNotepad.UtilityClasses.ErrorHandling.ExceptionDelegate;

namespace ScriptNotepad.UtilityClasses.ExternalProcessInteraction
{
    /// <summary>
    /// A class for interacting with the Command Prompt (cmd.exe) and with Windows PowerShell (powershell.exe).
    /// </summary>
    public static class CommandPromptInteraction
    {
        /// <summary>
        /// The last exception which occurred within a method of "this" static class.
        /// </summary>
        private static Exception _LastException = null;

        /// <summary>
        /// Gets the last exception of a something gone wrong.
        /// </summary>
        public static Exception LastException
        {
            get => _LastException;

            // private as only static methods of this class can actually set the value of a last exception..
            private set
            {
                // raise an event if there is an actual exception..
                if (value != null)
                {
                    // ..and the event is subscribed..
                    ExceptionOccurred?.Invoke(typeof(WindowsExplorerInteraction), new ExceptionEventArgs { Exception = value });
                }
                // save the last exception..
                _LastException = value;
            }
        }

        /// <summary>
        /// Occurs when a handled exception occurred within this class.
        /// </summary>
        public static event OnExceptionOccurred ExceptionOccurred = null;

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

                Process.Start(processStartInfo);
                
                return true;
            }
            catch (Exception ex)
            {
                LastException = ex; // log the exception..
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

                Process.Start(processStartInfo);

                return true;
            }
            catch (Exception ex)
            {
                LastException = ex; // log the exception..
                return false;
            }
        }
    }
}
