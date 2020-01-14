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
using SQLite.CodeFirst;

namespace ScriptNotepad.Database.Entity.Entities
{
    /// <summary>
    /// A history class which was used in a sample from the SQLite CodeFirst sample (https://github.com/msallin/SQLiteCodeFirst/tree/master/SQLite.CodeFirst.Console).
    /// The use of this class is unknown to me an I'll let it be that way.
    /// Implements the <see cref="SQLite.CodeFirst.IHistory" />
    /// </summary>
    /// <seealso cref="SQLite.CodeFirst.IHistory" />
    public class CustomHistory: IHistory
    {
        /// <summary>
        /// Gets or sets the identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the change (?) hash algorithm value as string.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the database context type name (SQLiteEntityFramework.SQLiteDbContext).
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
