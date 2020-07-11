#region License
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
using VPKSoft.ErrorLogger;

namespace ScriptNotepad.UtilityClasses.CodeDom
{
    /// <summary>
    /// A class to run C# script snippets against the contents of a Scintilla document as text.
    /// </summary>
    /// <seealso cref="CsCodeDomScriptRunnerParent" />
    public class CsCodeDomScriptRunnerText: CsCodeDomScriptRunnerParent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsCodeDomScriptRunnerText"/> class.
        /// </summary>
        public CsCodeDomScriptRunnerText()
        {
            CSharpScriptBase =
            string.Join(Environment.NewLine,
                // ReSharper disable once StringLiteralTypo
                "#region Usings",
                "using System;",
                "using System.Linq;",
                "using System.Collections;",
                "using System.Collections.Generic;",
                "using System.Text;",
                "using System.Text.RegularExpressions;",
                "using System.Xml.Linq;",
                "#endregion",
                Environment.NewLine,
                "public class ManipulateText",
                "{",
                "    public static string Evaluate(string fileContents)",
                "    {",
                "        // insert code here..",
                "        // A sample: fileContents = fileContents.Replace(\"some_text\", \"another_text\");",
                "        return fileContents;",
                "    }",
                "}");

             _ScriptCode = CSharpScriptBase;

            // pre-compile the script's contents..
            PreCompile();
        }

        /// <summary>
        /// Runs the C# script against the given string containing lines and returns the result as a string.
        /// <note type="note">The string may contain various different line endings.</note>
        /// </summary>
        /// <param name="fileContents">The file contents to run the C# script against.</param>
        /// <returns>A string containing the result as a string of the given manipulated string if the operation was successful; otherwise null.</returns>
        public string ExecuteText(string fileContents)
        {
            try
            {
                // try to run the C# script against the given file contents..
                object result = CompilerResults.CompiledAssembly.GetType("ManipulateText").GetMethod("Evaluate")
                    ?.Invoke(null, new object[] {fileContents});
                return result as string; // indicate a success..
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError(ex);
                return fileContents; // fail..
            }
        }
    }
}
