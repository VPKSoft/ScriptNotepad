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
