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
