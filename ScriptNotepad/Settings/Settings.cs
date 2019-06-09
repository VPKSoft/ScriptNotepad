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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using PropertyChanged; // (C): https://github.com/Fody/PropertyChanged, MIT license
using System.Reflection;
using VPKSoft.ConfLib;
using VPKSoft.ErrorLogger;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ScintillaNET;
using ScriptNotepad.UtilityClasses.SearchAndReplace;
using TabDrawMode = ScintillaNET.TabDrawMode;

namespace ScriptNotepad.Settings
{
    /// <summary>
    /// An attribute class for describing a setting name and it's type (VPKSoft.ConfLib).
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)] // target a property only..
    public class SettingAttribute: Attribute
    {
        /// <summary>
        /// Gets or sets the name of the setting (VPKSoft.ConfLib).
        /// </summary>
        public string SettingName { get; set; }

        /// <summary>
        /// Gets or sets the type of the setting (VPKSoft.ConfLib).
        /// </summary>
        public Type SettingType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingAttribute"/> class.
        /// </summary>
        /// <param name="settingName">Name of the setting (VPKSoft.ConfLib).</param>
        /// <param name="type">The type of the setting (VPKSoft.ConfLib).</param>
        public SettingAttribute(string settingName, Type type)
        {
            SettingName = settingName; // save the given values..
            SettingType = type;
        }
    }

    /// <summary>
    /// Settings for the ScriptNotepad software.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class Settings : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            if (conflib == null) // don't initialize if already initialized..
            {
                conflib = new Conflib
                {
                    AutoCreateSettings = true // set it to auto-create SQLite database tables..
                }; // create a new instance of the Conflib class..
            }

            #region SpecialSetting
            PropertyInfo propertyInfo = // first get the property info for the property..
                GetType().GetProperty("DefaultEncoding", BindingFlags.Instance | BindingFlags.Public);

            // get the setting attribute value of the property.. 
            if (propertyInfo != null)
            {
                SettingAttribute settingAttribute = (SettingAttribute)propertyInfo.GetCustomAttribute(typeof(SettingAttribute));

                // set the default encoding value..
                DefaultEncoding = Encoding.GetEncoding(conflib[settingAttribute.SettingName, DefaultEncoding.WebName]);
                #endregion

                // get all public instance properties of this class..
                PropertyInfo[] propertyInfos = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                // loop through the properties..
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    // a special property to which the Convert class can't be used..
                    if (propertyInfos[i].Name == "DefaultEncoding")
                    {
                        continue; // ..so do continue..
                    }

                    // a CultureInfo instance, which is not an auto-property..
                    if (propertyInfos[i].Name == "Culture")
                    {
                        continue; // ..so do continue..
                    }


                    try // avoid crashes..
                    {
                        // get the SettingAttribute class instance of the property..
                        settingAttribute = (SettingAttribute)propertyInfos[i].GetCustomAttribute(typeof(SettingAttribute));

                        // get the default value for the property..
                        object currentValue = propertyInfos[i].GetValue(this);

                        // set the value for the property using the default value as a
                        // fall-back value..

                        if (settingAttribute.SettingType == typeof(Color))
                        {
                            propertyInfos[i].SetValue(this, ColorTranslator.FromHtml(
                                conflib[settingAttribute.SettingName, ColorTranslator.ToHtml((Color)currentValue)]));
                        }
                        else
                        {
                            if (currentValue == null)
                            {
                                continue;
                            }
                            propertyInfos[i].SetValue(this, Convert.ChangeType(conflib[settingAttribute.SettingName, currentValue.ToString()], settingAttribute.SettingType));                        
                        }
                    }
                    catch (Exception ex)
                    {
                        // log the exception..
                        ExceptionLogger.LogError(ex);
                    }
                }
            }

            // subscribe the event handler..
            PropertyChanged += Settings_PropertyChanged;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Settings class instance.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // NOTE:: Do use this attribute, if no notification is required from a property: [DoNotNotify]

            try // just try from the beginning..
            {
                PropertyInfo propertyInfo = // first get the property info for the property..
                GetType().GetProperty(e.PropertyName, BindingFlags.Instance | BindingFlags.Public);

                // get the property value..
                object value = propertyInfo?.GetValue(this);

                // get the setting attribute value of the property.. 
                if (propertyInfo != null)
                {
                    SettingAttribute settingAttribute = (SettingAttribute)propertyInfo.GetCustomAttribute(typeof(SettingAttribute));

                    if (value != null && settingAttribute != null)
                    {
                        // this is a special case, otherwise try just to use simple types..
                        if (settingAttribute.SettingType == typeof(Encoding))
                        {
                            Encoding encoding = (Encoding)value;
                            conflib[settingAttribute.SettingName] = encoding.WebName;
                        }
                        else if (settingAttribute.SettingType == typeof(Color))
                        {
                            conflib[settingAttribute.SettingName] = ColorTranslator.ToHtml((Color) value);
                        }
                        else // a simple type..
                        {
                            conflib[settingAttribute.SettingName] = value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);
            }
        }

        /// <summary>
        /// An instance to a Conflib class.
        /// </summary>
        private readonly Conflib conflib;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
#pragma warning disable CS0067 // disable the CS0067 as the PropertyChanged event is raised via the PropertyChanged.Fody class library..
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067

        #region Settings
        // NOTE::
        // These properties must have a default value for them to work properly with the class logic!

        /// <summary>
        /// Gets or sets the default encoding to be used with the files within this software.
        /// </summary>
        [Setting("main/encoding", typeof(Encoding))]
        public Encoding DefaultEncoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// Gets or sets a value indicating whether the software should try to auto-detect the encoding of a file.
        /// </summary>
        [Setting("main/autoDetectEncoding", typeof(bool))]
        public bool AutoDetectEncoding { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the software should try to detect a no-BOM unicode files.
        /// </summary>
        [Setting("main/detectNoBom", typeof(bool))]
        public bool DetectNoBom { get; set; } = false;

        /// <summary>
        /// The amount of files to be saved to a document history.
        /// </summary>
        [Setting("gui/history", typeof(int))]
        public int HistoryListAmount { get; set; } = 20;

        /// <summary>
        /// Gets or sets the color of the current line background style.
        /// </summary>
        /// <value>The color of the current line background style.</value>
        [Setting("color/currentLineBackground", typeof(Color))]
        public Color CurrentLineBackground { get; set; } = Color.FromArgb(232, 232, 255);

        /// <summary>
        /// Gets or sets the color of the smart highlight style.
        /// </summary>
        /// <value>The color of the smart highlight style.</value>
        [Setting("color/smartHighLight", typeof(Color))]
        public Color SmartHighlight { get; set; } = Color.FromArgb(0, 255, 0);

        /// <summary>
        /// Gets or sets the color of the mark one style.
        /// </summary>
        /// <value>The color of the mark one style.</value>
        [Setting("color/mark1", typeof(Color))]
        public Color Mark1Color { get; set; } = Color.FromArgb(0, 255, 255);

        /// <summary>
        /// Gets or sets the color of the mark two style.
        /// </summary>
        /// <value>The color of the mark two style.</value>
        [Setting("color/mark2", typeof(Color))]
        public Color Mark2Color { get; set; } = Color.FromArgb(255, 128, 0);

        /// <summary>
        /// Gets or sets the color of the mark three style.
        /// </summary>
        /// <value>The color of the mark three style.</value>
        [Setting("color/mark3", typeof(Color))]
        public Color Mark3Color { get; set; } = Color.FromArgb(255, 255, 0);

        /// <summary>
        /// Gets or sets the color of the mark four style.
        /// </summary>
        /// <value>The color of the mark four style.</value>
        [Setting("color/mark4", typeof(Color))]
        public Color Mark4Color { get; set; } = Color.FromArgb(128, 0, 255);

        /// <summary>
        /// Gets or sets the color of the mark five style.
        /// </summary>
        /// <value>The color of the mark five style.</value>
        [Setting("color/mark5", typeof(Color))]
        public Color Mark5Color { get; set; } = Color.FromArgb(0, 128, 0);

        /// <summary>
        /// Gets or sets the color of the mark used in the search and replace dialog.
        /// </summary>
        [Setting("color/markSearchReplace", typeof(Color))]
        public Color MarkSearchReplaceColor { get; set; } = Color.DeepPink;


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
        /// Gets or sets a value indicating whether the editor should use tabs.
        /// </summary>
        [Setting("editor/useTabs", typeof(bool))]
        public bool EditorUseTabs { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the editor <see cref="Scintilla"/> indent guide is enabled.
        /// </summary>
        [Setting("editor/indentGuideOn", typeof(bool))]
        public bool EditorIndentGuideOn { get; set; } = true;

        /// <summary>
        /// Gets or sets a value of the tab character symbol type.
        /// </summary>
        [Setting("editor/tabSymbol", typeof(int))]
        public int EditorTabSymbol { get; set; } = (int)TabDrawMode.LongArrow;

        /// <summary>
        /// Gets or sets the size of the editor white space in points.
        /// </summary>
        [Setting("editor/whiteSpaceSize", typeof(int))]
        public int EditorWhiteSpaceSize { get; set; } = 1;

        /// <summary>
        /// Gets or sets a value indicating whether the main window should capture some key combinations to simulate an AltGr+Key press for the active editor <see cref="Scintilla"/>.
        /// </summary>
        [Setting("editor/simulateAltGrKey", typeof(bool))]
        public bool SimulateAltGrKey { get; set; } = false;

        /// <summary>
        /// Gets or sets the index of the type of simulation of an AltGr+Key press for the active editor <see cref="Scintilla"/>.
        /// </summary>
        /// <value>The index of the simulate alt gr key.</value>
        [Setting("editor/simulateAltGrKeyIndex", typeof(int))]
        public int SimulateAltGrKeyIndex { get; set; } = -1;

        /// <summary>
        /// Gets or sets a value indicating whether the spell checking is enabled for the <see cref="Scintilla"/> document.
        /// </summary>
        [Setting("editorSpell/useSpellChecking", typeof(bool))]
        public bool EditorUseSpellChecking { get; set; } = false;

        /// <summary>
        /// Gets or sets a value of the Hunspell dictionary file to be used with spell checking for the <see cref="Scintilla"/> document.
        /// </summary>
        [Setting("editorSpell/dictionaryFile", typeof(string))]
        public string EditorHunspellDictionaryFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value of the Hunspell affix file to be used with spell checking for the <see cref="Scintilla"/> document.
        /// </summary>
        [Setting("editorSpell/affixFile", typeof(string))]
        public string EditorHunspellAffixFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the color of the spell check mark.
        /// </summary>
        [Setting("editorSpell/markColor", typeof(Color))]
        public Color EditorSpellCheckColor { get; set; } = Color.Red;

        /// <summary>
        /// Gets or sets the color of the spell check mark.
        /// </summary>
        [Setting("editorSpell/SpellRecheckAfterInactivity", typeof(int))]
        public int EditorSpellCheckInactivity { get; set; } = 500; // set the default to 500 milliseconds..

        /// <summary>
        /// Gets or sets the font for the <see cref="Scintilla"/> control.
        /// </summary>
        [Setting("editor/fontName", typeof(string))]
        public string EditorFontName { get; set; } = "Consolas";

        /// <summary>
        /// Gets or sets the tab width for the <see cref="Scintilla"/> control.
        /// </summary>
        [Setting("editor/tabWidth", typeof(int))]
        public int EditorTabWidth { get; set; } = 4;

        /// <summary>
        /// Gets or sets a value indicating whether to use code indentation with the <see cref="Scintilla"/> control.
        /// </summary>
        [Setting("editor/useCodeIndentation", typeof(bool))]
        public bool EditorUseCodeIndentation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the zoom value of the document should be to the database.
        /// </summary>
        [Setting("editor/saveZoom", typeof(bool))]
        public bool EditorSaveZoom { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the zoom value should be individual for all the open <see cref="Scintilla"/> documents.
        /// </summary>
        [Setting("editor/individualZoom", typeof(bool))]
        public bool EditorIndividualZoom { get; set; } = true;

        /// <summary>
        /// Gets or sets a value of the Hunspell dictionary file to be used with spell checking for the <see cref="Scintilla"/> document.
        /// </summary>
        [Setting("editorSpell/dictionaryPath", typeof(string))]
        public string EditorHunspellDictionaryPath { get; set; } = DefaultDirectory("Dictionaries");

        /// <summary>
        /// Gets or sets a value for the Notepad++ theme definition files for the <see cref="Scintilla"/> document.
        /// </summary>
        [Setting("style/notepadPlusPlusThemePath", typeof(string))]
        public string NotepadPlusPlusThemePath { get; set; } = DefaultDirectory("Notepad-plus-plus-themes");

        /// <summary>
        /// Gets or sets a value for the Notepad++ theme definition file name for the <see cref="Scintilla"/> document.
        /// </summary>
        [Setting("style/notepadPlusPlusThemeFile", typeof(string))]
        public string NotepadPlusPlusThemeFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether to use a style definition file from the Notepad++ software.
        /// </summary>
        [Setting("style/useNotepadPlusPlusTheme", typeof(bool))]
        public bool UseNotepadPlusPlusTheme { get; set; } = false;

        /// <summary>
        /// Gets or sets the size of the font used in the <see cref="Scintilla"/> control.
        /// </summary>
        [Setting("editor/fontSize", typeof(int))]
        public int EditorFontSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets a value indicating whether save closed file contents to database as history.
        /// </summary>
        [Setting("database/historyContents", typeof(bool))]
        public bool SaveFileHistoryContents { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether search and replace dialog should be transparent.
        /// </summary>
        [Setting("misc/searchBoxTransparency", typeof(int))]
        public int SearchBoxTransparency { get; set; } = 1; // 0 = false, 1 = false when inactive, 2 = always..

        /// <summary>
        /// Gets or sets the file's maximum size in megabytes (MB) to include in the file search.
        /// </summary>
        /// <value>The file search maximum size mb.</value>
        [Setting("search/fileSysFileMaxSizeMB", typeof(long))]
        public long FileSearchMaxSizeMb { get; set; } = 100;

        /// <summary>
        /// Gets or sets the limit count of the history texts (filters, search texts, replace texts and directories) to be saved and retrieved to the <see cref="FormSearchAndReplace"/> form.
        /// </summary>
        [Setting("search/commonHistoryLimit", typeof(int))]
        public int FileSearchHistoriesLimit { get; set; } = 25;

        /// <summary>
        /// Gets or sets a value of opacity of the <see cref="FormSearchAndReplace"/> form.
        /// </summary>
        [Setting("misc/searchBoxOpacity", typeof(double))]
        public double SearchBoxOpacity { get; set; } = 0.8;

        /// <summary>
        /// Gets or sets the save file history contents count.
        /// </summary>
        [Setting("database/historyContentsCount", typeof(int))]
        public int SaveFileHistoryContentsCount { get; set; } = 100;

        /// <summary>
        /// Gets or sets the current session (for the documents).
        /// </summary>
        [Setting("database/currentSession", typeof(string))]
        public string CurrentSession { get; set; } = "Default";

        /// <summary>
        /// Gets or sets a value indicating whether the default session name has been localized.
        /// </summary>
        [Setting("misc/currentSessionLocalized", typeof(bool))]
        public bool DefaultSessionLocalized { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to use auto-save with a specified <see cref="ProgramAutoSaveInterval"/>.
        /// </summary>
        [Setting("program/autoSave", typeof(bool))]
        public bool ProgramAutoSave { get; set; } = true;

        /// <summary>
        /// Gets or sets the program's automatic save interval in minutes.
        /// </summary>
        /// <value>The program automatic save interval.</value>
        [Setting("program/autoSaveInterval", typeof(int))]
        public int ProgramAutoSaveInterval { get; set; } = 5;

        /// <summary>
        /// Gets or sets a value indicating whether tp categorize the programming language menu with the language name starting character.
        /// </summary>
        [Setting("misc/categorizeStartCharacterProgrammingLanguage", typeof(bool))]
        public bool CategorizeStartCharacterProgrammingLanguage { get; set; } = true;

        /// <summary>
        /// Gets or sets the plug-in folder for the software.
        /// </summary>
        [Setting("misc/pluginFolder", typeof(string))]
        public string PluginFolder { get; set; } = FormSettings.CreateDefaultPluginDirectory();

        /// <summary>
        /// Gets or sets a value indicating whether the search three form should be an independent form or a docked control to the main form.
        /// </summary>
        [Setting("misc/dockSearchTree", typeof(bool))]
        public bool DockSearchTreeForm { get; set; } = true;

        // the current language (Culture) to be used with the software..
        // ReSharper disable once InconsistentNaming
        private static CultureInfo culture;

        /// <summary>
        /// Gets or sets the current language (Culture) to be used with the software's localization.
        /// </summary>
        [DoNotNotify]
        public CultureInfo Culture
        {
            get =>
                culture ?? new CultureInfo(conflib["language/culture", "en-US"]);

            set
            {
                culture = value;
                conflib["language/culture"] = culture.Name;
            }
        }

        /// <summary>
        /// Gets or sets the initial directory for a save as dialog.
        /// </summary>
        [Setting("fileDialog/locationSaveAs", typeof(string))]
        public string FileLocationSaveAs { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog on the main form.
        /// </summary>
        [Setting("fileDialog/locationOpen", typeof(string))]
        public string FileLocationOpen { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog on the main form with encoding.
        /// </summary>
        [Setting("fileDialog/locationOpenWithEncoding", typeof(string))]
        public string FileLocationOpenWithEncoding { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog to open the first file for the diff form.
        /// </summary>
        [Setting("fileDialog/locationOpenDiff1", typeof(string))]
        public string FileLocationOpenDiff1 { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog to open the second file for the diff form.
        /// </summary>
        [Setting("fileDialog/locationOpenDiff2", typeof(string))]
        public string FileLocationOpenDiff2 { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog to install a plugin from the plugin management dialog.
        /// </summary>
        [Setting("fileDialog/locationOpenPlugin", typeof(string))]
        public string FileLocationOpenPlugin { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog to open a Hunspell dictionary file from the settings form.
        /// </summary>
        [Setting("fileDialog/locationOpenDictionary", typeof(string))]
        public string FileLocationOpenDictionary { get; set; }

        /// <summary>
        /// Gets or sets the initial directory for an open file dialog to open a Hunspell affix file from the settings form.
        /// </summary>
        [Setting("fileDialog/locationOpenAffix", typeof(string))]
        public string FileLocationOpenAffix { get; set; }
        #endregion

        /// <summary>
        /// Creates a directory to the local application data folder for the software.
        /// </summary>
        public static string DefaultDirectory(string defaultDirectory)
        {
            if (defaultDirectory == string.Empty)
            {
                return VPKSoft.Utils.Paths.GetAppSettingsFolder();
            }

            // create a folder for plug-ins if it doesn't exist already.. 
            if (!Directory.Exists(Path.Combine(VPKSoft.Utils.Paths.GetAppSettingsFolder(), defaultDirectory)))
            {
                try
                {
                    // create the folder..
                    Directory.CreateDirectory(Path.Combine(VPKSoft.Utils.Paths.GetAppSettingsFolder(), defaultDirectory));
                }
                catch (Exception ex) // a failure so do log it..
                {
                    ExceptionLogger.LogError(ex);
                    return string.Empty;
                }
            }
            return Path.Combine(VPKSoft.Utils.Paths.GetAppSettingsFolder(), defaultDirectory);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // unsubscribe the event handler..
            PropertyChanged -= Settings_PropertyChanged;

            // close the conflib class instance..
            conflib?.Close();
        }
    }
}
