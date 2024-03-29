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

using System.ComponentModel.DataAnnotations.Schema;
using ScriptNotepad.Database.Entity.Enumerations;

#nullable enable

namespace ScriptNotepad.Database.Entity.Entities;

/// <summary>
/// A class for storing miscellaneous text data into the database.
/// Implements the <see cref="ScriptNotepad.Database.Entity.IEntity" />
/// </summary>
/// <seealso cref="ScriptNotepad.Database.Entity.IEntity" />
[Table("MiscellaneousTextEntries")]
public class MiscellaneousTextEntry: IEntity
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
    /// Gets or sets the text value.
    /// </summary>
    public string TextValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the text.
    /// </summary>
    public MiscellaneousTextType TextType { get; set; }

    /// <summary>
    /// Gets or sets the added date and time when the entry was added or updated to the database.
    /// </summary>
    public DateTime Added { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Session"/> this miscellaneous text data belongs to.
    /// </summary>
    [ForeignKey(nameof(SessionId))]
    public virtual FileSession? Session { get; set; }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
        return TextValue;
    }
}

#nullable restore