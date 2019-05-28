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
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ScriptNotepad.UtilityClasses.Common
{
    /// <summary>
    /// A class for a simple combo box for a tool strip.
    /// Implements the <see cref="System.Windows.Forms.ToolStripControlHost" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.ToolStripControlHost" />
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip)]
    // Not needed for internal use: [System.Drawing.ToolboxBitmap()]
    public class StatusStripComboItem: ToolStripControlHost
    {
        /// <summary>
        /// Gets or sets the ComboBox of the <see cref="StatusStripComboItem"/>.
        /// </summary>
        private ComboBox ComboBox { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusStripComboItem"/> class.
        /// </summary>
        public StatusStripComboItem()
            : base(new ComboBox())
        {
            ComboBox = Control as ComboBox;

            if (ComboBox != null)
            {
                ComboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                ComboBox.SelectedValueChanged += ComboBox_SelectedValueChanged;
            }
        }

        // event re-delegation..
        private void ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedValueChanged?.Invoke(sender, e);
        }

        // event re-delegation..
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(sender, e);
        }

        #region ComboBoxAccess
        /// <summary>
        /// Gets an object representing the collection of the items contained in this <see cref="T:System.Windows.Forms.ComboBox" />.
        /// </summary>
        [Browsable(true)]
        [Category("Data")]
        [Description("Gets an object representing the collection of the items contained in this ComboBox.")]
        public ComboBox.ObjectCollection Items => ComboBox.Items;

        /// <summary>
        /// Gets or sets a value specifying the style of the combo box.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets a value specifying the style of the combo box.")]
        public ComboBoxStyle DropDownStyle
        {
            get => ComboBox.DropDownStyle;
            set => ComboBox.DropDownStyle = value;
        }

        /// <summary>
        /// Gets or sets currently selected item in the <see cref="T:System.Windows.Forms.ComboBox" />.
        /// </summary>
        [Browsable(false)]
        public object SelectedItem
        {
            get => ComboBox.SelectedItem;
            set => ComboBox.SelectedItem = value;
        }

        /// <summary>
        /// Gets or sets the index specifying the currently selected item.
        /// </summary>
        [Browsable(false)]
        public int SelectedIndex
        {
            get => ComboBox.SelectedIndex;
            set => ComboBox.SelectedIndex = value;
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the text associated with this control.")]
        public new string Text
        {
            get => base.Text;

            set
            {
                base.Text = value;
                ComboBox.Text = value;
            } 
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.ComboBox.SelectedIndex" /> property has changed.
        /// </summary>
        public EventHandler SelectedIndexChanged;

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.ListControl.SelectedValue" /> property changes.
        /// </summary>
        public EventHandler SelectedValueChanged;
        #endregion
    }
}
