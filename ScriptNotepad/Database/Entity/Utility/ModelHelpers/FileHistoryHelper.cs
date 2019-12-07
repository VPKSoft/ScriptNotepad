using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.Database.Entity.Utility.ModelHelpers
{
    /// <summary>
    /// A class to help with file history and excess history cleanup.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class FileHistoryHelper: ErrorHandlingBase
    {
        /// <summary>
        /// Cleans up the closed (history) files from a given session with a given maximum amount to keep.
        /// </summary>
        /// <param name="keepMaximum">The maximum number of file history to keep per session.</param>
        /// <param name="session">The session </param>
        /// <returns><c>true</c> a tuple containing the value whether the clean up was successful and the amount of records deleted.</returns>
        public static (bool success, int count) CleanUpHistoryFiles(int keepMaximum, FileSession session)
        {
            try
            {
                var dbContext = ScriptNotepadDbContext.DbContext;
                var deleteSavesIds = dbContext.FileSaves
                    .Where(f => f.Session.SessionName == session.SessionName && f.IsHistory)
                    .Select(f => new {id = f.Id, modified = f.DatabaseModified});

                var deleteAmount = deleteSavesIds.Count() - keepMaximum;

                if (deleteAmount > 0)
                {
                    deleteSavesIds = deleteSavesIds.Take(deleteAmount);
                    var deleted = dbContext.FileSaves.RemoveRange(
                            dbContext.FileSaves.Where(f =>
                                deleteSavesIds.OrderBy(d => d.modified).Any(h => h.id == f.Id)))
                        .Count();

                    dbContext.SaveChanges();

                    return (true, deleted);
                }

                return (true, 0);
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return (false, 0);
            }
        }

        /// <summary>
        /// Cleanups the recent file list by removing older entries from the list by a given number to keep.
        /// </summary>
        /// <param name="keepMaximum">The maximum number of recent files to keep per session.</param>
        /// <param name="session">The session from which to clean the recent file from.</param>
        /// <returns><c>true</c> a tuple containing the value whether the clean up was successful and the amount of records deleted.</returns>
        public static (bool success, int count) CleanupHistoryList(int keepMaximum, FileSession session) // one could probably make this a bit more complicated..
        {
            try
            {
                var dbContext = ScriptNotepadDbContext.DbContext;

                session =
                    dbContext.FileSessions.FirstOrDefault(f => f.SessionName == session.SessionName);

                var closedCount =
                    dbContext.FileSaves.Count(f => f.IsHistory && f.Session.SessionName == session.SessionName);

                var removeFiles = dbContext.FileSaves
                    .Where(f => !f.IsHistory && f.Session.SessionName == session.SessionName)
                    .Select(f => f.FileNameFull);

                dbContext.RecentFiles.RemoveRange(dbContext.RecentFiles.Where(f =>
                    f.Session.SessionName == session.SessionName && removeFiles.Contains(f.FileNameFull)));

                var historyRemoveCount = closedCount - keepMaximum;

                if (historyRemoveCount > 0)
                {
                    var deleted = dbContext.RecentFiles.RemoveRange(dbContext.RecentFiles
                        .OrderByDescending(f => f.ClosedDateTime)
                        .Take(historyRemoveCount)).Count();

                    dbContext.SaveChanges();

                    return (true, deleted);
                }

                return (true, 0);
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return (false, 0);
            }
        }
    }
}
