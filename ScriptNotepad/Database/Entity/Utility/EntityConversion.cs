using System;
using System.Diagnostics;
using System.Linq;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
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
        private static Session GetSession(string sessionName)
        {
            var context = ScriptNotepadDbContext.DbContext;
            var session = context.Sessions?.FirstOrDefault(f => f.SessionName == sessionName);
            if (session == null && sessionName == "Default")
            {
                session = context.Sessions?.FirstOrDefault(f => f.Id == 1);
            }

            if (session == null)
            {
                session = new Session {SessionName = sessionName};
                session = context.Sessions?.Add(session);
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

            var context = ScriptNotepadDbContext.DbContext;
            foreach (var dataTuple in dataTuples)
            {
                var recentFile = new RecentFile
                {
                    Id = dataTuple.Id,
                    Session = GetSession(dataTuple.SessionName),
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

            return result;
        }
    }
}
