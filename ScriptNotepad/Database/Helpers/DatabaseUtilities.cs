#region License
/*
MIT License

Copyright(c) 2021 Petteri Kautonen

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

namespace ScriptNotepad.Database.Helpers
{
    /// <summary>
    /// Some database utilities for the <see cref="ScriptNotepadDbContext"/>.
    /// </summary>
    public static class DatabaseUtilities
    {
        /// <summary>
        /// Sets the miscellaneous parameter to the database.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="value">The value to set.</param>
        /// <returns><c>true</c> if the value is already set, <c>false</c> otherwise.</returns>
        public static bool SetMiscellaneousParameter(this ScriptNotepadDbContext context, string value)
        {
            if (!context.MiscellaneousParameters.Any(f => f.Value.Equals(value, StringComparison.Ordinal)))
            {
                return true;
            }

            context.MiscellaneousParameters.Add(new MiscellaneousParameters {Added = DateTime.Now, Value = value});
            context.SaveChanges();
            return false;
        }
    }
}
