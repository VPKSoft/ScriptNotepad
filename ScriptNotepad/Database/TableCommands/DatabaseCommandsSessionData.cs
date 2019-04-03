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

using ScriptNotepad.Database.Tables;
using ScriptNotepad.Database.UtilityClasses;
using System;

namespace ScriptNotepad.Database.TableCommands
{
    /// <summary>
    /// A class to help generate SQL sentences base to delete session related data from the database.
    /// </summary>
    /// <seealso cref="ScriptNotepad.Database.UtilityClasses.DataFormulationHelpers" />
    public class DatabaseCommandsSessionData: DataFormulationHelpers
    {
        /// <summary>
        /// Generates a SQL sentence to delete a session data from the <see cref="RECENT_FILES"/> table from the database.
        /// </summary>
        /// <param name="session">The session of which data to delete from the database.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenDeleteSessionDataHistory(SESSION_NAME session)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"DELETE",
                $"FROM",
                $"RECENT_FILES",
                $"WHERE SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(session.SESSIONNAME)};");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to delete a session data from the <see cref="DBFILE_SAVE"/> table from the database.
        /// </summary>
        /// <param name="session">The session of which data to delete from the database.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenDeleteSessionDataData(SESSION_NAME session)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"DELETE",
                $"FROM",
                $"DBFILE_SAVE",
                $"WHERE SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(session.SESSIONNAME)};");

            return sql;
        }
    }
}
