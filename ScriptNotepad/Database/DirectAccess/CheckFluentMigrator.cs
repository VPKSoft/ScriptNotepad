using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepad.Database.DirectAccess
{
    /// <summary>
    /// A class to create the VersionInfo table in case the code-first database already exists.
    /// </summary>
    internal class CheckFluentMigrator
    {
        /// <summary>
        /// Marks the first database migration as done.
        /// </summary>
        /// <param name="connectionString">The connection string fot the SQLite database table.</param>
        public static void MarkMigration(string connectionString)
        {
            using var connection = new SQLiteConnection(connectionString);

            connection.Open();

            using SQLiteCommand command = new SQLiteCommand(
                "CREATE TABLE IF NOT EXISTS VersionInfo (Version INTEGER NOT NULL, AppliedOn DATETIME, Description TEXT);",
                connection);

            command.ExecuteNonQuery();

            command.CommandText = string.Join(Environment.NewLine,
                "INSERT INTO VersionInfo (Version, AppliedOn, Description)",
                // ReSharper disable once StringLiteralTypo, this is a function name in the SQLite..
                "SELECT 20210101103253, strftime('%Y-%m-%dT%H:%M:%S','now'), 'InitialMigration'",
                "WHERE NOT EXISTS(SELECT * FROM VersionInfo WHERE Version = 20210101103253)");
            command.ExecuteNonQuery();
        }
    }
}
