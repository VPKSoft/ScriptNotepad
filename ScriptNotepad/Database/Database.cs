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
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPKSoft.ScintillaLexers;
using VPKSoft.ScintillaTabbedTextControl;

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
                return DateTime.ParseExact(value, "yyyy-MM-dd HH':'mm':'ss.fff", CultureInfo.InvariantCulture);
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
                return QS(dateTime.ToString("yyyy-MM-dd HH':'mm':'ss.fff", CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Saves a given text to a MemoryStream.
        /// </summary>
        /// <param name="text">The text to be saved to a MemoryStream.</param>
        /// <returns>An instance to a MemoryStream class containing the given text.</returns>
        public static MemoryStream TextToMemoryStream(string text)
        {
            MemoryStream ms = new MemoryStream();
            using (StreamWriter sw = new StreamWriter(ms))
            {
                sw.Write(text.ToArray());
                sw.Flush();
                ms.Position = 0;
            }
            return ms;
        }

        /// <summary>
        /// Adds a given file into the database cache.
        /// <note type="note">In case of an exception the <see cref="LastException"/> value is set to indicate the exception.</note>
        /// </summary>
        /// <param name="document">An instance to a ScintillaTabbedDocument class.</param>
        /// <param name="sessionName">A name of the session to which the document should be saved to.</param>
        /// <param name="isActive">A value indicating whether if the file is activated in the tab control.</param>
        /// <param name="isHistory"> a value indicating whether this entry is a history entry.</param>
        /// <param name="ID">An unique identifier for the file.</param>
        /// <returns>A DBFILE_SAVE class instance file was successfully added to the database; otherwise null.</returns>
        public static DBFILE_SAVE AddFile(ScintillaTabbedDocument document, bool isActive, bool isHistory, string sessionName = "Default", int ID = -1)
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
                        new FileInfo(document.FileName).LastWriteTimeUtc :
                        DateTime.MinValue,
                    DB_MODIFIED = DateTime.Now,
                    LEXER_CODE = document.LexerType,
                    FILE_CONTENTS = TextToMemoryStream(document.Scintilla.Text),
                    VISIBILITY_ORDER = (int)document.FileTabButton.Tag,
                    SESSIONNAME = sessionName,
                    ISACTIVE = isActive
                };

                long lastId = GetScalar<long>(DatabaseCommands.GenLatestDBFileSaveIDSentence());

                string sql = DatabaseCommands.GenInsertFileSentence(fileSave);

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

                fileSave.ID = lastId != newId ? newId : -1;

                return fileSave;
            }
            catch (Exception ex)
            {
                LastException = ex; // log the exception..
                return null;
            }
        }

        /// <summary>
        /// Adds a <paramref name="fileName"/> file to the database table RECENT_FILES if it doesn't exists.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>A RECENT_FILES class instance if the recent file was successfully added to the database; otherwise null.</returns>
        public static RECENT_FILES AddRecentFile(string fileName)
        {
            return AddRecentFile(RECENT_FILES.FromFilename(fileName));
        }

        /// <summary>
        /// Adds a given RECENT_FILES class instance to the RECENT_FILES database table.
        /// </summary>
        /// <param name="recentFile">A RECENT_FILES class instance to add to the database's RECENT_FILES table.</param>
        /// <returns>A RECENT_FILES class instance if the recent file was successfully added to the database; otherwise null.</returns>
        public static RECENT_FILES AddRecentFile(RECENT_FILES recentFile)
        {
            try
            {
                long lastId = GetScalar<long>(DatabaseCommands.GenLatestDBRecentFileIDSentence());

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
        /// <returns>The updated instance of the given <paramref name="recentFile"/> class instance.</returns>
        public static RECENT_FILES UpdateRecentFile(RECENT_FILES recentFile)
        {
            ExecuteArbitrarySQL(DatabaseCommands.GenHistoryUpdate(ref recentFile));
            return recentFile;
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
        /// <param name="maxCount">Maximum count of recent file entries to return.</param>
        /// <returns>A collection RECENT_FILES classes.</returns>
        public static IEnumerable<RECENT_FILES> GetRecentFiles(int maxCount = 15)
        {
            List<RECENT_FILES> result = new List<RECENT_FILES>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommands.GenHistorySelect(maxCount), conn))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // ID: 0, FILENAME_FULL: 1, FILENAME: 2, FILEPATH: 3, CLOSED_DATETIME: 4, REFERENCEID: 5
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
                                REFERENCEID = reader.IsDBNull(5) ? null : (long?)reader.GetInt64(5)
                            });
                    }
                }
            }

            return result;
        }
    }
}
