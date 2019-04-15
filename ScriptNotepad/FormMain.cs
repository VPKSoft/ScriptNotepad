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

#region Usings
using ScintillaNET; // (C)::https://github.com/jacobslusser/ScintillaNET
using ScintillaNET_FindReplaceDialog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using VPKSoft.LangLib;
using VPKSoft.ScintillaLexers;
using VPKSoft.IPC;
using Microsoft.Win32;
using VPKSoft.PosLib;
using VPKSoft.ErrorLogger;
using VPKSoft.ScintillaTabbedTextControl;
using ScriptNotepad.UtilityClasses.StreamHelpers;
using ScriptNotepad.UtilityClasses.Encoding.CharacterSets;
using ScriptNotepad.DialogForms;
using static ScriptNotepad.Database.DatabaseEnumerations;
using ScriptNotepad.Database.UtilityClasses;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.ExternalProcessInteraction;
using ScriptNotepad.IOPermission;
using ScriptNotepad.UtilityClasses.SessionHelpers;
using ScriptNotepad.UtilityClasses.Clipboard;
using ScriptNotepad.UtilityClasses.Process;
using System.Reflection;
using ScriptNotepadPluginBase.PluginTemplateInterface;
using ScriptNotepadPluginBase.EventArgClasses;
using ScriptNotepad.PluginHandling;
using ScriptNotepad.Database.Tables;
using ScriptNotepad.Database.TableMethods;
using System.Linq;
using ScriptNotepad.UtilityClasses.Session;
using ScriptNotepad.Localization;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.CodeDom;
using ScriptNotepad.UtilityClasses.SearchAndReplace;
using ScriptNotepad.UtilityClasses.SearchAndReplace.Misc;
using VPKSoft.ScintillaLexers.HelperClasses;
#endregion

namespace ScriptNotepad
{
    public partial class FormMain : DBLangEngineWinforms
    {
        #region MassiveConstructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        /// <exception cref="Exception">Thrown if the database script isn't successfully run.</exception>
        public FormMain()
        {
            // Add this form to be positioned..
            PositionForms.Add(this);

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

            this.Text +=
                (ProcessElevation.IsElevated ? " (" +
                DBLangEngine.GetMessage("msgProcessIsElevated", "Administrator|A message indicating that a process is elevated.") + ")" : string.Empty);

            // initialize a connection to the SQLite database..
            Database.Database.InitConnection("Data Source=" + DBLangEngine.DataDir + "ScriptNotepad.sqlite;Pooling=true;FailIfMissing=false;Cache Size=10000;"); // PRAGMA synchronous=OFF;PRAGMA journal_mode=OFF

            // localize the open file dialog..
            StaticLocalizeFileDialog.InitFileDialog(odAnyFile);

            // localize the save file dialog..
            StaticLocalizeFileDialog.InitFileDialog(sdAnyFile);

            // localize the open and save file dialog titles..
            sdAnyFile.Title = DBLangEngine.GetMessage("msgSaveFileAs", "Save As|A title for a save file as dialog");
            odAnyFile.Title = DBLangEngine.GetMessage("msgOpenFile", "Open|A title for a open file dialog");

            // initialize the helper class for the status strip's labels..
            StatusStripTexts.InitLabels(ssLbLineColumn, ssLbLinesColumnSelection, ssLbLDocLinesSize, 
                ssLbLineEnding, ssLbEncoding, ssLbSessionName, ssLbInsertOverride);

            // set the status strip label's to indicate that there is no active document..
            StatusStripTexts.SetEmptyTexts(CurrentSession);

            // localize some other class properties, etc..
            FormLocalizationHelper.LocalizeMisc();

            // localize the "Default" session name for the current culture..
            if (!CurrentSessionLocalized)
            {
                Database.Database.LocalizeDefaultSessionName(
                    DBLangEngine.GetStatMessage("msgDefaultSessionName", "Default|A name of the default session for the documents"));

                // set the flag to indicate that the "Default" session name was localized..
                CurrentSessionLocalized = true;

                // also set the current session to the localized value..
                if (CurrentSession == "Default")
                {
                    CurrentSession =
                        DBLangEngine.GetStatMessage("msgDefaultSessionName", "Default|A name of the default session for the documents");
                }
            }

            // get the session ID number from the database..
            CurrentSessionID = Database.Database.GetSessionID(CurrentSession);

            // load the recent documents which were saved during the program close..
            LoadDocumentsFromDatabase(CurrentSession, false);

            CharacterSetMenuBuilder.CreateCharacterSetMenu(mnuCharSets, false, "convert_encoding");
            CharacterSetMenuBuilder.EncodingMenuClicked += CharacterSetMenuBuilder_EncodingMenuClicked;

            SessionMenuBuilder.SessionMenuClicked += SessionMenuBuilder_SessionMenuClicked;
            SessionMenuBuilder.CreateSessionMenu(mnuSession, CurrentSession);

            // enable the test menu only when debugging..
            mnuTest.Visible = System.Diagnostics.Debugger.IsAttached;

            // localize the recent files open all files text..
            RecentFilesMenuBuilder.MenuOpenAllRecentText =
                DBLangEngine.GetMessage("msgOpenAllRecentFiles", "Open all recent files...|A message in the recent files menu to open all the recent files");

            // create a menu for recent files..
            RecentFilesMenuBuilder.CreateRecentFilesMenu(mnuRecentFiles, CurrentSession, HistoryListAmount, true, mnuSplit2);

            // subscribe the click event for the recent file menu items..
            RecentFilesMenuBuilder.RecentFileMenuClicked += RecentFilesMenuBuilder_RecentFileMenuClicked;

            // create a dynamic action for the database exception logging..
            Database.Database.ExceptionLogAction = ExceptionLogger.LogError;

            // set the current session name to the status strip..
            StatusStripTexts.SetSessionName(CurrentSession);

            // subscribe the RequestDocuments event of the search and replace dialog..
            FormSearchAndReplace.Instance.RequestDocuments += InstanceRequestDocuments;

            // set the flag to reset the search area as the functionality is incomplete as of yet..
            FormSearchAndReplace.ResetSearchArea = false; // TODO::Make this a setting..

            // the user is either logging of from the system or is shutting down the system..
            SystemEvents.SessionEnding += SystemEvents_SessionEnding;

            // subscribe to the event when a search result is clicked from the FormSearchResultTree form..
            FormSearchResultTree.SearchResultSelected += FormSearchResultTreeSearchResultSelected;

            // subscribe to events which will occur with the FormSearchResultTree instance docking..
            FormSearchResultTree.RequestDockMainForm += FormSearchResultTree_RequestDockMainForm;
            FormSearchResultTree.RequestDockReleaseMainForm += FormSearchResultTree_RequestDockReleaseMainForm;

            // localize the new file name..
            sttcMain.NewFilenameStart =
                DBLangEngine.GetMessage("msgNewFileStart", "new |A starting text of how a new document should be named");

            // create a dynamic action for the class exception logging..
            AssignExceptionReportingActions();

            // create the default directory for the plug-ins if it doesn't exist yet..
            Settings.FormSettings.CreateDefaultPluginDirectory();

            // localize the about "box"..
            VPKSoft.About.FormAbout.OverrideCultureString = Settings.FormSettings.Settings.Culture.Name;

            // initialize the plug-in assemblies..
            InitializePlugins();
        }

        private void FormSearchResultTree_RequestDockReleaseMainForm(object sender, EventArgs e)
        {
            var dockForm = (FormSearchResultTree)sender;
            pnDock.Controls.Remove(dockForm);
            dockForm.Close();
        }

        private void FormSearchResultTree_RequestDockMainForm(object sender, EventArgs e)
        {
            // no docking if disabled..
            if (!FormSettings.Settings.DockSearchTreeForm)
            {
                return;
            }

            var dockForm = (FormSearchResultTree)sender;
            dockForm.TopLevel = false;
            dockForm.AutoScroll = true;
            dockForm.FormBorderStyle = FormBorderStyle.None;
            dockForm.Location = new Point(0, 0);
            dockForm.Width = pnDock.Width;
            dockForm.Height = Height * 15 / 100;
            dockForm.IsDocked = true;
            pnDock.Controls.Add(dockForm);
            //dockForm.Dock = DockStyle.Fill;
            dockForm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }

        private void FormSearchResultTreeSearchResultSelected(object sender, SearchResultTreeViewClickEventArgs e)
        {
            // check if the file is opened in the editor..
            if (e.SearchResult.isFileOpen)
            {
                int idx = sttcMain.Documents.FindIndex(f => f.FileName == e.SearchResult.fileName);
                if (idx != -1)
                {
                    sttcMain.ActivateDocument(idx);
                    var scintilla = sttcMain.Documents[idx].Scintilla;
                    scintilla.GotoPosition(e.SearchResult.startLocation);
                    scintilla.CurrentPosition = e.SearchResult.startLocation;
                    scintilla.SetSelection(e.SearchResult.startLocation, e.SearchResult.startLocation + e.SearchResult.length);
                    Focus();
                }
            }
            else
            {
                //TODO::Open the file..
            }
        }
        #endregion

        #region HelperMethods
        /// <summary>
        /// Initializes the plug-ins for the software.
        /// </summary>
        private void InitializePlugins()
        {
            // initialize the action to detect if a plug-in has crashed the program..
            ModuleExceptionHandler = delegate (string module) 
            {
                try
                {
                    var plugin = Plugins.FirstOrDefault(f => f.Plugin.FILENAME_FULL == module);
                    if (plugin.Plugin != null)
                    {
                        plugin.Plugin.APPLICATION_CRASHES++;
                        plugin.Plugin.ISACTIVE = false;
                        DatabasePlugins.UpdatePlugin(plugin.Plugin);
                    }
                }
                catch
                {
                    // the application is about to crash - let the ExceptionLogger do it's job and log the crash..
                }
            };

            IEnumerable<PLUGINS> databaseEntries = DatabasePlugins.GetPlugins();
            bool pluginDeleted = false;
            foreach (var pluginEntry in databaseEntries)
            {
                if (pluginEntry.PENDING_DELETION)
                {
                    try
                    {
                        File.Delete(pluginEntry.FILENAME_FULL);
                    }
                    catch (Exception ex)
                    {
                        // log the exception..
                        ExceptionLogger.LogError(ex);
                    }
                    DatabasePlugins.DeletePlugin(pluginEntry);
                    pluginDeleted = true;
                }
            }

            if (pluginDeleted)
            {
                databaseEntries = DatabasePlugins.GetPlugins();
            }

            // load the existing plug-ins..
            var plugins = PluginDirectoryRoaming.GetPluginAssemblies(Settings.FormSettings.Settings.PluginFolder);

            // loop through the found plug-ins..
            foreach (var plugin in plugins)
            {
                // check if the plug-in is already in the database..
                var pluginEntry =
                    databaseEntries.
                    FirstOrDefault(
                        f => f.FILENAME == Path.GetFileName(plugin.Path));

                // if the plug-in has been logged into the database and is disabled
                // save the flag..
                bool loadPlugin = true;
                if (pluginEntry != null)
                {
                    loadPlugin = pluginEntry.ISACTIVE;
                }

                // only valid plug-ins are accepted..
                if (plugin.IsValid && loadPlugin)
                {
                    // try to load the assembly and the plug-in..
                    var pluginAssembly = PluginInitializer.LoadPlugin(plugin.Path);

                    // set the locale for the plug-in..
                    if (pluginAssembly.Plugin != null)
                    {
                        pluginAssembly.Plugin.Locale = Settings.FormSettings.Settings.Culture.Name;
                    }

                    // try to initialize the plug-in..
                    if (PluginInitializer.InitializePlugin(pluginAssembly.Plugin,
                        RequestActiveDocument,
                        RequestAllDocuments,
                        PluginException, menuMain, 
                        mnuPlugins, 
                        CurrentSession, this))
                    {
                        if (pluginEntry == null)
                        {
                            pluginEntry = PluginDatabaseEntry.FromPlugin(plugin.Assembly, pluginAssembly.Plugin, plugin.Path);
                        }
                        else
                        {
                            pluginEntry = PluginDatabaseEntry.UpdateFromPlugin(pluginEntry, plugin.Assembly, pluginAssembly.Plugin, plugin.Path);
                        }

                        // on success, add the plug-in assembly and its instance to the internal list..
                        Plugins.Add((plugin.Assembly, pluginAssembly.Plugin, pluginEntry));
                    }

                    // update the possible version and the update time stamp..
                    pluginEntry.SetPluginUpdated(plugin.Assembly);

                    // update the plug-in information to the database..
                    DatabasePlugins.AddOrUpdatePlugin(pluginEntry);
                }
                else
                {
                    if (pluginEntry != null)
                    {
                        pluginEntry.LOAD_FAILURES++;
                    }
                    else
                    {
                        pluginEntry = PluginDatabaseEntry.InvalidPlugin(plugin.Assembly, plugin.Path);
                    }
                    // update the possible version and the update time stamp..
                    pluginEntry.SetPluginUpdated(plugin.Assembly);

                    // update the plug-in information to the database..
                    DatabasePlugins.AddOrUpdatePlugin(pluginEntry);

                    // on failure, add the "invalid" plug-in assembly and its instance to the internal list..
                    Plugins.Add((plugin.Assembly, null, pluginEntry));
                }
            }

            // set the plug-in menu visible only if any of the plug-ins added to the plug-in menu..
            mnuPlugins.Visible = mnuPlugins.DropDownItems.Count > 0;
        }

        /// <summary>
        /// Unsubscribes the external event handlers and disposes of the items created by other classes.
        /// </summary>
        private void DisposeExternal()
        {
            // release the references for the status strip labels..
            StatusStripTexts.Initialized = false;

            // dispose of the encoding menu items..
            CharacterSetMenuBuilder.DisposeCharacterSetMenu(mnuCharSets);

            // dispose of the recent file menu items..
            RecentFilesMenuBuilder.DisposeRecentFilesMenu(mnuRecentFiles);

            // dispose the session menu items..
            SessionMenuBuilder.DisposeSessionMenu();

            // unsubscribe the recent file menu item click handler..
            RecentFilesMenuBuilder.RecentFileMenuClicked -= RecentFilesMenuBuilder_RecentFileMenuClicked;

            // unsubscribe to events which will occur with the FormSearchResultTree instance docking..
            FormSearchResultTree.RequestDockMainForm -= FormSearchResultTree_RequestDockMainForm;
            FormSearchResultTree.RequestDockReleaseMainForm -= FormSearchResultTree_RequestDockReleaseMainForm;

            // unsubscribe to the event when a search result is clicked from the FormSearchResultTree form..
            FormSearchResultTree.SearchResultSelected -= FormSearchResultTreeSearchResultSelected;

            // unsubscribe the IpcClientServer MessageReceived event handler..
            IpcClientServer.RemoteMessage.MessageReceived -= RemoteMessage_MessageReceived;

            // unsubscribe the encoding menu clicked handler..
            CharacterSetMenuBuilder.EncodingMenuClicked -= CharacterSetMenuBuilder_EncodingMenuClicked;

            // unsubscribe the session menu clicked handler.. 
            SessionMenuBuilder.SessionMenuClicked -= SessionMenuBuilder_SessionMenuClicked;

            // unsubscribe the request documents event handler of the search and replace dialog..
            FormSearchAndReplace.Instance.RequestDocuments -= InstanceRequestDocuments;

            // set the search and replace dialog to be allowed to be disposed of..
            FormSearchAndReplace.AllowInstanceDispose = true;

            // dispose of the loaded plug-in..
            DisposePlugins();
        }

        /// <summary>
        /// Disposes the plug-ins loaded into the software.
        /// </summary>
        private void DisposePlugins()
        {
            // loop through the list of loaded plug-ins..
            for (int i = Plugins.Count -1; i >= 0; i--)
            {
                try
                {
                    using (Plugins[i].PluginInstance)
                    {
                        // just the disposal..
                    }
                }
                catch (Exception ex)
                {
                    Plugins[i].Plugin.EXCEPTION_COUNT++;

                    // the disposal failed so do add to the exception count..
                    DatabasePlugins.UpdatePlugin(Plugins[i].Plugin);

                    // log the dispose failures as well..
                    ExceptionLogger.LogMessage($"Plug-in dispose failed. Plug-in: {Plugins[i].PluginInstance.PluginName}, Assembly: {Plugins[i].Assembly.FullName}.");
                    ExceptionLogger.LogError(ex);
                }
            }

            // clear the list..
            Plugins.Clear();
        }
             

        /// <summary>
        /// Creates dynamic actions to static classes for error reporting.
        /// </summary>
        private void AssignExceptionReportingActions()
        {
            // create a dynamic action for the class exception logging..

            // many classes are inherited from this one (less copy/paste code!).. 
            ErrorHandlingBase.ExceptionLogAction = ExceptionLogger.LogError;

            PluginDirectoryRoaming.ExceptionLogAction =
                delegate (Exception ex, Assembly assembly, string assemblyFile)
                {
                    ExceptionLogger.LogMessage($"Assembly load failed. Assembly: {(assembly == null ? "unknown" : assembly.FullName)}, FileName: {(assemblyFile == null ? "unknown" : assemblyFile)}.");
                    ExceptionLogger.LogError(ex);
                };

            PluginInitializer.ExceptionLogAction =
                delegate (Exception ex, Assembly assembly, string assemblyFile, string methodName)
                {
                    ExceptionLogger.LogMessage($"Plug-in assembly initialization failed. Assembly: {(assembly == null ? "unknown" : assembly.FullName)}, FileName: {(assemblyFile == null ? "unknown" : assemblyFile)}, Method: {methodName}.");
                    ExceptionLogger.LogError(ex);
                };

            // END: create a dynamic action for the class exception logging..
        }

        /// <summary>
        /// This method should be called when the application is about to close.
        /// </summary>
        /// <param name="noUserInteraction">A flag indicating whether any user interaction/dialog should occur in the application closing event.</param>
        private void EndSession(bool noUserInteraction)
        {
            // if the user interaction is denied, prevent the application activation event from checking file system changes, etc..
            if (noUserInteraction)
            {
                Activated -= FormMain_Activated;
                FormClosed -= FormMain_FormClosed;

                // close all other open forms except this MainForm as they might dialogs, etc. to prevent the
                // session log of procedure..
                CloseFormUtils.CloseOpenForms(this);
            }

            // save the current session's documents to the database..
            SaveDocumentsToDatabase(CurrentSession);

            // delete excess document contents saved in the database..
            var cleanupContents = Database.Database.CleanupHistoryDocumentContents(CurrentSession, SaveFileHistoryContentsCount);

            ExceptionLogger.LogMessage($"Database history contents cleanup: success = {cleanupContents.success}, amount = {cleanupContents.deletedAmount}, session = {CurrentSession}.");

            // delete excess entries from the file history list from the database..
            cleanupContents = Database.Database.CleanUpHistoryList(CurrentSession, HistoryListAmount);

            ExceptionLogger.LogMessage($"Database history list cleanup: success = {cleanupContents.success}, amount = {cleanupContents.deletedAmount}, session = {CurrentSession}.");

            // close the main form as the call came from elsewhere than the FormMain_FormClosed event..
            if (noUserInteraction)
            {
                Close();
            }
        }

        /// <summary>
        /// Closes the current given session.
        /// </summary>
        /// <param name="sessionName">The name of the session of which documents to close.</param>
        private void CloseSession(string sessionName)
        {
            // save the current session's documents to the database..
            SaveDocumentsToDatabase(sessionName);

            // delete excess document contents saved in the database..
            var cleanupContents = Database.Database.CleanupHistoryDocumentContents(sessionName, SaveFileHistoryContentsCount);

            ExceptionLogger.LogMessage($"Database history contents cleanup: success = {cleanupContents.success}, amount = {cleanupContents.deletedAmount}, session = {CurrentSession}.");

            // delete excess entries from the file history list from the database..
            cleanupContents = Database.Database.CleanUpHistoryList(sessionName, HistoryListAmount);

            ExceptionLogger.LogMessage($"Database history list cleanup: success = {cleanupContents.success}, amount = {cleanupContents.deletedAmount}, session = {CurrentSession}.");

            // close all the documents..
            sttcMain.CloseAllDocuments();
        }

        /// <summary>
        /// Checks if an open document has been changed in the file system or removed from the file system and 
        /// queries the user form appropriate action for the file.
        /// </summary>
        private void CheckFileSysChanges()
        {
            for (int i = sttcMain.DocumentsCount - 1; i >= 0; i--)
            {
                // get the DBFILE_SAVE class instance from the document's tag..
                DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.Documents[i].Tag;

                // avoid excess checks further in the code..
                if (fileSave == null)
                {
                    continue;
                }

                // check if the file exists because it cannot be reloaded otherwise 
                // from the file system..
                if (File.Exists(sttcMain.Documents[i].FileName) && !fileSave.ShouldQueryFileReappeared)
                {
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


                            // set the flag that the form should be activated after the dialog..
                            bringToFrontQueued = true;
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

                            // set the flag that the form should be activated after the dialog..
                            bringToFrontQueued = true;
                        }
                    }
                }
                else
                {
                    // query the user if one wishes to keep a deleted
                    // file from the file system in the editor..
                    if (fileSave.ShouldQueryKeepFile)
                    {
                        if (MessageBox.Show(
                            DBLangEngine.GetMessage("msgFileHasBeenDeleted", "The file '{0}' has been deleted. Keep the file in the editor?|As in the opened file has been deleted from the file system and user is asked if to keep the deleted file in the editor", fileSave.FILENAME_FULL),
                            DBLangEngine.GetMessage("msgFileHasBeenDeletedTitle", "A file has been deleted|A caption message for a message dialog which will ask if a deleted file should be kept in the editor"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            // the user answered yes..
                            fileSave.EXISTS_INFILESYS = false; // set the flag to false..
                            fileSave.ISHISTORY = false;

                            DatabaseFileSave.AddOrUpdateFile(fileSave, sttcMain.Documents[i]);

                            // just in case set the tag back..
                            sttcMain.Documents[i].Tag = fileSave;

                            // set the flag that the form should be activated after the dialog..
                            bringToFrontQueued = true;
                        }
                        else
                        {
                            // call the handle method..
                            HandleCloseTab(fileSave, true, false, false, false);
                        }
                    }
                    else if (fileSave.ShouldQueryFileReappeared)
                    {
                        if (MessageBox.Show(
                            DBLangEngine.GetMessage("msgFileHasReappeared", "The file '{0}' has reappeared. Reload from the file system?|As in the file has reappeared to the file system and the software queries whether to reload it's contents from the file system", fileSave.FILENAME_FULL),
                            DBLangEngine.GetMessage("msgFileHasReappearedTitle", "A file has reappeared|A caption message for a message dialog which will ask if a reappeared file should be reloaded from the file system"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            // the user answered yes..
                            fileSave.EXISTS_INFILESYS = true; // set the flag to true..
                            fileSave.ISHISTORY = false;

                            DatabaseFileSave.AddOrUpdateFile(fileSave, sttcMain.Documents[i]);

                            // just in case set the tag back..
                            sttcMain.Documents[i].Tag = fileSave;

                            // reload the contents as the user answered yes..
                            sttcMain.SuspendTextChangedEvents = true; // suspend the changed events on the ScintillaTabbedTextControl..
                            fileSave.ReloadFromDisk(sttcMain.Documents[i]); // reload the file..
                            sttcMain.SuspendTextChangedEvents = false; // resume the changed events on the ScintillaTabbedTextControl..

                            // set the flag that the form should be activated after the dialog..
                            bringToFrontQueued = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A common method to handle different file closing "events".
        /// </summary>
        /// <param name="fileSave">An instance to <see cref="DBFILE_SAVE"/> class instance.</param>
        /// <param name="fileDeleted">A flag indicating if the file was deleted from the file system and a user decided to not the keep the file in the editor.</param>
        /// <param name="fileReappeared">A flag indicating that a file reappeared to the file system after being gone.</param>
        /// <param name="tabClosing">A flag indicating whether this call was made from the tab closing event of a <see cref="ScintillaTabbedDocument"/> class instance.</param>
        /// <param name="closeTab">A flag indicating whether the tab containing the given <paramref name="fileSave"/> should be closed.</param>
        /// <returns>A modified <see cref="DBFILE_SAVE"/> class instance based on the given parameters.</returns>
        private DBFILE_SAVE HandleCloseTab(DBFILE_SAVE fileSave, bool fileDeleted, 
            bool fileReappeared, bool tabClosing, bool closeTab)
        {
            // set the flags according to the parameters..
            fileSave.ISHISTORY = tabClosing || fileDeleted || closeTab; 

            // set the exists in file system flag..
            fileSave.EXISTS_INFILESYS = File.Exists(fileSave.FILENAME_FULL);

            // get the tabbed document index via the ID number..
            int docIndex = sttcMain.Documents.FindIndex(f => f.ID == fileSave.ID);

            // this should never be -1 but do check still in case of a bug..
            if (docIndex != -1)
            {
                // update the file save to the database..
                DatabaseFileSave.AddOrUpdateFile(fileSave, sttcMain.Documents[docIndex]);

                // update the file history list in the database..
                DatabaseRecentFiles.AddOrUpdateRecentFile(fileSave.FILENAME_FULL, fileSave.SESSIONNAME, fileSave.ENCODING);

                // the file was not requested to be kept in the editor after a deletion from the file system or the tab was requested to be closed..
                if (fileDeleted || closeTab)
                {
                    // ..so close the tab..
                    sttcMain.CloseDocument(docIndex);
                }

            }
            // set the bring to front flag based on the given parameters..
            bringToFrontQueued = fileDeleted;

            // re-create a menu for recent files..
            RecentFilesMenuBuilder.CreateRecentFilesMenu(mnuRecentFiles, CurrentSession, HistoryListAmount, true, mnuSplit2);

            // return the modified DBFILE_SAVE class instance.. 
            return fileSave; 
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
            new Test.FormTestThings().Show();
        }
        #endregion

        #region DocumentHelperMethods
        /// <summary>
        /// Saves the active document snapshots in to the SQLite database.
        /// </summary>
        /// <param name="sessionName">A name of the session to which the documents should be tagged with.</param>
        private void SaveDocumentsToDatabase(string sessionName)
        {
            for (int i = 0; i < sttcMain.DocumentsCount; i++)
            {
                DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.Documents[i].Tag;

                fileSave.ISACTIVE = sttcMain.Documents[i].FileTabButton.IsActive;
                fileSave.VISIBILITY_ORDER = i;
                DatabaseFileSave.AddOrUpdateFile(fileSave, sttcMain.Documents[i]);
                DatabaseRecentFiles.AddOrUpdateRecentFile(sttcMain.Documents[i].FileName, sessionName, fileSave.ENCODING);
            }
        }

        /// <summary>
        /// Sets the application title to indicate no active document.
        /// </summary>
        private void SetEmptyApplicationTitle()
        {
            this.Text =
                DBLangEngine.GetMessage("msgAppTitleWithoutFileName",
                "ScriptNotepad|As in the application name without an active file name") +
                (ProcessElevation.IsElevated ? " (" +
                DBLangEngine.GetMessage("msgProcessIsElevated", "Administrator|A message indicating that a process is elevated.") + ")" : string.Empty);
        }

        /// <summary>
        /// Sets the application title to indicate the currently active document.
        /// </summary>
        /// <param name="document">The <see cref="ScintillaTabbedDocument"/> document.</param>
        private void SetApplicationTitle(ScintillaTabbedDocument document)
        {
            this.Text =
                DBLangEngine.GetMessage("msgAppTitleWithFileName",
                "ScriptNotepad [{0}]|As in the application name combined with an active file name",
                document.FileName) +
                (ProcessElevation.IsElevated ? " (" +
                DBLangEngine.GetMessage("msgProcessIsElevated", "Administrator|A message indicating that a process is elevated.") + ")" : string.Empty);
        }

        /// <summary>
        /// Loads the document snapshots from the SQLite database.
        /// </summary>
        /// <param name="sessionName">A name of the session to which the documents are tagged with.</param>
        /// <param name="history">An indicator if the documents should be closed ones. I.e. not existing with the current session.</param>
        private void LoadDocumentsFromDatabase(string sessionName, bool history)
        {
            // set the status strip label's to indicate that there is no active document..
            StatusStripTexts.SetEmptyTexts(CurrentSession);

            // set the application title to indicate no active document..
            SetEmptyApplicationTitle();

            IEnumerable<DBFILE_SAVE> files = DatabaseFileSave.GetFilesFromDatabase(sessionName, DatabaseHistoryFlag.NotHistory);

            string activeDocument = string.Empty;

            foreach (DBFILE_SAVE file in files)
            {
                if (file.ISACTIVE)
                {
                    activeDocument = file.FILENAME_FULL;
                }
                sttcMain.AddDocument(file.FILENAME_FULL, (int)file.ID, file.ENCODING, StreamStringHelpers.TextToMemoryStream(file.FILE_CONTENTS, file.ENCODING));
                if (sttcMain.LastAddedDocument != null)
                {
                    sttcMain.LastAddedDocument.Tag = file;

                    sttcMain.LastAddedDocument.FileTabButton.ContextMenuStrip = cmsFileTab;
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
        /// Loads the document from the database based on a given <paramref name="recentFile"/> class instance.
        /// </summary>
        /// <param name="recentFile">A <see cref="RECENT_FILES"/> class instance containing the file data.</param>
        private void LoadDocumentFromDatabase(RECENT_FILES recentFile)
        {
            // get the file from the database..
            DBFILE_SAVE file = DatabaseFileSave.GetFileFromDatabase(recentFile.SESSIONNAME, recentFile.FILENAME_FULL);

            // only if something was gotten from the database..
            if (file != null)
            {
                sttcMain.AddDocument(file.FILENAME_FULL, (int)file.ID, file.ENCODING, StreamStringHelpers.TextToMemoryStream(file.FILE_CONTENTS, file.ENCODING));
                if (sttcMain.LastAddedDocument != null)
                {
                    // not history any more..
                    file.ISHISTORY = false;

                    // update the history flag to the database..
                    DatabaseFileSave.UpdateFileHistoryFlag(file);

                    // assign the context menu strip for the tabbed document..
                    sttcMain.LastAddedDocument.FileTabButton.ContextMenuStrip = cmsFileTab;

                    sttcMain.LastAddedDocument.Tag = file;
                    // the file load can't add an undo option the Scintilla..
                    sttcMain.LastAddedDocument.Scintilla.EmptyUndoBuffer();
                }
                sttcMain.ActivateDocument(file.FILENAME_FULL);

                UpdateUndoRedoIndicators();

                // re-create a menu for recent files..
                RecentFilesMenuBuilder.CreateRecentFilesMenu(mnuRecentFiles, CurrentSession, HistoryListAmount, true, mnuSplit2);
            }
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
                    OpenDocument(args[i], DefaultEncoding);
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
                            FILE_CONTENTS = "",
                            SESSIONNAME = CurrentSession,
                            SESSIONID = CurrentSessionID,
                        };

                    // assign the context menu strip for the tabbed document..
                    sttcMain.LastAddedDocument.FileTabButton.ContextMenuStrip = cmsFileTab;

                    // get a DBFILE_SAVE class instance from the document's tag..
                    DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;

                    // save the DBFILE_SAVE class instance to the Tag property..
                    sttcMain.CurrentDocument.Tag = DatabaseFileSave.AddOrUpdateFile(fileSave, sttcMain.CurrentDocument);
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
                        // check the database first for a DBFILE_SAVE class instance..
                        DBFILE_SAVE fileSave = DatabaseFileSave.GetFileFromDatabase(CurrentSession, fileName);

                        if (sttcMain.CurrentDocument.Tag == null && fileSave == null)
                        {
                            sttcMain.CurrentDocument.Tag = DatabaseFileSave.AddOrUpdateFile(sttcMain.CurrentDocument, DatabaseHistoryFlag.DontCare, CurrentSession, encoding);
                        }
                        else if (fileSave != null)
                        {
                            sttcMain.CurrentDocument.Tag = fileSave;
                            sttcMain.CurrentDocument.ID = (int)fileSave.ID;
                        }

                        // get a DBFILE_SAVE class instance from the document's tag..
                        fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;

                        // ..update the database with the document..
                        fileSave = DatabaseFileSave.AddOrUpdateFile(fileSave, sttcMain.CurrentDocument);
                        sttcMain.CurrentDocument.ID = (int)fileSave.ID;

                        if (reloadContents)
                        {
                            fileSave.ReloadFromDisk(sttcMain.CurrentDocument);
                        }

                        // save the DBFILE_SAVE class instance to the Tag property..
                        // USELESS CODE?::fileSave = Database.Database.AddOrUpdateFile(sttcMain.CurrentDocument, DatabaseHistoryFlag.DontCare, CurrentSession, fileSave.ENCODING);
                        sttcMain.CurrentDocument.Tag = fileSave;

                        // assign the context menu strip for the tabbed document..
                        sttcMain.LastAddedDocument.FileTabButton.ContextMenuStrip = cmsFileTab;

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
                        fileSave = DatabaseFileSave.AddOrUpdateFile(fileSave, document);

                        // save the DBFILE_SAVE class instance to the Tag property..
                        sttcMain.CurrentDocument.Tag = DatabaseFileSave.AddOrUpdateFile(sttcMain.CurrentDocument, DatabaseHistoryFlag.DontCare, CurrentSession, fileSave.ENCODING);

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
                    fileSave.FILE_CONTENTS = document.Scintilla.Text;

                    // only an existing file can be saved directly..
                    if (fileSave.EXISTS_INFILESYS && !saveAs)
                    {
                        File.WriteAllText(fileSave.FILENAME_FULL, fileSave.FILE_CONTENTS, fileSave.ENCODING);

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
                            File.WriteAllText(sdAnyFile.FileName, fileSave.FILE_CONTENTS, fileSave.ENCODING);

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
                            document.LexerType = LexerFileExtensions.LexerTypeFromFileName(fileSave.FILENAME_FULL);

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
        // the search and replace dialog is requesting for documents opened on this main form..
        private void InstanceRequestDocuments(object sender, ScintillaDocumentEventArgs e)
        {
            if (e.RequestAllDocuments)
            {
                e.Documents = sttcMain.Documents.Select(f => (f.Scintilla, f.FileName)).ToList();
            }
            else if (sttcMain.CurrentDocument != null)
            {
                e.Documents.Add((sttcMain.CurrentDocument.Scintilla, sttcMain.CurrentDocument.FileName));
            }
        }

        // a user wishes to manage sessions used by the software..
        private void mnuManageSessions_Click(object sender, EventArgs e)
        {
            // display the session management dialog..
            FormDialogSessionManage.Execute();

            #region SessionRename
            SESSION_NAME session = DatabaseSessionName.GetSessions().FirstOrDefault(f => f.SESSIONID == CurrentSessionID);
            // if true the current session has been renamed..
            if (session != null && CurrentSession != session.SESSIONNAME)
            {
                // set the current session name..
                Settings.FormSettings.Settings.CurrentSession = session.SESSIONNAME;

                // set the session name for all open documents..
                for (int i = 0; i < sttcMain.DocumentsCount; i++)
                {
                    ((DBFILE_SAVE)sttcMain.Documents[i].Tag).SESSIONNAME = session.SESSIONNAME;
                }

                // re-display the current document's status strip if any documents is active..
                if (sttcMain.CurrentDocument != null)
                {
                    StatusStripTexts.SetStatusStringText(sttcMain.CurrentDocument, CurrentSession);
                }
                else
                {
                    // no documents are active so re-display status strip with empty contents..
                    StatusStripTexts.SetEmptyTexts(CurrentSession);

                    // set the application title to indicate no active document..
                    SetEmptyApplicationTitle();
                }
            }
            #endregion

            // re-create the current session menu..
            SessionMenuBuilder.CreateSessionMenu(mnuSession, CurrentSession);

            // re-create a menu for recent files..
            RecentFilesMenuBuilder.CreateRecentFilesMenu(mnuRecentFiles, CurrentSession, HistoryListAmount, true, mnuSplit2);
        }

        /// <summary>
        /// Tries catch an event crashing the whole application if the error is causes by a plug-in.
        /// </summary>
        public static Action<string> ModuleExceptionHandler { get; set; } = null;

        // checks if file selected in the open file dialog requires elevation and the file exists..
        private void odAnyFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (FileIOPermission.FileRequiresElevation(odAnyFile.FileName).ElevationRequied)
            {
                if (MessageBox.Show(
                DBLangEngine.GetMessage("msgElevationRequiredForFile",
                "Opening the file '{0}' requires elevation (Run as Administrator). Restart the software as Administrator?|A message describing that a access to a file requires elevated permissions (Administrator)", odAnyFile.FileName),
                DBLangEngine.GetMessage("msgConfirm", "Confirm|A caption text for a confirm dialog."),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    e.Cancel = true;
                    Program.ElevateFile = odAnyFile.FileName;
                    Program.RestartElevated = true;
                    EndSession(true);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = !File.Exists(odAnyFile.FileName);
            }
        }

        // the user is either logging of from the system or is shutting down the system..
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            // end the without any user interaction/dialog..
            EndSession(true);
        }
        
        // a user wishes to open a recent file..
        private void RecentFilesMenuBuilder_RecentFileMenuClicked(object sender, RecentFilesMenuClickEventArgs e)
        {
            // if a file snapshot exists in the database then load it..
            if (e.RecentFile != null && e.RecentFile.EXISTSINDB)
            {
                LoadDocumentFromDatabase(e.RecentFile);
            }
            // else open the file from the file system..
            else if (e.RecentFile != null)
            {
                OpenDocument(e.RecentFile.FILENAME_FULL, e.RecentFile.ENCODING);
            }
            // in this case the menu item should contain all the recent files belonging to a session..
            else if (e.RecentFiles != null)
            {
                // loop through the recent files and open them all..
                foreach (RECENT_FILES recentFile in e.RecentFiles)
                {
                    // if a file snapshot exists in the database then load it..
                    if (recentFile.EXISTSINDB)
                    {
                        LoadDocumentFromDatabase(recentFile);
                    }
                    // else open the file from the file system..
                    else
                    {
                        OpenDocument(recentFile.FILENAME_FULL, recentFile.ENCODING);
                    }
                }
            }

        }

        // a user wishes to change the settings of the software..
        private void mnuSettings_Click(object sender, EventArgs e)
        {
            // ..so display the settings dialog..
            Settings.FormSettings.Execute();
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (
                // a user pressed a keyboard combination of CTRL+Z, which indicates undo for
                // the Scintilla control..
                (e.KeyCode == Keys.Z && e.Control && !e.Shift && !e.Alt) ||
                // a user pressed a keyboard combination of CTRL+Y, which indicates redo for
                // the Scintilla control..
                (e.KeyCode == Keys.Y && e.Control && !e.Shift && !e.Alt) ||
                // a user pressed the insert key a of a keyboard, which indicates toggling for
                // insert / override mode for the Scintilla control..
                (e.KeyCode == Keys.Insert && !e.Control && !e.Shift && !e.Alt))
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

                        sttcMain.CurrentDocument.Tag = fileSave;
                    }
                }
                UpdateUndoRedoIndicators();

                // special case called the "Insert" key..
                if (e.KeyCode == Keys.Insert)
                {
                    // only if a document exists..
                    if (sttcMain.CurrentDocument != null)
                    {
                        // ..set the insert / override text for the status strip..
                        StatusStripTexts.SetInsertOverrideStatusStripText(sttcMain.CurrentDocument, true);
                    }
                }
            }
            else if (
                e.KeyCode == Keys.Up || 
                e.KeyCode == Keys.Down || 
                e.KeyCode == Keys.Left || 
                e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.PageDown ||
                e.KeyCode == Keys.PageUp)
            {
                // set the flag to suspend the selection update to avoid excess CPU load..
                suspendSelectionUpdate = true;
            }
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.PageDown ||
                e.KeyCode == Keys.PageUp)
            {
                // release the flag which suspends the selection update to avoid excess CPU load..
                suspendSelectionUpdate = false;
                StatusStripTexts.SetStatusStringText(sttcMain.CurrentDocument, CurrentSession);
            }
        }

        private void sttcMain_DocumentMouseDown(object sender, MouseEventArgs e)
        {
            // set the flag to suspend the selection update to avoid excess CPU load..
            suspendSelectionUpdate = true;
        }

        private void sttcMain_DocumentMouseUp(object sender, MouseEventArgs e)
        {
            // release the flag which suspends the selection update to avoid excess CPU load..
            suspendSelectionUpdate = false;
            StatusStripTexts.SetStatusStringText(sttcMain.CurrentDocument, CurrentSession);
        }

        // this event is raised when another instance of this application receives a file name
        // via the IPC (no multiple instance allowed)..
        private void RemoteMessage_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate { OpenDocument(e.Message, DefaultEncoding); }));
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
            FormSearchAndReplace.ShowSearch();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // get the list of plug-in database entries..
            var pluginDatabaseEntries = Plugins.Select(f => f.Plugin);

            // update the list of plug-in database into the database..
            foreach (var entry in pluginDatabaseEntries)
            {
                DatabasePlugins.UpdatePlugin(entry);
            }

            // unsubscribe the external event handlers and dispose of the items created by other classes..
            DisposeExternal();
        }

        // if the form is closing, save the snapshots of the open documents to the SQLite database..
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            EndSession(false);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text of the Scintilla changed via changing the encoding.
        /// </summary>
        private bool TextChangedViaEncodingChange { get; set; } = false;

        /// <summary>
        /// A common method to change or convert the encoding of the active document.
        /// </summary>
        /// <param name="encoding">The encoding to change or convert into.</param>
        internal void ChangeDocumentEncoding(Encoding encoding)
        {
            if (encoding != null)
            {
                // if there is an active document..
                if (sttcMain.CurrentDocument != null)
                {
                    // get the DBFILE_SAVE class instance from the tag..
                    DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;
                    if (fileSave.EXISTS_INFILESYS) // the file exists in the file system..
                    {
                        // if the file has been changed in the editor, so confirm the user for a 
                        // reload from the file system..
                        if (fileSave.IsChangedInEditor)
                        {
                            if (MessageBox.Show(
                                DBLangEngine.GetMessage("msgFileHasChangedInEditorAction", "The file '{0}' has been changed in the editor and a reload from the file system is required. Continue?|A file has been changed in the editor and a reload from the file system is required to complete an arbitrary action", fileSave.FILENAME_FULL),
                                DBLangEngine.GetMessage("msgFileArbitraryFileChange", "A file has been changed|A caption message for a message dialog which will ask if a changed file should be reloaded"),
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                return; // the user decided not to reload..
                            }
                        }

                        fileSave.ENCODING = encoding; // set the new encoding..

                        // reload the file with the user given encoding..
                        fileSave.ReloadFromDisk(sttcMain.CurrentDocument);

                        // save the DBFILE_SAVE instance to the document's Tag property..
                        sttcMain.CurrentDocument.Tag = fileSave; 
                    }
                    // the file only exists in the database..
                    else
                    {
                        // convert the contents to a new encoding..
                        sttcMain.CurrentDocument.Scintilla.Text =
                            StreamStringHelpers.ConvertEncoding(fileSave.ENCODING, encoding, sttcMain.CurrentDocument.Scintilla.Text);

                        // save the previous encoding for an undo-possibility..
                        fileSave.PreviousEncodings.Add(fileSave.ENCODING);

                        fileSave.ENCODING = encoding; // set the new encoding..

                        // save the DBFILE_SAVE instance to the document's Tag property..
                        sttcMain.CurrentDocument.Tag = fileSave;
                    }
                }
            }
        }

        // an event when user clicks the change encoding main menu..
        private void mnuCharSets_Click(object sender, EventArgs e)
        {
            ChangeDocumentEncoding(FormDialogQueryEncoding.Execute());
        }

        // an event which is fired if an encoding menu item is clicked..
        private void CharacterSetMenuBuilder_EncodingMenuClicked(object sender, EncodingMenuClickEventArgs e)
        {
            // a user requested to change the encoding of the file..
            if (e.Data != null && e.Data.ToString() == "convert_encoding")
            {
                ChangeDocumentEncoding(e.Encoding);
            }
        }

        // a user wishes to change the current session..
        private void SessionMenuBuilder_SessionMenuClicked(object sender, SessionMenuClickEventArgs e)
        {
            // set the current session to a new value..
            CurrentSession = e.SessionName.SESSIONNAME;
        }

        // a user is logging of or the system is shutting down..
        private void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
        {
            // ..just no questions asked save the document snapshots into the SQLite database..
            SaveDocumentsToDatabase(CurrentSession);
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
                OpenDocument(odAnyFile.FileName, DefaultEncoding);
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

        // a user wishes to help with localization of the software (!!)..
        private void MnuLocalization_Click(object sender, EventArgs e)
        {
            try
            {
                string args = "--localize=\"" +
                              Path.Combine(
                                  Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                  "ScriptNotepad",
                                  "lang.sqlite") + "\"";

                Process.Start(Application.ExecutablePath, args);
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex, "Localization");
            }
        }

        // a user wishes to dump (update) the current language database..
        private void MnuDumpLanguage_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.ExecutablePath, "--dblang");
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex, "Localization dump");
            }
        }

        // the software's main form was activated so check if any open file has been changes..
        private void FormMain_Activated(object sender, EventArgs e)
        {
            // release the flag which suspends the selection update to avoid excess CPU load..
            suspendSelectionUpdate = false;

            CheckFileSysChanges();

            // start the timer to bring the main form to the front..
            leftActivatedEvent = true;
            tmGUI.Enabled = true;

            FormSearchAndReplace.Instance.ToggleStayTop(true);
        }

        // a tab is closing so save it into the history..
        private void sttcMain_TabClosing(object sender, TabClosingEventArgsExt e)
        {
            // call the handle method..
            HandleCloseTab((DBFILE_SAVE)e.ScintillaTabbedDocument.Tag, false, false, true, false);

            // if there are no documents any more..
            if (sttcMain.DocumentsCount - 1 <= 0) 
            {
                // set the status strip label's to indicate that there is no active document..
                StatusStripTexts.SetEmptyTexts(CurrentSession);

                // set the application title to indicate no active document..
                SetEmptyApplicationTitle();
            }
        }

        // a user activated a tab (document) so display it's file name..
        private void sttcMain_TabActivated(object sender, TabActivatedEventArgs e)
        {
            // set the application title to indicate the currently active document..
            SetApplicationTitle(e.ScintillaTabbedDocument);

            StatusStripTexts.SetDocumentSizeText(e.ScintillaTabbedDocument);

            StatusStripTexts.SetStatusStringText(e.ScintillaTabbedDocument, CurrentSession);

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
            if (e.ScintillaTabbedDocument.Scintilla.SelectionEnd == e.ScintillaTabbedDocument.Scintilla.SelectionStart)
            {
                e.ScintillaTabbedDocument.Scintilla.IndicatorClearRange(0, e.ScintillaTabbedDocument.Scintilla.TextLength);
            }

            if (!suspendSelectionUpdate)
            {
                StatusStripTexts.SetStatusStringText(e.ScintillaTabbedDocument, CurrentSession);
            }
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
            fileSave.DB_MODIFIED = DateTime.Now;
            fileSave.FILE_CONTENTS = e.ScintillaTabbedDocument.Scintilla.Text;

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

        private void Database_ExceptionOccurred(object sender, ExceptionEventArgs exceptionEventArgs)
        {
            // log the database exception..
            ExceptionLogger.LogError(exceptionEventArgs.Exception);
        }

        // a timer to prevent an endless loop with the form activated event (probably a poor solution)..
        private void tmGUI_Tick(object sender, EventArgs e)
        {
            tmGUI.Enabled = false;
            if (bringToFrontQueued && leftActivatedEvent)
            {
                // this event in this case leads to an endless loop..
                Activated -= FormMain_Activated;

                // bring the form to the front..
                BringToFront();
                Activate();

                // this event in this case leads to an endless loop..
                Activated += FormMain_Activated;
            }
            bringToFrontQueued = false;
            leftActivatedEvent = false;
        }

        // occurs when a plug-in requests for the currently active document..
        private void RequestActiveDocument(object sender, RequestScintillaDocumentEventArgs e)
        {
            // verify that there is an active document, etc..
            if (sttcMain.CurrentDocument != null && sttcMain.CurrentDocument.Tag != null)
            {
                DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;
                e.AllDocuments = false; // set to flag indicating all the documents to false..

                // add the document details to the event arguments..
                e.Documents.Add(
                    (fileSave.ENCODING,
                    sttcMain.CurrentDocument.Scintilla,
                    fileSave.FILENAME_FULL,
                    fileSave.FILESYS_MODIFIED,
                    fileSave.DB_MODIFIED, 
                    true));
            }
        }

        // occurs when a plug-in requests for all the open documents..
        private void RequestAllDocuments(object sender, RequestScintillaDocumentEventArgs e)
        {
            // loop through all the documents..
            for (int i = 0; i < sttcMain.DocumentsCount; i++)
            {
                // verify the document's validity..
                if (sttcMain.Documents[i].Tag == null)
                {
                    continue;
                }
                DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.Documents[i].Tag;

                // add the document details to the event arguments..
                e.Documents.Add(
                    (fileSave.ENCODING,
                    sttcMain.Documents[i].Scintilla,
                    fileSave.FILENAME_FULL,
                    fileSave.FILESYS_MODIFIED,
                    fileSave.DB_MODIFIED,
                    true));
            }
            e.AllDocuments = true; // set to flag indicating all the documents to true..
        }

        // occurs when an exception has occurred in a plug-in (NOTE: The plug-in must have exception handling!)..
        private void PluginException(object sender, PluginExceptionEventArgs e)
        {
            ExceptionLogger.LogMessage($"PLUG-IN EXCEPTION: '{e.PluginModuleName}'.");
            ExceptionLogger.LogError(e.Exception);
        }

        // a user wishes to manage the plug-ins used by the software..
        private void mnuManagePlugins_Click(object sender, EventArgs e)
        {
            // get the current plug-ins as a List<T>..
            var plugins = Plugins.Select(f => f.Plugin).ToList();

            // display the plug-in management form passing a reference to the plug-in list..
            if (FormPluginManage.Execute(ref plugins))
            {
                // if the user selected OK, accepted the dialog,
                // loop through the plug-ins..
                for (int i = 0; i < plugins.Count; i++)
                {
                    // find an index to the plug-in possibly modified by the dialog..
                    int idx = Plugins.FindIndex(f => f.Plugin.ID == plugins[i].ID);

                    // if a valid index was found..
                    if (idx != -1)
                    {
                        // set the new value for the PLUGINS class instance..
                        Plugins[idx] = (Plugins[idx].Assembly, Plugins[idx].PluginInstance, plugins[i]);
                    }
                }
            }
        }

        private FormWindowState prev = FormWindowState.Normal;

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && prev != FormWindowState.Minimized)
            {
                FormSearchAndReplace.Instance.ToggleVisible(false);
                prev = WindowState;
            }
            else if (prev != WindowState)
            {
                FormSearchAndReplace.Instance.ToggleVisible(true);
                prev = WindowState;
            }
        }

        private void FormMain_Deactivate(object sender, EventArgs e)
        {
            FormSearchAndReplace.Instance.ToggleStayTop(false);
        }

        private void FormMain_ResizeBegin(object sender, EventArgs e)
        {
            SuspendLayout();
        }

        private void FormMain_ResizeEnd(object sender, EventArgs e)
        {
            ResumeLayout();
        }

        private void SttcMain_DocumentMouseDoubleClick(object sender, MouseEventArgs e)
        {
            var scintilla = (Scintilla)sender;
            Highlight.HighlightWord(scintilla, scintilla.SelectedText, Color.LimeGreen);
        }
        #endregion

        #region PrivateFields        
        /// <summary>
        /// A find and replace dialog for the ScintillaNET.
        /// </summary>
        private FindReplace findReplace = new FindReplace();

        /// <summary>
        /// An IPC client / server to transmit Windows shell file open requests to the current process.
        /// (C): VPKSoft: https://gist.github.com/VPKSoft/5d78f1c06ec51ebad34817b491fe6ac6
        /// </summary>
        private IpcClientServer ipcServer = new IpcClientServer();

        /// <summary>
        /// A flag indicating if the main form should be activated.
        /// </summary>
        private bool bringToFrontQueued = false;

        /// <summary>
        /// A flag indicating if the main form's execution has left the Activated event.
        /// </summary>
        private bool leftActivatedEvent = false;

        /// <summary>
        /// A flag indicating whether the selection should be update to the status strip.
        /// Continuous updates with keyboard will cause excess CPU usage.
        /// </summary>
        private bool suspendSelectionUpdate = false;
        #endregion

        #region PrivateProperties
        /// <summary>
        /// Gets or sets the current session for the documents.
        /// </summary>
        private string CurrentSession
        {
            get => Settings.FormSettings.Settings == null ? "Default" : Settings.FormSettings.Settings.CurrentSession;

            set
            {
                bool sessionChanged = Settings.FormSettings.Settings.CurrentSession != value;
                string previousSession = Settings.FormSettings.Settings.CurrentSession;

                Settings.FormSettings.Settings.CurrentSession = value;

                if (sessionChanged)
                {
                    CloseSession(previousSession);

                    // load the recent documents which were saved during the program close..
                    LoadDocumentsFromDatabase(CurrentSession, false);
                }

                // get the session ID number from the database..
                CurrentSessionID = Database.Database.GetSessionID(CurrentSession);
            }
        }

        /// <summary>
        /// Gets or sets the loaded active plug-ins.
        /// </summary>
        List<(Assembly Assembly, IScriptNotepadPlugin PluginInstance, PLUGINS Plugin)> Plugins { get; set; } = new List<(Assembly Assembly, IScriptNotepadPlugin PluginInstance, PLUGINS Plugin)>();

        /// <summary>
        /// Gets or sets a value indicating whether the default session name has been localized.
        /// </summary>
        private bool CurrentSessionLocalized
        {
            get => Settings.FormSettings.Settings.DefaultSessionLocalized;
            set => Settings.FormSettings.Settings.DefaultSessionLocalized = value;
        }

        /// <summary>
        /// Gets or sets the default encoding to be used with the files within this software.
        /// </summary>
        private Encoding DefaultEncoding
        {
            get => Settings.FormSettings.Settings.DefaultEncoding;
        }

        /// <summary>
        /// The amount of files to be saved to a document history.
        /// </summary>
        private int HistoryListAmount
        {
            get => Settings.FormSettings.Settings.HistoryListAmount;
            set => Settings.FormSettings.Settings.HistoryListAmount = value;
        }

        /// <summary>
        /// Gets the save file history contents count.
        /// </summary>
        private int SaveFileHistoryContentsCount
        {
            get => Settings.FormSettings.Settings.SaveFileHistoryContents ?
                // the setting value if the setting is enabled..
                Settings.FormSettings.Settings.SaveFileHistoryContentsCount : 
                int.MinValue; // the minimum value if the setting is disabled..
        }

        /// <summary>
        /// Gets or sets the ID number for the current session for the documents.
        /// </summary>
        private long CurrentSessionID { get; set; } = -1;
        #endregion

        #region FileContextMenu
        // a user wishes to do "do something" with the file (existing one)..
        private void CommonContextMenu_FileInteractionClick(object sender, EventArgs e)
        {
            if (sttcMain.CurrentDocument != null) // the first null check..
            {
                var document = sttcMain.CurrentDocument; // get the active document..

                // get the DBFILE_SAVE from the active document..
                var fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag; 

                if (fileSave != null) // the second null check..
                {
                    // based on the sending menu item, select the appropriate action..
                    if (sender.Equals(mnuOpenContainingFolderInCmd))
                    {
                        // open the command prompt with the file's path..
                        CommandPromptInteraction.OpenCmdWithPath(Path.GetDirectoryName(fileSave.FILENAME_FULL));
                    }
                    else if (sender.Equals(mnuOpenContainingFolderInWindowsPowerShell))
                    {
                        // open the Windows PowerShell with the file's path..
                        CommandPromptInteraction.OpenPowerShellWithPath(Path.GetDirectoryName(fileSave.FILENAME_FULL));
                    }
                    else if (sender.Equals(mnuOpenContainingFolderInExplorer))
                    {
                        // open the Windows explorer and select the file from it..
                        WindowsExplorerInteraction.ShowFileOrPathInExplorer(fileSave.FILENAME_FULL);
                    }
                    else if (sender.Equals(mnuOpenWithAssociatedApplication))
                    {
                        // open the file with an associated software..
                        WindowsExplorerInteraction.OpenWithAssociatedProgram(fileSave.FILENAME_FULL);
                    }
                    else if (sender.Equals(mnuFullFilePathToClipboard))
                    {
                        // copy the full file path to the clipboard..
                        ClipboardTextHelper.ClipboardSetText(Path.GetDirectoryName(fileSave.FILENAME_FULL));
                    }
                    else if (sender.Equals(mnuFullFilePathAndNameToClipboard))
                    {
                        // copy the full file name to the clipboard..
                        ClipboardTextHelper.ClipboardSetText(fileSave.FILENAME_FULL);
                    }
                    else if (sender.Equals(mnuFileNameToClipboard))
                    {
                        // copy the file name to the clipboard..
                        ClipboardTextHelper.ClipboardSetText(Path.GetFileName(fileSave.FILENAME_FULL));
                    }
                    else if (sender.Equals(mnuCloseTab))
                    {
                        sttcMain.CloseDocument(document, true);
                    }
                }
            }
        }

        // the context menu is opening for user to "do something" with the file..
        private void cmsFileTab_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sttcMain.CurrentDocument != null) // the first null check..
            {
                var document = sttcMain.CurrentDocument; // get the active document..

                // get the DBFILE_SAVE from the active document..
                var fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;

                if (fileSave != null) // the second null check..
                {
                    // enable / disable items which requires the file to exist in the file system..
                    mnuOpenContainingFolderInExplorer.Enabled = File.Exists(fileSave.FILENAME_FULL);
                    mnuOpenWithAssociatedApplication.Enabled = File.Exists(fileSave.FILENAME_FULL);
                    mnuOpenContainingFolderInCmd.Enabled = File.Exists(fileSave.FILENAME_FULL);
                    mnuOpenContainingFolderInWindowsPowerShell.Enabled = File.Exists(fileSave.FILENAME_FULL);
                    mnuOpenWithAssociatedApplication.Enabled = File.Exists(fileSave.FILENAME_FULL);
                }
            }
        }

        /// <summary>
        /// A method for closing multiple documents depending of the given parameters.
        /// </summary>
        /// <param name="right">A flag indicating that from the active document to the right all documents should be closed.</param>
        /// <param name="left">A flag indicating that from the active document to the left all documents should be closed.</param>
        private void CloseAllFunction(bool right, bool left)
        {
            // get the document in question..
            var document = sttcMain.CurrentDocument;

            // get the index for the active document..
            int idx = document != null ? sttcMain.Documents.FindIndex(f => f.Equals(document)) : -1;

            // validate the index..
            if (idx != -1)
            {
                // do a backward loop and close all the documents except the active one..
                for (int i = sttcMain.DocumentsCount - 1; i >= 0; i--)
                {
                    // validate the right and left flags..
                    if ((left && i > idx) || (right && i < idx))
                    {
                        // ..if this is a match then do continue..
                        continue;
                    }

                    // the index is the active document..
                    if (idx == i)
                    {
                        // ..so skip the document..
                        continue;
                    }

                    // call the handle method..
                    HandleCloseTab((DBFILE_SAVE)sttcMain.Documents[i].Tag, false, false, false, true);
                }
            }
        }

        // a user wishes to close all expect the active document or many documents 
        // to the right or to the left from the active document..
        private void CommonCloseManyDocuments(object sender, EventArgs e)
        {
            // call the CloseAllFunction method with this "wondrous" logic..
            CloseAllFunction(sender.Equals(mnuCloseAllToTheRight), sender.Equals(mnuCloseAllToTheLeft));
        }
        #endregion
    }
}
