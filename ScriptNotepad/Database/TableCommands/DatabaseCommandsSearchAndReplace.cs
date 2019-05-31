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
using ScriptNotepad.Database.Tables;
using ScriptNotepad.Database.UtilityClasses;

namespace ScriptNotepad.Database.TableCommands
{
    /// <summary>
    /// A class which is used to formulate SQL sentences for the <see cref="Database"/> class for the SEARCH_HISTORY and REPLACE_HISTORY database tables.
    /// Implements the <see cref="ScriptNotepad.Database.UtilityClasses.DataFormulationHelpers" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.Database.UtilityClasses.DataFormulationHelpers" />
    public class DatabaseCommandsSearchAndReplace: DataFormulationHelpers
    {
        /// <summary>
        /// Gets a database field name by a given table name for the database tables of SEARCH_HISTORY or REPLACE_HISTORY.
        /// </summary>
        /// <param name="tableName">Name of the database table.</param>
        /// <returns>System.String.</returns>
        private static string FieldNameByTableName(string tableName)
        {
            if (tableName == "SEARCH_HISTORY")
            {
                return "SEARCHTEXT";
            }

            if (tableName == "REPLACE_HISTORY")
            {
                return "REPLACETEXT";
            }

            return string.Empty;
        }

        /// <summary>
        /// Generates a SQL sentence to insert a search or a replace entry into the database.
        /// </summary>
        /// <param name="searchAndReplace">A SEARCH_AND_REPLACE_HISTORY class instance to be used for the SQL sentence generation.</param>
        /// <param name="tableName">The name of the table where the insert should be done.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenInsertSearchAndReplace(SEARCH_AND_REPLACE_HISTORY searchAndReplace, string tableName)
        {
            string sql =
                string.Join(Environment.NewLine,
                    $"INSERT INTO {tableName} ({FieldNameByTableName(tableName)}, CASE_SENSITIVE, TYPE, ADDED, SESSIONID) ",
                    $"SELECT {QS(searchAndReplace.SEARCH_OR_REPLACE_TEXT)},",
                    $"{BS(searchAndReplace.CASE_SENSITIVE)},",
                    $"{searchAndReplace.TYPE},",
                    $"{DateToDBString(DateTime.Now)},",
                    $"{DatabaseCommandsGeneral.GenSessionNameIDCondition(searchAndReplace.SESSIONNAME)}",
                    $"WHERE NOT EXISTS(SELECT * FROM {tableName} WHERE",
                    $"TYPE = {searchAndReplace.TYPE} AND {FieldNameByTableName(tableName)} = {QS(searchAndReplace.SEARCH_OR_REPLACE_TEXT)} AND SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(searchAndReplace.SESSIONNAME)});");

            return sql;
        }


        /// <summary>
        /// Generates an update sentence for update a search or a replace entry in the database.
        /// </summary>
        /// <param name="searchAndReplace">A SEARCH_AND_REPLACE_HISTORY class instance to be used for the SQL sentence generation.</param>
        /// <param name="tableName">The name of the table where the update should be done.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenInsertUpdateSearchAndReplace(SEARCH_AND_REPLACE_HISTORY searchAndReplace,
            string tableName)
        {
            string sql =
                string.Join(Environment.NewLine,
                    $"UPDATE {tableName} SET",
                    $"{FieldNameByTableName(tableName)} = {QS(searchAndReplace.SEARCH_OR_REPLACE_TEXT)},",
                    $"CASE_SENSITIVE = {BS(searchAndReplace.CASE_SENSITIVE)},",
                    $"TYPE = {searchAndReplace.TYPE},",
                    $"ADDED = {DateToDBString(DateTime.Now)}",
                    $"WHERE ID = {searchAndReplace.ID};");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to select search or replace entries from the database saved in the SEARCH_HISTORY or the REPLACE_HISTORY table.
        /// </summary>
        /// <param name="tableName">The name of the table where the results should be gotten from.</param>
        /// <param name="sessionName">A name of the session to which the search or the replace entries belong to.</param>
        /// <param name="maxCount">A maximum amount of search and/or replace entries to get.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenSearchAndReplaceSelect(string tableName, string sessionName, int maxCount)
        {
            string sql =
                string.Join(Environment.NewLine,
                    // ID: 0, TEXTFIELD: 1, CASE_SENSITIVE: 2, TYPE: 3, ADDED: 4, SESSIONID: 5, SESSIONNAME: 6
                    $"SELECT ID, {FieldNameByTableName(tableName)}, CASE_SENSITIVE, TYPE, ADDED, SESSIONID,",
                    $"{DatabaseCommandsGeneral.GenSessionNameNameCondition(sessionName)} AS SESSIONNAME",
                    $"FROM",
                    $"{tableName}",
                    $"WHERE",
                    $"SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)}",
                    $"ORDER BY ADDED DESC, {FieldNameByTableName(tableName)} COLLATE NOCASE",
                    $"LIMIT {maxCount};");

            return sql;
        }

        /// <summary>
        /// Gets the existing database search and/or replace identifier sentence.
        /// </summary>
        /// <param name="searchAndReplace">A SEARCH_AND_REPLACE_HISTORY class instance to be used for the SQL sentence generation.</param>
        /// <param name="tableName">The name of the table where the ID should be gotten from.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GetExistingDBSearchAndReplacIDSentence(SEARCH_AND_REPLACE_HISTORY searchAndReplace, string tableName)
        {
            string sql =
                string.Join(Environment.NewLine,
                    $"SELECT ID FROM {tableName}",
                    $"WHERE",
                    $"{DatabaseCommandsGeneral.GenSessionNameIDCondition(searchAndReplace.SESSIONNAME)} AND",
                    $"TYPE = {searchAndReplace.TYPE} AND {FieldNameByTableName(tableName)} = {QS(searchAndReplace.SEARCH_OR_REPLACE_TEXT)};");

            return sql;
        }

        /// <summary>Generates a sentence to delete older entries from the <see cref="T:ScriptNotepad.Database.Tables.SEARCH_AND_REPLACE_HISTORY"/> database table.</summary>
        /// <param name="tableName">The name of the table to generate the cleanup sentence to.</param>
        /// <param name="remainAmount">The remaining amount of entries that should be left untouched.</param>
        /// <param name="sessionName">A name of the session to which the search or the replace entries belong to.</param>
        /// <param name="types">Types of either search and/or replace history entries.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenDeleteOlderEntries(string tableName, int remainAmount, string sessionName, params int[] types)
        {
            string sql =
                string.Join(Environment.NewLine,
                    $"DELETE FROM {tableName} WHERE ID IN(",
                    $"SELECT ID FROM {tableName}",
                    $"WHERE SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)} AND TYPE IN ({string.Join(", ", types)})",
                    $"ORDER BY ADDED",
                    $"LIMIT",
                    $"CASE WHEN (SELECT COUNT(*) FROM {tableName} WHERE TYPE IN ({string.Join(", ", types)}) AND SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)}) - {remainAmount} > 0 THEN (SELECT COUNT(*) FROM {tableName} WHERE TYPE IN ({string.Join(", ", types)}) AND SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)}) - {remainAmount} ELSE 0 END);");

            return sql;
        }
    }
}
