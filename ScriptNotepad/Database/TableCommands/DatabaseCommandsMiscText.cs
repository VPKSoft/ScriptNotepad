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
    /// A class which is used to formulate SQL sentences for the <see cref="Database"/> class for the MISCTEXT_LIST database table.
    /// Implements the <see cref="ScriptNotepad.Database.UtilityClasses.DataFormulationHelpers" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.Database.UtilityClasses.DataFormulationHelpers" />
    public class DatabaseCommandsMiscText: DataFormulationHelpers
    {
        /// <summary>
        /// Generates a SQL sentence to insert a an entry to the <see cref="MISCTEXT_LIST"/> database table.
        /// </summary>
        /// <param name="miscText">A MISCTEXT_LIST class instance to be used for the SQL sentence generation.</param>
        /// <param name="sessionName">A name of the session to which the misc text entry belongs to.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenInsertMiscText(MISCTEXT_LIST miscText, string sessionName = null)
        {
            string sql =
                string.Join(Environment.NewLine,
                    $"INSERT INTO MISCTEXT_LIST (TEXTVALUE, TYPE, ADDED, SESSIONID) ",
                    $"SELECT {QS(miscText.TEXTVALUE)},",
                    $"{(int)miscText.TYPE},",
                    $"{DateToDBString(DateTime.Now)},",
                    $"{DatabaseCommandsGeneral.GenSessionNameIDConditionNull(sessionName)}",
                    $"WHERE NOT EXISTS(SELECT * FROM MISCTEXT_LIST WHERE",
                    $"TYPE = {(int)miscText.TYPE} AND TEXTVALUE = {QS(miscText.TEXTVALUE)});");

            return sql;
        }

        /// <summary>
        /// Generates an update sentence for update a a misc text entry into the database.
        /// </summary>
        /// <param name="miscText">A MISCTEXT_LIST class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenUpdateMiscText(MISCTEXT_LIST miscText)
        {
            string sql =
                string.Join(Environment.NewLine,
                    $"UPDATE MISCTEXT_LIST SET",
                    $"TEXTVALUE = {QS(miscText.TEXTVALUE)},",
                    $"TYPE = {(int)miscText.TYPE},",
                    $"ADDED = {DateToDBString(DateTime.Now)},",
                    $"SESSIONID = {NI(miscText.SESSIONID)}",
                    $"WHERE ID = {miscText.ID};");

            return sql;
        }

        /// <summary>
        /// Generates a select sentence to select entries from the <see cref="MISCTEXT_LIST"/> database table.
        /// </summary>
        /// <param name="textType">Type of the text.</param>
        /// <param name="maxCount">Maximum count of misc text entries to return.</param>
        /// <param name="sessionName">A name of the session to which the misc text entry belongs to.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenSelectMiscText(MiscTextType textType, int maxCount, string sessionName = null)
        {
            string sql =
                string.Join(Environment.NewLine,
                    // ID: 0, TEXTVALUE: 1, TYPE: 2, ADDED: 3, SESSIONID: 4, SESSIONNAME: 5
                    $"SELECT ID, TEXTVALUE, TYPE, ADDED, SESSIONID,",
                    $"{DatabaseCommandsGeneral.GenSessionNameIDConditionNull(sessionName)} AS SESSIONNAME",
                    $"FROM MISCTEXT_LIST",
                    $"WHERE",
                    $"TYPE = {(int)textType} AND SESSIONID {DatabaseCommandsGeneral.GenSessionNameIDConditionIsNull(sessionName)}",
                    $"ORDER BY ADDED DESC, TEXTVALUE COLLATE NOCASE",
                    $"LIMIT {maxCount};");

            return sql;
        }

        /// <summary>
        /// Generates a select sentence to delete older entries from the <see cref="MISCTEXT_LIST"/> database table.
        /// </summary>
        /// <param name="textType">Type of the text the text entries belong to.</param>
        /// <param name="remainAmount">The remaining amount of entries that should be left untouched.</param>
        /// <param name="sessionName">A name of the session to which the misc text entries belongs to.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenDeleteOlderEntries(MiscTextType textType, int remainAmount, string sessionName = null)
        {
            string sql =
                string.Join(Environment.NewLine,
                    $"DELETE FROM MISCTEXT_LIST WHERE TYPE = {(int)textType} AND ID IN(",
                    $"SELECT ID FROM MISCTEXT_LIST",
                    $"ORDER BY ADDED, TEXTVALUE COLLATE NOCASE",
                    $"LIMIT",
                    $"CASE WHEN (SELECT COUNT(*) FROM MISCTEXT_LIST WHERE TYPE = {(int)textType}) - {remainAmount} > 0 THEN (SELECT COUNT(*) FROM MISCTEXT_LIST WHERE TYPE = {(int)textType}) - {remainAmount} ELSE 0 END);");

            return sql;
        }

        /// <summary>
        /// Gets the existing database misc text identifier sentence.
        /// </summary>
        /// <param name="miscText">A MISCTEXT_LIST class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        // ReSharper disable once InconsistentNaming
        public static string GetExistingDBMiscTextIDSentence(MISCTEXT_LIST miscText)
        {
            string sql =
                string.Join(Environment.NewLine,
                    $"SELECT ID FROM MISCTEXT_LIST",
                    $"WHERE",
                    $"{DatabaseCommandsGeneral.GenSessionNameIDConditionIsNull(miscText.SESSIONNAME)} AND",
                    $"TYPE = {(int)miscText.TYPE} AND TEXTVALUE = {QS(miscText.TEXTVALUE)};");

            return sql;
        }

    }
}
