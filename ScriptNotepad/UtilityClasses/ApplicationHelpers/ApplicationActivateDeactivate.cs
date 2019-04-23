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
using System.Windows.Forms;


// (C)::https://www.cyotek.com/blog/how-to-be-notified-when-your-application-is-activated-and-deactivated
namespace ScriptNotepad.UtilityClasses.ApplicationHelpers
{
    /// <summary>
    /// A class containing helper methods to listen to the WM_ACTIVATEAPP WndProc message.
    /// </summary>
    public static class ApplicationActivateDeactivate
    {
        /// <summary>
        /// Occurs when the application was activated.
        /// </summary>
        public static event EventHandler ApplicationActivated;

        /// <summary>
        /// Occurs when application was deactivated.
        /// </summary>
        public static event EventHandler ApplicationDeactivated;

        /// <summary>
        /// A message send to the WndProc when the application activates / deactivates.
        /// </summary>
        public const int WM_ACTIVATEAPP = 0x1C;

        /// <summary>
        /// A method which can be called from within a overridden <see cref="Form.WndProc"/> method to raise the <see cref="ApplicationActivated"/> and the <see cref="ApplicationDeactivated"/> events.
        /// </summary>
        /// <param name="form">The form from which WndProc method this method was called.</param>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        /// <returns><c>true</c> if the <see cref="m"/> was handled by the method, <c>false</c> otherwise.</returns>
        public static bool WndProcApplicationActivateHelper(Form form, ref Message m)
        {
            if (m.Msg == WM_ACTIVATEAPP)
            {
                if (m.WParam != IntPtr.Zero)
                {
                    // the application is getting activated..
                    ApplicationActivated?.Invoke(form, new EventArgs());
                }
                else
                {
                    // the application is getting deactivated..
                    ApplicationDeactivated?.Invoke(form, new EventArgs());
                }

                return true;
            }

            return false;
        }
    }
}
