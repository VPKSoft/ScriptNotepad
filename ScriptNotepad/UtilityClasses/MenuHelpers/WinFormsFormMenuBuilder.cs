using System;
using System.Windows.Forms;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.UtilityClasses.MenuHelpers
{
    /// <summary>
    /// A class to help build an application-wide menu for all the visible forms within the application.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class WinFormsFormMenuBuilder : ErrorHandlingBase, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WinFormsFormMenuBuilder"/> class.
        /// </summary>
        /// <param name="mainItem">The main menu item to the add the open forms within the <see cref="Application"/>.</param>
        public WinFormsFormMenuBuilder(ToolStripMenuItem mainItem)
        {
            // save the window menu for further use..
            this.mainItem = mainItem;

            // subscribe to the window menu's opening event..
            mainItem.DropDownOpening += MainItem_DropDownOpening;

            // create a timer to poll the menu changes..
            Timer = new Timer {Interval = 500, Enabled = true};

            // subscribe to the timer event..
            Timer.Tick += Timer_Tick;
        }

        // this timer will monitor the open form amount of the application..
        private void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Enabled = false;

            // enable/disable the window menu based on the value whether the application has
            // any other open forms than the main form..
            mainItem.Enabled = HasOpenForms;

            Timer.Enabled = true;
        }

        /// <summary>
        /// Gets or sets the timer used to update the enabled state of <see cref="Application"/>'s window menu.
        /// </summary>
        private Timer Timer { get; }

        /// <summary>
        /// A field to hold the main menu item for the opened forms within the <see cref="Application"/>.
        /// </summary>
        private readonly ToolStripMenuItem mainItem;

        private void MainItem_DropDownOpening(object sender, EventArgs e)
        {
            // create a menu of the opened forms within the Application..
            CreateMenuOpenForms();
        }

        /// <summary>
        /// Clears the previous drop-down items from the main menu item.
        /// </summary>
        private void ClearPreviousMenu()
        {
            // reverse loop..
            for (int i = mainItem.DropDownItems.Count - 1; i >= 0; i--)
            {
                // un-subscribe the click event handler..
                mainItem.DropDownItems[i].Click -= Item_Click;
            }

            // clear the items..
            mainItem.DropDownItems.Clear();
        }

        /// <summary>
        /// Gets the value indicating whether the <see cref="Application"/> has opened forms other than the main form.
        /// </summary>
        private bool HasOpenForms
        {
            get
            {
                // a way to get a explicit type for a var variable definition..
                var formMain = (Form)null;

                bool result = false;

                // if there is a main form, do get its instance..
                if (Application.OpenForms.Count > 0)
                {
                    formMain = Application.OpenForms[0];
                }

                // loop through the open forms within the application..
                foreach (Form openForm in Application.OpenForms)
                {
                    // the main form will not be added to the list of open forms..
                    if (openForm.Equals(formMain))
                    {
                        // ..so do continue..
                        continue;
                    }

                    // hidden forms will not be added to the list of open form..
                    if (!openForm.Visible)
                    {
                        continue;
                    }

                    // set the result to true..
                    result = true;

                    // break after the result is set..
                    break;
                }

                return result;
            }
        }

        /// <summary>
        /// Creates the menu of the open forms within the <see cref="Application"/>.
        /// </summary>
        private void CreateMenuOpenForms()
        {
            // clear the previously created menu..
            ClearPreviousMenu();

            // a way to get a explicit type for a var variable definition..
            var formMain = (Form) null; 

            // if there is a main form, do get its instance..
            if (Application.OpenForms.Count > 0)
            {
                formMain = Application.OpenForms[0];
            }

            // loop through the open forms within the application..
            foreach (Form openForm in Application.OpenForms)
            {
                // the main form will not be added to the list of open forms..
                if (openForm.Equals(formMain))
                {
                    // ..so do continue..
                    continue;
                }

                // hidden forms will not be added to the list of open form..
                if (!openForm.Visible)
                {
                    continue;
                }

                // create a new ToolStripMenuItem for the form..
                ToolStripMenuItem item = new ToolStripMenuItem(openForm.Text) {Tag = openForm};

                // subscribe to the click event..
                item.Click += Item_Click;

                // ad the item to the main menu item's DropDownItems collection..
                mainItem.DropDownItems.Add(item);
            }
        }

        // an event that occurs when a user clicks the form menu item..
        private void Item_Click(object sender, EventArgs e)
        {
            // get the sending ToolStripMenuItem..
            var menu = (ToolStripMenuItem) sender;

            // get the Form in question..
            var form = (Form) menu.Tag;

            // display the form..
            form.Show();

            // bring the form to the front..
            form.BringToFront();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // disable the form check timer..
            Timer.Enabled = false;

            // clear the previously created menu..
            ClearPreviousMenu();

            // unsubscribe the events subscribed by this class instance..
            mainItem.DropDownOpening -= MainItem_DropDownOpening;

            using (Timer)
            {
                Timer.Tick -= Timer_Tick;
            }
        }
    }
}
