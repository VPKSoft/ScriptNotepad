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

using ScriptNotepad.UtilityClasses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ScriptNotepad.Database.UtilityClasses
{
    /// <summary>
    /// A class for creating a recent file menu for a Windows Forms application.
    /// </summary>
    public static class RecentFilesMenuBuilder
    {
        /// <summary>
        /// A localizable text for a menu item which would open all recent files.
        /// </summary>
        public static string MenuOpenAllRecentText { get; set; } = "Open all recent files...";

        /// <summary>
        /// Creates a recent files menu to a given parent menu.
        /// </summary>
        /// <param name="menuItem">The menu item to add the recent files list.</param>
        /// <param name="sessionName">A name of the session to which the history documents belong to.</param>
        /// <param name="maxCount">Maximum count of recent file entries to add to the given <paramref name="menuItem"/>.</param>
        /// <param name="addMenuOpenAll">A flag indicating whether the menu should contain an item to open all recent files.</param>
        public static void CreateRecentFilesMenu(ToolStripMenuItem menuItem, string sessionName, 
            int maxCount, bool addMenuOpenAll)
        {
            // dispose of the previous menu items..
            DisposeRecentFilesMenu(menuItem);

            // get the recent files from the database..
            IEnumerable<RECENT_FILES> recentFiles = Database.GetRecentFiles(sessionName, maxCount);

            if (addMenuOpenAll)
            {
                List<RECENT_FILES> recentFilesAll = recentFiles.ToList();

                if (recentFilesAll.Count > 1)
                {
                    // create a menu item for all the recent files..
                    DataToolStripMenuItem menuItemRecentFile =
                        new DataToolStripMenuItem(
                            string.IsNullOrWhiteSpace(MenuOpenAllRecentText) ?
                            "Open all recent files..." : MenuOpenAllRecentText);

                    // set the user given additional data for the menu item..
                    menuItemRecentFile.Data = recentFilesAll;

                    // subscribe the click event..
                    menuItemRecentFile.Click += MenuItemRecentFile_Click;

                    // add the menu item to the recent files menu..
                    menuItem.DropDownItems.Add(menuItemRecentFile);

                    // add a separator menu item to the recent files menu..
                    menuItem.DropDownItems.Add(new ToolStripSeparator());
                }
            }

            // loop through the results..
            foreach (var recentFile in recentFiles)
            {
                // create a menu item for the encoding..
                DataToolStripMenuItem menuItemRecentFile = new DataToolStripMenuItem(recentFile.ToString());

                // set the user given additional data for the menu item..
                menuItemRecentFile.Data = recentFile;

                // subscribe the click event..
                menuItemRecentFile.Click += MenuItemRecentFile_Click;

                // add the menu item to the recent files menu..
                menuItem.DropDownItems.Add(menuItemRecentFile);
            }

            // the recent file menu should only be visible if there are any drop down items..
            menuItem.Visible = menuItem.DropDownItems.Count > 0;
        }

        /// <summary>
        /// Handles the Click event of a recent file menu item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private static void MenuItemRecentFile_Click(object sender, EventArgs e)
        {
            // get the clicked item..
            var item = (DataToolStripMenuItem)sender;

            // this shouldn't happen, but just in case..
            if (item.Data == null)
            {
                // ..so just return..
                return;
            }

            // the menu item contains a single recent file..
            if (item.Data.GetType() == typeof(RECENT_FILES))
            {
                // raise the event if subscribed..
                RecentFileMenuClicked?.Invoke(sender,                  // the recent file for the event..
                    new RecentFilesMenuClickEventArgs() { RecentFile = (RECENT_FILES)item.Data, RecentFiles = null });
            }
            // the menu item contains a list of recent files..
            else if (item.Data.GetType() == typeof(List<RECENT_FILES>))
            {
                // raise the event if subscribed..
                RecentFileMenuClicked?.Invoke(sender,                        // the recent file list for the event..
                    new RecentFilesMenuClickEventArgs() { RecentFile = null, RecentFiles = (List<RECENT_FILES>)item.Data });
            }
        }

        /// <summary>
        /// Disposes the recent file menu constructed via the <see cref="CreateRecentFilesMenu"/> method.
        /// </summary>
        /// <param name="parent">The parent tool strip menu item.</param>
        public static void DisposeRecentFilesMenu(ToolStripMenuItem parent)
        {
            List<ToolStripMenuItem> disposeList = new List<ToolStripMenuItem>();
            foreach (var item in parent.DropDownItems)
            {
                // only accept types of ToolStripMenuItem..
                if (item.GetType() != typeof(ToolStripMenuItem))
                {
                    continue;
                }

                // cast the object as ToolStripMenuItem..
                var recentFileMenuItem = (ToolStripMenuItem)item;

                // unsubscribe the event handler..
                recentFileMenuItem.Click -= MenuItemRecentFile_Click;

                // clear the drop down menu item..
                parent.DropDownItems.Clear();

                // add the menu item to the list of ToolStripMenuItems to disposed of..
                disposeList.Add(recentFileMenuItem);
            }

            // clear the drop down items from the parent menu item..
            parent.DropDownItems.Clear();

            // loop through the list of ToolStripMenuItems to disposed of..
            for (int i = 0; i < disposeList.Count; i++)
            {
                // dispose..
                using (disposeList[i])
                {
                    // null assignment isn't necessary, but the using clause 
                    // would look a little "orphan" without that..
                    disposeList[i] = null;
                }
            }

            // no reason to display an empty menu which should have drop down items..
            parent.Visible = false;
        }

        /// <summary>
        /// A delegate for the RecentFileMenuClicked event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="RecentFilesMenuClickEventArgs"/> instance containing the event data.</param>
        public delegate void OnRecentFileMenuClicked(object sender, RecentFilesMenuClickEventArgs e);

        /// <summary>
        /// Occurs when a recent file menu item was clicked.
        /// </summary>
        public static event OnRecentFileMenuClicked RecentFileMenuClicked = null;
    }

    /// <summary>
    /// Event arguments for the RecentFileMenuClicked event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class RecentFilesMenuClickEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the <see cref="RECENT_FILES"/> of the clicked recent file menu item.
        /// </summary>
        public RECENT_FILES RecentFile { get; internal set; }

        /// <summary>
        /// Gets a list of the all <see cref="RECENT_FILES"/> of the clicked recent file menu item to open all the recent files.
        /// </summary>
        public List<RECENT_FILES> RecentFiles { get; internal set; } = null;
    }
}
