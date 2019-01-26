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
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPKSoft.ScintillaLexers;
using VPKSoft.ScintillaTabbedTextControl;
using ScriptNotepad.UtilityClasses.StreamHelpers;

namespace ScriptNotepad.Database
{
    /// <summary>
    /// A static class the handle the SQLite database.
    /// </summary>
    public static class Database
    {
        // the database connection to use..
        private static SQLiteConnection conn = null;

        /// <summary>
        /// This method stands for Quoted String. Simply double-quote the "insides" of a string and add quotes to the both sides (').
        /// </summary>
        /// <param name="str">A string to 'quote'.</param>
        /// <returns>A 'quoted' string.</returns>
        public static string QS(string str)
        {
            return "'" + str.Replace("'", "''") + "'"; // as simple as it can be..
        }

        /// <summary>
        /// Converts a boolean value to database-understandable format.
        /// </summary>
        /// <param name="boolean">A boolean value to a database-understandable format.</param>
        /// <returns>If <paramref name="boolean"/> is true then 1; otherwise 0.</returns>
        public static string BS(bool boolean)
        {
            return boolean ? "1" : "0";
        }

        /// <summary>
        /// Creates a new SQLiteConnection class for the Database class with the given connection string.
        /// </summary>
        /// <param name="connectionString">A connection string to create a SQLite database connection.</param>
        public static void InitConnection(string connectionString)
        {
            conn = new SQLiteConnection(connectionString); // create a new SQLiteConnection class instance..
            conn.Open();
        }

        /// <summary>
        /// Gets a DateTime value from a give string from the database.
        /// </summary>
        /// <param name="value">The date and time value as it's stored in to the database.</param>
        /// <returns>A DateTime value converted from a given string.</returns>
        public static DateTime DateFromDBString(string value)
        {
            try
            {
                // try to parse the given date time string to a DateTime value and return it..
                DateTime result = DateTime.ParseExact(value, "yyyy-MM-dd HH':'mm':'ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

                return result;
            }
            catch
            {
                // the format was invalid, so return DateTime.MinValue..
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converts a given DateTime value to a quoted string.
        /// </summary>
        /// <param name="dateTime">The DateTime value to convert.</param>
        /// <returns>A quoted string converted from a given DateTime value.</returns>
        public static string DateToDBString(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                // return "nothing" if the date and time value is either
                // the minimum value or the maximum value..
                return QS("0000-00-00 00:00:00.000");
            }
            else
            {
                // return a quoted string from the given the date and time value..

                string result = QS(dateTime.ToString("yyyy-MM-dd HH':'mm':'ss.fff", CultureInfo.InvariantCulture));
                return result;
            }
        }

        /// <summary>
        /// Adds a given DBFILE_SAVE class instance into the database cache.
        /// <note type="note">In case of an exception the <see cref="LastException"/> value is set to indicate the exception.</note>
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be added into the database.</param>
        /// <returns>A DBFILE_SAVE class instance file was successfully added to the database; otherwise null.</returns>
        public static DBFILE_SAVE AddFile(DBFILE_SAVE fileSave)
        {
            try
            {
                long lastId = GetScalar<long>(DatabaseCommands.GenLatestDBFileSaveIDSentence());

                string sql = DatabaseCommands.GenInsertFileSentence(fileSave);

                fileSave.FILESYS_MODIFIED = File.Exists(fileSave.FILENAME_FULL) ?
                    new FileInfo(fileSave.FILENAME_FULL).LastWriteTime :
                    DateTime.MinValue;

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;
                    // add parameters to the command..

                    // add the contents of the Scintilla.NET document as a parameter..
                    command.Parameters.Add("@FILE", System.Data.DbType.Binary).Value = fileSave.FILE_CONTENTS.ToArray();

                    // do the insert..
                    command.ExecuteNonQuery();
                }

                long newId = GetScalar<long>(DatabaseCommands.GenLatestDBFileSaveIDSentence());

                fileSave.ID = lastId != newId ? newId : fileSave.ID;

                return fileSave;
            }
            catch (Exception ex)
            {
                LastException = ex; // log the exception..
                return null;
            }
        }

        /// <summary>
        /// Adds a given file into the database cache.
        /// <note type="note">In case of an exception the <see cref="LastException"/> value is set to indicate the exception.</note>
        /// </summary>
        /// <param name="document">An instance to a ScintillaTabbedDocument class.</param>
        /// <param name="sessionName">A name of the session to which the document should be saved to.</param>
        /// <param name="isHistory"> a value indicating whether this entry is a history entry.</param>
        /// <param name="ID">An unique identifier for the file.</param>
        /// <returns>A DBFILE_SAVE class instance file was successfully added to the database; otherwise null.</returns>
        public static DBFILE_SAVE AddFile(ScintillaTabbedDocument document, bool isHistory, string sessionName, int ID = -1)
        {
            try
            {
                DBFILE_SAVE fileSave = new DBFILE_SAVE()
                {
                    ID = document.ID,
                    EXISTS_INFILESYS = File.Exists(document.FileName),
                    FILENAME_FULL = document.FileName,
                    FILENAME = Path.GetFileName(document.FileName),
                    FILEPATH = Path.GetDirectoryName(document.FileName),
                    FILESYS_MODIFIED = File.Exists(document.FileName) ?
                        new FileInfo(document.FileName).LastWriteTime :
                        DateTime.MinValue,
                    DB_MODIFIED = DateTime.Now,
                    LEXER_CODE = document.LexerType,
                    FILE_CONTENTS = StreamStringHelpers.TextToMemoryStream(document.Scintilla.Text),
                    VISIBILITY_ORDER = (int)document.FileTabButton.Tag,
                    SESSIONNAME = sessionName,
                    ISACTIVE = document.FileTabButton.IsActive
                };

                return AddFile(fileSave);
            }
            catch (Exception ex)
            {
                LastException = ex; // log the exception..
                return null;
            }
        }

        /// <summary>
        /// Adds or updates a a given file into the database cache.
        /// </summary>
        /// <param name="document">An instance to a ScintillaTabbedDocument class.</param>
        /// <param name="sessionName">A name of the session to which the document should be saved to.</param>
        /// <param name="isHistory"> a value indicating whether this entry is a history entry.</param>
        /// <param name="ID">An unique identifier for the file.</param>
        /// <returns>An instance to a DBFILE_SAVE class if the operations was successful; otherwise null;</returns>
        public static DBFILE_SAVE AddOrUpdateFile(ScintillaTabbedDocument document, bool isHistory, string sessionName, int ID = -1)
        {
            return UpdateFile(AddFile(document, isHistory, sessionName, ID));
        }

        /// <summary>
        /// Adds or updates a a given file into the database cache.
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be added or updated into the database.</param>
        /// <param name="document">An instance to a ScintillaTabbedDocument class.</param>
        /// <returns>An instance to a DBFILE_SAVE class if the operations was successful; otherwise null;</returns>
        public static DBFILE_SAVE AddOrUpdateFile(DBFILE_SAVE fileSave, ScintillaTabbedDocument document)
        {
            fileSave.FILE_CONTENTS = StreamStringHelpers.TextToMemoryStream(document.Scintilla.Text);
            return UpdateFile(AddFile(fileSave));
        }

        /// <summary>
        /// Adds a given code snippet into the database.
        /// </summary>
        /// <param name="codeSnippet">A CODE_SNIPPETS class instance to be added into the database.</param>
        /// <returns>A CODE_SNIPPETS class instance file was successfully added to the database; otherwise null.</returns>
        public static CODE_SNIPPETS AddCodeSnippet(CODE_SNIPPETS codeSnippet)
        {
            try
            {
                // generate a SQL sentence for the insert..
                string sql = DatabaseCommands.GenScriptInsertSentence(ref codeSnippet);

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    // do the insert..
                    command.ExecuteNonQuery();
                }

                return codeSnippet;
            }
            catch (Exception ex)
            {
                LastException = ex; // log the exception..
                return null;
            }
        }

        /// <summary>
        /// Updates a recent CODE_SNIPPETS class instance into the database table CODE_SNIPPETS.
        /// </summary>
        /// <param name="codeSnippet">A CODE_SNIPPETS class instance to update to the database's CODE_SNIPPETS table.</param>
        /// <returns>The updated instance of the given <paramref name="codeSnippet"/> class instance if the operation was successful; otherwise null.</returns>
        public static CODE_SNIPPETS UpdateCodeSnippet(CODE_SNIPPETS codeSnippet)
        {
            try
            {
                // generate a SQL sentence for the insert..
                string sql = DatabaseCommands.GenScriptUpdateSentence(ref codeSnippet);

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    // do the insert..
                    command.ExecuteNonQuery();
                }

                return codeSnippet;
            }
            catch (Exception ex)
            {
                LastException = ex; // log the exception..
                return null;
            }
        }

        /// <summary>
        /// Adds or updates a a given code snippet into the database.
        /// </summary>
        /// <param name="codeSnippet">A CODE_SNIPPETS class instance to be added or updated into the database.</param>
        /// <returns>An instance to a CODE_SNIPPETS class if the operations was successful; otherwise null;</returns>
        public static CODE_SNIPPETS AddOrUpdateCodeSnippet(CODE_SNIPPETS codeSnippet)
        {
            return UpdateCodeSnippet(AddCodeSnippet(codeSnippet));
        }

        /// <summary>
        /// Deletes the given code snippet from the database.
        /// </summary>
        /// <param name="codeSnippet">The code snippet to delete from the database.</param>
        /// <returns>True if the snippet was successfully delete from the database; otherwise false.</returns>
        public static bool DeleteCodeSnippet(CODE_SNIPPETS codeSnippet)
        {
            try
            {
                // generate a SQL sentence for the deletion..
                string sql = DatabaseCommands.GenScriptDelete(codeSnippet);

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    // do the deletion..
                    command.ExecuteNonQuery();
                }

                // success..
                return true;
            }
            catch (Exception ex)
            {
                LastException = ex; // log the exception..
                return false; // fail..
            }
        }

        /// <summary>
        /// Adds a <paramref name="fileName"/> file to the database table RECENT_FILES if it doesn't exists.
        /// </summary>
        /// <param name="fileName">A file name to generate a RECENT_FILES class instance.</param>
        /// <param name="sessionName">A name of the session to which the document belongs to.</param>
        /// <returns>A RECENT_FILES class instance if the recent file was successfully added to the database; otherwise null.</returns>
        public static RECENT_FILES AddRecentFile(string fileName, string sessionName)
        {
            return AddRecentFile(RECENT_FILES.FromFilename(fileName), sessionName);
        }

        /// <summary>
        /// Adds a given RECENT_FILES class instance to the RECENT_FILES database table.
        /// </summary>
        /// <param name="recentFile">A RECENT_FILES class instance to add to the database's RECENT_FILES table.</param>
        /// <param name="sessionName">A name of the session to which the document belongs to.</param>
        /// <returns>A RECENT_FILES class instance if the recent file was successfully added to the database; otherwise null.</returns>
        public static RECENT_FILES AddRecentFile(RECENT_FILES recentFile, string sessionName)
        {
            try
            {
                long lastId = GetScalar<long>(DatabaseCommands.GenLatestDBRecentFileIDSentence());

                recentFile.SESSIONNAME = sessionName;

                string sql = DatabaseCommands.GenHistoryInsert(recentFile);

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;

                    // do the insert..
                    command.ExecuteNonQuery();
                }

                long newId = GetScalar<long>(DatabaseCommands.GenLatestDBRecentFileIDSentence());

                recentFile.ID = lastId != newId ? newId : -1;

                return recentFile;
            }
            catch (Exception ex)
            {
                LastException = ex; // log the exception..
                return null;
            }
        }

        /// <summary>
        /// Updates a recent RECENT_FILES class instance into the database table RECENT_FILES.
        /// </summary>
        /// <param name="recentFile">A RECENT_FILES class instance to update to the database's RECENT_FILES table.</param>
        /// <param name="sessionName">A name of the session to which the document belongs to.</param>
        /// <returns>The updated instance of the given <paramref name="recentFile"/> class instance if the operation was successful; otherwise null.</returns>
        public static RECENT_FILES UpdateRecentFile(RECENT_FILES recentFile, string sessionName)
        {
            recentFile.SESSIONNAME = sessionName;
            if (ExecuteArbitrarySQL(DatabaseCommands.GenHistoryUpdate(ref recentFile)))
            {
                return recentFile;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds or updates a <paramref name="fileName"/> file to the database table RECENT_FILES.
        /// </summary>
        /// <param name="fileName">A file name to generate a RECENT_FILES class instance.</param>
        /// <param name="sessionName">A name of the session to which the document belongs to.</param>
        /// <returns>True if the operations was successful; otherwise false;</returns>
        public static bool AddOrUpdateRecentFile(string fileName, string sessionName)
        {
            return UpdateRecentFile(AddRecentFile(fileName, sessionName), sessionName) != null;
        }

        /// <summary>
        /// Gets the last exception of a SQL sentence gone wrong.
        /// </summary>
        public static Exception LastException { get; private set; } = null;

        /// <summary>
        /// Updates a given file to the database cache.
        /// <note type="note">In case of an exception the <see cref="LastException"/> value is set to indicate the exception.</note>
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be updated into the database.</param>
        /// <returns>A modified instance of the DBFILE_SAVE if the operation was successful; otherwise null;</returns>
        public static DBFILE_SAVE UpdateFile(DBFILE_SAVE fileSave)
        {
            string sql = DatabaseCommands.GenUpdateFileSentence(ref fileSave);
            try
            {
                fileSave.FILESYS_MODIFIED = File.Exists(fileSave.FILENAME_FULL) ?
                    new FileInfo(fileSave.FILENAME_FULL).LastWriteTime :
                    DateTime.MinValue;

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;
                    // add parameters to the command..

                    // add the contents of the Scintilla.NET document as a parameter..
                    command.Parameters.Add("@FILE", DbType.Binary).Value = fileSave.FILE_CONTENTS.ToArray();

                    // do the insert..
                    command.ExecuteNonQuery();
                }
                return fileSave;
            }
            catch (Exception ex)
            {
                LastException = ex; // log the exception..
                return null;
            }
        }

        /// <summary>
        /// Executes a scalar SQL sentence against the database.
        /// <note type="note">In case of an exception the <see cref="LastException"/> value is set to indicate the exception.</note>
        /// </summary>
        /// <typeparam name="T">The return type of the scalar SQL sentence.</typeparam>
        /// <param name="sql">A scalar SQL sentence to be executed against the database.</param>
        /// <returns>The value of type T if the operation was successful; otherwise a default value of T is returned.</returns>
        public static T GetScalar<T>(string sql)
        {
            try
            {
                // execute a scalar SQL sentence against the database..
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;

                    // ..and return the value casted into a typeof(T)..
                    return (T)command.ExecuteScalar(); 
                }
            }
            catch (Exception ex)
            {
                LastException = ex; // log the exception..
                // failed, return default(T).. 
                return default(T);
            }
        }

        /// <summary>
        /// Executes a arbitrary SQL into the database.
        /// </summary>
        /// <param name="sql">A string containing SQL sentences to be executed to the database.</param>
        /// <returns>True if the given SQL sentences were executed successfully; otherwise false;</returns>
        public static bool ExecuteArbitrarySQL(string sql)
        {
            // as the SQLiteCommand is disposable a using clause is required..
            using (SQLiteCommand command = new SQLiteCommand(sql, conn))
            {
                try
                {
                    // try to execute the given SQL..
                    command.ExecuteNonQuery();
                    return true; // success..
                }
                catch (Exception ex) // something went wrong so do log the reason.. (ex avoids the EventArgs e in all cases!)..
                {
                    LastException = ex; // log the exception..
                    return false; // failure..
                }
            }
        }

        /// <summary>
        /// Gets the recent file list saved to the database.
        /// </summary>
        /// <param name="sessionName">A name of the session to which the history documents belong to.</param>
        /// <param name="maxCount">Maximum count of recent file entries to return.</param>
        /// <returns>A collection RECENT_FILES classes.</returns>
        public static IEnumerable<RECENT_FILES> GetRecentFiles(string sessionName, int maxCount = 15)
        {
            List<RECENT_FILES> result = new List<RECENT_FILES>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommands.GenHistorySelect(sessionName, maxCount), conn))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // ID: 0, FILENAME_FULL: 1, FILENAME: 2, FILEPATH: 3, CLOSED_DATETIME: 4, SESSIONID: 5, REFERENCEID: 6, SESSION_NAME: 7
                    while (reader.Read())
                    {
                        result.Add(
                            new RECENT_FILES()
                            {
                                ID = reader.GetInt64(0),
                                FILENAME_FULL = reader.GetString(1),
                                FILENAME = reader.GetString(2),
                                FILEPATH = reader.GetString(3),
                                CLOSED_DATETIME = DateFromDBString(reader.GetString(4)),
                                SESSIONID = reader.GetInt32(5),
                                REFERENCEID = reader.IsDBNull(6) ? null : (long?)reader.GetInt64(6),
                                SESSIONNAME = reader.GetString(7),
                            });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets all the code snippets from the database.
        /// </summary>
        /// <returns>A collection CODE_SNIPPETS classes.</returns>
        public static IEnumerable<CODE_SNIPPETS> GetCodeSnippets()
        {
            List<CODE_SNIPPETS> result = new List<CODE_SNIPPETS>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommands.GenScriptSelect(), conn))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // ID: 0, SCRIPT_CONTENTS: 1, SCRIPT_NAME: 2, MODIFIED: 3, SCRIPT_TYPE: 4, SCRIPT_LANGUAGE: 5
                    while (reader.Read())
                    {
                        result.Add(
                            new CODE_SNIPPETS()
                            {
                                ID = reader.GetInt64(0),
                                SCRIPT_CONTENTS = reader.GetString(1),
                                SCRIPT_NAME = reader.GetString(2),
                                MODIFIED = DateFromDBString(reader.GetString(3)),
                                SCRIPT_TYPE = reader.GetInt32(4),
                                SCRIPT_LANGUAGE = reader.GetInt32(5),
                            });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a memory stream from the given SQLiteBlob.
        /// </summary>
        /// <param name="blob">A SQLiteBlob to create a memory stream from.</param>
        /// <returns>A memory stream created from the given <paramref name="blob"/>.</returns>
        public static MemoryStream MemoryStreamFromBlob(SQLiteBlob blob)
        {
            int size = blob.GetCount();
            byte[] blobBytes = new byte[size];
            blob.Read(blobBytes, size, 0);
            return new MemoryStream(blobBytes); // remember to dispose of this..
        }

        /// <summary>
        /// Gets the file snapshots from the database.
        /// </summary>
        /// <param name="sessionName">Name of the session with the saved file snapshots belong to.</param>
        /// <param name="isHistory">An indicator whether to select documents marked as history.</param>
        /// <returns>A collection of DBFILE_SAVE class instances matching the given parameters.</returns>
        public static IEnumerable<DBFILE_SAVE> GetFilesFromDatabase(string sessionName, bool isHistory)
        {
            List<DBFILE_SAVE> result = new List<DBFILE_SAVE>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommands.GenDocumentSelect(sessionName, isHistory), conn))
            {
                                                                       // can't get the BLOB without this (?!)..
                using (SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (reader.Read())
                    {
                        try
                        {
                            // ID: 0, EXISTS_INFILESYS: 1, FILENAME_FULL: 2, FILENAME: 3, FILEPATH: 4,
                            // FILESYS_MODIFIED: 5, DB_MODIFIED: 6, LEXER_CODE: 7, FILE_CONTENTS: 8,
                            // VISIBILITY_ORDER: 9, SESSIONID: 10, ISACTIVE: 11, ISHISTORY: 12, SESSIONNAME: 13
                            // FILESYS_SAVED: 14
                            result.Add(
                                new DBFILE_SAVE()
                                {
                                    ID = reader.GetInt64(0),
                                    EXISTS_INFILESYS = reader.GetInt32(1) == 1,
                                    FILENAME_FULL = reader.GetString(2),
                                    FILENAME = reader.GetString(3),
                                    FILEPATH = reader.GetString(4),
                                    FILESYS_MODIFIED = DateFromDBString(reader.GetString(5)),
                                    DB_MODIFIED = DateFromDBString(reader.GetString(6)),
                                    LEXER_CODE = (LexerType)reader.GetInt32(7), // cast to a lexer type..
                                    FILE_CONTENTS = MemoryStreamFromBlob(reader.GetBlob(8, true)),
                                    VISIBILITY_ORDER = reader.GetInt32(9),
                                    SESSIONID = reader.GetInt32(10),
                                    ISACTIVE = reader.GetInt32(11) == 1,
                                    ISHISTORY = reader.GetInt32(12) == 1,
                                    SESSIONNAME = reader.GetString(13),
                                    FILESYS_SAVED = DateFromDBString(reader.GetString(14))
                                });
                        }
                        catch (Exception ex)
                        {
                            LastException = ex;
                        }
                    }
                }
            }

            return result;
        }
    }
}
