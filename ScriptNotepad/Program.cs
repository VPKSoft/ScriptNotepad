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

namespace ScriptNotepad
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // if the application is running send the arguments to the existing instance..
            if (AppRunning.CheckIfRunning("VPKSoft.ScriptNotepad.C#"))
            {
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
                        if (File.Exists(file))
                        {
                            ipcClient.SendMessage(file);
                        }
                    }
                }
                catch
                {
                    // just in case something fails with the IPC communication..
                }
                return;
            }

            ExceptionLogger.Bind(); // bind before any visual objects are created
            ExceptionLogger.ApplicationCrash += ExceptionLogger_ApplicationCrash;


            Paths.MakeAppSettingsFolder(); // ensure there is an application settings folder..

            // Save languages..
            if (VPKSoft.LangLib.Utils.ShouldLocalize() != null)
            {
                new FormMain();
                new FormScript();
                new FormDialogScriptLoad();
                ExceptionLogger.UnBind(); // unbind so the truncate thread is stopped successfully..
                ExceptionLogger.ApplicationCrash -= ExceptionLogger_ApplicationCrash;
                return;
            }

            PositionCore.Bind(); // attach the PosLib to the application
            LocalizationSetting.Locale = "fi";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            PositionCore.UnBind(); // release the event handlers used by the PosLib and save the default data
            ExceptionLogger.UnBind(); // unbind so the truncate thread is stopped successfully..
            ExceptionLogger.ApplicationCrash -= ExceptionLogger_ApplicationCrash;
        }

        // as the application is about to crash do some cleanup..
        private static void ExceptionLogger_ApplicationCrash()
        {
            // unsubscribe this event handler..
            ExceptionLogger.ApplicationCrash -= ExceptionLogger_ApplicationCrash;
            ExceptionLogger.UnBind(); // unbind the exception logger..

            // kill self as the native inter-op libraries may have some issues of keeping the process alive..
            Process.GetCurrentProcess().Kill();

            // This is the end..
        }
    }
}
