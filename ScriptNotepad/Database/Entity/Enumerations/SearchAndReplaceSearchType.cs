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

namespace ScriptNotepad.Database.Entity.Enumerations;

/// <summary>
/// An enumeration for a text search type.
/// </summary>
[Flags]
public enum SearchAndReplaceSearchType
{
    /// <summary>
    /// A normal text search.
    /// </summary>
    Normal = 1,

    /// <summary>
    /// An extended text search.
    /// </summary>
    Extended = 2,

    /// <summary>
    /// A text search using regular expressions.
    /// </summary>
    RegularExpression = 4,

    /// <summary>
    /// The simple text search with some additional extended formatting possibilities.
    /// </summary>
    SimpleExtended = 8,

    /// <summary>
    /// All the possibilities combined.
    /// </summary>
    All = Normal | Extended | RegularExpression | SimpleExtended,
}