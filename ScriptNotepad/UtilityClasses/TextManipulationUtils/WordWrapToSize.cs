#region License
/*
Copyright (c) 2019 Rosetta Code.
  Permission is granted to copy, distribute and/or modify this document
  under the terms of the GNU Free Documentation License, Version 1.2
  or any later version published by the Free Software Foundation;
  with no Invariant Sections, no Front-Cover Texts, and no Back-Cover
  Texts.  A copy of the license is included in the section entitled "GNU
  Free Documentation License".
*/
#endregion

using System;
using System.Collections.Generic;

namespace ScriptNotepad.UtilityClasses.TextManipulationUtils
{
    /// <summary>
    /// A helper class to wrap text into lines with given length.
    /// (C): https://www.rosettacode.org/wiki/Word_wrap#C.23 (GNU Free Documentation License 1.2 / https://www.gnu.org/licenses/old-licenses/fdl-1.2.txt)
    /// </summary>
    public static class WordWrapToSize
    {
        /// <summary>
        /// Wraps the specified text into lines with the maximum length of <paramref name="lineWidth"/>.
        /// </summary>
        /// <param name="text">The text to wrap.</param>
        /// <param name="lineWidth">The maximum width of the line.</param>
        /// <returns>System.String.</returns>
        public static string Wrap(string text, int lineWidth)
        {
            text = RemoveLineEndings(text);
            return string.Join(string.Empty,
                Wrap(
                    text.Split(new[] {' '}, // changed this to a white space (VPKSoft)..
                        StringSplitOptions
                            .RemoveEmptyEntries),
                    lineWidth));
        }

        /// <summary>
        /// Removes the line endings from a given string replacing the line endings with a white space character.
        /// </summary>
        /// <param name="toRemoveFrom">The string to remove the line endings from.</param>
        /// <returns>System.String.</returns>
        private static string RemoveLineEndings(string toRemoveFrom)
        {
            toRemoveFrom = toRemoveFrom.Replace("\r\n", " ");
            toRemoveFrom = toRemoveFrom.Replace("\n\r", " ");
            toRemoveFrom = toRemoveFrom.Replace("\r", " ");
            toRemoveFrom = toRemoveFrom.Replace("\n", " ");
            return toRemoveFrom;
        }

        /// <summary>
        /// Wraps the specified words into lines.
        /// </summary>
        /// <param name="words">The words to wrap.</param>
        /// <param name="lineWidth">The width of the line.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        private static IEnumerable<string> Wrap(IEnumerable<string> words,
            int lineWidth)
        {
            var currentWidth = 0;
            foreach (var word in words)
            {
                if (currentWidth != 0)
                {
                    if (currentWidth + word.Length < lineWidth)
                    {
                        currentWidth++;
                        yield return " ";
                    }
                    else
                    {
                        currentWidth = 0;
                        yield return Environment.NewLine;
                    }
                }
                currentWidth += word.Length;
                yield return word;
            }
        }
    }
}
