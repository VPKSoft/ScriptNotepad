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
using System.IO;
using ScintillaNET;
using ScriptNotepad.Settings;
using VPKSoft.LangLib;
using VPKSoft.ScintillaSpellCheck;
using VPKSoft.ScintillaTabbedTextControl;
using VPKSoft.SearchText;

namespace ScriptNotepad.UtilityClasses.SpellCheck
{
    /// <summary>
    /// A helper class for the <see cref="ScintillaTabbedDocument"/> for spell checking.
    /// </summary>
    public class TabbedDocumentSpellCheck: IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabbedDocumentSpellCheck"/> class.
        /// </summary>
        /// <param name="document">The <see cref="ScintillaTabbedDocument"/> to attach the spell checker.</param>
        public TabbedDocumentSpellCheck(ScintillaTabbedDocument document)
        {
            // verify the settings and the fact that the document doesn't already have this instance..
            if (FormSettings.Settings.EditorUseSpellChecking &&
                File.Exists(FormSettings.Settings.EditorHunspellDictionaryFile) &&
                File.Exists(FormSettings.Settings.EditorHunspellAffixFile) &&
                document.Tag0 == null)
            {
                SpellCheck = new ScintillaSpellCheck(document.Scintilla,
                    FormSettings.Settings.EditorHunspellDictionaryFile,
                    FormSettings.Settings.EditorHunspellAffixFile, 
                    UserDictionaryFile, 
                    UserIgnoreWordFile)
                {
                    MenuIgnoreText = DBLangEngine.GetStatMessage("msgSpellCheckIgnoreWordMenuText",
                        "Ignore word \"{0}\".|A context menu item for spell checking to ignore a word"),
                    MenuAddToDictionaryText = DBLangEngine.GetStatMessage("msgSpellCheckAddWordToDictionaryText",
                        "Add word \"{0}\" to the dictionary.|A context menu item for spell checking to add a word to the dictionary"),
                    MenuDictionaryTopItemText =  DBLangEngine.GetStatMessage("msgSpellChecking",
                    "Spell checking|A message displayed in a spelling correct menu's top item."),
                    ShowDictionaryTopMenuItem = true,
                    AddBottomSeparator = true,
                    ShowIgnoreMenu = true,
                    ShowAddToDictionaryMenu = true,
                    ScintillaIndicatorColor = FormSettings.Settings.EditorSpellCheckColor,
                };

                // add this instance to the document's Tag0 property..
                document.Tag0 = this;

                // subscribe to the event where a user wishes to correct a
                // misspelled word via the context menu..
                SpellCheck.UserWordReplace += SpellCheck_UserWordReplace;

                // subscribe to the Scintilla text changed event..
                document.Scintilla.TextChanged += Scintilla_TextChanged;

                // subscribe the event when a user is requesting a word to be added to the personal dictionary..
                SpellCheck.WordAddDictionaryRequested += SpellCheck_WordAddDictionaryOrIgnoreRequested;

                // subscribe to the event when a user is requesting to add a word to personal ignore list..
                SpellCheck.WordIgnoreRequested += SpellCheck_WordAddDictionaryOrIgnoreRequested;

                // save the Scintilla instance to unsubscribe the events..
                Scintilla = document.Scintilla;
                
                // spell check the document for the first time..
                SpellCheck?.SpellCheckScintillaFast();

                // save the time of the latest spell check..
                LastSpellCheck = DateTime.Now;
            }
        }

        private void SpellCheck_WordAddDictionaryOrIgnoreRequested(object sender, WordHandleEventArgs e)
        {
            // check if the word was requested to be added to the dictionary..
            if (e.AddToDictionary)
            {                
                e.ScintillaSpellCheck.AddToUserDictionary(e.Word);
                SpellCheckEnabled = true; // indicate to force a spell check..
                DoSpellCheck();
            }
            // check if the word was requested to be added to the ignore word list..
            else if (e.AddToIgnore)
            {
                e.ScintillaSpellCheck.AddToUserIgnoreList(e.Word);
                SpellCheckEnabled = true; // indicate to force a spell check..
                DoSpellCheck();
            }
        }

        /// <summary>
        /// Gets or sets the user dictionary file.
        /// </summary>
        public static string UserDictionaryFile { get; set; } =
            Path.Combine(VPKSoft.Utils.Paths.GetAppSettingsFolder(), "user_dictionary.dic");

        /// <summary>
        /// Gets or sets the user ignore word file.
        /// </summary>
        public static string UserIgnoreWordFile { get; set; } =
            Path.Combine(VPKSoft.Utils.Paths.GetAppSettingsFolder(), "user_dictionary.ignore");

        /// <summary>
        /// Gets or set the <see cref="Scintilla"/> instance this class has event subscription for.
        /// </summary>
        private Scintilla Scintilla { get; set; }

        /// <summary>
        /// Runs a spell check for the <see cref="Scintilla"/> document.
        /// </summary>
        public void DoSpellCheck()
        {
            // prevent the spell checking to take place if it's
            // explicitly disabled or there is no need no redo the
            // spell checking..
            if (!ShouldSpellCheck || !Enabled)
            {
                return;
            }

            // spell check the document..
            SpellCheck?.SpellCheckScintillaFast();

            // reset the value of the flag..
            SpellCheckEnabled = false;

            // reset the time of the latest spell check..
            LastSpellCheck = DateTime.Now;
        }

        /// <summary>
        /// Handles the TextChanged event of the Scintilla control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Scintilla_TextChanged(object sender, EventArgs e)
        {
            // reset the time of the latest spell check..
            LastSpellCheck = DateTime.Now;

            if (TextChangedViaSpellCheck)
            {
                return;
            }

            // reset the value of the flag..
            SpellCheckEnabled = true;
        }

        /// <summary>
        /// Handles the UserWordReplace event of the SpellCheck control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WordHandleEventArgs"/> instance containing the event data.</param>
        private void SpellCheck_UserWordReplace(object sender, WordHandleEventArgs e)
        {
            // if the user changed the text of a Scintilla via the spell check context
            // menu to correct a word, then there is no reason to spell check the document again after the text has been changed..
            TextChangedViaSpellCheck = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // unsubscribe to the event where a user wishes to correct a
            // misspelled word via the context menu..
            SpellCheck.UserWordReplace -= SpellCheck_UserWordReplace;

            // unsubscribe to the Scintilla text changed event..
            Scintilla.TextChanged -= Scintilla_TextChanged;

            SpellCheck.WordAddDictionaryRequested -= SpellCheck_WordAddDictionaryOrIgnoreRequested;

            SpellCheck.WordIgnoreRequested -= SpellCheck_WordAddDictionaryOrIgnoreRequested;

            // save the user's dictionary to a file..
            SpellCheck.SaveUserDictionaryToFile(UserDictionaryFile);

            // save the user's ignore word list to a file..
            SpellCheck.SaveUserWordIgnoreListToFile(UserIgnoreWordFile);

            // dispose of the ScintillaSpellCheck class..
            using (SpellCheck)
            {
                SpellCheck = null; // empty using clause is ugly :-(
            }
        }

        private int textChangedViaSpellCheck = -1;

        /// <summary>
        /// Gets or sets a value indicating if the text was changed via spelling correction.
        /// </summary>
        private bool TextChangedViaSpellCheck
        {
            get
            {
                // this value returns true for a two times if set to true..
                var result = textChangedViaSpellCheck >= 0;

                // decrease the value..
                textChangedViaSpellCheck--; 

                return result;
            }
            set => textChangedViaSpellCheck = value ? 2 : 0;
        }

        // a field for the Enabled property..
        private bool enabled = true;

        /// <summary>
        /// Gets or set a value whether the spell checking is enabled.
        /// </summary>
        public bool Enabled
        {
            get => enabled;

            set
            {
                if (value != enabled)
                {
                    SpellCheckEnabled = value;
                    enabled = value;
                    if (!value)
                    {
                        SpellCheck.ClearSpellCheck();
                    }
                }
            }
        }

        /// <summary>
        /// A flag indicating whether the text was changed via a spelling correction or another way.
        /// </summary>
        private bool SpellCheckEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the last time to document was spell checked for.
        /// </summary>
        public DateTime LastSpellCheck = DateTime.Now;

        /// <summary>
        /// Gets a value whether a spell check should be done.
        /// </summary>
        public bool ShouldSpellCheck =>
            (DateTime.Now - LastSpellCheck).TotalMilliseconds > FormSettings.Settings.EditorSpellCheckInactivity &&
            SpellCheckEnabled && !TextChangedViaSpellCheck;

        /// <summary>
        /// Gets or sets the <see cref="VPKSoft.ScintillaSpellCheck.ScintillaSpellCheck"/> class instance.
        /// </summary>
        /// <value>The spell check.</value>
        public ScintillaSpellCheck SpellCheck { get; set; }
    }
}
