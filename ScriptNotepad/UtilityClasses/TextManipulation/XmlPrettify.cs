#region License
/*
MIT License

Copyright(c) 2021 Petteri Kautonen

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

using ScriptNotepad.UtilityClasses.TextManipulation.Xml;

namespace ScriptNotepad.UtilityClasses.TextManipulation;

/// <summary>
/// A class for XML text utilities.
/// </summary>
public static class XmlUtils
{
    private static readonly XmlMultilineConvert XmlMultilineConvert = new();

    private static readonly XmlSingleLineConvert XmlSingleLineConvert = new();

    /// <summary>
    /// Prettifies a specified XML text.
    /// </summary>
    /// <param name="value">The value to prettify.</param>
    /// <returns>Intended XML text.</returns>
    public static string XmlPrettify(this string value)
    {
        return XmlMultilineConvert.Manipulate(value);
    }


    /// <summary>
    /// Uglifies a specified XML text (he-hee).
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>System.String.</returns>
    public static string XmlUglify(this string value)
    {
        return XmlSingleLineConvert.Manipulate(value);
    }
}