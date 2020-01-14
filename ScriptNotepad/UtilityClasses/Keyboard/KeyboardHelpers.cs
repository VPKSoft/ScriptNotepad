#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

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

using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.Keyboard
{
    /// <summary>
    /// A class containing some helper methods dealing with the keyboard.
    /// </summary>
    public static class KeyboardHelpers
    {
        /// <summary>
        /// Gets a value indicating if some of the modifier keys are down (Control, Alt or Shift).
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <returns><c>true</c> if some of the modifier keys are down, <c>false</c> otherwise.</returns>
        public static bool SomeModifierKeysDown(this KeyEventArgs e)
        {
            return e.Alt || e.Control || e.Shift;
        }

        /// <summary>
        /// Gets a value indicating if all of the modifier keys are down (Control, Alt or Shift).
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <returns><c>true</c> if all of the modifier keys are down, <c>false</c> otherwise.</returns>
        public static bool AllModifierKeysDown(this KeyEventArgs e)
        {
            return e.Alt && e.Control && e.Shift;
        }

        /// <summary>
        /// Gets a value indicating if none of the modifier keys are down (Control, Alt or Shift).
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <returns><c>true</c> if none of the modifier keys are down, <c>false</c> otherwise.</returns>
        public static bool NoModifierKeysDown(this KeyEventArgs e)
        {
            return !e.Alt && !e.Control && !e.Shift;
        }

        /// <summary>
        /// Gets a value if only the Alt modifier key is down.
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <returns><c>true</c> if only the Alt modifier key is down, <c>false</c> otherwise.</returns>
        public static bool OnlyAlt(this KeyEventArgs e)
        {
            return e.ModifierKeysDown(true, false, false);
        }

        /// <summary>
        /// Gets a value if only the Shift modifier key is down.
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <returns><c>true</c> if only the Shift modifier key is down, <c>false</c> otherwise.</returns>
        public static bool OnlyShift(this KeyEventArgs e)
        {
            return e.ModifierKeysDown(false, false, true);
        }

        /// <summary>
        /// Gets a value if only the Control modifier key is down.
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <returns><c>true</c> if only the Control modifier key is down, <c>false</c> otherwise.</returns>
        public static bool OnlyControl(this KeyEventArgs e)
        {
            return e.ModifierKeysDown(false, true, false);
        }

        /// <summary>
        /// Gets a value indicating if the given modifier keys are down (Control, Alt or Shift).
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <param name="alt">if set to <c>true</c> the Alt key should be down.</param>
        /// <param name="control">if set to <c>true</c> the Control key should be down.</param>
        /// <param name="shift">if set to <c>true</c> the Shift key should be down.</param>
        /// <returns><c>true</c> if the given modifier keys are down, <c>false</c> otherwise.</returns>
        public static bool ModifierKeysDown(this KeyEventArgs e, bool alt, bool control, bool shift)
        {
            return e.Alt && alt || e.Control && control || e.Shift && shift;
        }

        /// <summary>
        /// Gets a value if some key is down in the given list of <see cref="Keys"/>.
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <param name="keys">The keys to check for.</param>
        /// <returns><c>true</c> if some key is down in the given list of <see cref="Keys"/>, <c>false</c> otherwise.</returns>
        public static bool KeyCodeIn(this KeyEventArgs e, params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (e.KeyCode == key)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
