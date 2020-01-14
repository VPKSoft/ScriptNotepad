﻿#region License
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
using System.Collections.Generic;
using ScintillaNET;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.LinesAndBinary;

namespace ScriptNotepad.UtilityClasses.TextManipulation
{
    /// <summary>
    /// A class for handling duplicate lines within a 
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class DuplicateLines: ErrorHandlingBase
    {
        /// <summary>
        /// Removes the duplicate lines from a given <see cref="Scintilla"/> control.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla"/> control.</param>
        /// <param name="stringComparison">The type of string comparison.</param>
        /// <param name="fileLineTypes">The type of the line ending in the <see cref="Scintilla"/> control.</param>
        public static void RemoveDuplicateLines(Scintilla scintilla, StringComparison stringComparison, FileLineTypes fileLineTypes)
        {
            try
            {
                var lines = ScintillaLinesToStringList.GetScintillaLinesAsStringList(scintilla);
                var linesNew = new List<string>();
                foreach (var line in lines)
                {
                    if (!linesNew.Exists(f => f.Equals(line, stringComparison)))
                    {
                        linesNew.Add(line);
                    }
                }

                scintilla.Text = string.Concat(linesNew);
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
            }
        }
    }
}
