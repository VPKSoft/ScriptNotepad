using ScintillaPrinting;
using ScriptNotepad.DialogForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptNotepad.Test
{
    /// <summary>
    /// A test form for various things (...).
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormTestThings : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormTestThings"/> class.
        /// </summary>
        public FormTestThings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(FormDialogQueryEncoding.Execute().ToString());

//            Printing printer = new Printing(sttcMain.Documents[0].Scintilla);
//            printer.PrintPreview();

        }
    }
}
