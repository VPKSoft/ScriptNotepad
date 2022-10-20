#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

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

using ScriptNotepad.UtilityClasses.TextManipulation.Json;

namespace ScriptNotepad.UtilityClasses.TextManipulation;

/// <summary>
/// A class for Json text utilities.
/// </summary>
public static class JsonUtils
{
    private static readonly JsonMultilineConvert JsonMultilineConvert = new();

    private static readonly JsonSingleLineConvert JsonSingleLineConvert = new();

    /// <summary>
    /// Prettifies a specified Json text.
    /// </summary>
    /// <param name="value">The value to prettify.</param>
    /// <returns>Intended Json text.</returns>
    public static string JsonPrettify(this string value)
    {
        return JsonMultilineConvert.Manipulate(value);
    }

    /// <summary>
    /// Uglifies a specified Json text (he-hee).
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>System.String.</returns>
    public static string JsonUglify(this string value)
    {
        return JsonSingleLineConvert.Manipulate(value);
    }
}