using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.ExternalProcessInteraction
{
    /// <summary>
    /// A class to run the current application with parameters.
    /// </summary>
    public static class ApplicationProcess
    {
        public static bool RunApplicationProcess(bool elevated, string arguments)
        {
            try
            {
                var processStartInfo = new ProcessStartInfo()
                {
                    FileName = Application.ExecutablePath,
                    //UseShellExecute = false,
                    LoadUserProfile = true,
                    Verb = elevated ? "runas" : null,
                    Arguments = arguments
                };

                Process.Start(processStartInfo);

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
