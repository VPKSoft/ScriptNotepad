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

using ScriptNotepadPluginBase.About;
using ScriptNotepadPluginBase.EventArgClasses;
using ScriptNotepadPluginBase.PluginTemplateInterface;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using static ScriptNotepadPluginBase.Types.DelegateTypes;

namespace PluginTemplate
{
    /// <summary>
    /// An interface to wright plug-ins for ScriptNotepad software.
    /// </summary>
    /// <seealso cref="PluginTemplate.PluginTemplateInterface.IScriptNotepadPlugin" />
    public class SamplePlugin : ScriptNotepadPlugin, IScriptNotepadPlugin
    {
        /// <summary>
        /// Occurs when the plug-in requests the active document of the ScriptNotepad.
        /// </summary>
        public event OnRequestActiveDocument RequestActiveDocument = null;

        /// <summary>
        /// Occurs when the plug-in requests for all the open documents of the ScriptNotepad.
        /// </summary>
        public event OnRequestAllDocuments RequestAllDocuments = null;

        /// <summary>
        /// Occurs when an exception occurred within the plug-in so the hosting 
        /// software (ScriptNotepad) can log it and possibly take necessary actions 
        /// for the plug-in (i.e. disable it).
        /// </summary>
        public event OnPluginException PluginException = null;

        /// <summary>
        /// A field to save the event to unsubscribe it in the Dispose method.
        /// </summary>
        private OnRequestActiveDocument onRequestActiveDocument;

        /// <summary>
        /// A field to save the event to unsubscribe it in the Dispose method.
        /// </summary>
        private OnRequestAllDocuments onRequestAllDocuments;

        /// <summary>
        /// A field to save the event to unsubscribe it in the Dispose method.
        /// </summary>
        private OnPluginException onPluginException;

        /// <summary>
        /// A menu strip given by the main software (ScriptNotepad) for a plug-in to construct it's own menu.
        /// </summary>
        private ToolStripMenuItem pluginMenuStrip = null;

        /// <summary>
        /// Gets the name of the plug-in (i.e. "My Awesome Plug-in).
        /// </summary>
        public string PluginName { get => "SamplePlugin"; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // unsubscribe the event handlers..
            RequestActiveDocument -= onRequestActiveDocument;
            RequestAllDocuments -= onRequestAllDocuments;
            PluginException -= onPluginException;
            // END: unsubscribe the event handlers..

            // leave no references to the host program (ScriptNotepad)..
            ScriptNotepadMainForm = null;

            // dispose of the menu constructed by this plug-in..
            DisposeMenu();
        }

        /// <summary>
        /// Gets or sets the name of the session.
        /// </summary>
        public string SessionName { get; set; }

        /// <summary>
        /// The description of this plug-in.
        /// </summary>
        public string PluginDescription { get; set; } = "Sample plug-in by VPKSoft";

        /// <summary>
        /// A list containing messages for localization. Please do fill at least the en-US localization.
        /// </summary>
        public List<(string MessageName, string Message, string CultureName)> LocalizationTexts { get; set; } = new List<(string MessageName, string Message, string CultureName)>();

        /// <summary>
        /// The main form of the hosting software (ScriptNotepad).
        /// </summary>
        public Form ScriptNotepadMainForm { get; set; } = null;

        // set the culture to current UI culture..
        private string _Locale = CultureInfo.CurrentUICulture.Name;

        /// <summary>
        /// Gets or sets the current locale for the plug-in.
        /// </summary>
        public string Locale
        {
            get => _Locale;
            set
            {
                _Locale = value;
                // and some localization here..
                PluginDescription = GetMessage("plgDescription", "Sample plug-in by VPKSoft", value);

                // the about menu for this plug-in has been constructed..
                if (pluginAboutMenu != null)
                {
                    // ..localize the about menu constructed by this plug-in..
                    pluginAboutMenu.Text = GetMessage("txtAbout", "About", value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the tool strip menu item the plug-in constructed.
        /// </summary>
        public ToolStripMenuItem PluginMenu { get; set; }

        /// <summary>
        /// A drop down menu item for the <see cref="PluginMenu"/>.
        /// </summary>
        ToolStripMenuItem pluginMainMenu;

        /// <summary>
        /// A drop down menu item for the <see cref="pluginMainMenu"/>.
        /// </summary>
        ToolStripMenuItem pluginAboutMenu;

        /// <summary>
        /// Additional initialization method for the plug-in.
        /// </summary>
        /// <param name="onRequestActiveDocument">The event provided by the hosting software (ScriptNotepad) to request for the active document within the software.</param>
        /// <param name="onRequestAllDocuments">The event provided by the hosting software (ScriptNotepad) to request for all open documents within the software.</param>
        /// <param name="onPluginException">The event provided by the hosting software (ScriptNotepad) for error reporting.</param>
        /// <param name="pluginMenuStrip">The <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> which is the plug-in menu in the hosting software (ScriptNotepad).</param>
        /// <param name="sessionName">The name of the current session in the hosting software (ScriptNotepad).</param>
        /// <param name="scriptNotepadMainForm">A reference to the main form of the hosting software (ScriptNotepad).</param>
        public void Initialize(OnRequestActiveDocument onRequestActiveDocument, 
            OnRequestAllDocuments onRequestAllDocuments,
            OnPluginException onPluginException,
            ToolStripMenuItem pluginMenuStrip,
            string sessionName,
            Form scriptNotepadMainForm)
        {
            // save the given delegates so they can be unsubscribed on disposal..
            this.onRequestActiveDocument = onRequestActiveDocument;
            this.onRequestAllDocuments = onRequestAllDocuments;
            this.onPluginException = onPluginException;

            // save the main form of the ScriptNotepad software..
            ScriptNotepadMainForm = scriptNotepadMainForm;

            // save the plug-in menu string of the ScriptNotepad software..
            this.pluginMenuStrip = pluginMenuStrip;

            // save the name of the current session of the ScriptNotepad software..
            SessionName = sessionName;

            // subscribe the event handlers..
            RequestActiveDocument += onRequestActiveDocument;
            RequestAllDocuments += onRequestAllDocuments;
            PluginException += onPluginException;
            // END: subscribe the event handlers..

            // create a menu for the plug-in..
            pluginMainMenu = new ToolStripMenuItem() { Text = PluginName, Tag = this };
            pluginMenuStrip.DropDownItems.Add(pluginMainMenu);

            pluginAboutMenu = new ToolStripMenuItem() { Text = GetMessage("txtAbout", "About", Locale), Tag = this };
            pluginMainMenu.DropDownItems.Add(pluginAboutMenu);

            // subscribe events for the menu created by the plug-in..
            pluginAboutMenu.Click += PluginAboutMenu_Click;

            // write extra initialization code here if required..
        }

        /// <summary>
        /// Handles the Click event of the pluginAboutMenu <see cref="ToolStripMenuItem"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PluginAboutMenu_Click(object sender, System.EventArgs e)
        {
            // display the about dialog for this plug-in from a self-created menu item click..
            AbountDialog();
        }

        /// <summary>
        /// This method is called by the hosting software (ScriptNotepad) if documents in the software have been changed.
        /// </summary>
        public void NotifyDocumentChanged()
        {
            RequestScintillaDocumentEventArgs args = new RequestScintillaDocumentEventArgs() { AllDocuments = false };
            RequestActiveDocument?.Invoke(this, args);
        }

        /// <summary>
        /// The basic constructor for the plug-in.
        /// </summary>
        public SamplePlugin()
        {
            GetLocalizedTexts(Properties.Resources.tab_deli_localization);
            PluginDescription = GetMessage("plgDescription", "Sample plug-in by VPKSoft", Locale);
            // write extra initialization code here if required..
        }

        /// <summary>
        /// Displays the about dialog for this plug-in.
        /// </summary>
        public void AbountDialog()
        {
            // display the about dialog for the plug-in..
            new FormPluginAbout(
                ScriptNotepadMainForm, // the hosting software main form (ScriptNotepad)..
                Assembly.GetAssembly(GetType()), // get the assembly (this) the about dialog should use..
                "MIT", // give a name for the license..
                "https://raw.githubusercontent.com/VPKSoft/ScriptNotepadPluginBase/master/LICENSE", // give a link to the license..
                Locale, // give the current locale for the dialog..
                Properties.Resources.VPKSoft, // give an icon for the dialog..
                PluginName, // give this plug-in name for the dialog..
                Properties.Resources.VPKSoftLogo_App); // give a logo banner for the dialog..
        }

        /// <summary>
        /// Disposes the menu created by the plug-in.
        /// </summary>
        public void DisposeMenu()
        {
            // the Dispose method or this method may be called before the Initialize method (!),
            // so the null check if the menu even has been constructed..
            if (pluginMainMenu != null)
            {
                // remove the menu constructed by the plug-in from the hosting
                // program's (ScriptNotepad) menu for plug-ins..
                pluginMenuStrip.DropDownItems.Remove(pluginMainMenu);

                // unsubscribe the events..
                pluginAboutMenu.Click -= PluginAboutMenu_Click; 

                using (pluginMainMenu)
                {
                    // dispose of the menu created for the plug-in..
                };

                // leave no references to the host program's (ScriptNotepad) menu..
                pluginMenuStrip = null;
            }
        }
    }
}
