#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

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

#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace ScriptNotepad.UtilityClasses.CodeDom;

/// <summary>
/// A class to run C# scripts with Roslyn.
/// </summary>
public class RoslynScriptRunner
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoslynScriptRunner"/> class.
    /// </summary>
    public RoslynScriptRunner()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RoslynScriptRunner"/> class.
    /// </summary>
    /// <param name="globalValue">The globals object value. This can not be used within a class.</param>
    public RoslynScriptRunner(object globalValue)
    {
        GlobalValue = globalValue;
        GlobalValueType = globalValue.GetType();
    }

    /// <summary>
    /// Gets the globals value for the script.
    /// </summary>
    /// <value>The globals value for the script.</value>
    private object? GlobalValue { get; }

    /// <summary>
    /// Gets the type of the globals value.
    /// </summary>
    /// <value>The type of the globals value.</value>
    private Type? GlobalValueType { get; }

    /// <summary>
    /// Gets or sets the state of the script.
    /// </summary>
    /// <value>The state of the script.</value>
    private ScriptState? ScriptState { get; set; }

    /// <summary>
    /// Gets the <see cref="ScriptOptions"/> options.
    /// </summary>
    /// <value>The <see cref="ScriptOptions"/> options.</value>
    private ScriptOptions Options { get; } = ScriptOptions.Default
        .AddReferences(typeof(object).Assembly,
            typeof(Thread).Assembly,
            typeof(Task).Assembly,
            typeof(List<>).Assembly,
            typeof(Regex).Assembly,
            typeof(StringBuilder).Assembly,
            typeof(Uri).Assembly,
            typeof(Enumerable).Assembly,
            typeof(IEnumerable).Assembly,
            typeof(Path).Assembly,
            typeof(System.Reflection.Assembly).Assembly,
            typeof(System.Text.RegularExpressions.Regex).Assembly,
            typeof(System.Linq.Enumerable).Assembly,
            typeof(XmlDocument).Assembly,
            typeof(XDocument).Assembly);

    /// <summary>
    /// Executes C# script as an asynchronous operation.
    /// </summary>
    /// <param name="code">The code to either append to the previous or script or to evaluate.</param>
    /// <returns>System.Nullable&lt;System.Object&gt; containing the script result value or an exception object if one occurred.</returns>
    public async Task<object?> ExecuteAsync(string code)
    {
        // success, no exceptions..
        PreviousCompileException = null;

        try
        {
            ScriptState = ScriptState == null
                ? await CSharpScript.RunAsync(code, Options, GlobalValue, GlobalValueType)
                : await ScriptState.ContinueWithAsync(code, Options);
        }
        catch (Exception exception)
        {
            // save the exception for further analysis..
            PreviousCompileException = exception;

            // return the exception from the CSharpScript..
            return exception;
        }

        if (ScriptState?.ReturnValue != null && !string.IsNullOrEmpty(ScriptState.ReturnValue.ToString()))
        {
            var result = ScriptState.ReturnValue;
            // reset the script state after a result is gotten..
            ScriptState = null;

            return result;
        }

        return null;
    }

    /// <summary>
    /// Gets the previous compile exception.
    /// </summary>
    /// <value>The previous compile exception.</value>
    public Exception? PreviousCompileException { get; private set; }
}