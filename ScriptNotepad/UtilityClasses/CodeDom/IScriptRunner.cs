using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
