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
    class CSCodeDomeScriptRunnerLines
    {
        /// <summary>
        /// A CSharpCodeProvider class instance to translate the C# script code.
        /// </summary>
        private CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider();

        /// <summary>
        /// The compiler parameters for the CSharpCodeProvider which runs the C# script code.
        /// </summary>
        private CompilerParameters compilerParameters = new CompilerParameters();

        /// <summary>
        /// The results of a CSharpCodeProvider class instance to detect a warning or error.
        /// </summary>
        private CompilerResults CompilerResults = null;

        /// <summary>
        /// Gets the base "skeleton" C# code snippet for manipulating text as lines.
        /// </summary>
        public static string CSharpScriptBaseLines { get; private set; } =
            string.Join(Environment.NewLine,
                "using System;",
                "using System.Linq;",
                "using System.Collections;",
                "using System.Collections.Generic;",
                "using System.Text;",
                "using System.Text.RegularExpressions;",
                "using System.Xml.Linq;",
                Environment.NewLine,
                "public class ManipulateLineLines",
                "{",
                "    public static string Evaluate(List<string> fileLines)",
                "    {",
                "        string result = string.Empty;",
                "        // insert code here..",
                "        result = string.Concat(fileLines); // concatenate the result lines.. ",
                "        return result;",
                "    }",
                "}");

        /// <summary>
        /// Gets a value indicating whether the script compile failed.
        /// </summary>
        public bool CompileFailed { get; private set; } = false;

        /// <summary>
        /// The base C# script code for manipulating a collection of lines.
        /// </summary>
        private string _ScriptCode = CSharpScriptBaseLines;

        /// <summary>
        /// Gets or sets the C# script code.
        /// </summary>
        public string ScriptCode
        {
            get
            {
                // just return the value..
                return _ScriptCode;
            }

            set
            {
                // set the value..
                _ScriptCode = value;

                // when a value is set pre-compile the script's contents..
                PreCompile();
            }
        }

        /// <summary>
        /// Pre-compiles the <see cref="ScriptCode"/> and sets the <see cref="CompileFailed"/> property value to indicate whether the compilation was a success.
        /// </summary>
        private void PreCompile()
        {
            try
            {
                // compile the C# script..
                CompilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, new string[] { ScriptCode });

                // loop through the errors..
                foreach (CompilerError err in CompilerResults.Errors)
                {
                    if (!err.IsWarning) // only errors will indicate a failure..
                    {
                        CompileFailed = true; // ..so set the fail flag..
                        return; // ..at this point when an error has been detected it's useless to continue the loop..
                    }
                }

                // set the flag to indicate successful compilation..
                CompileFailed = false;
            }
            catch
            {
                // set the flag to indicate failed compilation..
                CompileFailed = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSCodeDomeScriptRunnerLines"/> class.
        /// </summary>
        public CSCodeDomeScriptRunnerLines()
        {
            // set some flags for the CodeDom compiler..
            compilerParameters.GenerateExecutable = false;
            compilerParameters.GenerateInMemory = false;
            compilerParameters.IncludeDebugInformation = false;
            compilerParameters.TreatWarningsAsErrors = false;

            // add useful assemblies for text manipulation..
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Linq.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Xml.Linq.dll");

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
