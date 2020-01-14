#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

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
using System.Data.SQLite;
using System.Text;
using ScriptNotepadOldDatabase.Database.TableCommands;
using ScriptNotepadOldDatabase.Database.Tables;
using ScriptNotepadOldDatabase.UtilityClasses.Encodings;

namespace ScriptNotepadOldDatabase.Database.TableMethods
{
    /// <summary>
    /// A class containing methods for database interaction with the RECENT_FILES table.
    /// </summary>
    /// <seealso cref="Database" />
    internal class DatabaseRecentFiles: ScriptNotepadOldDatabase.Database.Database
    {
        /// <summary>
        /// Updates a recent RECENT_FILES class instance into the database table RECENT_FILES.
        /// </summary>
        /// <param name="recentFile">A RECENT_FILES class instance to update to the database's RECENT_FILES table.</param>
        /// <param name="sessionName">A name of the session to which the document belongs to.</param>
        /// <returns>The updated instance of the given <paramref name="recentFile"/> class instance if the operation was successful; otherwise null.</returns>
        internal static RECENT_FILES UpdateRecentFile(RECENT_FILES recentFile, string sessionName)
        {
            recentFile.SESSIONNAME = sessionName;
            if (ExecuteArbitrarySQL(DatabaseCommandsRecentFiles.GenHistoryUpdate(ref recentFile)))
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
        /// <param name="encoding">The encoding of the recent file.</param>
        /// <returns>True if the operations was successful; otherwise false;</returns>
        internal static bool AddOrUpdateRecentFile(string fileName, string sessionName, Encoding encoding)
        {
            return UpdateRecentFile(AddRecentFile(fileName, sessionName, encoding), sessionName) != null;
        }

        /// <summary>
        /// Gets all the data to from the table convert to Entity Framework.
        /// </summary>
        /// <param name="connectionString">A SQLite database connection string.</param>
        /// <returns>IEnumerable&lt;System.ValueTuple&lt;System.Int32, System.String, System.String, Encoding, System.String, DateTime, System.String&gt;&gt;.</returns>
        internal static
            IEnumerable<(int Id, string FileNameFull, string SessionName, Encoding Encoding, string FileName, DateTime
                ClosedDateTime, string FilePath)> GetEntityData(string connectionString)
        {
            InitConnection(connectionString);

            using (var sqLiteConnection = new SQLiteConnection(connectionString))
            {
                var recentFiles = GetRecentFiles();
                foreach (var recentFile in recentFiles)
                {
                    var legacy = recentFile;
                    yield return ((int)legacy.ID, legacy.FILENAME_FULL, legacy.SESSIONNAME, legacy.ENCODING, legacy.FILENAME,
                        legacy.CLOSED_DATETIME, legacy.FILEPATH);
                }
            }

            using (Connection)
            {
                // dispose of the connection..
            }
        }

        /// <summary>
        /// Gets the recent file list saved to the database.
        /// </summary>
        /// <param name="sessionName">A name of the session to which the history documents belong to.</param>
        /// <param name="maxCount">Maximum count of recent file entries to return.</param>
        /// <returns>A collection RECENT_FILES classes.</returns>
        internal static IEnumerable<RECENT_FILES> GetRecentFiles(string sessionName, int maxCount)
        {
            List<RECENT_FILES> result = new List<RECENT_FILES>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsRecentFiles.GenHistorySelect(sessionName, maxCount), Connection))
            {
                // loop through the result set..
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    return FromDataReader(reader);
                }
            }
        }

        /// <summary>
        /// Gets the recent file list saved to the database.
        /// </summary>
        /// <returns>A collection RECENT_FILES classes.</returns>
        internal static IEnumerable<RECENT_FILES> GetRecentFiles()
        {
            List<RECENT_FILES> result = new List<RECENT_FILES>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsRecentFiles.GenHistorySelect(), Connection))
            {
                // loop through the result set..
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    return FromDataReader(reader);
                }
            }
        }


        internal static IEnumerable<RECENT_FILES> FromDataReader(SQLiteDataReader reader)
        {
            List<RECENT_FILES> result = new List<RECENT_FILES>();
            // ID: 0, FILENAME_FULL: 1, FILENAME: 2, FILEPATH: 3, CLOSED_DATETIME: 4, 
            // SESSIONID: 5, REFERENCEID: 6, SESSION_NAME: 7, EXISTSINDB: 8, ENCODING = 9
            while (reader.Read())
            {
                RECENT_FILES recentFile =
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
                        EXISTSINDB = reader.GetInt32(8) == 1,
                        ENCODING = EncodingData.EncodingFromString(reader.GetString(9)),
                    };

                // the file must exist somewhere..
                if (recentFile.EXISTSINDB || recentFile.EXISTSINFILESYS)
                {
                    result.Add(recentFile);
                }
            }
            return result;
        }

        /// <summary>
        /// Adds a <paramref name="fileName"/> file to the database table RECENT_FILES if it doesn't exists.
        /// </summary>
        /// <param name="fileName">A file name to generate a RECENT_FILES class instance.</param>
        /// <param name="sessionName">A name of the session to which the document belongs to.</param>
        /// <param name="encoding">The encoding of the recent file.</param>
        /// <returns>A RECENT_FILES class instance if the recent file was successfully added to the database; otherwise null.</returns>
        internal static RECENT_FILES AddRecentFile(string fileName, string sessionName, Encoding encoding)
        {
            return AddRecentFile(RECENT_FILES.FromFilename(fileName, encoding), sessionName);
        }

        /// <summary>
        /// Adds a given RECENT_FILES class instance to the RECENT_FILES database table.
        /// </summary>
        /// <param name="recentFile">A RECENT_FILES class instance to add to the database's RECENT_FILES table.</param>
        /// <param name="sessionName">A name of the session to which the document belongs to.</param>
        /// <returns>A RECENT_FILES class instance if the recent file was successfully added to the database; otherwise null.</returns>
        internal static RECENT_FILES AddRecentFile(RECENT_FILES recentFile, string sessionName)
        {
            int recordsAffected = 0;
            try
            {
                recentFile.SESSIONNAME = sessionName;

                recentFile.SESSIONID = GetScalar<long>(DatabaseCommandsGeneral.GenSessionNameIDSelect(sessionName));

                string sql = DatabaseCommandsRecentFiles.GenHistoryInsert(recentFile);

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(Connection))
                {
                    command.CommandText = sql;

                    // do the insert..
                    recordsAffected = command.ExecuteNonQuery();
                }

                long id = GetScalar<long>(DatabaseCommandsMisc.GenGetCurrentIDForTable("RECENT_FILES"));

                recentFile.ID = (recordsAffected > 0 && recentFile.ID == -1) ? id : recentFile.ID;

                // no negative ID number is accepted..
                if (recentFile.ID == -1)
                {
                    recentFile.ID = GetScalar<long>(DatabaseCommandsRecentFiles.GetExistingDBRecentFileIDSentence(recentFile));
                }

                return recentFile;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }
    }
}
