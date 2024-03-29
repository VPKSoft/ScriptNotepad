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
using ScriptNotepad.Database.Entity.Enumerations;

namespace ScriptNotepad.Database.Entity.Entities;

/// <summary>
/// A class for storing code snippets into the database.
/// Implements the <see cref="ScriptNotepad.Database.Entity.IEntity" />
/// </summary>
/// <seealso cref="ScriptNotepad.Database.Entity.IEntity" />
[Table("CodeSnippets")]
public class CodeSnippet: IEntity
{
    /// <summary>
    /// Gets or sets the identifier for the entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the script's contents.
    /// </summary>
    public string? ScriptContents { get; set; }

    /// <summary>
    /// Gets or sets the name of the script.
    /// </summary>
    public string ScriptName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the script was previously modified.
    /// </summary>
    public DateTime Modified { get; set; }

    /// <summary>
    /// Gets or sets the language type of the script snippet.
    /// </summary>
    public CodeSnippetLanguage ScriptLanguage { get; set; }

    /// <summary>
    /// Gets or sets the type of the script text manipulation.
    /// </summary>
    public ScriptSnippetType ScriptTextManipulationType  { get; set; }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
        return ScriptName;
    }
}

#nullable restore
