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

using System.Linq;
using ScintillaNET;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.UtilityClasses.ScintillaHelpers
{
    /// <summary>
    /// A class to save and restore the folding state of the <see cref="Scintilla"/> document.
    /// </summary>
    public static class ScintillaFold
    {
        /// <summary>
        /// Saves the folding state of the <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla"/> document.</param>
        /// <returns>A string containing the folding state data.</returns>
        public static string SaveFolding(this Scintilla scintilla)
        {
            try
            {
                var builder = new StringBuilder();
                for (int i = 0; i < scintilla.Lines.Count; i++)
                {
                    if (!scintilla.Lines[i].Expanded)
                    {
                        builder.AppendFormat("{0}|{1};", i,
                            scintilla.Lines[i].Expanded); // this is useless for now, but keep the possibility open..
                    }
                }

                var result = builder.ToString().TrimEnd(';');

                return result;
            }
            catch (Exception ex)
            {
                ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Restores the folding state of the <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla"/> document.</param>
        /// <param name="foldSave">The string containing the folding state data.</param>
        public static void RestoreFolding(this Scintilla scintilla, string foldSave)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(foldSave))
                {
                    return;
                }

                var saveValues = foldSave.Split(';').Select(f => new {Array = f.Split('|')}).Select(f => new
                {
                    Line = int.Parse(f.Array[0]),
                    Expanded = bool.Parse(f.Array[1]) // this is useless for now, but keep the possibility open..
                }).ToList();

                for (int i = scintilla.Lines.Count - 1; i >= 0; i--)
                {
                    var save = saveValues.FirstOrDefault(f => f.Line == i);
                    if (save != null && !save.Expanded)
                    {
                        scintilla.Lines[i].FoldLine(FoldAction.Contract);
                    }
                    else
                    {
                        scintilla.Lines[i].FoldLine(FoldAction.Expand);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
            }
        }
    }
}
