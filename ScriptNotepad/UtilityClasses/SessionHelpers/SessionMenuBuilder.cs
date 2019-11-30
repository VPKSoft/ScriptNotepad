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
using System.Windows.Forms;
using ScriptNotepad.Database.Entity.Context;
using ScriptNotepad.Database.Entity.Entities;

namespace ScriptNotepad.UtilityClasses.SessionHelpers
{
    /// <summary>
    /// A class to help to build a menu for sessions within the software (<see cref="Database.Entity.Entities.FileSession"/>).
    /// </summary>
    public class SessionMenuBuilder
    {
        /// <summary>
        /// Creates the session menu to a given parent tool strip menu item.
        /// </summary>
        /// <param name="parent">The parent tool strip menu item to create the session menu to.</param>
        /// <param name="currentSession">The currently active session.</param>
        public static void CreateSessionMenu(ToolStripMenuItem parent, FileSession currentSession)
        {
            // first dispose the previous menu..
            DisposeSessionMenu();

            foreach (var session in ScriptNotepadDbContext.DbContext.FileSessions)
            {
                var item = new ToolStripMenuItem
                {
                    Text = session.SessionName,
                    Tag = session, CheckOnClick = true,
                    Checked = session.Equals(currentSession)
                };

                item.Click += SessionMenuItem_Click;
                item.Checked = session.Equals(currentSession);

                CurrentMenu.Add(item);

                parent.DropDownItems.Add(item);
            }
        }

        /// <summary>
        /// Gets or sets the current menu items in the session parent menu.
        /// </summary>
        private static List<ToolStripMenuItem> CurrentMenu { get; } = new List<ToolStripMenuItem>();
             

        /// <summary>
        /// Disposes the session menu.
        /// </summary>
        public static void DisposeSessionMenu()
        {
            // create a list of tool strip menu items to dispose of..
            List<ToolStripMenuItem> disposeList = new List<ToolStripMenuItem>();
            foreach (var item in CurrentMenu)
            {
                // only accept types of ToolStripMenuItem..
                if (item.GetType() != typeof(ToolStripMenuItem))
                {
                    continue;
                }

                // cast the object as ToolStripMenuItem..
                var sessionMenuItem = item;

                disposeList.Add(sessionMenuItem);
            }

            // clear the drop down items from the parent menu item..
            CurrentMenu.Clear();

            // loop through the list of ToolStripMenuItems to disposed of..
            for (int i = 0; i < disposeList.Count; i++)
            {
                // dispose..
                using (disposeList[i])
                {
                    // unsubscribe the internal event..
                    disposeList[i].Click -= SessionMenuItem_Click;
                }
            }
        }

        // a user clicked a drop-down menu item in the sessions menu..
        private static void SessionMenuItem_Click(object sender, EventArgs e)
        {
            // toggle the checked state of the session in the sessions menu..
            foreach (var item in CurrentMenu)
            {
                item.Checked = item.Equals(sender);
            }

            // get the data from the clicked menu item..
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            var session = (FileSession)menuItem.Tag;

            // raise the event if subscribed..
            SessionMenuClicked?.Invoke(sender, new SessionMenuClickEventArgs { Session = session, Data = null });
        }

        /// <summary>
        /// A delegate for the SessionMenuClicked event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="SessionMenuClickEventArgs"/> instance containing the event data.</param>
        public delegate void OnSessionMenuClicked(object sender, SessionMenuClickEventArgs e);

        /// <summary>
        /// Occurs when a session menu item was clicked.
        /// </summary>
        public static event OnSessionMenuClicked SessionMenuClicked;
    }

    /// <summary>
    /// Event arguments for the SessionMenuClicked event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class SessionMenuClickEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the session of the clicked session menu item.
        /// </summary>
        public FileSession Session { get; internal set; }

        /// <summary>
        /// Gets the data associated with the session menu item.
        /// </summary>
        public object Data { get; internal set; }
    }
}

