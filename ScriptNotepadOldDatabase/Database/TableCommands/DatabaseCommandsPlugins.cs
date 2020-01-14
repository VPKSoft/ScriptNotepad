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
using ScriptNotepadOldDatabase.Database.Tables;
using ScriptNotepadOldDatabase.Database.UtilityClasses;

namespace ScriptNotepadOldDatabase.Database.TableCommands
{
    /// <summary>
    /// A class which is used to formulate SQL sentences for the <see cref="Database"/> class for the PLUGINS database table.
    /// </summary>
    internal class DatabaseCommandsPlugins: DataFormulationHelpers
    {
        /// <summary>
        /// Generates a SQL sentence to insert a plug-in entry into the 
        /// </summary>
        /// <param name="plugin">A plug-in to insert into the </param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        public static string GenPluginInsert(PLUGINS plugin)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"INSERT INTO PLUGINS(FILENAME_FULL, FILENAME, FILEPATH, PLUGIN_NAME, PLUGIN_VERSION, PLUGIN_DESCTIPTION,",
                $"ISACTIVE, EXCEPTION_COUNT, LOAD_FAILURES, APPLICATION_CRASHES, SORTORDER, RATING,",
                $"PLUGIN_INSTALLED, PLUGIN_UPDATED, PENDING_DELETION)",
                $"SELECT",
                $"{QS(plugin.FILENAME_FULL)},",
                $"{QS(plugin.FILENAME)},",
                $"{QS(plugin.FILEPATH)},",
                $"{QS(plugin.PLUGIN_NAME)},",
                $"{QS(plugin.PLUGIN_VERSION)},",
                $"{QS(plugin.PLUGIN_DESCTIPTION)},",
                $"{BS(plugin.ISACTIVE)},",
                $"{plugin.EXCEPTION_COUNT},",
                $"{plugin.LOAD_FAILURES},",
                $"{plugin.APPLICATION_CRASHES},",
                $"{plugin.SORTORDER},",
                $"{plugin.RATING},",
                $"{DateToDBString(plugin.PLUGIN_INSTALLED)},",
                $"{DateToDBString(plugin.PLUGIN_UPDATED)},",
                $"{BS(plugin.PENDING_DELETION)}",
                $"WHERE NOT EXISTS(SELECT * FROM PLUGINS WHERE FILENAME = {QS(plugin.FILENAME)});");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to update a plug-in on the 
        /// </summary>
        /// <param name="plugin">A plug-in to be updated on the </param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenPluginUpdate(PLUGINS plugin)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"UPDATE PLUGINS SET",
                $"FILENAME_FULL = {QS(plugin.FILENAME_FULL)},",
                $"FILENAME = {QS(plugin.FILENAME)},",
                $"FILEPATH = {QS(plugin.FILEPATH)},",
                $"PLUGIN_NAME = {QS(plugin.PLUGIN_NAME)},",
                $"PLUGIN_VERSION = {QS(plugin.PLUGIN_VERSION)},",
                $"PLUGIN_DESCTIPTION = {QS(plugin.PLUGIN_DESCTIPTION)},",
                $"ISACTIVE = {BS(plugin.ISACTIVE)},",
                $"EXCEPTION_COUNT = {plugin.EXCEPTION_COUNT},",
                $"LOAD_FAILURES = {plugin.LOAD_FAILURES},",
                $"APPLICATION_CRASHES = {plugin.APPLICATION_CRASHES},",
                $"SORTORDER = {plugin.SORTORDER},",
                $"RATING = {plugin.RATING},",
                $"PLUGIN_UPDATED = {DateToDBString(plugin.PLUGIN_UPDATED)},",
                $"PENDING_DELETION = {BS(plugin.PENDING_DELETION)}",
                $"WHERE ID = {plugin.ID};");

            return sql;
        }

        /// <summary>
        /// Generates a SQL sentence to select code plug-ins from the PLUGINS table in the 
        /// </summary>
        /// <returns>A generated SQL sentence.</returns>
        internal static string GenPluginSelect()
        {
            // ID: 0, FILENAME_FULL: 1, FILENAME: 2, FILEPATH: 3, PLUGIN_NAME: 4, PLUGIN_VERSION: 5,
            // PLUGIN_DESCTIPTION: 6, ISACTIVE: 7, EXCEPTION_COUNT: 8,
            // LOAD_FAILURES : 9, APPLICATION_CRASHES: 10, SORTORDER: 11,
            // RATING: 12, PLUGIN_INSTALLED: 13, PLUGIN_UPDATED: 14, PENDING_DELETION: 15
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT ID, FILENAME_FULL, FILENAME,",
                $"FILEPATH, PLUGIN_NAME, PLUGIN_VERSION,",
                $"PLUGIN_DESCTIPTION, ISACTIVE, EXCEPTION_COUNT,",
                $"LOAD_FAILURES, APPLICATION_CRASHES, SORTORDER,",
                $"RATING, PLUGIN_INSTALLED, PLUGIN_UPDATED, PENDING_DELETION",
                $"FROM PLUGINS",
                $"ORDER BY SORTORDER, RATING DESC, PLUGIN_NAME COLLATE NOCASE, PLUGIN_DESCTIPTION COLLATE NOCASE;");

            return sql;
        }

        /// <summary>
        /// Generates the existing database plug-in identifier sentence.
        /// </summary>
        /// <param name="plugin">A PLUGINS class instance to be used for the SQL sentence generation.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GetExistingPluginIDSentence(PLUGINS plugin)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"SELECT ID FROM PLUGINS",
                $"WHERE",
                $"WHERE FILENAME = {QS(plugin.FILENAME)};");

            return sql;
        }

        /// <summary>
        /// Generates the delete plug-in SQL sentence.
        /// </summary>
        /// <param name="plugin">The plug-in to delete from the database.</param>
        /// <returns>A generated SQL sentence based on the given parameters.</returns>
        internal static string GenDeletePluginSentence(PLUGINS plugin)
        {
            string sql =
                string.Join(Environment.NewLine,
                $"DELETE FROM PLUGINS",
                $"WHERE",
                $"ID = {plugin.ID} OR",
                $"FILENAME = {QS(plugin.FILENAME)};");

            return sql;
        }
    }
}
