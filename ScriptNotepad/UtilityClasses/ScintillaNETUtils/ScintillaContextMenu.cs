﻿#region License
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
using System.Windows.Forms;
using ScintillaNET;
using VPKSoft.LangLib;

// (C)::Loosely based on the article: https://github.com/jacobslusser/ScintillaNET/issues/334

namespace ScriptNotepad.UtilityClasses.ScintillaNETUtils
{
    /// <summary>
    /// A class used internally by the <see cref="ScintillaContextMenu"/> class for data passing.
    /// </summary>
    public class ToolStripTagItem
    {
        /// <summary>
        /// Gets or sets the function identifier as an integer.
        /// </summary>
        public int FunctionId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Scintilla"/>.
        /// </summary>
        public Scintilla Scintilla { get; set; }
    }

    /// <summary>
    /// A helper class to create a modifiable context a menu to a <see cref="Scintilla"/> class.
    /// </summary>
    public class ScintillaContextMenu
    {
        /// <summary>
        /// Gets or sets the text for undo tool strip menu item for localization.
        /// </summary>
        public static string TextUndo { get; set; } = "Undo";

        /// <summary>
        /// Gets or sets the text for redo tool strip menu item for localization.
        /// </summary>
        public static string TextRedo { get; set; } = "Redo";

        /// <summary>
        /// Gets or sets the text for cot tool strip menu item for localization.
        /// </summary>
        public static string TextCut { get; set; } = "Cut";

        /// <summary>
        /// Gets or sets the text for paste tool strip menu item for localization.
        /// </summary>
        public static string TextPaste { get; set; } = "Paste";

        /// <summary>
        /// Gets or sets the text for delete tool strip menu item for localization.
        /// </summary>
        public static string TextDelete { get; set; } = "Delete";

        /// <summary>
        /// Gets or sets the text for select all tool strip menu item for localization.
        /// </summary>
        public static string TextSelectAll { get; set; } = "Select All";

        /// <summary>
        /// Localizes the texts used to build the <see cref="ContextMenuStrip"/> with the <seealso cref="CreateBasicContextMenuStrip"/> method.
        /// This should be called before any context menu strips have been created but after the <see cref="DBLangEngine"/> has been initialized.
        /// </summary>
        public static void LocalizeTexts()
        {
            TextUndo = DBLangEngine.GetStatMessage("msgContextUndo",
                "Undo|A message for a context menu to describe an undo action");

            TextRedo = DBLangEngine.GetStatMessage("msgContextRedo",
                "Redo|A message for a context menu to describe a redo action");

            TextCut = DBLangEngine.GetStatMessage("msgContextCut",
                "Cut|A message for a context menu to describe a cut action");

            TextPaste = DBLangEngine.GetStatMessage("msgContextPaste",
                "Paste|A message for a context menu to describe a paste action");

            TextDelete = DBLangEngine.GetStatMessage("msgContextDelete",
                "Delete|A message for a context menu to describe a delete text action");

            TextSelectAll = DBLangEngine.GetStatMessage("msgContextSelectAll",
                "Select All|A message for a context menu to describe a select all text action");
        }

        /// <summary>
        /// Creates the basic localized context menu strip for a given <see cref="Scintilla"/>.
        /// </summary>
        /// <param name="scintilla">The Scintilla to add the <see cref="ContextMenuStrip"/> to.</param>
        /// <param name="onUndo">An action which is called when the menu invokes undo action for the <see cref="Scintilla"/>.</param>
        /// <param name="onRedo">An action which is called when the menu invokes redo action for the <see cref="Scintilla"/>.</param>
        /// <returns>The created <see cref="ContextMenuStrip"/>.</returns>
        public static ContextMenuStrip CreateBasicContextMenuStrip(Scintilla scintilla, Action<Scintilla> onUndo, Action<Scintilla> onRedo)
        {
            // initialize a new instance of a ContextMenuStrip class with Tag property
            // value of the given Scintilla instance as a parameter..
            ContextMenuStrip contextMenu = new ContextMenuStrip() {Tag = scintilla};

            // create an undo ToolStripMenuItem..
            contextMenu.Items.Add(new ToolStripMenuItem(TextUndo, null,
                    (sender, e) =>
                    {
                        var tagItem = ((ToolStripTagItem) ((ToolStripItem) sender).Tag);
                        tagItem.Scintilla.Undo();
                        onUndo(tagItem.Scintilla);
                    })
                {Tag = new ToolStripTagItem {FunctionId = 0, Scintilla = scintilla}});

            // create a redo ToolStripMenuItem..
            contextMenu.Items.Add(new ToolStripMenuItem(TextRedo, null,
                    (sender, e) =>
                    {
                        var tagItem = ((ToolStripTagItem) ((ToolStripItem) sender).Tag);
                        tagItem.Scintilla.Redo();
                        onRedo(tagItem.Scintilla);
                    })
                {Tag = new ToolStripTagItem {FunctionId = 1, Scintilla = scintilla}});

            // create a tool strip separator..
            contextMenu.Items.Add(new ToolStripSeparator());

            // create a cut ToolStripMenuItem..
            contextMenu.Items.Add(new ToolStripMenuItem(TextCut, null,
                    (sender, e) => ((ToolStripTagItem) ((ToolStripItem) sender).Tag).Scintilla.Cut())
                {Tag = new ToolStripTagItem {FunctionId = 2, Scintilla = scintilla}});

            // create a paste ToolStripMenuItem..
            contextMenu.Items.Add(new ToolStripMenuItem(TextPaste, null,
                    (sender, e) => ((ToolStripTagItem) ((ToolStripItem) sender).Tag).Scintilla.Paste())
                {Tag = new ToolStripTagItem {FunctionId = 3, Scintilla = scintilla}});

            // create a delete ToolStripMenuItem..
            contextMenu.Items.Add(new ToolStripMenuItem(TextDelete, null,
                    (sender, e) => ((ToolStripTagItem) ((ToolStripItem) sender).Tag).Scintilla.ReplaceSelection(""))
                {Tag = new ToolStripTagItem {FunctionId = 4, Scintilla = scintilla}});

            // create a tool strip separator..
            contextMenu.Items.Add(new ToolStripSeparator());

            // create a select all ToolStripMenuItem..
            contextMenu.Items.Add(new ToolStripMenuItem(TextSelectAll, null,
                    (sender, e) => ((ToolStripTagItem) ((ToolStripItem) sender).Tag).Scintilla.SelectAll())
                {Tag = new ToolStripTagItem {FunctionId = 5, Scintilla = scintilla}});

            // set the Scintilla's ContextMenuStrip property value to the just
            // created ContextMenuStrip instance..
            scintilla.ContextMenuStrip = contextMenu;

            // subscribe the opening event for the context menu to enable or disable
            // the basic menu items depending on the state of the Scintilla instance..
            scintilla.ContextMenuStrip.Opening += ContextMenuStrip_Opening;

            // return the just created ContextMenuStrip instance..
            return contextMenu;
        }

        /// <summary>
        /// Unsubscribes the events added to the created context menu item.
        /// </summary>
        /// <param name="scintilla">An instance to a <see cref="Scintilla"/> class of which <see cref="ContextMenuStrip"/> event subscriptions to unsubscribe.</param>
        public static void UnsubscribeEvents(Scintilla scintilla)
        {
            // only if not null..
            if (scintilla.ContextMenuStrip != null)
            {
                // ..unsubscribe the event(s)..
                scintilla.ContextMenuStrip.Opening -= ContextMenuStrip_Opening;
            }
        }

        /// <summary>
        /// Handles the Opening event of the ContextMenuStrip control. This enables or disables the basic context menu items for the <see cref="Scintilla"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private static void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // get the ContextMenuStrip instance from the sender..
            ContextMenuStrip contextMenu = (ContextMenuStrip) sender; 

            // loop through the context menu strip items..
            for (int i = 0; i < contextMenu.Items.Count; i++)
            {
                // if the item is constructed by this class the item's Tag contains a ToolStripTagItem class instance..
                if (contextMenu.Items[i].Tag != null && contextMenu.Items[i].Tag.GetType() == typeof(ToolStripTagItem))
                {
                    // get the ToolStripTagItem class instance..
                    ToolStripTagItem tagItem = (ToolStripTagItem) contextMenu.Items[i].Tag;

                    // based on the ToolStripTagItem class instance property of FunctionId and the state of
                    // the Scintilla enable or disable the ToolStripMenuItems accordingly..
                    switch (tagItem.FunctionId) 
                    {
                        // if the Scintilla instance can undo, then the undo item is enabled..
                        case 0: contextMenu.Items[i].Enabled = tagItem.Scintilla.CanUndo; break;

                        // if the Scintilla instance can redo, then the redo item is enabled..
                        case 1: contextMenu.Items[i].Enabled = tagItem.Scintilla.CanRedo; break;

                        // if the Scintilla instance has text selected then the cut item is enabled..
                        case 2: contextMenu.Items[i].Enabled = tagItem.Scintilla.SelectedText.Length > 0; break;

                        // if the Scintilla instance can paste text from the clipboard, then the paste item is enabled..
                        case 3: contextMenu.Items[i].Enabled = tagItem.Scintilla.CanPaste; break;

                        // if the Scintilla instance has text selected then the delete item is enabled..
                        case 4: contextMenu.Items[i].Enabled = tagItem.Scintilla.SelectedText.Length > 0; break;

                        // if the Scintilla instance has any text, then the select all item is enabled..
                        case 5: contextMenu.Items[i].Enabled = tagItem.Scintilla.Text.Length > 0; break;
                    }
                }
            }
        }
    }
}
