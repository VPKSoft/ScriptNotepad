using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    /// <summary>
    /// Event arguments for the search and replace dialog to be able to interact with the main form.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ScintillaDocumentEventArgs: EventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether all the open documents are requested.
        /// </summary>
        public bool RequestAllDocuments { get; set; } = false;

        /// <summary>
        /// Gets or sets the <see cref="Scintilla"/> documents requested by an event.
        /// </summary>
        public List<Scintilla> Documents { get; set; } = new List<Scintilla>();
    }
}
