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
using System.Collections.Generic;
using System.Threading.Tasks;
using VPKSoft.ErrorLogger;

namespace ScriptNotepad.UtilityClasses.CodeDom
{
    /// <summary>
    /// A class to run C# script snippets a file contents as line strings with line endings.
    /// </summary>
    /// <seealso cref="RoslynScriptRunner" />
    public class CsCodeDomScriptRunnerLines: IScriptRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsCodeDomScriptRunnerLines"/> class.
        /// </summary>
        public CsCodeDomScriptRunnerLines()
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

            ScriptCode = CSharpScriptBase;
        }

        /// <summary>
        /// Runs the C# script against the given lines and returns the concatenated lines as a string.
        /// <note type="note">The line strings may contain various different line endings.</note>
        /// </summary>
        /// <param name="fileLines">The file lines to run the C# script against.</param>
        /// <returns>A string containing the concatenated result of the lines manipulated as a string if the operation was successful; otherwise null.</returns>
        public async Task<string> ExecuteLinesAsync(List<string> fileLines)
        {
            try
            {
                CompileException = null;

                ScriptRunner = new RoslynScriptRunner(new RoslynGlobals<List<string>> {DataVariable = fileLines});

                await ScriptRunner.ExecuteAsync(ScriptCode);

                // try to run the C# script against the given file lines..
                object result = await ScriptRunner.ExecuteAsync("ManipulateLineLines.Evaluate(DataVariable)");

                if (result is Exception exception)
                {
                    throw exception;
                }

                CompileFailed = false;

                return result as string; // indicate a success..
            }
            catch (Exception ex)
            {
                CompileException = ScriptRunner?.PreviousCompileException;
                CompileFailed = true;
                ExceptionLogger.LogError(ex);
                // fail..
                return string.Concat(fileLines);
            }
        }

        /// <summary>
        /// Gets or sets the C# script runner.
        /// </summary>
        /// <value>The C# script runner.</value>
        public RoslynScriptRunner ScriptRunner { get; set; }

        /// <summary>
        /// Gets or sets the base "skeleton" C# code snippet for manipulating text.
        /// </summary>
        /// <value>The base "skeleton" C# code snippet for manipulating text.</value>
        public string CSharpScriptBase { get; set; }

        /// <summary>
        /// Gets or sets the C# script code.
        /// </summary>
        /// <value>The C# script code.</value>
        public string ScriptCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the script compile failed.
        /// </summary>
        /// <value><c>true</c> if the script compile failed; otherwise, <c>false</c>.</value>
        public bool CompileFailed { get; set; }

        /// <summary>
        /// Gets the previous compile exception.
        /// </summary>
        /// <value>The previous compile exception.</value>
        public Exception CompileException { get; set; }

        /// <summary>
        /// Executes the script.
        /// </summary>
        /// <param name="text">The text in some format.</param>
        /// <returns>The object containing the manipulated text if the operation was successful.</returns>
        public async Task<object> ExecuteScript(object text)
        {
            return await ExecuteLinesAsync((List<string>) text);
        }
    }
}
