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

using System;
using System.IO;
using System.Text;
using System.Xml;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.TextManipulation.BaseClasses;

namespace ScriptNotepad.UtilityClasses.TextManipulation.Xml
{
    /// <summary>
    /// A class to convert single-line XML to formatted XML.
    /// Implements the <see cref="TextManipulationCommandBase" />
    /// </summary>
    /// <seealso cref="TextManipulationCommandBase" />
    public class XmlMultilineConvert: TextManipulationCommandBase
    {
        /// <summary>
        /// Manipulates the specified text value.
        /// </summary>
        /// <param name="value">The value to manipulate.</param>
        /// <returns>A string containing the manipulated text.</returns>
        public override string Manipulate(string value)
        {
            try
            {
                var doc = new XmlDocument();

                var utf16 = value.Contains("encoding=\"utf-16\"");

                doc.LoadXml(value);

                var memoryStream = new MemoryStream();

                Encoding encoding = utf16 ? new UnicodeEncoding(false, false) : new UTF8Encoding(false);

                var builder = new StringBuilder();
                using var writer =
                    XmlWriter.Create(memoryStream, new XmlWriterSettings { Indent = true, IndentChars = "\t", Encoding = encoding });

                doc.Save(writer);

                writer.Close();

                return encoding.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
                return value;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return MethodName;
        }
    }
}
