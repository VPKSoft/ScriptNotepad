using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace ScriptNotepad.UtilityClasses.MathUtils
{
    /// <summary>
    /// Some mathematical utilities.
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// Determines the maximum value of the given parameters.
        /// </summary>
        /// <typeparam name="T">The type of the object which maximum value to get.</typeparam>
        /// <param name="values">The values of type T which maximum value to get.</param>
        /// <returns>T.</returns>
        public static T Max<T>(params T[] values)
        {
            return values.Max();
        }

        /// <summary>
        /// Determines the minimum value of the given parameters.
        /// </summary>
        /// <typeparam name="T">The type of the object which minimum value to get.</typeparam>
        /// <param name="values">The values of type T which minimum value to get.</param>
        /// <returns>T.</returns>
        public static T Min<T>(params T[] values)
        {
            return values.Min();
        }
    }
}
