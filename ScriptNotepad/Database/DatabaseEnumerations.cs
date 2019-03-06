using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepad.Database
{
    /// <summary>
    /// Some enumerations used by the database classes.
    /// </summary>
    public static class DatabaseEnumerations
    {
        /// <summary>
        /// An enumeration indicating how the database commands should react to the ISHISTORY flag of the <see cref="DBFILE_SAVE"/> class.
        /// </summary>
        public enum DatabaseHistoryFlag
        {
            /// <summary>
            /// No history files should be included.
            /// </summary>
            NotHistory,

            /// <summary>
            /// Only history files should be included.
            /// </summary>
            IsHistory,

            /// <summary>
            /// The ISHISTORY flag should be ignored.
            /// </summary>
            DontCare
        }
    }
}
