using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.Encoding.CharacterSets
{
    /// <summary>
    /// A tool strip menu item containing additional data.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.ToolStripMenuItem" />
    public class DataToolStripMenuItem : ToolStripMenuItem
    {
        /// <summary>
        /// Gets or sets the additional data assigned to the tool strip menu item.
        /// </summary>
        public object Data { get; set; } = null;

        #region BaseConstructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DataToolStripMenuItem"/> class.
        /// </summary>
        public DataToolStripMenuItem(): base()
        {
            // nothing to do here..
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataToolStripMenuItem"/> class.
        /// </summary>
        /// <param name="text">The text to display on the menu item.</param>
        public DataToolStripMenuItem(string text): base(text)
        {
            // nothing to do here..
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataToolStripMenuItem"/> class.
        /// </summary>
        /// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
        public DataToolStripMenuItem(System.Drawing.Image image) : base(image)
        {
            // nothing to do here..
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataToolStripMenuItem"/> class.
        /// </summary>
        /// <param name="text">The text to display on the menu item.</param>
        /// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
        public DataToolStripMenuItem(string text, System.Drawing.Image image) : base(text, image)
        {
            // nothing to do here..
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataToolStripMenuItem"/> class.
        /// </summary>
        /// <param name="text">The text to display on the menu item.</param>
        /// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
        /// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
        public DataToolStripMenuItem(string text, System.Drawing.Image image, EventHandler onClick) : 
            base(text, image, onClick)
        {
            // nothing to do here..
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataToolStripMenuItem"/> class.
        /// </summary>
        /// <param name="text">The text to display on the menu item.</param>
        /// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
        /// <param name="dropDownItems">The menu items to display when the control is clicked.</param>
        public DataToolStripMenuItem(string text, System.Drawing.Image image, params ToolStripItem[] dropDownItems) : 
            base(text, image, dropDownItems)
        {
            // nothing to do here..
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataToolStripMenuItem"/> class.
        /// </summary>
        /// <param name="text">The text to display on the menu item.</param>
        /// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
        /// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
        /// <param name="shortcutKeys">One of the values of <see cref="T:System.Windows.Forms.Keys" /> that represents the shortcut key for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
        public DataToolStripMenuItem(string text, System.Drawing.Image image, EventHandler onClick, Keys shortcutKeys) : 
            base(text, image, onClick, shortcutKeys)
        {
            // nothing to do here..
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataToolStripMenuItem"/> class.
        /// </summary>
        /// <param name="text">The text to display on the menu item.</param>
        /// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
        /// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
        /// <param name="name">The name of the menu item.</param>
        public DataToolStripMenuItem(string text, System.Drawing.Image image, EventHandler onClick, string name) : 
            base(text, image, onClick, name)
        {
            // nothing to do here..
        }
        #endregion
    }

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
                ToolStripMenuItem menuItem = new ToolStripMenuItem(encodingCharacterSet.GetCharacterSetName(item));

                // set the tag to contain the character set enumeration value..
                menuItem.Tag = item;

                // get the encodings for the character set..
                var encodings = encodingCharacterSet[item];

                // loop through the encodings within the character set..
                foreach (var encoding in encodings)
                {
                    // create a menu item for the encoding..
                    DataToolStripMenuItem menuItemEncoding = new DataToolStripMenuItem(encoding.EncodingName);

                    // set the Tag property to contain the encoding..
                    menuItemEncoding.Tag = encoding;

                    // set the user given additional data for the menu item..
                    menuItemEncoding.Data = data;

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
                var charserMenuItem = (ToolStripMenuItem)item;

                // loop through the character set menu item's drop down items..
                foreach (var encodingItem in charserMenuItem.DropDownItems)
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
                charserMenuItem.DropDownItems.Clear();

                // add the menu item to the list of ToolStripMenuItems to disposed of..
                disposeList.Add(charserMenuItem);
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
        public static event OnEncodingMenuClicked EncodingMenuClicked = null;
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
