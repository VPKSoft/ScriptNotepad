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

using ScriptNotepadOldDatabase.Database.TableCommands;
using ScriptNotepadOldDatabase.Database.Tables;

namespace ScriptNotepadOldDatabase.Database.TableMethods
{
    /// <summary>
    /// A class containing methods for database interaction with the session related data in the database.
    /// </summary>
    /// <seealso cref="Database" />
    internal class DatabaseSessionData: Database
    {
        /// <summary>
        /// Deletes the session data from the <see cref="RECENT_FILES"/> table in the database.
        /// </summary>
        /// <param name="session">The session of which history data to delete from the database.</param>
        /// <returns>True if the session history data was successfully deleted from the database; otherwise false.</returns>
        internal static bool DeleteSessionDataRecent(SESSION_NAME session)
        {
            return ExecuteArbitrarySQL(DatabaseCommandsSessionData.GenDeleteSessionDataHistory(session));
        }

        /// <summary>
        /// Deletes the session data from the <see cref="DBFILE_SAVE"/> table in the database.
        /// </summary>
        /// <param name="session">The session of which data to delete from the database.</param>
        /// <returns>True if the session data was successfully deleted from the database; otherwise false.</returns>
        internal static bool DeleteSessionDataData(SESSION_NAME session)
        {
            return ExecuteArbitrarySQL(DatabaseCommandsSessionData.GenDeleteSessionDataData(session));
        }

        /// <summary>
        /// Deletes the session data and the session from the database.
        /// <param name="session">The session of which entire data to delete from the database.</param>
        /// </summary>
        /// <returns>True if the session data and the session history data was successfully deleted from the database; otherwise false.</returns>
        internal static bool DeleteEntireSession(SESSION_NAME session)
        {
            bool result = true;
            result &= DeleteSessionData(session);
            result &= DatabaseSessionName.DeleteSession(session);
            return result;
        }

        /// <summary>
        /// Deletes the session data and the session history data from the database.
        /// </summary>
        /// <param name="session">The session of which data and the history data to delete from the database.</param>
        /// <returns>True if the entire session was successfully deleted from the database; otherwise false.</returns>
        internal static bool DeleteSessionData(SESSION_NAME session)
        {
            bool result = true;
            result &= DeleteSessionDataRecent(session);
            result &= DeleteSessionDataData(session);
            return result;
        }
    }
}
