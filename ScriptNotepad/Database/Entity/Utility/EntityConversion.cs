using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Database.Entity.Enumerations;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepadOldDatabase;
using VPKSoft.LangLib;
using VPKSoft.Utils;

namespace ScriptNotepad.Database.Entity.Utility
{
    /// <summary>
    /// A class to handle conversion from a "legacy" database format to Entity Framework Code-First database.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class EntityConversion: ErrorHandlingBase
    {
        /// <summary>
        /// Gets the file session for a given session name.
        /// </summary>
        /// <param name="sessionName">Name of the file session.</param>
        /// <param name="context">An optional <see cref="ScriptNotepadDbContext"/> context.</param>
        /// <returns>A <see cref="FileSession"/> class instance matching the session name.</returns>
        private static FileSession GetSession(string sessionName, ScriptNotepadDbContext context = null)
        {
            if (context == null)
            {
                context = ScriptNotepadDbContext.DbContext;
            }

            var session = context.FileSessions?.FirstOrDefault(f => f.SessionName == sessionName);
            if (session == null && sessionName == "Default")
            {
                session = context.FileSessions?.FirstOrDefault(f => f.Id == 1);
            }

            if (session == null)
            {
                session = new FileSession {SessionName = sessionName};
                session = context.FileSessions?.Add(session);
                context.SaveChanges();
            }

            return session;
        }

        /// <summary>
        /// Gets the file session for a given session Id number.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="context">An optional <see cref="ScriptNotepadDbContext"/> context.</param>
        /// <param name="sessionName">An optional session name.</param>
        /// <returns>A <see cref="FileSession"/> class instance matching the session identifier.</returns>
        private static FileSession GetSession(int sessionId, ScriptNotepadDbContext context = null, string sessionName = null)
        {
            if (context == null)
            {
                context = ScriptNotepadDbContext.DbContext;
            }
            var session = context.FileSessions?.FirstOrDefault(f => f.Id == sessionId);

            if (session == null && sessionName != null)
            {
                session = context.FileSessions?.FirstOrDefault(f => f.SessionName == sessionName);
            }

            return session;
        }

        /// <summary>
        /// Converts the RECENT_FILES database table into a Entity Framework Code-First <see cref="RecentFile"/> data.
        /// </summary>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool DatabaseRecentFilesToEntity()
        {
            var result = true;
            var connectionString = "Data Source=" + DBLangEngine.DataDir +
                                   "ScriptNotepad.sqlite;Pooling=true;FailIfMissing=false;";

            var dataTuples = DataGetOld.GetEntityDataRecentFiles(connectionString);


            connectionString = "Data Source=" + DBLangEngine.DataDir + "ScriptNotepadEntity.sqlite;Pooling=true;FailIfMissing=false;";

            var sqLiteConnection = new SQLiteConnection(connectionString);
            sqLiteConnection.Open();

            using (var context = new ScriptNotepadDbContext(sqLiteConnection, true))
            {
                foreach (var dataTuple in dataTuples)
                {
                    var recentFile = new RecentFile
                    {
                        Id = dataTuple.Id,
                        Session = GetSession(dataTuple.SessionName, context),
                        Encoding = dataTuple.Encoding,
                        FileNameFull = dataTuple.FileNameFull,
                        FileName = dataTuple.FileName,
                        ClosedDateTime = dataTuple.ClosedDateTime,
                        FilePath = dataTuple.FilePath,
                    };

                    try
                    {
                        context.RecentFiles.Add(recentFile);
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
        /// Searches the and replace history to entity.
        /// </summary>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool SearchAndReplaceHistoryToEntity()
        {
            var result = true;
            var connectionString = "Data Source=" + DBLangEngine.DataDir +
                                   "ScriptNotepad.sqlite;Pooling=true;FailIfMissing=false;";

            var dataTuples = DataGetOld.GetEntityDataSearchAndReplace(connectionString);

            connectionString = "Data Source=" + DBLangEngine.DataDir +
                               "ScriptNotepadEntity.sqlite;Pooling=true;FailIfMissing=false;";

            var sqLiteConnection = new SQLiteConnection(connectionString);
            sqLiteConnection.Open();

            using (var context = new ScriptNotepadDbContext(sqLiteConnection, true))
            {
                foreach (var dataTuple in dataTuples)
                {
                    var search = new SearchAndReplaceHistory
                    {
                        Id = dataTuple.Id,
                        Session = GetSession(dataTuple.FileSession, context),
                        SearchAndReplaceSearchType = (SearchAndReplaceSearchType) dataTuple.SearchAndReplaceSearchType,
                        SearchAndReplaceType = (SearchAndReplaceType) dataTuple.SearchAndReplaceType,
                        SearchOrReplaceText = dataTuple.SearchOrReplaceText,
                        Added = dataTuple.Added,
                        CaseSensitive = dataTuple.CaseSensitive,
                    };


                    try
                    {
                        context.SearchAndReplaceHistories.Add(search);
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
        /// Converts the CODE_SNIPPETS database table into a Entity Framework Code-First <see cref="CodeSnippet"/> data.
        /// </summary>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool DatabaseCodeSnippetsToEntity()
        {
            var result = true;
            var connectionString = "Data Source=" + DBLangEngine.DataDir +
                                   "ScriptNotepad.sqlite;Pooling=true;FailIfMissing=false;";

            var dataTuples = DataGetOld.GetEntityDataCodeSnippets(connectionString);


            connectionString = "Data Source=" + DBLangEngine.DataDir + "ScriptNotepadEntity.sqlite;Pooling=true;FailIfMissing=false;";

            var sqLiteConnection = new SQLiteConnection(connectionString);
            sqLiteConnection.Open();

            using (var context = new ScriptNotepadDbContext(sqLiteConnection, true))
            {
                foreach (var dataTuple in dataTuples)
                {
                    var codeSnippet = new CodeSnippet
                    {
                        Id = dataTuple.Id,
                        ScriptTextManipulationType = (ScriptSnippetType) dataTuple.ScriptTextManipulationType,
                        ScriptLanguage = CodeSnippetLanguage.Cs,
                        ScriptName = dataTuple.ScriptName,
                        Modified = dataTuple.Modified,
                        ScriptContents = dataTuple.ScriptContents,
                    };

                    try
                    {
                        context.CodeSnippets.Add(codeSnippet);
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
        /// Converts the MISCTEXT_LIST database table into a Entity Framework Code-First <see cref="MiscellaneousTextEntry"/> data.
        /// </summary>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool DatabaseMiscTextsToEntity()
        {
            var result = true;
            var connectionString = "Data Source=" + DBLangEngine.DataDir +
                                   "ScriptNotepad.sqlite;Pooling=true;FailIfMissing=false;";

            var dataTuples = DataGetOld.GetEntityDataMiscText(connectionString);


            connectionString = "Data Source=" + DBLangEngine.DataDir + "ScriptNotepadEntity.sqlite;Pooling=true;FailIfMissing=false;";

            var sqLiteConnection = new SQLiteConnection(connectionString);
            sqLiteConnection.Open();

            using (var context = new ScriptNotepadDbContext(sqLiteConnection, true))
            {
                foreach (var dataTuple in dataTuples)
                {
                    var miscText = new MiscellaneousTextEntry
                    {
                        Id = dataTuple.Id,
                        Session = GetSession(dataTuple.SessionName, context), 
                        TextType = (MiscellaneousTextType)dataTuple.Type, 
                        TextValue = dataTuple.TextValue, 
                        Added = dataTuple.Added,
                    };

                    try
                    {
                        context.MiscellaneousTextEntries.Add(miscText);
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
        /// Converts the PLUGINS database table into a Entity Framework Code-First <see cref="Plugin"/> data.
        /// </summary>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool PluginsToEntity()
        {
            var result = true;
            var connectionString = "Data Source=" + DBLangEngine.DataDir +
                                   "ScriptNotepad.sqlite;Pooling=true;FailIfMissing=false;";

            var dataTuples = DataGetOld.GetEntityDataPlugins(connectionString);


            connectionString = "Data Source=" + DBLangEngine.DataDir + "ScriptNotepadEntity.sqlite;Pooling=true;FailIfMissing=false;";

            var sqLiteConnection = new SQLiteConnection(connectionString);
            sqLiteConnection.Open();

            using (var context = new ScriptNotepadDbContext(sqLiteConnection, true))
            {
                foreach (var dataTuple in dataTuples)
                {
                    var plugin = new Plugin
                    {
                        Id = dataTuple.Id,
                        FileNameFull = dataTuple.FileNameFull,
                        FileName = dataTuple.FileName,
                        FilePath = dataTuple.FilePath,
                        PluginVersion = dataTuple.PluginVersion,
                        PluginDescription = dataTuple.PluginDescription,
                        PluginName = dataTuple.PluginName,
                        ExceptionCount = dataTuple.ExceptionCount,
                        SortOrder = dataTuple.SortOrder,
                        PluginUpdated = dataTuple.PluginUpdated,
                        LoadFailures = dataTuple.LoadFailures,
                        ApplicationCrashes = dataTuple.ApplicationCrashes,
                        IsActive = dataTuple.IsActive,
                        PendingDeletion = dataTuple.PendingDeletion,
                        PluginInstalled = dataTuple.PluginInstalled,
                        Rating = dataTuple.Rating,
                    };

                    try
                    {
                        context.Plugins.Add(plugin);
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
        /// Converts the SESSION_NAME database table into a Entity Framework Code-First <see cref="FileSave"/> data.
        /// </summary>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool SessionDataToEntity()
        {
            var result = true;
            var connectionString = "Data Source=" + DBLangEngine.DataDir +
                                   "ScriptNotepad.sqlite;Pooling=true;FailIfMissing=false;";

            var dataTuples = DataGetOld.GetEntityDataSession(connectionString);


            connectionString = "Data Source=" + DBLangEngine.DataDir + "ScriptNotepadEntity.sqlite;Pooling=true;FailIfMissing=false;";

            var sqLiteConnection = new SQLiteConnection(connectionString);
            sqLiteConnection.Open();

            using (var context = new ScriptNotepadDbContext(sqLiteConnection, true))
            {
                foreach (var dataTuple in dataTuples)
                {
                    var session = new FileSession
                    {
                        Id = dataTuple.Id,
                        SessionName = dataTuple.SessionName,
                    };

                    try
                    {
                        context.FileSessions.Add(session);
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
        /// Converts the DBFILE_SAVE database table into a Entity Framework Code-First <see cref="FileSave"/> data.
        /// </summary>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool FileSavesToEntity()
        {
            var result = true;
            var connectionString = "Data Source=" + DBLangEngine.DataDir +
                                   "ScriptNotepad.sqlite;Pooling=true;FailIfMissing=false;";

            var dataTuples = DataGetOld.GetEntityDataFileSave(connectionString);


            connectionString = "Data Source=" + DBLangEngine.DataDir + "ScriptNotepadEntity.sqlite;Pooling=true;FailIfMissing=false;";

            var sqLiteConnection = new SQLiteConnection(connectionString);
            sqLiteConnection.Open();

            using (var context = new ScriptNotepadDbContext(sqLiteConnection, true))
            {
                foreach (var dataTuple in dataTuples)
                {
                    var fileSave = new FileSave
                    {
                        Id = dataTuple.Id, 
                        Session = GetSession(dataTuple.SessionId, context, dataTuple.SessionName), 
                        FileNameFull = dataTuple.FileNameFull, 
                        Encoding = dataTuple.Encoding, 
                        CurrentCaretPosition = dataTuple.CurrentCaretPosition, 
                        DatabaseModified = dataTuple.DatabaseModified, 
                        EditorZoomPercentage = dataTuple.EditorZoomPercentage, 
                        ExistsInFileSystem = dataTuple.ExistsInFileSystem, 
                        FileContents = dataTuple.FileContents, 
                        FileName = dataTuple.FileName, 
                        FilePath = dataTuple.FilePath, 
                        FileSystemModified = dataTuple.FileSystemModified, 
                        FileSystemSaved = dataTuple.FileSystemSaved, 
                        IsActive = dataTuple.IsActive, 
                        IsHistory = dataTuple.IsHistory, 
                        LexerType = dataTuple.LexerType, 
                        UseSpellChecking = dataTuple.UseSpellChecking, 
                        VisibilityOrder = dataTuple.VisibilityOrder,
                    };

                    try
                    {
                        context.FileSaves.Add(fileSave);
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
    }
}
