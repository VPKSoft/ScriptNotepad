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
using ScriptNotepadOldDatabase.Database.Tables;
using ScriptNotepadOldDatabase.Database.UtilityClasses;

namespace ScriptNotepadOldDatabase.Database.TableCommands
{
    /// <summary>
    /// A class which is used to formulate SQL sentences for the <see cref="Database"/> class for the CODE_SNIPPETS database table.
    /// </summary>
    internal class DatabaseCommandsCodeSnippets: DataFormulationHelpers
    {
        /// <summary>
        /// Generates a SQL sentence to insert a code snippet into the database.
        /// </summary>
        /// <param name="codeSnippet">A CODE_SNIPPETS class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenScriptInsertSentence(ref CODE_SNIPPETS codeSnippet)
        {
            codeSnippet.MODIFIED = DateTime.Now;

            string sql =
                string.Join(Environment.NewLine,
                $"INSERT INTO CODE_SNIPPETS (SCRIPT_CONTENTS, SCRIPT_NAME, MODIFIED, SCRIPT_TYPE, SCRIPT_LANGUAGE) ",
                $"SELECT {QS(codeSnippet.SCRIPT_CONTENTS)},",
                $"{QS(codeSnippet.SCRIPT_NAME)},",
                $"{DateToDBString(codeSnippet.MODIFIED)},",
                $"{codeSnippet.SCRIPT_TYPE},",
                $"{codeSnippet.SCRIPT_LANGUAGE}",
                $"WHERE NOT EXISTS(SELECT * FROM CODE_SNIPPETS WHERE ID = {codeSnippet.ID} AND",
                $"SCRIPT_NAME = {QS(codeSnippet.SCRIPT_NAME)} COLLATE NOCASE);");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to update an existing code snippet into the database.
        /// </summary>
        /// <param name="codeSnippet">A CODE_SNIPPETS class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenScriptUpdateSentence(ref CODE_SNIPPETS codeSnippet)
        {
            codeSnippet.MODIFIED = DateTime.Now;

            string sql =
                string.Join(Environment.NewLine,
                $"UPDATE CODE_SNIPPETS SET",
                $"SCRIPT_CONTENTS = {QS(codeSnippet.SCRIPT_CONTENTS)},",
                $"SCRIPT_NAME = {QS(codeSnippet.SCRIPT_NAME)}, ",
                $"MODIFIED = {DateToDBString(codeSnippet.MODIFIED)},",
                $"SCRIPT_TYPE = {codeSnippet.SCRIPT_TYPE},",
                $"SCRIPT_LANGUAGE = {codeSnippet.SCRIPT_LANGUAGE}",
                $"WHERE ID = {codeSnippet.ID};");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to select code snippets from the CODE_SNIPPETS table in the database.
        /// </summary>
        /// <returns>A generated SQL sentence.</returns>
        internal static string GenScriptSelect()
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT ID, SCRIPT_CONTENTS, SCRIPT_NAME,",
                $"MODIFIED, SCRIPT_TYPE, SCRIPT_LANGUAGE",
                $"FROM CODE_SNIPPETS",
                $"ORDER BY SCRIPT_TYPE, SCRIPT_NAME COLLATE NOCASE, MODIFIED");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to delete code snippet from the CODE_SNIPPETS table in the database.
        /// </summary>
        /// <param name="codeSnippet">The code snippet to delete from the database.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenScriptDelete(CODE_SNIPPETS codeSnippet)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"DELETE",
                $"FROM CODE_SNIPPETS",
                $"WHERE ID = {codeSnippet.ID}");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to the get the count of reserved names in the <paramref name="reservedNames"/>.
        /// </summary>
        /// <param name="codeSnippet">A CODE_SNIPPETS class instance of which ID field is to be used for the SQL sentence generation.</param>
        /// <param name="reservedNames">A list of reserved names in for the script snippets in the database.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenCountReservedScripts(CODE_SNIPPETS codeSnippet, params string[] reservedNames)
        {
            // if not null..
            if (reservedNames != null)
            {
                // ..quote the reserved name list so it can be used in an SQL in list..
                for (int i = 0; i < reservedNames.Length; i++)
                {
                    reservedNames[i] = QS(reservedNames[i]);
                }
            }

            string sql =
                string.Join(Environment.NewLine,
                $"SELECT COUNT(*) AS RESERVED_SCRIPTS",
                $"FROM CODE_SNIPPETS",
                $"WHERE SCRIPT_NAME IN ({string.Join(", ", reservedNames)}) AND ID = {codeSnippet.ID}");

            return sql;
        }
    }
}
