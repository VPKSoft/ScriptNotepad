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
using ScriptNotepad.Database.UtilityClasses;

namespace ScriptNotepad.Database
{
    /// <summary>
    /// A class the handle the SQLite database.
    /// </summary>
    public class Database: DataFormulationHelpers
    {
        // the database connection to use..
        internal static SQLiteConnection Connection { get; set; }

        /// <summary>
        /// Creates a new SQLiteConnection class for the Database class with the given connection string.
        /// </summary>
        /// <param name="connectionString">A connection string to create a SQLite database connection.</param>
        public static void InitConnection(string connectionString)
        {
            Connection = new SQLiteConnection(connectionString); // create a new SQLiteConnection class instance..
            Connection.Open();
        }

        /// <summary>
        /// Gets or sets the action to be used to log an exception.
        /// </summary>
        public static Action<Exception> ExceptionLogAction { get; set; } = null;

        /// <summary>
        /// Executes a scalar SQL sentence against the database.
        /// </summary>
        /// <typeparam name="T">The return type of the scalar SQL sentence.</typeparam>
        /// <param name="sql">A scalar SQL sentence to be executed against the database.</param>
        /// <returns>The value of type T if the operation was successful; otherwise a default value of T is returned.</returns>
        public static T GetScalar<T>(string sql)
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
        public static bool ExecuteArbitrarySQL(string sql)
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
        public static MemoryStream MemoryStreamFromBlob(SQLiteBlob blob)
        {
            int size = blob.GetCount();
            byte[] blobBytes = new byte[size];
            blob.Read(blobBytes, size, 0);
            return new MemoryStream(blobBytes); // remember to dispose of this..
        }
    }
}
