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

using ScintillaNET;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    /// <summary>
    /// A helper class for searching opened <see cref="Scintilla"/> documents.
    /// </summary>
    /// <seealso cref="SearchOpenDocumentsBase"/>
    public class SearchOpenDocuments: SearchOpenDocumentsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchOpenDocuments"/> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla"/> document to search from.</param>
        /// <param name="openDocuments">A list of all the open documents in the software.</param>
        /// <param name="isRegExp">If set to <c>true</c> regular expressions are used with the search.</param>
        /// <param name="matchCase">if set to <c>true</c> the search is case-sensitive.</param>
        /// <param name="matchWholeWord">If set to <c>true</c> the search will only match a whole word.</param>
        /// <param name="matchWordStart">If set to <c>true</c> the search will match with a word start.</param>
        /// <param name="resetTarget">If set to <c>true</c> the search area of the document is reset to the whole document.</param>
        /// <param name="wrapAround">If set to <c>true</c> the search can continue from the beginning of the document after reaching the end of the document or from the end of the document after reaching the start of the document.</param>
        /// <param name="extended">If set to <c>true</c> the search translates escape characters into their corresponding character values.</param>
        /// <param name="searchText">The text to search from the <see cref="Scintilla"/> document.</param>
        public SearchOpenDocuments(Scintilla scintilla, List<Scintilla> openDocuments, bool isRegExp, bool
            matchCase, bool matchWholeWord, bool matchWordStart, bool resetTarget, bool wrapAround, bool extended, string searchText)
        {
            // save the Scintilla instance..
            Scintilla = scintilla;

            // save the instances of all the open documents in the software..
            OpenDocuments = openDocuments;

            // save the flag whether to continue the search from either the beginning or from the end
            // depending of the search direction..
            WrapAround = wrapAround;

            // apply some additional checks for the resetTarget parameter..
            resetTarget |= scintilla.TargetStart == scintilla.TargetEnd;

            // if the flag is set, reset the Scintilla's search area..
            if (resetTarget)
            {
                scintilla.TargetStart = 0;
                scintilla.TargetEnd = scintilla.TextLength;
            }
            else // otherwise let it be as is..
            {
                // set the value of the previous search direction to indicate a reset on the first search time..
                previousSearchDirection = PreviousDirection.ResetOnDirection;

                // set the search area assuming the search is done forwards..
                scintilla.TargetStart = scintilla.CurrentPosition;
                scintilla.TargetEnd = scintilla.TextLength;
            }

            // create the search flags for the Scintilla instance base on the given parameters..
            Scintilla.SearchFlags =
                ScintillaSearchFlagHelper.CreateFlags(
                    isRegExp, matchCase, matchWholeWord && !isRegExp, matchWordStart && !isRegExp);

            // if the search is extended, translate the escape characters to real characters..
            if (extended)
            {
                searchText = Regex.Unescape(searchText);
            }

            // set the property values for further use..
            SearchText = searchText;
            MatchCase = matchCase;
            IsRegExp = isRegExp;
            Extended = extended;
            // END: set the property values for further use..
        }

        /// <summary>
        /// Searches all matches within the current document.
        /// </summary>
        /// <returns>IEnumerable&lt;System.ValueTuple&lt;System.Int32, System.Int32&gt;&gt; containing the search results.</returns>
        public IEnumerable<(int lineNumber, int startLocation, int length)> SearchAll()
        {
            int len = SearchText.Length;
            var result = new List<(int lineNumber, int startLocation, int length)>();
            var find = Search(false, false);
            while (!result.Exists(f => f.startLocation == find.foundLocation) && find.success)
            {
                result.Add((Scintilla.LineFromPosition(find.foundLocation) + 1, find.foundLocation, len));
                find = Search(false, false);
            }

            result = result.OrderBy(f => f.startLocation).ToList();

            return result;
        }
    }
}
