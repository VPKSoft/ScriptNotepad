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

using ScintillaNET;
using System.Collections.Generic;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace;

/// <summary>
/// Event arguments for the search and replace dialog to be able to interact with the main form.
/// </summary>
/// <seealso cref="System.EventArgs" />
public class ScintillaDocumentEventArgs: EventArgs
{
    /// <summary>
    /// Gets or sets a value indicating whether all the open documents are requested.
    /// </summary>
    public bool RequestAllDocuments { get; set; } = false;

    /// <summary>
    /// Gets or sets the <see cref="Scintilla"/> documents requested by an event.
    /// </summary>
    public List<(Scintilla scintilla, string fileName)> Documents { get; set; } = new List<(Scintilla scintilla, string fileName)>();
}