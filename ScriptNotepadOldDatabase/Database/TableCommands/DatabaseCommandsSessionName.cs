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

using System;
using ScriptNotepadOldDatabase.Database.Tables;
using ScriptNotepadOldDatabase.Database.UtilityClasses;

namespace ScriptNotepadOldDatabase.Database.TableCommands
{
    /// <summary>
    /// A class which is used to formulate SQL sentences for the <see cref="ScriptNotepad.Database"/> class for the SESSION_NAME database table.
    /// </summary>
    /// <seealso cref="DataFormulationHelpers" />
    internal class DatabaseCommandsSessionName: DataFormulationHelpers
    {
        /// <summary>
        /// Generates a SQL sentence to select sessions from the database.
        /// </summary>
        /// <returns>A generated SQL sentence.</returns>
        internal static string GenSessionSelect()
        {
            // SESSIONID: 0, SESSIONNAME: 1
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT SESSIONID, SESSIONNAME",
                $"FROM",
                $"SESSION_NAME",
                $"ORDER BY (CASE WHEN SESSIONID = 1 THEN 1 ELSE 0 END) DESC, SESSIONNAME COLLATE NOCASE;");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to delete a session from the database.
        /// </summary>
        /// <param name="session">The session to delete.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenDeleteSession(SESSION_NAME session)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"DELETE",
                $"FROM",
                $"SESSION_NAME",
                $"WHERE SESSIONID = {session.SESSIONID};");

            return sql;
        }


        /// <summary>
        /// Generates a SQL sentence to update the default session name in the database.
        /// </summary>
        /// <param name="name">The new name for the default session.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenUpdateDefaultSessionName(string name)
        {
            return GenUpdateSessionName(name, 1);
        }

        /// <summary>
        /// Generates a SQL sentence to update the session name in the database.
        /// </summary>
        /// <param name="name">The new name for the session.</param>
        /// <param name="ID">The ID (SESSIONID) number for the session.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenUpdateSessionName(string name, long ID)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"UPDATE",
                $"SESSION_NAME",
                $"SET SESSIONNAME = {QS(name)}",
                $"WHERE SESSIONID = {ID} AND",
                // prevent multiple sessions with the same name..
                $"NOT EXISTS(SELECT * FROM SESSION_NAME WHERE SESSIONNAME = {QS(name)} AND SESSIONID <> {ID});");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to insert a session name in to the database.
        /// </summary>
        /// <param name="name">The name for the new session.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenInsertSessionName(string name)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"INSERT INTO SESSION_NAME(SESSIONNAME)",
                $"SELECT {QS(name)}",
                $"WHERE",
                // prevent multiple sessions with the same name..
                $"NOT EXISTS(SELECT * FROM SESSION_NAME WHERE SESSIONNAME = {QS(name)});");

            return sql;
        }
    }
}
