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
using VPKSoft.ErrorLogger;
using ScriptNotepad.UtilityClasses.CodeDom;
using ScriptNotepad.Database;
using VPKSoft.ScintillaTabbedTextControl;
using ScriptNotepad.UtilityClasses;
using ScriptNotepad.UtilityClasses.StreamHelpers;
using ScriptNotepad.UtilityClasses.Encoding.CharacterSets;
using ScriptNotepad.DialogForms;

namespace ScriptNotepad
{
    public partial class FormMain : DBLangEngineWinforms
    {
        #region PrivateFields
        private FindReplace findReplace = new FindReplace();
        IpcClientServer ipcServer = new IpcClientServer();
        #endregion

        #region PrivateProperties
        private string CurrentSession { get; set; } = "Default";
        #endregion

        #region MassiveConstructor
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

            // localize the save file dialog..
            Localization.StaticLocalizeFileDialog.InitFileDialog(sdAnyFile);

            // localize the open and save file dialog titles..
            sdAnyFile.Title = DBLangEngine.GetMessage("msgSaveFileAs", "Save As|A title for a save file as dialog");
            odAnyFile.Title = DBLangEngine.GetMessage("msgOpenFile", "Open|A title for a open file dialog");

            // load the recent documents which were saved during the program close..
            LoadDocumentsFromDatabase(CurrentSession, false);

            // localize some other class properties, etc..
            FormLocalizationHelper.LocalizeMisc();

            CharacterSetMenuBuilder.CreateCharacterSetMenu(mnuCharSets, false, "convert_encoding");
            CharacterSetMenuBuilder.EncodingMenuClicked += CharacterSetMenuBuilder_EncodingMenuClicked;
        }
        #endregion

        #region HelperMethods
        /// <summary>
        /// Sets the main status strip values for the currently active document..
        /// </summary>
        /// <param name="document"></param>
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

            ssLbLineEnding.Text = string.Empty;

            if (document.Tag != null) // TODO::Only detect the file line ending type if the contents have been changed..
            {
                DBFILE_SAVE fileSave = (DBFILE_SAVE)document.Tag;
                var fileLineTypes = UtilityClasses.LinesAndBinary.FileLineType.GetFileLineTypes(fileSave.FILE_CONTENTS);

                ssLbLineEnding.Text =
                    DBLangEngine.GetMessage("msgLineEndingShort", "LE: |A short message indicating a file line ending type value(s) as a concatenated text");

                string endAppend = string.Empty;

                foreach (var fileLineType in fileLineTypes)
                {
                    if (!fileLineType.Key.HasFlag(UtilityClasses.LinesAndBinary.FileLineTypes.Mixed))
                    {
                        ssLbLineEnding.Text += fileLineType.Value + ", ";
                    }
                    else
                    {
                        endAppend = $" ({fileLineType.Value})";
                    }

                    ssLbLineEnding.Text = ssLbLineEnding.Text.TrimEnd(',', ' ') + endAppend;
                }

                ssLbEncoding.Text =
                    DBLangEngine.GetMessage("msgShortEncodingPreText", "Encoding: |A short text to describe a detected encoding value (i.e.) Unicode (UTF-8).") +
                    fileSave.ENCODING.EncodingName;
            }

            ssLbInsertOverride.Text =
                document.Scintilla.Overtype ?
                    DBLangEngine.GetMessage("msgOverrideShort", "Cursor mode: OVR|As in the text to be typed to the Scintilla would override the underlying text") :
                    DBLangEngine.GetMessage("msgInsertShort", "Cursor mode: INS|As in the text to be typed to the Scintilla would be inserted within the already existing text");
        }

        /// <summary>
        /// Checks if an open document has been changed in the file system and queries if the user wishes to reload it's contents from the file system.
        /// </summary>
        private void CheckFileSysChanges()
        {
            for (int i = 0; i < sttcMain.DocumentsCount; i++)
            {
                // check if the file exists because it cannot be reloaded otherwise 
                // from the file system..
                if (File.Exists(sttcMain.Documents[i].FileName))
                {
                    // get the DBFILE_SAVE class instance from the document's tag..
                    DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.Documents[i].Tag;

                    // query the user if one wishes to reload
                    // the changed file from the disk..
                    if (fileSave.ShouldQueryDiskReload)
                    {
                        if (MessageBox.Show(
                            DBLangEngine.GetMessage("msgFileHasChanged", "The file '{0}' has been changed. Reload from the file system?|As in the opened file has been changed outside the software so do as if a reload should happed", fileSave.FILENAME_FULL),
                            DBLangEngine.GetMessage("msgFileArbitraryFileChange", "A file has been changed|A caption message for a message dialog which will ask if a changed file should be reloaded"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            // the user answered yes..
                            sttcMain.SuspendTextChangedEvents = true; // suspend the changed events on the ScintillaTabbedTextControl..
                            fileSave.ReloadFromDisk(sttcMain.Documents[i]); // reload the file..
                            sttcMain.SuspendTextChangedEvents = false; // resume the changed events on the ScintillaTabbedTextControl..

                            // just in case set the tag back..
                            sttcMain.Documents[i].Tag = fileSave;

                            // bring the form to the front..
                            BringToFront(); 
                        }
                        else // the user doesn't want to load the changes made to the document from the file system..
                        {
                            // indicate that the query shouldn't happen again..
                            fileSave.ShouldQueryDiskReload = false;

                            // set the flag that the file's modified date in the database
                            // has been changed as the user didn't wish to reload the file from the file system: FS != DB..
                            fileSave.DB_MODIFIED = DateTime.Now;

                            // just in case set the tag back..
                            sttcMain.Documents[i].Tag = fileSave;

                            // bring the form to the front..
                            BringToFront();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the indicators if the file changes have been saved to the file system.
        /// <note type="note">The logic is weird.</note>
        /// </summary>
        private void UpdateDocumentSaveIndicators()
        {
            // loop through the documents..
            foreach (ScintillaTabbedDocument document in sttcMain.Documents)
            {
                // get the file DBFILE_SAVE instance from the tag..
                DBFILE_SAVE fileSave = (DBFILE_SAVE)document.Tag;
                if (!fileSave.EXISTS_INFILESYS) // non-existing file is always changed by the software..
                {
                    document.FileTabButton.IsSaved = false; // ..so set the indicator accordingly..
                }
                // if the document exists in the file system, use different methods of detection..
                else
                {
                    document.FileTabButton.IsSaved = 
                        !DBFILE_SAVE.DateTimeLarger(fileSave.FILESYS_MODIFIED, fileSave.FILESYS_SAVED) ||
                        !DBFILE_SAVE.DateTimeLarger(fileSave.DB_MODIFIED, fileSave.FILESYS_SAVED);
                }
            }
            UpdateUndoRedoIndicators();
        }

        /// <summary>
        /// Updates the undo and redo indicator buttons and menu items.
        /// </summary>
        private void UpdateUndoRedoIndicators()
        {
            if (sttcMain.CurrentDocument != null)
            {
                // get the active tab's Scintilla document
                Scintilla scintilla = sttcMain.CurrentDocument.Scintilla;

                tsbUndo.Enabled = scintilla.CanUndo;
                tsbRedo.Enabled = scintilla.CanRedo;
            }
        }
        #endregion

        #region UselessCode
        UTF8Encoding UTF8Encoding = new UTF8Encoding(false);

        // a test menu item for running "absurd" tests with the software..
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Settings settings = new Settings.Settings();
            settings.DefaultEncoding = Encoding.UTF8;
            settings.HistoryListAmount = 20;
            return;
            MessageBox.Show(FormDialogQueryEncoding.Execute().ToString());
            return;

            sttcMain.CurrentDocument.Scintilla.Text =
                StreamStringHelpers.ConvertEncoding(Encoding.UTF8, Encoding.GetEncoding("koi8-u"), sttcMain.CurrentDocument.Scintilla.Text);
            return;
            CharacterSetMenuBuilder.DisposeCharacterSetMenu(mnuCharSets);
            return;

            DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;
            var lineEndings = UtilityClasses.LinesAndBinary.FileLineType.GetFileLineTypes(fileSave.FILE_CONTENTS);
            foreach (var lineEnding in lineEndings)
            {
                MessageBox.Show(lineEnding.Value);
            }

            return;
            new FormHexEdit().Show();
            return;
            Printing printer = new Printing(sttcMain.Documents[0].Scintilla);




            //            printer.PageSettings = new ScintillaNetPrinting.PageSettings() { ColorMode = ScintillaNePrinting.PageSettings.PrintColorMode.BlackOnWhite };

            //            printer.Print();

            //            or


            //printer.Print();
            printer.PrintPreview();
        }
        #endregion

        #region DocumentHelperMethods
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
                sttcMain.AddDocument(file.FILENAME_FULL, (int)file.ID, file.ENCODING, file.FILE_CONTENTS);
                if (sttcMain.LastAddedDocument != null)
                {
                    sttcMain.LastAddedDocument.Tag = file;
                    // the file load can't add an undo option the Scintilla..
                    sttcMain.LastAddedDocument.Scintilla.EmptyUndoBuffer();
                }                
            }

            if (activeDocument != string.Empty)
            {
                sttcMain.ActivateDocument(activeDocument);
            }

            UpdateUndoRedoIndicators();
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
                    OpenDocument(args[i], Encoding.Default);
                }
            }
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

                    // get a DBFILE_SAVE class instance from the document's tag..
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
        /// <param name="encoding">The encoding to be used to open the file.</param>
        /// <param name="reloadContents">An indicator if the contents of the document should be reloaded from the file system.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        private bool OpenDocument(string fileName, Encoding encoding, bool reloadContents = false)
        {
            // check the file's existence first..
            if (File.Exists(fileName))
            {
                // a false would happen if the document (file) can not be accessed or required permissions to access a file
                // would be missing (also a bug might occur)..
                if (sttcMain.AddDocument(fileName, -1, encoding))
                {
                    if (sttcMain.CurrentDocument != null) // if the document was added or updated to the control..
                    {
                        if (sttcMain.CurrentDocument.Tag == null)
                        {
                            sttcMain.CurrentDocument.Tag = Database.Database.AddOrUpdateFile(sttcMain.CurrentDocument, false, CurrentSession, encoding);
                        }
                        // get a DBFILE_SAVE class instance from the document's tag..
                        DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;

                        // ..update the database with the document..
                        Database.Database.AddOrUpdateFile(fileSave, sttcMain.CurrentDocument);

                        if (reloadContents)
                        {
                            fileSave.ReloadFromDisk(sttcMain.CurrentDocument);
                        }

                        // save the DBFILE_SAVE class instance to the Tag property..
                        sttcMain.CurrentDocument.Tag = Database.Database.AddOrUpdateFile(sttcMain.CurrentDocument, false, CurrentSession, fileSave.ENCODING);

                        // the file load can't add an undo option the Scintilla..
                        sttcMain.CurrentDocument.Scintilla.EmptyUndoBuffer();
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
                if (sttcMain.AddDocument(fileSave.FILENAME_FULL, -1, fileSave.ENCODING))
                {
                    if (sttcMain.CurrentDocument != null) // if the document was added or updated to the control..
                    {
                        // ..update the database with the document..
                        Database.Database.AddOrUpdateFile(fileSave, document);

                        // save the DBFILE_SAVE class instance to the Tag property..
                        sttcMain.CurrentDocument.Tag = Database.Database.AddOrUpdateFile(sttcMain.CurrentDocument, false, CurrentSession, fileSave.ENCODING);

                        // the file load can't add an undo option the Scintilla..
                        sttcMain.CurrentDocument.Scintilla.EmptyUndoBuffer();
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
        /// Saves the document in to the file system.
        /// </summary>
        /// <param name="document">The document to be saved.</param>
        /// <param name="saveAs">An indicator if the document should be saved as a new file.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        private bool SaveDocument(ScintillaTabbedDocument document, bool saveAs)
        {
            try
            {
                // check that the given parameter is valid..
                if (document != null && document.Tag != null)
                {
                    // get the DBFILE_SAVE class instance from the document's tag..
                    DBFILE_SAVE fileSave = (DBFILE_SAVE)document.Tag;

                    // set the contents to match the document's text..
                    fileSave.FILE_CONTENTS = StreamStringHelpers.TextToMemoryStream(document.Scintilla.Text, fileSave.ENCODING);

                    // only an existing file can be saved directly..
                    if (fileSave.EXISTS_INFILESYS && !saveAs)
                    {
                        // write the new contents of a file to the existing file overriding it's contents..
                        using (FileStream fileStream = new FileStream(fileSave.FILENAME_FULL, FileMode.Create, FileAccess.Write))
                        {
                            fileSave.FILE_CONTENTS.Position = 0; // position the stream..
                            fileSave.FILE_CONTENTS.WriteTo(fileStream); // write the contents of the stream to the file..
                        }

                        // update the file system modified time stamp so the software doesn't ask if the file should
                        // be reloaded from the file system..
                        fileSave.FILESYS_MODIFIED = new FileInfo(fileSave.FILENAME_FULL).LastWriteTime;
                        fileSave.FILESYS_SAVED = fileSave.FILESYS_MODIFIED;
                        fileSave.DB_MODIFIED = fileSave.FILESYS_MODIFIED;
                        document.Tag = fileSave;
                    }
                    // the file doesn't exist in the file system or the user wishes to use save as dialog so
                    // display a save file dialog..
                    else 
                    {
                        sdAnyFile.FileName = fileSave.FILENAME_FULL;
                        if (sdAnyFile.ShowDialog() == DialogResult.OK)
                        {
                            fileSave.FILESYS_MODIFIED = DateTime.Now;

                            // write the new contents of a file to the existing file overriding it's contents..
                            using (FileStream fileStream = new FileStream(sdAnyFile.FileName, FileMode.Create, FileAccess.Write))
                            {
                                fileSave.FILE_CONTENTS.Position = 0; // position the stream..
                                fileSave.FILE_CONTENTS.WriteTo(fileStream); // write the contents of the stream to the file..
                            }

                            // the file now exists in the file system..
                            fileSave.EXISTS_INFILESYS = true;

                            // the file now has a location so update it..
                            fileSave.FILENAME = Path.GetFileName(sdAnyFile.FileName);
                            fileSave.FILENAME_FULL = sdAnyFile.FileName;
                            fileSave.FILEPATH = Path.GetDirectoryName(document.FileName);

                            // update the document..
                            document.FileName = fileSave.FILENAME_FULL;
                            document.FileNameNotPath = fileSave.FILENAME;
                            document.FileTabButton.Text = fileSave.FILENAME;

                            // a new lexer might have to be assigned..
                            document.LexerType = ScintillaLexers.LexerTypeFromFileName(fileSave.FILENAME_FULL);

                            // update the file system modified time stamp so the software doesn't ask if the file should
                            // be reloaded from the file system..
                            fileSave.FILESYS_MODIFIED = new FileInfo(fileSave.FILENAME_FULL).LastWriteTime;
                            fileSave.FILESYS_SAVED = fileSave.FILESYS_MODIFIED;
                            fileSave.DB_MODIFIED = fileSave.FILESYS_MODIFIED;
                            document.Tag = fileSave;
                        }
                        else
                        {
                            // the user canceled the file save dialog..
                            return false;
                        }
                    }

                    // update the saved indicators..
                    UpdateDocumentSaveIndicators();
                    // indicate success..
                    return true;
                }
                else
                {
                    // the given parameter was invalid so fail..
                    return false;
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);

                // an exception occurred so fail..
                return false;
            }
        }

        /// <summary>
        /// Saves all open documents from the <see cref="ScintillaTabbedTextControl"/> to the file system.
        /// </summary>
        /// <param name="nonExisting">An indicator if the documents only existing in a "virtual" space should also be saved in to the file system.</param>
        private void SaveAllDocuments(bool nonExisting)
        {
            // loop through the documents..
            foreach (ScintillaTabbedDocument document in sttcMain.Documents)
            {
                // get the DBFILE_SAVE class instance from the tag..
                DBFILE_SAVE fileSave = (DBFILE_SAVE)document.Tag;
                if (fileSave.EXISTS_INFILESYS)
                {
                    // save the document..
                    SaveDocument(document, nonExisting);
                }
            }
        }
        #endregion

        #region InternalEvents
        // a user wishes to change the settings of the software..
        private void mnuSettings_Click(object sender, EventArgs e)
        {
            // ..so display the settings dialog..
            Settings.FormSettings.Execute();
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            // a user pressed a keyboard combination of CTRL+Z, which indicates undo for
            // the Scintilla control..
            if (e.KeyCode == Keys.Z && e.Control && !e.Shift && !e.Alt)
            {
                // if there is an active document..
                if (sttcMain.CurrentDocument != null)
                {
                    // ..then if the is possible..
                    if (sttcMain.CurrentDocument.Scintilla.CanUndo)
                    {
                        // get a DBFILE_SAVE class instance from the document's tag..
                        DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;

                        // undo the encoding change..
                        fileSave.UndoEncodingChange();
                    }
                }
                UpdateUndoRedoIndicators();
            }
        }

        // this event is raised when another instance of this application receives a file name
        // via the IPC (no multiple instance allowed)..
        private void RemoteMessage_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate { OpenDocument(e.Message, Encoding.Default); }));
        }

        // a user wanted to create a new file..
        private void tsbNew_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        // the user wishes to manage the script snippets in the database..
        private void mnuManageScriptSnippets_Click(object sender, EventArgs e)
        {
            // ..so display the dialog..
            FormDialogScriptLoad.Execute(true);
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

        // if the form is closing, save the snapshots of the open documents to the SQLite database..
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // unsubscribe the IpcClientServer MessageReceived event handler..
            IpcClientServer.RemoteMessage.MessageReceived -= RemoteMessage_MessageReceived;

            // unsubscribe the encoding menu clicked handler..
            CharacterSetMenuBuilder.EncodingMenuClicked -= CharacterSetMenuBuilder_EncodingMenuClicked;

            // dispose of the encoding menu items..
            CharacterSetMenuBuilder.DisposeCharacterSetMenu(mnuCharSets);

            // save the current session's documents to the database..
            SaveDocumentsToDatabase(CurrentSession, true);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text of the Scintilla changed via changing the encoding.
        /// </summary>
        private bool TextChangedViaEncodingChange { get; set; } = false;

        // an event which is fired if an encoding menu item is clicked..
        private void CharacterSetMenuBuilder_EncodingMenuClicked(object sender, EncodingMenuClickEventArgs e)
        {
            // a user requested to change the encoding of the file..
            if (e.Data != null && e.Data.ToString() == "convert_encoding")
            {
                // if there is an active document..
                if (sttcMain.CurrentDocument != null)
                {
                    // get the DBFILE_SAVE class instance from the tag..
                    DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;

                    // convert the contents to a new encoding..
                    sttcMain.CurrentDocument.Scintilla.Text =
                        StreamStringHelpers.ConvertEncoding(fileSave.ENCODING, e.Encoding, sttcMain.CurrentDocument.Scintilla.Text);

                    fileSave.PreviousEncodings.Add(fileSave.ENCODING);

                    // set the new encoding..
                    fileSave.ENCODING = e.Encoding;

                    TextChangedViaEncodingChange = true;

                    UpdateUndoRedoIndicators();
                }
            }
        }

        // a user is logging of or the system is shutting down..
        private void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
        {
            // ..just no questions asked save the document snapshots into the SQLite database..
            SaveDocumentsToDatabase(CurrentSession, true);
        }

        // the form is shown..
        private void FormMain_Shown(object sender, EventArgs e)
        {
            // ..so open the files given as arguments for the program..
            OpenArgumentFiles();
        }

        // a user decided to save the file..
        private void munSave_Click(object sender, EventArgs e)
        {
            // ..so lets obey for once..
            SaveDocument(sttcMain.CurrentDocument, sender.Equals(mnuSaveAs) || sender.Equals(tsbSaveAs));
        }

        // a user wanted to open a file via the main menu..
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            // if the file dialog was accepted (i.e. OK) then open the file to the view..
            if (odAnyFile.ShowDialog() == DialogResult.OK)
            {
                OpenDocument(odAnyFile.FileName, Encoding.Default);
            }
        }

        // a user wanted to open a file with encoding via the main menu..
        private void mnuOpenWithEncoding_Click(object sender, EventArgs e)
        {
            // ask the encoding first from the user..
            Encoding encoding = FormDialogQueryEncoding.Execute();
            if (encoding != null)
            {
                // if the file dialog was accepted (i.e. OK) then open the file to the view..
                if (odAnyFile.ShowDialog() == DialogResult.OK)
                {
                    OpenDocument(odAnyFile.FileName, encoding);
                }
            }
        }


        // the software's main form was activated so check if any open file has been changes..
        private void FormMain_Activated(object sender, EventArgs e)
        {
            CheckFileSysChanges();
        }

        // a tab is closing so save it into the history..
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

            ssLbLDocLinesSize.Text =
                DBLangEngine.GetMessage("msgDocSizeLines", "length: {0}  lines: {1}|As in the ScintillaNET document size in lines and in characters",
                e.ScintillaTabbedDocument.Scintilla.Text.Length,
                e.ScintillaTabbedDocument.Scintilla.Lines.Count);

            SetStatusStringText(e.ScintillaTabbedDocument);

            UpdateUndoRedoIndicators();
        }

        // a user wanted to see an about dialog of the software..
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            new VPKSoft.About.FormAbout(this, "MIT", "https://raw.githubusercontent.com/VPKSoft/ScriptNotepad/master/LICENSE");
        }

        // this is the event listener for the ScintillaTabbedDocument's selection and caret position change events..
        private void sttcMain_SelectionCaretChanged(object sender, ScintillaTabbedDocumentEventArgsExt e)
        {
            SetStatusStringText(e.ScintillaTabbedDocument);
        }

        // a user wanted to save all documents..
        private void mnuSaveAll_Click(object sender, EventArgs e)
        {
            SaveAllDocuments(sender.Equals(tsbSaveAllWithUnsaved) || sender.Equals(mnuSaveAllWithUnsaved));
        }

        // saves the changed ScintillaNET document's contents to a MemoryStream if the contents have been changed..
        // hopefully not stressful for the memory or CPU..
        private void sttcMain_DocumentTextChanged(object sender, ScintillaTextChangedEventArgs e)
        {
            DBFILE_SAVE fileSave = (DBFILE_SAVE)e.ScintillaTabbedDocument.Tag;
            fileSave.DisposeMemoryStream();
            fileSave.DB_MODIFIED = DateTime.Now;
            fileSave.FILE_CONTENTS = StreamStringHelpers.TextToMemoryStream(e.ScintillaTabbedDocument.Scintilla.Text, fileSave.ENCODING);

            // if the text has been changed and id did not occur by encoding change
            // just clear the undo "buffer"..
            if (!TextChangedViaEncodingChange)
            {
                fileSave.PreviousEncodings.Clear();
                TextChangedViaEncodingChange = false;
            }

            UpdateUndoRedoIndicators();
        }

        // a user wishes to do some scripting (!)..
        private void mnuRunScript_Click(object sender, EventArgs e)
        {
            // ..so display the script from and allow multiple instances of it..
            FormScript formScript = new FormScript();

            // subscribe an event for the C# script form when it requires a Scintilla document..
            formScript.ScintillaRequired += FormScript_ScintillaRequired;

            // subscribe the FormClosed event so the events can be unsubscribed (ridiculous -right ?)..
            formScript.FormClosed += FormScript_FormClosed;

            // show the form..
            formScript.Show();
        }

        // a C# script form was closed, so do unsubscribe the events..
        private void FormScript_FormClosed(object sender, FormClosedEventArgs e)
        {
            // get the FormScript instance..
            FormScript formScript = (FormScript)sender;

            // unsubscribe the events..
            formScript.ScintillaRequired -= FormScript_ScintillaRequired;
            formScript.FormClosed -= FormScript_FormClosed;

            // bring the form to the front..
            BringToFront();
        }

        // a script form requested an active document for a script manipulation..
        private void FormScript_ScintillaRequired(object sender, FormScript.ScintillaRequiredEventArgs e)
        {
            if (sttcMain.CurrentDocument != null)
            {
                e.Scintilla = sttcMain.CurrentDocument.Scintilla;
            }
        }

        // a user wishes to undo changes..
        private void tsbUndo_Click(object sender, EventArgs e)
        {
            // if there is an active document..
            if (sttcMain.CurrentDocument != null)
            {
                // ..then undo if it's possible..
                if (sttcMain.CurrentDocument.Scintilla.CanUndo)
                {
                    // undo..
                    sttcMain.CurrentDocument.Scintilla.Undo();

                    // get a DBFILE_SAVE class instance from the document's tag..
                    DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;

                    // undo the encoding change..
                    fileSave.UndoEncodingChange();
                }
            }
            UpdateUndoRedoIndicators();
        }

        // a user wishes to redo changes..
        private void tsbRedo_Click(object sender, EventArgs e)
        {
            if (sttcMain.CurrentDocument != null)
            {
                // ..then redo if it's possible..
                if (sttcMain.CurrentDocument.Scintilla.CanRedo)
                {
                    sttcMain.CurrentDocument.Scintilla.Redo();
                }
            }
            UpdateUndoRedoIndicators();
        }
        #endregion
    }
}
