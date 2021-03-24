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
using System.Linq;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.Editor.Utility.ModelHelpers
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

                    var deleted = dbContext.FileSaves.Count(f => deleteSavesIds.OrderBy(d => d.modified).Any(h => h.id == f.Id));

                    dbContext.FileSaves.RemoveRange(
                        dbContext.FileSaves.Where(f =>
                            deleteSavesIds.OrderBy(d => d.modified).Any(h => h.id == f.Id)));

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
                    var deleted = dbContext.RecentFiles
                        .OrderByDescending(f => f.ClosedDateTime)
                        .Take(historyRemoveCount).Count();

                    dbContext.RecentFiles.RemoveRange(dbContext.RecentFiles
                        .OrderByDescending(f => f.ClosedDateTime)
                        .Take(historyRemoveCount));

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
