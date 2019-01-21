using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepad.Database
{
    /// <summary>
    /// A class for indicating for code snippets in the database.
    /// </summary>
    public class CODE_SNIPPETS
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return SCRIPT_NAME;
        }

        /// <summary>
        /// Gets or sets the ID number of the entry in the database.
        /// </summary>
        public long ID { get; set; } = -1;

        /// <summary>
        /// Gets or sets the script's contents.
        /// </summary>
        public string SCRIPT_CONTENTS { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the script.
        /// </summary>
        public string SCRIPT_NAME { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the script was previously modified.
        /// </summary>
        public DateTime MODIFIED { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the type of the script where a value of 0 means text manipulation and a value of 1 means lines manipulation.
        /// </summary>
        public int SCRIPT_TYPE { get; set; } = 0; // currently only 

        /// <summary>
        /// Gets or sets the language type of the script snippet. Currently only C# is supported with a value of 0.
        /// </summary>
        public int SCRIPT_LANGUAGE { get; set; } = 0;
    }
}
