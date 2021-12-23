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

using System.Threading.Tasks;

namespace ScriptNotepad.UtilityClasses.CodeDom
{
    /// <summary>
    /// An interface for the script runner classes.
    /// </summary>
    public interface IScriptRunner
    {
        /// <summary>
        /// Gets or sets the C# script runner.
        /// </summary>
        /// <value>The C# script runner.</value>
        public RoslynScriptRunner ScriptRunner { get; set; }

        /// <summary>
        /// Gets or sets the base "skeleton" C# code snippet for manipulating text.
        /// </summary>
        /// <value>The base "skeleton" C# code snippet for manipulating text.</value>
        string CSharpScriptBase { get; set; }

        /// <summary>
        /// Gets or sets the C# script code.
        /// </summary>
        /// <value>The C# script code.</value>
        string ScriptCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the script compile failed.
        /// </summary>
        /// <value><c>true</c> if the script compile failed; otherwise, <c>false</c>.</value>
        bool CompileFailed { get; set; }

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
        Task<object> ExecuteScript(object text);
    }
}
