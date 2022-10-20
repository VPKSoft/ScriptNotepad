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

using System.Globalization;

namespace ScriptNotepad.UtilityClasses.TextManipulation;

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

    /// <summary>
    /// Reverses the specified string.
    /// </summary>
    /// <param name="str">The string to reverse.</param>
    /// <returns>A string reversed character by character compared to the original string.</returns>
    public static string Reverse(this string str)
    {
        var result = string.Empty;
        for (int i = str.Length - 1; i >= 0; i--)
        {
            result += str[i];
        }

        return result;
    }

    /// <summary>
    /// Retrieves a substring from this instance.
    /// The substring starts at a specified character position and continues to the end or to the start of the string
    /// depending on the sign of the index.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="startIndex">The zero-based starting or ending character position of a substring in this instance.</param>
    /// <returns>A string that is equivalent to the substring that begins at or ends to the <paramref name="startIndex" /> in this instance, or <see cref="F:System.String.Empty" /> if <paramref name="startIndex" /> is equal to the length of this instance or in case of an invalid index.</returns>
    public static string SafeSubString(this string str, int startIndex)
    {
        try
        {
            var result = startIndex >= 0 ? 
                str.Substring(startIndex) : 
                str.Substring(str.Length + startIndex);
            return result;
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Retrieves a substring from this instance. The substring starts at a specified character position and has a specified length.
    /// If the length is specified as negative the the length is decreased from the specified character position.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="startIndex">The zero-based starting character position of a substring in this instance.</param>
    /// <param name="length">The number of characters in the substring. Also negative lengths is allowed and the start index is the decremented by the length.</param>
    /// <returns>A string that is equivalent to the substring of length <paramref name="length" /> that begins at <paramref name="startIndex" /> in this instance or at at <paramref name="startIndex" /> decremented by a given negative length <paramref name="length"/>, or <see cref="F:System.String.Empty" /> if <paramref name="startIndex" /> is equal to the length of this instance and <paramref name="length" /> is zero.</returns>
    public static string SafeSubString(this string str, int startIndex, int length)
    {
        try
        {
            var result = length >= 0 ? 
                str.Substring(startIndex, length) : 
                str.Substring(startIndex + length, -length);
            return result;
        }
        catch
        {
            return string.Empty;
        }
    }
}