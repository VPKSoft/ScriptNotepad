﻿#region License
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

using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.TextManipulation.BaseClasses;

namespace ScriptNotepad.UtilityClasses.TextManipulation.base64;

/// <summary>
/// A class to convert a base64 encoded data into UTF-8 encoded string.
/// Implements the <see cref="ScriptNotepad.UtilityClasses.TextManipulation.BaseClasses.TextManipulationCommandBase" />
/// </summary>
/// <seealso cref="ScriptNotepad.UtilityClasses.TextManipulation.BaseClasses.TextManipulationCommandBase" />
public class StringToBase64: TextManipulationCommandBase
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
            var bytes = Convert.FromBase64String(value);

            return Encoding.UTF8.GetString(bytes);
        }
        catch (Exception ex)
        {
            ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
            return value;
        }
    }

    /// <inheritdoc cref="TextManipulationCommandBase.PreferSelectedText" />
    public override bool PreferSelectedText { get; set; } = true;

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
        return MethodName;
    }
}