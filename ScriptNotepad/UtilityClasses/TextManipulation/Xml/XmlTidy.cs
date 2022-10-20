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

using System.Text.RegularExpressions;
using System.Xml;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.UtilityClasses.TextManipulation.Xml;

/// <summary>
/// An utility class to clean up XML data.
/// </summary>
internal static class XmlTidy
{
    /// <summary>
    /// Tidies the specified XML string value.
    /// </summary>
    /// <param name="value">The XML string value.</param>
    /// <param name="multiLine">if set to <c>true</c> return the XML as multiline.</param>
    /// <returns>The specified XML string formatted.</returns>
    public static string Tidy(string value, bool multiLine)
    {
        try
        {
            var doc = new XmlDocument();

            // Only support for utf-8 and utf-16 encodings.
            var utf16 = value.Contains("encoding=\"utf-16\"");

            // Check if the XML contains the encoding data.
            var regex = new Regex(@"<\?xml version=\"".*?\"" encoding=\"".*?\""\?>");

            var hasEncoding = regex.IsMatch(value);

            doc.LoadXml(value);

            var memoryStream = new MemoryStream();

            Encoding encoding = utf16 ? new UnicodeEncoding(false, false) : new UTF8Encoding(false);

            // Set the XML "formatting" as requested.
            var settings = multiLine
                ? new XmlWriterSettings { Indent = true, IndentChars = "\t", Encoding = encoding, }
                : new XmlWriterSettings { Encoding = encoding, };

            using var writer = XmlWriter.Create(memoryStream, settings);

            doc.Save(writer);

            writer.Close();

            var result = encoding.GetString(memoryStream.ToArray());

            // Remove the encoding data if the original value didn't contain it.
            if (!hasEncoding)
            {
                result = regex.Replace(result, string.Empty);
                result = result.TrimStart('\n', '\r');
            }

            return result;
        }
        catch (Exception ex)
        {
            ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
            return value;
        }
    }
}