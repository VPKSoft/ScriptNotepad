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
using System.CodeDom.Compiler;

namespace ScriptNotepad.UtilityClasses.CodeDom
{
    /// <summary>
    /// A class to run C# script snippets a file contents as line strings with line endings.
    /// </summary>
    public class CsCodeDomScriptRunnerParent
    {
        /// <summary>
        /// A CSharpCodeProvider class instance to translate the C# script code.
        /// </summary>
        private readonly CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider();

        /// <summary>
        /// The compiler parameters for the CSharpCodeProvider which runs the C# script code.
        /// </summary>
        private readonly CompilerParameters compilerParameters = new CompilerParameters();

        /// <summary>
        /// The results of a CSharpCodeProvider class instance to detect a warning or error.
        /// </summary>
        public CompilerResults CompilerResults { get; set; }

        /// <summary>
        /// Gets the base "skeleton" C# code snippet for manipulating text as lines.
        /// </summary>
        public string CSharpScriptBase { get; set; } = string.Empty;

        /// <summary>
        /// Gets a value indicating whether the script compile failed.
        /// </summary>
        public bool CompileFailed { get; private set; }

        /// <summary>
        /// The base C# script code for manipulating a collection of lines.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        internal string _ScriptCode;

        /// <summary>
        /// Gets or sets the C# script code.
        /// </summary>
        public string ScriptCode
        {
            get => _ScriptCode;

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
        /// <returns>The results of the script compilation.</returns>
        internal CompilerResults PreCompile()
        {
            try
            {
                // compile the C# script..
                CompilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, ScriptCode);


                // loop through the errors..
                foreach (CompilerError err in CompilerResults.Errors)
                {
                    if (!err.IsWarning) // only errors will indicate a failure..
                    {
                        CompileFailed = true; // ..so set the fail flag..
                        return CompilerResults; // ..at this point when an error has been detected it's useless to continue the loop..
                    }
                }

                // set the flag to indicate successful compilation..
                CompileFailed = false;
                return CompilerResults; // return the results of the compilation..
            }
            catch
            {
                // set the flag to indicate failed compilation..
                CompileFailed = true;
                return CompilerResults; // return the results of the compilation..
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsCodeDomScriptRunnerLines"/> class.
        /// </summary>
        public CsCodeDomScriptRunnerParent()
        {
            // set some flags for the CodeDom compiler..
            compilerParameters.GenerateExecutable = false;
            compilerParameters.GenerateInMemory = true;
            compilerParameters.IncludeDebugInformation = false;
            compilerParameters.TreatWarningsAsErrors = false;

            // add useful assemblies for text manipulation..
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Xml.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Linq.dll");
            compilerParameters.ReferencedAssemblies.Add("System.Xml.Linq.dll");
        }
    }
}
