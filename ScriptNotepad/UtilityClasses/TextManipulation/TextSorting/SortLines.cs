#region License
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

using System;
using System.Collections.Generic;
using System.Linq;
using ScintillaNET;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using VPKSoft.LangLib;

namespace ScriptNotepad.UtilityClasses.TextManipulation.TextSorting
{
    /// <summary>
    /// A class for sorting lines within a <see cref="Scintilla"/> control.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class SortLines: ErrorHandlingBase
    {
        /// <summary>
        /// Gets or sets the default sort function for the Sort method.
        /// </summary>
        public static Func<IEnumerable<string>, SortTextStyle, bool, IEnumerable<string>>
            DefaultSortFunc { get; set; } =
            delegate(IEnumerable<string> lines, SortTextStyle sortTextStyle, bool descending)
            {
                lines =
                    descending ? lines
                        .OrderByDescending(f => f.ToComparisonVariant((StringComparison) sortTextStyle))
                    : lines
                        .OrderBy(f => f.ToComparisonVariant((StringComparison) sortTextStyle));
                return lines;
            };

        /// <summary>
        /// A method to sort the contents of a <see cref="Scintilla"/> control.
        /// </summary>
        /// <param name="scintilla">The scintilla control which contents to sort.</param>
        /// <param name="stringComparison">The type of string comparison to use with the sort.</param>
        /// <param name="descending">A value indicating whether to sort in descending order.</param>
        public static void Sort(Scintilla scintilla, StringComparison stringComparison, bool descending)
        {
            Sort(scintilla, stringComparison, descending, DefaultSortFunc);
        }

        /// <summary>
        /// A method to sort the contents of a <see cref="Scintilla"/> control.
        /// </summary>
        /// <param name="scintilla">The scintilla control which contents to sort.</param>
        /// <param name="stringComparison">The type of string comparison to use with the sort.</param>
        /// <param name="descending">A value indicating whether to sort in descending order.</param>
        /// <param name="sortFunc">A Func to to do the actual string sorting.</param>
        public static void Sort(Scintilla scintilla, StringComparison stringComparison, bool descending,
            Func<IEnumerable<string>, SortTextStyle, bool, IEnumerable<string>> sortFunc)
        {
            try
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
                    var lines = sortFunc(
                        scintilla.Lines.Where(f => f.Index >= startLine && f.Index <= endLine).Select(f => f.Text),
                        (SortTextStyle) stringComparison, descending);

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
                    var lines = sortFunc(
                        scintilla.Lines.OrderBy(f => f.Text.ToComparisonVariant(stringComparison)).Select(f => f.Text),
                        (SortTextStyle) stringComparison, descending);

                    // set the text..
                    scintilla.Text = string.Concat(lines);                    
                }
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
            }
        }

        /// <summary>
        /// A method to sort the contents of a <see cref="Scintilla"/> control.
        /// </summary>
        /// <param name="scintilla">The scintilla control which contents to sort.</param>
        /// <param name="sortText">The sort text.</param>
        /// <param name="stringComparison">The type of string comparison to use with the sort.</param>
        public static void Sort(Scintilla scintilla, SortText sortText, StringComparison stringComparison)
        {
            if ((int) sortText.SortTextStyle >= (int) StringComparison.CurrentCulture &&
                (int) sortText.SortTextStyle < (int) StringComparison.OrdinalIgnoreCase)
            {
                Sort(scintilla, (StringComparison) sortText.SortTextStyle, sortText.Descending);
            }
            else // lambda functions here for the other sorting styles..
            {
                if (sortText.SortTextStyle == SortTextStyle.Length)
                {
                    // sort by length..
                    Sort(scintilla, (StringComparison) sortText.SortTextStyle, sortText.Descending,
                        delegate(IEnumerable<string> lines, SortTextStyle style, bool descending)
                        {
                            var result = descending
                                ? lines.OrderByDescending(f => f.Length)
                                : lines.OrderBy(f => f.Length);
                            return result;
                        });
                    return;
                }

                if (sortText.SortTextStyle == SortTextStyle.LowerFirst ||
                    sortText.SortTextStyle == SortTextStyle.UpperFirst)
                {
                    // sort by explicit lower or upper case characters firs..
                    Sort(scintilla, (StringComparison) sortText.SortTextStyle, sortText.Descending,
                        delegate(IEnumerable<string> lines, SortTextStyle style, bool descending)
                        {
                            var enumerable = lines as string[] ?? lines.ToArray();

                            var orphan = enumerable.Where(f => f.ToLower() == f && f.ToUpper() == f).ToList();
                            var lower = enumerable.Where(f => f.ToLower() == f && f.ToUpper() != f).ToList();
                            var upper = enumerable.Where(f => f.ToUpper() == f && f.ToLower() != f).ToList();

                            lower =
                                descending
                                    ? lower.OrderByDescending(f => f.ToComparisonVariant(stringComparison)).ToList()
                                    : lower.OrderBy(f => f.ToComparisonVariant(stringComparison)).ToList();

                            upper = descending
                                ? upper.OrderByDescending(f => f.ToComparisonVariant(stringComparison)).ToList()
                                : upper.OrderBy(f => f.ToComparisonVariant(stringComparison)).ToList();

                            orphan = descending
                                ? orphan.OrderByDescending(f => f.ToComparisonVariant(stringComparison)).ToList()
                                : orphan.OrderBy(f => f.ToComparisonVariant(stringComparison)).ToList();

                            var result = new List<string>();

                            if (sortText.SortTextStyle == SortTextStyle.LowerFirst)
                            {
                                result.AddRange(lower);
                                result.AddRange(upper);
                            }
                            else
                            {
                                result.AddRange(upper);
                                result.AddRange(lower);
                            }

                            result.AddRange(orphan);

                            return result;
                        });
                }
            }
        }
    }


    /// <summary>
    /// An enumeration for a text sorting style.
    /// </summary>
    public enum SortTextStyle
    {
        /// <summary>
        /// Compare strings using culture-sensitive sort rules and the current culture.
        /// </summary>
        CurrentCulture,

        /// <summary>
        /// Compare strings using culture-sensitive sort rules, the current culture, and ignoring the case of the strings being compared.
        /// </summary>
        CurrentCultureIgnoreCase,

        /// <summary>
        /// Compare strings using culture-sensitive sort rules and the invariant culture.
        /// </summary>
        InvariantCulture,

        /// <summary>
        /// Compare strings using culture-sensitive sort rules, the invariant culture, and ignoring the case of the strings being compared.
        /// </summary>
        InvariantCultureIgnoreCase,
        /// <summary>
        /// Compare strings using ordinal (binary) sort rules.
        /// </summary>
        Ordinal,

        /// <summary>
        /// Compare strings using ordinal (binary) sort rules and ignoring the case of the strings being compared.
        /// </summary>
        OrdinalIgnoreCase,

        /// <summary>
        /// Compare strings by their length.
        /// </summary>
        Length,

        /// <summary>
        /// First compare complete upper-case strings.
        /// </summary>
        UpperFirst,

        /// <summary>
        /// First compare complete lower-case strings.
        /// </summary>
        LowerFirst,
    }

    /// <summary>
    /// A class to indicate a single text sorting style.
    /// </summary>
    public class SortText
    {
        /// <summary>
        /// Gets a display name for this <see cref="SortText"/>.
        /// </summary>
        public string DisplayName
        {
            get
            {
                switch (SortTextStyle)
                {
                    case SortTextStyle.CurrentCulture:
                        return DBLangEngine.GetStatMessage("msgCurrentCulture",
                            "Current culture|A short explanation for the System.StringComparison.CurrentCulture enumeration value");
                    case SortTextStyle.CurrentCultureIgnoreCase:
                        return DBLangEngine.GetStatMessage("msgCurrentCultureIgnoreCase",
                            "Current culture (no case)|A short explanation for the System.StringComparison.CurrentCultureIgnoreCase enumeration value");
                    case SortTextStyle.InvariantCulture:
                        return DBLangEngine.GetStatMessage("msgInvariantCulture",
                            "Invariant|A short explanation for the System.StringComparison.InvariantCulture enumeration value");
                    case SortTextStyle.InvariantCultureIgnoreCase:
                        return DBLangEngine.GetStatMessage("msgInvariantCultureIgnoreCase",
                            "Invariant (no case)|A short explanation for the System.StringComparison.InvariantCultureIgnoreCase enumeration value");
                    case SortTextStyle.Ordinal:
                        return DBLangEngine.GetStatMessage("msgOrdinalCulture",
                            "Ordinal|A short explanation for the System.StringComparison.Ordinal enumeration value");
                    case SortTextStyle.OrdinalIgnoreCase:
                        return DBLangEngine.GetStatMessage("msgOrdinalCultureIgnoreCase",
                            "Ordinal (no case)|A short explanation for the System.StringComparison.OrdinalIgnoreCase enumeration value");
                    case SortTextStyle.Length:
                        return DBLangEngine.GetStatMessage("msgStringComparisonLength",
                            "Length|A short explanation for a text comparison (sorting by length)");
                    case SortTextStyle.UpperFirst:
                        return DBLangEngine.GetStatMessage("msgStringComparisonUpperFirst",
                            "Upper first|A short explanation for a text comparison (completely upper-case strings are compared first)");
                    case SortTextStyle.LowerFirst:
                        return DBLangEngine.GetStatMessage("msgStringComparisonLowerFirst",
                            "Lower first|A short explanation for a text comparison (completely lower-case strings are compared first)");
                    default:
                        return DBLangEngine.GetStatMessage("msgUnknown",
                            "Unknown|A short message to describe an unknown or undefined value");
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Gets the sorting style for this <see cref="SortText"/> class instance.
        /// </summary>
        public SortTextStyle SortTextStyle { get; set; }

        /// <summary>
        /// A value indicating whether to use descending sorting for the text.
        /// </summary>
        public bool Descending { get; set; }
    }
}
