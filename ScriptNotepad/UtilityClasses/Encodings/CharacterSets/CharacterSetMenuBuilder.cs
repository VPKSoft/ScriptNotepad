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
using System.Collections.Generic;
using System.Windows.Forms;
using ScriptNotepad.UtilityClasses.Common;

namespace ScriptNotepad.UtilityClasses.Encodings.CharacterSets
{
    /// <summary>
    /// A class to help to build a menu for known character sets (<see cref="EncodingCharacterSet"/>).
    /// </summary>
    public static class CharacterSetMenuBuilder
    {
        /// <summary>
        /// Creates the character set menu with encodings as sub menu items.
        /// </summary>
        /// <param name="parent">The parent tool strip menu item.</param>
        /// <param name="singleCodePageResults">A flag indicating if character sets containing only single encoding should be returned.</param>
        /// <param name="data">Additional data to be assigned to the encoding tool strip menu item.</param>
        public static void CreateCharacterSetMenu(ToolStripMenuItem parent, bool singleCodePageResults, object data)
        {
            // create an instance of the EncodingCharacterSet class..
            var encodingCharacterSet = new EncodingCharacterSet();

            // get the character sets contained in the EncodingCharacterSet class..
            var charSets = encodingCharacterSet.GetCharacterSetList(singleCodePageResults);

            // loop through the character sets..
            foreach (var item in charSets)
            {
                // create a "dummy" menu to contain the actual encodings for the character set..
                ToolStripMenuItem menuItem =
                    new ToolStripMenuItem(encodingCharacterSet.GetCharacterSetName(item)) {Tag = item};

                // set the tag to contain the character set enumeration value..

                // get the encodings for the character set..
                var encodings = encodingCharacterSet[item];

                // loop through the encodings within the character set..
                foreach (var encoding in encodings)
                {
                    // create a menu item for the encoding..
                    DataToolStripMenuItem menuItemEncoding = new DataToolStripMenuItem(encoding.EncodingName)
                    {
                        Tag = encoding, Data = data
                    };

                    // set the Tag property to contain the encoding..

                    // set the user given additional data for the menu item..

                    // subscribe the click event..
                    menuItemEncoding.Click += MenuItemEncoding_Click;

                    // add the menu item to the character set menu..
                    menuItem.DropDownItems.Add(menuItemEncoding);
                }

                // add the character set menu item to the given parent menu..
                parent.DropDownItems.Add(menuItem);
            }
        }

        /// <summary>
        /// Disposes the character set menu constructed via the <see cref="CreateCharacterSetMenu"/> method.
        /// </summary>
        /// <param name="parent">The parent tool strip menu item.</param>
        public static void DisposeCharacterSetMenu(ToolStripMenuItem parent)
        {
            List<ToolStripMenuItem> disposeList = new List<ToolStripMenuItem>();
            foreach (var item in parent.DropDownItems)
            {
                // only accept types of ToolStripMenuItem..
                if (item.GetType() != typeof(ToolStripMenuItem))
                {
                    continue;
                }

                // cast the object as ToolStripMenuItem..
                var charsetMenuItem = (ToolStripMenuItem)item;

                // loop through the character set menu item's drop down items..
                foreach (var encodingItem in charsetMenuItem.DropDownItems)
                {
                    // only accept types of DataToolStripMenuItem..
                    if (encodingItem.GetType() != typeof(DataToolStripMenuItem))
                    {
                        continue;
                    }

                    // cast the object as DataToolStripMenuItem..
                    var encodingMenuItem = (DataToolStripMenuItem)encodingItem;

                    // unsubscribe the event handler..
                    encodingMenuItem.Click -= MenuItemEncoding_Click;

                    // add the menu item to the list of ToolStripMenuItems to disposed of..
                    disposeList.Add(encodingMenuItem);
                }

                // clear the drop down menu item..
                charsetMenuItem.DropDownItems.Clear();

                // add the menu item to the list of ToolStripMenuItems to disposed of..
                disposeList.Add(charsetMenuItem);
            }

            // clear the drop down items from the parent menu item..
            parent.DropDownItems.Clear();

            // loop through the list of ToolStripMenuItems to disposed of..
            for (int i = 0; i < disposeList.Count; i++)
            {
                // dispose..
                using (disposeList[i])
                {
                    // null assignment isn't necessary, but the using clause 
                    // would look a little "orphan" without that..
                    disposeList[i] = null;
                }
            }
        }

        // an internal event handler to raise the EncodingMenuClicked event if subscribed..
        private static void MenuItemEncoding_Click(object sender, EventArgs e)
        {
            // get the sender and assume a type of DataToolStripMenuItem..
            DataToolStripMenuItem dataToolStripMenuItem = (DataToolStripMenuItem)sender;

            // raise the event if subscribed..
            EncodingMenuClicked?.
                Invoke(sender,
                new EncodingMenuClickEventArgs
                {
                    Encoding = (System.Text.Encoding)dataToolStripMenuItem.Tag,
                    Data = dataToolStripMenuItem.Data
                });
        }

        /// <summary>
        /// A delegate for the EncodingMenuClicked event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="EncodingMenuClickEventArgs"/> instance containing the event data.</param>
        public delegate void OnEncodingMenuClicked(object sender, EncodingMenuClickEventArgs e);

        /// <summary>
        /// Occurs when an encoding menu item was clicked.
        /// </summary>
        public static event OnEncodingMenuClicked EncodingMenuClicked;
    }

    /// <summary>
    /// Event arguments for the EncodingMenuClicked event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class EncodingMenuClickEventArgs: EventArgs
    {
        /// <summary>
        /// Gets the encoding of the clicked encoding menu item.
        /// </summary>
        public System.Text.Encoding Encoding { get; internal set; }

        /// <summary>
        /// Gets the data associated with the encoding menu item.
        /// </summary>
        public object Data { get; internal set; }
    }
}
