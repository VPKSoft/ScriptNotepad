namespace ScriptNotepad.PluginHandling
{
    partial class FormPluginManage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPluginManage));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.btOK = new System.Windows.Forms.Button();
            this.tlpPluginList = new System.Windows.Forms.TableLayoutPanel();
            this.lbFilterPlugins = new System.Windows.Forms.Label();
            this.tbFilterPlugins = new System.Windows.Forms.TextBox();
            this.lbPluginList = new System.Windows.Forms.ListBox();
            this.pnArrangePlugins = new System.Windows.Forms.Panel();
            this.ttArrangePlugins = new System.Windows.Forms.ToolStrip();
            this.tsbArrangePluginDownwards = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbArrangePlugins = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbArrangePluginUpwards = new System.Windows.Forms.ToolStripButton();
            this.btInstallPlugin = new System.Windows.Forms.Button();
            this.gpPluginSettings = new System.Windows.Forms.GroupBox();
            this.tlpPluginStats = new System.Windows.Forms.TableLayoutPanel();
            this.lbPluginVersion = new System.Windows.Forms.Label();
            this.cbPluginActive = new System.Windows.Forms.CheckBox();
            this.tbUpdatedAt = new System.Windows.Forms.TextBox();
            this.tbPluginVersion = new System.Windows.Forms.TextBox();
            this.lbUpdatedAt = new System.Windows.Forms.Label();
            this.lbExceptionThrown = new System.Windows.Forms.Label();
            this.tbInstalledAt = new System.Windows.Forms.TextBox();
            this.tbExceptionsThrown = new System.Windows.Forms.TextBox();
            this.lbInstalledAt = new System.Windows.Forms.Label();
            this.lbLoadFailures = new System.Windows.Forms.Label();
            this.tbAppCrashes = new System.Windows.Forms.TextBox();
            this.tbLoadFailures = new System.Windows.Forms.TextBox();
            this.lbAppCrashes = new System.Windows.Forms.Label();
            this.lbPluginRating = new System.Windows.Forms.Label();
            this.nudRating = new System.Windows.Forms.NumericUpDown();
            this.lbPluginActive = new System.Windows.Forms.Label();
            this.lbPluginExists = new System.Windows.Forms.Label();
            this.cbPluginExists = new System.Windows.Forms.CheckBox();
            this.btDeletePlugin = new System.Windows.Forms.Button();
            this.tbPluginDescription = new System.Windows.Forms.TextBox();
            this.lbPluginDescription = new System.Windows.Forms.Label();
            this.tbPluginName = new System.Windows.Forms.TextBox();
            this.lbPluginName = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.odDLL = new System.Windows.Forms.OpenFileDialog();
            this.tlpMain.SuspendLayout();
            this.tlpPluginList.SuspendLayout();
            this.pnArrangePlugins.SuspendLayout();
            this.ttArrangePlugins.SuspendLayout();
            this.gpPluginSettings.SuspendLayout();
            this.tlpPluginStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRating)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.ColumnCount = 6;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33313F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33313F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33313F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0002F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0002F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0002F));
            this.tlpMain.Controls.Add(this.btOK, 3, 5);
            this.tlpMain.Controls.Add(this.tlpPluginList, 0, 0);
            this.tlpMain.Controls.Add(this.gpPluginSettings, 3, 0);
            this.tlpMain.Controls.Add(this.btCancel, 5, 5);
            this.tlpMain.Location = new System.Drawing.Point(12, 12);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 6;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(758, 425);
            this.tlpMain.TabIndex = 0;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(306, 396);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 26);
            this.btOK.TabIndex = 7;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // tlpPluginList
            // 
            this.tlpPluginList.ColumnCount = 1;
            this.tlpMain.SetColumnSpan(this.tlpPluginList, 3);
            this.tlpPluginList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPluginList.Controls.Add(this.lbFilterPlugins, 0, 0);
            this.tlpPluginList.Controls.Add(this.tbFilterPlugins, 0, 1);
            this.tlpPluginList.Controls.Add(this.lbPluginList, 0, 3);
            this.tlpPluginList.Controls.Add(this.pnArrangePlugins, 0, 2);
            this.tlpPluginList.Controls.Add(this.btInstallPlugin, 0, 4);
            this.tlpPluginList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPluginList.Location = new System.Drawing.Point(0, 0);
            this.tlpPluginList.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPluginList.Name = "tlpPluginList";
            this.tlpPluginList.RowCount = 5;
            this.tlpMain.SetRowSpan(this.tlpPluginList, 6);
            this.tlpPluginList.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPluginList.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPluginList.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPluginList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPluginList.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPluginList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPluginList.Size = new System.Drawing.Size(303, 425);
            this.tlpPluginList.TabIndex = 0;
            // 
            // lbFilterPlugins
            // 
            this.lbFilterPlugins.AutoSize = true;
            this.lbFilterPlugins.Location = new System.Drawing.Point(3, 0);
            this.lbFilterPlugins.Name = "lbFilterPlugins";
            this.lbFilterPlugins.Size = new System.Drawing.Size(32, 13);
            this.lbFilterPlugins.TabIndex = 0;
            this.lbFilterPlugins.Text = "Filter:";
            // 
            // tbFilterPlugins
            // 
            this.tbFilterPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilterPlugins.Location = new System.Drawing.Point(3, 16);
            this.tbFilterPlugins.Name = "tbFilterPlugins";
            this.tbFilterPlugins.Size = new System.Drawing.Size(297, 20);
            this.tbFilterPlugins.TabIndex = 1;
            this.tbFilterPlugins.TextChanged += new System.EventHandler(this.tbFilterPlugins_TextChanged);
            // 
            // lbPluginList
            // 
            this.lbPluginList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPluginList.FormattingEnabled = true;
            this.lbPluginList.Location = new System.Drawing.Point(3, 73);
            this.lbPluginList.Name = "lbPluginList";
            this.lbPluginList.Size = new System.Drawing.Size(297, 317);
            this.lbPluginList.TabIndex = 2;
            this.lbPluginList.SelectedValueChanged += new System.EventHandler(this.lbPluginList_SelectedValueChanged);
            // 
            // pnArrangePlugins
            // 
            this.pnArrangePlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnArrangePlugins.Controls.Add(this.ttArrangePlugins);
            this.pnArrangePlugins.Location = new System.Drawing.Point(0, 39);
            this.pnArrangePlugins.Margin = new System.Windows.Forms.Padding(0);
            this.pnArrangePlugins.Name = "pnArrangePlugins";
            this.pnArrangePlugins.Size = new System.Drawing.Size(303, 31);
            this.pnArrangePlugins.TabIndex = 9;
            // 
            // ttArrangePlugins
            // 
            this.ttArrangePlugins.Dock = System.Windows.Forms.DockStyle.None;
            this.ttArrangePlugins.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ttArrangePlugins.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbArrangePluginDownwards,
            this.toolStripSeparator2,
            this.tsbArrangePlugins,
            this.toolStripSeparator1,
            this.tsbArrangePluginUpwards});
            this.ttArrangePlugins.Location = new System.Drawing.Point(20, 0);
            this.ttArrangePlugins.Name = "ttArrangePlugins";
            this.ttArrangePlugins.Size = new System.Drawing.Size(84, 25);
            this.ttArrangePlugins.TabIndex = 0;
            // 
            // tsbArrangePluginDownwards
            // 
            this.tsbArrangePluginDownwards.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArrangePluginDownwards.Enabled = false;
            this.tsbArrangePluginDownwards.Image = global::ScriptNotepad.Properties.Resources.Down_2;
            this.tsbArrangePluginDownwards.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArrangePluginDownwards.Name = "tsbArrangePluginDownwards";
            this.tsbArrangePluginDownwards.Size = new System.Drawing.Size(23, 22);
            this.tsbArrangePluginDownwards.Text = "Change sort order to lower";
            this.tsbArrangePluginDownwards.Click += new System.EventHandler(this.tsbArrangePluginDownwards_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbArrangePlugins
            // 
            this.tsbArrangePlugins.CheckOnClick = true;
            this.tsbArrangePlugins.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArrangePlugins.Image = global::ScriptNotepad.Properties.Resources.up_down;
            this.tsbArrangePlugins.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArrangePlugins.Name = "tsbArrangePlugins";
            this.tsbArrangePlugins.Size = new System.Drawing.Size(23, 22);
            this.tsbArrangePlugins.Text = "Arrange plug-ins";
            this.tsbArrangePlugins.Click += new System.EventHandler(this.tsbArrangePlugins_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbArrangePluginUpwards
            // 
            this.tsbArrangePluginUpwards.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArrangePluginUpwards.Enabled = false;
            this.tsbArrangePluginUpwards.Image = global::ScriptNotepad.Properties.Resources.Up;
            this.tsbArrangePluginUpwards.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArrangePluginUpwards.Name = "tsbArrangePluginUpwards";
            this.tsbArrangePluginUpwards.Size = new System.Drawing.Size(23, 22);
            this.tsbArrangePluginUpwards.Text = "Change sort order to upper";
            this.tsbArrangePluginUpwards.Click += new System.EventHandler(this.tsbArrangePluginUpwards_Click);
            // 
            // btInstallPlugin
            // 
            this.btInstallPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btInstallPlugin.Image = ((System.Drawing.Image)(resources.GetObject("btInstallPlugin.Image")));
            this.btInstallPlugin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btInstallPlugin.Location = new System.Drawing.Point(3, 396);
            this.btInstallPlugin.Name = "btInstallPlugin";
            this.btInstallPlugin.Size = new System.Drawing.Size(297, 26);
            this.btInstallPlugin.TabIndex = 10;
            this.btInstallPlugin.Text = "Install a plug-in";
            this.btInstallPlugin.UseVisualStyleBackColor = true;
            this.btInstallPlugin.Click += new System.EventHandler(this.btInstallPlugin_Click);
            // 
            // gpPluginSettings
            // 
            this.tlpMain.SetColumnSpan(this.gpPluginSettings, 3);
            this.gpPluginSettings.Controls.Add(this.tlpPluginStats);
            this.gpPluginSettings.Controls.Add(this.tbPluginDescription);
            this.gpPluginSettings.Controls.Add(this.lbPluginDescription);
            this.gpPluginSettings.Controls.Add(this.tbPluginName);
            this.gpPluginSettings.Controls.Add(this.lbPluginName);
            this.gpPluginSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpPluginSettings.Location = new System.Drawing.Point(306, 3);
            this.gpPluginSettings.Name = "gpPluginSettings";
            this.tlpMain.SetRowSpan(this.gpPluginSettings, 5);
            this.gpPluginSettings.Size = new System.Drawing.Size(449, 384);
            this.gpPluginSettings.TabIndex = 1;
            this.gpPluginSettings.TabStop = false;
            this.gpPluginSettings.Text = "Plug-in settings";
            // 
            // tlpPluginStats
            // 
            this.tlpPluginStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpPluginStats.ColumnCount = 4;
            this.tlpPluginStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpPluginStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpPluginStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpPluginStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpPluginStats.Controls.Add(this.lbPluginVersion, 0, 0);
            this.tlpPluginStats.Controls.Add(this.cbPluginActive, 1, 4);
            this.tlpPluginStats.Controls.Add(this.tbUpdatedAt, 3, 2);
            this.tlpPluginStats.Controls.Add(this.tbPluginVersion, 1, 0);
            this.tlpPluginStats.Controls.Add(this.lbUpdatedAt, 2, 2);
            this.tlpPluginStats.Controls.Add(this.lbExceptionThrown, 2, 0);
            this.tlpPluginStats.Controls.Add(this.tbInstalledAt, 1, 2);
            this.tlpPluginStats.Controls.Add(this.tbExceptionsThrown, 3, 0);
            this.tlpPluginStats.Controls.Add(this.lbInstalledAt, 0, 2);
            this.tlpPluginStats.Controls.Add(this.lbLoadFailures, 0, 1);
            this.tlpPluginStats.Controls.Add(this.tbAppCrashes, 3, 1);
            this.tlpPluginStats.Controls.Add(this.tbLoadFailures, 1, 1);
            this.tlpPluginStats.Controls.Add(this.lbAppCrashes, 2, 1);
            this.tlpPluginStats.Controls.Add(this.lbPluginRating, 0, 3);
            this.tlpPluginStats.Controls.Add(this.nudRating, 3, 3);
            this.tlpPluginStats.Controls.Add(this.lbPluginActive, 0, 4);
            this.tlpPluginStats.Controls.Add(this.lbPluginExists, 2, 4);
            this.tlpPluginStats.Controls.Add(this.cbPluginExists, 3, 4);
            this.tlpPluginStats.Controls.Add(this.btDeletePlugin, 0, 5);
            this.tlpPluginStats.Location = new System.Drawing.Point(9, 103);
            this.tlpPluginStats.Name = "tlpPluginStats";
            this.tlpPluginStats.RowCount = 8;
            this.tlpPluginStats.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPluginStats.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPluginStats.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPluginStats.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPluginStats.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPluginStats.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPluginStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPluginStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPluginStats.Size = new System.Drawing.Size(434, 249);
            this.tlpPluginStats.TabIndex = 17;
            // 
            // lbPluginVersion
            // 
            this.lbPluginVersion.AutoEllipsis = true;
            this.lbPluginVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPluginVersion.Location = new System.Drawing.Point(3, 0);
            this.lbPluginVersion.Name = "lbPluginVersion";
            this.lbPluginVersion.Size = new System.Drawing.Size(80, 26);
            this.lbPluginVersion.TabIndex = 4;
            this.lbPluginVersion.Text = "Version:";
            this.lbPluginVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbPluginActive
            // 
            this.cbPluginActive.AutoSize = true;
            this.cbPluginActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPluginActive.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbPluginActive.Location = new System.Drawing.Point(191, 107);
            this.cbPluginActive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 4);
            this.cbPluginActive.Name = "cbPluginActive";
            this.cbPluginActive.Padding = new System.Windows.Forms.Padding(3);
            this.cbPluginActive.Size = new System.Drawing.Size(21, 20);
            this.cbPluginActive.TabIndex = 6;
            this.cbPluginActive.UseVisualStyleBackColor = true;
            this.cbPluginActive.CheckedChanged += new System.EventHandler(this.cbPluginActive_CheckedChanged);
            // 
            // tbUpdatedAt
            // 
            this.tbUpdatedAt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUpdatedAt.Location = new System.Drawing.Point(305, 55);
            this.tbUpdatedAt.Name = "tbUpdatedAt";
            this.tbUpdatedAt.ReadOnly = true;
            this.tbUpdatedAt.Size = new System.Drawing.Size(126, 20);
            this.tbUpdatedAt.TabIndex = 16;
            // 
            // tbPluginVersion
            // 
            this.tbPluginVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPluginVersion.Location = new System.Drawing.Point(89, 3);
            this.tbPluginVersion.Name = "tbPluginVersion";
            this.tbPluginVersion.ReadOnly = true;
            this.tbPluginVersion.Size = new System.Drawing.Size(124, 20);
            this.tbPluginVersion.TabIndex = 5;
            // 
            // lbUpdatedAt
            // 
            this.lbUpdatedAt.AutoEllipsis = true;
            this.lbUpdatedAt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbUpdatedAt.Location = new System.Drawing.Point(219, 52);
            this.lbUpdatedAt.Name = "lbUpdatedAt";
            this.lbUpdatedAt.Size = new System.Drawing.Size(80, 26);
            this.lbUpdatedAt.TabIndex = 15;
            this.lbUpdatedAt.Text = "Update date:";
            this.lbUpdatedAt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbExceptionThrown
            // 
            this.lbExceptionThrown.AutoEllipsis = true;
            this.lbExceptionThrown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbExceptionThrown.Location = new System.Drawing.Point(219, 0);
            this.lbExceptionThrown.Name = "lbExceptionThrown";
            this.lbExceptionThrown.Size = new System.Drawing.Size(80, 26);
            this.lbExceptionThrown.TabIndex = 7;
            this.lbExceptionThrown.Text = "Exceptions:";
            this.lbExceptionThrown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbInstalledAt
            // 
            this.tbInstalledAt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInstalledAt.Location = new System.Drawing.Point(89, 55);
            this.tbInstalledAt.Name = "tbInstalledAt";
            this.tbInstalledAt.ReadOnly = true;
            this.tbInstalledAt.Size = new System.Drawing.Size(124, 20);
            this.tbInstalledAt.TabIndex = 14;
            // 
            // tbExceptionsThrown
            // 
            this.tbExceptionsThrown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExceptionsThrown.Location = new System.Drawing.Point(305, 3);
            this.tbExceptionsThrown.Name = "tbExceptionsThrown";
            this.tbExceptionsThrown.ReadOnly = true;
            this.tbExceptionsThrown.Size = new System.Drawing.Size(126, 20);
            this.tbExceptionsThrown.TabIndex = 8;
            this.tbExceptionsThrown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbInstalledAt
            // 
            this.lbInstalledAt.AutoEllipsis = true;
            this.lbInstalledAt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInstalledAt.Location = new System.Drawing.Point(3, 52);
            this.lbInstalledAt.Name = "lbInstalledAt";
            this.lbInstalledAt.Size = new System.Drawing.Size(80, 26);
            this.lbInstalledAt.TabIndex = 13;
            this.lbInstalledAt.Text = "Install date:";
            this.lbInstalledAt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbLoadFailures
            // 
            this.lbLoadFailures.AutoEllipsis = true;
            this.lbLoadFailures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLoadFailures.Location = new System.Drawing.Point(3, 26);
            this.lbLoadFailures.Name = "lbLoadFailures";
            this.lbLoadFailures.Size = new System.Drawing.Size(80, 26);
            this.lbLoadFailures.TabIndex = 9;
            this.lbLoadFailures.Text = "Load failures:";
            this.lbLoadFailures.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbAppCrashes
            // 
            this.tbAppCrashes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAppCrashes.Location = new System.Drawing.Point(305, 29);
            this.tbAppCrashes.Name = "tbAppCrashes";
            this.tbAppCrashes.ReadOnly = true;
            this.tbAppCrashes.Size = new System.Drawing.Size(126, 20);
            this.tbAppCrashes.TabIndex = 12;
            this.tbAppCrashes.Text = "0";
            this.tbAppCrashes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbLoadFailures
            // 
            this.tbLoadFailures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLoadFailures.Location = new System.Drawing.Point(89, 29);
            this.tbLoadFailures.Name = "tbLoadFailures";
            this.tbLoadFailures.ReadOnly = true;
            this.tbLoadFailures.Size = new System.Drawing.Size(124, 20);
            this.tbLoadFailures.TabIndex = 10;
            this.tbLoadFailures.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbAppCrashes
            // 
            this.lbAppCrashes.AutoEllipsis = true;
            this.lbAppCrashes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAppCrashes.Location = new System.Drawing.Point(219, 26);
            this.lbAppCrashes.Name = "lbAppCrashes";
            this.lbAppCrashes.Size = new System.Drawing.Size(80, 26);
            this.lbAppCrashes.TabIndex = 11;
            this.lbAppCrashes.Text = "Application crashes:";
            this.lbAppCrashes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbPluginRating
            // 
            this.lbPluginRating.AutoEllipsis = true;
            this.tlpPluginStats.SetColumnSpan(this.lbPluginRating, 2);
            this.lbPluginRating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPluginRating.Location = new System.Drawing.Point(3, 78);
            this.lbPluginRating.Name = "lbPluginRating";
            this.lbPluginRating.Size = new System.Drawing.Size(210, 26);
            this.lbPluginRating.TabIndex = 17;
            this.lbPluginRating.Text = "Rating (0-100):";
            this.lbPluginRating.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudRating
            // 
            this.nudRating.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRating.Location = new System.Drawing.Point(305, 81);
            this.nudRating.Name = "nudRating";
            this.nudRating.Size = new System.Drawing.Size(126, 20);
            this.nudRating.TabIndex = 18;
            this.nudRating.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbPluginActive
            // 
            this.lbPluginActive.AutoEllipsis = true;
            this.lbPluginActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPluginActive.Location = new System.Drawing.Point(3, 104);
            this.lbPluginActive.Name = "lbPluginActive";
            this.lbPluginActive.Size = new System.Drawing.Size(80, 27);
            this.lbPluginActive.TabIndex = 19;
            this.lbPluginActive.Text = "Is active:";
            this.lbPluginActive.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbPluginExists
            // 
            this.lbPluginExists.AutoEllipsis = true;
            this.lbPluginExists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPluginExists.Location = new System.Drawing.Point(219, 104);
            this.lbPluginExists.Name = "lbPluginExists";
            this.lbPluginExists.Size = new System.Drawing.Size(80, 27);
            this.lbPluginExists.TabIndex = 20;
            this.lbPluginExists.Text = "Exists:";
            this.lbPluginExists.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbPluginExists
            // 
            this.cbPluginExists.AutoSize = true;
            this.cbPluginExists.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbPluginExists.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbPluginExists.Enabled = false;
            this.cbPluginExists.Location = new System.Drawing.Point(409, 107);
            this.cbPluginExists.Margin = new System.Windows.Forms.Padding(4, 3, 4, 4);
            this.cbPluginExists.Name = "cbPluginExists";
            this.cbPluginExists.Padding = new System.Windows.Forms.Padding(3);
            this.cbPluginExists.Size = new System.Drawing.Size(21, 20);
            this.cbPluginExists.TabIndex = 21;
            this.cbPluginExists.UseVisualStyleBackColor = true;
            // 
            // btDeletePlugin
            // 
            this.btDeletePlugin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpPluginStats.SetColumnSpan(this.btDeletePlugin, 4);
            this.btDeletePlugin.Image = ((System.Drawing.Image)(resources.GetObject("btDeletePlugin.Image")));
            this.btDeletePlugin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btDeletePlugin.Location = new System.Drawing.Point(3, 134);
            this.btDeletePlugin.Name = "btDeletePlugin";
            this.btDeletePlugin.Size = new System.Drawing.Size(428, 26);
            this.btDeletePlugin.TabIndex = 33;
            this.btDeletePlugin.Text = "Delete plug-in (a restart is required)";
            this.btDeletePlugin.UseVisualStyleBackColor = true;
            this.btDeletePlugin.Click += new System.EventHandler(this.btDeletePlugin_Click);
            // 
            // tbPluginDescription
            // 
            this.tbPluginDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPluginDescription.Location = new System.Drawing.Point(9, 77);
            this.tbPluginDescription.Name = "tbPluginDescription";
            this.tbPluginDescription.ReadOnly = true;
            this.tbPluginDescription.Size = new System.Drawing.Size(434, 20);
            this.tbPluginDescription.TabIndex = 3;
            // 
            // lbPluginDescription
            // 
            this.lbPluginDescription.AutoSize = true;
            this.lbPluginDescription.Location = new System.Drawing.Point(6, 61);
            this.lbPluginDescription.Name = "lbPluginDescription";
            this.lbPluginDescription.Size = new System.Drawing.Size(96, 13);
            this.lbPluginDescription.TabIndex = 2;
            this.lbPluginDescription.Text = "Plug-in description:";
            // 
            // tbPluginName
            // 
            this.tbPluginName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPluginName.Location = new System.Drawing.Point(9, 38);
            this.tbPluginName.Name = "tbPluginName";
            this.tbPluginName.ReadOnly = true;
            this.tbPluginName.Size = new System.Drawing.Size(434, 20);
            this.tbPluginName.TabIndex = 1;
            // 
            // lbPluginName
            // 
            this.lbPluginName.AutoSize = true;
            this.lbPluginName.Location = new System.Drawing.Point(6, 22);
            this.lbPluginName.Name = "lbPluginName";
            this.lbPluginName.Size = new System.Drawing.Size(102, 13);
            this.lbPluginName.TabIndex = 0;
            this.lbPluginName.Text = "Name of the plug-in:";
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(680, 396);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 26);
            this.btCancel.TabIndex = 8;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // odDLL
            // 
            this.odDLL.DefaultExt = "*.dll";
            this.odDLL.Filter = "DLL Files|*.dll";
            // 
            // FormPluginManage
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(782, 449);
            this.Controls.Add(this.tlpMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPluginManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage plug-ins";
            this.Shown += new System.EventHandler(this.FormPluginManage_Shown);
            this.SizeChanged += new System.EventHandler(this.FormPluginManage_SizeChanged);
            this.tlpMain.ResumeLayout(false);
            this.tlpPluginList.ResumeLayout(false);
            this.tlpPluginList.PerformLayout();
            this.pnArrangePlugins.ResumeLayout(false);
            this.pnArrangePlugins.PerformLayout();
            this.ttArrangePlugins.ResumeLayout(false);
            this.ttArrangePlugins.PerformLayout();
            this.gpPluginSettings.ResumeLayout(false);
            this.gpPluginSettings.PerformLayout();
            this.tlpPluginStats.ResumeLayout(false);
            this.tlpPluginStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRating)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpPluginList;
        private System.Windows.Forms.Label lbFilterPlugins;
        private System.Windows.Forms.TextBox tbFilterPlugins;
        private System.Windows.Forms.ListBox lbPluginList;
        private System.Windows.Forms.GroupBox gpPluginSettings;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TextBox tbExceptionsThrown;
        private System.Windows.Forms.Label lbExceptionThrown;
        private System.Windows.Forms.CheckBox cbPluginActive;
        private System.Windows.Forms.TextBox tbPluginVersion;
        private System.Windows.Forms.Label lbPluginVersion;
        private System.Windows.Forms.TextBox tbPluginDescription;
        private System.Windows.Forms.Label lbPluginDescription;
        private System.Windows.Forms.TextBox tbPluginName;
        private System.Windows.Forms.Label lbPluginName;
        private System.Windows.Forms.TextBox tbAppCrashes;
        private System.Windows.Forms.Label lbAppCrashes;
        private System.Windows.Forms.TextBox tbLoadFailures;
        private System.Windows.Forms.Label lbLoadFailures;
        private System.Windows.Forms.TableLayoutPanel tlpPluginStats;
        private System.Windows.Forms.TextBox tbUpdatedAt;
        private System.Windows.Forms.Label lbUpdatedAt;
        private System.Windows.Forms.TextBox tbInstalledAt;
        private System.Windows.Forms.Label lbInstalledAt;
        private System.Windows.Forms.Label lbPluginRating;
        private System.Windows.Forms.NumericUpDown nudRating;
        private System.Windows.Forms.Label lbPluginActive;
        private System.Windows.Forms.Label lbPluginExists;
        private System.Windows.Forms.CheckBox cbPluginExists;
        private System.Windows.Forms.ToolStrip ttArrangePlugins;
        private System.Windows.Forms.ToolStripButton tsbArrangePluginDownwards;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbArrangePlugins;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbArrangePluginUpwards;
        private System.Windows.Forms.Panel pnArrangePlugins;
        private System.Windows.Forms.Button btInstallPlugin;
        private System.Windows.Forms.Button btDeletePlugin;
        private System.Windows.Forms.OpenFileDialog odDLL;
    }
}