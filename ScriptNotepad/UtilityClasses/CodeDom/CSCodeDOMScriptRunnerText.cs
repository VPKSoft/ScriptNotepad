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

using System.Collections.Generic;
using System.Threading.Tasks;
using VPKSoft.ErrorLogger;

namespace ScriptNotepad.UtilityClasses.CodeDom;

/// <summary>
/// A class to run C# script snippets against the contents of a Scintilla document as text.
/// </summary>
/// <seealso cref="RoslynScriptRunner" />
public class CsCodeDomScriptRunnerText: IScriptRunner
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

        ScriptCode = CSharpScriptBase;
    }

    /// <summary>
    /// Evaluates the C# script.
    /// </summary>
    /// <param name="fileContents">The file contents to run the C# script against.</param>
    /// <returns>A <see cref="KeyValuePair{TKey,TValue}"/> containing the file contents after the script manipulation and a boolean value indicating whether the script execution succeeded.</returns>
    public async Task<bool> Evaluate(string fileContents)
    {
        try
        {
            ScriptRunner = new RoslynScriptRunner(new RoslynGlobals<string> { DataVariable = fileContents, });

            var result = await ScriptRunner.ExecuteAsync(ScriptCode);

            if (result is Exception exceptionEvaluate)
            {
                throw exceptionEvaluate;
            }

            CompileFailed = false;

            return true; // indicate a success..
        }
        catch (Exception ex)
        {
            CompileException = ScriptRunner?.PreviousCompileException;
            CompileFailed = true;
            ExceptionLogger.LogError(ex);
            return false; // fail..
        }
    }

    /// <summary>
    /// Runs the C# script against the given string containing lines and returns the result as a string.
    /// <note type="note">The string may contain various different line endings.</note>
    /// </summary>
    /// <param name="fileContents">The file contents to run the C# script against.</param>
    /// <returns>A <see cref="KeyValuePair{TKey,TValue}"/> containing the file contents after the script manipulation and a boolean value indicating whether the script execution succeeded.</returns>
    public async Task<KeyValuePair<string, bool>> ExecuteText(string fileContents)
    {
        try
        {
            CompileException = null;

            // try to run the C# script against the given file contents..
            object result = await ScriptRunner.ExecuteAsync("ManipulateText.Evaluate(DataVariable)");
                    
            if (result is Exception exception)
            {
                throw exception; 
            }

            CompileFailed = false;

            return new KeyValuePair<string, bool>(result as string, true); // indicate a success..
        }
        catch (Exception ex)
        {
            CompileException = ScriptRunner?.PreviousCompileException;
            CompileFailed = true;
            ExceptionLogger.LogError(ex);
            return new KeyValuePair<string, bool>(fileContents, false); // fail..
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

    private string scriptCode = string.Empty;

    /// <summary>
    /// Gets or sets the C# script code.
    /// </summary>
    /// <value>The C# script code.</value>
    public string ScriptCode
    {
        get => scriptCode;
        set
        {
            scriptCode = value;
            CompileFailed = !Evaluate(scriptCode).Result;
        }
    }

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
        return await ExecuteText((string)text);
    }
}