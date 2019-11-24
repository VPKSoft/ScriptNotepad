using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepad.Database.Entity.Enumerations
{
    /// <summary>
    /// An enumeration for a type of a search.
    /// </summary>
    public enum SearchAndReplaceType
    {
        /// <summary>
        /// The type of the search and replace history is search.
        /// </summary>
        Search,

        /// <summary>
        /// The type of the search and replace history is replace.
        /// </summary>
        Replace,
    }
}
