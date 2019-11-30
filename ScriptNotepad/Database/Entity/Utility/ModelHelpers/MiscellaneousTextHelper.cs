#region License
/*
MIT License

Copyright(c) 2019 Petteri Kautonen

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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Database.Entity.Enumerations;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.Database.Entity.Utility.ModelHelpers
{
    /// <summary>
    /// A class to help with <see cref="MiscellaneousTextEntry"/> entities.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class MiscellaneousTextHelper: ErrorHandlingBase
    {
        /// <summary>
        /// Adds an unique miscellaneous text value to the database.
        /// </summary>
        /// <param name="miscText">The misc text.</param>
        /// <param name="miscellaneousTextType">Type of the miscellaneous text.</param>
        /// <param name="fileSession">The file session.</param>
        /// <returns>An instance to a <see cref="MiscellaneousTextEntry"/> if successful, <c>null</c> otherwise.</returns>
        public static MiscellaneousTextEntry AddUniqueMiscellaneousText(string miscText,
            MiscellaneousTextType miscellaneousTextType, FileSession fileSession)
        {
            try
            {
                var context = ScriptNotepadDbContext.DbContext;

                if (!context.MiscellaneousTextEntries.Any(f =>
                    f.TextValue == miscText && f.TextType == miscellaneousTextType && f.Session.SessionName == fileSession.SessionName))
                {
                    var result = new MiscellaneousTextEntry
                    {
                        Session = fileSession,
                        TextType = miscellaneousTextType,
                        TextValue = miscText
                    };

                    context.MiscellaneousTextEntries.Add(result);
                    context.SaveChanges();
                    return result;
                }

                return context.MiscellaneousTextEntries.FirstOrDefault(f =>
                    f.TextValue == miscText && f.TextType == miscellaneousTextType &&
                    f.Session.SessionName == fileSession.SessionName); // success..
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return null; // failure..
            }
        }

        /// <summary>
        /// Deletes the older <see cref="MiscellaneousTextEntry"/> entries by a given limit to keep.
        /// </summary>
        /// <param name="miscellaneousTextType">Type of the miscellaneous text.</param>
        /// <param name="limit">The limit of how many to entries to keep.</param>
        /// <param name="fileSession">The file session.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool DeleteOlderEntries(MiscellaneousTextType miscellaneousTextType, int limit, FileSession fileSession)
        {
            try
            {
                ScriptNotepadDbContext.DbContext.MiscellaneousTextEntries.RemoveRange(
                    ScriptNotepadDbContext.DbContext.MiscellaneousTextEntries.Where(f => f.Session.SessionName == fileSession.SessionName)
                        .Except(GetEntriesByLimit(miscellaneousTextType, limit, fileSession)));

                return true; // success..
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return false; // failure..
            }
        }

        /// <summary>
        /// Gets the <see cref="MiscellaneousTextEntry"/> entries by a given limit.
        /// </summary>
        /// <param name="miscellaneousTextType">Type of the miscellaneous text.</param>
        /// <param name="limit">The limit of how many to entries to get.</param>
        /// <param name="fileSession">The file session.</param>
        /// <returns>System.Collections.Generic.IEnumerable&lt;ScriptNotepad.Database.Entity.Entities.MiscellaneousTextEntry&gt;.</returns>
        public static IEnumerable<MiscellaneousTextEntry> GetEntriesByLimit(MiscellaneousTextType miscellaneousTextType, int limit, FileSession fileSession)
        {
            return ScriptNotepadDbContext.DbContext
                .MiscellaneousTextEntries.Where(f => f.Session.SessionName == fileSession.SessionName).OrderBy(f => f.Added)
                .Take(limit);
        }
    }
}
