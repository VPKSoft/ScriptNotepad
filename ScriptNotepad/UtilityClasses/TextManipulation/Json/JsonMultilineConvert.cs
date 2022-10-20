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

using Newtonsoft.Json.Linq;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.TextManipulation.BaseClasses;

namespace ScriptNotepad.UtilityClasses.TextManipulation.Json;

/// <summary>
/// A class to convert single-line JSON to formatted JSON.
/// Implements the <see cref="TextManipulationCommandBase" />
/// </summary>
/// <seealso cref="TextManipulationCommandBase" />
public class JsonMultilineConvert: TextManipulationCommandBase
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
            var result = JToken.Parse(value).ToString();

            return result; 
        }
        catch (Exception ex)
        {
            ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
            return value; 
        }
    }

    /// <inheritdoc cref="TextManipulationCommandBase.PreferSelectedText" />
    public override bool PreferSelectedText { get; set; } = false;

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
        return MethodName;
    }
}