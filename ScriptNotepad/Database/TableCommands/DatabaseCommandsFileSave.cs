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
using System.IO;
using static ScriptNotepad.Database.DatabaseEnumerations;

namespace ScriptNotepad.Database.TableCommands
{
    /// <summary>
    /// A class which is used to formulate SQL sentences for the <see cref="Database"/> class for the DBFILE_SAVE database table.
    /// </summary>
    public class DatabaseCommandsFileSave: DataFormulationHelpers
    {
        /// <summary>
        /// Gets the ID of the latest file insert from the database.
        /// </summary>
        /// <returns>A generated SQL sentence.</returns>
        public static string GenLatestDBFileSaveIDSentence()
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT IFNULL(MAX(ID), 1) FROM DBFILE_SAVE;");

            return sql;
        }

        /// <summary>
        /// Gets the existing database file save identifier sentence.
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GetExistingDBFileSaveIDSentence(DBFILE_SAVE fileSave)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT ID FROM DBFILE_SAVE",
                $"WHERE",
                $"{DatabaseCommandsGeneral.GenSessionNameIDCondition(fileSave.SESSIONNAME)} AND",
                $"FILENAME_FULL = {QS(fileSave.FILENAME_FULL)};");

            return sql;
        }

        /// <summary>
        /// Gets a database select condition based on the <paramref name="databaseHistoryFlag"/>.
        /// </summary>
        /// <param name="databaseHistoryFlag">An enumeration indicating how to behave with the <see cref="DBFILE_SAVE"/> class ISHISTORY flag.</param>
        /// <param name="prefix">A prefix string to add to the result if the condition is a combined one (i.e. AND).</param>
        /// <returns>A string containing a database select condition based on the <paramref name="databaseHistoryFlag"/>.</returns>
        private static string GetHistorySelectCondition(DatabaseHistoryFlag databaseHistoryFlag, string prefix)
        {
            switch (databaseHistoryFlag)
            {
                case DatabaseHistoryFlag.IsHistory:
                    return prefix + " ISHISTORY = 1";
                case DatabaseHistoryFlag.NotHistory:
                    return prefix + " ISHISTORY = 0";
                case DatabaseHistoryFlag.DontCare:
                    return string.Empty;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Generates a SQL sentence to select the oldest history files in the database belonging to the given <paramref name="sessionName"/> session.
        /// </summary>
        /// <param name="sessionName">A name of the session to which the history documents belong to.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenHistoryCleanupListSelect(string sessionName)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT ID, (SELECT COUNT(*) FROM DBFILE_SAVE WHERE",
                $"SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)} AND ISHISTORY = 1) AS HISTORY_AMOUNT",
                $"FROM DBFILE_SAVE",
                $"WHERE",
                $"SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)} AND",
                $"ISHISTORY = 1",
                $"ORDER BY DB_MODIFIED;");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to delete entries from the <see cref="DBFILE_SAVE"/> table with a given ID list.
        /// </summary>
        /// <param name="ids">A list of ID numbers to generate a SQL sentence to delete <see cref="DBFILE_SAVE"/> entries from the database.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenDeleteDBFileSaveIDList(List<long> ids)
        {
            string deleteIDList = string.Join(", ", ids);
            string sql =
                string.Join(Environment.NewLine,
                $"DELETE FROM DBFILE_SAVE WHERE ID IN({deleteIDList});");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to select document snapshots from the DBFILE_SAVE table in the database.
        /// </summary>
        /// <param name="sessionName">Name of the session with the saved file snapshots belong to.</param>
        /// <param name="databaseHistoryFlag">An enumeration indicating how to behave with the <see cref="DBFILE_SAVE"/> class ISHISTORY flag.</param>
        /// <param name="fileNameFull">A file name in case of a single file is being queried from the database.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenDocumentSelect(string sessionName, DatabaseHistoryFlag databaseHistoryFlag, string fileNameFull = "")
        {
            string fileNameCondition = fileNameFull == string.Empty ? string.Empty :
                $"AND FILENAME_FULL = {QS(fileNameFull)}";

            string sql =
                string.Join(Environment.NewLine,
                $"SELECT ID, EXISTS_INFILESYS, FILENAME_FULL, FILENAME, FILEPATH,",
                $"FILESYS_MODIFIED, DB_MODIFIED, LEXER_CODE, FILE_CONTENTS,",
                $"VISIBILITY_ORDER, SESSIONID, ISACTIVE, ISHISTORY,",
                $"{DatabaseCommandsGeneral.GenSessionNameNameCondition(sessionName)} AS SESSIONNAME,",
                $"FILESYS_SAVED, ENCODING",
                $"FROM DBFILE_SAVE",
                $"WHERE",
                $"SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(sessionName)}",
                fileNameCondition,
                GetHistorySelectCondition(databaseHistoryFlag, "AND"),
                $"ORDER BY VISIBILITY_ORDER;");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to insert a file into the database.
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenInsertFileSentence(DBFILE_SAVE fileSave)
        {
            string existsCondition =
                string.Join(Environment.NewLine,
                $"WHERE NOT EXISTS(SELECT * FROM DBFILE_SAVE WHERE FILENAME_FULL = {QS(fileSave.FILENAME_FULL)} AND",
                $"SESSIONID = {DatabaseCommandsGeneral.GenSessionNameIDCondition(fileSave.SESSIONNAME)})");

            if (fileSave.ID != -1)
            {
                existsCondition = $"WHERE NOT EXISTS(SELECT * FROM DBFILE_SAVE WHERE ID = {fileSave.ID});";
            }

            string sql =
                string.Join(Environment.NewLine,
                $"INSERT INTO DBFILE_SAVE (EXISTS_INFILESYS, FILENAME_FULL, FILENAME, FILEPATH, FILESYS_MODIFIED, ",
                $"FILESYS_SAVED, DB_MODIFIED, LEXER_CODE, FILE_CONTENTS, VISIBILITY_ORDER, ISACTIVE, ISHISTORY, SESSIONID, ENCODING) ",
                $"SELECT {BS(fileSave.EXISTS_INFILESYS)},",
                $"{QS(fileSave.FILENAME_FULL)},",
                $"{QS(fileSave.FILENAME)},",
                $"{QS(fileSave.FILEPATH)},",
                $"{DateToDBString(fileSave.FILESYS_MODIFIED)},",
                $"{DateToDBString(fileSave.FILESYS_SAVED)},",
                $"{DateToDBString(fileSave.DB_MODIFIED)},",
                $"{(int)fileSave.LEXER_CODE}, @FILE,",
                $"{fileSave.VISIBILITY_ORDER},",
                $"{BS(fileSave.ISACTIVE)},",
                $"{BS(fileSave.ISHISTORY)},",
                $"{DatabaseCommandsGeneral.GenSessionNameIDCondition(fileSave.SESSIONNAME)},",
                $"{QS(fileSave.ENCODING.WebName)}",
                existsCondition,
                $";");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to update a file's ISHISTORY flag in the database.
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenUpdateFileHistoryFlag(DBFILE_SAVE fileSave)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"UPDATE DBFILE_SAVE SET ISHISTORY = {fileSave.ISHISTORY} WHERE ID = {fileSave.ID};");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to update a file in the database.
        /// </summary>
        /// <param name="fileSave">A reference to a DBFILE_SAVE class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenUpdateFileSentence(ref DBFILE_SAVE fileSave)
        {
            fileSave.FILESYS_MODIFIED = File.Exists(fileSave.FILENAME_FULL) ? new FileInfo(fileSave.FILENAME_FULL).LastWriteTime : DateTime.MinValue;
            // NOT HERE: fileSave.DB_MODIFIED = DateTime.Now;
            fileSave.EXISTS_INFILESYS = File.Exists(fileSave.FILENAME_FULL);

            string sql =
                string.Join(Environment.NewLine,
                $"UPDATE DBFILE_SAVE SET",
                $"EXISTS_INFILESYS = {BS(fileSave.EXISTS_INFILESYS)},",
                $"FILENAME_FULL = {QS(fileSave.FILENAME_FULL)}, ",
                $"FILENAME = {QS(fileSave.FILENAME)},",
                $"FILEPATH = {QS(fileSave.FILEPATH)},",
                $"FILESYS_MODIFIED = {DateToDBString(fileSave.FILESYS_MODIFIED)},",
                $"FILESYS_SAVED = {DateToDBString(fileSave.FILESYS_SAVED)},",
                $"DB_MODIFIED = {DateToDBString(fileSave.DB_MODIFIED)},",
                $"LEXER_CODE = {(int)fileSave.LEXER_CODE},",
                $"FILE_CONTENTS = @FILE,",
                $"VISIBILITY_ORDER = {fileSave.VISIBILITY_ORDER},",
                $"ISACTIVE = {BS(fileSave.ISACTIVE)},",
                $"ISHISTORY = {BS(fileSave.ISHISTORY)},",
                $"SESSIONID = {fileSave.SESSIONID},",
                $"ENCODING = {QS(fileSave.ENCODING.WebName)}",
                $"WHERE ID = {fileSave.ID};");

            return sql;
        }
    }
}
