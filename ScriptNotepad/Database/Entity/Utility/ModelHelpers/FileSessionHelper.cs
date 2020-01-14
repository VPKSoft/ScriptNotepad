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
    /// A class to help with <see cref="FileSessionHelper"/> entities.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class FileSessionHelper: ErrorHandlingBase
    {
        /// <summary>
        /// Deletes the entire session from the database context.
        /// </summary>
        /// <param name="session">The session to delete.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool DeleteEntireSession(FileSession session)
        {
            try
            {
                var context = ScriptNotepadDbContext.DbContext;

                context.FileSaves.RemoveRange(context.FileSaves.Where(f => f.Session.SessionName == session.SessionName));
                context.MiscellaneousTextEntries.RemoveRange(context.MiscellaneousTextEntries.Where(f => f.Session.SessionName == session.SessionName));
                context.RecentFiles.RemoveRange(context.RecentFiles.Where(f => f.Session.SessionName == session.SessionName));

                session = context.FileSessions.FirstOrDefault(f => f.SessionName == session.SessionName);

                if (session != null)
                {
                    context.FileSessions.Remove(session);
                }

                context.SaveChanges();
                return true; // success..
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return false; // failure..
            }
        }
    }
}
