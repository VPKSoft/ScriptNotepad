#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

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

using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.UtilityClasses.Encodings;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.Editor.EntityHelpers;

/// <summary>
/// Helper methods for the <see cref="RecentFile"/> entity.
/// </summary>
public static class RecentFileHelper
{
    /// <summary>
    /// Gets the encoding of the recent file.
    /// </summary>
    /// <param name="recentFile">The <see cref="RecentFile"/> instance.</param>
    public static Encoding GetEncoding(this RecentFile recentFile)
    {
        return EncodingData.EncodingFromString(recentFile.EncodingAsString) ?? Encoding.UTF8;
    }

    /// <summary>
    /// Gets the encoding of the recent file.
    /// </summary>
    /// <param name="recentFile">The <see cref="RecentFile"/> instance.</param>
    /// <param name="value">The encoding to set for the recent file.</param>
    public static void SetEncoding(this RecentFile recentFile, Encoding value)
    {
        recentFile.EncodingAsString = EncodingData.EncodingToString(value);
    }

    /// <summary>
    /// Gets a value indicating whether a snapshot of the file in question exists in the database.
    /// </summary>
    /// <param name="recentFile">The <see cref="RecentFile"/> instance.</param>
    /// <param name="fileSaves">The <see cref="FileSave"/> entities to compare the specified <see cref="RecentFile"/> to.</param>
    public static bool ExistsInDatabase(this RecentFile recentFile, DbSet<FileSave> fileSaves)
    {
        return fileSaves.Count(f => f.FileNameFull == recentFile.FileNameFull && f.Session.SessionName == recentFile.Session.SessionName && f.IsHistory) > 0;
    }

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
                recentFile.SetEncoding(fileSave.GetEncoding());
            }
            else
            {
                dbContext.RecentFiles.Add(new RecentFile
                {
                    FileNameFull = fileSave.FileNameFull,
                    Session = fileSave.Session,
                    EncodingAsString = EncodingData.EncodingToString(fileSave.GetEncoding()),
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
            ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
            return false;
        }
    }
}