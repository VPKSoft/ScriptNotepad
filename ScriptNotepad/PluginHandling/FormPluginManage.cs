using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        /// <returns>True if the user accepted the changes made in the dialog; otherwise false.</returns>
        public static bool Execute()
        {
            FormPluginManage formPluginManage = new FormPluginManage();
            return formPluginManage.ShowDialog() == DialogResult.OK;
        }

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
        }
    }
}
