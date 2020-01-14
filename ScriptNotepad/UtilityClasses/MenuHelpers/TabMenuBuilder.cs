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
using System.Windows.Forms;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using VPKSoft.ScintillaTabbedTextControl;

namespace ScriptNotepad.UtilityClasses.MenuHelpers
{
    class TabMenuBuilder : ErrorHandlingBase, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WinFormsFormMenuBuilder"/> class.
        /// </summary>
        /// <param name="mainItem">The main menu item to the add the open forms within the <see cref="Application"/>.</param>
        /// <param name="tabbedTextControl">The main form's <see cref="ScintillaTabbedTextControl"/> class instance.</param>
        public TabMenuBuilder(ToolStripMenuItem mainItem, ScintillaTabbedTextControl tabbedTextControl)
        {
            // save the Tab menu for further use..
            this.mainItem = mainItem;

            this.tabbedTextControl = tabbedTextControl;

            // subscribe to the Tab menu's opening event..
            mainItem.DropDownOpening += MainItem_DropDownOpening;
        }

        private void MainItem_DropDownOpening(object sender, EventArgs e)
        {
            CreateMenuOpenTabs();
        }

        /// <summary>
        /// Clears the previous drop-down items from the main menu item.
        /// </summary>
        private void ClearPreviousMenu()
        {
            // reverse loop..
            for (int i = mainItem.DropDownItems.Count - 1; i >= 0; i--)
            {
                // un-subscribe the click event handler..
                mainItem.DropDownItems[i].Click -= Item_Click;
            }

            // clear the items..
            mainItem.DropDownItems.Clear();
        }

        /// <summary>
        /// Creates the menu of the open forms within the <see cref="tabbedTextControl"/>.
        /// </summary>
        internal void CreateMenuOpenTabs()
        {
            // clear the previously created menu..
            ClearPreviousMenu();


            // loop through the open tabs in the ScintillaTabbedTextControl..
            foreach (ScintillaTabbedDocument document in tabbedTextControl.Documents)
            {
                // create a new ToolStripMenuItem for the tab..
                ToolStripMenuItem item = new ToolStripMenuItem(document.FileNameNotPath)
                    {Tag = document, Checked = document.Equals(tabbedTextControl.CurrentDocument)};

                // subscribe to the click event..
                item.Click += Item_Click;

                // ad the item to the main menu item's DropDownItems collection..
                mainItem.DropDownItems.Add(item);
            }

            mainItem.Enabled = tabbedTextControl.DocumentsCount > 0;
        }


        private void Item_Click(object sender, EventArgs e)
        {
            var menu = (ToolStripMenuItem) sender;
            var document = (ScintillaTabbedDocument) menu.Tag;
            int docIndex = tabbedTextControl.Documents.FindIndex(f => f.ID == document.ID);
            if (docIndex != -1)
            {
                tabbedTextControl.ActivateDocument(docIndex);
            }
        }

        public void Dispose()
        {
            // clear the previously created menu..
            ClearPreviousMenu();

            // unsubscribe the events subscribed by this class instance..
            mainItem.DropDownOpening -= MainItem_DropDownOpening;
        }

        /// <summary>
        /// A field to hold the main menu item for the open Tabs within the <see cref="ScintillaTabbedTextControl"/>.
        /// </summary>
        private readonly ToolStripMenuItem mainItem;

        /// <summary>
        /// A field to hold the main menu item for the open Tabs within the <see cref="ScintillaTabbedTextControl"/>.
        /// </summary>
        private readonly ScintillaTabbedTextControl tabbedTextControl;
    }
}
