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
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Database.Entity.Enumerations;
using ScriptNotepad.Database.TableCommands;
using ScriptNotepad.Database.Tables;
using VPKSoft.LangLib;

namespace ScriptNotepad.Database.TableMethods
{
    /// <summary>
    /// A class containing methods for database interaction with the SEARCH_HISTORY and the REPLACE_HISTORY tables.
    /// </summary>
    /// <seealso cref="ScriptNotepad.Database.Database" />
    public class DatabaseSearchAndReplace: Database
    {
        /// <summary>
        /// Updates a recent SEARCH_AND_REPLACE_HISTORY class instance into the database either to the SEARCH_HISTORY table or the REPLACE_HISTORY table.
        /// </summary>
        /// <param name="searchAndReplace">A SEARCH_AND_REPLACE_HISTORY class instance to update to the database's SEARCH_HISTORY or to the database's REPLACE_HISTORY table.</param>
        /// <param name="sessionName">A name of the session to which the search or replace entry belongs to.</param>
        /// <param name="tableName">A name of the table the entry to be updated belongs to; Either SEARCH_HISTORY or REPLACE_HISTORY tables are accepted values.</param>
        /// <returns>The updated instance of the given <paramref name="searchAndReplace"/> class instance if the operation was successful; otherwise null.</returns>
        public static SEARCH_AND_REPLACE_HISTORY UpdateSearchAndReplace(SEARCH_AND_REPLACE_HISTORY searchAndReplace, string sessionName, string tableName)
        {
            searchAndReplace.SESSIONNAME = sessionName;

            string sql = DatabaseCommandsSearchAndReplace.GenInsertUpdateSearchAndReplace(searchAndReplace, tableName);

            if (ExecuteArbitrarySQL(sql))
            {
                return searchAndReplace;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a given SEARCH_AND_REPLACE_HISTORY class instance to either the SEARCH_HISTORY database table or to the REPLACE_HISTORY database table.
        /// </summary>
        /// <param name="searchAndReplace">A SEARCH_AND_REPLACE_HISTORY class instance to add to the database's SEARCH_HISTORY or to the database's REPLACE_HISTORY table.</param>
        /// <param name="sessionName">A name of the session to which the search and replace entry belongs to.</param>
        /// <param name="tableName">A name of the table the entry to be inserted belongs to; Either SEARCH_HISTORY or REPLACE_HISTORY tables are accepted values.</param>
        /// <returns>A SEARCH_AND_REPLACE_HISTORY class instance if the search and/or replace entry was successfully added to the database; otherwise null.</returns>
        public static SEARCH_AND_REPLACE_HISTORY AddSearchAndReplace(SEARCH_AND_REPLACE_HISTORY searchAndReplace, string sessionName, string tableName)
        {
            try
            {
                searchAndReplace.SESSIONNAME = sessionName;

                searchAndReplace.SESSIONID = GetScalar<long>(DatabaseCommandsGeneral.GenSessionNameIDSelect(sessionName));

                string sql = DatabaseCommandsSearchAndReplace.GenInsertSearchAndReplace(searchAndReplace, tableName);

                // as the SQLiteCommand is disposable a using clause is required..
                int recordsAffected;
                using (SQLiteCommand command = new SQLiteCommand(Connection))
                {
                    command.CommandText = sql;

                    // do the insert..
                    recordsAffected = command.ExecuteNonQuery();
                }

                long id = GetScalar<long>(DatabaseCommandsMisc.GenGetCurrentIDForTable(tableName));

                searchAndReplace.ID = (recordsAffected > 0 && searchAndReplace.ID == -1) ? id : searchAndReplace.ID;

                // no negative ID number is accepted..
                if (searchAndReplace.ID == -1)
                {
                    searchAndReplace.ID = GetScalar<long>(DatabaseCommandsSearchAndReplace.GetExistingDBSearchAndReplacIDSentence(searchAndReplace, tableName));
                }

                return searchAndReplace;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Adds or updates a <paramref name="searchAndReplace"/> search and/or replace to the database either to the table SEARCH_HISTORY or to the table REPLACE_HISTORY.
        /// </summary>
        /// <param name="searchAndReplace">A SEARCH_AND_REPLACE_HISTORY class instance to add or to update to the database's SEARCH_HISTORY or to the database's REPLACE_HISTORY table.</param>
        /// <param name="sessionName">A name of the session to which the search and replace entry belongs to.</param>
        /// <param name="tableName">A name of the table the entry to be inserted belongs to; Either SEARCH_HISTORY or REPLACE_HISTORY tables are accepted values.</param>
        /// <returns>An instance to a <see cref="SEARCH_AND_REPLACE_HISTORY"/> class if the operations was successful; otherwise null;</returns>
        public static SEARCH_AND_REPLACE_HISTORY AddOrUpdateSearchAndReplace(SEARCH_AND_REPLACE_HISTORY searchAndReplace, string sessionName, string tableName)
        {
            return UpdateSearchAndReplace(AddSearchAndReplace(searchAndReplace, sessionName, tableName), sessionName,
                       tableName);
        }

        /// <summary>
        /// Deletes the older entries from the database.
        /// </summary>
        /// <param name="tableName">A name of the table the cleanup should be done to; Either SEARCH_HISTORY or REPLACE_HISTORY tables are accepted values.</param>
        /// <param name="remainAmount">The remaining amount of entries that should be left untouched.</param>
        /// <param name="sessionName">A name of the session to which the search or the replace entries belong to.</param>
        /// <param name="types">A array of types to include to the cleanup.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool DeleteOlderEntries(string tableName, int remainAmount, string sessionName,
            params int[] types)
        {
            string sql =
                DatabaseCommandsSearchAndReplace.GenDeleteOlderEntries(tableName, remainAmount, sessionName, types);

            return ExecuteArbitrarySQL(sql);
        }

        /// <summary>
        /// Converts the legacy database table <see cref="SEARCH_AND_REPLACE_HISTORY"/> to Entity Framework format.
        /// </summary>
        /// <returns><c>true</c> if the migration to the Entity Framework's Code-First migration was successful, <c>false</c> otherwise.</returns>
        public static bool ToEntity()
        {
            var result = true;
            var connectionString = "Data Source=" + DBLangEngine.DataDir + "ScriptNotepadEntity.sqlite;Pooling=true;FailIfMissing=false;";

            var sqLiteConnection = new SQLiteConnection(connectionString);
            sqLiteConnection.Open();


            using (var context = new ScriptNotepadDbContext(sqLiteConnection, true))
            {
                var searchAndReplaces = GetSearchesAndReplaces();
                foreach (var entry in searchAndReplaces)
                {

                    var legacy = entry;

                    var session = context.Sessions?.FirstOrDefault(f => f.SessionName == legacy.SESSIONNAME);
                    if (session == null && legacy.SESSIONNAME == "Default")
                    {
                        session = context.Sessions?.FirstOrDefault(f => f.Id == 1);
                    }

                    if (session == null)
                    {
                        session = new Session {SessionName = legacy.SESSIONNAME};
                        session = context.Sessions?.Add(session);
                        context.SaveChanges();
                    }

                    var searchAndReplaceHistoryNew = new SearchAndReplaceHistory
                    {
                        Id = (int) legacy.ID, Session = session, 
                        SearchAndReplaceType = (SearchAndReplaceType)(legacy.ISREPLACE ? 1: 0), 
                        Added = legacy.ADDED, 
                        CaseSensitive = legacy.CASE_SENSITIVE, 
                        SearchAndReplaceSearchType = (SearchAndReplaceSearchType)legacy.TYPE, 
                        SearchOrReplaceText = legacy.SEARCH_OR_REPLACE_TEXT,
                    };
                    try
                    {
                        context.SearchAndReplaceHistories.Add(searchAndReplaceHistoryNew);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        result = false;
                        ExceptionLogAction?.Invoke(ex);
                        Debug.WriteLine(ex.Message);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the recent file list saved to the database.
        /// </summary>
        /// <param name="sessionName">A name of the session to which the history documents belong to.</param>
        /// <param name="tableName">The name of the table where the results should be gotten from.</param>
        /// <param name="maxCount">Maximum count of recent file entries to return.</param>
        /// <returns>A collection RECENT_FILES classes.</returns>
        public static List<SEARCH_AND_REPLACE_HISTORY> GetSearchesAndReplaces(string sessionName, string tableName, int maxCount)
        {
            List<SEARCH_AND_REPLACE_HISTORY> result = new List<SEARCH_AND_REPLACE_HISTORY>();

            string sql = DatabaseCommandsSearchAndReplace.GenSearchAndReplaceSelect(tableName, sessionName, maxCount);

            using (SQLiteCommand command = new SQLiteCommand(sql, Connection))
            {
                // loop through the result set..
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // ID: 0, TEXTFIELD: 1, CASE_SENSITIVE: 2, TYPE: 3, ADDED: 4, SESSIONID: 5, SESSIONNAME: 6
                    while (reader.Read())
                    {
                        SEARCH_AND_REPLACE_HISTORY searchAndReplace =
                            new SEARCH_AND_REPLACE_HISTORY()
                            {
                                ID = reader.GetInt64(0),
                                SEARCH_OR_REPLACE_TEXT = reader.GetString(1),
                                CASE_SENSITIVE = reader.GetInt32(2) == 1,
                                TYPE = reader.GetInt32(3),
                                ADDED = DateFromDBString(reader.GetString(4)),
                                SESSIONID = reader.GetInt32(5),
                            };

                            result.Add(searchAndReplace);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets all the search and replace history entries from the database.
        /// </summary>
        /// <returns>A collection SEARCH_AND_REPLACE_HISTORY classes.</returns>
        public static List<SEARCH_AND_REPLACE_HISTORY> GetSearchesAndReplaces()
        {
            List<SEARCH_AND_REPLACE_HISTORY> result = new List<SEARCH_AND_REPLACE_HISTORY>();

            string sql = DatabaseCommandsSearchAndReplace.GenSearchAndReplaceSelect();

            using (SQLiteCommand command = new SQLiteCommand(sql, Connection))
            {
                // loop through the result set..
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // ID: 0, TEXTFIELD: 1, CASE_SENSITIVE: 2, TYPE: 3, ADDED: 4, SESSIONID: 5, SESSIONNAME: 6, ISREPLACE: 7
                    while (reader.Read())
                    {
                        SEARCH_AND_REPLACE_HISTORY searchAndReplace =
                            new SEARCH_AND_REPLACE_HISTORY()
                            {
                                ID = reader.GetInt64(0),
                                SEARCH_OR_REPLACE_TEXT = reader.GetString(1),
                                CASE_SENSITIVE = reader.GetInt32(2) == 1,
                                TYPE = reader.GetInt32(3),
                                ADDED = DateFromDBString(reader.GetString(4)),
                                SESSIONNAME = reader.GetString(6),
                                SESSIONID = reader.GetInt32(5),
                                ISREPLACE = reader.GetInt32(7) == 1,
                            };

                            result.Add(searchAndReplace);
                    }
                }
            }

            return result;
        }
    }
}
