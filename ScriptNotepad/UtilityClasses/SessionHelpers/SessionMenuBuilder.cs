using ScriptNotepad.Database.TableMethods;
using ScriptNotepad.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.SessionHelpers
{
    /// <summary>
    /// A class to help to build a menu for sessions within the software (<see cref="SESSION_NAME"/>).
    /// </summary>
    public class SessionMenuBuilder
    {
        /// <summary>
        /// Creates the session menu to a given parent tool strip menu item.
        /// </summary>
        /// <param name="parent">The parent tool strip menu item to create the session menu to.</param>
        /// <param name="currentSession">The currently active session name.</param>
        public static void CreateSessionMenu(ToolStripMenuItem parent, string currentSession)
        {
            // first dispose the previous menu..
            DisposeSessionMenu();

            // get the session list from the database..
            List<SESSION_NAME> sessions = DatabaseSessionName.GetSessions();

            foreach (var session in sessions)
            {
                var item = new ToolStripMenuItem
                {
                    Text = session.SESSIONNAME,
                    Tag = session, CheckOnClick = true,
                    Checked = session.SESSIONNAME == currentSession
                };

                item.Click += SessionMenuItem_Click;
                item.Checked = session.SESSIONNAME == currentSession;

                CurrentMenu.Add(item);

                parent.DropDownItems.Add(item);
            }
        }

        /// <summary>
        /// Gets or sets the current menu items in the session parent menu.
        /// </summary>
        private static List<ToolStripMenuItem> CurrentMenu { get; set; } = new List<ToolStripMenuItem>();
             

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
                var sessionMenuItem = (ToolStripMenuItem)item;

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
            SESSION_NAME session = (SESSION_NAME)menuItem.Tag;

            // raise the event if subscribed..
            SessionMenuClicked?.Invoke(sender, new SessionMenuClickEventArgs { SessionName = session, Data = null });
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
        public static event OnSessionMenuClicked SessionMenuClicked = null;
    }

    /// <summary>
    /// Event arguments for the SessionMenuClicked event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class SessionMenuClickEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the session name of the clicked session menu item.
        /// </summary>
        public SESSION_NAME SessionName { get; internal set; }

        /// <summary>
        /// Gets the data associated with the session menu item.
        /// </summary>
        public object Data { get; internal set; }
    }
}

