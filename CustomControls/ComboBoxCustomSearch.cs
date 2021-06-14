using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CustomControls
{
    // Parts of the code from (C): https://social.msdn.microsoft.com/Forums/en-US/4ebaaed0-cd29-4663-9a43-973729d66cea/autocomplete-combobox-match-any-part-of-string-not-only-beginning-string?forum=winforms

    /// <summary>
    /// A <see cref="ComboBox"/> implementation auto-completing case-insensitively items containing the typed text.
    /// Implements the <see cref="System.Windows.Forms.ComboBox" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.ComboBox" />
    public class ComboBoxCustomSearch : ComboBox
    {
        private IList<object> collectionList;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CustomControls.ComboBoxCustomSearch" /> class.
        /// </summary>
        public ComboBoxCustomSearch()
        {
            collectionList = new List<object>();
        }

        // ReSharper disable four times InconsistentNaming, WinApi constant..
        // ReSharper disable four times IdentifierTypo, WinApi constant..
        private const int  CB_ADDSTRING = 0x143;
        private const int CB_DELETESTRING = 0x144;
        private const int CB_INSERTSTRING = 0x14A;
        private const int CB_RESETCONTENT  = 0x14B;

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The <see cref="Message"/> message to process.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg is CB_ADDSTRING or CB_DELETESTRING or CB_INSERTSTRING or CB_RESETCONTENT)
            {
                if (!filtering)
                {
                    collectionList = Items.OfType<object>().ToList();
                }
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// A flag indicting whether the combo box is being filtered.
        /// </summary>
        private bool filtering;

        /// <summary>
        /// A flag indicating whether <see cref="OnCreateControl"/> has been called once.
        /// </summary>
        private bool controlCreated;

        /// <summary>
        /// Raises the <see cref="ComboBox.TextUpdate"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnTextUpdate(EventArgs e)
        {
            filtering = true;
            IList<object> Values = collectionList
                .Where(x => (x.ToString() ?? "").Contains(Text, StringComparison.OrdinalIgnoreCase))
                .ToList<object>();

            Items.Clear();
            Items.AddRange(Text != string.Empty ? Values.ToArray() : collectionList.ToArray());

            SelectionStart = Text.Length;
            DroppedDown = true;
            filtering = false;
        }


        /// <summary>
        /// Raises the <see cref="Control.CreateControl"/> method.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!controlCreated)
            {
                collectionList = Items.OfType<object>().ToList();
                controlCreated = true;
            }
        }
    }
}
