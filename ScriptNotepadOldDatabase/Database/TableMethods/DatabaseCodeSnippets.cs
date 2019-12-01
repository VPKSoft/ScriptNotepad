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
using ScriptNotepadOldDatabase.Database.TableCommands;
using ScriptNotepadOldDatabase.Database.Tables;
using VPKSoft.LangLib;

namespace ScriptNotepadOldDatabase.Database.TableMethods
{
    /// <summary>
    /// A class containing methods for database interaction with the CODE_SNIPPETS table.
    /// </summary>
    /// <seealso cref="Database" />
    internal class DatabaseCodeSnippets: Database
    {
        /// <summary>
        /// Adds a given code snippet into the database.
        /// </summary>
        /// <param name="codeSnippet">A CODE_SNIPPETS class instance to be added into the database.</param>
        /// <returns>A CODE_SNIPPETS class instance file was successfully added to the database; otherwise null.</returns>
        internal static CODE_SNIPPETS AddCodeSnippet(CODE_SNIPPETS codeSnippet)
        {
            try
            {
                // generate a SQL sentence for the insert..
                string sql = DatabaseCommandsCodeSnippets.GenScriptInsertSentence(ref codeSnippet);

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(sql, Connection))
                {
                    // do the insert..
                    command.ExecuteNonQuery();
                }

                return codeSnippet;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Updates a recent CODE_SNIPPETS class instance into the database table CODE_SNIPPETS.
        /// </summary>
        /// <param name="codeSnippet">A CODE_SNIPPETS class instance to update to the database's CODE_SNIPPETS table.</param>
        /// <returns>The updated instance of the given <paramref name="codeSnippet"/> class instance if the operation was successful; otherwise null.</returns>
        internal static CODE_SNIPPETS UpdateCodeSnippet(CODE_SNIPPETS codeSnippet)
        {
            try
            {
                // generate a SQL sentence for the insert..
                string sql = DatabaseCommandsCodeSnippets.GenScriptUpdateSentence(ref codeSnippet);

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(sql, Connection))
                {
                    // do the insert..
                    command.ExecuteNonQuery();
                }

                return codeSnippet;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Adds or updates a a given code snippet into the database.
        /// </summary>
        /// <param name="codeSnippet">A CODE_SNIPPETS class instance to be added or updated into the database.</param>
        /// <returns>An instance to a CODE_SNIPPETS class if the operations was successful; otherwise null;</returns>
        internal static CODE_SNIPPETS AddOrUpdateCodeSnippet(CODE_SNIPPETS codeSnippet)
        {
            return UpdateCodeSnippet(AddCodeSnippet(codeSnippet));
        }

        /// <summary>
        /// Deletes the given code snippet from the database.
        /// </summary>
        /// <param name="codeSnippet">The code snippet to delete from the database.</param>
        /// <returns>True if the snippet was successfully delete from the database; otherwise false.</returns>
        internal static bool DeleteCodeSnippet(CODE_SNIPPETS codeSnippet)
        {
            try
            {
                // generate a SQL sentence for the deletion..
                string sql = DatabaseCommandsCodeSnippets.GenScriptDelete(codeSnippet);

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(sql, Connection))
                {
                    // do the deletion..
                    command.ExecuteNonQuery();
                }

                // success..
                return true;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return false; // fail..
            }
        }

        /// <summary>
        /// Makes the code snippet valid for insert or update.
        /// </summary>
        /// <param name="codeSnippet">A CODE_SNIPPETS class instance of which ID field is to be updated if the script is not valid for update otherwise.</param>
        /// <param name="reservedNames">A list of reserved names in for the script snippets in the database.</param>
        internal static void MakeCodeSnippetValidForInsertOrUpdate(ref CODE_SNIPPETS codeSnippet, params string[] reservedNames)
        {
            long count = GetScalar<long>(DatabaseCommandsCodeSnippets.GenCountReservedScripts(codeSnippet, reservedNames));
            codeSnippet.ID = count > 0 ? -1 : codeSnippet.ID;
        }

        /// <summary>
        /// Gets all the data to from the table convert to Entity Framework.
        /// </summary>
        /// <param name="connectionString">A SQLite database connection string.</param>
        /// <returns>IEnumerable&lt;System.ValueTuple&lt;System.Int32, System.String, System.String, DateTime, System.Int32&gt;&gt;.</returns>
        internal static IEnumerable<(int Id, string ScriptContents, string ScriptName, DateTime Modified, int
                ScriptTextManipulationType)>
            GetEntityData(string connectionString)
        {
            InitConnection(connectionString);

            using (var sqLiteConnection = new SQLiteConnection(connectionString))
            {
                var codeSnippets = GetCodeSnippets(1, 2, 3);
                foreach (var codeSnippet in codeSnippets)
                {
                    var legacy = codeSnippet;
                    yield return ((int) legacy.ID, legacy.SCRIPT_CONTENTS, legacy.SCRIPT_NAME, legacy.MODIFIED,
                        legacy.SCRIPT_TYPE);
                }
            }

            using (Connection)
            {
                // dispose of the connection..
            }
        }

        /// <summary>
        /// Gets all the code snippets from the database.
        /// </summary>
        /// <returns>A collection CODE_SNIPPETS classes.</returns>
        internal static IEnumerable<CODE_SNIPPETS> GetCodeSnippets(params long[] excludeIds)
        {
            List<CODE_SNIPPETS> result = new List<CODE_SNIPPETS>();

            List<long> excludeList = new List<long>();

            if (excludeIds != null)
            {
                excludeList.AddRange(excludeIds);
            }

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsCodeSnippets.GenScriptSelect(), Connection))
            {
                // loop through the result set..
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // ID: 0, SCRIPT_CONTENTS: 1, SCRIPT_NAME: 2, MODIFIED: 3, SCRIPT_TYPE: 4, SCRIPT_LANGUAGE: 5
                    while (reader.Read())
                    {
                        if (excludeList.Contains(reader.GetInt64(0)))
                        {
                            continue;
                        }

                        result.Add(
                            new CODE_SNIPPETS()
                            {
                                ID = reader.GetInt64(0),
                                SCRIPT_CONTENTS = reader.GetString(1),
                                SCRIPT_NAME = reader.GetString(2),
                                MODIFIED = DateFromDBString(reader.GetString(3)),
                                SCRIPT_TYPE = reader.GetInt32(4),
                                SCRIPT_LANGUAGE = reader.GetInt32(5),
                            });
                    }
                }
            }

            return result;
        }
    }
}
