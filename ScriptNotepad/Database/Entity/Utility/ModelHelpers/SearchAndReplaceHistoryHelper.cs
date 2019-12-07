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
using System.Collections.Generic;
using System.Linq;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Database.Entity.Enumerations;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.Database.Entity.Utility.ModelHelpers
{
    /// <summary>
    /// A class to help with <see cref="SearchAndReplaceHistory"/> entities.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class SearchAndReplaceHistoryHelper: ErrorHandlingBase
    {
        /// <summary>
        /// Deletes the older entities with a given limit.
        /// </summary>
        /// <param name="searchAndReplaceSearchType">Type of the search and replace search.</param>
        /// <param name="searchAndReplaceType">Type of the search and replace.</param>
        /// <param name="limit">The limit of how many to entities to keep.</param>
        /// <param name="fileSession">The file session.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool DeleteOlderEntries(SearchAndReplaceSearchType searchAndReplaceSearchType, SearchAndReplaceType searchAndReplaceType, int limit,
            FileSession fileSession)
        {
            try
            {
                ScriptNotepadDbContext.DbContext.SearchAndReplaceHistories.RemoveRange(
                    ScriptNotepadDbContext.DbContext.SearchAndReplaceHistories.Where(f =>
                            f.Session.SessionName == fileSession.SessionName &&
                            f.SearchAndReplaceSearchType.HasFlag(searchAndReplaceSearchType) &&
                            f.SearchAndReplaceType == searchAndReplaceType)
                        .Except(GetEntriesByLimit(searchAndReplaceSearchType, searchAndReplaceType, limit,
                            fileSession)));

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
        /// Gets the <see cref="SearchAndReplaceHistory"/> entities by a given limit.
        /// </summary>
        /// <param name="searchAndReplaceSearchType">Type of the search and replace search.</param>
        /// <param name="searchAndReplaceType">Type of the search and replace.</param>
        /// <param name="limit">The limit of how many to entities to get.</param>
        /// <param name="fileSession">The file session.</param>
        /// <returns>IEnumerable&lt;SearchAndReplaceHistory&gt;.</returns>
        public static IEnumerable<SearchAndReplaceHistory> GetEntriesByLimit(
            SearchAndReplaceSearchType searchAndReplaceSearchType, SearchAndReplaceType searchAndReplaceType, int limit,
            FileSession fileSession)
        {
            return ScriptNotepadDbContext.DbContext
                .SearchAndReplaceHistories
                .Where(f => f.Session.SessionName == fileSession.SessionName &&
                            f.SearchAndReplaceSearchType.HasFlag(searchAndReplaceSearchType) &&
                            f.SearchAndReplaceType == searchAndReplaceType).OrderBy(f => f.Added)
                .Take(limit);
        }

        /// <summary>
        /// Adds or updates a <see cref="SearchAndReplaceHistory"/> entity.
        /// </summary>
        /// <param name="text">The text used for searching or replacing.</param>
        /// <param name="searchAndReplaceSearchType">Type of the search and replace search.</param>
        /// <param name="searchAndReplaceType">Type of the search and replace.</param>
        /// <param name="caseSensitive">if set to <c>true</c> the search or replace is case sensitive.</param>
        /// <param name="fileSession">The file session.</param>
        /// <returns>SearchAndReplaceHistory.</returns>
        public static SearchAndReplaceHistory AddOrUpdateAndReplaceHistory(string text,
            SearchAndReplaceSearchType searchAndReplaceSearchType, SearchAndReplaceType searchAndReplaceType,
            bool caseSensitive, FileSession fileSession)
        {
            try
            {
                var context = ScriptNotepadDbContext.DbContext;

                if (!context.SearchAndReplaceHistories.Any(f =>
                    f.SearchOrReplaceText == text && 
                    f.SearchAndReplaceSearchType.HasFlag(searchAndReplaceSearchType) && 
                    f.SearchAndReplaceType == searchAndReplaceType &&
                    f.CaseSensitive == caseSensitive &&
                    f.Session.SessionName == fileSession.SessionName))
                {
                    var result = new SearchAndReplaceHistory
                    {
                        SearchOrReplaceText = text, 
                        SearchAndReplaceSearchType = searchAndReplaceSearchType, 
                        SearchAndReplaceType = searchAndReplaceType, 
                        CaseSensitive = caseSensitive,
                        Session = fileSession,
                    };

                    result = context.SearchAndReplaceHistories.Add(result);
                    context.SaveChanges();
                    return result;
                }

                return context.SearchAndReplaceHistories.FirstOrDefault(f =>
                    f.SearchOrReplaceText == text && 
                    f.SearchAndReplaceSearchType.HasFlag(searchAndReplaceSearchType) && 
                    f.SearchAndReplaceType == searchAndReplaceType &&
                    f.CaseSensitive == caseSensitive &&
                    f.Session.SessionName == fileSession.SessionName); // success..
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return null; // failure..
            }
        }
    }
}
