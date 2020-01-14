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
using System.Diagnostics;
using ScriptNotepadOldDatabase.Database.TableCommands;
using ScriptNotepadOldDatabase.Database.Tables;
using VPKSoft.LangLib;

namespace ScriptNotepadOldDatabase.Database.TableMethods
{
    /// <summary>
    /// Class DatabaseMiscText.
    /// Implements the <see cref="Database" />
    /// </summary>
    /// <seealso cref="Database" />
    class DatabaseMiscText : Database
    {
        /// <summary>
        /// Updates a <see cref="MISCTEXT_LIST"/> class instance to the database to the MISCTEXT_LIST table.
        /// </summary>
        /// <param name="miscText">A <see cref="MISCTEXT_LIST"/> class instance</param>
        /// <param name="sessionName">A name of the session to which the entry belongs to.</param>
        /// <returns>The updated instance of the given <paramref name="miscText"/> class instance if the operation was successful; otherwise null.</returns>
        internal static MISCTEXT_LIST UpdateMiscText(MISCTEXT_LIST miscText, string sessionName = null)
        {
            miscText.SESSIONNAME = sessionName;

            string sql = DatabaseCommandsMiscText.GenUpdateMiscText(miscText);

            if (ExecuteArbitrarySQL(sql))
            {
                return miscText;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a given MISCTEXT_LIST class instance to the MISCTEXT_LIST database table.
        /// </summary>
        /// <param name="miscText">A <see cref="MISCTEXT_LIST"/> class instance</param>
        /// <param name="sessionName">A name of the session to which the entry belongs to.</param>
        /// <returns>A MISCTEXT_LIST class instance if the entry was successfully added to the database; otherwise null.</returns>
        internal static MISCTEXT_LIST AddMiscText(MISCTEXT_LIST miscText, string sessionName = null)
        {
            try
            {
                miscText.SESSIONNAME = sessionName;

                miscText.SESSIONID = GetScalar<long>(DatabaseCommandsGeneral.GenSessionNameIdSelectNull(sessionName));

                // default(long) returns 0, so make it a null..
                if (miscText.SESSIONID == 0)
                {
                    // ..as we are dealing with a nullable long..
                    miscText.SESSIONID = null;
                }

                string sql = DatabaseCommandsMiscText.GenInsertMiscText(miscText, sessionName);

                // as the SQLiteCommand is disposable a using clause is required..
                int recordsAffected;
                using (SQLiteCommand command = new SQLiteCommand(Connection))
                {
                    command.CommandText = sql;

                    // do the insert..
                    recordsAffected = command.ExecuteNonQuery();
                }

                long id = GetScalar<long>(DatabaseCommandsMisc.GenGetCurrentIDForTable("MISCTEXT_LIST"));

                miscText.ID = (recordsAffected > 0 && miscText.ID == -1) ? id : miscText.ID;

                // no negative ID number is accepted..
                if (miscText.ID == -1)
                {
                    miscText.ID = GetScalar<long>(DatabaseCommandsMiscText.GetExistingDBMiscTextIDSentence(miscText));
                }

                return miscText;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Adds or updates a <paramref name="miscText"/> misc text to the database to the table <see cref="MISCTEXT_LIST"/>.
        /// </summary>
        /// <param name="miscText">A MISCTEXT_LIST class instance to add or to update to the database's MISCTEXT_LIST table.</param>
        /// <param name="sessionName">A name of the session to which the misc text entry belongs to.</param>
        /// <returns>An instance to a <see cref="MISCTEXT_LIST"/> class if the operations was successful; otherwise null;</returns>
        internal static MISCTEXT_LIST AddOrUpdateMiscText(MISCTEXT_LIST miscText, string sessionName = null)
        {
            return UpdateMiscText(AddMiscText(miscText, sessionName), sessionName);
        }

        /// <summary>
        /// Gets the miscellaneous data saved to the database.
        /// </summary>
        /// <returns>A collection MISCTEXT_LIST classes.</returns>
        internal static IEnumerable<MISCTEXT_LIST> GetMiscTextLists()
        {
            List<RECENT_FILES> result = new List<RECENT_FILES>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsMiscText.GenSelectMiscText(), Connection))
            {
                // loop through the result set..
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return
                            new MISCTEXT_LIST()
                            {
                                ID = reader.GetInt64(0),
                                TEXTVALUE = reader.GetString(1),
                                TYPE = (MiscTextType) reader.GetInt32(2),
                                ADDED = DateFromDBString(reader.GetString(3)),
                                SESSIONID = reader.IsDBNull(4) ? null : (int?) reader.GetInt32(4),
                                SESSIONNAME = reader.IsDBNull(5) ? null : reader.GetString(5),
                            };
                    }
                }
            }
        }

        /// <summary>
        /// Gets all the data to from the table convert to Entity Framework.
        /// </summary>
        /// <param name="connectionString">A SQLite database connection string.</param>
        /// <returns>IEnumerable&lt;System.ValueTuple&lt;System.Int32, System.String, System.Int32, DateTime, System.String&gt;&gt;.</returns>
        internal static IEnumerable<(int Id, string TextValue, int Type, DateTime Added, string SessionName)>
            GetEntityData(string connectionString)
        {
            InitConnection(connectionString);

            using (var sqLiteConnection = new SQLiteConnection(connectionString))
            {
                var miscTexts = GetMiscTextLists();
                foreach (var miscText in miscTexts)
                {
                    var legacy = miscText;
                    yield return ((int) legacy.ID, legacy.TEXTVALUE, (int) legacy.TYPE, legacy.ADDED,
                        legacy.SESSIONNAME);
                }
            }

            using (Connection)
            {
                // dispose of the connection..
            }
        }

        /// <summary>
        /// Gets the misc texts saved into the database.
        /// </summary>
        /// <param name="textType">Type of the text the text entries belong to.</param>
        /// <param name="sessionName">A name of the session to which the misc text belong to.</param>
        /// <param name="maxCount">Maximum count of misc text entries to return.</param>
        /// <returns>A collection MISCTEXT_LIST classes.</returns>
        public static List<MISCTEXT_LIST> GetMiscTexts(MiscTextType textType, int maxCount, string sessionName = null)
        {
            List<MISCTEXT_LIST> result = new List<MISCTEXT_LIST>();

            string sql = DatabaseCommandsMiscText.GenSelectMiscText(textType, maxCount, sessionName);

            using (SQLiteCommand command = new SQLiteCommand(sql, Connection))
            {
                // loop through the result set..
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // ID: 0, TEXTVALUE: 1, TYPE: 2, ADDED: 3, SESSIONID: 4, SESSIONNAME: 5
                    while (reader.Read())
                    {
                        MISCTEXT_LIST miscText =
                            new MISCTEXT_LIST()
                            {
                                ID = reader.GetInt64(0),
                                TEXTVALUE = reader.GetString(1),
                                TYPE = (MiscTextType) reader.GetInt32(2),
                                ADDED = DateFromDBString(reader.GetString(3)),
                                SESSIONID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4), 
                                SESSIONNAME = reader.IsDBNull(5) ? null : reader.GetString(5),
                            };

                            result.Add(miscText);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Deletes the older entries from the database.
        /// </summary>
        /// <param name="textType">Type of the text the text entries belong to.</param>
        /// <param name="remainAmount">The remaining amount of entries that should be left untouched.</param>
        /// <param name="sessionName">A name of the session to which the misc text entries belong to.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool DeleteOlderEntries(MiscTextType textType, int remainAmount, string sessionName = null)
        {
            string sql = DatabaseCommandsMiscText.GenDeleteOlderEntries(textType, remainAmount, sessionName);

            return ExecuteArbitrarySQL(sql);
        }
    }
}
