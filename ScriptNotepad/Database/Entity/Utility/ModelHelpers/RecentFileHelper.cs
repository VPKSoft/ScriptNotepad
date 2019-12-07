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
    /// A class to help with the <see cref="RecentFile"/> entities.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class RecentFileHelper: ErrorHandlingBase
    {
        /// <summary>
        /// Adds or updates a <see cref="RecentFile"/> entity into the database.
        /// </summary>
        /// <param name="fileSave">The <see cref="FileSave"/> entity to use for a recent file data.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool AddOrUpdateRecentFile(FileSave fileSave)
        {
            try
            {
                var dbContext = ScriptNotepadDbContext.DbContext;
                var recentFile = dbContext.RecentFiles.FirstOrDefault(f =>
                    f.FileNameFull == fileSave.FileNameFull && f.Session.SessionName == fileSave.Session.SessionName);

                if (recentFile != null)
                {
                    recentFile.ClosedDateTime = DateTime.Now;
                    recentFile.Encoding = fileSave.Encoding;
                }
                else
                {
                    dbContext.RecentFiles.Add(new RecentFile
                    {
                        FileNameFull = fileSave.FileNameFull,
                        Session = fileSave.Session,
                        Encoding = fileSave.Encoding,
                        ClosedDateTime = DateTime.Now,
                        FileName = fileSave.FileName,
                        FilePath = fileSave.FilePath,
                    });
                }

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return false;
            }
        }
    }
}
