#region License
/*
MIT License

Copyright(c) 2019 Petteri Kautonen

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
using ScriptNotepad.Database.Tables;
using System.Windows.Forms;
using VPKSoft.LangLib;
using VPKSoft.ScintillaTabbedTextControl;

namespace ScriptNotepad.Localization
{
    /// <summary>
    /// A helper class to set localized status tool strip status label texts.
    /// </summary>
    public class StatusStripTexts
    {
        /// <summary>
        /// Gets or sets a status label for a line and a column.
        /// </summary>
        public static ToolStripStatusLabel LabelLineColumn { get; set; }

        /// <summary>
        /// Gets or sets a status label for a line, a column and a selection length of a selection.
        /// </summary>
        public static ToolStripStatusLabel LabelLineColumnSelection { get; set; }

        /// <summary>
        /// Gets or sets a status label for a count of lines in a document and the size of the document in characters.
        /// </summary>
        public static ToolStripStatusLabel LabelDocumentLinesSize { get; set; }

        /// <summary>
        /// Gets or sets a status label for a line ending type text.
        /// </summary>
        public static ToolStripStatusLabel LabelLineEnding { get; set; }

        /// <summary>
        /// Gets or sets a status label for a file encoding name text.
        /// </summary>
        public static ToolStripStatusLabel LabelEncoding { get; set; }

        /// <summary>
        /// Gets or sets a status label for a session name text.
        /// </summary>
        public static ToolStripStatusLabel LabelSessionName { get; set; }

        /// <summary>
        /// Gets or sets a status label indicating whether a document editor is in insert or in override mode text.
        /// </summary>
        public static ToolStripStatusLabel LabelModeInsertOverride { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the status strip labels have been assigned.
        /// </summary>
        public static bool Initialized
        {
            get => LabelLineColumn != null &&
                    LabelLineColumnSelection != null &&
                    LabelDocumentLinesSize != null &&
                    LabelLineEnding != null &&
                    LabelEncoding != null &&
                    LabelSessionName != null &&
                    LabelModeInsertOverride != null;

            set
            {
                if (!value)
                {
                    UninitLables();
                }
            }
        }

        /// <summary>
        /// Initializes the status strip labels with given values.
        /// </summary>
        /// <param name="labelLineColumn">The status label for a line and a column.</param>
        /// <param name="labelLineColumnSelection">The status label for a line, a column and a selection length of a selection.</param>
        /// <param name="labelDocumentLinesSize">The status label for a count of lines in a document and the size of the document in characters.</param>
        /// <param name="labelLineEnding">The status label for a line ending type text.</param>
        /// <param name="labelEncoding">The status label for a file encoding name text.</param>
        /// <param name="labelSessionName">The status label for a session name text.</param>
        /// <param name="labelModeInsertOverride">The status label indicating whether a document editor is in insert or in override mode text.</param>
        public static void InitLabels(
            ToolStripStatusLabel labelLineColumn,
            ToolStripStatusLabel labelLineColumnSelection,
            ToolStripStatusLabel labelDocumentLinesSize,
            ToolStripStatusLabel labelLineEnding,
            ToolStripStatusLabel labelEncoding,
            ToolStripStatusLabel labelSessionName,
            ToolStripStatusLabel labelModeInsertOverride)
        {
            LabelLineColumn = labelLineColumn;
            LabelLineColumnSelection = labelLineColumnSelection;
            LabelDocumentLinesSize = labelDocumentLinesSize;
            LabelLineEnding = labelLineEnding;
            LabelEncoding = labelEncoding;
            LabelSessionName = labelSessionName;
            LabelModeInsertOverride = labelModeInsertOverride;
        }

        /// <summary>
        /// Un-initializes the status strip labels with given values.
        /// </summary>
        public static void UninitLables()
        {
            LabelLineColumn = null;
            LabelLineColumnSelection = null;
            LabelDocumentLinesSize = null;
            LabelLineEnding = null;
            LabelEncoding = null;
            LabelSessionName = null;
            LabelModeInsertOverride = null;
        }

        /// <summary>
        /// Sets the name of the session for the corresponding status strip label.
        /// </summary>
        /// <param name="sessionName">Name of the session to set for the label.</param>
        public static void SetSessionName(string sessionName)
        {
            // the tool strip labels must have been assigned..
            if (!Initialized)
            {
                // ..so to avoid a null reference just return..
                return;
            }

            LabelSessionName.Text =
                DBLangEngine.GetStatMessage("msgSessionName", "Session: {0}|A message describing a session name with the name as a parameter", sessionName);
        }

        /// <summary>
        /// Sets the document size status strip label's text.
        /// </summary>
        /// <param name="document">The <see cref="ScintillaTabbedDocument"/> document of which properties to use to set the status strip values to indicate.</param>
        public static void SetDocumentSizeText(ScintillaTabbedDocument document)
        {
            // the tool strip labels must have been assigned..
            if (!Initialized)
            {
                // ..so to avoid a null reference just return..
                return;
            }

            LabelDocumentLinesSize.Text =
                DBLangEngine.GetStatMessage("msgDocSizeLines", "length: {0}  lines: {1}, pos: {2}|As in the ScintillaNET document size in lines and in characters and the current location in characters",
                document.Scintilla.Text.Length,
                document.Scintilla.Lines.Count, document.Scintilla.CurrentPosition);
        }

        /// <summary>
        /// Sets the main status strip values for the currently active document..
        /// </summary>
        /// <param name="document">The <see cref="ScintillaTabbedDocument"/> document of which properties to use to set the status strip values to indicate.</param>
        /// <param name="sessionName">Name of the session to set for the label.</param>
        public static void SetStatusStringText(ScintillaTabbedDocument document, string sessionName)
        {
            // first check the parameter validity..
            if (document == null)
            {
                return;
            }

            // the tool strip labels must have been assigned..
            if (!Initialized)
            {
                // ..so to avoid a null reference just return..
                return;
            }

            LabelLineColumn.Text =
                DBLangEngine.GetStatMessage("msgColLine", "Line: {0}  Col: {1}|As in the current column and the current line in a ScintillaNET control",
                document.LineNumber + 1, document.Column + 1);

            LabelLineColumnSelection.Text =
                DBLangEngine.GetStatMessage("msgColLineSelection", "Sel1: {0}|{1}  Sel2: {2}|{3}  Len: {4}|The selection start, end and length in a ScintillaNET control in columns, lines and character count",
                document.SelectionStartLine + 1,
                document.SelectionStartColumn + 1,
                document.SelectionEndLine + 1,
                document.SelectionEndColumn + 1,
                document.SelectionLength);

            SetDocumentSizeText(document);

            LabelLineEnding.Text = string.Empty;

            if (document.Tag != null)
            {
                DBFILE_SAVE fileSave = (DBFILE_SAVE)document.Tag;

                LabelLineEnding.Text = fileSave.FileLineEndingText;

                LabelEncoding.Text =
                    DBLangEngine.GetStatMessage("msgShortEncodingPreText", "Encoding: |A short text to describe a detected encoding value (i.e.) Unicode (UTF-8).") +
                    fileSave.ENCODING.EncodingName;

                LabelSessionName.Text =
                    DBLangEngine.GetStatMessage("msgSessionName", "Session: {0}|A message describing a session name with the name as a parameter", sessionName);
            }

            // set the insert / override text for the status strip..
            SetInsertOverrideStatusStripText(document, false);
        }

        /// <summary>
        /// Sets the main status strip value for insert / override mode for the currently active document..
        /// </summary>
        /// <param name="document">The <see cref="ScintillaTabbedDocument"/> document of which properties to use to set the status strip values to indicate.</param>
        /// <param name="isKeyPreview">A flag indicating whether the software captured the change before the control; thus indicating an inverted value.</param>
        public static void SetInsertOverrideStatusStripText(ScintillaTabbedDocument document, bool isKeyPreview)
        {
            // the tool strip labels must have been assigned..
            if (!Initialized)
            {
                // ..so to avoid a null reference just return..
                return;
            }

            if (isKeyPreview)
            {
                LabelModeInsertOverride.Text =
                    !document.Scintilla.Overtype ?
                        DBLangEngine.GetStatMessage("msgOverrideShort", "Cursor mode: OVR|As in the text to be typed to the Scintilla would override the underlying text") :
                        DBLangEngine.GetStatMessage("msgInsertShort", "Cursor mode: INS|As in the text to be typed to the Scintilla would be inserted within the already existing text");
            }
            else
            {
                LabelModeInsertOverride.Text =
                    document.Scintilla.Overtype ?
                        DBLangEngine.GetStatMessage("msgOverrideShort", "Cursor mode: OVR|As in the text to be typed to the Scintilla would override the underlying text") :
                        DBLangEngine.GetStatMessage("msgInsertShort", "Cursor mode: INS|As in the text to be typed to the Scintilla would be inserted within the already existing text");
            }
        }

        /// <summary>
        /// Sets the main status strip values to indicate there is no active document.
        /// </summary>
        /// <param name="sessionName">Name of the session to set for the label.</param>
        public static void SetEmptyTexts(string sessionName)
        {
            // the tool strip labels must have been assigned..
            if (!Initialized)
            {
                // ..so to avoid a null reference just return..
                return;
            }

            LabelLineColumn.Text =
                DBLangEngine.GetStatMessage("msgColLine", "Line: {0}  Col: {1}|As in the current column and the current line in a ScintillaNET control",
                1, 1);

            LabelLineColumnSelection.Text =
                DBLangEngine.GetStatMessage("msgColLineSelection", "Sel1: {0}|{1}  Sel2: {2}|{3}  Len: {4}|The selection start, end and length in a ScintillaNET control in columns, lines and character count",
                1, 1, 1, 1, 0);

            LabelLineEnding.Text =
                DBLangEngine.GetStatMessage("msgLineEndingShort", "LE: |A short message indicating a file line ending type value(s) as a concatenated text") +
                $" ({DBLangEngine.GetStatMessage("msgNA", "N/A|A message indicating a none value")})";

            LabelEncoding.Text =
                DBLangEngine.GetStatMessage("msgShortEncodingPreText", "Encoding: |A short text to describe a detected encoding value (i.e.) Unicode (UTF-8).") +
                DBLangEngine.GetStatMessage("msgNA", "N/A|A message indicating a none value");

            LabelSessionName.Text =
                DBLangEngine.GetStatMessage("msgSessionName", "Session: {0}|A message describing a session name with the name as a parameter", sessionName);

            LabelModeInsertOverride.Text =
                    DBLangEngine.GetStatMessage("msgInsertShort", "Cursor mode: INS|As in the text to be typed to the Scintilla would be inserted within the already existing text");

        }
    }
}
