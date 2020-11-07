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
using System.Linq;
using System.Windows.Forms;
using InstallerBaseWixSharp.Files.Dialogs.DialogClasses;
using WixSharp.UI.Forms;

namespace InstallerBaseWixSharp.Files.Dialogs
{
    /// <summary>
    /// An installer dialog to associate files for an application.
    /// Implements the <see cref="WixSharp.UI.Forms.ManagedForm" />
    /// </summary>
    /// <seealso cref="WixSharp.UI.Forms.ManagedForm" />
    public partial class AssociationsDialog : ManagedForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssociationsDialog"/> class.
        /// </summary>
        public AssociationsDialog()
        {
            //NOTE: If this assembly is compiled for v4.0.30319 runtime, it may not be compatible with the MSI hosted CLR.
            //The incompatibility is particularly possible for the Embedded UI scenarios.
            //The safest way to avoid the problem is to compile the assembly for v3.5 Target Framework.InstallerBaseWixSharp
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the associations displayed by the dialog.
        /// </summary>
        /// <value>The associations displayed by the dialog.</value>
        /// <remarks>Format for single extension: .ext. For multiple extensions: (.ext1/.ext2)</remarks>
        private List<FileAssociation> Associations { get; set; } =
            new List<FileAssociation>(new[]
            {
                new FileAssociation(".txt", "AssociationDlg_Association_0"),
            });

        void dialog_Load(object sender, EventArgs e)
        {
            banner.Image = Runtime.Session.GetResourceBitmap("WixUI_Bmp_Banner");

            //resolve all Control.Text cases with embedded MSI properties (e.g. 'ProductName') and *.wxl file entries
            Localize();

            
            foreach (var association in Associations)
            {
                association.AssociationName = MsiRuntime.Localize(association.AssociationName);
            }

            Associations = Associations.OrderBy(f => f.AssociationName).ToList();
            clbFileAssociations.Items.AddRange(Associations.Cast<object>().ToArray());
            CheckAllItems(true);
        }

        void back_Click(object sender, EventArgs e)
        {
            Shell.GoPrev();
        }

        void next_Click(object sender, EventArgs e)
        {
            try
            {
                Associations = Associations.Where((association, i) => clbFileAssociations.CheckedIndices.Contains(i)).ToList();

                var associationsPropertyValue = string.Empty;
                try
                {
                    // no deal with the semicolon (';') delimiter..
                    associationsPropertyValue = string.Join(":", Associations.Select(f => f.ToSerializeString()));
                }
                catch
                {
                    // ignored..
                }

                MsiRuntime.Session["ASSOCIATIONS"] = associationsPropertyValue;
            }
            catch
            {
                // ignored..
            }
            Shell.GoNext();
        }

        void cancel_Click(object sender, EventArgs e)
        {
            Shell.Cancel();
        }

        private void CheckAllItems(bool check)
        {
            for (var i = 0; i < clbFileAssociations.Items.Count; i++)
            {
                clbFileAssociations.SetItemChecked(i, check);
            }
        }

        private void cbCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            var check = ((CheckBox) sender).Checked;
            CheckAllItems(check);
        }

        private void clbFileAssociations_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var checkedIndices = clbFileAssociations.CheckedIndices.Cast<int>().ToList();

            if (e.NewValue == CheckState.Checked)
            {
                checkedIndices.Add(e.Index);
            }

            if (e.NewValue == CheckState.Unchecked)
            {
                checkedIndices.Remove(e.Index);
            }

            var allChecked = clbFileAssociations.Items.Count == checkedIndices.Count;

            cbCheckAll.CheckStateChanged -= cbCheckAll_CheckedChanged;
            cbCheckAll.Checked = allChecked;
            cbCheckAll.CheckStateChanged += cbCheckAll_CheckedChanged;
        }
    }
}

