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

using ScriptNotepad.Database.TableCommands;
using ScriptNotepad.Database.Tables;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ScriptNotepad.Database.TableMethods
{
    /// <summary>
    /// A class containing methods for database interaction with the SESSION_NAME table.
    /// </summary>
    /// <seealso cref="ScriptNotepad.Database.Database" />
    public class DatabaseSessionName: Database
    {
        /// <summary>
        /// Deletes the session from the database if the given <paramref name="session"/> is not the default session.
        /// </summary>
        /// <param name="session">The session to delete from the database.</param>
        /// <returns>True if the session was successfully deleted from the database; otherwise false.</returns>
        public static bool DeleteSession(SESSION_NAME session)
        {
            // the default session can not be deleted..
            if (session.IsDefault)
            {
                // ..so just return a false..
                return false;
            }

            return ExecuteArbitrarySQL(DatabaseCommandsSessionName.GenDeleteSession(session));
        }

        /// <summary>
        /// Updates the session into the database.
        /// </summary>
        /// <param name="session">An instance of a SESSION_NAME class instance to be updated into the database.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public static bool UpdateSession(SESSION_NAME session)
        {
            // the name of the session can not be an "empty" string.. 
            if (string.IsNullOrWhiteSpace(session.SESSIONNAME))
            {
                // ..so just return a false..
                return false;
            }

            return ExecuteArbitrarySQL(DatabaseCommandsSessionName.GenUpdateSessionName(session.SESSIONNAME, session.SESSIONID));
        }

        /// <summary>
        /// Updates the name of the session.
        /// </summary>
        /// <param name="session">The session which name to update.</param>
        /// <param name="newName">A new name to update for the session.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public static bool UpdateSessionName(SESSION_NAME session, string newName)
        {
            // the name of the session can not be an "empty" string.. 
            if (string.IsNullOrWhiteSpace(newName))
            {
                // ..so just return a false..
                return false;
            }

            return ExecuteArbitrarySQL(DatabaseCommandsSessionName.GenUpdateSessionName(newName, session.SESSIONID));
        }

        /// <summary>
        /// Gets a collection of the sessions in the database.
        /// </summary>
        /// <returns>A collection of session in the database.</returns>
        public static List<SESSION_NAME> GetSessions()
        {
            List<SESSION_NAME> result = new List<SESSION_NAME>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsSessionName.GenSessionSelect(), conn))
            {
                // loop through the result set..
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // SESSIONID: 0, SESSIONNAME: 1
                    while (reader.Read())
                    {
                        SESSION_NAME sessionName =
                            new SESSION_NAME()
                            {
                                SESSIONID = reader.GetInt64(0),
                                SESSIONNAME = reader.GetString(1),
                            };

                        result.Add(sessionName);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Inserts a session into the database.
        /// </summary>
        /// <param name="name">The name of the session to be inserted into the database.</param>
        /// <returns>A SESSION_NAME class instance if the operation was successful; otherwise null.</returns>
        public static SESSION_NAME InsertSession(string name)
        {
            // the name of the session can not be an "empty" string.. 
            if (string.IsNullOrWhiteSpace(name))
            {
                // ..so just return a null..
                return null;
            }

            int recordsAffected = 0;
            try
            {
                SESSION_NAME session = new SESSION_NAME { SESSIONNAME = name };

                string sql = DatabaseCommandsSessionName.GenInsertSessionName(name);

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;

                    // do the insert..
                    recordsAffected = command.ExecuteNonQuery();
                }

                long id = GetScalar<long>(DatabaseCommandsMisc.GenGetCurrentIDForTable("SESSION_NAME"));

                session.SESSIONID = id;

                return session;
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
