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

using System.ComponentModel.DataAnnotations.Schema;
using VPKSoft.ScintillaLexers;

#nullable enable

namespace ScriptNotepad.Database.Entity.Entities;

/// <summary>
/// A class representing a single file save entry in the database.
/// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
/// Implements the <see cref="ScriptNotepad.Database.Entity.IEntity" />
/// </summary>
/// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
/// <seealso cref="ScriptNotepad.Database.Entity.IEntity" />
[Table("FileSaves")]
public class FileSave: IEntity
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
    /// Gets or sets a string representing the encoding of the file save.
    /// </summary>
    public string EncodingAsString { get; set; } = "utf-8;65001;True;False;False";

    /// <summary>
    /// Gets or sets a value indicating whether the file exists in the file system.
    /// </summary>
    public bool ExistsInFileSystem { get; set; }

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
    /// Gets or sets the value indicating when the file was modified in the file system.
    /// </summary>
    public DateTime FileSystemModified { get; set; }

    /// <summary>
    /// Gets or sets the value indicating when the file was saved to the file system by the software.
    /// </summary>
    public DateTime FileSystemSaved { get; set; }

    /// <summary>
    /// Gets or sets the value indicating when the file was modified in the database.
    /// </summary>
    public DateTime DatabaseModified { get; set; } = DateTime.MinValue;

    /// <summary>
    /// Gets or sets the lexer number with the ScintillaNET.
    /// </summary>
    public LexerEnumerations.LexerType LexerType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to use the file system to store the contents of the file instead of a database BLOB.
    /// </summary>
    public bool? UseFileSystemOnContents { get; set; }

    /// <summary>
    /// Gets or sets the location of the temporary file save in case the file changes are cached into the file system.
    /// </summary>
    public string? TemporaryFileSaveName { get; set; }

    /// <summary>
    /// Gets or sets the file contents.
    /// </summary>
    public byte[]? FileContents { get; set; }

    /// <summary>
    /// Gets or sets the visibility order (in a tabbed control).
    /// </summary>
    public int VisibilityOrder { get; set; } 

    /// <summary>
    /// Gets or sets a value indicating whether the file is activated in the tab control.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this entry is a history entry.
    /// </summary>
    public bool IsHistory { get; set; } = false;

    /// <summary>
    /// Gets or sets the current position (cursor / caret) of the file.
    /// </summary>
    public int CurrentCaretPosition { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to use spell check with this document.
    /// </summary>
    public bool UseSpellChecking { get; set; }

    /// <summary>
    /// Gets or sets the editor zoom value in percentage.
    /// </summary>
    public int EditorZoomPercentage { get; set; }

    /// <summary>
    /// Gets or sets the value containing the document folding data.
    /// </summary>
    /// <value>The value containing the document folding data.</value>
    public string FoldSave { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the session the <see cref="FileSave"/> belongs to.
    /// </summary>
    [ForeignKey(nameof(SessionId))]
    public virtual FileSession? Session { get; set; }
}

#nullable restore