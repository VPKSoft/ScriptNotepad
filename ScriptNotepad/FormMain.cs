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
using VPKSoft.ScintillaTabbedTextControl;
using ScriptNotepad.UtilityClasses;

namespace ScriptNotepad
{
    public partial class FormMain : DBLangEngineWinforms
    {
        private FindReplace findReplace = new FindReplace();

        IpcClientServer ipcServer = new IpcClientServer();

        private string CurrentSession { get; set; } = "Default";

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        /// <exception cref="Exception">Thrown if the database script isn't successfully run.</exception>
        public FormMain()
        {
            // Add this form to be positioned..
            PositionForms.Add(this, PositionCore.SizeChangeMode.MoveTopLeft);

            // add positioning..
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

            // subscribe to the session ended event to save the documents without asking stupid questions..
            SystemEvents.SessionEnded += SystemEvents_SessionEnded;

            // subscribe to the IPC event if the application receives a message from another instance of this application..
            IpcClientServer.RemoteMessage.MessageReceived += RemoteMessage_MessageReceived;

            // create an IPC server at localhost, the port was randomized in the development phase..
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

            // initialize a connection to the SQLite database..
            Database.Database.InitConnection("Data Source=" + DBLangEngine.DataDir + "ScriptNotepad.sqlite;Pooling=true;FailIfMissing=false;Cache Size=10000;"); // PRAGMA synchronous=OFF;PRAGMA journal_mode=OFF

            // localize the open file dialog..
            Localization.StaticLocalizeFileDialog.InitFileDialog(odAnyFile);

            // load the recent documents which were saved during the program close..
            LoadDocumentsFromDatabase(CurrentSession, false);
        }

        /// <summary>
        /// Checks if an open document has been changed in the file system and queries if the user wishes to reload it's contents from the file system.
        /// </summary>
        private void CheckFileSysChanges()
        {
            foreach (ScintillaTabbedDocument document in sttcMain.Documents)
            {
                if (File.Exists(document.FileName))
                {
                    DBFILE_SAVE fileSave = (DBFILE_SAVE)document.Tag;
                    DateTime dtUpdated = new FileInfo(fileSave.FILENAME_FULL).LastWriteTime;


                    if (fileSave.ShouldQueryDiskReload)
                    {
                        if (MessageBox.Show(
                            DBLangEngine.GetMessage("msgFileHasChanged", "The file '{0}' has been changed. Reload from the file system?|As in the opened file has been changed outside the software so do as if a reload should happed", fileSave.FILENAME_FULL),
                            DBLangEngine.GetMessage("msgFileArbitraryFileChange", "A file has been changed|A caption message for a message dialog which will ask if a changed file should be reloaded"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            sttcMain.SuspendTextChangedEvents = true;
                            fileSave.ReloadFromDisk(document);
                            sttcMain.SuspendTextChangedEvents = false;
                            document.Tag = fileSave;
                            //                            OpenDocument(fileSave.FILENAME_FULL);
                        }
                        else
                        {
                            fileSave.ShouldQueryDiskReload = false;
                        }
                    }
                    fileSave.DB_MODIFIED = dtUpdated;
                }
            }
        }

        // this event is raised when another instance of this application receives a file name
        // via the IPC (no multiple instance allowed)..
        private void RemoteMessage_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate { OpenDocument(e.Message); }));
        }

        // a user wanted to create a new file..
        private void tsbNew_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        // a user wanted to find or find and replace something of the active document..
        private void mnuFind_Click(object sender, EventArgs e)
        {
            if (sttcMain.CurrentDocument != null)
            {
                findReplace.Scintilla = sttcMain.CurrentDocument.Scintilla;
                findReplace.ShowFind();
            }
        }

        // a test menu item for running "absurd" tests with the software..
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Printing printer = new Printing(sttcMain.Documents[0].Scintilla);



            //            printer.PageSettings = new ScintillaNetPrinting.PageSettings() { ColorMode = ScintillaNePrinting.PageSettings.PrintColorMode.BlackOnWhite };

            //            printer.Print();

            //            or


            //printer.Print();
            printer.PrintPreview();
        }

        // if the form is closing, save the snapshots of the open documents to the SQLite database..
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            IpcClientServer.RemoteMessage.MessageReceived -= RemoteMessage_MessageReceived;

            SaveDocumentsToDatabase(CurrentSession, true);
        }

        /// <summary>
        /// Saves the active document snapshots in to the SQLite database.
        /// </summary>
        /// <param name="dispose">An indicator if the underlying MemoryStream should be disposed of.</param>
        /// <param name="sessionName">A name of the session to which the documents should be tagged with.</param>
        private void SaveDocumentsToDatabase(string sessionName, bool dispose)
        {
            for (int i = 0; i < sttcMain.DocumentsCount; i++)
            {
                DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.Documents[i].Tag;
                fileSave.ISACTIVE = sttcMain.Documents[i].FileTabButton.IsActive;
                fileSave.VISIBILITY_ORDER = i;
                Database.Database.AddOrUpdateFile(fileSave, sttcMain.Documents[i]);
                Database.Database.AddOrUpdateRecentFile(sttcMain.Documents[i].FileName, sessionName);

                if (dispose)
                {
                    fileSave.DisposeMemoryStream();
                }
            }
        }

        /// <summary>
        /// Loads the document snapshots from the SQLite database.
        /// </summary>
        /// <param name="sessionName">A name of the session to which the documents are tagged with.</param>
        /// <param name="history">An indicator if the documents should be closed ones. I.e. not existing with the current session.</param>
        private void LoadDocumentsFromDatabase(string sessionName, bool history)
        {
            IEnumerable<DBFILE_SAVE> files = Database.Database.GetFilesFromDatabase(sessionName, history);

            string activeDocument = string.Empty;

            foreach (DBFILE_SAVE file in files)
            {
                if (file.ISACTIVE)
                {
                    activeDocument = file.FILENAME_FULL;
                }
                sttcMain.AddDocument(file.FILENAME_FULL, (int)file.ID, file.FILE_CONTENTS);
                if (sttcMain.LastAddedDocument != null)
                {
                    sttcMain.LastAddedDocument.Tag = file;
                }
            }

            if (activeDocument != string.Empty)
            {
                sttcMain.ActivateDocument(activeDocument);
            }

        }

        // the form is shown..
        private void FormMain_Shown(object sender, EventArgs e)
        {
            // ..so open the files given as arguments for the program..
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
                    OpenDocument(args[i]);
                }
            }
        }

        // a user is logging of or the system is shutting down..
        private void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
        {
            // ..just no questions asked save the document snapshots into the SQLite database..
            SaveDocumentsToDatabase(CurrentSession, true);
        }

        /// <summary>
        /// Adds a new document in to the view.
        /// </summary>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        private bool NewDocument()
        {
            // a false would happen if the document (file) can not be accessed or required permissions to access a file
            // would be missing (also a bug might occur)..
            if (sttcMain.AddNewDocument())
            {
                if (sttcMain.CurrentDocument != null) // if the document was added or updated to the control..
                {
                    sttcMain.CurrentDocument.Tag =
                        new DBFILE_SAVE()
                        {
                            FILENAME = sttcMain.CurrentDocument.FileName,
                            FILENAME_FULL = sttcMain.CurrentDocument.FileName,
                            FILEPATH = string.Empty,
                            FILE_CONTENTS = new MemoryStream()
                        };

                    DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;

                    // save the DBFILE_SAVE class instance to the Tag property..
                    sttcMain.CurrentDocument.Tag = Database.Database.AddOrUpdateFile(fileSave, sttcMain.CurrentDocument);
                    return true;
                }
                else
                {
                    // fail with the current document being null..
                    return false;
                }
            }
            else
            {
                // fail with the ScintillaTabbedTextControl returning an error..
                return false;
            }
        }

        /// <summary>
        /// Opens the document with a given file name into the view.
        /// </summary>
        /// <param name="fileName">Name of the file to load into the view.</param>
        /// <param name="reloadContents">An indicator if the contents of the document should be reloaded from the file system.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        private bool OpenDocument(string fileName, bool reloadContents = false) // TODO::!!
        {
            if (File.Exists(fileName))
            {
                // a false would happen if the document (file) can not be accessed or required permissions to access a file
                // would be missing (also a bug might occur)..
                if (sttcMain.AddDocument(fileName, -1))
                {
                    if (sttcMain.CurrentDocument != null) // if the document was added or updated to the control..
                    {
                        if (sttcMain.CurrentDocument.Tag == null)
                        {
                            sttcMain.CurrentDocument.Tag = Database.Database.AddOrUpdateFile(sttcMain.CurrentDocument, false, CurrentSession);
                        }
                        DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;

                        // ..update the database with the document..
                        Database.Database.AddOrUpdateFile(fileSave, sttcMain.CurrentDocument);

                        if (reloadContents)
                        {
                            fileSave.ReloadFromDisk(sttcMain.CurrentDocument);
                        }

                        // save the DBFILE_SAVE class instance to the Tag property..
                        sttcMain.CurrentDocument.Tag = Database.Database.AddOrUpdateFile(sttcMain.CurrentDocument, false, CurrentSession);
                        return true;
                    }
                    else
                    {
                        // fail with the current document being null..
                        return false;
                    }
                }
                else
                {
                    // fail with the ScintillaTabbedTextControl returning an error..
                    return false;
                }
            }
            else
            {
                // fail as the file does not exist..
                return false;
            }
        }

        /// <summary>
        /// Opens the document with a given DBFILE_SAVE document snapshot into the view.
        /// </summary>
        /// <param name="fileSave">An instance to a DBFILE_SAVE class containing a snapshot of the document's contents to be loaded into the view.</param>
        /// <param name="document">An instance to a ScintillaTabbedDocument class for the tabbed document view control.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        private bool OpenDocument(DBFILE_SAVE fileSave, ScintillaTabbedDocument document)
        {
            if (File.Exists(fileSave.FILENAME_FULL))
            {
                // a false would happen if the document (file) can not be accessed or required permissions to access a file
                // would be missing (also a bug might occur)..
                if (sttcMain.AddDocument(fileSave.FILENAME_FULL, -1))
                {
                    if (sttcMain.CurrentDocument != null) // if the document was added or updated to the control..
                    {
                        // ..update the database with the document..
                        Database.Database.AddOrUpdateFile(fileSave, document);

                        // save the DBFILE_SAVE class instance to the Tag property..
                        sttcMain.CurrentDocument.Tag = Database.Database.AddOrUpdateFile(sttcMain.CurrentDocument, false, CurrentSession);
                        return true;
                    }
                    else
                    {
                        // fail with the current document being null..
                        return false;
                    }
                }
                else
                {
                    // fail with the ScintillaTabbedTextControl returning an error..
                    return false;
                }
            }
            else
            {
                // fail as the file does not exist..
                return false;
            }
        }

        // a user wanted to open a file via the main menu..
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            // if the file dialog was accepted (i.e. OK) then open the file to the view..
            if (odAnyFile.ShowDialog() == DialogResult.OK)
            {
                OpenDocument(odAnyFile.FileName);
            }
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            CheckFileSysChanges();
        }

        private void sttcMain_TabClosing(object sender, TabClosingEventArgsExt e)
        {
            DBFILE_SAVE fileSave = (DBFILE_SAVE)e.ScintillaTabbedDocument.Tag;
            fileSave.ISHISTORY = true;
            Database.Database.AddOrUpdateFile(fileSave, e.ScintillaTabbedDocument);
        }

        // a user activated a tab (document) so display it's file name..
        private void sttcMain_TabActivated(object sender, TabActivatedEventArgs e)
        {
            Text =
                DBLangEngine.GetMessage("msgAppTitleWithFileName",
                "ScriptNotepad [{0}]|As in the application name combined with an active file name",
                e.ScintillaTabbedDocument.FileName);

            SetStatusStringText(e.ScintillaTabbedDocument);
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            new VPKSoft.About.FormAbout(this, "MIT", "https://raw.githubusercontent.com/VPKSoft/ScriptNotepad/master/LICENSE");
        }

        private void sttcMain_SelectionCaretChanged(object sender, ScintillaTabbedDocumentEventArgsExt e)
        {
            SetStatusStringText(e.ScintillaTabbedDocument);
        }

        private void SetStatusStringText(ScintillaTabbedDocument document)
        {
            ssLbLineColumn.Text =
                DBLangEngine.GetMessage("msgColLine", "Line: {0}  Col: {1}|As in the current column and the current line in a ScintillaNET control",
                document.LineNumber + 1, document.Column + 1);

            ssLbLinesColumnSelection.Text =
                DBLangEngine.GetMessage("msgColLineSelection", "Sel1: {0}|{1}  Sel2: {2}|{3}  Len: {4}|The selection start, end and length in a ScintillaNET control in columns, lines and character count",
                document.SelectionStartLine + 1,
                document.SelectionStartColumn + 1,
                document.SelectionEndLine + 1,
                document.SelectionEndColumn + 1,
                document.SelectionLength);
        }
    }
}
