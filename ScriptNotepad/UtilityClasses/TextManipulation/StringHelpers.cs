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
using System.Globalization;

namespace ScriptNotepad.UtilityClasses.TextManipulation
{
    /// <summary>
    /// Some helper methods for strings.
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// Returns a copy of this <see cref="T:System.String" /> object converted to style based on using the casing rules of the culture determined from the <paramref name="comparison"/>.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <param name="comparison">The comparison <see cref="StringComparison"/> to detect the string conversion from.</param>
        /// <returns>The string equivalent to the style based on using the casing rules of the culture determined from the <paramref name="comparison"/> of the current string.</returns>
        public static string ToComparisonVariant(this string str, StringComparison comparison)
        {
            switch (comparison)
            {
                case StringComparison.CurrentCulture:
                    return str.ToString(CultureInfo.CurrentCulture);

                case StringComparison.CurrentCultureIgnoreCase:
                    return str.ToLower(CultureInfo.CurrentCulture);

                case StringComparison.InvariantCulture:
                    return str.ToString(CultureInfo.InvariantCulture);

                case StringComparison.InvariantCultureIgnoreCase:
                    return str.ToLowerInvariant();

                case StringComparison.Ordinal:
                    return str;

                case StringComparison.OrdinalIgnoreCase:
                    return str.ToLower();
            }

            return str;
        }
    }
}
