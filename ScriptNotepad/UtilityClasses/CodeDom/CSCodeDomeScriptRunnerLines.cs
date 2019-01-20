#region License
/*
MIT License

Copyright(c) 2019 Petteri Kautonen

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

using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace ScriptNotepad.UtilityClasses.CodeDom
{
    /// <summary>
    /// A class to run C# script snippets a file contents as line strings with line endings.
    /// </summary>
    /// <seealso cref="CSCodeDOMScriptRunnerParent" />
    public class CSCodeDOMeScriptRunnerLines: CSCodeDOMScriptRunnerParent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSCodeDOMeScriptRunnerLines"/> class.
        /// </summary>
        public CSCodeDOMeScriptRunnerLines(): base()
        {
            CSharpScriptBase =
                string.Join(Environment.NewLine,
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
                    "public class ManipulateLineLines",
                    "{",
                    "    public static string Evaluate(List<string> fileLines)",
                    "    {",
                    "        string result = string.Empty;",
                    "        // insert code here..",
                    "        for (int i = 0; i < fileLines.Count; i++)",
                    "        {",
                    "           // A sample: fileLines[i] = fileLines[i]; // NOTE: Line manipulation must be added..",
                    "        }",
                    string.Empty,
                    "        result = string.Concat(fileLines); // concatenate the result lines.. ",
                    "        return result;",
                    "    }",
                    "}");

            _ScriptCode = CSharpScriptBase;

            // pre-compile the script's contents..
            PreCompile();
        }

        /// <summary>
        /// Runs the C# script against the given lines and returns the concatenated lines as a string.
        /// <note type="note">The line strings may contain various different line endings.</note>
        /// </summary>
        /// <param name="fileLines">The file lines to run the C# script against.</param>
        /// <returns>A string containing the concatenated result of the lines manipulated as a string if the operation was successful; otherwise null.</returns>
        public string ExecuteLines(List<string> fileLines)
        {
            try
            {
                // try to run the C# script against the given file lines..
                object result = CompilerResults.CompiledAssembly.GetType("ManipulateLineLines").GetMethod("Evaluate").Invoke(null, new object[] { fileLines });
                return result as string; // indicate a success..
            }
            catch
            {
                // fail..
                return null;
            }
        }
    }
}
