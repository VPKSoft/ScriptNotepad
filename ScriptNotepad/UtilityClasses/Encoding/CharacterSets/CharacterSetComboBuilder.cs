using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.Encoding.CharacterSets
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
        public System.Text.Encoding Encoding { get; set; } = null;

        /// <summary>
        /// Gets a value indicating whether this instance contains an encoding class instance.
        /// </summary>
        public bool ContainsEncoding { get => Encoding != null; }

        /// <summary>
        /// Gets or sets an arbitrary object value that can be used to store custom information about this element.
        /// </summary>
        public object Tag { get; set; } = null;

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
        /// <param name="singleCodePageResults">A flag indicating if character sets containing only single encoding should be returned.</param>
        /// <param name="data">Additional data to be assigned to an instance of the CharacterSetComboItem class.</param>
        public CharacterSetComboBuilder(ComboBox characterSetComboBox, ComboBox encodingComboBox, bool singleCodePageResults, object data)
        {
            // save the combo box instance to the class..
            CharacterSetComboBox = characterSetComboBox;

            // save the combo box instance to the class..
            EncodingComboBox = encodingComboBox;

            // save the data for the combo boxes..
            Data = data;

            // get an instance of the EncodingCharacterSet class..
            var encodingCharacterSet = EncodingCharacterSet;

            // get the character sets contained in the EncodingCharacterSet class..
            var charSets = encodingCharacterSet.GetCharacterSetList(singleCodePageResults);

            // loop through the character sets..
            foreach (var item in charSets)
            {
                // add the character sets to the characterSetComboBox..
                characterSetComboBox.Items.Add(new CharacterSetComboItem { CharacterSet = item, Tag = data });
            }

            // subscribe the SelectedIndexChanged event..
            CharacterSetComboBox.SelectedIndexChanged += CharacterSetComboBox_SelectedIndexChanged;

            // subscribe the SelectedIndexChanged event..
            EncodingComboBox.SelectedIndexChanged += EncodingComboBox_SelectedIndexChanged;

            // set the index for the combo box..
            if (characterSetComboBox.Items.Count > 0)
            {
                // ..if the combo box has any items..
                characterSetComboBox.SelectedIndex = 0;
            }
        }

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
                    EncodingComboBox.Items.Add(new CharacterSetComboItem { CharacterSet = item.CharacterSet, Encoding = encoding, Tag = Data });
                }

                // set the index for the combo box..
                if (EncodingComboBox.Items.Count > 0)
                {
                    // ..if the combo box has any items..
                    EncodingComboBox.SelectedIndex = 0;
                }
            }
        }

        // the encoding combo box selected item was changed..
        private void EncodingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the combo box instance..
            ComboBox comboBox = (ComboBox)sender;

            // a null check just in case..
            if (comboBox.SelectedItem != null)
            {
                // get the selected item of the combo box..
                CharacterSetComboItem item = (CharacterSetComboItem)comboBox.SelectedItem;
                EncodingSelected?.Invoke(this, new OnEncodingSelectedEventArgs { Data = Data, Encoding = item.Encoding } );
            }
        }

        /// <summary>
        /// Releases all resources used by the class.
        /// </summary>
        public void Dispose()
        {
            // unsubscribe the SelectedIndexChanged event..
            CharacterSetComboBox.SelectedIndexChanged -= CharacterSetComboBox_SelectedIndexChanged;

            // unsubscribe the SelectedIndexChanged event..
            EncodingComboBox.SelectedIndexChanged -= EncodingComboBox_SelectedIndexChanged;
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
        public event OnEncodingSelected EncodingSelected = null;
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
