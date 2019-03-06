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
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.Common
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
        public DataToolStripMenuItem() : base()
        {
            // nothing to do here..
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataToolStripMenuItem"/> class.
        /// </summary>
        /// <param name="text">The text to display on the menu item.</param>
        public DataToolStripMenuItem(string text) : base(text)
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
}
