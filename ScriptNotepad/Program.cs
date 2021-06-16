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

using System;
using System.Windows.Forms;
using VPKSoft.Utils;
using System.IO;
using VPKSoft.PosLib; // (C): http://www.vpksoft.net/, GNU Lesser General Public License Version 3
using VPKSoft.ErrorLogger; // (C): http://www.vpksoft.net/, GNU Lesser General Public License Version 3
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using RpcSelf;
using ScriptNotepad.DialogForms;
using VPKSoft.LangLib;
using ScriptNotepad.UtilityClasses.ExternalProcessInteraction;
using ScriptNotepad.PluginHandling;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.CodeDom;
using ScriptNotepad.UtilityClasses.ColorHelpers;
using ScriptNotepad.UtilityClasses.MiscForms;
using ScriptNotepad.UtilityClasses.Session;
using ScriptNotepad.UtilityClasses.SearchAndReplace;
using ScriptNotepad.UtilityClasses.TextManipulation.TextSorting;
using VPKSoft.Utils.XmlSettingsMisc;
using VPKSoft.WaitForProcessUtil;

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
        static void Main(string [] args)
        {
            WaitForProcess.WaitForProcessArguments(args, 30);

            if (!Debugger.IsAttached) // this is too efficient, the exceptions aren't caught by the ide :-)
            {
                ExceptionLogger.Bind(); // bind before any visual objects are created
                ExceptionLogger.ApplicationCrashData += ExceptionLogger_ApplicationCrashData;
            }

            // localizeProcess (user wishes to localize the software)..
            Process localizeProcess = Utils.CreateDBLocalizeProcess(Paths.AppInstallDir);

            // if the localize process was requested via the command line..
            if (localizeProcess != null)
            {
                // start the DBLocalization.exe and return..
                localizeProcess.Start();

                ExceptionLogger.LogMessage("Started localize process..");

                if (!Debugger.IsAttached)
                {
                    ExceptionLogger.UnBind(); // unbind so the truncate thread is stopped successfully..
                }
                return;
            }

            Paths.MakeAppSettingsFolder(Misc.AppType.Winforms); // ensure there is an application settings folder..

            // Save languages..
            if (Utils.ShouldLocalize() != null)
            {
                // ReSharper disable once ObjectCreationAsStatement
                new FormMain();
                // ReSharper disable once ObjectCreationAsStatement
                new FormScript();
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogScriptLoad();
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogQueryEncoding();
                // ReSharper disable once ObjectCreationAsStatement
                new FormHexEdit();
                // ReSharper disable once ObjectCreationAsStatement
                new FormSettings();
                // ReSharper disable once ObjectCreationAsStatement
                new FormPluginManage();
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogSessionManage();
                // ReSharper disable once ObjectCreationAsStatement
                new FormSearchResultTree();
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogSearchReplaceProgress();
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogSearchReplaceProgressFiles();
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogQueryNumber();
                // ReSharper disable once ObjectCreationAsStatement
                new FormPickAColor();
                // ReSharper disable once ObjectCreationAsStatement
                new FormFileDiffView();
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogQueryJumpLocation();
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogSelectFileTab();
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogRenameNewFile();
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogQuerySortTextStyle();
                // ReSharper disable once ObjectCreationAsStatement
                new FormDialogCustomSpellCheckerInfo();
                // this form is instantiated in a different way..
                _ = FormSnippetRunner.Instance;

                FormSearchAndReplace.CreateLocalizationInstance(); // special form, special handling..

                if (!Debugger.IsAttached)
                {
                    ExceptionLogger.UnBind(); // unbind so the truncate thread is stopped successfully..
                }

                ExceptionLogger.ApplicationCrashData -= ExceptionLogger_ApplicationCrashData;
                return;
            }

            // if the application is running send the arguments to the existing instance..
            if (AppRunning.CheckIfRunning("VPKSoft.ScriptNotepad.C#"))
            {
                ExceptionLogger.LogMessage($"Application is running. Checking for open file requests. The current directory is: '{Environment.CurrentDirectory}'.");
                ExceptionLogger.LogMessage($"The arguments are: '{string.Join("', '", args)}'.");
                try
                {
                    RpcSelfClient<string> ipcClient = new RpcSelfClient<string>(50670);

                    // only send the existing files to the running instance..
                    foreach (var arg in args)
                    {
                        if (arg == Process.GetCurrentProcess().MainModule?.FileName)
                        {
                            // don't open your self..
                            continue;
                        }
                        string file = arg;
                        
                        ExceptionLogger.LogMessage($"Request file open: '{file}'.");
                        if (File.Exists(file))
                        {
                            ExceptionLogger.LogMessage($"File exists: '{file}'. Send open request.");
                            ipcClient.SendData(file);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogError(ex);
                    // just in case something fails with the IPC communication..
                }

                if (!Debugger.IsAttached)
                {
                    ExceptionLogger.UnBind(); // unbind so the truncate thread is stopped successfully..
                }

                ExceptionLogger.ApplicationCrashData -= ExceptionLogger_ApplicationCrashData;
                return;
            }

            PositionCore.Bind(ApplicationType.WinForms); // attach the PosLib to the application            

            SettingFileName = PathHandler.GetSettingsFile(Assembly.GetEntryAssembly(), ".xml",
                Environment.SpecialFolder.LocalApplicationData);

            // create a Settings class instance for the settings form..
            FormSettings.Settings = new Settings.Settings();

            FormSettings.Settings.RequestTypeConverter += settings_RequestTypeConverter;

            if (!File.Exists(SettingFileName))
            {
                using var settingsOld = new SettingsOld();
                settingsOld.MoveSettings(FormSettings.Settings);
                FormSettings.Settings.Save(SettingFileName);
            }
            else
            {
                FormSettings.Settings.DatabaseMigrationLevel = 1;
            }

            FormSettings.Settings.Load(SettingFileName);

            DBLangEngine.UseCulture = FormSettings.Settings.Culture; // set the localization value..

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            PositionCore.UnBind(ApplicationType.WinForms); // release the event handlers used by the PosLib and save the default data

            if (!Debugger.IsAttached)
            {
                ExceptionLogger.UnBind(); // unbind so the truncate thread is stopped successfully..
            }

            ExceptionLogger.ApplicationCrashData -= ExceptionLogger_ApplicationCrashData;

            if (RestartElevated && File.Exists(ElevateFile) && !Restart)
            {
                ApplicationProcess.RunApplicationProcess(true, ElevateFile);
            }

            if (Restart)
            {
                ApplicationProcess.RunApplicationProcess(false, string.Empty);
            }
        }

        private static void settings_RequestTypeConverter(object sender, RequestTypeConverterEventArgs e)
        {
            if (e.TypeToConvert == typeof(Color))
            {
                e.TypeConverter = new ColorConverter();
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
                // ignored, no point of return..
            }

            // unsubscribe this event handler..
            ExceptionLogger.ApplicationCrashData -= ExceptionLogger_ApplicationCrashData;

            if (!Debugger.IsAttached)
            {
                ExceptionLogger.UnBind(); // unbind the exception logger..
            }

            // kill self as the native inter-op libraries may have some issues of keeping the process alive..
            Process.GetCurrentProcess().Kill();

            // This is the end..
        }

        /// <summary>
        /// Gets or sets a value indicating whether to restart the software upon closing it.
        /// </summary>
        internal static bool Restart { get; set; } = false;

        /// <summary>
        /// Gets or sets the name of the setting file.
        /// </summary>
        /// <value>The name of the setting file.</value>
        internal static string SettingFileName { get; set; }

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
