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

using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.Assembly;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ScriptNotepad.Database.Entity.Entities;
using VPKSoft.ErrorLogger;
using VPKSoft.LangLib;
using VPKSoft.MessageBoxExtended;

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
                DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages");
        }

        private List<Plugin> plugins = new List<Plugin>();

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        /// <returns>True if the user accepted the changes made in the dialog; otherwise false.</returns>
        public static bool Execute(ref List<Plugin> plugins)
        {
            FormPluginManage formPluginManage = new FormPluginManage {plugins = plugins};


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
                plugins.OrderBy(f => f.SortOrder).
                    ThenByDescending(f => f.Rating).
                    ThenBy(f => f.PluginName.ToLowerInvariant()).
                    ThenBy(f => f.PluginVersion.ToLowerInvariant()).ToList();

            if (!string.IsNullOrWhiteSpace(filterString))
            {
                sortedAndFiltered =
                    sortedAndFiltered.Where(f => f.ToString().ToLowerInvariant().Contains(filterString.ToLowerInvariant())).ToList();
            }

            foreach (var plugin in sortedAndFiltered)
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
        private void DisplayPluginData(Plugin plugin)
        {
            tbPluginName.Text = plugin == null ? string.Empty : plugin.PluginName;
            tbPluginDescription.Text = plugin == null ? string.Empty : plugin.PluginDescription;
            tbPluginVersion.Text = plugin == null ? string.Empty : plugin.PluginVersion;
            tbExceptionsThrown.Text = plugin == null ? string.Empty : plugin.ExceptionCount.ToString();
            tbLoadFailures.Text = plugin == null ? string.Empty : plugin.LoadFailures.ToString();
            tbAppCrashes.Text = plugin == null ? string.Empty : plugin.ApplicationCrashes.ToString();
            tbInstalledAt.Text = plugin == null ? string.Empty : plugin.PluginInstalled.ToShortDateString();
            tbUpdatedAt.Text = plugin == null ? string.Empty : (plugin.PluginUpdated == DateTime.MinValue ?
                DBLangEngine.GetMessage("msgNA", "N/A|A message indicating a none value") :
                plugin.PluginUpdated.ToShortDateString());
            nudRating.Value = plugin?.Rating ?? 0;

            cbPluginActive.Checked = plugin != null && plugin.IsActive;
            cbPluginExists.Checked = plugin != null && File.Exists(plugin.FileNameFull);

            nudRating.Enabled = plugin != null;
            btDeletePlugin.Enabled = plugin != null;
            cbPluginActive.Enabled = plugin != null;

            // indicate a serious flaw with the plug-in..
            if (plugin != null && plugin.ApplicationCrashes > 0)
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
            Plugin plugin = (Plugin)listBox.SelectedItem;
            DisplayPluginData(plugin);
        }

        private void tbFilterPlugins_TextChanged(object sender, EventArgs e)
        {
            ListPlugins(tbFilterPlugins.Text);
        }

        private void btDeletePlugin_Click(object sender, EventArgs e)
        {
            Plugin plugin = (Plugin)lbPluginList.SelectedItem;

            if (plugin != null)
            {
                int idx = plugins.FindIndex(f => f.Id == plugin.Id);

                if (idx != -1)
                {
                    plugins[idx].PendingDeletion = true;
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
                    var plugin = (Plugin)lbPluginList.Items[i];
                    int idx = plugins.FindIndex(f => f.Id == plugin.Id);
                    if (idx != -1)
                    {
                        plugins[idx].SortOrder = i;
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
                            // ReSharper disable once AssignNullToNotNullAttribute
                            Path.Combine(FormSettings.Settings.PluginFolder, Path.GetFileName(odDLL.FileName)), 
                            true);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxExtended.Show(
                            DBLangEngine.GetMessage("msgErrorPluginInstall",
                            "The plug-in instillation failed with the following error: '{0}'.|Something failed during copying a plug-in to the plug-ins folder", ex.Message),
                            DBLangEngine.GetMessage("msgError", "Error|A message describing that some kind of error occurred."),
                            MessageBoxButtonsExtended.OK, MessageBoxIcon.Error, ExtendedDefaultButtons.Button1);

                        // log the exception..
                        ExceptionLogger.LogError(ex);
                    }
                }
                else
                {
                    MessageBoxExtended.Show(
                        DBLangEngine.GetMessage("msgWarningPluginNotAssembly",
                        "The plug-in initialization failed because it is not a valid .NET Framework assembly.|The given assembly isn't a .NET Framework assembly"),
                        DBLangEngine.GetMessage("msgWarning", "Warning|A message warning of some kind problem."),
                        MessageBoxButtonsExtended.OK, MessageBoxIcon.Warning, ExtendedDefaultButtons.Button1);
                }
            }
        }

        // the user wishes to toggle the active state of a plug-in..
        private void cbPluginActive_CheckedChanged(object sender, EventArgs e)
        {
            var plugin = (Plugin)lbPluginList.SelectedItem;

            if (plugin != null)
            {
                int idx = plugins.FindIndex(f => f.Id == plugin.Id);

                if (idx != -1)
                {
                    plugins[idx].IsActive = ((CheckBox)sender).Checked;
                }
            }
        }
    }
}
