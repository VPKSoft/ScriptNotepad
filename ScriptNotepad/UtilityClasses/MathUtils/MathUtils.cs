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
