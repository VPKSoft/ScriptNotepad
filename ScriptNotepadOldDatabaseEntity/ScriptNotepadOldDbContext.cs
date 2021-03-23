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

namespace ScriptNotepadOldDatabaseEntity
{
    /// <summary>
    /// A Class to migrate the EF6 database to EF Core.
    /// </summary>
    public class ScriptNotepadOldDbContext
    {
        public static List<Exception> MigrateToEfCore(string oldDatabaseFile, string newDatabaseFile)
        {
            var result = new List<Exception>();

            try
            {
                using var connection = new SQLiteConnection($"Data Source={newDatabaseFile}");
                connection.Open();

                try
                {
                    using var command = new SQLiteCommand($"ATTACH '{oldDatabaseFile}' AS OLD", connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result.Add(ex);
                }

                try
                {
                    using var command =
                        new SQLiteCommand(
                            string.Join(Environment.NewLine,
                                "INSERT INTO",
                                "FileSessions(Id, SessionName, TemporaryFilePath, UseFileSystemOnContents)",
                                "SELECT",
                                "Id, SessionName, TemporaryFilePath, UseFileSystemOnContents",
                                "FROM",
                                "OLD.FileSessions",
                                "WHERE ID > 1"), connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result.Add(ex);
                }

                try
                {
                    using var command =
                        new SQLiteCommand(
                            string.Join(Environment.NewLine,
                                "INSERT INTO",
                                "CodeSnippets(ScriptContents, ScriptName, Modified, ScriptLanguage, ScriptTextManipulationType)",
                                "SELECT",
                                "ScriptContents, ScriptName, Modified, ScriptLanguage, ScriptTextManipulationType",
                                "FROM",
                                "OLD.CodeSnippets",
                                "WHERE Id > 3"), connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result.Add(ex);
                }

                try
                {
                    using var command =
                        new SQLiteCommand(
                            string.Join(Environment.NewLine,
                                "INSERT INTO",
                                "FileSaves(EncodingAsString, ExistsInFileSystem, FileNameFull, FileName, FilePath, FileSystemModified, FileSystemSaved, DatabaseModified, LexerType, UseFileSystemOnContents, TemporaryFileSaveName, FileContents, TemporaryFileSaveName, UseFileSystemOnContents, FileContents, VisibilityOrder, IsActive, IsHistory, CurrentCaretPosition, UseSpellChecking, EditorZoomPercentage, SessionId)",
                                "SELECT",
                                "EncodingAsString, ExistsInFileSystem, FileNameFull, FileName, FilePath, FileSystemModified, FileSystemSaved, DatabaseModified, LexerType, UseFileSystemOnContents, TemporaryFileSaveName, FileContents, TemporaryFileSaveName, UseFileSystemOnContents, FileContents, VisibilityOrder, IsActive, IsHistory, CurrentCaretPosition, UseSpellChecking, EditorZoomPercentage, Session_Id",
                                "FROM",
                                "OLD.FileSaves"), connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result.Add(ex);
                }

                try
                {
                    using var command =
                        new SQLiteCommand(
                            string.Join(Environment.NewLine,
                                "INSERT INTO",
                                "MiscellaneousParameters(Added, Value)",
                                "SELECT",
                                "Added, Value",
                                "FROM",
                                "OLD.MiscellaneousParameters"), connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result.Add(ex);
                }

                try
                {
                    using var command =
                        new SQLiteCommand(
                            string.Join(Environment.NewLine,
                                "INSERT INTO",
                                "MiscellaneousTextEntries(TextValue, TextType, Added, SessionId)",
                                "SELECT",
                                "TextValue, TextType, Added, Session_Id",
                                "FROM",
                                "OLD.MiscellaneousTextEntries"), connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result.Add(ex);
                }

                try
                {
                    using var command =
                        new SQLiteCommand(
                            string.Join(Environment.NewLine,
                                "INSERT INTO",
                                "Plugins(FileNameFull, FileName, FilePath, PluginName, PluginVersion, PluginDescription, IsActive, ExceptionCount, LoadFailures, ApplicationCrashes, SortOrder, Rating, PluginInstalled, PluginUpdated, PendingDeletion)",
                                "SELECT",
                                "FileNameFull, FileName, FilePath, PluginName, PluginVersion, PluginDescription, IsActive, ExceptionCount, LoadFailures, ApplicationCrashes, SortOrder, Rating, PluginInstalled, PluginUpdated, PendingDeletion",
                                "FROM",
                                "OLD.Plugins"), connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result.Add(ex);
                }

                try
                {
                    using var command =
                        new SQLiteCommand(
                            string.Join(Environment.NewLine,
                                "INSERT INTO",
                                "RecentFiles(FileNameFull, FileName, FilePath, ClosedDateTime, EncodingAsString, SessionId)",
                                "SELECT",
                                "FileNameFull, FileName, FilePath, ClosedDateTime, EncodingAsString, Session_Id",
                                "FROM",
                                "OLD.RecentFiles"), connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result.Add(ex);
                }

                try
                {
                    using var command =
                        new SQLiteCommand(
                            string.Join(Environment.NewLine,
                                "INSERT INTO",
                                "SearchAndReplaceHistories(SearchOrReplaceText, CaseSensitive, SearchAndReplaceSearchType, SearchAndReplaceType, Added, SessionId)",
                                "SELECT",
                                "SearchOrReplaceText, CaseSensitive, SearchAndReplaceSearchType, SearchAndReplaceType, Added, Session_Id",
                                "FROM",
                                "OLD.SearchAndReplaceHistories"), connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result.Add(ex);
                }
            }
            catch (Exception ex)
            {
                result.Add(ex);
            }

            return result;
        }
    }
}
