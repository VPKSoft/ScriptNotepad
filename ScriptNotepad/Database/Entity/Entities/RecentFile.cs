﻿#region License
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

#nullable enable

using System.ComponentModel.DataAnnotations.Schema;

namespace ScriptNotepad.Database.Entity.Entities;

/// <summary>
/// A class for storing recent file data into the database.
/// Implements the <see cref="ScriptNotepad.Database.Entity.IEntity" />
/// </summary>
/// <seealso cref="ScriptNotepad.Database.Entity.IEntity" />
[Table("RecentFiles")]
public class RecentFile: IEntity
{
    /// <summary>
    /// Gets or sets the identifier for the entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the session identifier.
    /// </summary>
    /// <value>The session identifier.</value>
    public int SessionId { get; set; }

    /// <summary>
    /// Gets or sets the full file name with path.
    /// </summary>
    public string FileNameFull { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file name without path.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the full path for the file.
    /// </summary>
    public string? FilePath { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the file was closed in the editor.
    /// </summary>
    public DateTime ClosedDateTime { get; set; }

    /// <summary>
    /// Gets or sets the session the recent file belongs to.
    /// </summary>
    [ForeignKey(nameof(SessionId))]
    public virtual FileSession? Session { get; set; }

    /// <summary>
    /// Gets or sets a string representing the encoding of the file save.
    /// </summary>
    public string EncodingAsString { get; set; } = "utf-8;65001;True;False;False";

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    public override string ToString()
    {
        return FileNameFull;
    }
}

#nullable restore