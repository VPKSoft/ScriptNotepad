using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptNotepad.UtilityClasses.SearchAndReplace;
using VPKSoft.ErrorLogger;
using VPKSoft.Utils;
using VPKSoft.Utils.XmlSettingsMisc;
using ScriptNotepad.Database.Entity.Entities;
using VPKSoft.LangLib;
using ScriptNotepad.Database.Entity.Context;
using TabDrawMode = ScintillaNET.TabDrawMode;

namespace ScriptNotepad.Settings
{
    /// <summary>
    /// A class for the application settings.
    /// Implements the <see cref="VPKSoft.Utils.XmlSettings" />
    /// </summary>
    /// <seealso cref="VPKSoft.Utils.XmlSettings" />
    public class Settings: XmlSettings
    {
        /// <summary>
        /// The amount of files to be saved to a document history.
        /// </summary>
        /// <value>The amount of files to be saved to a document history.</value>
        public int HistoryListAmount { get; set; } = 20;

        #region DatabaseState

        /// <summary>
        /// Gets or sets the fancy-named database migration level; for now this is used for the Entity Framework conversion.
        /// </summary>
        /// <value>The migration level.</value>
        [IsSetting] // return an invalid value before the software is ready to branch merge..
        public int DatabaseMigrationLevel { get; set; } = 1;
        #endregion

        #region ColorSettings
        /// <summary>
        /// Gets or sets the color of the current line background style.
        /// </summary>
        /// <value>The color of the current line background style.</value>
        [IsSetting]
        public Color CurrentLineBackground { get; set; } = Color.FromArgb(232, 232, 255);

        /// <summary>
        /// Gets or sets the color of the smart highlight style.
        /// </summary>
        /// <value>The color of the smart highlight style.</value>
        [IsSetting]
        public Color SmartHighlight { get; set; } = Color.FromArgb(0, 255, 0);

        /// <summary>
        /// Gets or sets the color of the mark one style.
        /// </summary>
        /// <value>The color of the mark one style.</value>
        [IsSetting]
        public Color Mark1Color { get; set; } = Color.FromArgb(0, 255, 255);

        /// <summary>
        /// Gets or sets the color of the mark two style.
        /// </summary>
        /// <value>The color of the mark two style.</value>
        [IsSetting]
        public Color Mark2Color { get; set; } = Color.FromArgb(255, 128, 0);

        /// <summary>
        /// Gets or sets the color of the mark three style.
        /// </summary>
        /// <value>The color of the mark three style.</value>
        [IsSetting]
        public Color Mark3Color { get; set; } = Color.FromArgb(255, 255, 0);

        /// <summary>
        /// Gets or sets the color of the mark four style.
        /// </summary>
        /// <value>The color of the mark four style.</value>
        [IsSetting]
        public Color Mark4Color { get; set; } = Color.FromArgb(128, 0, 255);

        /// <summary>
        /// Gets or sets the color of the mark five style.
        /// </summary>
        /// <value>The color of the mark five style.</value>
        [IsSetting]
        public Color Mark5Color { get; set; } = Color.FromArgb(0, 128, 0);

        /// <summary>
        /// Gets or sets the color of the mark used in the search and replace dialog.
        /// </summary>
        [IsSetting]
        public Color MarkSearchReplaceColor { get; set; } = Color.DeepPink;

        /// <summary>
        /// Gets or sets the foreground color of brace highlighting.
        /// </summary>
        [IsSetting]
        public Color BraceHighlightForegroundColor { get; set; } = Color.BlueViolet;

        /// <summary>
        /// Gets or sets the background color of brace highlighting.
        /// </summary>
        [IsSetting]
        public Color BraceHighlightBackgroundColor { get; set; } = Color.LightGray;

        /// <summary>
        /// Gets or sets the foreground color of a bad brace.
        /// </summary>
        [IsSetting]
        public Color BraceBadHighlightForegroundColor { get; set; } = Color.Red;
        #endregion

        #region DeprecatedSettings
        /// <summary>
        /// Gets or sets the default encoding to be used with the files within this software.
        /// </summary>
        [Obsolete("DefaultEncoding is deprecated, the EncodingList property replaces this property.")]
        [IsSetting]
        public Encoding DefaultEncoding { get; set; } = Encoding.UTF8;
        #endregion

        #region MainSettings
        /// <summary>
        /// Gets or sets a value indicating whether the thread locale should be set matching to the localization value.
        /// </summary>
        [IsSetting]
        public bool LocalizeThread { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the software should try to auto-detect the encoding of a file.
        /// </summary>
        [IsSetting]
        public bool AutoDetectEncoding { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the software should try to detect a no-BOM unicode files.
        /// </summary>
        [IsSetting]
        public bool DetectNoBom { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the software should skip trying to detect little-endian Unicode encoding if the <see cref="DetectNoBom"/> is enabled.
        /// </summary>
        [IsSetting]
        public bool SkipUnicodeDetectLe { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the software should skip trying to detect big-endian Unicode encoding if the <see cref="DetectNoBom"/> is enabled.
        /// </summary>
        [IsSetting]
        public bool SkipUnicodeDetectBe { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the software should skip trying to detect little-endian UTF32 encoding if the <see cref="DetectNoBom"/> is enabled.
        /// </summary>
        [IsSetting]
        public bool SkipUtf32Le { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the software should skip trying to detect big-endian UTF32 encoding if the <see cref="DetectNoBom"/> is enabled.
        /// </summary>
        [IsSetting]
        public bool SkipUtf32Be { get; set; }

        // a field to hold the EncodingList property value..
        private string encodingList = string.Empty;

        /// <summary>
        /// Gets or sets an ordered encoding list for the software to try in the order.
        /// </summary>
        [IsSetting]
        public string EncodingList
        {
            // the list type is Encoding1.WebName;FailOnErrorInCaseOfUnicode;BOMInCaseOfUnicode|Encoding2.WebName;FailOnErrorInCaseOfUnicode;BOMInCaseOfUnicode|...
            get
            {
                // if empty; create a list of one item..
                if (encodingList == string.Empty)
                {
// the deprecated property is still in for backwards compatibility - so disable the warning..
#pragma warning disable 618
                    encodingList =
                        new UTF8Encoding().WebName + ';' + true + ';' + true + '|' +
                        new UTF8Encoding().WebName + ';' + true + ';' + false + '|' +
                        (DefaultEncoding.WebName == new UTF8Encoding().WebName
                            ? Encoding.Default.WebName
                            : DefaultEncoding.WebName) + ';' + false + ';' + false + '|';
#pragma warning restore 618
                }

                return encodingList;
            } 
            set => encodingList = value;
        }
        #endregion

        #region EditorSpell
        /// <summary>
        /// Gets or sets a value indicating whether the spell checking is enabled for the <see cref="Scintilla"/> document.
        /// </summary>
        [IsSetting]
        public bool EditorUseSpellChecking { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the spell checking is enabled for the <see cref="Scintilla"/> document when opening a file via the shell context menu.
        /// </summary>
        [IsSetting]
        public bool EditorUseSpellCheckingShellContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the spell checking is enabled for the <see cref="Scintilla"/> document for new files.
        /// </summary>
        [IsSetting]
        public bool EditorUseSpellCheckingNewFiles { get; set; }

        /// <summary>
        /// Gets or sets a value of the Hunspell dictionary file to be used with spell checking for the <see cref="Scintilla"/> document.
        /// </summary>
        [IsSetting]
        public string EditorHunspellDictionaryFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value of the Hunspell affix file to be used with spell checking for the <see cref="Scintilla"/> document.
        /// </summary>
        [IsSetting]
        public string EditorHunspellAffixFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the color of the spell check mark.
        /// </summary>
        [IsSetting]
        public Color EditorSpellCheckColor { get; set; } = Color.Red;

        /// <summary>
        /// Gets or sets the color of the spell check mark.
        /// </summary>
        [IsSetting] 
        public int EditorSpellCheckInactivity { get; set; } = 500; // set the default to 500 milliseconds..

        /// <summary>
        /// Gets or sets a value of the Hunspell dictionary file to be used with spell checking for the <see cref="Scintilla"/> document.
        /// </summary>
        [IsSetting]
        public string EditorHunspellDictionaryPath { get; set; } = DefaultDirectory("Dictionaries");

        /// <summary>
        /// Gets or sets a value indicating whether the spell checker should use a custom dictionary (an external assembly) for the spell checking.
        /// </summary>
        [IsSetting]
        public bool EditorSpellUseCustomDictionary { get; set; }

        /// <summary>
        /// Gets or sets the editor spell custom dictionary (an external assembly) definition file.
        /// </summary>
        [IsSetting]
        public string EditorSpellCustomDictionaryDefinitionFile { get; set; }

        /// <summary>
        /// Gets or sets the editor spell custom dictionary install path.
        /// </summary>
        [IsSetting]
        public string EditorSpellCustomDictionaryInstallPath { get; set; } =
            FormSettings.CreateDefaultCustomDictionaryDirectory();
        #endregion

        #region UrlDetection

        /// <summary>
        /// Gets or sets a value indicating whether to highlight URLs within the <see cref="Scintilla"/> control.
        /// </summary>
        [IsSetting]
        public bool HighlightUrls { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to start an associated program on clicking a highlighted URL.
        /// </summary>
        [IsSetting]
        public bool StartProcessOnUrlClick { get; set; } = true;

        /// <summary>
        /// Gets or sets the color of the URL text.
        /// </summary>
        [IsSetting]
        public Color UrlTextColor { get; set; } = Color.Blue;

        /// <summary>
        /// Gets or sets the color of the URL indicator.
        /// </summary>
        [IsSetting]
        public Color UrlIndicatorColor { get; set; } = Color.Blue;

        /// <summary>
        /// Gets or sets the URL indicator style.
        /// </summary>
        [IsSetting]
        public int UrlIndicatorStyle { get; set; } = (int) IndicatorStyle.Plain;

        /// <summary>
        /// Gets or sets a value indicating whether to use a dwell tool tip on URLs.
        /// </summary>
        [IsSetting]
        public bool UrlUseDwellToolTip { get; set; } = true;

        /// <summary>
        /// Gets or sets the foreground color to be used with the URL tool tips.
        /// </summary>
        [IsSetting]
        public Color UrlDwellToolTipForegroundColor { get; set; } = SystemColors.InfoText;

        /// <summary>
        /// Gets or sets the background color to be used with the URL tool tips.
        /// </summary>
        [IsSetting]
        public Color UrlDwellToolTipBackgroundColor { get; set; } = SystemColors.Info;

        /// <summary>
        /// Gets or sets the foreground color to be used with the URL tool tips.
        /// </summary>
        [IsSetting]
        public int UrlDwellToolTipTime { get; set; } = 400;

        /// <summary>
        /// Gets or sets the URL maximum length before the use of an ellipsis to shorten it.
        /// </summary>
        [IsSetting]
        public int UrlMaxLengthBeforeEllipsis { get; set; } = 60;

        /// <summary>
        /// Gets or sets a value indicating whether to use automatic ellipsis on long URLs.
        /// </summary>
        [IsSetting]
        public bool UrlUseAutoEllipsis { get; set; } = true;
        #endregion

        #region Editor
        /// <summary>
        /// Gets or sets the font for the <see cref="Scintilla"/> control.
        /// </summary>
        // ReSharper disable once StringLiteralTypo
        [IsSetting]
        // ReSharper disable once StringLiteralTypo
        public string EditorFontName { get; set; } = @"Consolas";

        /// <summary>
        /// Gets or sets the tab width for the <see cref="Scintilla"/> control.
        /// </summary>
        [IsSetting]
        public int EditorTabWidth { get; set; } = 4;
        
        /// <summary>
        /// Gets or sets a value indicating whether to use code indentation with the <see cref="Scintilla"/> control.
        /// </summary>
        [IsSetting]
        public bool EditorUseCodeIndentation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the zoom value of the document should be to the database.
        /// </summary>
        [IsSetting]
        public bool EditorSaveZoom { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the zoom value should be individual for all the open <see cref="Scintilla"/> documents.
        /// </summary>
        [IsSetting]
        public bool EditorIndividualZoom { get; set; } = true;

        /// <summary>
        /// Gets or sets the size of the font used in the <see cref="Scintilla"/> control.
        /// </summary>
        [IsSetting]
        public int EditorFontSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets a value indicating whether the editor should use tabs.
        /// </summary>
        [IsSetting]
        public bool EditorUseTabs { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the editor <see cref="Scintilla"/> indent guide is enabled.
        /// </summary>
        [IsSetting]
        public bool EditorIndentGuideOn { get; set; } = true;

        /// <summary>
        /// Gets or sets a value of the tab character symbol type.
        /// </summary>
        [IsSetting]
        public int EditorTabSymbol { get; set; } = (int) TabDrawMode.LongArrow;

        /// <summary>
        /// Gets or sets the size of the editor white space in points.
        /// </summary>
        [IsSetting]
        public int EditorWhiteSpaceSize { get; set; } = 1;

        /// <summary>
        /// Gets or sets a value indicating whether the main window should capture some key combinations to simulate an AltGr+Key press for the active editor <see cref="Scintilla"/>.
        /// </summary>
        [IsSetting]
        public bool SimulateAltGrKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the editor (<see cref="Scintilla"/>) should highlight braces.
        /// </summary>
        [IsSetting]
        public bool HighlightBraces { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the editor (<see cref="Scintilla"/>) should use italic font style when highlighting braces.
        /// </summary>
        [IsSetting]
        public bool HighlightBracesItalic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the editor (<see cref="Scintilla"/>) should use bold font style when highlighting braces.
        /// </summary>
        [IsSetting]
        public bool HighlightBracesBold { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the editor should use RTL (Right-to-left) script with the <see cref="Scintilla"/> controls.
        /// </summary>
        [IsSetting]
        // ReSharper disable once UnusedMember.Global, perhaps in the future..
        public bool EditorUseRtl { get; set; }

        /// <summary>
        /// Gets or sets the index of the type of simulation of an AltGr+Key press for the active editor <see cref="Scintilla"/>.
        /// </summary>
        [IsSetting]
        public int SimulateAltGrKeyIndex { get; set; } = -1;
        #endregion

        #region EditorAdditional        
        /// <summary>
        /// Gets or sets a value indicating whether to use automatic code completion for the C# programming language.
        /// </summary>
        /// <value><c>true</c> to use automatic code completion for the C# programming language; otherwise, <c>false</c>.</value>
        [IsSetting]
        public bool UseCSharpAutoComplete { get; set; }
        #endregion

        #region Styles
        /// <summary>
        /// Gets or sets a value for the Notepad++ theme definition files for the <see cref="Scintilla"/> document.
        /// </summary>
        [IsSetting]
        public string NotepadPlusPlusThemePath { get; set; } = DefaultDirectory("Notepad-plus-plus-themes");

        /// <summary>
        /// Gets or sets a value for the Notepad++ theme definition file name for the <see cref="Scintilla"/> document.
        /// </summary>
        [IsSetting]
        public string NotepadPlusPlusThemeFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether to use a style definition file from the Notepad++ software.
        /// </summary>
        [IsSetting]
        public bool UseNotepadPlusPlusTheme { get; set; }
        #endregion

        #region MiscSettings

        /// <summary>
        /// Gets or sets a value indicating whether search and replace dialog should be transparent.
        /// </summary>
        [IsSetting]
        public int SearchBoxTransparency { get; set; } = 1; // 0 = false, 1 = false when inactive, 2 = always..

        /// <summary>
        /// Gets or sets a value of opacity of the <see cref="FormSearchAndReplace"/> form.
        /// </summary>
        [IsSetting]
        public double SearchBoxOpacity { get; set; } = 0.8;

        /// <summary>
        /// Gets or sets a value indicating whether the default session name has been localized.
        /// </summary>
        [IsSetting]
        public bool DefaultSessionLocalized { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the software should check for updates upon startup.
        /// </summary>
        [IsSetting]
        public bool UpdateAutoCheck { get; set; }

        /// <summary>
        /// Gets or sets the plug-in folder for the software.
        /// </summary>
        [IsSetting]
        public string PluginFolder { get; set; } = FormSettings.CreateDefaultPluginDirectory();

        /// <summary>
        /// Gets or sets a value indicating whether the search three form should be an independent form or a docked control to the main form.
        /// </summary>
        [IsSetting]
        public bool DockSearchTreeForm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether tp categorize the programming language menu with the language name starting character.
        /// </summary>
        [IsSetting]
        public bool CategorizeStartCharacterProgrammingLanguage { get; set; }
        #endregion

        #region DataSettings

        /// <summary>
        /// Gets or sets a value indicating whether save closed file contents to database as history.
        /// </summary>
        [IsSetting]
        public bool SaveFileHistoryContents { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to use file system to cache the contents of the files. Otherwise the database is used.
        /// </summary>
        [IsSetting]
        public bool UseFileSystemCache { get; set; }

        /// <summary>
        /// Gets or sets the save file history contents count.
        /// </summary>
        [IsSetting]
        public int SaveFileHistoryContentsCount { get; set; } = 100;

        /// <summary>
        /// Gets or sets the current session (for the documents).
        /// </summary>
        [IsSetting]
        public string CurrentSession { get; set; } = @"Default";

        /// <summary>
        /// Gets the current session entity.
        /// </summary>
        public FileSession CurrentSessionEntity
        {
            get
            {
                var defaultSessionName = DBLangEngine.GetStatMessage("msgDefaultSessionName",
                    "Default|A name of the default session for the documents");

                try
                {
                    var session =
                        ScriptNotepadDbContext.DbContext.FileSessions.FirstOrDefault(f => f.Id == 1);

                    if (session != null)
                    {
                        if (session.SessionName != defaultSessionName)
                        {
                            session.SessionName = defaultSessionName;
                        }
                    }

                    return session;
                }
                catch
                {
                    return null;
                }
            }

            set => CurrentSession = value.SessionName;
        }
        #endregion

        #region SearchSettings

        /// <summary>
        /// Gets or sets the file's maximum size in megabytes (MB) to include in the file search.
        /// </summary>
        /// <value>The file search maximum size mb.</value>
        [IsSetting]
        public long FileSearchMaxSizeMb { get; set; } = 100;

        /// <summary>
        /// Gets or sets the limit count of the history texts (filters, search texts, replace texts and directories) to be saved and retrieved to the <see cref="FormSearchAndReplace"/> form.
        /// </summary>
        [IsSetting]
        public int FileSearchHistoriesLimit { get; set; } = 25;

        /// <summary>
        /// Gets or sets the value whether to use auto-complete on the search dialog combo boxes.
        /// </summary>
        [IsSetting]
        public bool AutoCompleteEnabled { get; set; } = true;
        #endregion

        #region ProgramSettings

        /// <summary>
        /// Gets or sets a value indicating whether to use auto-save with a specified <see cref="ProgramAutoSaveInterval"/>.
        /// </summary>
        [IsSetting]
        public bool ProgramAutoSave { get; set; } = true;

        /// <summary>
        /// Gets or sets the program's automatic save interval in minutes.
        /// </summary>
        /// <value>The program automatic save interval.</value>
        [IsSetting]
        public int ProgramAutoSaveInterval { get; set; } = 5;

        // the current language (Culture) to be used with the software..
        private static CultureInfo _culture;

        /// <summary>
        /// Gets or sets the name of the culture used with localization.
        /// </summary>
        /// <value>The name of the culture used with localization.</value>
        [IsSetting]
        public string CultureName { get; set; } = "en-US";

        /// <summary>
        /// Gets or sets the current language (Culture) to be used with the software's localization.
        /// </summary>
        public CultureInfo Culture
        {
            get =>
                _culture ?? new CultureInfo(CultureName ?? "en-US");

            set
            {
                _culture = value;
                CultureName = value.Name;
            }
        }
        #endregion

        #region SaveOpenDialogSettings
        /// <summary>
        /// Gets or sets the initial directory for a save as dialog.
        /// </summary>
        [IsSetting]
        public string FileLocationSaveAs { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for a save as HTML dialog.
        /// </summary>
        [IsSetting]
        public string FileLocationSaveAsHtml { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog on the main form.
        /// </summary>
        [IsSetting]
        public string FileLocationOpen { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog on the main form with encoding.
        /// </summary>
        [IsSetting]
        public string FileLocationOpenWithEncoding { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog to open the first file for the diff form.
        /// </summary>
        [IsSetting]
        public string FileLocationOpenDiff1 { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog to open the second file for the diff form.
        /// </summary>
        [IsSetting]
        public string FileLocationOpenDiff2 { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog to install a plugin from the plugin management dialog.
        /// </summary>
        [IsSetting]
        public string FileLocationOpenPlugin { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog to open a Hunspell dictionary file from the settings form.
        /// </summary>
        [IsSetting]
        public string FileLocationOpenDictionary { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog to open a Hunspell affix file from the settings form.
        /// </summary>
        [IsSetting]
        public string FileLocationOpenAffix { get; set; }
        #endregion

        #region DateTimeSettings

        /// <summary>
        /// Gets or sets the date and/or time format 1.
        /// </summary>
        [IsSetting]
        public string DateFormat1 { get; set; } = @"yyyy'/'MM'/'dd"; // default to american..

        /// <summary>
        /// Gets or sets the date and/or time format 2.
        /// </summary>
        [IsSetting]
        public string DateFormat2 { get; set; } = @"dd'.'MM'.'yyyy"; // default to european..

        /// <summary>
        /// Gets or sets the date and/or time format 3.
        /// </summary>
        [IsSetting]
        public string DateFormat3 { get; set; } = @"yyyy'/'MM'/'dd hh'.'mm tt"; // default to american..

        /// <summary>
        /// Gets or sets the date and/or time format 4.
        /// </summary>
        [IsSetting]
        public string DateFormat4 { get; set; } = @"dd'.'MM'.'yyyy HH':'mm':'ss"; // default to european..

        /// <summary>
        /// Gets or sets the date and/or time format 5.
        /// </summary>
        [IsSetting]
        public string DateFormat5 { get; set; } = @"hh'.'mm tt"; // default to american..

        /// <summary>
        /// Gets or sets the date and/or time format 6.
        /// </summary>
        [IsSetting]
        public string DateFormat6 { get; set; } = @"HH':'mm':'ss"; // default to european..

        /// <summary>
        /// Gets or sets a value indicating whether to use invariant culture formatting date and time via the edit menu.
        /// </summary>
        [IsSetting]
        public bool DateFormatUseInvariantCulture { get; set; }
        #endregion

        #region TextSettings
        /// <summary>
        /// Gets or sets a value indicating whether to use case sensitivity with text manipulation.
        /// </summary>
        [IsSetting]
        public bool TextUpperCaseComparison { get; set; }

        /// <summary>
        /// Gets or sets the type of the text comparison to use with text manipulation.
        /// </summary>
        [IsSetting]
        public int TextComparisonType { get; set; } = 0; // 0 = invariant, 1 = current, 2 = ordinal..

        /// <summary>
        /// Gets the text current comparison type <see cref="StringComparison"/>.
        /// </summary>
        public StringComparison TextCurrentComparison
        {
            get
            {
                switch (TextComparisonType)
                {
                    case 0:
                        return TextUpperCaseComparison
                            ? StringComparison.InvariantCulture
                            : StringComparison.InvariantCultureIgnoreCase;

                    case 1:
                        return TextUpperCaseComparison
                            ? StringComparison.CurrentCulture
                            : StringComparison.CurrentCultureIgnoreCase;

                    case 2:
                        return TextUpperCaseComparison
                            ? StringComparison.Ordinal
                            : StringComparison.OrdinalIgnoreCase;
                }

                return TextUpperCaseComparison
                    ? StringComparison.InvariantCulture
                    : StringComparison.InvariantCultureIgnoreCase;
            }
        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Gets the color of the mark.
        /// </summary>
        /// <param name="index">The index of the mark style (0-4).</param>
        /// <returns>A color matching the given marks index.</returns>
        public Color GetMarkColor(int index)
        {
            switch (index)
            {
                case 0: return Mark1Color;
                case 1: return Mark2Color;
                case 2: return Mark3Color;
                case 3: return Mark4Color;
                case 4: return Mark5Color;
                default: return SmartHighlight;
            }
        }

        /// <summary>
        /// Gets the default encoding list for the settings form grid.
        /// </summary>
        public static string DefaultEncodingList =>
            new UTF8Encoding().WebName + ';' + true + ';' + true + '|' +
            new UTF8Encoding().WebName + ';' + true + ';' + false + '|' +
            Encoding.Default.WebName + ';' + false + ';' + false + '|';

        /// <summary>
        /// Gets the <see cref="EncodingList"/> property value as a value tuple.
        /// </summary>
        /// <returns>A value tuple containing the encodings from the <see cref="EncodingList"/> property.</returns>
        public List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)>
            GetEncodingList()
        {
            return GetEncodingList(EncodingList);
        }

        /// <summary>
        /// Gets the given string value as a value tuple containing encodings.
        /// </summary>
        /// <param name="encodingList">A string containing a delimited list of encodings.</param>
        /// <returns>A value tuple containing the encodings from the <paramref name="encodingList"/> value.</returns>
        public static List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)>
            GetEncodingList(string encodingList)
        {
            // create a return value..
            List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)> result =
                new List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)>();

            string[] encodings = encodingList.Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var encoding in encodings)
            {
                var enc = Encoding.GetEncoding(encoding.Split(';')[0]);

                // UTF7..
                if (enc.CodePage == 65000)
                {
                    enc = new UTF7Encoding(bool.Parse(encoding.Split(';')[1]));
                }

                // UTF8..
                if (enc.CodePage == 65001)
                {
                    enc = new UTF8Encoding(bool.Parse(encoding.Split(';')[2]), 
                        bool.Parse(encoding.Split(';')[1]));
                }

                // Unicode, little/big endian..
                if (enc.CodePage == 1200 || enc.CodePage == 1201)
                {
                    enc = new UnicodeEncoding(enc.CodePage == 1201, bool.Parse(encoding.Split(';')[2]),
                        bool.Parse(encoding.Split(';')[1]));
                }

                // UTF32, little/big endian..
                if (enc.CodePage == 12000 || enc.CodePage == 12001)
                {
                    enc = new UTF32Encoding(enc.CodePage == 12001, bool.Parse(encoding.Split(';')[2]),
                        bool.Parse(encoding.Split(';')[1]));
                }

                // add the encoding to the return value..
                result.Add((encodingName: enc.EncodingName, encoding: enc,
                    unicodeFailOnInvalidChar: bool.Parse(encoding.Split(';')[1]),
                    unicodeBOM: bool.Parse(encoding.Split(';')[2])));
            }

            return result;
        }

        /// <summary>
        /// Generates a list of value tuples containing encoding data from a given <see cref="System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns>A list of value tuples containing the encoding information.</returns>
        public List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)>
            EncodingsFromDataGrid(DataGridView dataGridView)
        {
            // create a return value..
            List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)> result =
                new List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                result.Add((EncodingsFromObjects(row.Cells[0].Value, ((Encoding) row.Cells[0].Value).WebName,
                    row.Cells[3].Value, row.Cells[2].Value)));
            }

            return result;
        }

        /// <summary>
        /// Gets an encoding data from a given parameters to be used for the <see cref="FormSettings"/> form.
        /// </summary>
        /// <param name="objects">An array of objects to cast into a value tuple.</param>
        /// <returns>A value tuple containing the encoding information.</returns>
        public (string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)
            EncodingsFromObjects(params object[] objects)
        {
            // create a return value..
            (string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM) result = (
                (string) objects[1], (Encoding) objects[0], (bool) objects[2],
                (bool) objects[3]);

            return result;
        }

        /// <summary>
        /// Gets an encoding list formatted as a string from a given value tuple list.
        /// </summary>
        /// <param name="encodings">A value tuple list containing the data for the encodings.</param>
        /// <returns>A formatted string suitable to be assigned to the <see cref="EncodingList"/> property value.</returns>
        public string EncodingStringFromDefinitionList(
            List<(string encodingName, Encoding encoding, bool unicodeFailOnInvalidChar, bool unicodeBOM)> encodings)
        {
            // the list type is Encoding1.WebName;FailOnErrorInCaseOfUnicode;BOMInCaseOfUnicode|Encoding2.WebName;FailOnErrorInCaseOfUnicode;BOMInCaseOfUnicode|...
            string result = string.Empty;
            foreach (var encoding in encodings)
            {
                result +=
                    encoding.encodingName + ';' + encoding.unicodeFailOnInvalidChar + ';' + encoding.unicodeBOM + '|';
            }

            return result;
        }

        /// <summary>
        /// Creates a directory to the local application data folder for the software.
        /// </summary>
        public static string DefaultDirectory(string defaultDirectory)
        {
            if (defaultDirectory == string.Empty)
            {
                return Paths.GetAppSettingsFolder(Misc.AppType.Winforms);
            }

            // create a folder for plug-ins if it doesn't exist already.. 
            if (!Directory.Exists(Path.Combine(Paths.GetAppSettingsFolder(Misc.AppType.Winforms), defaultDirectory)))
            {
                try
                {
                    // create the folder..
                    Directory.CreateDirectory(Path.Combine(Paths.GetAppSettingsFolder(Misc.AppType.Winforms),
                        defaultDirectory));
                }
                catch (Exception ex) // a failure so do log it..
                {
                    ExceptionLogger.LogError(ex);
                    return string.Empty;
                }
            }

            return Path.Combine(Paths.GetAppSettingsFolder(Misc.AppType.Winforms), defaultDirectory);
        }
        #endregion
    }
}
