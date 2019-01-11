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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ScriptNotepad.Database.Database;

namespace ScriptNotepad.Database
{
    /// <summary>
    /// A class which is used to formulate SQL sentences for the <see cref="Database"/> class.
    /// </summary>
    public static class DatabaseCommands
    {
        /// <summary>
        /// Generates a SQL sentence to insert a file into the database.
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenInsertFileSentence(DBFILE_SAVE fileSave)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"INSERT INTO DBFILE_SAVE (EXISTS_INFILESYS, FILENAME_FULL, FILENAME, FILEPATH, FILESYS_MODIFIED, ",
                $"DB_MODIFIED, LEXER_CODE, FILE_CONTENTS, VISIBILITY_ORDER, ISACTIVE, ISHISTORY, SESSIONID) ",
                $"SELECT {BS(fileSave.EXISTS_INFILESYS)},",
                $"{QS(fileSave.FILENAME_FULL)},",
                $"{QS(fileSave.FILENAME)},",
                $"{QS(fileSave.FILEPATH)},",
                $"{DateToDBString(fileSave.FILESYS_MODIFIED)},",
                $"{DateToDBString(fileSave.DB_MODIFIED)},",
                $"{(int)fileSave.LEXER_CODE}, @FILE,",
                $"{fileSave.VISIBILITY_ORDER},",
                $"{BS(fileSave.ISACTIVE)},",
                $"{BS(fileSave.ISHISTORY)},",
                $"IFNULL((SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {QS(fileSave.SESSIONNAME)}), (SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = 'Default'))",
                $"WHERE NOT EXISTS(SELECT * FROM DBFILE_SAVE WHERE ID = {fileSave.ID});");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to update a file in the database.
        /// </summary>
        /// <param name="fileSave">A reference to a DBFILE_SAVE class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenUpdateFileSentence(ref DBFILE_SAVE fileSave)
        {
            fileSave.FILESYS_MODIFIED = File.Exists(fileSave.FILENAME_FULL) ? new FileInfo(fileSave.FILENAME_FULL).LastWriteTimeUtc : DateTime.MinValue;
            fileSave.DB_MODIFIED = DateTime.Now;
            fileSave.EXISTS_INFILESYS = File.Exists(fileSave.FILENAME_FULL);

            string sql =
                string.Join(Environment.NewLine,
                $"UPDATE DBFILE_SAVE SET",
                $"EXISTS_INFILESYS = {BS(fileSave.EXISTS_INFILESYS)},",
                $"FILENAME_FULL = {QS(fileSave.FILENAME_FULL)}, ",
                $"FILENAME = {QS(fileSave.FILENAME)},",
                $"FILEPATH = {QS(fileSave.FILEPATH)},",
                $"FILESYS_MODIFIED = {DateToDBString(fileSave.FILESYS_MODIFIED)},",
                $"DB_MODIFIED = {DateToDBString(fileSave.DB_MODIFIED)},",
                $"LEXER_CODE = {(int)fileSave.LEXER_CODE},",
                $"FILE_CONTENTS = @FILE,",
                $"VISIBILITY_ORDER = {fileSave.VISIBILITY_ORDER},",
                $"ISACTIVE = {BS(fileSave.ISACTIVE)},",
                $"ISHISTORY = {BS(fileSave.ISHISTORY)},",
                $"SESSIONID = {fileSave.SESSIONID}",
                $"WHERE ID = {fileSave.ID};");

            return sql;
        }

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
        /// <param name="maxCount">A maximum amount of file history to get.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenHistorySelect(int maxCount)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT ID, FILENAME_FULL, FILENAME,",
                $"FILEPATH, CLOSED_DATETIME, REFERENCEID",
                $"FROM RECENT_FILES",
                $"ORDER BY CLOSED_DATETIME DESC",
                $"LIMIT {maxCount};");

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
                $"INSERT INTO RECENT_FILES(FILENAME_FULL, FILENAME, FILEPATH, CLOSED_DATETIME, REFERENCEID)",
                $"SELECT",
                $"{QS(recentFile.FILENAME_FULL)},",
                $"{QS(recentFile.FILENAME)},",
                $"{QS(recentFile.FILEPATH)},",
                $"{DateToDBString(recentFile.CLOSED_DATETIME)},",
                $"{(recentFile.REFERENCEID == null ? "NULL" : recentFile.REFERENCEID.ToString())}",
                $"WHERE NOT EXISTS(SELECT * FROM RECENT_FILES WHERE ID = {recentFile.ID});");

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
                $"REFERENCEID = {(recentFile.REFERENCEID == null ? "NULL" : recentFile.REFERENCEID.ToString())}",
                $"WHERE ID = {recentFile.ID};");

            return sql;
        }
    }
}
