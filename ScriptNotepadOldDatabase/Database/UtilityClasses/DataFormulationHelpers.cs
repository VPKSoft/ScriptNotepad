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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepadOldDatabase.Database.UtilityClasses
{
    /// <summary>
    /// A class to help formulate certain types of data to suitable format for written SQL.
    /// </summary>
    internal class DataFormulationHelpers
    {
        /// <summary>
        /// This method stands for Quoted String. Simply double-quote the "insides" of a string and add quotes to the both sides (').
        /// </summary>
        /// <param name="str">A string to 'quote'.</param>
        /// <returns>A 'quoted' string.</returns>
        internal static string QS(string str)
        {
            return "'" + str.Replace("'", "''") + "'"; // as simple as it can be..
        }

        /// <summary>
        /// Converts a boolean value to database-understandable format.
        /// </summary>
        /// <param name="boolean">A boolean value to a database-understandable format.</param>
        /// <returns>If <paramref name="boolean"/> is true then 1; otherwise 0.</returns>
        internal static string BS(bool boolean)
        {
            return boolean ? "1" : "0";
        }

        /// <summary>
        /// Converts a value type of T to string to be inserted into the database ('NI' stands for NULLIF).
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to be inserted to the database..</param>
        /// <returns>System.String; if the value is null a string "NULL" is returned. If the type of the value is string it's automatically quoted.</returns>
        internal static string NI<T>(T value)
        {
            // null is NULL..
            if (value == null)
            {
                return "NULL";
            }

            // a string needs to be quoted..
            if (value is string)
            {
                return (QS(value.ToString()));
            }

            // otherwise just return the value converted to a string..
            return value.ToString();
        }

        /// <summary>
        /// Converts a value type of T to string to be inserted into the database ('NI' stands for NULLIF) with correct equality comparer (IS NULL or = value).
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to be inserted to the database..</param>
        /// <returns>System.String; if the value is null a string "NULL" is returned. If the type of the value is string it's automatically quoted.</returns>
        internal static string NIS<T>(T value)
        {
            // null is NULL..
            if (value == null)
            {
                return "IS NULL";
            }

            // a string needs to be quoted..
            if (value is string)
            {
                return " = " + (QS(value.ToString()));
            }

            // otherwise just return the value converted to a string..
            return " = " +value.ToString();
        }

        /// <summary>
        /// Gets a DateTime value from a give string from the database.
        /// </summary>
        /// <param name="value">The date and time value as it's stored in to the database.</param>
        /// <returns>A DateTime value converted from a given string.</returns>
        internal static DateTime DateFromDBString(string value)
        {
            try
            {
                // try to parse the given date time string to a DateTime value and return it..
                DateTime result = DateTime.ParseExact(value, "yyyy-MM-dd HH':'mm':'ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

                return result;
            }
            catch
            {
                // the format was invalid, so return DateTime.MinValue..
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converts a given DateTime value to a quoted string.
        /// </summary>
        /// <param name="dateTime">The DateTime value to convert.</param>
        /// <returns>A quoted string converted from a given DateTime value.</returns>
        internal static string DateToDBString(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                // return "nothing" if the date and time value is either
                // the minimum value or the maximum value..
                return QS("0000-00-00 00:00:00.000");
            }
            else
            {
                // return a quoted string from the given the date and time value..

                string result = QS(dateTime.ToString("yyyy-MM-dd HH':'mm':'ss.fff", CultureInfo.InvariantCulture));
                return result;
            }
        }
    }
}
