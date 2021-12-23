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

using System.Linq;
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.Encodings.CharacterSets
{
    /// <summary>
    /// A class to be used with combo boxes for listing character sets and their encodings.
    /// </summary>
    public class CharacterSetComboItem
    {
        /// <summary>
        /// Gets or sets the character set.
        /// </summary>
        public CharacterSets CharacterSet { get; set; } = CharacterSets.Unicode;

        /// <summary>
        /// Gets or sets the encoding "bound" to the character set.
        /// </summary>
        public System.Text.Encoding Encoding { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance contains an encoding class instance.
        /// </summary>
        public bool ContainsEncoding => Encoding != null;

        /// <summary>
        /// Gets or sets an arbitrary object value that can be used to store custom information about this element.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            // return a value based on to a comparison whether this instance contains an encoding or not..
            return ContainsEncoding ? Encoding.EncodingName : CharacterSetComboBuilder.EncodingCharacterSet.GetCharacterSetName(CharacterSet);
        }
    }


    /// <summary>
    /// A class to help to help to fill combo boxes with known character sets (<see cref="EncodingCharacterSet"/>) and their encodings <see cref="System.Text.Encoding"/>.
    /// </summary>
    public class CharacterSetComboBuilder: IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSetComboBuilder"/> class.
        /// </summary>
        /// <param name="characterSetComboBox">An instance to a combo box containing the character sets.</param>
        /// <param name="encodingComboBox">An instance to a combo box containing the encodings belonging to a character set.</param>
        /// <param name="filterEncodingTextBox">A text box for searching for an encoding.</param>
        /// <param name="singleCodePageResults">A flag indicating if character sets containing only single encoding should be used.</param>
        /// <param name="data">Additional data to be assigned to an instance of the CharacterSetComboItem class.</param>
        public CharacterSetComboBuilder(
            ComboBox characterSetComboBox,
            ComboBox encodingComboBox,
            TextBox filterEncodingTextBox,
            bool singleCodePageResults,
            object data)
        {
            ConstructorHelper(characterSetComboBox, encodingComboBox, filterEncodingTextBox, singleCodePageResults, data);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSetComboBuilder"/> class.
        /// </summary>
        /// <param name="characterSetComboBox">An instance to a combo box containing the character sets.</param>
        /// <param name="encodingComboBox">An instance to a combo box containing the encodings belonging to a character set.</param>
        /// <param name="singleCodePageResults">A flag indicating if character sets containing only single encoding should be used.</param>
        /// <param name="data">Additional data to be assigned to an instance of the CharacterSetComboItem class.</param>
        public CharacterSetComboBuilder(
            ComboBox characterSetComboBox, 
            ComboBox encodingComboBox, 
            bool singleCodePageResults, 
            object data)
        {
            ConstructorHelper(characterSetComboBox, encodingComboBox, null, singleCodePageResults, data);
        }

        /// <summary>
        /// A helper method for multiple constructor overloads.
        /// </summary>
        /// <param name="characterSetComboBox">An instance to a combo box containing the character sets.</param>
        /// <param name="encodingComboBox">An instance to a combo box containing the encodings belonging to a character set.</param>
        /// <param name="filterEncodingTextBox">A text box for searching for an encoding.</param>
        /// <param name="singleCodePageResults">A flag indicating if character sets containing only single encoding should be used.</param>
        /// <param name="data">Additional data to be assigned to an instance of the CharacterSetComboItem class.</param>
        private void ConstructorHelper(
            ComboBox characterSetComboBox,
            ComboBox encodingComboBox,
            TextBox filterEncodingTextBox,
            bool singleCodePageResults,
            object data)
        {
            // save the combo box instance to the class..
            CharacterSetComboBox = characterSetComboBox;

            // save the combo box instance to the class..
            EncodingComboBox = encodingComboBox;

            // save the text box instance to the class..
            FilterEncodingTextBox = filterEncodingTextBox;

            // save the singleCodePageResults parameter value..
            SingleCodePageResults = singleCodePageResults;

            // save the data for the combo boxes..
            Data = data;

            // subscribe the SelectedIndexChanged event..
            CharacterSetComboBox.SelectedIndexChanged += CharacterSetComboBox_SelectedIndexChanged;

            // subscribe the SelectedIndexChanged event..
            EncodingComboBox.SelectedIndexChanged += EncodingComboBox_SelectedIndexChanged;

            // subscribe the text changed event..
            if (FilterEncodingTextBox != null) // this can be null for the constructor overload..
            {
                FilterEncodingTextBox.TextChanged += FilterEncodingTextBox_TextChanged;
            }

            // list the encodings..
            if (FilterEncodingTextBox != null)
            {
                CreateFilteredEncodingList();
            }
            else
            {
                CreateUnFilteredEncodingList();
            }
        }

        /// <summary>
        /// Creates a non-filtered encoding list for the combo box.
        /// </summary>
        private void CreateUnFilteredEncodingList()
        {
            // get the character sets contained in the EncodingCharacterSet class..
            var charSets = EncodingCharacterSet.GetCharacterSetList(SingleCodePageResults);

            CharacterSetComboBox.Items.Clear();

            // loop through the character sets..
            foreach (var item in charSets)
            {
                // add the character sets to the characterSetComboBox..
                CharacterSetComboBox.Items.Add(new CharacterSetComboItem { CharacterSet = item, Tag = Data });
            }

            // set the index for the combo box..
            if (CharacterSetComboBox.Items.Count > 0)
            {
                // ..if the combo box has any items..
                CharacterSetComboBox.SelectedIndex = 0;
            }
            else
            {
                // if there are no character sets, clear the encoding combo box as well..
                EncodingComboBox.Items.Clear();
            }
        }

        /// <summary>
        /// Creates a filtered encoding list for the combo box.
        /// </summary>
        private void CreateFilteredEncodingList()
        {
            string text = FilterText;

            // if there is no filter the just list the encodings non-filtered..
            if (text == string.Empty)
            {
                CreateUnFilteredEncodingList();
                return;
            }

            // get an instance of the EncodingCharacterSet class..
            var encodingCharacterSet = EncodingCharacterSet;

            // get the character sets contained in the EncodingCharacterSet class..
            var charSets = encodingCharacterSet.GetCharacterSetList(SingleCodePageResults);

            CharacterSetComboBox.Items.Clear();

            // loop through the character sets..
            foreach (var item in charSets)
            {
                var encodings = encodingCharacterSet[item];
                encodings = encodings.Where(FilterEncoding);

                if (encodings.ToList().Count > 0)
                {
                    // add the character sets to the characterSetComboBox..
                    CharacterSetComboBox.Items.Add(new CharacterSetComboItem { CharacterSet = item, Tag = Data });
                }
            }

            // set the index for the combo box..
            if (CharacterSetComboBox.Items.Count > 0)
            {
                // ..if the combo box has any items..
                CharacterSetComboBox.SelectedIndex = 0;
            }
            else
            {
                // if there are no character sets, clear the encoding combo box as well..
                EncodingComboBox.Items.Clear();
            }
            EncodingSelected?.Invoke(this, new OnEncodingSelectedEventArgs { Data = Data, Encoding = CurrentEncoding });
        }

        /// <summary>
        /// Gets the filter text for the encodings.
        /// </summary>
        private string FilterText =>
            FilterEncodingTextBox == null ? string.Empty :
            string.IsNullOrWhiteSpace(FilterEncodingTextBox.Text) ? string.Empty : FilterEncodingTextBox.Text.Trim(' ').ToLowerInvariant();

        /// <summary>
        /// Checks if the given encoding matches the current filter text.
        /// </summary>
        /// <param name="encoding">The encoding to check.</param>
        /// <returns>True if the encoding matches the filter text; otherwise false;</returns>
        private bool FilterEncoding(System.Text.Encoding encoding)
        {
            if (FilterText == string.Empty)
            {
                return true;
            }

            // split the filter text with space character..
            string[] filters = FilterText.Split(' ');

            bool match = true;
            foreach (string filter in filters)
            {
                // check the string property match..
                match &= encoding.BodyName.ToLowerInvariant().Contains(filter) ||
                                            encoding.EncodingName.ToLowerInvariant().Contains(filter) ||
                                            encoding.HeaderName.ToLowerInvariant().Contains(filter) ||
                                            encoding.WebName.ToLowerInvariant().Contains(filter);

                // check the code page match..
                match |= encoding.CodePage.ToString() == filter;
            }
            return match;
        }

        // the filter text box text was changed..
        private void FilterEncodingTextBox_TextChanged(object sender, EventArgs e)
        {
            // so filter the encodings..
            CreateFilteredEncodingList();
        }

        /// <summary>
        /// Selects the character set combo box and the encoding combo box selected items by given encoding.
        /// </summary>
        /// <param name="encoding">The encoding to set the selected index of the both combo boxes.</param>
        /// <param name="singleCodePageResults">A flag indicating if character sets containing only single encoding should be used.</param>
        public void SelectItemByEncoding(System.Text.Encoding encoding, bool singleCodePageResults)
        {
            var charSet = EncodingCharacterSet.GetCharacterSetsForEncoding(encoding, singleCodePageResults).FirstOrDefault();
            try
            {
                for (int i = 0; i < CharacterSetComboBox.Items.Count; i++)
                {
                    var item = (CharacterSetComboItem)CharacterSetComboBox.Items[i];
                    if (item.CharacterSet == charSet)
                    {
                        CharacterSetComboBox.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < EncodingComboBox.Items.Count; i++)
                {
                    var item = (CharacterSetComboItem)EncodingComboBox.Items[i];
                    if (!item.ContainsEncoding)
                    {
                        continue;
                    }

                    if (item.Encoding.CodePage == encoding.CodePage)
                    {
                        EncodingComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LastException = ex;
            }
        }

        /// <summary>
        /// Gets or sets the last exception thrown by an instance of this class.
        /// </summary>
        public static Exception LastException { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the combo box.
        /// </summary>
        private object Data { get; set; }

        /// <summary>
        /// Gets or sets the character set ComboBox.
        /// </summary>
        private ComboBox CharacterSetComboBox { get; set; }

        /// <summary>
        /// Gets or sets the encoding ComboBox.
        /// </summary>
        private ComboBox EncodingComboBox { get; set; }

        /// <summary>
        /// Gets or sets the filter text box.
        /// </summary>
        private TextBox FilterEncodingTextBox { get; set; }

        /// <summary>
        /// A flag indicating if character sets containing only single encoding should be used.
        /// </summary>
        private bool SingleCodePageResults { get; set; }

        /// <summary>
        /// Gets or sets the encoding character set (a single instance is required and needs not to be disposed of).
        /// </summary>
        internal static EncodingCharacterSet EncodingCharacterSet { get; set; } = new EncodingCharacterSet();

        // the character set combo box selected item was changed..
        private void CharacterSetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the combo box instance..
            ComboBox comboBox = (ComboBox)sender;

            // a null check just in case..
            if (comboBox.SelectedItem != null)
            {
                // get the selected item of the combo box..
                CharacterSetComboItem item = (CharacterSetComboItem)comboBox.SelectedItem;
                var encodings = EncodingCharacterSet[item.CharacterSet];

                // clear the previous items from the encoding combo box..
                EncodingComboBox.Items.Clear();

                // loop through the encodings and add them the combo box containing the encodings.. 
                foreach (var encoding in encodings)
                {
                    // if the encodings are being filtered..
                    if (FilterText != string.Empty)
                    {
                        if (FilterEncoding(encoding))
                        {
                            // only add the matching encodings..
                            EncodingComboBox.Items.Add(new CharacterSetComboItem { CharacterSet = item.CharacterSet, Encoding = encoding, Tag = Data });
                        }
                    }
                    else
                    {
                        // add all the encoding as the encodings are not being filtered..
                        EncodingComboBox.Items.Add(new CharacterSetComboItem { CharacterSet = item.CharacterSet, Encoding = encoding, Tag = Data });
                    }
                }

                // set the index for the combo box..
                if (EncodingComboBox.Items.Count > 0)
                {
                    // ..if the combo box has any items..
                    EncodingComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Gets the currently selected encoding in the encoding combo box.
        /// </summary>
        public System.Text.Encoding CurrentEncoding => ((CharacterSetComboItem) EncodingComboBox.SelectedItem)?.Encoding;

        // the encoding combo box selected item was changed..
        private void EncodingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EncodingSelected?.Invoke(this, new OnEncodingSelectedEventArgs { Data = Data, Encoding = CurrentEncoding });
        }

        /// <summary>
        /// Releases all resources used by the class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // unsubscribe the SelectedIndexChanged event..
                CharacterSetComboBox.SelectedIndexChanged -= CharacterSetComboBox_SelectedIndexChanged;

                // unsubscribe the SelectedIndexChanged event..
                EncodingComboBox.SelectedIndexChanged -= EncodingComboBox_SelectedIndexChanged;

                // unsubscribe the TextChanged event if the FilterEncodingTextBox is not null..
                if (FilterEncodingTextBox != null)
                {
                    FilterEncodingTextBox.TextChanged -= FilterEncodingTextBox_TextChanged;
                }
            }
        }


        /// <summary>
        /// A delegate for the EncodingMenuClicked event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="EncodingMenuClickEventArgs"/> instance containing the event data.</param>
        public delegate void OnEncodingSelected(object sender, OnEncodingSelectedEventArgs e);

        /// <summary>
        /// Occurs when an encoding menu item was clicked.
        /// </summary>
        public event OnEncodingSelected EncodingSelected;
    }

    /// <summary>
    /// Event arguments for the EncodingSelected event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class OnEncodingSelectedEventArgs : EventArgs
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
