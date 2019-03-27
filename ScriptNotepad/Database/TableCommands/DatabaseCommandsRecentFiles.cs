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
using System.Collections.Generic;

namespace ScriptNotepad.Database.TableCommands
{
    /// <summary>
    /// A class which is used to formulate SQL sentences for the <see cref="Database"/> class for the RECENT_FILES database table.
    /// </summary>
    public class DatabaseCommandsRecentFiles: DataFormulationHelpers
    {
        /// <summary>
        /// Gets the ID of the latest recent file insert from the database.
        /// </summary>
        /// <returns>A generated SQL sentence.</returns>
        public static string GenLatestDBRecentFileIDSentence()
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT IFNULL(MAX(ID), 1) FROM RECENT_FILES;");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to select file names saved in the RECENT_FILES table in the database.
        /// </summary>
        /// <param name="sessionName">A name of the session to which the history documents belongs to.</param>
        /// <param name="maxCount">A maximum amount of file history to get.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenHistorySelect(string sessionName, int maxCount)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT ID, FILENAME_FULL, FILENAME,",
                $"FILEPATH, CLOSED_DATETIME, SESSIONID, REFERENCEID,",
                $"{DatabaseCommandsGeneral.GenSessionNameNameCondition(sessionName)} AS SESSIONNAME,",
                $"CAST(CASE WHEN EXISTS(SELECT * FROM DBFILE_SAVE WHERE FILENAME_FULL = RECENT_FILES.FILENAME_FULL AND ISHISTORY = 1 AND SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)}) THEN 1 ELSE 0 END AS INTEGER) AS EXISTSINDB, ",
                $"ENCODING",
                $"FROM RECENT_FILES",
                $"WHERE SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)}",
                $"AND NOT EXISTS(SELECT * FROM DBFILE_SAVE WHERE FILENAME_FULL = RECENT_FILES.FILENAME_FULL AND ISHISTORY = 0 AND SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)}) ",
                $"ORDER BY CLOSED_DATETIME DESC",
                $"LIMIT {maxCount};");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to ID numbers saved in the RECENT_FILES table in the database.
        /// </summary>
        /// <param name="sessionName">A name of the session to which the history list belongs to.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenHistoryListSelect(string sessionName)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT ID, (SELECT COUNT(*) FROM RECENT_FILES WHERE SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)}) AS LIST_AMOUNT ",
                $"FROM",
                $"RECENT_FILES",
                $"WHERE",
                $"SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)}",
                $"ORDER BY CLOSED_DATETIME;");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to delete entries from the <see cref="RECENT_FILES"/> table with a given ID list.
        /// </summary>
        /// <param name="ids">A list of ID numbers to generate a SQL sentence to delete <see cref="RECENT_FILES"/> entries from the database.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenDeleteDBFileHistoryIDList(List<long> ids)
        {
            string deleteIDList = string.Join(", ", ids);
            string sql =
                string.Join(Environment.NewLine,
                $"DELETE FROM RECENT_FILES WHERE ID IN({deleteIDList});");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to insert a recent file into the database.
        /// </summary>
        /// <param name="recentFile">A recent file to insert into the database.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenHistoryInsert(RECENT_FILES recentFile)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"INSERT INTO RECENT_FILES(FILENAME_FULL, FILENAME, FILEPATH, CLOSED_DATETIME, SESSIONID, ENCODING, REFERENCEID)",
                $"SELECT",
                $"{QS(recentFile.FILENAME_FULL)},",
                $"{QS(recentFile.FILENAME)},",
                $"{QS(recentFile.FILEPATH)},",
                $"{DateToDBString(recentFile.CLOSED_DATETIME)},",
                $"{DatabaseCommandsGeneral.GenSessionNameIDCondition(recentFile.SESSIONNAME)},",
                $"{QS(recentFile.ENCODING.WebName)},",
                $"{(recentFile.REFERENCEID == null ? "NULL" : recentFile.REFERENCEID.ToString())}",
                $"WHERE NOT EXISTS(SELECT * FROM RECENT_FILES WHERE FILENAME_FULL = {QS(recentFile.FILENAME_FULL)} AND SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(recentFile.SESSIONNAME)});");

            return sql;
        }

        /// <summary>
        /// Gets the existing database recent file identifier sentence.
        /// </summary>
        /// <param name="recentFile">A RECENT_FILES class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GetExistingDBRecentFileIDSentence(RECENT_FILES recentFile)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT ID FROM RECENT_FILES",
                $"WHERE",
                $"{DatabaseCommandsGeneral.GenSessionNameIDCondition(recentFile.SESSIONNAME)} AND",
                $"FILENAME_FULL = {QS(recentFile.FILENAME_FULL)};");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to update a recent file on the database.
        /// </summary>
        /// <param name="recentFile">A recent file to be update on the database.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenHistoryUpdate(ref RECENT_FILES recentFile)
        {
            recentFile.CLOSED_DATETIME = DateTime.Now;

            string sql =
                string.Join(Environment.NewLine,
                $"UPDATE RECENT_FILES SET",
                $"FILENAME_FULL = {QS(recentFile.FILENAME_FULL)},",
                $"FILENAME = {QS(recentFile.FILENAME)},",
                $"FILEPATH = {QS(recentFile.FILEPATH)},",
                $"CLOSED_DATETIME = {DateToDBString(recentFile.CLOSED_DATETIME)},",
                $"SESSIONID = {recentFile.SESSIONID},",
                $"ENCODING = {QS(recentFile.ENCODING.WebName)},",
                $"REFERENCEID = {(recentFile.REFERENCEID == null ? "NULL" : recentFile.REFERENCEID.ToString())}",
                $"WHERE ID = {recentFile.ID};");

            return sql;
        }
    }
}
