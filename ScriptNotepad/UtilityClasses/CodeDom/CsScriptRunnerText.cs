﻿#region License
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScriptNotepad.UtilityClasses.ScintillaHelpers;
using VPKSoft.ErrorLogger;

namespace ScriptNotepad.UtilityClasses.CodeDom
{
    public class CsScriptRunnerText
    {
        // a CodeDOM provider for executing C# scripts for a list of lines..
        private static readonly CsCodeDomScriptRunnerLines ScriptRunnerLines = new ();

        // a CodeDOM provider for executing C# scripts for a string..
        private static readonly CsCodeDomScriptRunnerText ScriptRunnerText = new ();

        public static async Task<KeyValuePair<string, bool>> RunScriptText(string code, string text)
        {
            try
            {
                ScriptRunnerText.ScriptCode = code;

                // a reference to a Scintilla document was gotten so do run the code..
                return new KeyValuePair<string, bool>(await ScriptRunnerText.ExecuteText(text), true);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError(ex);
            }

            return new KeyValuePair<string, bool>(string.Empty, false);
        }

        public static async Task<KeyValuePair<string, bool>> RunScriptLines(string code, List<string> lines)
        {
            try
            {
                ScriptRunnerText.ScriptCode = code;

                // a reference to a Scintilla document was gotten so do run the code..
                return new KeyValuePair<string, bool>((await ScriptRunnerLines.ExecuteScript(lines))?.ToString() ?? string.Empty, true);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError(ex);
            }

            return new KeyValuePair<string, bool>(string.Empty, false);
        }
    }
}
