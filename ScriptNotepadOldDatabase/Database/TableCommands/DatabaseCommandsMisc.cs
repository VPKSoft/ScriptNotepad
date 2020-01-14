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
using ScriptNotepadOldDatabase.Database.UtilityClasses;

namespace ScriptNotepadOldDatabase.Database.TableCommands
{
    /// <summary>
    /// A class which is used to formulate SQL sentences for the <see cref="ScriptNotepad.Database"/> class with miscellaneous commands.
    /// </summary>
    internal class DatabaseCommandsMisc: DataFormulationHelpers
    {
        /// <summary>
        /// Generates a SQL sentence to localize the "Default" session name.
        /// </summary>
        /// <param name="name">The name for the "Default" session.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenLocalizeDefaultSessionName(string name)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"UPDATE SESSION_NAME SET SESSIONNAME = {QS(name)} WHERE SESSIONID = 1;");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to get the next ID for an auto-increment table.
        /// </summary>
        /// <param name="tableName">Name of the table of which next ID number to get.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenGetNextIDForTable(string tableName)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT",
                $"CASE",
                $"  WHEN(EXISTS(SELECT SEQ FROM SQLITE_SEQUENCE WHERE NAME = {QS(tableName)})) THEN",
                $"    (SELECT SEQ + 1 FROM SQLITE_SEQUENCE WHERE NAME = {QS(tableName)})",
                $"  ELSE 1",
                $"END");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to get the most recent ID for an auto-increment table.
        /// </summary>
        /// <param name="tableName">Name of the table of which next ID number to get.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenGetCurrentIDForTable(string tableName)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT",
                $"CASE",
                $"  WHEN(EXISTS(SELECT SEQ FROM SQLITE_SEQUENCE WHERE NAME = {QS(tableName)})) THEN",
                $"    (SELECT SEQ FROM SQLITE_SEQUENCE WHERE NAME = {QS(tableName)})",
                $"  ELSE 1",
                $"END");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to get an ID for a given session name.
        /// </summary>
        /// <param name="sessionName">The name of the session which ID to get.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenGetCurrentSessionID(string sessionName)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {QS(sessionName)};");

            return sql;
        }
    }
}
