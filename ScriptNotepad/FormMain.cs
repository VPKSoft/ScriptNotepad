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

using ScintillaNET; // (C)::https://github.com/jacobslusser/ScintillaNET
using ScintillaNET_FindReplaceDialog;
using ScintillaPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPKSoft.LangLib;
using VPKSoft.ScintillaLexers;
using VPKSoft.IPC;
using Microsoft.Win32;
using VPKSoft.PosLib;
using ScriptNotepad.UtilityClasses.CodeDom;
using ScriptNotepad.Database;

namespace ScriptNotepad
{
    public partial class FormMain : DBLangEngineWinforms
    {
        private FindReplace findReplace = new FindReplace();

        IpcClientServer ipcServer = new IpcClientServer();

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        /// <exception cref="Exception">Thrown if the database script isn't successfully run.</exception>
        public FormMain()
        {
            // Add this form to be positioned..
            PositionForms.Add(this, PositionCore.SizeChangeMode.MoveTopLeft);

            PositionCore.Bind();

            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages");


            SystemEvents.SessionEnded += SystemEvents_SessionEnded;

            IpcClientServer.RemoteMessage.MessageReceived += RemoteMessage_MessageReceived;

            ipcServer.CreateServer("localhost", 50670);

            // run the script to keep the database up to date..
            if (!ScriptRunner.RunScript(Path.Combine(DBLangEngine.DataDir, "ScriptNotepad.sqlite"),
                Path.Combine(VPKSoft.Utils.Paths.AppInstallDir, "DatabaseScript", "script.sql_script")))
            {
                MessageBox.Show(
                    DBLangEngine.GetMessage("msgErrorInScript",
                    "A script error occurred on the database update|Something failed during running the database update script"),
                    DBLangEngine.GetMessage("msgError", "Error|A message describing that some kind of error occurred."),
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                // at this point there is no reason to continue the program's execution as the database might be in an invalid state..
                throw new Exception(DBLangEngine.GetMessage("msgErrorInScript",
                    "A script error occurred on the database update|Something failed during running the database update script"));
            }

            Database.Database.InitConnection("Data Source=" + DBLangEngine.DataDir + "ScriptNotepad.sqlite;Pooling=true;FailIfMissing=false;Cache Size=10000;"); // PRAGMA synchronous=OFF;PRAGMA journal_mode=OFF
            Localization.StaticLocalizeFileDialog.InitFileDialog(odAnyFile);

        }


        private void RemoteMessage_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate { sttcMain.AddDocument(e.Message, -1); }));
        }

        private void button1_Click(object sender, EventArgs e)
        {

            RECENT_FILES r = Database.Database.GetRecentFiles().First();
            Database.Database.UpdateRecentFile(r);
            return;


            DBFILE_SAVE fileSave = Database.Database.AddFile(sttcMain.Documents[0], sttcMain.Documents[0].FileTabButton.IsActive, false);
            Database.Database.UpdateFile(fileSave);
            return;

            string text = sttcMain.Documents[0].Scintilla.Text;

            List<string> lines = sttcMain.Documents[0].Scintilla.Lines.Select(f => f.Text).ToList();

            //CSCodeDomeScriptRunnerText runner = new CSCodeDomeScriptRunnerText();
            CSCodeDomeScriptRunnerLines runner = new CSCodeDomeScriptRunnerLines();
            //            runner.ExecuteText(ref text);
//            MessageBox.Show(runner.ExecuteText(text));
            MessageBox.Show(runner.ExecuteLines(lines));



            return;
            

            sttcMain.AddDocument(@"C:\Users\Petteri Kautonen\Documents\Visual Studio 2013\Projects\SunMoonCalendar\releasing.bat", -1);
            sttcMain.AddDocument(@"C:\Users\Petteri Kautonen\Documents\Visual Studio 2013\Projects\ScriptNotepad\ScriptNotepad\DatabaseScript\script.sql_script", -1);
                        sttcMain.AddDocument(@"C:\Users\Petteri Kautonen\Documents\Visual Studio 2013\Projects\ScriptNotepad\ScriptNotepad\UtilityClasses\CodeDomScriptRunner.cs", -1);
                        sttcMain.AddDocument(@"C:\Users\Petteri Kautonen\Documents\Visual Studio 2013\Projects\ScriptNotepad\ScriptNotepad\FormMain.cs", -1);
                        sttcMain.AddDocument(@"C:\Users\Petteri Kautonen\Documents\Visual Studio 2013\Projects\ScriptNotepad\ScriptNotepad\FormMain.Designer.cs", -1);
                        sttcMain.AddDocument(@"C:\Users\Petteri Kautonen\Documents\Visual Studio 2013\Projects\ScriptNotepad\ScriptNotepad\Program.cs", -1);
                        sttcMain.AddDocument(@"C:\Users\Petteri Kautonen\Documents\Visual Studio 2013\Projects\GIT\VPKSoft.VisualComponents\VideoBrowser.cs", -1);
                        sttcMain.AddDocument(@"C:\Program Files (x86)\Notepad++\langs.model.xml", -1);
                        sttcMain.AddDocument(@"F:\Source\SciTE\scintilla\lexers\LexCPP.cxx", -1);
            sttcMain.AddDocument(@"C:\Files\GitHub\amp\Installer\setup_ampsharp.nsi", -1);
//            ScintillaLexers.LexerColors.DescribeLexerColors(LexerType.Cs).Save(@"F:\colorTest.xml");
            ScintillaLexers.LexerColors.LoadDescribedLexerColorsFromXml(@"F:\colorTest.xml", LexerType.Cs);

            /*
            Scintilla scintilla = new Scintilla();
            */

            
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            sttcMain.AddNewDocument();
        }

        private void mnuFind_Click(object sender, EventArgs e)
        {
            if (sttcMain.CurrentDocument != null)
            {
                findReplace.Scintilla = sttcMain.CurrentDocument.Scintilla;
                findReplace.ShowFind();
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Printing printer = new Printing(sttcMain.Documents[0].Scintilla);



            //            printer.PageSettings = new ScintillaNetPrinting.PageSettings() { ColorMode = ScintillaNePrinting.PageSettings.PrintColorMode.BlackOnWhite };

            //            printer.Print();

            //            or


            //printer.Print();
            printer.PrintPreview();

        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            IpcClientServer.RemoteMessage.MessageReceived -= RemoteMessage_MessageReceived;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            // open the files given as arguments for the program..
            OpenArgumentFiles();
        }

        /// <summary>
        /// Opens files given as arguments for the software.
        /// </summary>
        private void OpenArgumentFiles()
        {
            string[] args = Environment.GetCommandLineArgs();

            // only send the existing files to the running instance, don't send the executable's
            // file name thus the start from 1..
            for (int i = 1; i < args.Length; i++)
            {
                // a file must exist..
                if (File.Exists(args[i]))
                {
                    // add the file to the document control..
                    sttcMain.AddDocument(args[i], -1);
                }
            }
        }

        private static void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            if (odAnyFile.ShowDialog() == DialogResult.OK)
            {
                sttcMain.AddDocument(odAnyFile.FileName, -1);
            }
        }
    }
}
