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
using System.IO;
using ScriptNotepadOldDatabase.Database.TableCommands;
using ScriptNotepadOldDatabase.Database.UtilityClasses;

namespace ScriptNotepadOldDatabase.Database
{
    /// <summary>
    /// A class the handle the SQLite database.
    /// </summary>
    internal class Database: DataFormulationHelpers
    {
        // the database connection to use..
        internal static SQLiteConnection Connection { get; set; }

        /// <summary>
        /// Creates a new SQLiteConnection class for the Database class with the given connection string.
        /// </summary>
        /// <param name="connectionString">A connection string to create a SQLite database connection.</param>
        internal static void InitConnection(string connectionString)
        {
            Connection = new SQLiteConnection(connectionString); // create a new SQLiteConnection class instance..
            Connection.Open();
        }

        /// <summary>
        /// Cleans up the history file list for the given session.
        /// </summary>
        /// <param name="sessionName">Name of the session of which history file list should be cleaned from the database.</param>
        /// <param name="maxAmount">The maximum amount of entries to keep in the history file list.</param>
        /// <returns>A named tuple containing an indicator of the success of the deletion and the amount of deleted records from the database.</returns>
        internal static (bool success, int deletedAmount) CleanUpHistoryList(string sessionName, int maxAmount)
        {
            try
            {
                // a list of RECENT_FILES ID numbers to be deleted from the database..
                List<long> ids = new List<long>();

                // generate a SQL sentence for the selection..
                string sql = DatabaseCommandsRecentFiles.GenHistoryListSelect(sessionName);

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(sql, Connection))
                {
                    // loop through the result set..
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // ID: 0, LIST_AMOUNT: 1
                        while (reader.Read())
                        {
                            // stop if the amount of the history list is lower than the given maximum amount..
                            if (reader.GetInt64(1) < maxAmount)
                            {
                                return (false, 0);
                            }

                            // get the total count value..
                            long count = reader.GetInt64(1);

                            // collect the maximum amount of ID numbers to be deleted from the database..
                            if (count - maxAmount > ids.Count)
                            {
                                ids.Add(reader.GetInt64(0));
                            }
                            else // the amount if full..
                            {
                                // ..so break the loop..
                                break;
                            }
                        }
                    }
                }

                // return the success value and the amount of deleted RECENT_FILES entries..
                if (ids.Count > 0)
                {
                    // ..if any..
                    return (ExecuteArbitrarySQL(DatabaseCommandsRecentFiles.GenDeleteDBFileHistoryIDList(ids)), ids.Count);
                }
                else
                {
                    // ..none where to be deleted..
                    return (false, 0);
                }
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return (false, 0); // failure..
            }
        }


        /// <summary>
        /// Gets or sets the action to be used to log an exception.
        /// </summary>
        internal static Action<Exception> ExceptionLogAction { get; set; } = null;

        /// <summary>
        /// Executes a scalar SQL sentence against the database.
        /// </summary>
        /// <typeparam name="T">The return type of the scalar SQL sentence.</typeparam>
        /// <param name="sql">A scalar SQL sentence to be executed against the database.</param>
        /// <returns>The value of type T if the operation was successful; otherwise a default value of T is returned.</returns>
        internal static T GetScalar<T>(string sql)
        {
            try
            {
                // execute a scalar SQL sentence against the database..
                using (SQLiteCommand command = new SQLiteCommand(Connection))
                {
                    command.CommandText = sql;

                    // ..and return the value casted into a typeof(T)..

                    var value = command.ExecuteScalar();

                    if (value == null)
                    {
                        return default;
                    }

                    return (T)value;
                }
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                // failed, return default(T).. 
                return default;
            }
        }

        /// <summary>
        /// Executes a arbitrary SQL into the database.
        /// </summary>
        /// <param name="sql">A string containing SQL sentences to be executed to the database.</param>
        /// <returns>True if the given SQL sentences were executed successfully; otherwise false;</returns>
        // ReSharper disable once InconsistentNaming
        internal static bool ExecuteArbitrarySQL(string sql)
        {
            // as the SQLiteCommand is disposable a using clause is required..
            using (SQLiteCommand command = new SQLiteCommand(sql, Connection))
            {
                try
                {
                    // try to execute the given SQL..
                    command.ExecuteNonQuery();
                    return true; // success..
                }
                catch (Exception ex) // something went wrong so do log the reason.. (ex avoids the EventArgs e in all cases!)..
                {
                    // log the exception if the action has a value..
                    ExceptionLogAction?.Invoke(ex);
                    return false; // failure..
                }
            }
        }

        /// <summary>
        /// Creates a memory stream from the given SQLiteBlob.
        /// </summary>
        /// <param name="blob">A SQLiteBlob to create a memory stream from.</param>
        /// <returns>A memory stream created from the given <paramref name="blob"/>.</returns>
        internal static MemoryStream MemoryStreamFromBlob(SQLiteBlob blob)
        {
            int size = blob.GetCount();
            byte[] blobBytes = new byte[size];
            blob.Read(blobBytes, size, 0);
            return new MemoryStream(blobBytes); // remember to dispose of this..
        }

        /// <summary>
        /// Localizes the default name session name.
        /// </summary>
        /// <param name="name">The localized name for "Default".</param>
        internal static void LocalizeDefaultSessionName(string name)
        {
            // update the name..
            ExecuteArbitrarySQL(DatabaseCommandsMisc.GenLocalizeDefaultSessionName(name));
        }

        /// <summary>
        /// Gets the session ID for a given session name.
        /// </summary>
        /// <param name="sessionName">The name of the session which ID to get.</param>
        /// <returns>An ID number for the given session name.</returns>
        // ReSharper disable once InconsistentNaming
        internal static long GetSessionID(string sessionName)
        {
            return GetScalar<long>(DatabaseCommandsMisc.GenGetCurrentSessionID(sessionName));
        }
    }
}
