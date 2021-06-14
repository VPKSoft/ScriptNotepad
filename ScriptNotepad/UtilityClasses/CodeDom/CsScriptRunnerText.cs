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
