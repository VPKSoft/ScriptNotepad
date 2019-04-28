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

using ScriptNotepad.Database.UtilityClasses;

namespace ScriptNotepad.Database.TableCommands
{
    /// <summary>
    /// A class which is used to formulate SQL sentences for the <see cref="Database"/> class for general purpose commands.
    /// </summary>
    public class DatabaseCommandsGeneral: DataFormulationHelpers
    {
        /// <summary>
        /// Generates a SQL snippet to get the session ID by it's name.
        /// </summary>
        /// <param name="sessionName">Name of the session.</param>
        /// <returns>A generated SQL snippet based on the given parameters.</returns>
        public static string GenSessionNameIDCondition(string sessionName)
        {
            if (string.IsNullOrEmpty(sessionName))
            {
                return "NULL";
            }

            string sql =
                $"IFNULL((SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {QS(sessionName)}), (SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = 'Default'))";

            return sql;
        }

        /// <summary>
        /// Generates a SQL snippet to get the session ID by it's name accepting null values.
        /// </summary>
        /// <param name="sessionName">Name of the session.</param>
        /// <returns>A generated SQL snippet based on the given parameters.</returns>
        public static string GenSessionNameIDConditionNull(string sessionName)
        {
            if (string.IsNullOrEmpty(sessionName))
            {
                return "NULL";
            }

            string sql =
                $"(SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {NI(sessionName)}";

            return sql;
        }

        /// <summary>
        /// Generates a SQL snippet to get the session ID by it's name accepting null values with SQL equality (IS NULL or =).
        /// </summary>
        /// <param name="sessionName">Name of the session.</param>
        /// <returns>A generated SQL snippet based on the given parameters.</returns>
        public static string GenSessionNameIDConditionIsNull(string sessionName)
        {
            if (string.IsNullOrEmpty(sessionName))
            {
                return "IS NULL";
            }

            string sql =
                $"= (SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {NI(sessionName)}";

            return sql;
        }


        /// <summary>
        /// Generates a SQL sentence to get a session ID by it's name.
        /// </summary>
        /// <param name="sessionName">Name of the session.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenSessionNameIDSelect(string sessionName)
        {
            string sql =
                $"SELECT IFNULL((SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {QS(sessionName)}), (SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = 'Default'))";

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to get a session ID by it's name accepting database NULL.
        /// </summary>
        /// <param name="sessionName">Name of the session.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenSessionNameIdSelectNull(string sessionName)
        {
            string sql =
                $"SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME {NIS(sessionName)}";

            return sql;
        }


        /// <summary>
        /// Generates a SQL snippet to get the session name by it's ID.
        /// </summary>
        /// <param name="sessionID">The ID of the session.</param>
        /// <returns>A generated SQL snippet based on the given parameters.</returns>
        public static string GenSessionIDNameCondition(int sessionID)
        {
            string sql =
                $"IFNULL((SELECT SESSIONNAME FROM SESSION_NAME WHERE SESSIONID = {sessionID}), (SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = 'Default'))";

            return sql;
        }

        /// <summary>
        /// Generates a SQL snippet to get the session name by it's name.
        /// </summary>
        /// <param name="sessionName">Name of the session.</param>
        /// <returns>A generated SQL snippet based on the given parameters.</returns>
        public static string GenSessionNameNameCondition(string sessionName)
        {
            string sql =
                $"IFNULL((SELECT SESSIONNAME FROM SESSION_NAME WHERE SESSIONNAME = {QS(sessionName)}), (SELECT SESSIONNAME FROM SESSION_NAME WHERE SESSIONNAME = 'Default'))";

            return sql;
        }
    }
}
