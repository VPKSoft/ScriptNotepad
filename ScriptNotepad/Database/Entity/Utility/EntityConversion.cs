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
    }
}
