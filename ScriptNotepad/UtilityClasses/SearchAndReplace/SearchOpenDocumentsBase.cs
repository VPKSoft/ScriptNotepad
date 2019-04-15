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
using System.IO;
using System.Linq;
using ScintillaNET;
using ScriptNotepad.Database.Tables;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    /// <summary>
    /// The base class for search and replace operations.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class SearchOpenDocumentsBase: ErrorHandlingBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether the search can continue from the beginning of the document after reaching the end of the document or from the end of the document after reaching the start of the document..
        /// </summary>
        internal bool WrapAround { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the search is case-sensitive.
        /// </summary>
        internal bool MatchCase { get; set; }

        /// <summary>
        /// Gets or sets the Scintilla document which this class uses for searching text from.
        /// </summary>
        internal (Scintilla scintilla, string fileName) Scintilla { get; set; }

        /// <summary>
        /// Gets or sets all the open documents in the software.
        /// </summary>
        internal List<(Scintilla scintilla, string fileName)> OpenDocuments { get; set; } = new List<(Scintilla scintilla, string fileName)>();

        /// <summary>
        /// Gets or sets the search text to search from a <see cref="Scintilla"/> document.
        /// </summary>
        internal string SearchText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is searching the <see cref="Scintilla"/> document using regular expressions.
        /// </summary>
        internal bool IsRegExp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether search is extended to find escaped characters.
        /// </summary>
        internal bool Extended { get; set; }

        /// <summary>
        /// An enumeration describing the previous direction of the search.
        /// </summary>
        internal enum PreviousDirection
        {
            /// <summary>
            /// The direction hasn't yet been recorded.
            /// </summary>
            None,

            /// <summary>
            /// The Scintilla's search area is reset on the first search depending on the direction of the search.
            /// </summary>
            ResetOnDirection,

            /// <summary>
            /// The previous search was backwards.
            /// </summary>
            Backward,

            /// <summary>
            /// The previous search was forwards.
            /// </summary>
            Forward
        }

        // the value for the previous direction of the search..
        internal PreviousDirection previousSearchDirection = PreviousDirection.None;

        /// <summary>
        /// Set the value for the previous direction of the search.
        /// </summary>
        internal PreviousDirection PreviousSearchDirection
        {
            set
            { 
                if (previousSearchDirection != value && 
                    (previousSearchDirection != PreviousDirection.None ||
                     // this indicates that the reset must be done disregarding the direction of the search..
                     previousSearchDirection == PreviousDirection.ResetOnDirection 
                    ))
                {
                    if (value == PreviousDirection.Backward)
                    {
                        // TODO::Make the logic work!
                        Scintilla.scintilla.TargetStart = 0;
                        Scintilla.scintilla.TargetEnd =
                            Scintilla.scintilla.CurrentPosition <= Scintilla.scintilla.TargetStart
                                ? Scintilla.scintilla.TextLength
                                : Scintilla.scintilla.CurrentPosition;
                    }
                    else if (value == PreviousDirection.Forward)
                    {
                        // TODO::Make the logic work!
                        Scintilla.scintilla.TargetStart = Scintilla.scintilla.CurrentPosition == Scintilla.scintilla.TextLength
                            ? 0
                            : Scintilla.scintilla.CurrentPosition;
                        Scintilla.scintilla.TargetEnd = Scintilla.scintilla.TextLength;
                    }
                }
                previousSearchDirection = value;
            }
        }

        /// <summary>
        /// Searches the next or the previous occurrence of the search string from the <see cref="ScintillaNET.Scintilla"/> document.
        /// </summary>
        /// <param name="backward">If set to <c>true</c> the search is run backwards.</param>
        /// <param name="noReset">A flag indicating whether to reset allow the method to reset the search to match the entire document.</param>
        /// <param name="noSelect">A flag indicating whether the search result should be selected in the <see cref="Scintilla"/> or not.</param>
        /// <returns>True if the text was found; otherwise false.</returns>
        public (bool success, int foundLocation) Search(bool backward, bool noReset = false, bool noSelect = false)
        {
            // TODO::Make the noReset flag to work!
            PreviousSearchDirection = backward ? PreviousDirection.Backward : PreviousDirection.Forward;

            // the searching and selection methods change these values so do save them..
            int targetEnd = Scintilla.scintilla.TargetEnd;
            int targetStart;

            int foundLocation = Extended ? SearchExtended(backward) :
                (backward ? FindLastOccurrence(false) : Scintilla.scintilla.SearchInTarget(SearchText));

            if (foundLocation == -1 && WrapAround && !noReset)
            {
                targetStart = 0;
                targetEnd = Scintilla.scintilla.TextLength;
                Scintilla.scintilla.TargetStart = 0;
                Scintilla.scintilla.TargetEnd = Scintilla.scintilla.TextLength;

                foundLocation = Extended ? SearchExtended(backward) :
                    (backward ? FindLastOccurrence(false) : Scintilla.scintilla.SearchInTarget(SearchText));

                if (foundLocation == -1)
                {
                    Scintilla.scintilla.TargetStart = targetStart;
                    Scintilla.scintilla.TargetEnd = targetEnd;
                    if (ResetSearchArea(backward))
                    {
                        return Search(backward);
                    }
                    return (false, foundLocation);
                }
            }
            else if (foundLocation == -1 && !noReset)
            {
                if (ResetSearchArea(backward))
                {
                    return Search(backward);
                }
                return (false, foundLocation);
            }

            if (!noReset)
            {
                Scintilla.scintilla.GotoPosition(foundLocation);
            }

            if (IsRegExp)
            {
                if (noReset)
                {
                    var line = Scintilla.scintilla.Lines[Scintilla.scintilla.LineFromPosition(foundLocation)];
                    Scintilla.scintilla.TargetStart = line.EndPosition;
                    Scintilla.scintilla.TargetEnd = Scintilla.scintilla.TextLength;
                    if (Scintilla.scintilla.TargetStart + SearchText.Length >= Scintilla.scintilla.TextLength)
                    {
                        return (false, -1);
                    }
                }
                else
                {
                    var line = Scintilla.scintilla.Lines[
                        Scintilla.scintilla.LineFromPosition(Scintilla.scintilla.CurrentPosition)];

                    if (!noSelect) // just search, no selection..
                    {
                        Scintilla.scintilla.SetSelection(line.Position, 
                            line.EndPosition);
                    }

                    Scintilla.scintilla.TargetStart = backward ? 0 : line.EndPosition;
                    Scintilla.scintilla.TargetEnd =
                        backward ? Scintilla.scintilla.CurrentPosition - line.Length : targetEnd;
                }
            }
            else
            {
                if (noReset)
                {
                    Scintilla.scintilla.TargetStart += SearchText.Length;
                    Scintilla.scintilla.TargetEnd = Scintilla.scintilla.TextLength;
                    if (Scintilla.scintilla.TargetStart + SearchText.Length >= Scintilla.scintilla.TextLength)
                    {
                        return (false, -1);
                    }
                }
                else
                {
                    if (!noSelect) // just search, no selection..
                    {

                        Scintilla.scintilla.SetSelection(Scintilla.scintilla.CurrentPosition,
                            Scintilla.scintilla.CurrentPosition + SearchText.Length);
                    }

                    Scintilla.scintilla.TargetStart = backward ? 0 : Scintilla.scintilla.CurrentPosition + SearchText.Length;
                    Scintilla.scintilla.TargetEnd = backward ? Scintilla.scintilla.CurrentPosition - SearchText.Length : targetEnd;
                }
            }

            return (foundLocation != -1, foundLocation);
        }

        /// <summary>
        /// Finds the last occurrence index from the Scintilla.
        /// </summary>
        /// <param name="noRecall">If set to <c>true</c> then the method will not be called again with the full document search area.</param>
        /// <returns></returns>
        public int FindLastOccurrence(bool noRecall)
        {
            // the searching and selection methods change these values so do save them..
            int targetEnd = Scintilla.scintilla.TargetEnd;

            int targetStartModify = Scintilla.scintilla.TargetStart;

            int foundLocationPrevious = Scintilla.scintilla.SearchInTarget(SearchText);
            Scintilla.scintilla.TargetStart = targetStartModify;
            Scintilla.scintilla.TargetEnd = targetEnd;
            int foundLocation = foundLocationPrevious;

            if (foundLocation == -1 && !noRecall)
            {
                Scintilla.scintilla.TargetStart = 0;
                Scintilla.scintilla.TargetEnd = Scintilla.scintilla.TextLength;
                return FindLastOccurrence(true);
            }
            else if (foundLocation == -1)
            {
                return foundLocation;
            }

            while (Scintilla.scintilla.TargetStart < Scintilla.scintilla.TargetEnd &&
                  (foundLocation = Scintilla.scintilla.SearchInTarget(SearchText)) != -1)
            {
                foundLocationPrevious = foundLocation;
                if (!IsRegExp)
                {
                    targetStartModify = foundLocation + SearchText.Length;
                    Scintilla.scintilla.TargetStart = targetStartModify;
                    Scintilla.scintilla.TargetEnd = targetEnd;
                }
                else
                {
                    var line = Scintilla.scintilla.Lines[Scintilla.scintilla.LineFromPosition(foundLocation)];
                    targetStartModify = foundLocation + line.Length;
                    Scintilla.scintilla.TargetStart = targetStartModify;
                    Scintilla.scintilla.TargetEnd = targetEnd;
                }
            }
            return foundLocationPrevious;
        }

        /// <summary>
        /// Resets the search area if any occurrences can be found in the document.
        /// <param name="backward">If set to <c>true</c> the search is run backwards.</param>
        /// </summary>
        /// <returns>True if the search area reset yields search results; otherwise false.</returns>
        public bool ResetSearchArea(bool backward)
        {
            // the searching and selection methods change these values so do save them..
            int targetStart = Scintilla.scintilla.TargetStart;
            int targetEnd = Scintilla.scintilla.TargetEnd;

            if ((Extended ? SearchExtended(backward) :
                backward ? FindLastOccurrence(false) : Scintilla.scintilla.SearchInTarget(SearchText)) == -1)
            {
                Scintilla.scintilla.TargetStart = 0;
                Scintilla.scintilla.TargetEnd = Scintilla.scintilla.TextLength;
                if ((Extended ? SearchExtended(backward) :
                        backward ? FindLastOccurrence(false) : Scintilla.scintilla.SearchInTarget(SearchText)) != -1)
                {
                    Scintilla.scintilla.TargetStart = 0;
                    Scintilla.scintilla.TargetEnd = Scintilla.scintilla.TextLength;
                    return true;
                }
                else
                {
                    Scintilla.scintilla.TargetStart = targetStart;
                    Scintilla.scintilla.TargetEnd = targetEnd;
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Searches the text with extended method by translating escape sequences to characters.
        /// </summary>
        /// <param name="backward">If set to <c>true</c> the search is run backwards.</param>
        /// <returns>The position in the <seealso cref="ScintillaNET.Scintilla"/> where a match was found.</returns>
        private int SearchExtended(bool backward)
        {
            try
            {
                // the searching and selection methods change these values so do save them..
                int targetStart = Scintilla.scintilla.TargetStart;
                int targetEnd = Scintilla.scintilla.TargetEnd;

                int count = targetEnd - targetStart - 1;
                count += backward ? targetStart + count : 0;

                if (!backward)
                {
                    int index =
                        Scintilla.scintilla.Text.IndexOf(SearchText,
                            targetStart, count,
                            !MatchCase
                                ? StringComparison.InvariantCultureIgnoreCase
                                : StringComparison.InvariantCulture);

                    return index;
                }
                else
                {
                    /* This was too difficult and always throwing exceptions..
                    int index = 
                        Scintilla.Text.LastIndexOf(SearchText,
                            targetEnd, targetStart,
                            !MatchCase
                                ? StringComparison.InvariantCultureIgnoreCase
                                : StringComparison.InvariantCulture);
                    */
                    int index = LastIndexOf();

                    return index;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogAction?.Invoke(ex);
                return -1;
            }
        }

        /// <summary>
        /// Gets a last index of a string <see cref="SearchText"/> within the class instance.
        /// </summary>
        /// <returns>The last index of a string with the class instance.</returns>
        private int LastIndexOf()
        {
            // the searching and selection methods change these values so do save them..
            int targetStart = Scintilla.scintilla.TargetStart;
            int targetEnd = Scintilla.scintilla.TargetEnd;

            int count = targetEnd - targetStart - 1;

            int index;

            List<int> indices = new List<int>();

            while ((index =
                       Scintilla.scintilla.Text.IndexOf(SearchText,
                           targetStart, count,
                           !MatchCase
                               ? StringComparison.InvariantCultureIgnoreCase
                               : StringComparison.InvariantCulture)) != -1)
            {
                if (!indices.Contains(index))
                {
                    indices.Add(index);
                }

                targetStart += SearchText.Length;
                count -= SearchText.Length;
                if (count <= 0)
                {
                    break;
                }
            }

            indices.Sort();
            if (indices.Count > 0)
            {
                return indices.Last();
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Resets the search area from 0 to the length of the document.
        /// </summary>
        public void ResetSearchArea()
        {
            Scintilla.scintilla.TargetStart = 0;
            Scintilla.scintilla.TargetEnd = Scintilla.scintilla.TextLength;
        }

        /// <summary>
        /// Gets a count of string occurrences with <seealso cref="Extended"/> set to true.
        /// </summary>
        /// <returns>The count of the string occurrences with the extended search.</returns>
        public int CountExtended()
        {
            int index = 0;

            int result = 0;

            //find all indexes/occurrences of specified substring in string
            while ((index = Scintilla.scintilla.Text.IndexOf(SearchText, index,
                       MatchCase ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase)) !=
                   -1)
            {
                result++;
                index++;
            }

            return result;
        }

        /// <summary>
        /// Searches all matches within the current document.
        /// </summary>
        /// <returns>IEnumerable&lt;System.ValueTuple&lt;System.Int32, System.Int32&gt;&gt; containing the search results.</returns>
        public IEnumerable<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool isFileOpen)> SearchAll()
        {
            Scintilla.scintilla.SuspendLayout();
            // save the current position as the Search() method changes it..
            int pos = Scintilla.scintilla.CurrentPosition; 
            int len = SearchText.Length;
            var result = new List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool isFileOpen)>();
            var find = Search(false, false, true);
            while (!result.Exists(f => f.startLocation == find.foundLocation) && find.success)
            {
                result.Add((Scintilla.fileName,
                    Scintilla.scintilla.LineFromPosition(find.foundLocation) + 1, find.foundLocation, len,
                    Scintilla.scintilla.Lines[Scintilla.scintilla.LineFromPosition(find.foundLocation)].Text, true));
                find = Search(false, false, true);
            }

            result = result.OrderBy(f => f.startLocation).ToList();

            // restore the current position before the Search() method changes..
            Scintilla.scintilla.GotoPosition(pos);
            Scintilla.scintilla.ResumeLayout();

            return result;
        }

        /// <summary>
        /// Searches all in all opened documents.
        /// </summary>
        /// <returns>IEnumerable&lt;System.ValueTuple&lt;System.String, System.Int32, System.Int32, System.Int32, System.String, System.Boolean&gt;&gt;.</returns>
        public IEnumerable<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
            isFileOpen)> SearchAllInAllOpened()
        {
            List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                isFileOpen)> result =
                new List<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                    isFileOpen)>();

            var currentDocument = Scintilla;
            foreach (var openDocument in OpenDocuments)
            {
                Scintilla = openDocument;
                result.AddRange(SearchAll().ToList());
            }

            Scintilla = currentDocument;

            return result.OrderBy(f => Path.GetFileName(f.fileName)?.ToLowerInvariant());
        }
    }
}
