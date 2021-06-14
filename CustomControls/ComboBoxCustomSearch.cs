#region License
/*
MIT License

Copyright(c) 2021 Petteri Kautonen

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
