using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    /// <summary>
    /// A class to help to build search flags for a <see cref="Scintilla"/> document.
    /// </summary>
    public class ScintillaSearchFlagHelper
    {
        /// <summary>
        /// Creates the search flags for a <see cref="Scintilla"/>.
        /// </summary>
        /// <param name="isRegExp">If set to <c>true</c> regular expressions are used with the search.</param>
        /// <param name="matchCase">if set to <c>true</c> the search is case-sensitive.</param>
        /// <param name="matchWholeWord">if set to <c>true</c> the search will only match a whole word.</param>
        /// <param name="matchWordStart">if set to <c>true</c> the search will match with a word start.</param>
        /// <returns>A <see cref="SearchFlags"/> enumeration based on the given parameters.</returns>
        public static SearchFlags CreateFlags(bool isRegExp, bool matchCase, bool matchWholeWord, bool matchWordStart)
        {
            SearchFlags result = SearchFlags.None;

            if (isRegExp)
            {
                result |= SearchFlags.Regex;
            }

            if (matchCase)
            {
                result |= SearchFlags.MatchCase;
            }

            if (matchWholeWord)
            {
                result |= SearchFlags.WholeWord;
            }

            if (matchWordStart)
            {
                result |= SearchFlags.WordStart;
            }

            return result;
        }
    }
}
