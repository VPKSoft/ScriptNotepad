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
    /// A class containing methods for database interaction with the PLUGINS table.
    /// </summary>
    /// <seealso cref="Database" />
    internal class DatabasePlugins: Database
    {
        /// <summary>
        /// Adds a given PLUGINS class instance to the PLUGINS database table.
        /// </summary>
        /// <param name="plugin">A PLUGINS class instance to add to the database's PLUGINS table.</param>
        /// <returns>A PLUGINS class instance if the plug-in was successfully added to the database; otherwise null.</returns>
        internal static PLUGINS AddPlugin(PLUGINS plugin)
        {
            // a necessary null check..
            if (plugin == null)
            {
                return null;
            }

            int recordsAffected = 0;
            try
            {
                string sql = DatabaseCommandsPlugins.GenPluginInsert(plugin);

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(Connection))
                {
                    command.CommandText = sql;

                    // do the insert..
                    recordsAffected = command.ExecuteNonQuery();
                }

                long id = GetScalar<long>(DatabaseCommandsMisc.GenGetCurrentIDForTable("PLUGINS"));

                plugin.ID = (recordsAffected > 0 && plugin.ID == -1) ? id : plugin.ID;

                // no negative ID number is accepted..
                if (plugin.ID == -1)
                {
                    // the file must have an ID number..
                    plugin.ID = GetScalar<long>(DatabaseCommandsPlugins.GetExistingPluginIDSentence(plugin));
                }
                return plugin;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Updates the plug-in to the database.
        /// </summary>
        /// <param name="plugin">The plug-in <see cref="PLUGINS"/> class to be updated to the database.</param>
        /// <returns>A PLUGINS class instance if the plug-in was successfully updated to the database; otherwise null.</returns>
        internal static PLUGINS UpdatePlugin(PLUGINS plugin)
        {
            // a necessary null check..
            if (plugin == null)
            {
                return null;
            }

            string sql = DatabaseCommandsPlugins.GenPluginUpdate(plugin);

            try
            {
                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(Connection))
                {
                    command.CommandText = sql;

                    // do the update..
                    command.ExecuteNonQuery();
                }
                return plugin;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Adds or updates the plug-in data in to the database.
        /// </summary>
        /// <param name="plugin">The plug-in to be added or updated.</param>
        /// <returns>A PLUGINS class instance if the plug-in was successfully added or updated to the database; otherwise null.</returns>
        internal static PLUGINS AddOrUpdatePlugin(PLUGINS plugin)
        {
            try
            {
                return UpdatePlugin(AddPlugin(plugin));
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return plugin;
            }
        }

        /// <summary>
        /// Gets all the data to from the table convert to Entity Framework.
        /// </summary>
        /// <param name="connectionString">A SQLite database connection string.</param>
        /// <returns>IEnumerable&lt;System.ValueTuple&lt;System.Int32, System.String, System.String, System.String, System.String, System.String, System.String, System.Boolean, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, DateTime, DateTime, System.Boolean&gt;&gt;.</returns>
        internal static IEnumerable<(int Id, string FileNameFull, string FileName, string FilePath, string PluginName,
            string PluginVersion, string PluginDescription, bool IsActive, int ExceptionCount, int LoadFailures, int
            ApplicationCrashes, int SortOrder, int Rating, DateTime PluginInstalled, DateTime PluginUpdated, bool
            PendingDeletion)> GetEntityData(string connectionString)
        {
            InitConnection(connectionString);

            using (var sqLiteConnection = new SQLiteConnection(connectionString))
            {
                var plugins = GetPlugins();
                foreach (var plugin in plugins)
                {
                    var legacy = plugin;
                    yield return ((int) legacy.ID, legacy.FILENAME_FULL, legacy.FILENAME, legacy.FILEPATH,
                        legacy.PLUGIN_NAME, legacy.PLUGIN_VERSION, legacy.PLUGIN_DESCTIPTION, legacy.ISACTIVE,
                        legacy.EXCEPTION_COUNT, legacy.LOAD_FAILURES,
                        legacy.APPLICATION_CRASHES, legacy.SORTORDER, legacy.RATING, legacy.PLUGIN_INSTALLED,
                        legacy.PLUGIN_UPDATED, legacy.PENDING_DELETION);
                }
            }

            using (Connection)
            {
                // dispose of the connection..
            }
        }


        /// <summary>
        /// Gets the plug-in data stored into the database.
        /// </summary>
        /// <returns>A collection PLUGINS classes.</returns>
        internal static IEnumerable<PLUGINS> GetPlugins()
        {
            List<PLUGINS> result = new List<PLUGINS>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsPlugins.GenPluginSelect(), Connection))
            {
                // loop through the result set..
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // ID: 0, FILENAME_FULL: 1, FILENAME: 2, FILEPATH: 3, PLUGIN_NAME: 4, PLUGIN_VERSION: 5,
                    // PLUGIN_DESCTIPTION: 6, ISACTIVE: 7, EXCEPTION_COUNT: 8,
                    // LOAD_FAILURES : 9, APPLICATION_CRASHES: 10, SORTORDER: 11,
                    // RATING: 12, PLUGIN_INSTALLED: 13, PLUGIN_UPDATED: 14, PENDING_DELETION: 15
                    while (reader.Read())
                    {
                        PLUGINS plugin =
                            new PLUGINS()
                            {
                                ID = reader.GetInt64(0),
                                FILENAME_FULL = reader.GetString(1),
                                FILENAME = reader.GetString(2),
                                FILEPATH = reader.GetString(3),
                                PLUGIN_NAME = reader.GetString(4),
                                PLUGIN_VERSION = reader.GetString(5),
                                PLUGIN_DESCTIPTION = reader.GetString(6),
                                ISACTIVE = reader.GetInt32(7) == 1,
                                EXCEPTION_COUNT = reader.GetInt32(8),
                                LOAD_FAILURES = reader.GetInt32(9),
                                APPLICATION_CRASHES = reader.GetInt32(10),
                                SORTORDER = reader.GetInt32(11),
                                RATING = reader.GetInt32(12),
                                PLUGIN_INSTALLED = DateFromDBString(reader.GetString(13)),
                                PLUGIN_UPDATED = DateFromDBString(reader.GetString(14)),
                                PENDING_DELETION = reader.GetInt32(15) == 1,
                            };

                        result.Add(plugin);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a given plug-in from the database.
        /// </summary>
        /// <param name="plugin">The plug-in to delete.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        internal static bool DeletePlugin(PLUGINS plugin)
        {
            return ExecuteArbitrarySQL(DatabaseCommandsPlugins.GenDeletePluginSentence(plugin));
        }
    }
}
