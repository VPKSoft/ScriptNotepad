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

using ScintillaNET_FindReplaceDialog;
using System;
using System.Windows.Forms;
using VPKSoft.Utils;
using VPKSoft.IPC;
using System.IO;
using VPKSoft.PosLib; // (C): http://www.vpksoft.net/, GNU Lesser General Public License Version 3
using VPKSoft.ErrorLogger; // (C): http://www.vpksoft.net/, GNU Lesser General Public License Version 3
using System.Diagnostics;
using ScriptNotepad.DialogForms;
using VPKSoft.LangLib;
using ScriptNotepad.UtilityClasses.ExternalProcessInteraction;
using ScriptNotepad.PluginHandling;
using ScriptNotepad.UtilityClasses.CodeDom;
using ScriptNotepad.UtilityClasses.Session;
using ScriptNotepad.UtilityClasses.SearchAndReplace;

// limit the PropertyChanged to the Settings class (https://github.com/Fody/PropertyChanged)
[assembly: PropertyChanged.FilterType("ScriptNotepad.Settings.")] 
namespace ScriptNotepad
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // localizeProcess (user wishes to localize the software)..
            Process localizeProcess = VPKSoft.LangLib.Utils.CreateDBLocalizeProcess(Paths.AppInstallDir);

            // if the localize process was requested via the command line..
            if (localizeProcess != null)
            {
                // start the DBLocalization.exe and return..
                localizeProcess.Start();
                return;
            }

            Paths.MakeAppSettingsFolder(); // ensure there is an application settings folder..

            ExceptionLogger.Bind(); // bind before any visual objects are created
            ExceptionLogger.ApplicationCrashData += ExceptionLogger_ApplicationCrashData;

            // if the application is running send the arguments to the existing instance..
            if (AppRunning.CheckIfRunning("VPKSoft.ScriptNotepad.C#"))
            {
                ExceptionLogger.LogMessage($"Application is running. Checking for open file requests. The current directory is: '{Environment.CurrentDirectory}'.");
                try
                {
                    IpcClientServer ipcClient = new IpcClientServer();
                    ipcClient.CreateClient("localhost", 50670);
                    string[] args = Environment.GetCommandLineArgs();

                    // only send the existing files to the running instance, don't send the executable's
                    // file name thus the start from 1..
                    for (int i = 1; i < args.Length; i++)
                    {
                        string file = args[i];
                        
                        ExceptionLogger.LogMessage($"Request file open: '{file}'.");
                        if (File.Exists(file))
                        {
                            ExceptionLogger.LogMessage($"File exists: '{file}'. Send open request.");
                            ipcClient.SendMessage(file);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogError(ex);
                    // just in case something fails with the IPC communication..
                }
                ExceptionLogger.UnBind(); // unbind so the truncate thread is stopped successfully..
                ExceptionLogger.ApplicationCrashData -= ExceptionLogger_ApplicationCrashData;
                return;
            }

            // Save languages..
            if (VPKSoft.LangLib.Utils.ShouldLocalize() != null)
            {
                new FormMain();
                new FormScript();
                new FormDialogScriptLoad();
                new FormDialogQueryEncoding();
                new FormHexEdit();
                new Settings.FormSettings();
                new FormPluginManage();
                new FormDialogSessionManage();
                FormSearchAndReplace.CreateLocalizationInstance(); // special form, special handling..
                ExceptionLogger.UnBind(); // unbind so the truncate thread is stopped successfully..
                ExceptionLogger.ApplicationCrashData -= ExceptionLogger_ApplicationCrashData;
                return;
            }

            PositionCore.Bind(); // attach the PosLib to the application            

            // create a Settings class instance for the settings form..
            Settings.FormSettings.Settings = new Settings.Settings();

            // localize the ScintillaNET_FindReplaceDialog..
            LocalizationSetting.Locale = Settings.FormSettings.Settings.Culture.Name.Split('-')[0];

            DBLangEngine.UseCulture = Settings.FormSettings.Settings.Culture; // set the localization value..

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            PositionCore.UnBind(); // release the event handlers used by the PosLib and save the default data
            ExceptionLogger.UnBind(); // unbind so the truncate thread is stopped successfully..
            ExceptionLogger.ApplicationCrashData -= ExceptionLogger_ApplicationCrashData;

            if (RestartElevated && File.Exists(ElevateFile))
            {
                ApplicationProcess.RunApplicationProcess(true, ElevateFile);
            }
        }

        private static void ExceptionLogger_ApplicationCrashData(ApplicationCrashEventArgs e)
        {
            try
            {
                FormMain.ModuleExceptionHandler?.Invoke(e.Exception.TargetSite.Module.FullyQualifiedName);
            }
            catch
            {

            }

            // unsubscribe this event handler..
            ExceptionLogger.ApplicationCrashData -= ExceptionLogger_ApplicationCrashData;
            ExceptionLogger.UnBind(); // unbind the exception logger..

            // kill self as the native inter-op libraries may have some issues of keeping the process alive..
            Process.GetCurrentProcess().Kill();

            // This is the end..
        }

        #region InternalProperties        
        /// <summary>
        /// Gets or sets a value indicating whether the software should be run as elevated (Administrator) after closing self.
        /// </summary>
        internal static bool RestartElevated { get; set; } = false;

        /// <summary>
        /// Gets or sets the file name which should be opened in an elevated mode (Administrator).
        /// </summary>
        /// <value>
        /// The elevate file.
        /// </value>
        internal static string ElevateFile { get; set; } = "";
        #endregion
    }
}
