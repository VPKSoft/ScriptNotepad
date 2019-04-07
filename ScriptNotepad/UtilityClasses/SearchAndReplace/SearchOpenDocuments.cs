using ScintillaNET;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    /// <summary>
    /// A helper class for searching opened <see cref="Scintilla"/> documents.
    /// </summary>
    /// <seealso cref="ErrorHandlingBase"/>
    public class SearchOpenDocuments: ErrorHandlingBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchOpenDocuments"/> class.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla"/> document to search from.</param>
        /// <param name="isRegExp">If set to <c>true</c> regular expressions are used with the search.</param>
        /// <param name="matchCase">if set to <c>true</c> the search is case-sensitive.</param>
        /// <param name="matchWholeWord">If set to <c>true</c> the search will only match a whole word.</param>
        /// <param name="matchWordStart">If set to <c>true</c> the search will match with a word start.</param>
        /// <param name="resetTarget">If set to <c>true</c> the search area of the document is reset to the whole document.</param>
        /// <param name="wrapAround">If set to <c>true</c> the search can continue from the beginning of the document after reaching the end of the document or from the end of the document after reaching the start of the document.</param>
        /// <param name="extended">If set to <c>true</c> the search translates escape characters into their corresponding character values.</param>
        /// <param name="searchText">The text to search from the <see cref="Scintilla"/> document.</param>
        public SearchOpenDocuments(Scintilla scintilla, bool isRegExp, bool
            matchCase, bool matchWholeWord, bool matchWordStart, bool resetTarget, bool wrapAround, bool extended, string searchText)
        {
            // save the Scintilla instance..
            Scintilla = scintilla;

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
        /// Gets or sets a value indicating whether the search can continue from the beginning of the document after reaching the end of the document or from the end of the document after reaching the start of the document..
        /// </summary>
        private bool WrapAround { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the search is case-sensitive.
        /// </summary>
        private bool MatchCase { get; }

        /// <summary>
        /// Gets or sets the Scintilla document which this class uses for searching text from.
        /// </summary>
        private Scintilla Scintilla { get; }

        /// <summary>
        /// Gets or sets the search text to search from a <see cref="Scintilla"/> document.
        /// </summary>
        private string SearchText { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is searching the <see cref="Scintilla"/> document using regular expressions.
        /// </summary>
        private bool IsRegExp { get; }

        /// <summary>
        /// Gets or sets a value indicating whether search is extended to find escaped characters.
        /// </summary>
        private bool Extended { get; }

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
        private PreviousDirection previousSearchDirection = PreviousDirection.None;

        /// <summary>
        /// Set the value for the previous direction of the search.
        /// </summary>
        private PreviousDirection PreviousSearchDirection
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
                        Scintilla.TargetStart = 0;
                        Scintilla.TargetEnd =
                            Scintilla.CurrentPosition <= Scintilla.TargetStart
                                ? Scintilla.TextLength
                                : Scintilla.CurrentPosition;
                    }
                    else if (value == PreviousDirection.Forward)
                    {
                        // TODO::Make the logic work!
                        Scintilla.TargetStart = Scintilla.CurrentPosition == Scintilla.TextLength
                            ? 0
                            : Scintilla.CurrentPosition;
                        Scintilla.TargetEnd = Scintilla.TextLength;
                    }
                }
                previousSearchDirection = value;
            }
        }

        /// <summary>
        /// Searches the next or the previous occurrence of the search string from the <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="backward">If set to <c>true</c> the search is run backwards.</param>
        /// <param name="noReset">A flag indicating whether to reset allow the method to reset the search to match the entire document.</param>
        /// <returns>True if the text was found; otherwise false.</returns>
        public (bool success, int foundLocation) Search(bool backward, bool noReset = false)
        {
            // TODO::Make the noReset flag to work!
            PreviousSearchDirection = backward ? PreviousDirection.Backward : PreviousDirection.Forward;

            // the searching and selection methods change these values so do save them..
            int targetEnd = Scintilla.TargetEnd;
            int targetStart = Scintilla.TargetStart;

            int foundLocation = Extended ? SearchExtended(backward) :
                (backward ? FindLastOccurrence(false) : Scintilla.SearchInTarget(SearchText));

            if (foundLocation == -1 && WrapAround && !noReset)
            {
                targetStart = 0;
                targetEnd = Scintilla.TextLength;
                Scintilla.TargetStart = 0;
                Scintilla.TargetEnd = Scintilla.TextLength;

                foundLocation = Extended ? SearchExtended(backward) :
                    (backward ? FindLastOccurrence(false) : Scintilla.SearchInTarget(SearchText));

                if (foundLocation == -1)
                {
                    Scintilla.TargetStart = targetStart;
                    Scintilla.TargetEnd = targetEnd;
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
                Scintilla.GotoPosition(foundLocation);
            }

            if (IsRegExp)
            {
                if (noReset)
                {
                    var line = Scintilla.Lines[Scintilla.LineFromPosition(foundLocation)];
                    Scintilla.TargetStart = line.EndPosition;
                    Scintilla.TargetEnd = Scintilla.TextLength;
                    if (Scintilla.TargetStart + SearchText.Length >= Scintilla.TextLength)
                    {
                        return (false, -1);
                    }
                }
                else
                {
                    var line = Scintilla.Lines[Scintilla.LineFromPosition(Scintilla.CurrentPosition)];
                    Scintilla.SetSelection(line.Position, line.EndPosition);
                    Scintilla.TargetStart = backward ? 0 : line.EndPosition;
                    Scintilla.TargetEnd = backward ? Scintilla.CurrentPosition - line.Length : targetEnd;
                }
            }
            else
            {
                if (noReset)
                {
                    Scintilla.TargetStart += SearchText.Length;
                    Scintilla.TargetEnd = Scintilla.TextLength;
                    if (Scintilla.TargetStart + SearchText.Length >= Scintilla.TextLength)
                    {
                        return (false, -1);
                    }
                }
                else
                {
                    Scintilla.SetSelection(Scintilla.CurrentPosition, Scintilla.CurrentPosition + SearchText.Length);
                    Scintilla.TargetStart = backward ? 0 : Scintilla.CurrentPosition + SearchText.Length;
                    Scintilla.TargetEnd = backward ? Scintilla.CurrentPosition - SearchText.Length : targetEnd;
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
            int targetStart = Scintilla.TargetStart;
            int targetEnd = Scintilla.TargetEnd;

            int targetStartModify = Scintilla.TargetStart;

            int foundLocationPrevious = Scintilla.SearchInTarget(SearchText);
            Scintilla.TargetStart = targetStartModify;
            Scintilla.TargetEnd = targetEnd;
            int foundLocation = foundLocationPrevious;

            if (foundLocation == -1 && !noRecall)
            {
                Scintilla.TargetStart = 0;
                Scintilla.TargetEnd = Scintilla.TextLength;
                return FindLastOccurrence(true);
            }
            else if (foundLocation == -1)
            {
                return foundLocation;
            }

            while (Scintilla.TargetStart < Scintilla.TargetEnd &&
                  (foundLocation = Scintilla.SearchInTarget(SearchText)) != -1)
            {
                foundLocationPrevious = foundLocation;
                if (!IsRegExp)
                {
                    targetStartModify = foundLocation + SearchText.Length;
                    Scintilla.TargetStart = targetStartModify;
                    Scintilla.TargetEnd = targetEnd;
                }
                else
                {
                    var line = Scintilla.Lines[Scintilla.LineFromPosition(foundLocation)];
                    targetStartModify = foundLocation + line.Length;
                    Scintilla.TargetStart = targetStartModify;
                    Scintilla.TargetEnd = targetEnd;
                }
            }
            return foundLocationPrevious;
        }

        /// <summary>
        /// Resets the search area from 0 to the length of the document.
        /// </summary>
        public void ResetSearchArea()
        {
            Scintilla.TargetStart = 0;
            Scintilla.TargetEnd = Scintilla.TextLength;
        }

        /// <summary>
        /// Resets the search area if any occurrences can be found in the document.
        /// <param name="backward">If set to <c>true</c> the search is run backwards.</param>
        /// </summary>
        /// <returns>True if the search area reset yields search results; otherwise false.</returns>
        public bool ResetSearchArea(bool backward)
        {
            // the searching and selection methods change these values so do save them..
            int targetStart = Scintilla.TargetStart;
            int targetEnd = Scintilla.TargetEnd;

            if ((Extended ? SearchExtended(backward) :
                backward ? FindLastOccurrence(false) : Scintilla.SearchInTarget(SearchText)) == -1)
            {
                Scintilla.TargetStart = 0;
                Scintilla.TargetEnd = Scintilla.TextLength;
                if ((Extended ? SearchExtended(backward) :
                        backward ? FindLastOccurrence(false) : Scintilla.SearchInTarget(SearchText)) != -1)
                {
                    Scintilla.TargetStart = 0;
                    Scintilla.TargetEnd = Scintilla.TextLength;
                    return true;
                }
                else
                {
                    Scintilla.TargetStart = targetStart;
                    Scintilla.TargetEnd = targetEnd;
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Searches the text with extended method by translating escape sequences to characters.
        /// </summary>
        /// <param name="backward">If set to <c>true</c> the search is run backwards.</param>
        /// <returns>The position in the <seealso cref="Scintilla"/> where a match was found.</returns>
        private int SearchExtended(bool backward)
        {
            try
            {
                // the searching and selection methods change these values so do save them..
                int targetStart = Scintilla.TargetStart;
                int targetEnd = Scintilla.TargetEnd;

                int count = targetEnd - targetStart - 1;
                count += backward ? targetStart + count : 0;

                if (!backward)
                {
                    int index =
                        Scintilla.Text.IndexOf(SearchText,
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
            int targetStart = Scintilla.TargetStart;
            int targetEnd = Scintilla.TargetEnd;

            int count = targetEnd - targetStart - 1;

            int index;

            List<int> indices = new List<int>();

            while ((index =
                       Scintilla.Text.IndexOf(SearchText,
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
        /// Gets a count of string occurrences with <seealso cref="Extended"/> set to true.
        /// </summary>
        /// <returns>The count of the string occurrences with the extended search.</returns>
        public int CountExtended()
        {
            int index = 0;

            int result = 0;

            //find all indexes/occurrences of specified substring in string
            while ((index = Scintilla.Text.IndexOf(SearchText, index,
                       MatchCase ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase)) !=
                   -1)
            {
                result++;
                index++;
            }

            return result;
        }
    }
}
