using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace ScriptNotepad.UtilityClasses.TextManipulation.Interfaces
{
    interface ITextManipulationCallback: IMethodName
    {
        /// <summary>
        /// Gets or sets the callback action.
        /// </summary>
        /// <value>The callback action.</value>
        Action CallbackAction { get; set; }
    }
}
