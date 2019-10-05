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
using Microsoft.Win32;
using ScintillaNET; // (C)::https://github.com/jacobslusser/ScintillaNET
using ScriptNotepad.Database.TableMethods;
using ScriptNotepad.Database.Tables;
using ScriptNotepad.Database.UtilityClasses;
using ScriptNotepad.DialogForms;
using ScriptNotepad.IOPermission;
using ScriptNotepad.Localization;
using ScriptNotepad.Localization.Forms;
using ScriptNotepad.PluginHandling;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.Clipboard;
using ScriptNotepad.UtilityClasses.CodeDom;
using ScriptNotepad.UtilityClasses.Encodings;
using ScriptNotepad.UtilityClasses.Encodings.CharacterSets;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.ExternalProcessInteraction;
using ScriptNotepad.UtilityClasses.Keyboard;
using ScriptNotepad.UtilityClasses.MenuHelpers;
using ScriptNotepad.UtilityClasses.MiscForms;
using ScriptNotepad.UtilityClasses.Process;
using ScriptNotepad.UtilityClasses.ProgrammingLanguages;
using ScriptNotepad.UtilityClasses.ScintillaNETUtils;
using ScriptNotepad.UtilityClasses.ScintillaUtils;
using ScriptNotepad.UtilityClasses.SearchAndReplace;
using ScriptNotepad.UtilityClasses.SearchAndReplace.Misc;
using ScriptNotepad.UtilityClasses.Session;
using ScriptNotepad.UtilityClasses.SessionHelpers;
using ScriptNotepad.UtilityClasses.SpellCheck;
using ScriptNotepad.UtilityClasses.StreamHelpers;
using ScriptNotepad.UtilityClasses.TextManipulationUtils;
using ScriptNotepadPluginBase.EventArgClasses;
using ScriptNotepadPluginBase.PluginTemplateInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VPKSoft.ErrorLogger;
using VPKSoft.IPC;
using VPKSoft.LangLib;
using VPKSoft.PosLib;
using VPKSoft.ScintillaLexers;
using VPKSoft.ScintillaLexers.HelperClasses;
using VPKSoft.ScintillaTabbedTextControl;
using VPKSoft.VersionCheck;
using static ScriptNotepad.Database.DatabaseEnumerations;
using static ScriptNotepad.UtilityClasses.Encodings.FileEncoding;
using static VPKSoft.ScintillaLexers.GlobalScintillaFont;
#endregion

namespace ScriptNotepad
{
    /// <summary>
    /// The main form of this software.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
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
                DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages");

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

            // ReSharper disable once ArrangeThisQualifier
            // ReSharper disable once VirtualMemberCallInConstructor
            this.Text +=
                (ProcessElevation.IsElevated ? " (" +
                DBLangEngine.GetMessage("msgProcessIsElevated", "Administrator|A message indicating that a process is elevated.") + ")" : string.Empty);

            // initialize a connection to the SQLite database..
            Database.Database.InitConnection("Data Source=" + DBLangEngine.DataDir + "ScriptNotepad.sqlite;Pooling=true;FailIfMissing=false;Cache Size=10000;"); // PRAGMA synchronous=OFF;PRAGMA journal_mode=OFF

            // localize the open file dialog..
            StaticLocalizeFileDialog.InitFileDialog(odAnyFile);

            // localize the save file dialog..
            StaticLocalizeFileDialog.InitFileDialog(sdAnyFile);

            // localize the save HTML dialog..
            StaticLocalizeFileDialog.InitHTMLFileDialog(sdHTML);

            // localize the open and save file dialog titles..
            sdAnyFile.Title = DBLangEngine.GetMessage("msgSaveFileAs", "Save As|A title for a save file as dialog");
            odAnyFile.Title = DBLangEngine.GetMessage("msgOpenFile", "Open|A title for a open file dialog");

            // initialize the helper class for the status strip's labels..
            StatusStripTexts.InitLabels(ssLbLineColumn, ssLbLinesColumnSelection, ssLbLDocLinesSize, 
                ssLbLineEnding, ssLbEncoding, ssLbSessionName, ssLbInsertOverride, sslbZoom);

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

            // get the font size and family from the settings..
            FontFamilyName = FormSettings.Settings.EditorFontName;
            FontSize = FormSettings.Settings.EditorFontSize; 

            // localize the thread if set in the settings..
            if (FormSettings.Settings.LocalizeThread)
            {
                Thread.CurrentThread.CurrentCulture = DBLangEngine.UseCulture;
                Thread.CurrentThread.CurrentUICulture = DBLangEngine.UseCulture;
            }

            // get the session ID number from the database..
            CurrentSessionID = Database.Database.GetSessionID(CurrentSession);

            // localize the context menu before any of the context menus are build to the Scintilla controls..
            ScintillaContextMenu.LocalizeTexts();

            // create a programming language selection menu..
            ProgrammingLanguageHelper = new ProgrammingLanguageHelper(mnuProgrammingLanguage,
                FormSettings.Settings.CategorizeStartCharacterProgrammingLanguage);

            ProgrammingLanguageHelper.LanguageMenuClick += ProgrammingLanguageHelper_LanguageMenuClick;

            // get the brace highlight colors from the settings..
            sttcMain.UseBraceHighlight = FormSettings.Settings.HighlightBraces;
            sttcMain.ColorBraceHighlightForeground = FormSettings.Settings.BraceHighlightForegroundColor;
            sttcMain.ColorBraceHighlightBackground = FormSettings.Settings.BraceHighlightBackgroundColor;
            sttcMain.ColorBraceHighlightBad = FormSettings.Settings.BraceBadHighlightForegroundColor;
            // END::get the brace highlight colors from the settings..

            // load the recent documents which were saved during the program close..
            LoadDocumentsFromDatabase(CurrentSession);

            CharacterSetMenuBuilder.CreateCharacterSetMenu(mnuCharSets, false, "convert_encoding");
            CharacterSetMenuBuilder.EncodingMenuClicked += CharacterSetMenuBuilder_EncodingMenuClicked;

            SessionMenuBuilder.SessionMenuClicked += SessionMenuBuilder_SessionMenuClicked;
            SessionMenuBuilder.CreateSessionMenu(mnuSession, CurrentSession);

            // enable the test menu only when debugging..
            mnuTest.Visible = Debugger.IsAttached;

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
            FormSettings.CreateDefaultPluginDirectory();

            // localize the about "box"..
            FormAbout.OverrideCultureString = FormSettings.Settings.Culture.Name;

            // initialize the plug-in assemblies..
            InitializePlugins();

            // set the spell check timer interval..
            tmSpellCheck.Interval = FormSettings.Settings.EditorSpellCheckInactivity;

            // enable the spell check timer..
            tmSpellCheck.Enabled = true;

            // enable the GUI timer..
            tmGUI.Enabled = true;

            // set the code indentation value from the settings..
            sttcMain.UseCodeIndenting = FormSettings.Settings.EditorUseCodeIndentation;

            // set the tab width value from the settings..
            sttcMain.TabWidth = FormSettings.Settings.EditorTabWidth;

            // create a menu for open forms within the application..
            WinFormsFormMenuBuilder = new WinFormsFormMenuBuilder(mnuWindow);

            // create a menu for the open tabs..
            TabMenuBuilder = new TabMenuBuilder(mnuTab, sttcMain);

            // set the value whether to use individual zoom for the open document(s)..
            sttcMain.ZoomSynchronization = !FormSettings.Settings.EditorIndividualZoom;

            // this flag can suspend some events from taking place before
            // the constructor has finished..
            ConstructorFinished = true;

            // set the default encoding of the DetectEncoding class..
            DetectEncoding.FallBackEncoding = Encoding.Default;

            // set the state of the auto-save..
            tmAutoSave.Interval =
                (int) TimeSpan.FromMinutes(FormSettings.Settings.ProgramAutoSaveInterval).TotalMilliseconds;
            tmAutoSave.Enabled = FormSettings.Settings.ProgramAutoSave;
        }
        #endregion

        #region HelperMethods                
        /// <summary>
        /// Checks for new version of the application.
        /// </summary>
        private void CheckForNewVersion()
        {
            // no going to the internet if the user doesn't allow it..
            if (FormSettings.Settings.UpdateAutoCheck)
            {
                FormCheckVersion.CheckForNewVersion("https://www.vpksoft.net/versions/version.php",
                    Assembly.GetEntryAssembly(), FormSettings.Settings.Culture.Name);
            }
        }

        /// <summary>
        /// Sets the state of the spell checker.
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> the spell checking is enabled.</param>
        /// <param name="noDatabaseUpdate">A flag indicating whether to save the spell checking state to the database.</param>
        private void SetSpellCheckerState(bool enabled, bool noDatabaseUpdate)
        {
            CurrentDocumentAction(document =>
            {
                // validate that the ScintillaTabbedDocument instance has a spell checker attached to it..
                if (document.Tag0 != null &&
                    document.Tag0.GetType() == typeof(TabbedDocumentSpellCheck))
                {
                    // get the TabbedDocumentSpellCheck class instance..
                    var spellCheck = (TabbedDocumentSpellCheck) document.Tag0;

                    // set the document's spell check to either enabled or disabled..
                    spellCheck.Enabled = enabled;

                    DBFILE_SAVE fileSave = (DBFILE_SAVE) document.Tag;
                    fileSave.USESPELL_CHECK = spellCheck.Enabled;
                    if (!noDatabaseUpdate)
                    {
                        DatabaseFileSave.UpdateMiscFlags(fileSave);
                    }

                    document.Tag = fileSave;
                }
            });
        }

        /// <summary>
        /// Undo the document changes if possible.
        /// </summary>
        private void Undo()
        {
            // if there is an active document..
            CurrentDocumentAction(document =>
            {
                // ..then undo if it's possible..
                if (document.Scintilla.CanUndo)
                {
                    // undo..
                    document.Scintilla.Undo();

                    // get a DBFILE_SAVE class instance from the document's tag..
                    DBFILE_SAVE fileSave = (DBFILE_SAVE)document.Tag;

                    // undo the encoding change..
                    fileSave.UndoEncodingChange();

                    if (!document.Scintilla.CanUndo)
                    {
                        fileSave.PopPreviousDbModified();
                        document.FileTabButton.IsSaved = IsFileChanged(fileSave);
                    }
                }
            });

            UpdateToolbarButtonsAndMenuItems();
        }

        /// <summary>
        /// Redo the document changes if possible.
        /// </summary>
        private void Redo()
        {
            // if there is an active document..
            CurrentDocumentAction(document =>
                {
                    // ..then redo if it's possible..
                    if (document.Scintilla.CanRedo)
                    {
                        document.Scintilla.Redo();
                    }
                }
            );
            UpdateToolbarButtonsAndMenuItems();
        }

        /// <summary>
        /// Enables or disables the main form's GUI timers.
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> the timers are enabled.</param>
        private void EnableDisableTimers(bool enabled)
        {
            tmSpellCheck.Enabled = enabled;
            tmGUI.Enabled = enabled;
        }

        /// <summary>
        /// Disposes the spell checkers attached to the document tabs.
        /// </summary>
        private void DisposeSpellCheckers()
        {
            for (int i = 0; i < sttcMain.DocumentsCount; i++)
            {
                // validate that the ScintillaTabbedDocument instance has a spell checker attached to it..
                if (sttcMain.Documents[i] != null && sttcMain.Documents[i].Tag0 != null &&
                    sttcMain.Documents[i].Tag0.GetType() == typeof(TabbedDocumentSpellCheck))
                {
                    // dispose of the spell checker
                    var spellCheck = (TabbedDocumentSpellCheck) sttcMain.Documents[i].Tag0;

                    using (spellCheck)
                    {
                        sttcMain.Documents[i].Tag0 = null;
                    }
                }
            }
        }

        /// <summary>
        /// Unsubscribes the event handlers from the context menus.
        /// </summary>
        private void DisposeContextMenus()
        {
            for (int i = 0; i < sttcMain.DocumentsCount; i++)
            {
                // validate that the ScintillaTabbedDocument instance has a spell checker attached to it..
                if (sttcMain.Documents[i] != null && sttcMain.Documents[i].Scintilla.ContextMenuStrip != null)
                {
                    // unsubscribe the events from the context menu..
                    ScintillaContextMenu.UnsubscribeEvents(sttcMain.Documents[i].Scintilla);
                }
            }
        }

        /// <summary>
        /// Runs an action to the current document's <see cref="Scintilla"/> if there is a current document.
        /// </summary>
        /// <param name="action">The action to run.</param>
        public void CurrentScintillaAction(Action<Scintilla> action)
        {
            if (sttcMain.CurrentDocument != null)
            {
                try
                {
                    action(sttcMain.CurrentDocument.Scintilla);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogError(ex);
                }
            }
        }

        /// <summary>
        /// Runs an action to the current document if there is a current document.
        /// </summary>
        /// <param name="action">The action to run.</param>
        public void CurrentDocumentAction(Action<ScintillaTabbedDocument> action)
        {
            if (sttcMain.CurrentDocument != null)
            {
                try
                {
                    action(sttcMain.CurrentDocument);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogError(ex);
                }
            }
        }

        /// <summary>
        /// Runs an action to the last added if one exists.
        /// </summary>
        /// <param name="action">The action to run.</param>
        public void LastAddedDocumentAction(Action<ScintillaTabbedDocument> action)
        {
            if (sttcMain.LastAddedDocument != null)
            {
                try
                {
                    action(sttcMain.LastAddedDocument);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogError(ex);
                }
            }
        }

        /// <summary>
        /// Runs an action to the current document's <see cref="DBFILE_SAVE"/> class instance stored in the Tag property.
        /// </summary>
        /// <param name="action">The action to run.</param>
        public void CurrentFileSaveAction(Action<DBFILE_SAVE> action)
        {
            if (sttcMain.CurrentDocument != null && sttcMain.CurrentDocument.Tag != null)
            {
                if (sttcMain.CurrentDocument.Tag is DBFILE_SAVE fileSave)
                {
                    try
                    {
                        action(fileSave);
                        sttcMain.CurrentDocument.Tag = fileSave;
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.LogError(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Runs an action to the last added document's <see cref="DBFILE_SAVE"/> class instance stored in the Tag property.
        /// </summary>
        /// <param name="action">The action to run.</param>
        public void LastAddedFileSaveAction(Action<DBFILE_SAVE> action)
        {
            if (sttcMain.LastAddedDocument != null && sttcMain.LastAddedDocument.Tag != null)
            {
                if (sttcMain.LastAddedDocument.Tag is DBFILE_SAVE fileSave)
                {
                    try
                    {
                        action(fileSave);
                        sttcMain.LastAddedDocument.Tag = fileSave;
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.LogError(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Gets an item of type from a given object. Useful with events.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="sender">The sender of the event.</param>
        /// <returns>(T)<paramref name="sender"/>.</returns>
        public T ItemFromObj<T>(object sender)
        {
            return (T) sender;
        }

        /// <summary>
        /// The context menu of the scintilla called this method, so do some related stuff.
        /// </summary>
        /// <param name="scintilla">A <see cref="Scintilla"/> class instance from which the context menu strip called the undo method.</param>
        private void UndoFromExternal(Scintilla scintilla)
        {
            // if there is an active document..
            if (sttcMain.CurrentDocument != null && sttcMain.CurrentDocument.Scintilla.Equals(scintilla))
            {
                // get a DBFILE_SAVE class instance from the document's tag..
                DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;

                // undo the encoding change..
                fileSave.UndoEncodingChange();

                if (!sttcMain.CurrentDocument.Scintilla.CanUndo)
                {
                    fileSave.PopPreviousDbModified();
                    sttcMain.CurrentDocument.FileTabButton.IsSaved = IsFileChanged(fileSave);
                }
            }
            UpdateToolbarButtonsAndMenuItems();
        }

        /// <summary>
        /// The context menu of the scintilla called this method, so do some related stuff.
        /// </summary>
        /// <param name="scintilla">A <see cref="Scintilla"/> class instance from which the context menu strip called the redo method.</param>
        private void RedoFromExternal(Scintilla scintilla)
        {
            UpdateToolbarButtonsAndMenuItems();
        }

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
            // ReSharper disable once PossibleMultipleEnumeration
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
            var plugins = PluginDirectoryRoaming.GetPluginAssemblies(FormSettings.Settings.PluginFolder);

            // loop through the found plug-ins..
            foreach (var plugin in plugins)
            {
                // check if the plug-in is already in the database..
                var pluginEntry =
                    // ReSharper disable once PossibleMultipleEnumeration
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
                        pluginAssembly.Plugin.Locale = FormSettings.Settings.Culture.Name;
                    }

                    // try to initialize the plug-in..
                    if (PluginInitializer.InitializePlugin(pluginAssembly.Plugin,
                        RequestActiveDocument,
                        RequestAllDocuments,
                        PluginException, menuMain, 
                        mnuPlugins, 
                        CurrentSession, this))
                    {
                        pluginEntry = pluginEntry == null
                            ? PluginDatabaseEntry.FromPlugin(plugin.Assembly, pluginAssembly.Plugin, plugin.Path)
                            : PluginDatabaseEntry.UpdateFromPlugin(pluginEntry, plugin.Assembly, pluginAssembly.Plugin,
                                plugin.Path);

                        // on success, add the plug-in assembly and its instance to the internal list..
                        Plugins.Add((plugin.Assembly, pluginAssembly.Plugin, pluginEntry));
                    }

                    // update the possible version and the update time stamp..
                    if (pluginEntry != null)
                    {
                        pluginEntry.SetPluginUpdated(plugin.Assembly);

                        // update the plug-in information to the database..
                        DatabasePlugins.AddOrUpdatePlugin(pluginEntry);
                    }
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

            // dispose of the programming language menu helper..
            using (ProgrammingLanguageHelper)
            {
                // unsubscribe the programming language click event..
                ProgrammingLanguageHelper.LanguageMenuClick += ProgrammingLanguageHelper_LanguageMenuClick;                
            }            

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

            // set the flag for the diff viewer to allow the form to close..
            FormFileDiffView.ApplicationClosing = true;

            // dispose of the WinFormsFormMenuBuilder instance..
            using (WinFormsFormMenuBuilder)
            {
                WinFormsFormMenuBuilder = null;
            }

            // dispose of the tab menu..
            using (TabMenuBuilder)
            {
                TabMenuBuilder = null;
            }

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
                    ExceptionLogger.LogMessage($"Assembly load failed. Assembly: {(assembly == null ? "unknown" : assembly.FullName)}, FileName: {assemblyFile ?? "unknown"}.");
                    ExceptionLogger.LogError(ex);
                };

            PluginInitializer.ExceptionLogAction =
                delegate (Exception ex, Assembly assembly, string assemblyFile, string methodName)
                {
                    ExceptionLogger.LogMessage($"Plug-in assembly initialization failed. Assembly: {(assembly == null ? "unknown" : assembly.FullName)}, FileName: {assemblyFile ?? "unknown"}, Method: {methodName}.");
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

            // clean the old search path entries from the database..
            DatabaseMiscText.DeleteOlderEntries(MiscTextType.Path, FormSettings.Settings.FileSearchHistoriesLimit);

            // clean the old replace replace history entries from the database..
            DatabaseSearchAndReplace.DeleteOlderEntries("REPLACE_HISTORY", FormSettings.Settings.FileSearchHistoriesLimit,
                CurrentSession, 0, 1, 2, 3);

            // clean the old replace search history entries from the database..
            DatabaseSearchAndReplace.DeleteOlderEntries("SEARCH_HISTORY", FormSettings.Settings.FileSearchHistoriesLimit,
                CurrentSession, 0, 1, 2, 3);

            // close the main form as the call came from elsewhere than the FormMain_FormClosed event..
            if (noUserInteraction)
            {
                Close();
            }

            // dispose of the spell checkers attached to the documents..
            DisposeSpellCheckers();

            // unsubscribe the event handlers from the context menus..
            DisposeContextMenus();
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

            // dispose of the spell checkers attached to the documents..
            DisposeSpellCheckers();

            // unsubscribe the event handlers from the context menus..
            DisposeContextMenus();

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
                            DBLangEngine.GetMessage("msgFileHasChanged", "The file '{0}' has been changed. Reload from the file system?|As in the opened file has been changed outside the software so do as if a reload should happen", fileSave.FILENAME_FULL),
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
                            HandleCloseTab(fileSave, true, false, false,
                                sttcMain.Documents[i].Scintilla.CurrentPosition);
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
        /// <param name="tabClosing">A flag indicating whether this call was made from the tab closing event of a <see cref="ScintillaTabbedDocument"/> class instance.</param>
        /// <param name="closeTab">A flag indicating whether the tab containing the given <paramref name="fileSave"/> should be closed.</param>
        /// <param name="currentPosition">The position of the caret within the "file".</param>
        /// <returns>A modified <see cref="DBFILE_SAVE"/> class instance based on the given parameters.</returns>
        private void HandleCloseTab(DBFILE_SAVE fileSave, bool fileDeleted, bool tabClosing, bool closeTab,
            int currentPosition)
        {
            // disable the timers while a document is closing..
            EnableDisableTimers(false);

            // set the flags according to the parameters..
            fileSave.ISHISTORY = tabClosing || fileDeleted || closeTab; 

            // set the exists in file system flag..
            fileSave.EXISTS_INFILESYS = File.Exists(fileSave.FILENAME_FULL);

            fileSave.CURRENT_POSITION = currentPosition;

            // get the tabbed document index via the ID number..
            int docIndex = sttcMain.Documents.FindIndex(f => f.ID == fileSave.ID);

            ExceptionLogger.LogMessage($"Tab closing: {docIndex}");

            // this should never be -1 but do check still in case of a bug..
            if (docIndex != -1)
            {
                // unsubscribe the context menu event(s)..
                ScintillaContextMenu.UnsubscribeEvents(sttcMain.Documents[docIndex].Scintilla);

                // update the file save to the database..
                DatabaseFileSave.AddOrUpdateFile(fileSave, sttcMain.Documents[docIndex]);

                // update the file history list in the database..
                DatabaseRecentFiles.AddOrUpdateRecentFile(fileSave.FILENAME_FULL, fileSave.SESSIONNAME, fileSave.ENCODING);

                // validate that the ScintillaTabbedDocument instance has a spell checker attached to it..
                if (sttcMain.Documents[docIndex].Tag0 != null && sttcMain.Documents[docIndex].Tag0.GetType() == typeof(TabbedDocumentSpellCheck))
                {
                    // dispose of the spell checker
                    var spellCheck = (TabbedDocumentSpellCheck)sttcMain.Documents[docIndex].Tag0;

                    using (spellCheck)
                    {
                        sttcMain.Documents[docIndex].Tag0 = null;
                    }
                }

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

            // enable the timers after the document has closed..
            EnableDisableTimers(true);
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
                document.FileTabButton.IsSaved = fileSave.EXISTS_INFILESYS && IsFileChanged(fileSave);
            }
            UpdateToolbarButtonsAndMenuItems();
        }

        /// <summary>
        /// Determines whether the <see cref="DBFILE_SAVE"/> has changed in the editor vs. the file system.
        /// </summary>
        /// <param name="fileSave">The <see cref="DBFILE_SAVE"/> class to check for.</param>
        private bool IsFileChanged(DBFILE_SAVE fileSave)
        {
            return fileSave.EXISTS_INFILESYS &&
                   !(fileSave.EXISTS_INFILESYS && (fileSave.FILESYS_SAVED == DateTime.MinValue ||
                                                   fileSave.FILESYS_SAVED == fileSave.FILESYS_MODIFIED) &&
                     fileSave.FILESYS_MODIFIED < fileSave.DB_MODIFIED);
        }

        /// <summary>
        /// Updates the tool bar buttons and the menu items enabled states.
        /// </summary>
        private void UpdateToolbarButtonsAndMenuItems()
        {
            // get the active tab's Scintilla document
            CurrentScintillaAction(scintilla =>
            {
                tsbUndo.Enabled = scintilla.CanUndo;
                tsbRedo.Enabled = scintilla.CanRedo;

                mnuUndo.Enabled = scintilla.CanUndo;
                mnuRedo.Enabled = scintilla.CanRedo;

                tsbCopy.Enabled = scintilla.SelectedText.Length > 0;
                tsbPaste.Enabled = scintilla.CanPaste;
                tsbCut.Enabled = scintilla.SelectedText.Length > 0;

                mnuCopy.Enabled = scintilla.SelectedText.Length > 0;
                mnuCut.Enabled = scintilla.SelectedText.Length > 0;
                mnuPaste.Enabled = scintilla.CanPaste;
            });
        }
        #endregion

        #region UselessCode
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
                try
                {
                    DBFILE_SAVE fileSave = (DBFILE_SAVE) sttcMain.Documents[i].Tag;

                    fileSave.ISACTIVE = sttcMain.Documents[i].FileTabButton.IsActive;
                    fileSave.VISIBILITY_ORDER = i;
                    DatabaseFileSave.AddOrUpdateFile(fileSave, sttcMain.Documents[i]);
                    DatabaseRecentFiles.AddOrUpdateRecentFile(sttcMain.Documents[i].FileName, sessionName,
                        fileSave.ENCODING);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogError(ex);
                }
            }
        }

        /// <summary>
        /// Appends a possible style and a spell checking for the <see cref="ScintillaTabbedDocument"/> document.
        /// </summary>
        /// <param name="document">The document to append the possible style and a spell checking to.</param>
        private void AppendStyleAndSpellChecking(ScintillaTabbedDocument document)
        {
            // ReSharper disable once ObjectCreationAsStatement
            new TabbedDocumentSpellCheck(document);

            string fileName = FormSettings.NotepadPlusPlusStyleFile;

            // check if a user has selected a style definitions (Notepad++ style XML) file..
            if (File.Exists(fileName))
            {          
                // ..set the style for the document..
                ScintillaLexers.CreateLexerFromFile(document.Scintilla, document.LexerType, fileName);
            }
        }

        /// <summary>
        /// Sets the application title to indicate no active document.
        /// </summary>
        private void SetEmptyApplicationTitle()
        {
            // ReSharper disable once ArrangeThisQualifier
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
            // ReSharper disable once ArrangeThisQualifier
            this.Text =
                DBLangEngine.GetMessage("msgAppTitleWithFileName",
                "ScriptNotepad [{0}]|As in the application name combined with an active file name",
                document.FileName) +
                (ProcessElevation.IsElevated ? " (" +
                DBLangEngine.GetMessage("msgProcessIsElevated", "Administrator|A message indicating that a process is elevated.") + ")" : string.Empty);
        }

        /// <summary>
        /// Sets the document miscellaneous indicators.
        /// </summary>
        /// <param name="document">The <see cref="ScintillaTabbedDocument"/> document.</param>
        private void SetDocumentMiscIndicators(ScintillaTabbedDocument document)
        {
            // the spell checking enabled..
            // validate that the ScintillaTabbedDocument instance has a spell checker attached to it..
            if (document.Tag0 != null &&
                document.Tag0.GetType() == typeof(TabbedDocumentSpellCheck))
            {
                // get the TabbedDocumentSpellCheck class instance..
                var spellCheck = (TabbedDocumentSpellCheck) document.Tag0;

                // set the spell check enable/disable button to indicate the document's spell check state..
                tsbSpellCheck.Checked = spellCheck.Enabled;
            }
            else
            {
                // set the spell check enable/disable button to indicate the document's spell check state..
                tsbSpellCheck.Checked = false;                
            }

            // the percentage mark is also localizable (!)..
            sslbZoomPercentage.Text = (document.ZoomPercentage / 100.0) .ToString("P0", DBLangEngine.UseCulture);
        }

        private void SetCaretLineColor()
        {
            // enabled the caret line background color..
            sttcMain.LastAddedDocument.Scintilla.CaretLineVisible = true;

            // set the color for the caret line..
            sttcMain.LastAddedDocument.Scintilla.CaretLineBackColor =
                FormSettings.Settings.CurrentLineBackground;
        }

        /// <summary>
        /// Loads the document snapshots from the SQLite database.
        /// </summary>
        /// <param name="sessionName">A name of the session to which the documents are tagged with.</param>
        private void LoadDocumentsFromDatabase(string sessionName)
        {
            // set the status strip label's to indicate that there is no active document..
            StatusStripTexts.SetEmptyTexts(CurrentSession);

            // set the application title to indicate no active document..
            SetEmptyApplicationTitle();

            IEnumerable<DBFILE_SAVE> files = DatabaseFileSave.GetFilesFromDatabase(sessionName,
                DatabaseHistoryFlag.NotHistory, FormSettings.Settings.EditorSaveZoom);

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
                    // append additional initialization to the document..
                    AdditionalInitializeDocument(sttcMain.LastAddedDocument);
                    // set the saved position of the document's caret..
                    if (file.CURRENT_POSITION > 0 && file.CURRENT_POSITION < sttcMain.LastAddedDocument.Scintilla.TextLength)
                    {
                        sttcMain.LastAddedDocument.Scintilla.CurrentPosition = file.CURRENT_POSITION;
                        sttcMain.LastAddedDocument.Scintilla.SelectionStart = file.CURRENT_POSITION;
                        sttcMain.LastAddedDocument.Scintilla.SelectionEnd = file.CURRENT_POSITION;
                        sttcMain.LastAddedDocument.Scintilla.ScrollCaret();
                    }

                    sttcMain.LastAddedDocument.Scintilla.TabWidth = FormSettings.Settings.EditorTabWidth;

                    sttcMain.LastAddedDocument.Tag = file;

                    // append possible style and spell checking for the document..
                    AppendStyleAndSpellChecking(sttcMain.LastAddedDocument);

                    // set the lexer type from the saved database value..
                    sttcMain.LastAddedDocument.LexerType = file.LEXER_CODE;

                    SetSpellCheckerState(file.USESPELL_CHECK, true);

                    // enabled the caret line background color..
                    SetCaretLineColor();

                    // set the brace matching if enabled..
                    SetStyleBraceMatch.SetStyle(sttcMain.LastAddedDocument.Scintilla);

                    // set the context menu strip for the file tab..
                    sttcMain.LastAddedDocument.FileTabButton.ContextMenuStrip = cmsFileTab;

                    // the file load can't add an undo option the Scintilla..
                    sttcMain.LastAddedDocument.Scintilla.EmptyUndoBuffer();

                    // set the zoom value..
                    sttcMain.LastAddedDocument.ZoomPercentage = file.EDITOR_ZOOM;
                }            

                UpdateDocumentSaveIndicators();
            }

            if (activeDocument != string.Empty)
            {
                sttcMain.ActivateDocument(activeDocument);
            }

            UpdateToolbarButtonsAndMenuItems();

            // check if any files were changed in the file system..
            CheckFileSysChanges();
        }

        /// <summary>
        /// Sets the additional data for a <see cref="ScintillaTabbedDocument"/> upon opening or creating one.
        /// </summary>
        /// <param name="document">The document to initialize the additional data for.</param>
        private void AdditionalInitializeDocument(ScintillaTabbedDocument document)
        {
            // create a localizable context menu strip for the Scintilla..
            var menuStrip = ScintillaContextMenu.CreateBasicContextMenuStrip(document.Scintilla,
                UndoFromExternal, RedoFromExternal);

            // set the editor (Scintilla) properties from the saved settings..
            FormSettings.SetEditorSettings(document.Scintilla);

            // add the style mark menu to the context menu..
            ContextMenuStyles.CreateStyleMenu(menuStrip, StyleSelectOf_Click, ClearStyleOf_Click, ClearAllStyles_Click);

            // set the editor (Scintilla) properties from the saved settings..
            FormSettings.SetEditorSettings(document.Scintilla);

            // check the programming language menu item with the current lexer..
            ProgrammingLanguageHelper.CheckLanguage(document.LexerType);
        }

        /// <summary>
        /// Loads the document from the database based on a given <paramref name="recentFile"/> class instance.
        /// </summary>
        /// <param name="recentFile">A <see cref="RECENT_FILES"/> class instance containing the file data.</param>
        private void LoadDocumentFromDatabase(RECENT_FILES recentFile)
        {
            // get the file from the database..
            DBFILE_SAVE file = DatabaseFileSave.GetFileFromDatabase(recentFile.SESSIONNAME, recentFile.FILENAME_FULL,
                FormSettings.Settings.EditorSaveZoom);

            // only if something was gotten from the database..
            if (file != null)
            {
                sttcMain.AddDocument(file.FILENAME_FULL, (int)file.ID, file.ENCODING, StreamStringHelpers.TextToMemoryStream(file.FILE_CONTENTS, file.ENCODING));
                if (sttcMain.LastAddedDocument != null)
                {
                    // append additional initialization to the document..
                    AdditionalInitializeDocument(sttcMain.LastAddedDocument);

                    // set the lexer type from the saved database value..
                    sttcMain.LastAddedDocument.LexerType = file.LEXER_CODE;

                    // not history any more..
                    file.ISHISTORY = false;

                    // set the saved position of the document's caret..
                    if (file.CURRENT_POSITION > 0 && file.CURRENT_POSITION < sttcMain.LastAddedDocument.Scintilla.TextLength)
                    {
                        sttcMain.LastAddedDocument.Scintilla.CurrentPosition = file.CURRENT_POSITION;
                        sttcMain.LastAddedDocument.Scintilla.SelectionStart = file.CURRENT_POSITION;
                        sttcMain.LastAddedDocument.Scintilla.SelectionEnd = file.CURRENT_POSITION;
                        sttcMain.LastAddedDocument.Scintilla.ScrollCaret();
                    }

                    sttcMain.LastAddedDocument.Scintilla.TabWidth = FormSettings.Settings.EditorTabWidth;

                    // update the history flag to the database..
                    DatabaseFileSave.UpdateFileHistoryFlag(file);

                    // assign the context menu strip for the tabbed document..
                    sttcMain.LastAddedDocument.FileTabButton.ContextMenuStrip = cmsFileTab;

                    // set the zoom value..
                    sttcMain.LastAddedDocument.ZoomPercentage = file.EDITOR_ZOOM;

                    // enabled the caret line background color..
                    SetCaretLineColor();

                    // set the brace matching if enabled..
                    SetStyleBraceMatch.SetStyle(sttcMain.LastAddedDocument.Scintilla);

                    sttcMain.LastAddedDocument.Tag = file;

                    // the file load can't add an undo option the Scintilla..
                    sttcMain.LastAddedDocument.Scintilla.EmptyUndoBuffer();

                    // ReSharper disable once ObjectCreationAsStatement
                    new TabbedDocumentSpellCheck(sttcMain.LastAddedDocument);

                    // set the misc indicators..
                    SetDocumentMiscIndicators(sttcMain.LastAddedDocument);
                }
                sttcMain.ActivateDocument(file.FILENAME_FULL);

                UpdateToolbarButtonsAndMenuItems();

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

            // only send the existing files to the running instance, don't send the executable
            // file name thus the start from 1..
            for (int i = 1; i < args.Length; i++)
            {
                // a file must exist..
                if (File.Exists(args[i]))
                {
                    // add the file to the document control..
                    OpenDocument(args[i], DefaultEncodings, false, false);
                }
            }
        }

        /// <summary>
        /// Adds a new document in to the view.
        /// </summary>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        private void NewDocument()
        {
            // a false would happen if the document (file) can not be accessed or required permissions to access a file
            // would be missing (also a bug might occur)..
            if (sttcMain.AddNewDocument())
            {
                if (sttcMain.CurrentDocument != null) // if the document was added or updated to the control..
                {
                    // append additional initialization to the document..
                    AdditionalInitializeDocument(sttcMain.CurrentDocument);

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

                    sttcMain.LastAddedDocument.Scintilla.TabWidth = FormSettings.Settings.EditorTabWidth;

                    // assign the context menu strip for the tabbed document..
                    sttcMain.LastAddedDocument.FileTabButton.ContextMenuStrip = cmsFileTab;

                    // get a DBFILE_SAVE class instance from the document's tag..
                    DBFILE_SAVE fileSave = (DBFILE_SAVE)sttcMain.CurrentDocument.Tag;

                    // save the DBFILE_SAVE class instance to the Tag property..
                    sttcMain.CurrentDocument.Tag = DatabaseFileSave.AddOrUpdateFile(fileSave, sttcMain.CurrentDocument);

                    // append possible style and spell checking for the document..
                    AppendStyleAndSpellChecking(sttcMain.LastAddedDocument);

                    // set the brace matching if enabled..
                    SetStyleBraceMatch.SetStyle(sttcMain.LastAddedDocument.Scintilla);

                    // the default spell checking state..
                    SetSpellCheckerState(FormSettings.Settings.EditorUseSpellCheckingNewFiles, false);
                }
            }
        }

        /// <summary>
        /// Opens the document with a given file name into the view.
        /// </summary>
        /// <param name="fileName">Name of the file to load into the view.</param>
        /// <param name="encoding">The encoding to be used to open the file.</param>
        /// <param name="reloadContents">An indicator if the contents of the document should be reloaded from the file system.</param>
        /// <param name="encodingOverridden">The given encoding should be used while opening the file.</param>
        /// <param name="overrideDetectBom">if set to <c>true</c> the setting value whether to detect unicode file with no byte-order-mark (BOM) is overridden.</param>
        private void OpenDocument(string fileName,
            Encoding encoding,
            bool reloadContents, bool encodingOverridden,
            bool overrideDetectBom = false)
        {
            var encodingList =
                new List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)>();

            encodingList.Add((encoding.WebName, encoding, false, false));

            OpenDocument(fileName, encodingList, reloadContents, encodingOverridden, overrideDetectBom);
        }


        /// <summary>
        /// Opens the document with a given file name into the view.
        /// </summary>
        /// <param name="fileName">Name of the file to load into the view.</param>
        /// <param name="encodings">The encodings to be used to try to open the file.</param>
        /// <param name="reloadContents">An indicator if the contents of the document should be reloaded from the file system.</param>
        /// <param name="encodingOverridden">The given encoding should be used while opening the file.</param>
        /// <param name="overrideDetectBom">if set to <c>true</c> the setting value whether to detect unicode file with no byte-order-mark (BOM) is overridden.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        private void OpenDocument(string fileName, List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)> encodings, bool reloadContents, bool encodingOverridden,
            bool overrideDetectBom = false)
        {
            Encoding encoding = null;
            if (File.Exists(fileName))
            {
                try
                {
                    foreach (var encodingData in encodings)
                    {
                        // the encoding shouldn't change based on the file's contents if a snapshot of the file already exists in the database..
                        encoding = GetFileEncoding(CurrentSession, fileName, encodingData.encoding, reloadContents, encodingOverridden, 
                            overrideDetectBom,
                            out _,
                            out _, out _);

                        if (encoding != null)
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        DBLangEngine.GetMessage("msgErrorOpeningFile", "Error opening file '{0}' with message: '{1}'.|Some kind of error occurred while opening a file.",
                            fileName, ex.GetBaseException().Message),
                        DBLangEngine.GetMessage("msgError",
                            "Error|A message describing that some kind of error occurred."), MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }

                // a false would happen if the document (file) can not be accessed or required permissions to access a file
                // would be missing (also a bug might occur)..
                bool addSuccess = sttcMain.AddDocument(fileName, -1, encoding);

                if (addSuccess)
                {
                    if (sttcMain.LastAddedDocument != null) // if the document was added or updated to the control..
                    {
                        // append additional initialization to the document..
                        AdditionalInitializeDocument(sttcMain.LastAddedDocument);

                        // check the database first for a DBFILE_SAVE class instance..
                        DBFILE_SAVE fileSave = DatabaseFileSave.GetFileFromDatabase(CurrentSession, fileName,
                            FormSettings.Settings.EditorSaveZoom);

                        if (sttcMain.LastAddedDocument.Tag == null && fileSave == null)
                        {
                            sttcMain.LastAddedDocument.Tag = DatabaseFileSave.AddOrUpdateFile(
                                sttcMain.LastAddedDocument, DatabaseHistoryFlag.DontCare, CurrentSession,
                                CurrentSessionID, encoding);
                        }
                        else if (fileSave != null)
                        {
                            sttcMain.LastAddedDocument.Tag = fileSave;
                            sttcMain.LastAddedDocument.ID = (int)fileSave.ID;
                            fileSave.ENCODING = encoding;

                            // set the saved position of the document's caret..
                            if (fileSave.CURRENT_POSITION > 0 && fileSave.CURRENT_POSITION < sttcMain.LastAddedDocument.Scintilla.TextLength)
                            {
                                sttcMain.LastAddedDocument.Scintilla.CurrentPosition = fileSave.CURRENT_POSITION;
                                sttcMain.LastAddedDocument.Scintilla.SelectionStart = fileSave.CURRENT_POSITION;
                                sttcMain.LastAddedDocument.Scintilla.SelectionEnd = fileSave.CURRENT_POSITION;
                                sttcMain.LastAddedDocument.Scintilla.ScrollCaret();
                            }
                        }

                        sttcMain.LastAddedDocument.Scintilla.TabWidth = FormSettings.Settings.EditorTabWidth;

                        // get a DBFILE_SAVE class instance from the document's tag..
                        fileSave = (DBFILE_SAVE)sttcMain.LastAddedDocument.Tag;

                        // set the session ID number..
                        fileSave.SESSIONID = CurrentSessionID;

                        // not history at least anymore..
                        fileSave.ISHISTORY = false;

                        // ..update the database with the document..
                        fileSave = DatabaseFileSave.AddOrUpdateFile(fileSave, sttcMain.LastAddedDocument);
                        sttcMain.LastAddedDocument.ID = (int)fileSave.ID;

                        if (reloadContents)
                        {
                            fileSave.ReloadFromDisk(sttcMain.LastAddedDocument);
                        }

                        // save the DBFILE_SAVE class instance to the Tag property..
                        // USELESS CODE?::fileSave = Database.Database.AddOrUpdateFile(sttcMain.CurrentDocument, DatabaseHistoryFlag.DontCare, CurrentSession, fileSave.ENCODING);
                        sttcMain.LastAddedDocument.Tag = fileSave;

                        // append possible style and spell checking for the document..
                        AppendStyleAndSpellChecking(sttcMain.LastAddedDocument);

                        // set the lexer type from the saved database value..
                        sttcMain.LastAddedDocument.LexerType = fileSave.LEXER_CODE;

                        // enabled the caret line background color..
                        SetCaretLineColor();

                        // assign the context menu strip for the tabbed document..
                        sttcMain.LastAddedDocument.FileTabButton.ContextMenuStrip = cmsFileTab;

                        // the file load can't add an undo option the Scintilla..
                        sttcMain.LastAddedDocument.Scintilla.EmptyUndoBuffer();

                        // set the misc indicators..
                        SetDocumentMiscIndicators(sttcMain.LastAddedDocument);

                        // set the brace matching if enabled..
                        SetStyleBraceMatch.SetStyle(sttcMain.LastAddedDocument.Scintilla);

                        // set the zoom value..
                        sttcMain.LastAddedDocument.ZoomPercentage = fileSave.EDITOR_ZOOM;

                        // check the programming language menu item with the current lexer..
                        ProgrammingLanguageHelper.CheckLanguage(sttcMain.LastAddedDocument.LexerType);

                        // the default spell checking state..
                        SetSpellCheckerState(FormSettings.Settings.EditorUseSpellChecking, false);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the document in to the file system.
        /// </summary>
        /// <param name="document">The document to be saved.</param>
        /// <param name="saveAs">An indicator if the document should be saved as a new file.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        private void SaveDocument(ScintillaTabbedDocument document, bool saveAs)
        {
            try
            {
                // check that the given parameter is valid..
                if (document?.Tag != null)
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
                        sdAnyFile.Title = DBLangEngine.GetMessage("msgDialogSaveAs",
                            "Save as|A title for a save file dialog to indicate user that a file is being saved as with a new file name");

                        sdAnyFile.InitialDirectory = FormSettings.Settings.FileLocationSaveAs;

                        sdAnyFile.FileName = fileSave.FILENAME_FULL;
                        if (sdAnyFile.ShowDialog() == DialogResult.OK)
                        {
                            FormSettings.Settings.FileLocationSaveAs = Path.GetDirectoryName(sdAnyFile.FileName);

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

                            // update document misc data, i.e. the assigned lexer to the database..
                            DatabaseFileSave.UpdateMiscFlags(fileSave);

                            document.Tag = fileSave;
                        }
                        else
                        {
                            // the user canceled the file save dialog..
                            return;
                        }
                    }

                    // update the saved indicators..
                    UpdateDocumentSaveIndicators();
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);
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
        // a user wishes to search for an open file within the tabbed documents..
        private void MnuFindTab_Click(object sender, EventArgs e)
        {
            FormDialogSelectFileTab.ShowDialog(sttcMain);
        }

        // copy, paste and cut handler for the tool/menu strip..
        private void TsbCopyPasteCut_Click(object sender, EventArgs e)
        {
            CurrentScintillaAction(scintilla =>
            {
                if (sender.Equals(tsbCut) || sender.Equals(mnuCut))
                {
                    scintilla.Cut();
                }

                if (sender.Equals(tsbCopy) || sender.Equals(mnuCopy))
                {
                    scintilla.Copy();
                }

                if (sender.Equals(tsbPaste) || sender.Equals(mnuPaste))
                {
                    scintilla.Paste();
                }
            });
        }

        // printing (print)..
        private void TsbPrint_Click(object sender, EventArgs e)
        {
            CurrentScintillaAction(scintilla =>
            {
                if (pdPrint.ShowDialog() == DialogResult.OK)
                {
                    var print = new ScintillaPrinting.PrintDocument(scintilla)
                    {
                        PrinterSettings = pdPrint.PrinterSettings
                    };
                    print.Print();
                }
            });
        }

        // printing (preview)..
        private void TsbPrintPreview_Click(object sender, EventArgs e)
        {
            CurrentScintillaAction(scintilla =>
            {
                var print = new ScintillaPrinting.Printing(scintilla);
                print.PrintPreview(this);
            });
        }

        // the user wants to change the document's zoom value..
        private void ZoomInOut_Click(object sender, EventArgs e)
        {
            if (sender.Equals(tsbZoomIn))
            {
                sttcMain.CurrentDocument.ZoomPercentage += 10;
            }
            else if (sender.Equals(tsbZoomOut))
            {
                sttcMain.CurrentDocument.ZoomPercentage -= 10;
            }
        }

        // the user wishes to either save or to add a new document of the
        // current document formatted as HTML..
        private void MnuExportAsHTMLToNewDocument_Click(object sender, EventArgs e)
        {
            string html;
            CurrentScintillaAction(scintilla =>
            {
                // get the HTML from either the selection or from the whole document
                // depending if text is selected..
                if (scintilla.SelectedText.Length > 0)
                {
                    // ..no selection..
                    html = scintilla.GetTextRangeAsHtml(scintilla.SelectionStart,
                        scintilla.SelectionEnd - scintilla.SelectionStart);
                }
                else
                {
                    // ..get the selected text as HTML..
                    html = scintilla.GetTextRangeAsHtml(0, scintilla.TextLength);
                }
                
                // the user wants to save the HTML directly into a HTML file..
                if (sender.Equals(mnuHTMLToFile) || sender.Equals(mnuHTMLToFileExecute))
                {
                    sdHTML.InitialDirectory = FormSettings.Settings.FileLocationSaveAsHTML;
                    if (sdHTML.ShowDialog() == DialogResult.OK)
                    {
                        FormSettings.Settings.FileLocationSaveAsHTML = Path.GetDirectoryName(sdHTML.FileName);
                        File.WriteAllText(sdHTML.FileName, html, Encoding.UTF8);
                    }

                    // the user wants to display the saved HTML file in a web browser..
                    if (sender.Equals(mnuHTMLToFileExecute))
                    {
                        Process.Start(sdHTML.FileName);
                    }

                    return;
                }
                
                // the user wants the HTML to clipboard..
                if (sender.Equals(mnuHTMLToClipboard))
                {
                    // save the HTML to the clipboard..
                    ClipboardTextHelper.ClipboardSetText(html);
                    return;
                }

                // the user wants the HTML to a new tab..
                NewDocument();

                LastAddedDocumentAction(document =>
                {
                    // set the recently added document contents..
                    document.Scintilla.Text = html;

                    // as the contents os HTML, do set the lexer correctly..
                    document.LexerType = LexerEnumerations.LexerType.HTML;

                    LastAddedFileSaveAction(fileSave => { fileSave.LEXER_CODE = LexerEnumerations.LexerType.HTML; });

                    LastAddedFileSaveAction(fileSave => { DatabaseFileSave.UpdateMiscFlags(fileSave); });

                    // check the programming language menu item with the current lexer..
                    ProgrammingLanguageHelper.CheckLanguage(document.LexerType);
                });
            });
        }

        // the auto-save timer..
        private void TmAutoSave_Tick(object sender, EventArgs e)
        {
            tmAutoSave.Enabled = false;
            // save the current session's documents to the database..
            SaveDocumentsToDatabase(CurrentSession);
            tmAutoSave.Enabled = true;
        }

        // a user is dragging a file over the ScintillaTabbedControl instance..
        private void SttcMain_DragEnterOrOver(object sender, DragEventArgs e)
        {
            // set the effect to Copy == Accept..
            e.Effect = 
                e.Data.GetDataPresent(DataFormats.FileDrop) ? 
                    DragDropEffects.Copy : DragDropEffects.None;
        }

        // a user dropped file(s) over the ScintillaTabbedControl instance..
        private void SttcMain_DragDrop(object sender, DragEventArgs e)
        {
            // verify the data format..
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // get the dropped files and directories..
                string[] dropFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                // loop thought the files and directories
                foreach (string filePath in dropFiles)
                {
                    if (File.Exists(filePath))
                    {
                        OpenDocument(filePath, DefaultEncodings, false, false);
                    }
                }
            }
        }

        // the user clicked the zoom percentage label or the zoom percentage value
        // label on the tool strip --> reset the zoom..
        private void ResetZoom_Click(object sender, EventArgs e)
        {
            sttcMain.CurrentDocument.ZoomPercentage = 100;
        }

        // the document's zoom has changed, so do display the value..
        // NOTE: Ctrl+NP+, Ctrl+NP- and Control+NP/ and Control+mouse wheel control the zoom of the Scintilla..
        private void SttcMain_DocumentZoomChanged(object sender, ScintillaZoomChangedEventArgs e)
        {
            // check that the initialization of this class is done..
            if (!ConstructorFinished)
            {
                // ..if not, return..
                return;
            }

            // the percentage mark is also localizable (!)..
            sslbZoomPercentage.Text = (e.ZoomPercentage / 100.0).ToString("P0", DBLangEngine.UseCulture);

            CurrentDocumentAction(document =>
            {
                if (document.Tag != null)
                {
                    var fileSave = (DBFILE_SAVE) document.Tag;
                    fileSave.EDITOR_ZOOM = e.ZoomPercentage;
                    DatabaseFileSave.UpdateMiscFlags(fileSave);
                }
            });
        }

        // a user wishes to change the lexer of the current document..
        private void ProgrammingLanguageHelper_LanguageMenuClick(object sender, ProgrammingLanguageMenuClickEventArgs e)
        {
            CurrentDocumentAction(document =>
            {
                document.LexerType = e.LexerType;
                if (document.Tag != null)
                {
                    DBFILE_SAVE fileSave = (DBFILE_SAVE) document.Tag;
                    fileSave.LEXER_CODE = e.LexerType;
                    DatabaseFileSave.UpdateMiscFlags(fileSave);
                }
            });
        }

        // a user wishes to alphabetically order the the lines of the active document..
        private void MnuSortLines_Click(object sender, EventArgs e)
        {
            CurrentScintillaAction(scintilla =>
            {
                // if text is selected, do the ordering with a bit more complex algorithm..
                if (scintilla.SelectedText.Length > 0)
                {
                    // save the selection start into a variable..
                    int selStart = scintilla.SelectionStart;

                    // save the start line index of the selection into a variable..
                    int startLine = scintilla.LineFromPosition(selStart);

                    // adjust the selection start to match the actual line start of the selection..
                    selStart = scintilla.Lines[startLine].Position;

                    // save the selection end into a variable..
                    int selEnd = scintilla.SelectionEnd;

                    // save the end line index of the selection into a variable..
                    int endLine = scintilla.LineFromPosition(selEnd);

                    // adjust the selection end to match the actual line end of the selection..
                    selEnd = scintilla.Lines[endLine].EndPosition;

                    // reset the selection with the "corrected" values..
                    scintilla.SelectionStart = selStart;
                    scintilla.SelectionEnd = selEnd;

                    // get the lines in the selection and order the lines alphabetically with LINQ..
                    var lines = scintilla.Lines.Where(f => f.Index >= startLine && f.Index <= endLine)
                        .OrderBy(f => f.Text.ToLowerInvariant()).Select(f => f.Text);

                    // replace the modified selection with the sorted lines..
                    scintilla.ReplaceSelection(string.Join("", lines));

                    // get the "new" selection start..
                    selStart = scintilla.Lines[startLine].Position;

                    // get the "new" selection end..
                    selEnd = scintilla.Lines[endLine].EndPosition;

                    // set the "new" selection..
                    scintilla.SelectionStart = selStart;
                    scintilla.SelectionEnd = selEnd;
                }
                // somehow the whole document is easier..
                else
                {
                    // just LINQ it to sorted list..
                    var lines = scintilla.Lines.OrderBy(f => f.Text.ToLowerInvariant()).Select(f => f.Text);

                    // set the text..
                    scintilla.Text = string.Join("", lines);                    
                }
            });
        }

        // a timer to run a spell check for a Scintilla document if it has
        // been changed and the user has been idle for the timer's interval..
        private void TmSpellCheck_Tick(object sender, EventArgs e)
        {
            tmSpellCheck.Enabled = false; // disable the timer..
            CurrentDocumentAction(document =>
            {
                // validate that the ScintillaTabbedDocument instance has a spell checker attached to it..
                if (document.Tag0 != null &&
                    document.Tag0.GetType() == typeof(TabbedDocumentSpellCheck))
                {
                    // get the TabbedDocumentSpellCheck class instance..
                    var spellCheck = (TabbedDocumentSpellCheck) document.Tag0;

                    // check the document's spelling..
                    spellCheck.DoSpellCheck();
                }
            });
            tmSpellCheck.Enabled = true; // enabled the timer..
        }

        // a user wishes to temporarily disable or enable the spell checking of the current document..
        private void TsbSpellCheck_Click(object sender, EventArgs e)
        {
            // set the state of the spell checking functionality..
            SetSpellCheckerState(((ToolStripButton) sender).Checked, false);
        }

        // a user wishes to wrap the text to a specific line length..
        private void MnuWrapDocumentTo_Click(object sender, EventArgs e)
        {
            // query the line length..
            int wrapSize = FormDialogQueryNumber.Execute<int>(72,50, 150,
                DBLangEngine.GetMessage("msgWrapText",
                    "Wrap text|A message describing that some kind of wrapping is going to be done to some text"),
                DBLangEngine.GetMessage("msgWrapTextToLength",
                    "Wrap text to length:|A message describing that a text should be wrapped so the its lines have a specified maximum length"));

            // if the user accepted the dialog, wrap either the selection or the whole document..
            if (wrapSize != default)
            {
                CurrentScintillaAction(scintilla =>
                {
                    if (scintilla.SelectedText.Length > 0)
                    {
                        scintilla.ReplaceSelection(WordWrapToSize.Wrap(scintilla.SelectedText, wrapSize));
                    }
                    else
                    {
                        scintilla.Text = WordWrapToSize.Wrap(scintilla.Text, wrapSize);                        
                    }
                });
            }
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
            else if (!e.SearchResult.isFileOpen)
            {              
                OpenDocument(e.SearchResult.fileName, DefaultEncodings, false, false);
                var scintilla = sttcMain.LastAddedDocument?.Scintilla;
                if (scintilla != null)
                {
                    scintilla.GotoPosition(e.SearchResult.startLocation);
                    scintilla.CurrentPosition = e.SearchResult.startLocation;
                    scintilla.SetSelection(e.SearchResult.startLocation, e.SearchResult.startLocation + e.SearchResult.length);

                    var formSearchResultTree = (FormSearchResultTree) sender;
                    formSearchResultTree.SetFileOpened(e.SearchResult.fileName, true);
                    Focus();
                }
            }
        }

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
                FormSettings.Settings.CurrentSession = session.SESSIONNAME;

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
        public static Action<string> ModuleExceptionHandler { get; set; }

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
            // log the session ending..
            ExceptionLogger.LogMessage("The Windows session is ending --> save with no questions asked.");

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
                OpenDocument(e.RecentFile.FILENAME_FULL, e.RecentFile.ENCODING, false, false);
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
                        OpenDocument(recentFile.FILENAME_FULL, recentFile.ENCODING, false, false);
                    }
                }
            }

        }

        // a user wishes to change the settings of the software..
        private void mnuSettings_Click(object sender, EventArgs e)
        {
            // ..so display the settings dialog..
            FormSettings.Execute();
        }

        /// <summary>Processes a command key.</summary>
        /// <param name="msg">A <see cref="T:System.Windows.Forms.Message"/>, passed by reference, that represents the Win32 message to process.</param>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the key to process.</param>
        /// <returns>
        ///   <span class="keyword">
        ///     <span class="languageSpecificText">
        ///       <span class="cs">true</span>
        ///       <span class="vb">True</span>
        ///       <span class="cpp">true</span>
        ///     </span>
        ///   </span>
        ///   <span class="nu">
        ///     <span class="keyword">true</span> (<span class="keyword">True</span> in Visual Basic)</span> if the keystroke was processed and consumed by the control; otherwise, <span class="keyword"><span class="languageSpecificText"><span class="cs">false</span><span class="vb">False</span><span class="cpp">false</span></span></span><span class="nu"><span class="keyword">false</span> (<span class="keyword">False</span> in Visual Basic)</span> to allow further processing.
        /// </returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // somehow the software main form doesn't register keyboard keys of AltGr+2, AltGr+3, AltGr+4 on a finnish keyboard
            // so bypass it by the hard way..
            if (FormSettings.Settings.SimulateAltGrKey && FormSettings.Settings.SimulateAltGrKeyIndex == 0)
            {
                // something in the main form is capturing the "ALT GR" key - which at least in Finland is used to type few
                // necessary characters such as '@', '£' and '$', so make the software do this..
                if (keyData == (Keys.Control | Keys.Alt | Keys.D2))
                {
                    // the at sign ('@')..
                    CurrentScintillaAction(scintilla => { scintilla.ReplaceSelection("@"); });
                    return true;
                }

                // the pound sign ('£')..
                if (keyData == (Keys.Control | Keys.Alt | Keys.D3))
                {
                    CurrentScintillaAction(scintilla => { scintilla.ReplaceSelection("£"); });
                    return true;
                }

                // the dollar sign ('$')..
                if (keyData == (Keys.Control | Keys.Alt | Keys.D4))
                {
                    CurrentScintillaAction(scintilla => { scintilla.ReplaceSelection("$"); });
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCodeIn(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.PageDown, Keys.PageUp))
            {
                // release the flag which suspends the selection update to avoid excess CPU load..
                suspendSelectionUpdate = false;
                StatusStripTexts.SetStatusStringText(sttcMain.CurrentDocument, CurrentSession);
            }

            if (
                // a user pressed a keyboard combination of CTRL+Z, which indicates undo for
                // the Scintilla control..
                e.KeyCode == Keys.Z && e.OnlyControl() ||
                // a user pressed a keyboard combination of CTRL+Y, which indicates redo for
                // the Scintilla control..
                e.KeyCode == Keys.Y && e.OnlyControl())
            {
                // if there is an active document..
                CurrentDocumentAction(document =>
                {
                    // ..then if the undo is possible..
                    if (!document.Scintilla.CanUndo)
                    {
                        // get a DBFILE_SAVE class instance from the document's tag..
                        DBFILE_SAVE fileSave = (DBFILE_SAVE) document.Tag;

                        if (e.KeyCode == Keys.Z)
                        {
                            // undo the encoding change..
                            fileSave.UndoEncodingChange();
                            document.Tag = fileSave;
                        }

                        fileSave.PopPreviousDbModified();
                        document.FileTabButton.IsSaved = IsFileChanged(fileSave);
                    }
                });

                UpdateToolbarButtonsAndMenuItems();
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
            Invoke(new MethodInvoker(delegate
            {
                OpenDocument(e.Message, DefaultEncodings, false, false);
                // the user probably will like the program to show up, if the software activates
                // it self from a windows shell call to open a file..
                BringToFront();
                Activate();
            }));
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
            FormSearchAndReplace.ShowSearch(this, SearchString);
        }

        // a user wanted to find or find and replace something of the active document..
        private void MnuReplace_Click(object sender, EventArgs e)
        {
            FormSearchAndReplace.ShowReplace(this, SearchString);
        }

        // a user wanted to find or find and replace something in a defined set of files..
        private void MnuFindInFiles_Click(object sender, EventArgs e)
        {
            FormSearchAndReplace.ShowFindInFiles(this, SearchString);
        }

        // a user wanted to find and mark words of the current document..
        private void MnuMarkText_Click(object sender, EventArgs e)
        {
            FormSearchAndReplace.ShowMarkMatches(this, SearchString);
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

            // disable the timers not mess with application exit..
            tmAutoSave.Enabled = false;
            tmGUI.Enabled = false;
            tmSpellCheck.Enabled = false;

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
        private bool TextChangedViaEncodingChange { get; set; }

        /// <summary>
        /// A common method to change or convert the encoding of the active document.
        /// </summary>
        /// <param name="encoding">The encoding to change or convert into.</param>
        /// <param name="unicodeFailInvalidCharacters">In case of Unicode (UTF8, Unicode or UTF32) whether to fail on invalid characters.</param>
        internal void ChangeDocumentEncoding(Encoding encoding, bool unicodeFailInvalidCharacters)
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
            var encoding = FormDialogQueryEncoding.Execute(out _,
                out bool unicodeFailInvalidCharacters);
            ChangeDocumentEncoding(encoding, unicodeFailInvalidCharacters);
        }

        // an event which is fired if an encoding menu item is clicked..
        private void CharacterSetMenuBuilder_EncodingMenuClicked(object sender, EncodingMenuClickEventArgs e)
        {
            // a user requested to change the encoding of the file..
            if (e.Data != null && e.Data.ToString() == "convert_encoding")
            {
                ChangeDocumentEncoding(e.Encoding, true);
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

        /// <summary>
        /// Gets or sets a value indicating whether the form has shown once.
        /// </summary>
        /// <value><c>true</c> if [form first shown]; otherwise, <c>false</c>.</value>
        private bool FormFirstShown { get; set; }

        // the form is shown..
        private void FormMain_Shown(object sender, EventArgs e)
        {
            // ..so open the files given as arguments for the program..
            OpenArgumentFiles();

            // check for a new version from the internet..
            CheckForNewVersion();

            FormFirstShown = true;
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
            odAnyFile.Title = DBLangEngine.GetMessage("msgDialogOpenFile",
                "Open|A title for an open file dialog to indicate that a is user selecting a file to be opened");

            odAnyFile.InitialDirectory = FormSettings.Settings.FileLocationOpen;

            // if the file dialog was accepted (i.e. OK) then open the file to the view..
            if (odAnyFile.ShowDialog() == DialogResult.OK)
            {
                FormSettings.Settings.FileLocationOpen = odAnyFile.InitialDirectory;
                if (sender.Equals(mnuOpenNoBOM))
                {
                    OpenDocument(odAnyFile.FileName, DefaultEncodings, true, false, true);
                }
                else
                {
                    OpenDocument(odAnyFile.FileName, DefaultEncodings, false, false);
                }
            }
        }

        // a user wanted to open a file with encoding via the main menu..
        private void mnuOpenWithEncoding_Click(object sender, EventArgs e)
        {
            odAnyFile.Title = DBLangEngine.GetMessage("msgDialogOpenFileWithEncoding",
                "Open with encoding|A title for an open file dialog to indicate that a is user selecting a file to be opened with pre-selected encoding");

            // ask the encoding first from the user..
            Encoding encoding = FormDialogQueryEncoding.Execute(out _, out _);
            if (encoding != null)
            {
                odAnyFile.InitialDirectory = FormSettings.Settings.FileLocationOpenWithEncoding;

                // if the file dialog was accepted (i.e. OK) then open the file to the view..
                if (odAnyFile.ShowDialog() == DialogResult.OK)
                {
                    FormSettings.Settings.FileLocationOpenWithEncoding = Path.GetDirectoryName(odAnyFile.FileName);
                    OpenDocument(odAnyFile.FileName, encoding, false, true);
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
            tmGUI.Enabled = false;
            // release the flag which suspends the selection update to avoid excess CPU load..
            suspendSelectionUpdate = false;

            // this event in this case leads to an endless loop..
            Activated -= FormMain_Activated;

            CheckFileSysChanges();

            // this event in this case leads to an endless loop..
            Activated += FormMain_Activated;

            // start the timer to bring the main form to the front..
            leftActivatedEvent = true;
            tmGUI.Enabled = true;
        }

        // a tab is closing so save it into the history..
        private void sttcMain_TabClosing(object sender, TabClosingEventArgsExt e)
        {
            // call the handle method..
            HandleCloseTab((DBFILE_SAVE) e.ScintillaTabbedDocument.Tag, false, true, false,
                e.ScintillaTabbedDocument.Scintilla.CurrentPosition);

            // if there are no documents any more..
            if (sttcMain.DocumentsCount - 1 <= 0) 
            {
                // set the status strip label's to indicate that there is no active document..
                StatusStripTexts.SetEmptyTexts(CurrentSession);

                // set the application title to indicate no active document..
                SetEmptyApplicationTitle();
            }

            // re-create the tab menu..
            TabMenuBuilder.CreateMenuOpenTabs();
        }

        // a user activated a tab (document) so display it's file name..
        private void sttcMain_TabActivated(object sender, TabActivatedEventArgs e)
        {
            // set the application title to indicate the currently active document..
            SetApplicationTitle(e.ScintillaTabbedDocument);

            SetDocumentMiscIndicators(e.ScintillaTabbedDocument);

            StatusStripTexts.SetDocumentSizeText(e.ScintillaTabbedDocument);

            StatusStripTexts.SetStatusStringText(e.ScintillaTabbedDocument, CurrentSession);

            // re-create the tab menu..
            TabMenuBuilder?.CreateMenuOpenTabs();

            // check the programming language menu item with the current lexer..
            ProgrammingLanguageHelper.CheckLanguage(e.ScintillaTabbedDocument.LexerType);

            // the search must be re-set..
            CurrentDocumentAction(document =>
            {
                FormSearchAndReplace.Instance.CreateSingleSearchReplaceAlgorithm(
                    (document.Scintilla, document.FileName));
            });
            
            UpdateToolbarButtonsAndMenuItems();
        }

        // a tab was closed for various reasons..
        private void SttcMain_TabClosed(object sender, TabClosedEventArgs e)
        {
            // re-create the tab menu..
            TabMenuBuilder?.CreateMenuOpenTabs();
        }

        // a user wanted to see an about dialog of the software..
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            // ReSharper disable once ObjectCreationAsStatement
            new FormAbout(this, "MIT",
                "https://raw.githubusercontent.com/VPKSoft/ScriptNotepad/master/LICENSE",
                "https://www.vpksoft.net/versions/version.php");
        }

        // this is the event listener for the ScintillaTabbedDocument's selection and caret position change events..
        private void sttcMain_SelectionCaretChanged(object sender, ScintillaTabbedDocumentEventArgsExt e)
        {
            // set the search and replace from selection flag..
            if (ActiveForm != null && (e.ScintillaTabbedDocument.SelectionLength > 0 && ActiveForm.Equals(this)))
            {
                FormSearchAndReplace.Instance.SelectionChangedFromMainForm = true;
            }
            else
            {
                FormSearchAndReplace.Instance.SelectionChangedFromMainForm = false;
            }

            if (e.ScintillaTabbedDocument.Scintilla.SelectionEnd == e.ScintillaTabbedDocument.Scintilla.SelectionStart)
            {
                Highlight.ClearStyle(e.ScintillaTabbedDocument.Scintilla, 8);
            }
            
            if (!suspendSelectionUpdate)
            {
                StatusStripTexts.SetStatusStringText(e.ScintillaTabbedDocument, CurrentSession);
            }

            UpdateToolbarButtonsAndMenuItems();
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

            // update the search if the user manually modified the contents of the document..
            FormSearchAndReplace.Instance.UpdateSearchContents(e.ScintillaTabbedDocument.Scintilla.Text,
                e.ScintillaTabbedDocument.FileName);

            UpdateToolbarButtonsAndMenuItems();
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
            Undo();
        }

        // a user wishes to redo changes..
        private void tsbRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        // a timer to prevent an endless loop with the form activated event (probably a poor solution)..
        private void tmGUI_Tick(object sender, EventArgs e)
        {
            tmGUI.Enabled = false;
            if (bringToFrontQueued && leftActivatedEvent ||
                FormSearchAndReplace.Instance.ShouldMakeTopMost(true) && leftActivatedEvent) 
            {
                // this event in this case leads to an endless loop..
                Activated -= FormMain_Activated;

                if (FormSearchAndReplace.Instance.ShouldMakeTopMost(true))
                {
                    FormSearchAndReplace.Instance.ToggleStayTop(true);
                }

                // bring the form to the front..
                BringToFront();
                Activate();

                // this event in this case leads to an endless loop..
                Activated += FormMain_Activated;
            }

            bringToFrontQueued = false;
            leftActivatedEvent = false;
            tmGUI.Enabled = true;
        }

        // occurs when a plug-in requests for the currently active document..
        private void RequestActiveDocument(object sender, RequestScintillaDocumentEventArgs e)
        {
            // verify that there is an active document, etc..
            if (sttcMain.CurrentDocument?.Tag != null)
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
            ExceptionLogger.LogError(e.Exception, $"PLUG-IN EXCEPTION: '{e.PluginModuleName}'.");
            int idx = Plugins.FindIndex(f => f.Plugin.PLUGIN_NAME == e.PluginModuleName);
            if (idx != -1)
            {
                Plugins[idx].Plugin.EXCEPTION_COUNT++;
                DatabasePlugins.UpdatePlugin(Plugins[idx].Plugin);
            }
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
                foreach (var plugin in plugins)
                {
                    // find an index to the plug-in possibly modified by the dialog..
                    int idx = Plugins.FindIndex(f => f.Plugin.ID == plugin.ID);

                    // if a valid index was found..
                    if (idx != -1)
                    {
                        // set the new value for the PLUGINS class instance..
                        Plugins[idx] = (Plugins[idx].Assembly, Plugins[idx].PluginInstance, plugin);
                    }
                }
            }
        }

        /// <summary>
        /// The previous window state of this form.
        /// </summary>
        private FormWindowState previousWindowState = FormWindowState.Normal;

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (FormFirstShown)
            {
                if (WindowState == FormWindowState.Minimized && previousWindowState != FormWindowState.Minimized)
                {
                    FormSearchAndReplace.Instance.ToggleVisible(this, false);
                    previousWindowState = WindowState;
                }
                else if (previousWindowState != WindowState &&
                         (WindowState == FormWindowState.Maximized || WindowState == FormWindowState.Normal))
                {
                    FormSearchAndReplace.Instance.ToggleVisible(this, true);
                    previousWindowState = WindowState;
                }
            }
            else
            {
                previousWindowState = WindowState;
            }
        }

        private void FormMain_Deactivate(object sender, EventArgs e)
        {
            FormSearchAndReplace.Instance.ToggleStayTop(false);            
        }

        private void SttcMain_DocumentMouseDoubleClick(object sender, MouseEventArgs e)
        {
            var scintilla = (Scintilla)sender;
            // Indicators 0-7 could be in use by a lexer
            // so we'll use indicator 8 to highlight words.
            Highlight.HighlightWords(scintilla, 8, scintilla.SelectedText, FormSettings.Settings.SmartHighlight);
        }

        // a user wishes to mark all occurrences of the selected text with a style (1..5)..
        private void StyleSelectOf_Click(object sender, EventArgs e)
        {
            if (sttcMain.CurrentDocument != null)
            {
                int styleNum = int.Parse(((ToolStripMenuItem) sender).Tag.ToString());

                Highlight.HighlightWords(sttcMain.CurrentDocument.Scintilla, styleNum,
                    sttcMain.CurrentDocument.Scintilla.SelectedText, FormSettings.Settings.GetMarkColor(styleNum - 9));
            }
        }

        // a user wishes to clear style mark of style (1..5) from the editor..
        private void ClearStyleOf_Click(object sender, EventArgs e)
        {
            if (sttcMain.CurrentDocument != null)
            {
                Highlight.ClearStyle(sttcMain.CurrentDocument.Scintilla,
                    int.Parse(((ToolStripMenuItem) sender).Tag.ToString()));
            }
        }

        // a user wishes to clear all style markings of the current document..
        private void ClearAllStyles_Click(object sender, EventArgs e)
        {
            if (sttcMain.CurrentDocument != null)
            {
                for (int i = 9; i < 14; i++)
                {
                    Highlight.ClearStyle(sttcMain.CurrentDocument.Scintilla, i);
                }
            }
        }

        // a user wishes to reload the file contents from the disk..
        private void MnuReloadFromDisk_Click(object sender, EventArgs e)
        {
            if (sttcMain.CurrentDocument != null)
            {
                // get the DBFILE_SAVE class instance from the document's tag..
                DBFILE_SAVE fileSave = (DBFILE_SAVE) sttcMain.CurrentDocument.Tag;

                // avoid excess checks further in the code..
                if (fileSave == null)
                {
                    return;
                }

                // check if the file exists because it cannot be reloaded otherwise 
                // from the file system..
                if (File.Exists(sttcMain.CurrentDocument.FileName))
                {
                    // the encoding shouldn't change based on the file's contents if a snapshot of the file already exists in the database..
                    fileSave.ENCODING =
                        GetFileEncoding(CurrentSession, sttcMain.CurrentDocument.FileName, fileSave.ENCODING, true,
                            false, false, out _, out _, out _);

                    // the user answered yes..
                    sttcMain.SuspendTextChangedEvents =
                        true; // suspend the changed events on the ScintillaTabbedTextControl..

                    fileSave.ReloadFromDisk(sttcMain.CurrentDocument); // reload the file..
                    sttcMain.SuspendTextChangedEvents =
                        false; // resume the changed events on the ScintillaTabbedTextControl..

                    // just in case set the tag back..
                    sttcMain.CurrentDocument.Tag = fileSave;

                    fileSave.DB_MODIFIED = fileSave.FILESYS_MODIFIED;

                    sttcMain.CurrentDocument.FileTabButton.IsSaved = IsFileChanged(fileSave);

                    UpdateToolbarButtonsAndMenuItems();
                }
            }
        }

        // user wants to see the difference between two open files (tabs)
        // comparing the current document to the right side document..
        private void MnuDiffRight_Click(object sender, EventArgs e)
        {
            CurrentDocumentAction(document =>
            {
                // get the right side document..
                var documentTwo = GetRightOrLeftFromCurrent(true);

                // ..and if not null, do continue..
                if (documentTwo != null)
                {
                    FormFileDiffView.Execute(document.Scintilla.Text, documentTwo.Scintilla.Text);
                }
            });
        }

        // user wants to see the difference between two open files (tabs)
        // comparing the current document to the left side document..
        private void MnuDiffLeft_Click(object sender, EventArgs e)
        {
            CurrentDocumentAction(document =>
            {
                // get the left side document..
                var documentTwo = GetRightOrLeftFromCurrent(false);

                // ..and if not null, do continue..
                if (documentTwo != null)
                {
                    FormFileDiffView.Execute(document.Scintilla.Text, documentTwo.Scintilla.Text);
                }
            });
        }

        // the edit menu is opening..
        private void MnuEdit_DropDownOpening(object sender, EventArgs e)
        {
            // get the DBFILE_SAVE from the active document..
            var fileSave = (DBFILE_SAVE) sttcMain.CurrentDocument?.Tag;

            if (fileSave != null) // the second null check..
            {
                // enable / disable items which requires the file to exist in the file system..
                mnuRenameNewFileMainMenu.Enabled = !fileSave.EXISTS_INFILESYS;
            }
        }
        #endregion

        #region PrivateFields        
        /// <summary>
        /// An IPC client / server to transmit Windows shell file open requests to the current process.
        /// (C): VPKSoft: https://gist.github.com/VPKSoft/5d78f1c06ec51ebad34817b491fe6ac6
        /// </summary>
        private readonly IpcClientServer ipcServer = new IpcClientServer();

        /// <summary>
        /// A flag indicating if the main form should be activated.
        /// </summary>
        private bool bringToFrontQueued;

        /// <summary>
        /// A flag indicating if the main form's execution has left the Activated event.
        /// </summary>
        private bool leftActivatedEvent;

        /// <summary>
        /// A flag indicating whether the selection should be update to the status strip.
        /// Continuous updates with keyboard will cause excess CPU usage.
        /// </summary>
        private bool suspendSelectionUpdate;
        #endregion

        #region PrivateProperties                
        /// <summary>
        /// Gets the search string in case of a active document has a selection within a one line.
        /// </summary>
        private string SearchString
        {
            get
            {
                try
                {
                    if (sttcMain.CurrentDocument != null)
                    {
                        var scintilla = sttcMain.CurrentDocument.Scintilla;
                        if (scintilla.SelectedText.Length > 0)
                        {
                            var selectionStartLine = scintilla.LineFromPosition(scintilla.SelectionStart);
                            var selectionEndLine = scintilla.LineFromPosition(scintilla.SelectionEnd);
                            if (selectionStartLine == selectionEndLine)
                            {
                                return scintilla.SelectedText;
                            }
                        }
                    }

                    return string.Empty;
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionLogger.LogError(ex);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether constructor of this form has finished.
        /// </summary>
        private bool ConstructorFinished { get; set; }

        /// <summary>
        /// Gets or sets menu builder used to build the menu of the <see cref="Application"/>'s open forms.
        /// </summary>
        private WinFormsFormMenuBuilder WinFormsFormMenuBuilder { get; set; }

        /// <summary>
        /// Gets or sets the tab menu builder for the <see cref="ScintillaTabbedTextControl"/>.
        /// </summary>
        private TabMenuBuilder TabMenuBuilder { get; set; }

        /// <summary>
        /// Gets or sets the programming language helper class <see cref="ProgrammingLanguageHelper"/>.
        /// </summary>
        private ProgrammingLanguageHelper ProgrammingLanguageHelper { get; }

        /// <summary>
        /// Gets or sets the current session for the documents.
        /// </summary>
        private string CurrentSession
        {
            get => FormSettings.Settings == null ? "Default" : FormSettings.Settings.CurrentSession;

            set
            {
                bool sessionChanged = FormSettings.Settings.CurrentSession != value;
                string previousSession = FormSettings.Settings.CurrentSession;

                FormSettings.Settings.CurrentSession = value;

                if (sessionChanged)
                {
                    CloseSession(previousSession);

                    // load the recent documents which were saved during the program close..
                    LoadDocumentsFromDatabase(CurrentSession);
                }

                // get the session ID number from the database..
                CurrentSessionID = Database.Database.GetSessionID(CurrentSession);
            }
        }

        /// <summary>
        /// Gets or sets the loaded active plug-ins.
        /// </summary>
        private List<(Assembly Assembly, IScriptNotepadPlugin PluginInstance, PLUGINS Plugin)> Plugins { get; } =
            new List<(Assembly Assembly, IScriptNotepadPlugin PluginInstance, PLUGINS Plugin)>();

        /// <summary>
        /// Gets or sets a value indicating whether the default session name has been localized.
        /// </summary>
        private bool CurrentSessionLocalized
        {
            get => FormSettings.Settings.DefaultSessionLocalized;
            set => FormSettings.Settings.DefaultSessionLocalized = value;
        }

        /// <summary>
        /// Gets or sets the default encoding to be used with the files within this software.
        /// </summary>
        private List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)>
            DefaultEncodings => FormSettings.Settings.GetEncodingList();

        /// <summary>
        /// The amount of files to be saved to a document history.
        /// </summary>
        private int HistoryListAmount => FormSettings.Settings.HistoryListAmount;

        /// <summary>
        /// Gets the save file history contents count.
        /// </summary>
        private int SaveFileHistoryContentsCount =>
            FormSettings.Settings.SaveFileHistoryContents ?
                // the setting value if the setting is enabled..
                FormSettings.Settings.SaveFileHistoryContentsCount : 
                int.MinValue;

        /// <summary>
        /// Gets or sets the ID number for the current session for the documents.
        /// </summary>
        // ReSharper disable once InconsistentNaming
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
            // get the DBFILE_SAVE from the active document..
            var fileSave = (DBFILE_SAVE) sttcMain.CurrentDocument?.Tag;

            if (fileSave != null) // the second null check..
            {
                // enable / disable items which requires the file to exist in the file system..
                mnuOpenContainingFolderInExplorer.Enabled = File.Exists(fileSave.FILENAME_FULL);
                mnuOpenWithAssociatedApplication.Enabled = File.Exists(fileSave.FILENAME_FULL);
                mnuOpenContainingFolderInCmd.Enabled = File.Exists(fileSave.FILENAME_FULL);
                mnuOpenContainingFolderInWindowsPowerShell.Enabled = File.Exists(fileSave.FILENAME_FULL);
                mnuOpenWithAssociatedApplication.Enabled = File.Exists(fileSave.FILENAME_FULL);
                mnuRenameNewFile.Enabled = !fileSave.EXISTS_INFILESYS;
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
                    HandleCloseTab((DBFILE_SAVE) sttcMain.Documents[i].Tag, false, false, true,
                        sttcMain.Documents[i].Scintilla.CurrentPosition);
                }
            }
        }

        /// <summary>
        /// A method for getting the right-most or the left-most document compared to the active document.
        /// </summary>
        /// <param name="right">A flag indicating whether to get the right-most or the left-most document compared to active document.</param>
        /// <returns>The right-most or the left-most document compared to active document; if no document exists the method returns null.</returns>
        private ScintillaTabbedDocument GetRightOrLeftFromCurrent(bool right)
        {
            // get the document in question..
            var document = sttcMain.CurrentDocument;

            // get the index for the active document..
            int idx = document != null ? sttcMain.Documents.FindIndex(f => f.Equals(document)) : -1;

            // validate the index..
            if (idx != -1)
            {
                // just a simple plus/minus calculation..
                idx = right ? idx + 1 : idx - 1;

                if (idx >= 0 && idx < sttcMain.DocumentsCount)
                {
                    return sttcMain.Documents[idx];
                }
            }

            // no document was found, so do return null..
            return null;
        }

        // a user wishes to close all expect the active document or many documents 
        // to the right or to the left from the active document..
        private void CommonCloseManyDocuments(object sender, EventArgs e)
        {
            // call the CloseAllFunction method with this "wondrous" logic..
            CloseAllFunction(sender.Equals(mnuCloseAllToTheRight), sender.Equals(mnuCloseAllToTheLeft));
        }

        // a user wants to compare two unopened files..
        private void MnuDiffFiles_Click(object sender, EventArgs e)
        {
            try
            {
                string contentsOne;
                string contentsTwo;

                odAnyFile.InitialDirectory = FormSettings.Settings.FileLocationOpenDiff1;

                odAnyFile.Title = DBLangEngine.GetMessage("msgSelectFileDiff1",
                    "Select the first file to diff|A title for an open file dialog to indicate user selecting the first file to find differences with a second file");

                if (odAnyFile.ShowDialog() == DialogResult.OK)
                {
                    FormSettings.Settings.FileLocationOpenDiff1 = Path.GetDirectoryName(odAnyFile.FileName);
                    contentsOne = File.ReadAllText(odAnyFile.FileName);
                }
                else
                {
                    return;
                }

                odAnyFile.InitialDirectory = FormSettings.Settings.FileLocationOpenDiff2;

                odAnyFile.Title = DBLangEngine.GetMessage("msgSelectFileDiff2",
                    "Select the second file to diff|A title for an open file dialog to indicate user selecting the second file to find differences with a first file");

                if (odAnyFile.ShowDialog() == DialogResult.OK)
                {
                    FormSettings.Settings.FileLocationOpenDiff2 = Path.GetDirectoryName(odAnyFile.FileName);
                    contentsTwo = File.ReadAllText(odAnyFile.FileName);
                }
                else
                {
                    return;
                }

                FormFileDiffView.Execute(contentsOne, contentsTwo);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError(ex);
            }
        }

        // a user wishes to rename a new file..
        private void MnuRenameNewFile_Click(object sender, EventArgs e)
        {
            CurrentDocumentAction(document =>
            {
                var fileSave = (DBFILE_SAVE) document.Tag;
                if (fileSave.EXISTS_INFILESYS)
                {
                    return;
                }
                string newName;
                if ((newName = FormDialogRenameNewFile.ShowDialog(this, sttcMain)) != null)
                {
                    if (fileSave == null)
                    {
                        return;
                    }

                    // the file now has a location so update it..
                    fileSave.FILENAME = newName;
                    fileSave.FILENAME_FULL = newName;

                    // update the document..
                    document.FileName = newName;
                    document.FileNameNotPath = newName;
                    document.FileTabButton.Text = newName;

                    // update the time stamp..
                    fileSave.DB_MODIFIED = DateTime.Now;

                    // update document misc data, i.e. the assigned lexer to the database..
                    DatabaseFileSave.UpdateMiscFlags(fileSave);
                }
            });
        }
        #endregion

        #region EditorSymbols
        // enable/disable word wrap..
        private void MnuWordWrap_Click(object sender, EventArgs e)
        {
            CurrentScintillaAction(scintilla =>
            {
                scintilla.WrapMode = ItemFromObj<ToolStripMenuItem>(sender).Checked ? WrapMode.Word : WrapMode.None;
            });
        }

        // a user wishes to show or hide the white space symbols..
        private void MnuShowWhiteSpaceAndTab_Click(object sender, EventArgs e)
        {
            CurrentScintillaAction(scintilla =>
            {
                scintilla.ViewWhitespace = ItemFromObj<ToolStripMenuItem>(sender).Checked
                    ? WhitespaceMode.VisibleAlways
                    : WhitespaceMode.Invisible;
            });
        }

        // a user wishes to hide or show the end of line symbols..
        private void MnuShowEndOfLine_Click(object sender, EventArgs e)
        {
            CurrentScintillaAction(scintilla => { scintilla.ViewEol = ItemFromObj<ToolStripMenuItem>(sender).Checked; });
        }

        // the show symbol menu drop down items are going to be shown, so set their states accordingly..
        private void MnuShowSymbol_DropDownOpening(object sender, EventArgs e)
        {
            CurrentScintillaAction(scintilla => 
            {
                mnuShowEndOfLine.Checked = scintilla.ViewEol;

                mnuShowWhiteSpaceAndTab.Checked =
                    scintilla.ViewWhitespace != WhitespaceMode.Invisible;

                mnuShowIndentGuide.Checked = scintilla.IndentationGuides == IndentView.Real;

                mnuShowWrapSymbol.Checked = scintilla.WrapVisualFlags == WrapVisualFlags.End;
            });
        }

        // a user wishes to toggle the scintilla to show or hide the indentation guides..
        private void MnuShowIndentGuide_Click(object sender, EventArgs e)
        {
            CurrentScintillaAction(scintilla => 
                scintilla.IndentationGuides = ItemFromObj<ToolStripMenuItem>(sender).Checked
                    ? IndentView.Real
                    : IndentView.None);
        }

        // toggle whether to show the word wrap symbol..
        private void MnuShowWrapSymbol_Click(object sender, EventArgs e)
        {
            CurrentScintillaAction(scintilla =>
                scintilla.WrapVisualFlags = ItemFromObj<ToolStripMenuItem>(sender).Checked
                    ? WrapVisualFlags.End
                    : WrapVisualFlags.None);
        }
        #endregion
    }
}
