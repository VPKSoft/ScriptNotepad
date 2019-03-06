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
using static ScriptNotepad.Database.DatabaseEnumerations;

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
            string existsCondition =
                string.Join(Environment.NewLine,
                $"WHERE NOT EXISTS(SELECT * FROM DBFILE_SAVE WHERE FILENAME_FULL = {QS(fileSave.FILENAME_FULL)} AND",
                $"SESSIONID = IFNULL((SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {QS(fileSave.SESSIONNAME)}), " +
                  "(SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = 'Default')))");

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
                $"IFNULL((SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {QS(fileSave.SESSIONNAME)}), (SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = 'Default')),",
                $"{QS(fileSave.ENCODING.WebName)}",
                existsCondition,
                $";");

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

        /// <summary>
        /// Generates a SQL sentence to insert a code snippet into the database.
        /// </summary>
        /// <param name="codeSnippet">A CODE_SNIPPETS class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenScriptInsertSentence(ref CODE_SNIPPETS codeSnippet)
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
        public static string GenScriptUpdateSentence(ref CODE_SNIPPETS codeSnippet)
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
        public static string GenScriptSelect()
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
        public static string GenScriptDelete(CODE_SNIPPETS codeSnippet)
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
        public static string GenCountReservedScripts(CODE_SNIPPETS codeSnippet, params string[] reservedNames)
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
                $"IFNULL((SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {QS(fileSave.SESSIONNAME)}), (SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = 'Default')) AND",
                $"FILENAME_FULL = {QS(fileSave.FILENAME_FULL)};");

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
        /// <param name="sessionName">A name of the session to which the history documents belong to.</param>
        /// <param name="maxCount">A maximum amount of file history to get.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenHistorySelect(string sessionName, int maxCount)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT ID, FILENAME_FULL, FILENAME,",
                $"FILEPATH, CLOSED_DATETIME, SESSIONID, REFERENCEID,",
                $"IFNULL((SELECT SESSIONNAME FROM SESSION_NAME WHERE SESSIONID = RECENT_FILES.SESSIONID), {QS(sessionName)}) AS SESSIONNAME",
                $"FROM RECENT_FILES",
                $"WHERE SESSIONID = IFNULL((SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {QS(sessionName)}), (SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = 'Default'))",
                $"AND NOT EXISTS(SELECT * FROM DBFILE_SAVE WHERE FILENAME_FULL = RECENT_FILES.FILENAME_FULL AND ISHISTORY = 0) ",
                $"ORDER BY CLOSED_DATETIME DESC",
                $"LIMIT {maxCount};");

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
                $"IFNULL((SELECT SESSIONNAME FROM SESSION_NAME WHERE SESSIONID = DBFILE_SAVE.SESSIONID), {QS(sessionName)}) AS SESSIONNAME,",
                $"FILESYS_SAVED, ENCODING",
                $"FROM DBFILE_SAVE",
                $"WHERE",
                $"SESSIONID = IFNULL((SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {QS(sessionName)}), (SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = 'Default'))",
                fileNameCondition,
                GetHistorySelectCondition(databaseHistoryFlag, "AND"),
                $"ORDER BY VISIBILITY_ORDER;");

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
                $"INSERT INTO RECENT_FILES(FILENAME_FULL, FILENAME, FILEPATH, CLOSED_DATETIME, SESSIONID, REFERENCEID)",
                $"SELECT",
                $"{QS(recentFile.FILENAME_FULL)},",
                $"{QS(recentFile.FILENAME)},",
                $"{QS(recentFile.FILEPATH)},",
                $"{DateToDBString(recentFile.CLOSED_DATETIME)},",
                $"IFNULL((SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {QS(recentFile.SESSIONNAME)}), (SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = 'Default')),",
                $"{(recentFile.REFERENCEID == null ? "NULL" : recentFile.REFERENCEID.ToString())}",
                $"WHERE NOT EXISTS(SELECT * FROM RECENT_FILES WHERE FILENAME_FULL = {QS(recentFile.FILENAME_FULL)});");

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
                $"REFERENCEID = {(recentFile.REFERENCEID == null ? "NULL" : recentFile.REFERENCEID.ToString())}",
                $"WHERE ID = {recentFile.ID};");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to localize the "Default" session name.
        /// </summary>
        /// <param name="name">The name for the "Default" session.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenLocalizeDefaultSessionName(string name)
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
        public static string GenGetNextIDForTable(string tableName)
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
        /// Generates a SQL sentence to get an ID for a given session name.
        /// </summary>
        /// <param name="sessionName">The name of the session which ID to get.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenGetCurrentSessionID(string sessionName)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT SESSIONID FROM SESSION_NAME WHERE SESSIONNAME = {QS(sessionName)};");
        
            return sql;
        }
    }
}
