﻿#region License
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

#region Usings
using Microsoft.Win32;
using ScintillaNET; // (C)::https://github.com/jacobslusser/ScintillaNET
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Database.Entity.Enumerations;
using ScriptNotepad.DialogForms;
using ScriptNotepad.IOPermission;
using ScriptNotepad.Localization;
using ScriptNotepad.Localization.ExternalLibraryLoader;
using ScriptNotepad.Localization.Forms;
using ScriptNotepad.PluginHandling;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.Clipboard;
using ScriptNotepad.UtilityClasses.CodeDom;
using ScriptNotepad.UtilityClasses.Encodings;
using ScriptNotepad.UtilityClasses.Encodings.CharacterSets;
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
using ScriptNotepad.UtilityClasses.TextManipulation;
using ScriptNotepad.UtilityClasses.TextManipulation.TextSorting;
using ScriptNotepad.UtilityClasses.TextManipulationUtils;
using ScriptNotepadPluginBase.EventArgClasses;
using ScriptNotepadPluginBase.PluginTemplateInterface;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using RpcSelf;
using ScriptNotepad.Database.DirectAccess;
using ScriptNotepad.Database.Entity.Migrations;
using ScriptNotepad.Editor.EntityHelpers;
using ScriptNotepadOldDatabaseEntity;
using VPKSoft.ErrorLogger;
using VPKSoft.LangLib;
using VPKSoft.MessageBoxExtended;
using VPKSoft.MessageBoxExtended.Controls;
using VPKSoft.MessageHelper;
using VPKSoft.PosLib;
using VPKSoft.ScintillaLexers;
using VPKSoft.ScintillaLexers.HelperClasses;
using VPKSoft.ScintillaTabbedTextControl;
using VPKSoft.ScintillaUrlDetect;
using VPKSoft.VersionCheck;
using VPKSoft.VersionCheck.Forms;
using static ScriptNotepad.UtilityClasses.ApplicationHelpers.ApplicationActivateDeactivate;
using static ScriptNotepad.UtilityClasses.Encodings.FileEncoding;
using static VPKSoft.ScintillaLexers.GlobalScintillaFont;
using ErrorHandlingBase = ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase;
using ScriptNotepad.Editor.Utility;
using ScriptNotepad.Editor.Utility.ModelHelpers;
using ScriptNotepad.UtilityClasses.EventArguments;
using ScriptNotepad.UtilityClasses.ScintillaHelpers;
using ScriptNotepad.UtilityClasses.TextManipulation.BaseClasses;
using VPKSoft.DBLocalization;
using FileSaveHelper = ScriptNotepad.Editor.Utility.ModelHelpers.FileSaveHelper;
using FileSessionHelper = ScriptNotepad.Editor.EntityHelpers.FileSessionHelper;

#endregion

namespace ScriptNotepad;

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

        MessageBoxBase.DefaultOwner = this;

        // add positioning..
        PositionCore.Bind(ApplicationType.WinForms);

        InitializeComponent();

        DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

        if (Utils.ShouldLocalize() != null)
        {
            DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
            return; // After localization don't do anything more..
        }

        Instance = this;

        // register the code page encodings..
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        // this is required..
        FileSessionHelper.ApplicationDataDirectory = Path.Combine(DBLangEngine.DataDir, "Cache");
        if (!Directory.Exists(FileSessionHelper.ApplicationDataDirectory))
        {
            Directory.CreateDirectory(FileSessionHelper.ApplicationDataDirectory);
        }

        // initialize the language/localization database..
        DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages");

        // subscribe to the session ended event to save the documents without asking stupid questions..
        SystemEvents.SessionEnded += SystemEvents_SessionEnded;

        // create an IPC server at localhost, the port was randomized in the development phase..
        try 
        {
            IpcServer = new RpcSelfHost<string>(50670);
        }
        catch (Exception ex)
        {
            ExceptionLogger.LogError(ex);
        }

        // subscribe to the IPC event if the application receives a message from another instance of this application..
        if (IpcServer != null)
        { 
            IpcServer.MessageReceived += RemoteMessage_MessageReceived;
        }

        var databaseFile = DBLangEngine.DataDir + "ScriptNotepadEntityCore.sqlite";

        var connectionString = $"Data Source={databaseFile}";

        base.Text +=
            (ProcessElevation.IsElevated ? " (" +
                                           DBLangEngine.GetMessage("msgProcessIsElevated", "Administrator|A message indicating that a process is elevated.") + ")" : string.Empty);

        ExecuteDatabaseMigrate.DefaultSessionName = DBLangEngine.GetStatMessage("msgDefaultSessionName",
            "Default|A name of the default session for the documents");

        try
        {
            // run the database migrations (FluentMigrator)..
            ExecuteDatabaseMigrate.Migrate(connectionString);
        }
        catch (Exception ex)
        {
            // ..the database already exists, so do create and update the VersionInfo table..
            CheckFluentMigrator.MarkMigration(connectionString);
            ExceptionLogger.LogError(ex);
        }

        // initialize the ScriptNotepadDbContext class instance..
        ScriptNotepadDbContext.InitializeDbContext(connectionString);

        MigrateDatabaseEfCore(); // migrate to Entity Framework Core database..

        // load the external spell check library if defined..
        ExternalSpellChecker.Load();

        // localize the open file dialog..
        StaticLocalizeFileDialog.InitFileDialog(odAnyFile);

        // localize the save file dialog..
        StaticLocalizeFileDialog.InitFileDialog(sdAnyFile);

        // localize the save HTML dialog..
        StaticLocalizeFileDialog.InitHTMLFileDialog(sdHTML);

        // set the value whether to use auto-ellipsis on long URLs with the ScintillaUrlDetect..
        ScintillaUrlDetect.AutoEllipsisUrlLength = FormSettings.Settings.UrlUseAutoEllipsis
            ? FormSettings.Settings.UrlMaxLengthBeforeEllipsis
            : -1;

        // set the URL styling to use threads..
        ScintillaUrlDetect.UseThreadsOnUrlStyling = true;

        // localize the open and save file dialog titles..
        sdAnyFile.Title = DBLangEngine.GetMessage("msgSaveFileAs", "Save As|A title for a save file as dialog");
        odAnyFile.Title = DBLangEngine.GetMessage("msgOpenFile", "Open|A title for a open file dialog");

        // localize the dwell tool tips on used by the ScintillaUrlDetect class library..
        ScintillaUrlDetect.DwellToolTipTextUrl = DBLangEngine.GetMessage("msgUrlDetectToolTipOpenHyperlink",
            "Use CTRL + Click to follow the link: {0}|A message for the URL detect library tool tip to open a hyperlink.");

        ScintillaUrlDetect.DwellToolTipTextMailTo = DBLangEngine.GetMessage("msgUrlDetectToolTipOpenMailToLink",
            "Use CTRL + Click to sent email to: {0}|A message for the URL detect library tool tip to open a email program for a mailto link.");

        // initialize the helper class for the status strip's labels..
        StatusStripTexts.InitLabels(ssLbLineColumn, ssLbLinesColumnSelection, ssLbLDocLinesSize, 
            ssLbLineEnding, ssLbEncoding, ssLbSessionName, ssLbInsertOverride, sslbZoom, sslbTabs);

        // get the current file session..
        currentSession = FormSettings.Settings.CurrentSessionEntity;

        // set the status strip label's to indicate that there is no active document..
        StatusStripTexts.SetEmptyTexts(CurrentSession.SessionName);

        // localize some other class properties, etc..
        FormLocalizationHelper.LocalizeMisc();

        // get the font size and family from the settings..
        FontFamilyName = FormSettings.Settings.EditorFontName;
        FontSize = FormSettings.Settings.EditorFontSize; 

        // localize the thread if set in the settings..
        if (FormSettings.Settings.LocalizeThread)
        {
            Thread.CurrentThread.CurrentCulture = DBLangEngine.UseCulture;
            Thread.CurrentThread.CurrentUICulture = DBLangEngine.UseCulture;
        }

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
        LoadDocumentsFromDatabase(CurrentSession.SessionName);

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

        // set the current session name to the status strip..
        StatusStripTexts.SetSessionName(CurrentSession.SessionName);

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
        // create the default directory for custom dictionaries if it doesn't exist yet..
        FormSettings.CreateDefaultCustomDictionaryDirectory();

        // localize the about "box"..
        VersionCheck.AboutDialogDisplayDownloadDialog = true; // I want to make it fancy..
        VersionCheck.OverrideCultureString = FormSettings.Settings.Culture.Name; // I want it localized..
        VersionCheck.CacheUpdateHistory = true; // if the user wishes to refer to some change in the history of the software..

        VersionCheck.OverrideCultureString = FormSettings.Settings.Culture.Name;

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

        // subscribe to an event which is raised upon application activation..
        ApplicationDeactivated += FormMain_ApplicationDeactivated;
        ApplicationActivated += FormMain_ApplicationActivated;

        // get the case-sensitivity value from the settings..
        mnuCaseSensitive.Checked = FormSettings.Settings.TextUpperCaseComparison;

        // TODO::Take this into use..
        var boxStackContainer = new ToolStripMessageBoxExpandStack
        {
            Text = DBLangEngine.GetMessage("msgOpenDialogs",
                "Open dialogs|A message describing a dropdown control containing message dialog boxes."),
        };
        BoxStack = boxStackContainer.MessageBoxExpandStack;
        tsMain.Items.Add(boxStackContainer);
        BoxStack.Visible = false;

        // populate the text menu call backs to the run snippet dialog..
        PopulateTextMenuCallBacks();

        if (FormSettings.Settings.ShowRunSnippetToolbar)
        {
            ToggleSnippetRunner();
        }

        // the constructor code finalized executing..
        runningConstructor = false;
    }
    #endregion

    #region DatabaseMigration
    private void MigrateDatabaseEfCore()
    {
        try
        {
            // check the migration level..
            if (FormSettings.Settings.DatabaseMigrationLevel < 2 && File.Exists(Path.Combine(DBLangEngine.DataDir, "ScriptNotepadEntity.sqlite")))
            {
                MessageBoxExtended.Show(
                    DBLangEngine.GetMessage("msgDatabaseMigration1",
                        "The database will be updated. This might take a few minutes.|A message informing that database is migrating to a Entity Framework Code-First database and it might be a lengthy process."),
                    DBLangEngine.GetMessage("msgInformation",
                        "Information|A message title describing of some kind information."),
                    MessageBoxButtonsExtended.OK, MessageBoxIcon.Information, ExtendedDefaultButtons.Button1);

                List<Exception> migrateExceptions;

                if ((migrateExceptions = ScriptNotepadOldDbContext.MigrateToEfCore(
                        Path.Combine(DBLangEngine.DataDir, "ScriptNotepadEntity.sqlite"), 
                        Path.Combine(DBLangEngine.DataDir, "ScriptNotepadEntityCore.sqlite"))).Count > 0) 
                {
                    foreach (var migrateException in migrateExceptions)
                    {
                        MessageBoxExtended.Show(
                            migrateException.Message,
                            DBLangEngine.GetMessage("msgError",
                                "Error|A message describing that some kind of error occurred."), MessageBoxButtonsExtended.OK,
                            MessageBoxIcon.Error, ExtendedDefaultButtons.Button1);
                        ExceptionLogger.LogError(migrateException);
                    }
                    Process.GetCurrentProcess().Kill();
                    return;
                }

                FormSettings.Settings.DatabaseMigrationLevel = 2;

                File.Delete(Path.Combine(DBLangEngine.DataDir, "ScriptNotepadEntity.sqlite"));
            }
        }
        catch (Exception ex)
        {
            ExceptionLogger.LogError(ex);
        }
    }
    #endregion

    #region HelperMethods      
    /// <summary>
    /// Toggles the snippet runner tool bar.
    /// </summary>
    private void ToggleSnippetRunner()
    {
        var dockForm = FormSnippetRunner.Instance;

        if (pnDockRunSnippet.Controls.Contains(dockForm))
        {
            if (dockForm.SearchFocused)
            {
                dockForm.Close();
                FormSettings.Settings.ShowRunSnippetToolbar = false;
                return;
            }

            dockForm.SearchFocused = true;
            return;
        }

        dockForm.Visible = true;
        dockForm.TopLevel = false;
        dockForm.AutoScroll = true;
        dockForm.FormBorderStyle = FormBorderStyle.None;
        dockForm.Location = new Point(0, 0);
        dockForm.Width = pnDockRunSnippet.Width;
        dockForm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        dockForm.Closing += delegate { pnDockRunSnippet.Controls.Remove(dockForm); };

        pnDockRunSnippet.Controls.Add(dockForm);
        dockForm.Focus();
    }

    /// <summary>
    /// Populates the text menu call backs to the run snippet dialog.
    /// </summary>
    private void PopulateTextMenuCallBacks()
    {
        FormSnippetRunner.Callbacks.AddRange(new[]
        {
            new TextManipulationCallbackBase
            {
                CallbackAction = () => mnuSortAscending.PerformClick(),
                MethodName = DBLangEngine.GetMessage("msgUtilTextSortLinesAscending",
                    "Sort lines ascending|A message describing an action to sort lines in ascending order."),
            },
            new TextManipulationCallbackBase
            {
                CallbackAction = () => mnuSortDescending.PerformClick(),
                MethodName = DBLangEngine.GetMessage("msgUtilTextSortLinesDescending",
                    "Sort lines descending|A message describing an action to sort lines in descending order."),
            },
            new TextManipulationCallbackBase
            {
                CallbackAction = () => mnuCustomizedSort.PerformClick(),
                MethodName = DBLangEngine.GetMessage("msgUtilTextSortLinesCustom",
                    "Sort lines in custom order|A message describing an action to sort lines in custom order."),
            },
            new TextManipulationCallbackBase
            {
                CallbackAction = () => mnuWrapDocumentTo.PerformClick(),
                MethodName = DBLangEngine.GetMessage("msgUtilTextWrapDocument",
                    "Wrap document|A message describing an action to wrap document into specified maximum length lines."),
            },
        });
    }
        
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
                tsbSpellCheck.Checked = enabled;
                spellCheck.Enabled = enabled;

                var fileSave = (FileSave) document.Tag;
                fileSave.UseSpellChecking = spellCheck.Enabled;
                if (!noDatabaseUpdate)
                {
                    fileSave.AddOrUpdateFile();
                }

                document.Tag = fileSave;
            }
        });
    }

    /// <summary>
    /// Undo the document changes if possible.
    /// </summary>
    internal void Undo()
    {
        // if there is an active document..
        CurrentDocumentAction(document =>
        {
            sttcMain.SuspendTextChangedEvents = true; // suspend the changed events on the ScintillaTabbedTextControl..

            // ..then undo if it's possible..
            if (document.Scintilla.CanUndo)
            {
                // undo..
                document.Scintilla.Undo();

                // get a FileSave class instance from the document's tag..
                var fileSave = (FileSave)document.Tag;

                // undo the encoding change..
                fileSave.UndoEncodingChange();

                if (!document.Scintilla.CanUndo)
                {
                    fileSave.PopPreviousDbModified();
                    document.FileTabButton.IsSaved = !IsFileChanged(fileSave);
                }
            }
            sttcMain.SuspendTextChangedEvents = false; // suspend the changed events on the ScintillaTabbedTextControl..
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
    private void DisposeSpellCheckerAndUrlHighlight()
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

            // validate that the ScintillaTabbedDocument instance has an URL highlighter attached to it..
            if (sttcMain.Documents[i] != null && sttcMain.Documents[i].Tag1 != null &&
                sttcMain.Documents[i].Tag1.GetType() == typeof(ScintillaUrlDetect))
            {
                var urlDetect = (ScintillaUrlDetect) sttcMain.Documents[i].Tag1;

                using (urlDetect)
                {
                    sttcMain.Documents[i].Tag1 = null;
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
    /// Runs an action to the current document's <see cref="FileSave"/> class instance stored in the Tag property.
    /// </summary>
    /// <param name="action">The action to run.</param>
    public void CurrentFileSaveAction(Action<FileSave> action)
    {
        if (sttcMain.CurrentDocument?.Tag != null)
        {
            if (sttcMain.CurrentDocument.Tag is FileSave fileSave)
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
    /// Runs an action to the last added document's <see cref="FileSave"/> class instance stored in the Tag property.
    /// </summary>
    /// <param name="action">The action to run.</param>
    public void LastAddedFileSaveAction(Action<FileSave> action)
    {
        if (sttcMain.LastAddedDocument?.Tag != null)
        {
            if (sttcMain.LastAddedDocument.Tag is FileSave fileSave)
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
            // get a FileSave class instance from the document's tag..
            var fileSave = (FileSave)sttcMain.CurrentDocument.Tag;

            // undo the encoding change..
            fileSave.UndoEncodingChange();

            if (!sttcMain.CurrentDocument.Scintilla.CanUndo)
            {
                fileSave.PopPreviousDbModified();
                sttcMain.CurrentDocument.FileTabButton.IsSaved = !IsFileChanged(fileSave);
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
                var plugin = Plugins.FirstOrDefault(f => f.Plugin.FileNameFull == module);
                if (plugin.Plugin != null)
                {
                    plugin.Plugin.ApplicationCrashes++;
                    plugin.Plugin.IsActive = false;
                    ScriptNotepadDbContext.DbContext.SaveChanges();
                }
            }
            catch
            {
                // the application is about to crash - let the ExceptionLogger do it's job and log the crash..
            }
        };

        IEnumerable<Plugin> databaseEntries = ScriptNotepadDbContext.DbContext.Plugins.ToList();
        bool pluginDeleted = false;
        // ReSharper disable once PossibleMultipleEnumeration
        foreach (var pluginEntry in databaseEntries)
        {
            if (pluginEntry.PendingDeletion)
            {
                try
                {
                    File.Delete(pluginEntry.FileNameFull);
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionLogger.LogError(ex);
                }

                ScriptNotepadDbContext.DbContext.Plugins.Remove(pluginEntry);
                ScriptNotepadDbContext.DbContext.SaveChanges();
                pluginDeleted = true;
            }
        }

        if (pluginDeleted)
        {
            databaseEntries = ScriptNotepadDbContext.DbContext.Plugins.ToList();
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
                        f => f.FileName == Path.GetFileName(plugin.Path));

            // if the plug-in has been logged into the database and is disabled
            // save the flag..
            bool loadPlugin = true;
            if (pluginEntry != null)
            {
                loadPlugin = pluginEntry.IsActive;
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
                        CurrentSession.SessionName, this))
                {
                    pluginEntry = pluginEntry == null
                        ? PluginHelper.FromPlugin(plugin.Assembly, pluginAssembly.Plugin, plugin.Path)
                        : PluginHelper.UpdateFromPlugin(pluginEntry, plugin.Assembly, pluginAssembly.Plugin,
                            plugin.Path);

                    // on success, add the plug-in assembly and its instance to the internal list..
                    Plugins.Add((plugin.Assembly, pluginAssembly.Plugin, pluginEntry));
                }

                // update the possible version and the update time stamp..
                if (pluginEntry != null)
                {
                    AssemblyVersion.SetPluginUpdated(pluginEntry, plugin.Assembly);

                    // update the plug-in information to the database..
                    ScriptNotepadDbContext.DbContext.SaveChanges();
                }
            }
            else
            {
                if (pluginEntry != null)
                {
                    pluginEntry.LoadFailures++;
                }
                else
                {
                    pluginEntry = PluginHelper.InvalidPlugin(plugin.Assembly, plugin.Path);
                }
                // update the possible version and the update time stamp..
                AssemblyVersion.SetPluginUpdated(pluginEntry, plugin.Assembly);

                // update the plug-in information to the database..
                ScriptNotepadDbContext.DbContext.SaveChanges();

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

        // unsubscribe to an event which is raised upon application activation..
        ApplicationDeactivated -= FormMain_ApplicationDeactivated;
        ApplicationActivated -= FormMain_ApplicationActivated;

        // dispose of the programming language menu helper..
        using (ProgrammingLanguageHelper)
        {
            // unsubscribe the programming language click event..
            ProgrammingLanguageHelper.LanguageMenuClick += ProgrammingLanguageHelper_LanguageMenuClick;                
        }            

        // unsubscribe to the event when a search result is clicked from the FormSearchResultTree form..
        FormSearchResultTree.SearchResultSelected -= FormSearchResultTreeSearchResultSelected;

        // unsubscribe the IpcClientServer MessageReceived event handler..
        if (IpcServer != null)
        { 
            IpcServer.MessageReceived -= RemoteMessage_MessageReceived;
            IpcServer.Dispose();
        }

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
                Plugins[i].Plugin.ExceptionCount++;

                // the disposal failed so do add to the exception count..
                ScriptNotepadDbContext.DbContext.SaveChanges();

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

            // close all the dialog boxes with a dialog result of cancel..
            MessageBoxExtendedControl.CloseAllBoxesWithResult(DialogResultExtended.Cancel);

            // close all other open forms except this MainForm as they might dialogs, etc. to prevent the
            // session log of procedure..
            CloseFormUtils.CloseOpenForms(this);
        }

        // save the current session's documents to the database..
        SaveDocumentsToDatabase();

        // delete excess entries from the file history list from the database..
        var deleted = FileHistoryHelper.CleanupHistoryList(HistoryListAmount, currentSession);
        ExceptionLogger.LogMessage($"Database history list cleanup: success = {deleted.success}, amount = {deleted.count}, session = {CurrentSession}.");

        // delete excess document contents saved in the database..
        deleted = FileHistoryHelper.CleanUpHistoryFiles(SaveFileHistoryContentsCount, CurrentSession);
        ExceptionLogger.LogMessage($"Database history contents cleanup: success = {deleted.success}, amount = {deleted.count}, session = {CurrentSession}.");

        // clean the old search path entries from the database..
        MiscellaneousTextEntryHelper.DeleteOlderEntries(MiscellaneousTextType.Path,
            FormSettings.Settings.FileSearchHistoriesLimit, CurrentSession);

        // clean excess regular expressions from the custom text sorting dialog..
        MiscellaneousTextEntryHelper.DeleteOlderEntries(MiscellaneousTextType.RegexSorting,
            20, CurrentSession);

        // clean the old replace replace history entries from the database..
        SearchAndReplaceHistoryHelper.DeleteOlderEntries(SearchAndReplaceSearchType.All,
            SearchAndReplaceType.Replace, FormSettings.Settings.FileSearchHistoriesLimit, CurrentSession);

        // clean the old replace search history entries from the database..
        SearchAndReplaceHistoryHelper.DeleteOlderEntries(SearchAndReplaceSearchType.All,
            SearchAndReplaceType.Search, FormSettings.Settings.FileSearchHistoriesLimit, CurrentSession);

        // close the main form as the call came from elsewhere than the FormMain_FormClosed event..
        if (noUserInteraction)
        {
            Close();
        }

        // dispose of the spell checkers attached to the documents..
        DisposeSpellCheckerAndUrlHighlight();

        // unsubscribe the event handlers from the context menus..
        DisposeContextMenus();
    }

    /// <summary>
    /// Closes the current given session.
    /// </summary>
    private void CloseSession()
    {
        // save the current session's documents to the database..
        SaveDocumentsToDatabase();

        // delete excess entries from the file history list from the database..
        var deleted = FileHistoryHelper.CleanupHistoryList(HistoryListAmount, currentSession);
        ExceptionLogger.LogMessage($"Database history list cleanup: success = {deleted.success}, amount = {deleted.count}, session = {CurrentSession}.");

        // delete excess document contents saved in the database..
        deleted = FileHistoryHelper.CleanUpHistoryFiles(SaveFileHistoryContentsCount, CurrentSession);
        ExceptionLogger.LogMessage($"Database history contents cleanup: success = {deleted.success}, amount = {deleted.count}, session = {CurrentSession}.");

        // dispose of the spell checkers attached to the documents..
        DisposeSpellCheckerAndUrlHighlight();

        // unsubscribe the event handlers from the context menus..
        DisposeContextMenus();

        // close all the documents..
        sttcMain.CloseAllDocuments();
    }

    /// <summary>
    /// Determines whether this instance can display a query dialog. I.e. the main for is active and visible.
    /// </summary>
    /// <returns><c>true</c> if this instance can display a query dialog; otherwise, <c>false</c>.</returns>
    private bool CanDisplayQueryDialog()
    {
        var result = !runningConstructor && MessageBoxBase.MessageBoxInstances.Count == 0 && Visible &&
                     (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized);
        return result;
    }

    /// <summary>
    /// Checks if an open document has been changed in the file system or removed from the file system and 
    /// queries the user form appropriate action for the file.
    /// </summary>
    private void CheckFileSysChanges()
    {
        for (int i = sttcMain.DocumentsCount - 1; i >= 0; i--)
        {
            // get the FileSave class instance from the document's tag..
            var fileSave = (FileSave)sttcMain.Documents[i].Tag;

            // avoid excess checks further in the code..
            if (fileSave == null)
            {
                continue;
            }

            // check if the file exists because it cannot be reloaded otherwise 
            // from the file system..
            if (File.Exists(sttcMain.Documents[i].FileName) && !fileSave.ShouldQueryFileReappeared())
            {
                // query the user if one wishes to reload
                // the changed file from the disk..
                if (CanDisplayQueryDialog() && fileSave.GetShouldQueryDiskReload())
                { 
                    if (MessageBoxExtended.Show(
                            DBLangEngine.GetMessage("msgFileHasChanged", "The file '{0}' has been changed. Reload from the file system?|As in the opened file has been changed outside the software so do as if a reload should happen", fileSave.FileNameFull),
                            DBLangEngine.GetMessage("msgFileArbitraryFileChange", "A file has been changed|A caption message for a message dialog which will ask if a changed file should be reloaded"),
                            MessageBoxButtonsExtended.YesNo,
                            MessageBoxIcon.Question,
                            ExtendedDefaultButtons.Button1) == DialogResultExtended.Yes)
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
                        fileSave.SetShouldQueryDiskReload(false);

                        // set the flag that the file's modified date in the database
                        // has been changed as the user didn't wish to reload the file from the file system: FS != DB..
                        fileSave.SetDatabaseModified(DateTime.Now);

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
                if (CanDisplayQueryDialog() && fileSave.ShouldQueryKeepFile())
                {
                    if (MessageBoxExtended.Show(
                            DBLangEngine.GetMessage("msgFileHasBeenDeleted", "The file '{0}' has been deleted. Keep the file in the editor?|As in the opened file has been deleted from the file system and user is asked if to keep the deleted file in the editor", fileSave.FileNameFull),
                            DBLangEngine.GetMessage("msgFileHasBeenDeletedTitle", "A file has been deleted|A caption message for a message dialog which will ask if a deleted file should be kept in the editor"),
                            MessageBoxButtonsExtended.YesNo,
                            MessageBoxIcon.Question,
                            ExtendedDefaultButtons.Button1) == DialogResultExtended.Yes)
                    {
                        // the user answered yes..
                        fileSave.ExistsInFileSystem = false; // set the flag to false..
                        fileSave.IsHistory = false;

                        fileSave.AddOrUpdateFile(sttcMain.Documents[i], true, false, true);

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
                else if (CanDisplayQueryDialog() && fileSave.ShouldQueryFileReappeared())
                {
                    if (MessageBoxExtended.Show(
                            DBLangEngine.GetMessage("msgFileHasReappeared", "The file '{0}' has reappeared. Reload from the file system?|As in the file has reappeared to the file system and the software queries whether to reload it's contents from the file system", fileSave.FileNameFull),
                            DBLangEngine.GetMessage("msgFileHasReappearedTitle", "A file has reappeared|A caption message for a message dialog which will ask if a reappeared file should be reloaded from the file system"),
                            MessageBoxButtonsExtended.YesNo,
                            MessageBoxIcon.Question,
                            ExtendedDefaultButtons.Button1) == DialogResultExtended.Yes)
                    {
                        // the user answered yes..
                        fileSave.ExistsInFileSystem = true; // set the flag to true..
                        fileSave.IsHistory = false;

                        fileSave.AddOrUpdateFile(sttcMain.Documents[i], true, false, true);

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
    /// <param name="fileSave">An instance to <see cref="FileSave"/> class instance.</param>
    /// <param name="fileDeleted">A flag indicating if the file was deleted from the file system and a user decided to not the keep the file in the editor.</param>
    /// <param name="tabClosing">A flag indicating whether this call was made from the tab closing event of a <see cref="ScintillaTabbedDocument"/> class instance.</param>
    /// <param name="closeTab">A flag indicating whether the tab containing the given <paramref name="fileSave"/> should be closed.</param>
    /// <param name="currentPosition">The position of the caret within the "file".</param>
    /// <returns>A modified <see cref="FileSave"/> class instance based on the given parameters.</returns>
    private void HandleCloseTab(FileSave fileSave, bool fileDeleted, bool tabClosing, bool closeTab,
        int currentPosition)
    {
        // disable the timers while a document is closing..
        EnableDisableTimers(false);

        // set the flags according to the parameters..
        fileSave.IsHistory = tabClosing || fileDeleted || closeTab;

        // set the exists in file system flag..
        fileSave.ExistsInFileSystem = File.Exists(fileSave.FileNameFull);

        // delete the previous entries of the same file..
        if (fileSave.IsHistory && !fileSave.ExistsInFileSystem)
        {
            var existingFileSaves = ScriptNotepadDbContext.DbContext.FileSaves.Where(f =>
                f.IsHistory && f.FileName == fileSave.FileName && f.Id != fileSave.Id &&
                f.ExistsInFileSystem == fileSave.ExistsInFileSystem &&
                f.SessionId == fileSave.SessionId);

            ScriptNotepadDbContext.DbContext.FileSaves.RemoveRange(existingFileSaves);
        }

        fileSave.CurrentCaretPosition = currentPosition;

        // get the tabbed document index via the ID number..
        int docIndex = sttcMain.Documents.FindIndex(f => f.ID == fileSave.Id);

        ExceptionLogger.LogMessage($"Tab closing: {docIndex}");

        // this should never be -1 but do check still in case of a bug..
        if (docIndex != -1)
        {
            // unsubscribe the context menu event(s)..
            ScintillaContextMenu.UnsubscribeEvents(sttcMain.Documents[docIndex].Scintilla);

            // update the file save to the database..
            fileSave.AddOrUpdateFile(sttcMain.Documents[docIndex], true, false, false);
                
            // update the file history list in the database..
            RecentFileHelper.AddOrUpdateRecentFile(fileSave);

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

            // URL highlighter disposal..
            if (sttcMain.Documents[docIndex].Tag1 != null && sttcMain.Documents[docIndex].Tag1.GetType() == typeof(ScintillaUrlDetect))
            {
                // dispose of the URL highlighter..
                var urlCheck = (ScintillaUrlDetect)sttcMain.Documents[docIndex].Tag1;

                using (urlCheck)
                {
                    sttcMain.Documents[docIndex].Tag1 = null;
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
            // get the file FileSave instance from the tag..
            var fileSave = (FileSave)document.Tag;
            document.FileTabButton.IsSaved = !IsFileChanged(fileSave);
        }
        UpdateToolbarButtonsAndMenuItems();
    }

    /// <summary>
    /// Determines whether the <see cref="FileSave"/> has changed in the editor vs. the file system.
    /// </summary>
    /// <param name="fileSave">The <see cref="FileSave"/> class to check for.</param>
    private bool IsFileChanged(FileSave fileSave)
    {
        if (!fileSave.ExistsInFileSystem)
        {
            return true;
        }

        if (fileSave.FileSystemModified < fileSave.DatabaseModified)
        {
            return true;
        }

        return false;
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
    private void SaveDocumentsToDatabase()
    {
        for (int i = 0; i < sttcMain.DocumentsCount; i++)
        {
            try
            {
                var fileSave = (FileSave) sttcMain.Documents[i].Tag;

                fileSave.IsActive = sttcMain.Documents[i].FileTabButton.IsActive;
                fileSave.VisibilityOrder = i;
                fileSave.FoldSave = sttcMain.Documents[i].Scintilla.SaveFolding();
                    
                ScriptNotepadDbContext.DbContext.SaveChanges();
                RecentFileHelper.AddOrUpdateRecentFile(fileSave);
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
        new TabbedDocumentSpellCheck(document, !FormSettings.Settings.EditorSpellUseCustomDictionary);

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

        sslbTabsValue.Text = $@"{sttcMain.CurrentDocumentIndex + 1}/{sttcMain.DocumentsCount}";
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
        StatusStripTexts.SetEmptyTexts(CurrentSession.SessionName);

        // set the application title to indicate no active document..
        SetEmptyApplicationTitle();

        IEnumerable<FileSave> files =
            ScriptNotepadDbContext.DbContext.FileSaves.Where(f =>
                f.Session.SessionName == sessionName && !f.IsHistory);

        string activeDocument = string.Empty;

        foreach (var file in files)
        {
            if (file.IsActive)
            {
                activeDocument = file.FileNameFull;
            }

            sttcMain.AddDocument(file.FileNameFull, file.Id, file.GetEncoding(), file.GetFileContentsAsMemoryStream());

            if (sttcMain.LastAddedDocument != null)
            {
                // append additional initialization to the document..
                AdditionalInitializeDocument(sttcMain.LastAddedDocument);
                // set the saved position of the document's caret..
                if (file.CurrentCaretPosition > 0 && file.CurrentCaretPosition < sttcMain.LastAddedDocument.Scintilla.TextLength)
                {
                    sttcMain.LastAddedDocument.Scintilla.CurrentPosition = file.CurrentCaretPosition;
                    sttcMain.LastAddedDocument.Scintilla.SelectionStart = file.CurrentCaretPosition;
                    sttcMain.LastAddedDocument.Scintilla.SelectionEnd = file.CurrentCaretPosition;
                    sttcMain.LastAddedDocument.Scintilla.ScrollCaret();
                }

                sttcMain.LastAddedDocument.Scintilla.TabWidth = FormSettings.Settings.EditorTabWidth;

                sttcMain.LastAddedDocument.Tag = file;

                // append possible style and spell checking for the document..
                AppendStyleAndSpellChecking(sttcMain.LastAddedDocument);

                // set the lexer type from the saved database value..
                sttcMain.LastAddedDocument.LexerType = file.LexerType;

                SetSpellCheckerState(file.UseSpellChecking, true);

                // enabled the caret line background color..
                SetCaretLineColor();

                // set the brace matching if enabled..
                SetStyleBraceMatch.SetStyle(sttcMain.LastAddedDocument.Scintilla);

                // set the context menu strip for the file tab..
                sttcMain.LastAddedDocument.FileTabButton.ContextMenuStrip = cmsFileTab;

                // the file load can't add an undo option the Scintilla..
                sttcMain.LastAddedDocument.Scintilla.EmptyUndoBuffer();

                // set the zoom value..
                sttcMain.LastAddedDocument.ZoomPercentage = file.EditorZoomPercentage;

                sttcMain.LastAddedDocument.Scintilla.RestoreFolding(file.FoldSave);
            }            

            UpdateDocumentSaveIndicators();
        }
        //sttcMain.ResumeLayout();

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

        // set the URL detection if enabled..
        if (FormSettings.Settings.HighlightUrls)
        {
            var urlDetect = new ScintillaUrlDetect(document.Scintilla);
            FormSettings.SetUrlDetectStyling(urlDetect);

            urlDetect.AppendIndicatorClear(31);
            document.Tag1 = urlDetect;
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
            if (sttcMain.LastAddedDocument != null) // if the document was added or updated to the control..
            {
                // append additional initialization to the document..
                AdditionalInitializeDocument(sttcMain.LastAddedDocument);

                sttcMain.LastAddedDocument.Tag =
                    new FileSave
                    {
                        FileName = sttcMain.CurrentDocument.FileName,
                        FileNameFull = sttcMain.CurrentDocument.FileName,
                        FilePath = string.Empty,
                        Session = CurrentSession,
                        UseFileSystemOnContents = CurrentSession.UseFileSystemOnContents,
                    };

                sttcMain.LastAddedDocument.Scintilla.TabWidth = FormSettings.Settings.EditorTabWidth;

                // assign the context menu strip for the tabbed document..
                sttcMain.LastAddedDocument.FileTabButton.ContextMenuStrip = cmsFileTab;

                // get a FileSave class instance from the document's tag..
                var fileSave = (FileSave)sttcMain.LastAddedDocument.Tag;

                // save the FileSave class instance to the Tag property..
                sttcMain.LastAddedDocument.Tag =
                    fileSave.AddOrUpdateFile(sttcMain.LastAddedDocument, true, false, false);

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
    /// <param name="fromShellContext">A value indicating whether the file was opened from the Windows shell.</param>
    private void OpenDocument(string fileName,
        Encoding encoding,
        bool reloadContents, bool encodingOverridden,
        bool overrideDetectBom = false, bool fromShellContext = false)
    {
        var encodingList =
            new List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)>
            {
                (encoding.WebName, encoding, false, false),
            };

        OpenDocument(fileName, encodingList, reloadContents, encodingOverridden, overrideDetectBom, fromShellContext);
    }


    /// <summary>
    /// Opens the document with a given file name into the view.
    /// </summary>
    /// <param name="fileName">Name of the file to load into the view.</param>
    /// <param name="encodings">The encodings to be used to try to open the file.</param>
    /// <param name="reloadContents">An indicator if the contents of the document should be reloaded from the file system.</param>
    /// <param name="encodingOverridden">The given encoding should be used while opening the file.</param>
    /// <param name="fromShellContext">A value indicating whether the file was opened from the Windows shell.</param>
    /// <param name="overrideDetectBom">if set to <c>true</c> the setting value whether to detect unicode file with no byte-order-mark (BOM) is overridden.</param>
    /// <returns>True if the operation was successful; otherwise false.</returns>
    private void OpenDocument(string fileName, List<(string encodingName, Encoding encoding, 
            bool unicodeFailOnInvalidChar, bool unicodeBOM)> encodings, bool reloadContents, bool encodingOverridden,
        bool overrideDetectBom = false, bool fromShellContext = false)
    {
        Encoding encoding = null;
        if (File.Exists(fileName))
        {
            try
            {
                foreach (var encodingData in encodings)
                {
                    // the encoding shouldn't change based on the file's contents if a snapshot of the file already exists in the database..
                    encoding = GetFileEncoding(CurrentSession.SessionName, fileName, encodingData.encoding, reloadContents, encodingOverridden, 
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
                MessageBoxExtended.Show(
                    DBLangEngine.GetMessage("msgErrorOpeningFile", "Error opening file '{0}' with message: '{1}'.|Some kind of error occurred while opening a file.",
                        fileName, ex.GetBaseException().Message),
                    DBLangEngine.GetMessage("msgError",
                        "Error|A message describing that some kind of error occurred."), MessageBoxButtonsExtended.OK,
                    MessageBoxIcon.Error, ExtendedDefaultButtons.Button1);
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

                    // check the database first for a FileSave class instance..
                    var fileSave = ScriptNotepadDbContext.DbContext.FileSaves.FirstOrDefault(f =>
                        f.Session.SessionName == currentSession.SessionName && f.FileNameFull == fileName);

                    if (sttcMain.LastAddedDocument.Tag == null && fileSave == null)
                    {
                        sttcMain.LastAddedDocument.Tag =
                            FileSaveHelper.CreateFromTabbedDocument(sttcMain.LastAddedDocument, encoding,
                                currentSession);
                    }
                    else if (fileSave != null)
                    {
                        sttcMain.LastAddedDocument.Tag = fileSave;
                        sttcMain.LastAddedDocument.ID = fileSave.Id;
                        fileSave.SetEncoding(encoding ?? new UTF8Encoding(true));

                        // set the saved position of the document's caret..
                        if (fileSave.CurrentCaretPosition > 0 && fileSave.CurrentCaretPosition < sttcMain.LastAddedDocument.Scintilla.TextLength)
                        {
                            sttcMain.LastAddedDocument.Scintilla.CurrentPosition = fileSave.CurrentCaretPosition;
                            sttcMain.LastAddedDocument.Scintilla.SelectionStart = fileSave.CurrentCaretPosition;
                            sttcMain.LastAddedDocument.Scintilla.SelectionEnd = fileSave.CurrentCaretPosition;
                            sttcMain.LastAddedDocument.Scintilla.ScrollCaret();
                        }
                    }

                    sttcMain.LastAddedDocument.Scintilla.TabWidth = FormSettings.Settings.EditorTabWidth;

                    // get a FileSave class instance from the document's tag..
                    fileSave = (FileSave)sttcMain.LastAddedDocument.Tag;

                    // set the session ID number..
                    fileSave.Session = CurrentSession;

                    // not history at least anymore..
                    fileSave.IsHistory = false;

                    // ..update the database with the document..
                    fileSave = fileSave.AddOrUpdateFile(sttcMain.LastAddedDocument, true, false, true);
                    sttcMain.LastAddedDocument.ID = fileSave.Id;

                    if (reloadContents)
                    {
                        fileSave.ReloadFromDisk(sttcMain.LastAddedDocument);
                    }

                    // save the FileSave class instance to the Tag property..
                    sttcMain.LastAddedDocument.Tag = fileSave;

                    // append possible style and spell checking for the document..
                    AppendStyleAndSpellChecking(sttcMain.LastAddedDocument);

                    // set the lexer type from the saved database value..
                    sttcMain.LastAddedDocument.LexerType = fileSave.LexerType;

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
                    sttcMain.LastAddedDocument.ZoomPercentage = fileSave.EditorZoomPercentage;

                    // check the programming language menu item with the current lexer..
                    ProgrammingLanguageHelper.CheckLanguage(sttcMain.LastAddedDocument.LexerType);

                    // set the spell checker state based on the settings and the type of the
                    // file open method..
                    SetSpellCheckerState(
                        fromShellContext
                            ? FormSettings.Settings.EditorUseSpellCheckingShellContext
                            : FormSettings.Settings.EditorUseSpellChecking, false);
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
                // get the FileSave class instance from the document's tag..
                var fileSave = (FileSave)document.Tag;

                // set the contents to match the document's text..
                fileSave.SetContents(document.Scintilla.Text, true, true, true);

                // only an existing file can be saved directly..
                if (fileSave.ExistsInFileSystem && !saveAs)
                {
                    File.WriteAllBytes(fileSave.FileNameFull, fileSave.GetFileContents() ?? Array.Empty<byte>());

                    // update the file system modified time stamp so the software doesn't ask if the file should
                    // be reloaded from the file system..
                    fileSave.FileSystemModified = new FileInfo(fileSave.FileNameFull).LastWriteTime;
                    fileSave.FileSystemSaved = fileSave.FileSystemModified;
                    fileSave.SetDatabaseModified(fileSave.FileSystemModified);
                        
                    document.Tag = fileSave.AddOrUpdateFile();
                }
                // the file doesn't exist in the file system or the user wishes to use save as dialog so
                // display a save file dialog..
                else 
                {
                    sdAnyFile.Title = DBLangEngine.GetMessage("msgDialogSaveAs",
                        "Save as|A title for a save file dialog to indicate user that a file is being saved as with a new file name");

                    sdAnyFile.InitialDirectory = FormSettings.Settings.FileLocationSaveAs;

                    sdAnyFile.FileName = fileSave.FileNameFull;
                    if (sdAnyFile.ShowDialog() == DialogResult.OK)
                    {
                        FormSettings.Settings.FileLocationSaveAs = Path.GetDirectoryName(sdAnyFile.FileName);

                        fileSave.FileSystemModified = DateTime.Now;

                        // write the new contents of a file to the existing file overriding it's contents..
                        File.WriteAllBytes(sdAnyFile.FileName, fileSave.GetFileContents() ?? Array.Empty<byte>());

                        // the file now exists in the file system..
                        fileSave.ExistsInFileSystem = true;

                        // the file now has a location so update it..
                        fileSave.UpdateFileData(sdAnyFile.FileName);

                        // update the document..
                        document.FileName = fileSave.FileNameFull;
                        document.FileNameNotPath = fileSave.FilePath;
                        document.FileTabButton.Text = fileSave.FileName;

                        // a new lexer might have to be assigned..
                        document.LexerType = LexerFileExtensions.LexerTypeFromFileName(fileSave.FileNameFull);

                        // update the file system modified time stamp so the software doesn't ask if the file should
                        // be reloaded from the file system..
                        fileSave.FileSystemModified = new FileInfo(fileSave.FileNameFull).LastWriteTime;
                        fileSave.FileSystemSaved = fileSave.FileSystemModified;
                        fileSave.SetDatabaseModified(fileSave.FileSystemModified);

                        // update document misc data, i.e. the assigned lexer to the database..
                        fileSave.AddOrUpdateFile();

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
            // get the FileSave class instance from the tag..
            var fileSave = (FileSave)document.Tag;
            if (fileSave.ExistsInFileSystem)
            {
                // save the document..
                SaveDocument(document, nonExisting);
            }
        }
    }
    #endregion

    #region InternalEvents
    // the menu including time formats is about to be opened..
    private void MnuInsertDateAndTime_DropDownOpening(object sender, EventArgs e)
    {
        var toolStripMenuItem = (ToolStripMenuItem) sender;
        var useInvariantCulture = FormSettings.Settings.DateFormatUseInvariantCulture;

        foreach (ToolStripMenuItem toolStripSubMenuItem in toolStripMenuItem.DropDownItems)
        {
            var formatNumber = int.Parse(toolStripSubMenuItem.Tag.ToString() ?? "0");
            string format;
            switch (formatNumber)
            {
                case 0:
                    format = FormSettings.Settings.DateFormat1;
                    break;
                case 1:
                    format = FormSettings.Settings.DateFormat2;
                    break;
                case 2:
                    format = FormSettings.Settings.DateFormat3;
                    break;
                case 3:
                    format = FormSettings.Settings.DateFormat4;
                    break;
                case 4:
                    format = FormSettings.Settings.DateFormat5;
                    break;
                case 5:
                    format = FormSettings.Settings.DateFormat6;
                    break;
                default:
                    format = FormSettings.Settings.DateFormat1;
                    break;
            }

            // must try in case the user has specified and invalid date-time format..
            try
            {

                toolStripSubMenuItem.Text = DBLangEngine.GetMessage("msgDateTimeMenuItemText",
                    "Insert date and time type {0}: '{1}'|A message describing a text to insert a date and/or time to a Scintilla instance via a menu strip item.",
                    formatNumber + 1,
                    DateTime.Now.ToString(format,
                        // we need to ensure that an overridden thread locale will not affect the non-invariant culture setting..
                        useInvariantCulture ? CultureInfo.InvariantCulture : CultureInfo.InstalledUICulture));
            }
            catch (Exception ex)
            {
                toolStripSubMenuItem.Text = DBLangEngine.GetMessage(
                    "msgDateTimeInvalidFormat",
                    "Invalid date and/or time format|The user has issued an non-valid formatted date and/or time formatting string.");

                // log the exception..
                ExceptionLogger.LogError(ex);
            }
        }
    }

    private void GotoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        CurrentScintillaAction(FormDialogQueryJumpLocation.Execute);
    }

    // user wants to insert a date and/or time to the current Scintilla document..
    private void MnuDate1_Click(object sender, EventArgs e)
    {
        CurrentScintillaAction(scintilla =>
        {
            string format;

            int formatNumber = int.Parse(((ToolStripMenuItem) sender).Tag.ToString() ?? "0");

            var useInvariantCulture = FormSettings.Settings.DateFormatUseInvariantCulture;

            switch (formatNumber)
            {
                case 0:
                    format = FormSettings.Settings.DateFormat1;
                    break;
                case 1:
                    format = FormSettings.Settings.DateFormat2;
                    break;
                case 2:
                    format = FormSettings.Settings.DateFormat3;
                    break;
                case 3:
                    format = FormSettings.Settings.DateFormat4;
                    break;
                case 4:
                    format = FormSettings.Settings.DateFormat5;
                    break;
                case 5:
                    format = FormSettings.Settings.DateFormat6;
                    break;
                default:
                    format = FormSettings.Settings.DateFormat1;
                    break;
            }

            try
            {
                scintilla.InsertText(scintilla.CurrentPosition,
                    DateTime.Now.ToString(format,
                        // we need to ensure that an overridden thread locale will not affect the non-invariant culture setting..
                        useInvariantCulture ? CultureInfo.InvariantCulture : CultureInfo.InstalledUICulture));
            }
            // must try in case the user has specified and invalid date-time format..
            catch (Exception ex)
            {
                // TODO:: Show an error dialog..

                // log the exception..
                ExceptionLogger.LogError(ex);
            }
        });
    }

    // handles navigation to the next and previous tab and
    // to the next and previous session..
    private void MnuNextPrevious_Click(object sender, EventArgs e)
    {
        if (sender.Equals(mnuNextTab))
        {
            sttcMain.NextTab(true);
        }
        else if (sender.Equals(mnuPreviousTab))
        {
            sttcMain.PreviousTab(true);
        }
    }

    private void FormMain_KeyDown(object sender, KeyEventArgs e)
    {
        // a user wishes to navigate within the FormSearchResultTree..
        if (e.OnlyAlt() && e.KeyCodeIn(Keys.Left, Keys.Right, Keys.X))
        {
            // validate that there is an instance of the FormSearchResultTree which is visible..
            if (FormSearchResultTree.PreviousInstance != null && FormSearchResultTree.PreviousInstance.Visible)
            {
                // Alt+Left navigates to the previous tree node within the form..
                if (e.KeyCode == Keys.Left) 
                {
                    FormSearchResultTree.PreviousInstance.PreviousOccurrence();
                }
                // Alt+Right navigates to the next tree node within the form..
                else if (e.KeyCode == Keys.Right)
                {
                    FormSearchResultTree.PreviousInstance.NextOccurrence();
                }
                // Alt+X closes the FormSearchResultTree instance..
                else
                {
                    FormSearchResultTree.PreviousInstance.CloseTree();
                }

                // this is handled..
                e.Handled = true;
            }
            return;
        }

        // a user pressed the insert key a of a keyboard, which indicates toggling for
        // insert / override mode for the Scintilla control..
        if (e.KeyCode == Keys.Insert && e.NoModifierKeysDown())
        {
            // only if a document exists..
            if (sttcMain.CurrentDocument != null)
            {
                // ..set the insert / override text for the status strip..
                StatusStripTexts.SetInsertOverrideStatusStripText(sttcMain.CurrentDocument, true);
            }
            return;
        }

        if (e.KeyCode == Keys.F3)
        {
            if (e.OnlyShift() || e.NoModifierKeysDown())
            {
                // find the next result if available..
                FormSearchAndReplace.Instance.Advance(!e.OnlyShift());

                // this is handled..
                e.Handled = true;
                return;
            }
        }

        if (e.KeyCodeIn(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.PageDown, Keys.PageUp))
        {
            // set the flag to suspend the selection update to avoid excess CPU load..
            suspendSelectionUpdate = true;
        }
    }

    // the navigation menu is opening, so set the drop-down items states accordingly..
    private void MnuNavigation_DropDownOpening(object sender, EventArgs e)
    {
        mnuNextTab.Enabled = sttcMain.DocumentsCount > 0;
        mnuPreviousTab.Enabled = sttcMain.DocumentsCount > 0;
    }

    private void FormMain_ApplicationActivated(object sender, EventArgs e)
    {
        FormSearchAndReplace.Instance.TopMost = true;
    }

    private void FormMain_ApplicationDeactivated(object sender, EventArgs e)
    {
        FormSearchAndReplace.Instance.TopMost = false;
    }

    // a user wishes to search for an open file within the tabbed documents..
    private void MnuFindTab_Click(object sender, EventArgs e)
    {
        TimersEnabled = false;
        FormDialogSelectFileTab.ShowDialog(sttcMain);
        TimersEnabled = true;
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
                    PrinterSettings = pdPrint.PrinterSettings,
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
                sdHTML.InitialDirectory = FormSettings.Settings.FileLocationSaveAsHtml;
                if (sdHTML.ShowDialog() == DialogResult.OK)
                {
                    FormSettings.Settings.FileLocationSaveAsHtml = Path.GetDirectoryName(sdHTML.FileName);
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

                LastAddedFileSaveAction(fileSave => { fileSave.LexerType = LexerEnumerations.LexerType.HTML; });

                LastAddedFileSaveAction(fileSave => { fileSave.AddOrUpdateFile(); });

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
        SaveDocumentsToDatabase();
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
                var fileSave = (FileSave) document.Tag;
                fileSave.EditorZoomPercentage = e.ZoomPercentage;
                fileSave.AddOrUpdateFile();
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
                var fileSave = (FileSave) document.Tag;
                fileSave.LexerType = e.LexerType;
                fileSave.AddOrUpdateFile();
            }
        });
    }

    // a user wishes to alphabetically order the the lines of the active document in ascending or descending order..
    private void MnuSortLines_Click(object sender, EventArgs e)
    {
        CurrentScintillaAction(scintilla =>
        {
            SortLines.Sort(scintilla, FormSettings.Settings.TextCurrentComparison, sender.Equals(mnuSortDescending));
        });
    }

    // a user wishes to use custom ordering the the lines of the active document in ascending or descending order..
    private void mnuCustomizedSort_Click(object sender, EventArgs e)
    {
        CurrentDocumentAction(document =>
        {
            if (document.SelectionLength > 0 && document.SelectionStartLine == document.SelectionEndLine)
            {
                FormDialogQuerySortTextStyle.ShowDialog(this, document.Scintilla, this, document.SelectionStartColumn + 1, document.SelectionLength);
                return;
            }

            FormDialogQuerySortTextStyle.ShowDialog(this, document.Scintilla, this);
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
        if (FileIoPermission.FileRequiresElevation(odAnyFile.FileName).ElevationRequied)
        {
            if (MessageBoxExtended.Show(
                    DBLangEngine.GetMessage("msgElevationRequiredForFile",
                        "Opening the file '{0}' requires elevation (Run as Administrator). Restart the software as Administrator?|A message describing that a access to a file requires elevated permissions (Administrator)", odAnyFile.FileName),
                    DBLangEngine.GetMessage("msgConfirm", "Confirm|A caption text for a confirm dialog."),
                    MessageBoxButtonsExtended.YesNo, MessageBoxIcon.Question, ExtendedDefaultButtons.Button2) == DialogResultExtended.Yes)
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
        if (e.RecentFile.ExistsInDatabase(ScriptNotepadDbContext.DbContext.FileSaves))
        {
            LoadDocumentFromDatabase(e.RecentFile);
        }
        // else open the file from the file system..
        else if (e.RecentFile != null)
        {
            OpenDocument(e.RecentFile.FileName, EncodingData.EncodingFromString(e.RecentFile.EncodingAsString),
                false, false);
        }
        // in this case the menu item should contain all the recent files belonging to a session..
        else if (e.RecentFiles != null)
        {
            // loop through the recent files and open them all..
            foreach (var recentFile in e.RecentFiles)
            {
                // if a file snapshot exists in the database then load it..
                if (recentFile.ExistsInDatabase(ScriptNotepadDbContext.DbContext.FileSaves))
                {
                    LoadDocumentFromDatabase(recentFile);
                }
                // else open the file from the file system..
                else
                {
                    OpenDocument(recentFile.FileNameFull,
                        EncodingData.EncodingFromString(recentFile.EncodingAsString), false, false);
                }
            }
        }
    }

    /// <summary>
    /// Loads the document from the database based on a given <paramref name="recentFile"/> class instance.
    /// </summary>
    /// <param name="recentFile">A <see cref="RecentFile"/> class instance containing the file data.</param>
    private void LoadDocumentFromDatabase(RecentFile recentFile)
    {
        // get the file from the database..
        var file = ScriptNotepadDbContext.DbContext.FileSaves.FirstOrDefault(f =>
            f.FileNameFull == recentFile.FileNameFull && f.Session.SessionName == recentFile.Session.SessionName);

        // only if something was gotten from the database..
        if (file != null)
        {
            if (!FormSettings.Settings.EditorSaveZoom)
            {
                file.EditorZoomPercentage = 100;
            }

            sttcMain.AddDocument(file.FileNameFull, file.Id, file.GetEncoding(), file.GetFileContentsAsMemoryStream());
            if (sttcMain.LastAddedDocument != null)
            {
                // append additional initialization to the document..
                AdditionalInitializeDocument(sttcMain.LastAddedDocument);

                // set the lexer type from the saved database value..
                sttcMain.LastAddedDocument.LexerType = file.LexerType;

                // not history any more..
                file.IsHistory = false;

                // set the saved position of the document's caret..
                if (file.CurrentCaretPosition > 0 && file.CurrentCaretPosition < sttcMain.LastAddedDocument.Scintilla.TextLength)
                {
                    sttcMain.LastAddedDocument.Scintilla.CurrentPosition = file.CurrentCaretPosition;
                    sttcMain.LastAddedDocument.Scintilla.SelectionStart = file.CurrentCaretPosition;
                    sttcMain.LastAddedDocument.Scintilla.SelectionEnd = file.CurrentCaretPosition;
                    sttcMain.LastAddedDocument.Scintilla.ScrollCaret();
                    sttcMain.LastAddedDocument.FileTabButton.IsSaved = !IsFileChanged(file);
                }

                sttcMain.LastAddedDocument.Scintilla.TabWidth = FormSettings.Settings.EditorTabWidth;

                // update the history flag to the database..
                ScriptNotepadDbContext.DbContext.SaveChanges();

                // assign the context menu strip for the tabbed document..
                sttcMain.LastAddedDocument.FileTabButton.ContextMenuStrip = cmsFileTab;

                // set the zoom value..
                sttcMain.LastAddedDocument.ZoomPercentage = file.EditorZoomPercentage;

                // enabled the caret line background color..
                SetCaretLineColor();

                // set the brace matching if enabled..
                SetStyleBraceMatch.SetStyle(sttcMain.LastAddedDocument.Scintilla);

                sttcMain.LastAddedDocument.Tag = file;

                // the file load can't add an undo option the Scintilla..
                sttcMain.LastAddedDocument.Scintilla.EmptyUndoBuffer();

                // ReSharper disable once ObjectCreationAsStatement
                new TabbedDocumentSpellCheck(sttcMain.LastAddedDocument, !FormSettings.Settings.EditorSpellUseCustomDictionary);

                // set the misc indicators..
                SetDocumentMiscIndicators(sttcMain.LastAddedDocument);
            }
            sttcMain.ActivateDocument(file.FileNameFull);

            UpdateToolbarButtonsAndMenuItems();

            // re-create a menu for recent files..
            RecentFilesMenuBuilder.CreateRecentFilesMenu(mnuRecentFiles, CurrentSession, HistoryListAmount, true, mnuSplit2);
        }
    }

    // a user wishes to change the settings of the software..
    private void mnuSettings_Click(object sender, EventArgs e)
    {
        // ..so display the settings dialog..
        if (FormSettings.Execute(out var restart))
        {
            // if the user chose to restart the application for the changes to take affect..
            if (restart)
            {
                Program.Restart = true;
                Close();
            }
        }
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
            StatusStripTexts.SetStatusStringText(sttcMain.CurrentDocument, CurrentSession.SessionName);
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
                    // get a FileSave class instance from the document's tag..
                    var fileSave = (FileSave) document.Tag;

                    if (e.KeyCode == Keys.Z)
                    {
                        // undo the encoding change..
                        fileSave.UndoEncodingChange();
                        document.Tag = fileSave;
                    }

                    fileSave.PopPreviousDbModified();
                    document.FileTabButton.IsSaved = !IsFileChanged(fileSave);
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
        StatusStripTexts.SetStatusStringText(sttcMain.CurrentDocument, CurrentSession.SessionName);
    }

    // this event is raised when another instance of this application receives a file name
    // via the IPC (no multiple instance allowed)..
    private void RemoteMessage_MessageReceived(object sender, IpcExchangeEventArgs<string> e)
    {
        Invoke(new System.Windows.Forms.MethodInvoker(delegate
        {
            OpenDocument(e.Object, DefaultEncodings, 
                false, false, false, true);
            // the user probably will like the program to show up, if the software activates
            // it self from a windows shell call to open a file..
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = previousWindowState;
            }
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

    // fold the current level..
    private void mnuFoldCurrentLevel_Click(object sender, EventArgs e)
    {
        CurrentDocumentAction(document =>
        {
            if (document.Scintilla.CurrentLine != -1)
            {
                var parent = document.Scintilla.Lines[document.Scintilla.CurrentLine].FoldParent;
                if (parent >= 0)
                {
                    document.Scintilla.Lines[parent].FoldLine(sender.Equals(mnuFoldCurrentLevel) ? FoldAction.Contract : FoldAction.Expand);
                }
            }
        });
    }

    // fold a specified level..
    private void mnuFold1_Click(object sender, EventArgs e)
    {
        var level = 1;
        if (sender.Equals(mnuFold2))
        {
            level = 2;
        }
        else if (sender.Equals(mnuFold3))
        {
            level = 3;
        }
        else if (sender.Equals(mnuFold4))
        {
            level = 4;
        }
        else if (sender.Equals(mnuFold5))
        {
            level = 5;
        }
        else if (sender.Equals(mnuFold6))
        {
            level = 6;
        }
        else if (sender.Equals(mnuFold7))
        {
            level = 7;
        }
        else if (sender.Equals(mnuFold8))
        {
            level = 8;
        }

        CurrentDocumentAction(document =>
        {
            if (document.Scintilla.CurrentLine != -1)
            {
                foreach (var scintillaLine in document.Scintilla.Lines)
                {
                    if (scintillaLine.FoldLevel - 1023 == level)
                    {
                        scintillaLine.FoldLine(FoldAction.Contract);
                    }
                }
            }
        });
    }

    // unfold a specified level..
    private void mnuUnfold1_Click(object sender, EventArgs e)
    {
        var level = 1;
        if (sender.Equals(mnuUnfold2))
        {
            level = 2;
        }
        else if (sender.Equals(mnuUnfold3))
        {
            level = 3;
        }
        else if (sender.Equals(mnuUnfold4))
        {
            level = 4;
        }
        else if (sender.Equals(mnuUnfold5))
        {
            level = 5;
        }
        else if (sender.Equals(mnuUnfold6))
        {
            level = 6;
        }
        else if (sender.Equals(mnuUnfold7))
        {
            level = 7;
        }
        else if (sender.Equals(mnuUnfold8))
        {
            level = 8;
        }
            
        CurrentDocumentAction(document =>
        {
            if (document.Scintilla.CurrentLine != -1)
            {
                foreach (var scintillaLine in document.Scintilla.Lines)
                {
                    if (scintillaLine.FoldLevel - 1023 == level)
                    {
                        scintillaLine.FoldLine(FoldAction.Expand);
                    }
                }
            }
        });
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
        // save the changes into the database..
        ScriptNotepadDbContext.DbContext.SaveChanges();

        // save the possible setting changes..
        FormSettings.Settings.Save(Program.SettingFileName);

        // disable the timers not mess with application exit..
        tmAutoSave.Enabled = false;
        tmGUI.Enabled = false;
        tmSpellCheck.Enabled = false;

        Instance = null;

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
                // get the FileSave class instance from the tag..
                var fileSave = (FileSave)sttcMain.CurrentDocument.Tag;
                if (fileSave.ExistsInFileSystem) // the file exists in the file system..
                {
                    // if the file has been changed in the editor, so confirm the user for a 
                    // reload from the file system..
                    if (fileSave.IsChangedInEditor() && !runningConstructor)
                    {
                        if (MessageBoxExtended.Show(
                                DBLangEngine.GetMessage("msgFileHasChangedInEditorAction", "The file '{0}' has been changed in the editor and a reload from the file system is required. Continue?|A file has been changed in the editor and a reload from the file system is required to complete an arbitrary action", fileSave.FileNameFull),
                                DBLangEngine.GetMessage("msgFileArbitraryFileChange", "A file has been changed|A caption message for a message dialog which will ask if a changed file should be reloaded"),
                                MessageBoxButtonsExtended.YesNo,
                                MessageBoxIcon.Question,
                                ExtendedDefaultButtons.Button2) == DialogResultExtended.No)
                        {
                            return; // the user decided not to reload..
                        }
                    }

                    fileSave.SetEncoding(encoding); // set the new encoding..

                    // reload the file with the user given encoding..
                    fileSave.ReloadFromDisk(sttcMain.CurrentDocument);

                    // save the FileSave instance to the document's Tag property..
                    sttcMain.CurrentDocument.Tag = fileSave; 
                }
                // the file only exists in the database..
                else
                {
                    // convert the contents to a new encoding..
                    sttcMain.CurrentDocument.Scintilla.Text =
                        StreamStringHelpers.ConvertEncoding(fileSave.GetEncoding(), encoding, sttcMain.CurrentDocument.Scintilla.Text);

                    // save the previous encoding for an undo-possibility..
                    fileSave.AddPreviousEncoding(fileSave.GetEncoding());

                    fileSave.SetEncoding(encoding); // set the new encoding..

                    // save the FileSave instance to the document's Tag property..
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
        CurrentSession = e.Session;
    }

    // a user is logging of or the system is shutting down..
    private void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
    {
        // ..just no questions asked save the document snapshots into the SQLite database..
        SaveDocumentsToDatabase();
        EndSession(true);
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
            LocalizeRunner.RunLocalizeWindow(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "ScriptNotepad",
                // ReSharper disable once StringLiteralTypo
                "lang.sqlite"));
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
        HandleCloseTab((FileSave) e.ScintillaTabbedDocument.Tag, false, true, false,
            e.ScintillaTabbedDocument.Scintilla.CurrentPosition);

        // if there are no documents any more..
        if (sttcMain.DocumentsCount - 1 <= 0) 
        {
            // set the status strip label's to indicate that there is no active document..
            StatusStripTexts.SetEmptyTexts(CurrentSession.SessionName);

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

        StatusStripTexts.SetStatusStringText(e.ScintillaTabbedDocument, CurrentSession.SessionName);

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
            StatusStripTexts.SetStatusStringText(e.ScintillaTabbedDocument, CurrentSession.SessionName);
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
        var fileSave = (FileSave)e.ScintillaTabbedDocument.Tag;
        fileSave.SetDatabaseModified(DateTime.Now);
        fileSave.SetContents(e.ScintillaTabbedDocument.Scintilla.Text, false, false, true);

        e.ScintillaTabbedDocument.FileTabButton.IsSaved = !IsFileChanged(fileSave);

            
        // if the text has been changed and id did not occur by encoding change
        // just clear the undo "buffer"..
        if (!TextChangedViaEncodingChange)
        {
            fileSave.ClearPreviousEncodings();
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
        tmGUI.Enabled = true;
    }

    // occurs when a plug-in requests for the currently active document..
    private void RequestActiveDocument(object sender, RequestScintillaDocumentEventArgs e)
    {
        // verify that there is an active document, etc..
        if (sttcMain.CurrentDocument?.Tag != null)
        {
            var fileSave = (FileSave)sttcMain.CurrentDocument.Tag;
            e.AllDocuments = false; // set to flag indicating all the documents to false..

            // add the document details to the event arguments..
            e.Documents.Add(
                (fileSave.GetEncoding(),
                    sttcMain.CurrentDocument.Scintilla,
                    fileSave.FileNameFull,
                    fileSave.FileSystemModified,
                    fileSave.DatabaseModified, 
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
            var fileSave = (FileSave)sttcMain.Documents[i].Tag;

            // add the document details to the event arguments..
            e.Documents.Add(
                (fileSave.GetEncoding(),
                    sttcMain.Documents[i].Scintilla,
                    fileSave.FileNameFull,
                    fileSave.FileSystemModified,
                    fileSave.DatabaseModified,
                    true));
        }
        e.AllDocuments = true; // set to flag indicating all the documents to true..
    }

    // occurs when an exception has occurred in a plug-in (NOTE: The plug-in must have exception handling!)..
    private void PluginException(object sender, PluginExceptionEventArgs e)
    {
        ExceptionLogger.LogError(e.Exception, $"PLUG-IN EXCEPTION: '{e.PluginModuleName}'.");
        int idx = Plugins.FindIndex(f => f.Plugin.PluginName == e.PluginModuleName);
        if (idx != -1)
        {
            Plugins[idx].Plugin.ExceptionCount++;
            ScriptNotepadDbContext.DbContext.SaveChanges();
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
                int idx = Plugins.FindIndex(f => f.Plugin.Id == plugin.Id);

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
            if (WindowState == FormWindowState.Minimized)
            {
                FormSearchAndReplace.Instance.ToggleVisible(this, false);
            }
            else if (previousWindowState != WindowState &&
                     (WindowState == FormWindowState.Maximized || WindowState == FormWindowState.Normal))
            {
                FormSearchAndReplace.Instance.ToggleVisible(this, true);
            }

            SizeVisibilityChange?.Invoke(this, new MainFormSizeEventArgs { Size = Size, PreviousSize = PreviousSize, PreviousState = previousWindowState, State = WindowState, Visible = Visible, Location = Location, PreviousLocation = PreviousLocation, });

            PreviousSize = Size;
        }
    }

    // rise the SizeVisibilityChange event when visibility changes..
    private void FormMain_VisibleChanged(object sender, EventArgs e)
    {
        SizeVisibilityChange?.Invoke(this, new MainFormSizeEventArgs { Size = Size, PreviousSize = PreviousSize, PreviousState = previousWindowState, State = WindowState, Visible = Visible, Location = Location, PreviousLocation = PreviousLocation, });
    }

    // save the previous location and raise the SizeVisibilityChange event..
    private void FormMain_LocationChanged(object sender, EventArgs e)
    {
        SizeVisibilityChange?.Invoke(this, new MainFormSizeEventArgs { Size = Size, PreviousSize = PreviousSize, PreviousState = previousWindowState, State = WindowState, Visible = Visible, Location = Location, PreviousLocation = PreviousLocation, });
        PreviousLocation = Location;
    }

    /// <summary>
    /// Processes Windows messages.
    /// </summary>
    /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"/> to process.</param>
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    protected override void WndProc(ref Message m)
    {
        // ReSharper disable once InconsistentNaming
        const int WM_SYSCOMMAND = 0x0112;
        // ReSharper disable once InconsistentNaming
        const uint SC_MINIMIZE = 0xF020;
        // ReSharper disable once InconsistentNaming
        const uint SC_MAXIMIZE = 0xF030;
        // ReSharper disable once InconsistentNaming
        const uint SC_RESTORE = 0xF120;

        if (m.Msg == WM_SYSCOMMAND)
        {
            if (m.WParamLoWordUnsigned() == SC_MINIMIZE ||
                m.WParamLoWordUnsigned() == SC_MAXIMIZE ||
                m.WParamLoWordUnsigned() == SC_RESTORE)
            {
                SizeVisibilityChange?.Invoke(this, new MainFormSizeEventArgs { Size = Size, PreviousSize = PreviousSize, PreviousState = previousWindowState, State = WindowState, Visible = Visible, Location = Location, PreviousLocation = PreviousLocation, });
                previousWindowState = WindowState;
            }
        }

        base.WndProc(ref m);
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
            int styleNum = int.Parse(((ToolStripMenuItem) sender).Tag.ToString() ?? "-1");


            if (styleNum != -1)
            {
                Highlight.HighlightWords(sttcMain.CurrentDocument.Scintilla, styleNum,
                    sttcMain.CurrentDocument.Scintilla.SelectedText, FormSettings.Settings.GetMarkColor(styleNum - 9));
            }
        }
    }

    // a user wishes to clear style mark of style (1..5) from the editor..
    private void ClearStyleOf_Click(object sender, EventArgs e)
    {
        if (sttcMain.CurrentDocument != null)
        {
            Highlight.ClearStyle(sttcMain.CurrentDocument.Scintilla,
                int.Parse(((ToolStripMenuItem) sender).Tag.ToString() ?? "-1"));
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
            // get the FileSave class instance from the document's tag..
            var fileSave = (FileSave) sttcMain.CurrentDocument.Tag;

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
                fileSave.SetEncoding(
                    GetFileEncoding(CurrentSession.SessionName, sttcMain.CurrentDocument.FileName, fileSave.GetEncoding(), true,
                        false, false, out _, out _, out _));

                // the user answered yes..
                sttcMain.SuspendTextChangedEvents =
                    true; // suspend the changed events on the ScintillaTabbedTextControl..

                fileSave.ReloadFromDisk(sttcMain.CurrentDocument); // reload the file..
                sttcMain.SuspendTextChangedEvents =
                    false; // resume the changed events on the ScintillaTabbedTextControl..

                // just in case set the tag back..
                sttcMain.CurrentDocument.Tag = fileSave;

                fileSave.SetDatabaseModified(fileSave.FileSystemModified);

                sttcMain.CurrentDocument.FileTabButton.IsSaved = !IsFileChanged(fileSave);

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

    // a menu within the application is opening..
    private void MenuCommon_DropDownOpening(object sender, EventArgs e)
    {
        if (sender.Equals(mnuEdit)) // the edit menu is opening..
        {
            // get the FileSave from the active document..
            var fileSave = (FileSave) sttcMain.CurrentDocument?.Tag;

            if (fileSave != null) // the second null check..
            {
                // enable / disable items which requires the file to exist in the file system..
                mnuRenameNewFileMainMenu.Enabled = !fileSave.ExistsInFileSystem;
            }
        }

        if (sender.Equals(mnuText)) // the text menu is opening..
        {
            mnuSortLines.Enabled = sttcMain.CurrentDocument != null;
            mnuRemoveDuplicateLines.Enabled = sttcMain.CurrentDocument != null;
            mnuWrapDocumentTo.Enabled = sttcMain.CurrentDocument != null;
        }
    }

    // removes duplicate lines from a Scintilla control..
    private void mnuRemoveDuplicateLines_Click(object sender, EventArgs e)
    {
        CurrentDocumentAction(document =>
        {
            var fileSave = (FileSave) document.Tag;

            DuplicateLines.RemoveDuplicateLines(document.Scintilla,
                FormSettings.Settings.TextCurrentComparison,
                fileSave.GetFileLineType());

            if (!suspendSelectionUpdate)
            {
                StatusStripTexts.SetStatusStringText(document, CurrentSession.SessionName);
            }
        });
    }

    // set the value indicating whether Text menu functions should be case-sensitive..
    private void mnuCaseSensitive_Click(object sender, EventArgs e)
    {
        if (runningConstructor)
        {
            return;
        }

        var item = (ToolStripMenuItem) sender;
        item.Checked = !item.Checked;
        FormSettings.Settings.TextUpperCaseComparison = item.Checked;
    }

    // a user wants to jump to the last or to the first tab..
    private void mnuFirstLastTab_Click(object sender, EventArgs e)
    {
        if (sttcMain.DocumentsCount > 0)
        {
            var leftIndex = sender.Equals(mnuLastTab) ? sttcMain.DocumentsCount - 1 : 0;
            sttcMain.LeftFileIndex = leftIndex;
        }
    }

    private void mnuJsonPrettify_Click(object sender, EventArgs e)
    {
        CurrentDocumentAction(document =>
        {
            if (document.Tag != null)
            {
                document.Scintilla.Text = sender.Equals(mnuJsonPrettify)
                    ? document.Scintilla.Text.JsonPrettify()
                    : document.Scintilla.Text.JsonUglify();
            }
        });
    }

    private void mnuXMLPrettify_Click(object sender, EventArgs e)
    {
        CurrentDocumentAction(document =>
        {
            if (document.Tag != null)
            {
                document.Scintilla.Text = sender.Equals(mnuXMLPrettify)
                    ? document.Scintilla.Text.XmlPrettify()
                    : document.Scintilla.Text.XmlUglify();
            }
        });
    }

    private void mnuBase64ToString_Click(object sender, EventArgs e)
    {
        CurrentDocumentAction(document =>
        {
            if (document.Tag != null)
            {
                if (sender.Equals(mnuBase64ToString))
                {
                    document.Scintilla.SelectionReplaceWithValue(document.Scintilla.SelectedText.ToBase64());
                }
                else
                {
                    document.Scintilla.SelectionReplaceWithValue(document.Scintilla.SelectedText.FromBase64());
                }
            }
        });
    }

    private void mnuRunScriptOrCommand_Click(object sender, EventArgs e)
    {
        ToggleSnippetRunner();
    }

    // fold all the document lines..
    private void mnuFoldAll_Click(object sender, EventArgs e)
    {
        CurrentDocumentAction(document =>
        {
            foreach (var scintillaLine in document.Scintilla.Lines)
            {
                scintillaLine.FoldLine(FoldAction.Contract);
            }
        });
    }

    // unfold all the document lines..
    private void mnuUnfoldAll_Click(object sender, EventArgs e)
    {
        CurrentDocumentAction(document =>
        {
            foreach (var scintillaLine in document.Scintilla.Lines)
            {
                scintillaLine.FoldLine(FoldAction.Expand);
            }
        });
    }
    #endregion

    #region PrivateFields                
    /// <summary>
    /// A flag indicating whether the the dialog is still running the code within the constructor.
    /// </summary>
    private readonly bool runningConstructor = true;

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
    /// Gets the message box stack containing the <see cref="MessageBoxExtended"/> instances.
    /// </summary>
    private MessageBoxExpandStack BoxStack { get; }

    /// <summary>
    /// Gets or sets the value whether the timers within the program should be enabled or disabled.
    /// </summary>
    private bool TimersEnabled
    {
        // three timers..

        set
        {
            // comparison for the three timers..
            if ((tmAutoSave.Enabled || tmGUI.Enabled || tmSpellCheck.Enabled) && !value)
            {
                tmAutoSave.Enabled = false;
                tmGUI.Enabled = false;
                tmSpellCheck.Enabled = false;
            }

            if ((!tmAutoSave.Enabled || !tmGUI.Enabled || !tmSpellCheck.Enabled) && value)
            {
                tmAutoSave.Enabled = true;
                tmGUI.Enabled = true;
                tmSpellCheck.Enabled = true;
            }
        }
    }

    /// <summary>
    /// Gets or sets the IPC server.
    /// </summary>
    /// <value>The IPC server.</value>
    private static RpcSelfHost<string>? IpcServer { get; set; }

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
    private bool ConstructorFinished { get; }

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

    // a field for the CurrentSession property..
    private FileSession currentSession;

    /// <summary>
    /// Gets or sets the current session for the documents.
    /// </summary>
    private FileSession CurrentSession
    {
        get => currentSession;
        set
        {
            if (value == null) // don't allow a null value..
            {
                return;
            }

            if (value != currentSession)
            {
                FormSettings.Settings.CurrentSessionEntity = value;

                CloseSession();
                LoadDocumentsFromDatabase(value.SessionName);
            }
            currentSession = value;
        }
    }

    /// <summary>
    /// Gets or sets the loaded active plug-ins.
    /// </summary>
    private List<(Assembly Assembly, IScriptNotepadPlugin PluginInstance, Plugin Plugin)> Plugins { get; } =
        new();

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
    #endregion

    #region InternalProperties        
    internal static FormMain Instance { get; private set; }

    /// <summary>
    /// Sets the active <see cref="Scintilla"/> document.
    /// </summary>
    /// <value>The active <see cref="Scintilla"/> document.</value>
    internal Scintilla ActiveScintilla => sttcMain.CurrentDocument?.Scintilla;

    /// <summary>
    /// Gets the previous size of this form before resize.
    /// </summary>
    /// <value>The previous size of this form before resize.</value>
    internal Size PreviousSize { get; set; } = Size.Empty;

    /// <summary>
    /// Gets the previous location of this form before the change.
    /// </summary>
    /// <value>The previous location of this form before the change.</value>
    internal Point PreviousLocation { get; set; } = Point.Empty;
    #endregion

    #region Events
    internal event EventHandler<MainFormSizeEventArgs> SizeVisibilityChange;
    #endregion

    #region FileContextMenu
    // a user wishes to do "do something" with the file (existing one)..
    private void CommonContextMenu_FileInteractionClick(object sender, EventArgs e)
    {
        if (sttcMain.CurrentDocument != null) // the first null check..
        {
            var document = sttcMain.CurrentDocument; // get the active document..

            // get the FileSave from the active document..
            var fileSave = (FileSave)sttcMain.CurrentDocument.Tag; 

            if (fileSave != null) // the second null check..
            {
                // based on the sending menu item, select the appropriate action..
                if (sender.Equals(mnuOpenContainingFolderInCmd))
                {
                    // open the command prompt with the file's path..
                    CommandPromptInteraction.OpenCmdWithPath(Path.GetDirectoryName(fileSave.FileNameFull));
                }
                else if (sender.Equals(mnuOpenContainingFolderInWindowsPowerShell))
                {
                    // open the Windows PowerShell with the file's path..
                    CommandPromptInteraction.OpenPowerShellWithPath(Path.GetDirectoryName(fileSave.FileNameFull));
                }
                else if (sender.Equals(mnuOpenContainingFolderInExplorer))
                {
                    // open the Windows explorer and select the file from it..
                    WindowsExplorerInteraction.ShowFileOrPathInExplorer(fileSave.FileNameFull);
                }
                else if (sender.Equals(mnuOpenWithAssociatedApplication))
                {
                    // open the file with an associated software..
                    WindowsExplorerInteraction.OpenWithAssociatedProgram(fileSave.FileNameFull);
                }
                else if (sender.Equals(mnuFullFilePathToClipboard))
                {
                    // copy the full file path to the clipboard..
                    ClipboardTextHelper.ClipboardSetText(Path.GetDirectoryName(fileSave.FileNameFull));
                }
                else if (sender.Equals(mnuFullFilePathAndNameToClipboard))
                {
                    // copy the full file name to the clipboard..
                    ClipboardTextHelper.ClipboardSetText(fileSave.FileNameFull);
                }
                else if (sender.Equals(mnuFileNameToClipboard))
                {
                    // copy the file name to the clipboard..
                    ClipboardTextHelper.ClipboardSetText(Path.GetFileName(fileSave.FileNameFull));
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
        // get the FileSave from the active document..
        var fileSave = (FileSave) sttcMain.CurrentDocument?.Tag;

        if (fileSave != null) // the second null check..
        {
            // enable / disable items which requires the file to exist in the file system..
            mnuOpenContainingFolderInExplorer.Enabled = File.Exists(fileSave.FileNameFull);
            mnuOpenWithAssociatedApplication.Enabled = File.Exists(fileSave.FileNameFull);
            mnuOpenContainingFolderInCmd.Enabled = File.Exists(fileSave.FileNameFull);
            mnuOpenContainingFolderInWindowsPowerShell.Enabled = File.Exists(fileSave.FileNameFull);
            mnuOpenWithAssociatedApplication.Enabled = File.Exists(fileSave.FileNameFull);
            mnuRenameNewFile.Enabled = !fileSave.ExistsInFileSystem;
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
                HandleCloseTab((FileSave) sttcMain.Documents[i].Tag, false, false, true,
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
            var fileSave = (FileSave) document.Tag;
            if (fileSave.ExistsInFileSystem)
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
                fileSave.FileName = newName;
                fileSave.FileNameFull = newName;

                // update the document..
                document.FileName = newName;
                document.FileNameNotPath = newName;
                document.FileTabButton.Text = newName;
                sttcMain.LeftFileIndex = sttcMain.LeftFileIndex;

                // update the time stamp..
                fileSave.SetDatabaseModified(DateTime.Now);

                // update document misc data, i.e. the assigned lexer to the database..
                fileSave.AddOrUpdateFile();
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