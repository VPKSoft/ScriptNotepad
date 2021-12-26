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

using ScriptNotepad.UtilityClasses.TextManipulation.base64;

namespace ScriptNotepad.UtilityClasses.TextManipulation
{
    /// <summary>
    /// A class to convert base64 encoded string data.
    /// </summary>
    public static class Base64Convert
    {
        private static readonly Base64ToString Base64ToString = new();

        private static readonly StringToBase64 StringToBase64 = new();

        /// <summary>
        /// Converts the specified string value into base64 encoded data string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The specified string converted into base64 encoded data string.</returns>
        public static string ToBase64(this string value)
        {
            return StringToBase64.Manipulate(value);
        }

        /// <summary>
        /// Converts the specified base64 encoded string data into string a string value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The specified base64 encoded string data converted into string a string value.</returns>
        public static string FromBase64(this string value)
        {
            return Base64ToString.Manipulate(value);
        }
    }
}
