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
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.ExternalProcessInteraction
{
    /// <summary>
    /// A class to run the current application with parameters.
    /// </summary>
    public static class ApplicationProcess
    {
        /// <summary>
        /// Executes a new instance of the current application with possible elevated permissions.
        /// </summary>
        /// <param name="elevated">If set to true try to run the process as administrator.</param>
        /// <param name="arguments">The arguments to be passed to the process.</param>
        /// <returns></returns>
        public static bool RunApplicationProcess(bool elevated, string arguments)
        {
            try
            {
                var processStartInfo = new ProcessStartInfo()
                {
                    FileName = Application.ExecutablePath,
                    LoadUserProfile = true,
                    Verb = elevated ? "runas" : null, // process elevation..
                    Arguments = arguments
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
        /// Gets or sets the action to be used to log an exception.
        /// </summary>
        public static Action<Exception> ExceptionLogAction { get; set; } = null;
    }
}
