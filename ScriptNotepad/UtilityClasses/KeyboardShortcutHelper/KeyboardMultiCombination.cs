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

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.KeyboardShortcutHelper
{
    /*
     Usage:
        combination = new KeyboardCollectiveCombination(someForm);

        // Ctrl + D, 1, 2..
        combination.AddKeyChord(new KeyData {Ctrl = true, Key = Keys.D}, "test");
        combination.AddKeyChord(new KeyData {Ctrl = true, Key = Keys.D1}, "test");
        combination.AddKeyChord(new KeyData {Ctrl = true, Key = Keys.D2}, "test");

        // Ctrl + A, B..
        combination.AddKeyChord(new KeyData {Ctrl = true, Key = Keys.A}, "test2");
        combination.AddKeyChord(new KeyData {Ctrl = true, Key = Keys.B}, "test2");
    */

    /// <summary>
    /// A class to collect multi-key combinations. I.e. digits and letters.
    /// </summary>
    public class KeyboardMultiCombination: IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TestBench2.KeyboardCollectiveCombination"/> class.
        /// </summary>
        /// <param name="form">The form of which keyboard combinations to listen to .</param>
        /// <param name="timerIntervalMs">The timer interval in milliseconds to assume the key combination is broken.</param>
        public KeyboardMultiCombination(Form form, int timerIntervalMs = 1000)
        {
            this.form = form; // save the form instance..
            form.KeyPreview = true; // set the key-preview to true..
            form.KeyDown += Form_KeyDown; // subscribe the keydown event..

            ResetTimeMilliseconds = timerIntervalMs; // save the reset value..

            TimerKeyDown = new Timer
            {
                Interval = 10 // set the timer interval..
            };
            TimerKeyDown.Tick += TimerKeyDown_Tick; // subscribe the tick even..
            TimerKeyDown.Enabled = true; // enabled the timer..
        }

        /// <summary>
        /// A field to hold the <see cref="Form"/> instance of which key board combinations to listen to.
        /// </summary>
        private readonly Form form;

        /// <summary>
        /// Adds a given keyboard combination for the class to listen for. To make the keyboard combination to consist of many keys, use the method multiple times.
        /// </summary>
        /// <param name="keyData">A <see cref="KeyData"/> class instance describing a single key press. At least one modifier key must be down.</param>
        /// <param name="name">A name for a key combination.</param>
        /// <returns>True if the key combination was successfully added (no empty name and at least one modifier key down).</returns>
        public bool AddKeyChord(KeyData keyData, string name)
        {
            // the name cannot be empty..
            if (name == string.Empty)
            {
                return false;
            }

            // one modifier key must be down..
            if (!keyData.Alt && !keyData.Ctrl && !keyData.Shift)
            {
                return false;
            }

            // add the given chord to the key combination list..
            var location = KeyList.Count(f => f.name == name);

            KeyList.Add((keyData.Alt, keyData.Ctrl, keyData.Shift, keyData.Key, name, location));

            // success..
            return true;
        }

        /// <summary>
        /// Adds a given keyboard combination for the class to listen for.
        /// <param name="name">A name for a key combination.</param>
        /// <param name="keyDataList">An array of <see cref="KeyData"/> class instances describing a keyboard combination. At least one modifier key must be down.</param>
        /// </summary>
        /// <returns>True if the key combination was successfully added (no empty name and at least one modifier key down).</returns>
        public bool AddKeyCombination(string name, params KeyData[] keyDataList)
        {
            // the name cannot be empty..
            if (name == string.Empty)
            {
                return false;
            }

            // one modifier key must be down..
            foreach (var keyData in keyDataList)
            {
                if (!keyData.Alt && !keyData.Ctrl && !keyData.Shift)
                {
                    return false;
                }
            }

            // add the chords to the key combination list..
            foreach (var keyData in keyDataList)
            {
                AddKeyChord(keyData, name);
            }

            // success..
            return true;
        }

        /// <summary>
        /// Removes a key combination from the class.
        /// </summary>
        /// <param name="name">The name of the key combination to remove.</param>
        /// <returns>True if the key combination was successfully removed (existed); otherwise false.</returns>
        public bool ClearKeyCombination(string name)
        {
            return KeyList.RemoveAll(f => f.name == name) > 0;
        }

        /// <summary>
        /// Gets or sets the internal key combination list the class should listen for.
        /// </summary>
        private List<(bool modifierAlt, bool modifierCtrl, bool modifierShift, Keys keys, string name, int location)>
            KeyList { get; } =
            new List<(bool modifierAlt, bool modifierCtrl, bool modifierShift, Keys keys, string name, int location)>();

        internal List<KeyData> KeysCollected { get; set; } = new List<KeyData>();

        /// <summary>
        /// Gets or set a value of how many milliseconds has passed within the timers tick event running at 10 millisecond interval.
        /// </summary>
        private int MillisecondsPassed { get; set; }

        /// <summary>
        /// Gets or sets the reset amount in milliseconds to reset the keyboard listening.
        /// </summary>
        private int ResetTimeMilliseconds { get; }

        private void TimerKeyDown_Tick(object sender, EventArgs e)
        {
            // disable the timer..
            TimerKeyDown.Enabled = false;

            // increase the milliseconds passed count by the timer's interval..
            MillisecondsPassed += 10;

            // if the interval exceeds the given maximum value..
            if (MillisecondsPassed > ResetTimeMilliseconds)
            {
                // ..reset the collected key chords..
                MillisecondsPassed = 0;
                keyLocation = 0;
                KeysCollected.Clear();
            }

            // enable the timer..
            TimerKeyDown.Enabled = true;
        }


        /// <summary>
        /// A delegate for the <see cref="KeyboardMultiCombination.KeyCombination"/> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:ScriptNotepad.UtilityClasses.KeyboardShortcutHelper.KeyCombinationInformationEventArgs"/> instance containing the event data.</param>
        public delegate void OnKeyCombination(object sender, KeyCombinationInformationEventArgs e);

        /// <summary>
        /// Occurs when the program detects 
        /// </summary>
        public event OnKeyCombination KeyCombination;

        /// <summary>
        /// A timer to detect a keyboard event expiration.
        /// </summary>
        internal Timer TimerKeyDown { get; set; }

        /// <summary>
        /// A field to hold the keyboard location.
        /// </summary>
        private int keyLocation;

        /// <summary>
        /// Gets or set a list of keyboard combination names matching the current input.
        /// </summary>
        private List<string> CurrentCombinationNames { get; set; }

        /// <summary>
        /// Checks if a given <see cref="Keys"/> enumeration is in the list of key combinations at the current location.
        /// </summary>
        /// <param name="keys">A Keys enumeration value.</param>
        /// <returns>True if a keyboard combination exists in the current chord list; otherwise false.</returns>
        private bool IsSomeChordDown(Keys keys)
        {
            // find all with specific location..
            var keyList = KeyList.Where(f => f.location == keyLocation);

            // loop through the results..
            foreach (var keyEntry in keyList)
            {
                // check if the result contains the key..
                if (keyEntry.keys == keys)
                {
                    // ..if yes then return true..
                    return true;
                }
            }

            // no match was found..
            return false;
        }

        /// <summary>
        /// Handles the KeyDown event of the Form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            // reset the time passed counter on any keyboard event..
            MillisecondsPassed = 0;

            // at least one modifier key must be down and the key code must match a specified
            // key with the specified location on the key chord list..
            if ((e.Alt || e.Control || e.Shift) && IsSomeChordDown(e.KeyCode))
            {
                // get the possible key combinations..
                var combinations = KeyList.Where(f =>
                    f.location == keyLocation && f.modifierAlt == e.Alt && f.modifierCtrl == e.Control &&
                    f.modifierShift == e.Shift && (keyLocation == 0 || CurrentCombinationNames.Contains(f.name))).ToList();

                // get the name of the possible key combinations..
                var names = combinations.Select(f => f.name);

                // set the CurrentCombinationNames property value with the combinations as a list..
                CurrentCombinationNames = new List<string>(names.ToList());

                // if matching combination(s) where found..
                if (combinations.Count > 0)
                {
                    // ..add the collected key combination to the internal list..
                    KeysCollected.Add(new KeyData { Alt = e.Alt, Ctrl = e.Control, Shift = e.Shift, Key = e.KeyCode});

                    // suppress the key press..
                    e.SuppressKeyPress = true;

                    // detect if the collected key combination matches
                    // any specified combinations within this class instance..
                    string name = MatchCombination();

                    // ..a non-null value means yes..
                    if (name != null)
                    {
                        // ..so clear the collected keys and reset the chord location..
                        keyLocation = 0;
                        KeysCollected.Clear();

                        // raise the event with a combination name if subscribed..
                        KeyCombination?.Invoke(form, new KeyCombinationInformationEventArgs { Name = name});
                    }
                    else
                    {
                        // the combination wasn't found yet, but the chord was collected,
                        // increase the location..
                        keyLocation++;
                    }
                }
                // ..nothing valid was found..
                else
                {
                    // ..so do reset the collected key combination list and the chord location..
                    keyLocation = 0;
                    KeysCollected.Clear();
                }
            }
        }

        /// <summary>
        /// Gets a name of a key combination if it should match a user user input key combination.
        /// </summary>
        /// <returns>A name of the match for a key combination if found; otherwise null.</returns>
        private string MatchCombination()
        {
            // initialize a new tuple containing possible valid combinations..
            List<(bool modifierAlt, bool modifierCtrl, bool modifierShift, Keys keys, string name, int location)>
                combinations =
                    new List<(bool modifierAlt, bool modifierCtrl, bool modifierShift, Keys keys, string name, int
                        location)>();

            // get a list of invalid names; they have more chords than the currently
            // collected key combination list within the class..
            List<string> invalidNames =
                KeyList.Where(f => f.location > keyLocation).Select(f => f.name).Distinct().ToList();

            for (int i = 0; i < keyLocation; i++)
            {
                // if the collected chord index is larger than the currently
                // collected amount of key combinations, consider it as invalid..
                if (combinations.Exists(f => f.location > keyLocation))
                {
                    // ..so do continue the loop..
                    continue;
                }

                // add to the list of valid key combinations in case the
                // location and the chord matches the specified keyboard
                // combinations within the class..
                combinations.AddRange(
                    KeyList.Where(f =>
                        f.location == i && f.modifierAlt == KeysCollected[i].Alt &&
                        f.modifierCtrl == KeysCollected[i].Ctrl && f.modifierShift == KeysCollected[i].Shift &&
                        f.keys == KeysCollected[i].Key).ToList());
            }

            // select the collected combination names..
            var combination = combinations.Select(f => f.name).Distinct().ToList();

            // ..if a the name count is one..
            if (combination.Count == 1)
            {
                // ..and the is in the collection of invalid combinations..
                if (invalidNames.Contains(combination.FirstOrDefault()))
                {
                    // ..return null to indicate failure..
                    return null;
                }
                    
                // success..
                return combination.FirstOrDefault();
            }

            // somewhere something went wrong, so return null..
            return null;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            using (TimerKeyDown)
            {
                TimerKeyDown.Enabled = false;
                TimerKeyDown.Tick -= TimerKeyDown_Tick;
            }
            form.KeyDown -= Form_KeyDown; // unsubscribe the keydown event..
        }
    }

    /// <summary>
    /// A class which instance is passed via the <see cref="KeyboardMultiCombination.KeyCombination"/> event.
    /// </summary>
    public class KeyCombinationInformationEventArgs: EventArgs
    {
        /// <summary>
        /// Gets or sets the name of the keyboard combination which occurred.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// A class to hold a single key information.
    /// </summary>
    public class KeyData
    {
        /// <summary>
        /// Gets or set the value whether the Alt modifier key is down.
        /// </summary>
        internal bool Alt { get; set; }

        /// <summary>
        /// Gets or set the value whether the Ctrl modifier key is down.
        /// </summary>
        internal bool Ctrl { get; set; }

        /// <summary>
        /// Gets or set the value whether the Shift modifier key is down.
        /// </summary>
        internal bool Shift { get; set; }

        /// <summary>
        /// Gets or set the value of a key which is down.
        /// </summary>
        internal Keys Key { get; set; }
    }
}
