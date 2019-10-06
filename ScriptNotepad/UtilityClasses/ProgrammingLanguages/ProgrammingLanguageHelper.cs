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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using VPKSoft.LangLib;
using static VPKSoft.ScintillaLexers.LexerEnumerations;

namespace ScriptNotepad.UtilityClasses.ProgrammingLanguages
{
    /// <summary>
    /// A class to help build a programming language selection menu.
    /// </summary>
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class ProgrammingLanguageHelper: IDisposable
    {
        /// <summary>
        /// Creates the programming language selection menu.
        /// </summary>
        /// <param name="languageMenuStrip">The menu strip to create the programming language menu to.</param>
        /// <param name="withFirstChar">A flag indicating whether to categorize the programming languages in to drop-down menu items based on the language name first character.</param>
        public ProgrammingLanguageHelper(ToolStripMenuItem languageMenuStrip, bool withFirstChar)
        {
            menuStrip = languageMenuStrip;

            // dispose of the possible previous menu..
            DisposePreviousMenu();

            menuClickLanguage = delegate(object sender, EventArgs args)
            {
                var lexer = (LexerType) ((ToolStripMenuItem) sender).Tag;
                LanguageMenuClick?.Invoke(sender, new ProgrammingLanguageMenuClickEventArgs() {LexerType = lexer});
            };

            // get the listed programming languages and their starting characters..
            var languages = LexersList
                .Select(f => (f.programmingLanguageName, f.lexerType, f.programmingLanguageName.ToUpperInvariant()[0]))
                .OrderBy(f => f.Item3.ToString()).ThenBy(f => f.programmingLanguageName.ToLowerInvariant());

            // initialize a list of used characters to be used if the withFirstChar parameter is set to true..
            List<(char startChar, ToolStripMenuItem charItem)> usedCharMenus = new List<(char startChar, ToolStripMenuItem charItem)>();

            // loop through the list of languages..
            foreach (var language in languages)
            {
                // if the parameter is set to true..
                if (withFirstChar)
                {
                    // find the starting char category menu item..
                    int idx = usedCharMenus.FindIndex(f => f.startChar == language.Item3);
                    if (idx == -1)
                    {
                        // if not found, then create a new char category menu item..
                        usedCharMenus.Add((language.Item3, new ToolStripMenuItem(language.Item3.ToString())));
                        idx = usedCharMenus.FindIndex(f => f.startChar == language.Item3);

                        // add the character menu to the given tool strip menu item's drop down menu collection..
                        languageMenuStrip.DropDownItems.Add(usedCharMenus[idx].charItem);
                    }


                    // add the language menu to the category menu item's drop down menu collection..
                    usedCharMenus[idx].charItem.DropDownItems
                        .Add(new ToolStripMenuItem(language.programmingLanguageName, null, menuClickLanguage)
                            {Tag = language.lexerType, CheckOnClick = true});
                }
                else
                {
                    // add the language menu to the given tool strip menu item's drop down menu collection..
                    languageMenuStrip.DropDownItems
                        .Add(new ToolStripMenuItem(language.programmingLanguageName, null, menuClickLanguage)
                            {Tag = language.lexerType, CheckOnClick = true});
                }
            }
        }

        /// <summary>
        /// Finds and checks the menu item(s) with the given <see cref="LexerType"/>.
        /// </summary>
        /// <param name="lexerType">Type of the lexer of which corresponding menu item(s) to check.</param>
        public void CheckLanguage(LexerType lexerType)
        {
            ToolStripMenuItem checkMainMenuItem = null;
            foreach (ToolStripMenuItem menuItem in menuStrip.DropDownItems)
            {
                foreach (ToolStripMenuItem subMenuItem in menuItem.DropDownItems)
                {
                    // check or un-check the menu item..
                    if (subMenuItem.Tag != null && subMenuItem.Tag.GetType() == typeof(LexerType))
                    {
                        var menuLexerType = (LexerType) subMenuItem.Tag;

                        subMenuItem.Checked = menuLexerType == lexerType;
                        if (menuLexerType == lexerType)
                        {
                            // this gets unchecked in drop down styled menu, so save it for later..
                            checkMainMenuItem = menuItem;
                        }
                    }
                    else
                    {
                        subMenuItem.Checked = false;                        
                    }
                }

                // check or un-check the menu item..
                if (menuItem.Tag != null && menuItem.Tag.GetType() == typeof(LexerType))
                {
                    var menuLexerType = (LexerType) menuItem.Tag;

                    menuItem.Checked = menuLexerType == lexerType;
                }
                else
                {
                    menuItem.Checked = false;                        
                }
            }

            // if a menu item was assigned to be checked..
            if (checkMainMenuItem != null)
            {
                // ..check it..
                checkMainMenuItem.Checked = true;
            }
        }

        /// <summary>
        /// Disposes of the previous programming language menu.
        /// </summary>
        private void DisposePreviousMenu()
        {
            foreach (ToolStripMenuItem menuItem in menuStrip.DropDownItems)
            {
                foreach (ToolStripMenuItem subMenuItem in menuItem.DropDownItems)
                {
                    // unsubscribe the click event..
                    subMenuItem.Click -= menuClickLanguage;
                }
                menuItem.DropDownItems.Clear();

                // unsubscribe the click event..
                menuItem.Click -= menuClickLanguage;
            }
            menuStrip.DropDownItems.Clear();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            DisposePreviousMenu();
        }

        private readonly ToolStripMenuItem menuStrip;

        /// <summary>
        /// A click handler for the menu programming language items.
        /// </summary>
        private readonly EventHandler menuClickLanguage;

        /// <summary>A delegate for the <see cref="ProgrammingLanguageHelper.LanguageMenuClick"/> event.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ScriptNotepad.UtilityClasses.ProgrammingLanguages.ProgrammingLanguageMenuClickEventArgs"/> instance containing the event data.</param>
        public delegate void OnLanguageMenuClick(object sender, ProgrammingLanguageMenuClickEventArgs e);

        /// <summary>
        /// Occurs when the user clicked a programming language menu item.
        /// </summary>
        public event OnLanguageMenuClick LanguageMenuClick;

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified lexer type.
        /// </summary>
        /// <param name="lexerType">Type of the lexer.</param>
        /// <returns>System.String.</returns>
        public string this[LexerType lexerType]
        {
            // no need to validate anything..
            get => LexersList.FirstOrDefault(f => f.lexerType == lexerType).programmingLanguageName;

            set
            {
                // validate the name for the language..
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                // find the index for the language from the list of lexer types and their names..
                int idx = LexersList.FindIndex(f => f.lexerType == lexerType);
                if (idx != -1) // if an index was found..
                {
                    // ..set the new value..
                    LexersList[idx] = (lexerType, value);
                }
            }
        }

        /// <summary>
        /// A list of localizable lexer names with their corresponding enumeration values.
        /// </summary>
        public List<(LexerType lexerType, string
            programmingLanguageName)> LexersList { get; set; } =
            new List<(LexerType lexerType, string programmingLanguageName)>
            (new[]
            {
                (LexerType.Batch, 
                    DBLangEngine.GetStatMessage("msgProgrammingBatch",
                        "Batch script file|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.Cpp, 
                    DBLangEngine.GetStatMessage("msgProgrammingCpp",
                        "C++ programming language|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.Cs, 
                    DBLangEngine.GetStatMessage("msgProgrammingCs",
                        "C# programming language|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.HTML, 
                    DBLangEngine.GetStatMessage("msgProgrammingHtml",
                        "HTML (Hypertext Markup Language)|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.INI, 
                    DBLangEngine.GetStatMessage("msgProgrammingIni",
                        "INI properties file|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.Nsis,
                    DBLangEngine.GetStatMessage("msgProgrammingNsis",
                        "NSIS (Nullsoft Scriptable Install System)|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.Pascal,
                    DBLangEngine.GetStatMessage("msgProgrammingPascal",
                        "Pascal programming language|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.PHP, 
                    DBLangEngine.GetStatMessage("msgProgrammingPHP",
                        "PHP programming language|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.INI,
                    DBLangEngine.GetStatMessage("msgProgrammingIni2",
                        "Properties file (INI)|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.Python,
                    DBLangEngine.GetStatMessage("msgProgrammingPython",
                        "Python programming language|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.SQL,
                    DBLangEngine.GetStatMessage("msgProgrammingSQL",
                        "SQL (Structured Query Language)|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.Text,
                    DBLangEngine.GetStatMessage("msgProgrammingText",
                        "Plain text document|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.Unknown, 
                    DBLangEngine.GetStatMessage("msgProgrammingUnknown",
                        "Unknown|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.WindowsPowerShell, 
                    DBLangEngine.GetStatMessage("msgProgrammingWindowsPowerShell",
                        "WindowsPowerShell|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.Xml,
                    DBLangEngine.GetStatMessage("msgProgrammingXML",
                        "XML (eXtensible Markup Language)|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.YAML,
                    DBLangEngine.GetStatMessage("msgProgrammingYAML",
                        "YAML (YAML Ain't Markup Language)|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.Text,
                    DBLangEngine.GetStatMessage("msgProgrammingText2",
                        "Text document (plain)|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.Java,
                    DBLangEngine.GetStatMessage("msgProgrammingJava",
                        "Java programming language|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.JavaScript,
                    DBLangEngine.GetStatMessage("msgProgrammingJavaScript",
                        "JavaScript programming language|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.Css,
                    DBLangEngine.GetStatMessage("msgProgrammingCss",
                        "Cascading Style Sheets (CSS)|A programming, markup, setting, script or other file and/or language/text")),
                (LexerType.InnoSetup,
                    DBLangEngine.GetStatMessage("msgProgrammingInnoSetup",
                        "InnoSetup installer script|A programming, markup, setting, script or other file and/or language/text")),
            });
    }
}
