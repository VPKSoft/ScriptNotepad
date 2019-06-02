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

using ScriptNotepad.Database.Tables;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.Assembly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPKSoft.ErrorLogger;
using VPKSoft.LangLib;

namespace ScriptNotepad.PluginHandling
{
    /// <summary>
    /// A form to manage installed plug-ins for the software.
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormPluginManage : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormPluginManage"/> class.
        /// </summary>
        public FormPluginManage()
        {
            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages");
        }

        private List<PLUGINS> plugins = new List<PLUGINS>();

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        /// <returns>True if the user accepted the changes made in the dialog; otherwise false.</returns>
        public static bool Execute(ref List<PLUGINS> plugins)
        {
            FormPluginManage formPluginManage = new FormPluginManage();

            formPluginManage.plugins = plugins;

            if (formPluginManage.ShowDialog() == DialogResult.OK)
            {
                plugins = formPluginManage.plugins;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Lists the plug-ins to the list box.
        /// </summary>
        private void ListPlugins()
        {
            ListPlugins(string.Empty);
        }

        /// <summary>
        /// Lists the plug-ins to the list box and filters them if a filter string is given.
        /// </summary>
        /// <param name="filterString">A filter string to filter the plug-ins in the list box.</param>
        private void ListPlugins(string filterString)
        {
            lbPluginList.Items.Clear();
            var sortedAndFiltered =
                plugins.OrderBy(f => f.SORTORDER).
                    OrderByDescending(f => f.RATING).
                    OrderBy(f => f.PLUGIN_NAME.ToLowerInvariant()).
                    OrderBy(f => f.PLUGIN_VERSION.ToLowerInvariant()).ToList();

            if (!string.IsNullOrWhiteSpace(filterString))
            {
                sortedAndFiltered =
                    sortedAndFiltered.Where(f => f.ToString().ToLowerInvariant().Contains(filterString.ToLowerInvariant())).ToList();
            }

            foreach (PLUGINS plugin in sortedAndFiltered)
            {
                lbPluginList.Items.Add(plugin);
            }

            if (lbPluginList.Items.Count > 0)
            {
                lbPluginList.SelectedIndex = 0;
            }
            else
            {
                DisplayPluginData(null);
            }
        }

        /// <summary>
        /// Aligns the order tool strip to the view.
        /// </summary>
        private void AlignArrangeToolStrip()
        {
            ttArrangePlugins.Left = (pnArrangePlugins.Width - ttArrangePlugins.Width) / 2;
            ttArrangePlugins.Top = (pnArrangePlugins.Height - ttArrangePlugins.Height) / 2;
        }

        private void FormPluginManage_SizeChanged(object sender, EventArgs e)
        {
            AlignArrangeToolStrip();
        }

        private void FormPluginManage_Shown(object sender, EventArgs e)
        {
            AlignArrangeToolStrip();
            ListPlugins();
        }

        /// <summary>
        /// Displays the data of the given plug-in.
        /// </summary>
        /// <param name="plugin">The plug-in which data to display.</param>
        private void DisplayPluginData(PLUGINS plugin)
        {
            tbPluginName.Text = plugin == null ? string.Empty : plugin.PLUGIN_NAME;
            tbPluginDescription.Text = plugin == null ? string.Empty : plugin.PLUGIN_DESCTIPTION;
            tbPluginVersion.Text = plugin == null ? string.Empty : plugin.PLUGIN_VERSION;
            tbExceptionsThrown.Text = plugin == null ? string.Empty : plugin.EXCEPTION_COUNT.ToString();
            tbLoadFailures.Text = plugin == null ? string.Empty : plugin.LOAD_FAILURES.ToString();
            tbAppCrashes.Text = plugin == null ? string.Empty : plugin.APPLICATION_CRASHES.ToString();
            tbInstalledAt.Text = plugin == null ? string.Empty : plugin.PLUGIN_INSTALLED.ToShortDateString();
            tbUpdatedAt.Text = plugin == null ? string.Empty : (plugin.PLUGIN_UPDATED == DateTime.MinValue ?
                DBLangEngine.GetMessage("msgNA", "N/A|A message indicating a none value") :
                plugin.PLUGIN_UPDATED.ToShortDateString());
            nudRating.Value = plugin == null ? 0 : plugin.RATING;

            cbPluginActive.Checked = plugin == null ? false : plugin.ISACTIVE;
            cbPluginExists.Checked = plugin == null ? false : plugin.Exists;

            nudRating.Enabled = plugin != null;
            btDeletePlugin.Enabled = plugin != null;
            cbPluginActive.Enabled = plugin != null;

            // indicate a serious flaw with the plug-in..
            if (plugin != null && plugin.APPLICATION_CRASHES > 0)
            {
                lbAppCrashes.Font = new Font(Font, FontStyle.Bold);
                lbAppCrashes.ForeColor = Color.Red;
            }
            // the plug-in is more or less safe..
            else 
            {
                lbAppCrashes.Font = Font;
                lbAppCrashes.ForeColor = SystemColors.ControlText;
            }
        }

        private void lbPluginList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (tsbArrangePlugins.Checked)
            {
                // the plug-ins are being sorted by the user..
                return;
            }
            ListBox listBox = (ListBox)sender;
            PLUGINS plugin = (PLUGINS)listBox.SelectedItem;
            DisplayPluginData(plugin);
        }

        private void tbFilterPlugins_TextChanged(object sender, EventArgs e)
        {
            ListPlugins(tbFilterPlugins.Text);
        }

        private void btDeletePlugin_Click(object sender, EventArgs e)
        {
            PLUGINS plugin = (PLUGINS)lbPluginList.SelectedItem;

            if (plugin != null)
            {
                int idx = plugins.FindIndex(f => f.ID == plugin.ID);

                if (idx != -1)
                {
                    plugins[idx].PENDING_DELETION = true;
                }
            }
        }

        // a user wishes to arrange the plug-ins..
        private void tsbArrangePlugins_Click(object sender, EventArgs e)
        {
            ToolStripButton button = (ToolStripButton)sender;
            tbFilterPlugins.Enabled = !button.Checked;
            tsbArrangePluginDownwards.Enabled = button.Checked;
            tsbArrangePluginUpwards.Enabled = button.Checked;
            if (button.Checked)
            {
                DisplayPluginData(null);
                ListPlugins();
            }
            else
            {
                // save the user set ordering..
                for (int i = 0; i < lbPluginList.Items.Count; i++)
                {
                    PLUGINS plugin = (PLUGINS)lbPluginList.Items[i];
                    int idx = plugins.FindIndex(f => f.ID == plugin.ID);
                    if (idx != -1)
                    {
                        plugins[idx].SORTORDER = i;
                    }
                }
                ListPlugins(tbFilterPlugins.Text);
            }
        }

        // swap the items so the selected plug-in goes downwards in the ordering..
        private void tsbArrangePluginDownwards_Click(object sender, EventArgs e)
        {
            if (lbPluginList.SelectedIndex != -1 &&
                lbPluginList.SelectedIndex + 1 < lbPluginList.Items.Count) 
            {
                object item1 = lbPluginList.Items[lbPluginList.SelectedIndex];
                object item2 = lbPluginList.Items[lbPluginList.SelectedIndex + 1];
                lbPluginList.Items[lbPluginList.SelectedIndex + 1] = item1;
                lbPluginList.Items[lbPluginList.SelectedIndex] = item2;

                // increase the selected index..
                lbPluginList.SelectedIndex++;
            }
        }

        // swap the items so the selected plug-in goes upwards in the ordering..
        private void tsbArrangePluginUpwards_Click(object sender, EventArgs e)
        {
            if (lbPluginList.SelectedIndex != -1 &&
                lbPluginList.SelectedIndex - 1 > 0)
            {
                object item1 = lbPluginList.Items[lbPluginList.SelectedIndex];
                object item2 = lbPluginList.Items[lbPluginList.SelectedIndex - 1];
                lbPluginList.Items[lbPluginList.SelectedIndex - 1] = item1;
                lbPluginList.Items[lbPluginList.SelectedIndex] = item2;

                // decrease the selected index..
                lbPluginList.SelectedIndex++;
            }
        }

        private void btInstallPlugin_Click(object sender, EventArgs e)
        {
            odDLL.Title = DBLangEngine.GetMessage("msgDialogSelectPlugin",
                "Select a plugin to install|A title for an open file dialog to indicate user that the user is selecting a plugin dll to be installed");

            odDLL.InitialDirectory = FormSettings.Settings.FileLocationOpenPlugin;

            if (odDLL.ShowDialog() == DialogResult.OK)
            {
                FormSettings.Settings.FileLocationOpenPlugin = Path.GetDirectoryName(odDLL.FileName);

                if (TestFileIsAssembly.IsAssembly(odDLL.FileName))
                {
                    try
                    {
                        // try to copy the file to the plug-in folder..
                        File.Copy(odDLL.FileName, 
                            Path.Combine(FormSettings.Settings.PluginFolder, Path.GetFileName(odDLL.FileName)), 
                            true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            DBLangEngine.GetMessage("msgErrorPluginInstall",
                            "The plug-in instillation failed with the following error: '{0}'.|Something failed during copying a plug-in to the plug-ins folder", ex.Message),
                            DBLangEngine.GetMessage("msgError", "Error|A message describing that some kind of error occurred."),
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        // log the exception..
                        ExceptionLogger.LogError(ex);
                    }
                }
                else
                {
                    MessageBox.Show(
                        DBLangEngine.GetMessage("msgWarningPluginNotAssembly",
                        "The plug-in initialization failed because it is not a valid .NET Framework assembly.|The given assembly isn't a .NET Framework assembly"),
                        DBLangEngine.GetMessage("msgWarning", "Warning|A message warning of some kind problem."),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
        }

        // the user wishes to toggle the active state of a plug-in..
        private void cbPluginActive_CheckedChanged(object sender, EventArgs e)
        {
            PLUGINS plugin = (PLUGINS)lbPluginList.SelectedItem;

            if (plugin != null)
            {
                int idx = plugins.FindIndex(f => f.ID == plugin.ID);

                if (idx != -1)
                {
                    plugins[idx].ISACTIVE = ((CheckBox)sender).Checked;
                }
            }
        }
    }
}
