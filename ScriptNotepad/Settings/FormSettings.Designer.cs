namespace ScriptNotepad.Settings
{
    partial class FormSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpgGeneralSettings = new System.Windows.Forms.TabPage();
            this.nudMaximumSearchFileSize = new System.Windows.Forms.NumericUpDown();
            this.lbMaximumSearchFileSize = new System.Windows.Forms.Label();
            this.cbDockSearchTree = new System.Windows.Forms.CheckBox();
            this.pbDefaultFolder = new System.Windows.Forms.PictureBox();
            this.btSelectPluginFolder = new System.Windows.Forms.Button();
            this.tbPluginFolder = new System.Windows.Forms.TextBox();
            this.lbPluginFolder = new System.Windows.Forms.Label();
            this.lbSelectLanguageDescription = new System.Windows.Forms.Label();
            this.cmbSelectLanguageValue = new System.Windows.Forms.ComboBox();
            this.cbDocumentContentHistory = new System.Windows.Forms.CheckBox();
            this.nudDocumentContentHistory = new System.Windows.Forms.NumericUpDown();
            this.lbDocumentContentHistory = new System.Windows.Forms.Label();
            this.nudHistoryDocuments = new System.Windows.Forms.NumericUpDown();
            this.lbHistoryDocuments = new System.Windows.Forms.Label();
            this.gpDefaultEncoding = new System.Windows.Forms.GroupBox();
            this.btUTF8 = new System.Windows.Forms.PictureBox();
            this.btSystemDefaultEncoding = new System.Windows.Forms.PictureBox();
            this.cmbCharacterSet = new System.Windows.Forms.ComboBox();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.lbCharacterSet = new System.Windows.Forms.Label();
            this.lbEncoding = new System.Windows.Forms.Label();
            this.tpgColorSettings = new System.Windows.Forms.TabPage();
            this.btDefaults = new System.Windows.Forms.Button();
            this.btCurrentLineBackgroundColor = new System.Windows.Forms.Button();
            this.lbCurrentLineBackgroundColor = new System.Windows.Forms.Label();
            this.btMarkStyle5Color = new System.Windows.Forms.Button();
            this.lbMarkStyle5Color = new System.Windows.Forms.Label();
            this.btMarkStyle4Color = new System.Windows.Forms.Button();
            this.lbMarkStyle4Color = new System.Windows.Forms.Label();
            this.btMarkStyle3Color = new System.Windows.Forms.Button();
            this.lbMarkStyle3Color = new System.Windows.Forms.Label();
            this.btMarkStyle2Color = new System.Windows.Forms.Button();
            this.lbMarkStyle2Color = new System.Windows.Forms.Label();
            this.btMarkStyle1Color = new System.Windows.Forms.Button();
            this.lbMarkStyle1Color = new System.Windows.Forms.Label();
            this.btSmartHighlightColor = new System.Windows.Forms.Button();
            this.lbSmartHighlightColor = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.cdColors = new System.Windows.Forms.ColorDialog();
            this.lbHistoryAmount = new System.Windows.Forms.Label();
            this.nudHistoryAmount = new System.Windows.Forms.NumericUpDown();
            this.tcMain.SuspendLayout();
            this.tpgGeneralSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaximumSearchFileSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefaultFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDocumentContentHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryDocuments)).BeginInit();
            this.gpDefaultEncoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btUTF8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btSystemDefaultEncoding)).BeginInit();
            this.tpgColorSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpgGeneralSettings);
            this.tcMain.Controls.Add(this.tpgColorSettings);
            this.tcMain.Location = new System.Drawing.Point(12, 12);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(565, 397);
            this.tcMain.TabIndex = 0;
            // 
            // tpgGeneralSettings
            // 
            this.tpgGeneralSettings.Controls.Add(this.nudHistoryAmount);
            this.tpgGeneralSettings.Controls.Add(this.lbHistoryAmount);
            this.tpgGeneralSettings.Controls.Add(this.nudMaximumSearchFileSize);
            this.tpgGeneralSettings.Controls.Add(this.lbMaximumSearchFileSize);
            this.tpgGeneralSettings.Controls.Add(this.cbDockSearchTree);
            this.tpgGeneralSettings.Controls.Add(this.pbDefaultFolder);
            this.tpgGeneralSettings.Controls.Add(this.btSelectPluginFolder);
            this.tpgGeneralSettings.Controls.Add(this.tbPluginFolder);
            this.tpgGeneralSettings.Controls.Add(this.lbPluginFolder);
            this.tpgGeneralSettings.Controls.Add(this.lbSelectLanguageDescription);
            this.tpgGeneralSettings.Controls.Add(this.cmbSelectLanguageValue);
            this.tpgGeneralSettings.Controls.Add(this.cbDocumentContentHistory);
            this.tpgGeneralSettings.Controls.Add(this.nudDocumentContentHistory);
            this.tpgGeneralSettings.Controls.Add(this.lbDocumentContentHistory);
            this.tpgGeneralSettings.Controls.Add(this.nudHistoryDocuments);
            this.tpgGeneralSettings.Controls.Add(this.lbHistoryDocuments);
            this.tpgGeneralSettings.Controls.Add(this.gpDefaultEncoding);
            this.tpgGeneralSettings.Location = new System.Drawing.Point(4, 22);
            this.tpgGeneralSettings.Name = "tpgGeneralSettings";
            this.tpgGeneralSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpgGeneralSettings.Size = new System.Drawing.Size(557, 371);
            this.tpgGeneralSettings.TabIndex = 0;
            this.tpgGeneralSettings.Text = "General";
            this.tpgGeneralSettings.UseVisualStyleBackColor = true;
            // 
            // nudMaximumSearchFileSize
            // 
            this.nudMaximumSearchFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMaximumSearchFileSize.Location = new System.Drawing.Point(461, 247);
            this.nudMaximumSearchFileSize.Maximum = new decimal(new int[] {
            1999,
            0,
            0,
            0});
            this.nudMaximumSearchFileSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudMaximumSearchFileSize.Name = "nudMaximumSearchFileSize";
            this.nudMaximumSearchFileSize.Size = new System.Drawing.Size(90, 20);
            this.nudMaximumSearchFileSize.TabIndex = 35;
            this.nudMaximumSearchFileSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lbMaximumSearchFileSize
            // 
            this.lbMaximumSearchFileSize.AutoSize = true;
            this.lbMaximumSearchFileSize.Location = new System.Drawing.Point(3, 249);
            this.lbMaximumSearchFileSize.Name = "lbMaximumSearchFileSize";
            this.lbMaximumSearchFileSize.Size = new System.Drawing.Size(194, 13);
            this.lbMaximumSearchFileSize.TabIndex = 33;
            this.lbMaximumSearchFileSize.Text = "Maximum file size for searhing text (MB):";
            // 
            // cbDockSearchTree
            // 
            this.cbDockSearchTree.AutoSize = true;
            this.cbDockSearchTree.Location = new System.Drawing.Point(6, 223);
            this.cbDockSearchTree.Name = "cbDockSearchTree";
            this.cbDockSearchTree.Size = new System.Drawing.Size(126, 17);
            this.cbDockSearchTree.TabIndex = 32;
            this.cbDockSearchTree.Text = "Dock the search tree";
            this.cbDockSearchTree.UseVisualStyleBackColor = true;
            // 
            // pbDefaultFolder
            // 
            this.pbDefaultFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDefaultFolder.Image = global::ScriptNotepad.Properties.Resources.default_image;
            this.pbDefaultFolder.Location = new System.Drawing.Point(494, 195);
            this.pbDefaultFolder.Name = "pbDefaultFolder";
            this.pbDefaultFolder.Size = new System.Drawing.Size(21, 21);
            this.pbDefaultFolder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbDefaultFolder.TabIndex = 31;
            this.pbDefaultFolder.TabStop = false;
            this.ttMain.SetToolTip(this.pbDefaultFolder, "Set to default");
            this.pbDefaultFolder.Click += new System.EventHandler(this.pbDefaultFolder_Click);
            // 
            // btSelectPluginFolder
            // 
            this.btSelectPluginFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelectPluginFolder.Location = new System.Drawing.Point(520, 195);
            this.btSelectPluginFolder.Name = "btSelectPluginFolder";
            this.btSelectPluginFolder.Size = new System.Drawing.Size(31, 20);
            this.btSelectPluginFolder.TabIndex = 30;
            this.btSelectPluginFolder.Text = "...";
            this.btSelectPluginFolder.UseVisualStyleBackColor = true;
            this.btSelectPluginFolder.Click += new System.EventHandler(this.btSelectPluginFolder_Click);
            // 
            // tbPluginFolder
            // 
            this.tbPluginFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPluginFolder.Location = new System.Drawing.Point(225, 195);
            this.tbPluginFolder.Name = "tbPluginFolder";
            this.tbPluginFolder.Size = new System.Drawing.Size(263, 20);
            this.tbPluginFolder.TabIndex = 29;
            this.tbPluginFolder.TextChanged += new System.EventHandler(this.tbPluginFolder_TextChanged);
            // 
            // lbPluginFolder
            // 
            this.lbPluginFolder.AutoSize = true;
            this.lbPluginFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbPluginFolder.Location = new System.Drawing.Point(3, 198);
            this.lbPluginFolder.Name = "lbPluginFolder";
            this.lbPluginFolder.Size = new System.Drawing.Size(164, 13);
            this.lbPluginFolder.TabIndex = 28;
            this.lbPluginFolder.Text = "Plug-in path (a restart is required):";
            this.ttMain.SetToolTip(this.lbPluginFolder, "Open the plug-in folder");
            this.lbPluginFolder.Click += new System.EventHandler(this.lbPluginFolder_Click);
            // 
            // lbSelectLanguageDescription
            // 
            this.lbSelectLanguageDescription.AutoSize = true;
            this.lbSelectLanguageDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbSelectLanguageDescription.Location = new System.Drawing.Point(3, 162);
            this.lbSelectLanguageDescription.Margin = new System.Windows.Forms.Padding(17, 15, 17, 15);
            this.lbSelectLanguageDescription.Name = "lbSelectLanguageDescription";
            this.lbSelectLanguageDescription.Size = new System.Drawing.Size(156, 13);
            this.lbSelectLanguageDescription.TabIndex = 26;
            this.lbSelectLanguageDescription.Text = "Language (a restart is required):";
            this.lbSelectLanguageDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbSelectLanguageValue
            // 
            this.cmbSelectLanguageValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSelectLanguageValue.DisplayMember = "DisplayName";
            this.cmbSelectLanguageValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectLanguageValue.FormattingEnabled = true;
            this.cmbSelectLanguageValue.Location = new System.Drawing.Point(225, 159);
            this.cmbSelectLanguageValue.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.cmbSelectLanguageValue.Name = "cmbSelectLanguageValue";
            this.cmbSelectLanguageValue.Size = new System.Drawing.Size(326, 21);
            this.cmbSelectLanguageValue.TabIndex = 27;
            // 
            // cbDocumentContentHistory
            // 
            this.cbDocumentContentHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDocumentContentHistory.AutoSize = true;
            this.cbDocumentContentHistory.Checked = true;
            this.cbDocumentContentHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDocumentContentHistory.Location = new System.Drawing.Point(440, 126);
            this.cbDocumentContentHistory.Name = "cbDocumentContentHistory";
            this.cbDocumentContentHistory.Size = new System.Drawing.Size(15, 14);
            this.cbDocumentContentHistory.TabIndex = 13;
            this.cbDocumentContentHistory.UseVisualStyleBackColor = true;
            this.cbDocumentContentHistory.CheckedChanged += new System.EventHandler(this.cbDocumentContentHistory_CheckedChanged);
            // 
            // nudDocumentContentHistory
            // 
            this.nudDocumentContentHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDocumentContentHistory.Location = new System.Drawing.Point(461, 124);
            this.nudDocumentContentHistory.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudDocumentContentHistory.Name = "nudDocumentContentHistory";
            this.nudDocumentContentHistory.Size = new System.Drawing.Size(90, 20);
            this.nudDocumentContentHistory.TabIndex = 12;
            this.nudDocumentContentHistory.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lbDocumentContentHistory
            // 
            this.lbDocumentContentHistory.AutoSize = true;
            this.lbDocumentContentHistory.Location = new System.Drawing.Point(3, 126);
            this.lbDocumentContentHistory.Name = "lbDocumentContentHistory";
            this.lbDocumentContentHistory.Size = new System.Drawing.Size(289, 13);
            this.lbDocumentContentHistory.TabIndex = 11;
            this.lbDocumentContentHistory.Text = "How many closed document contents to keep in the history:";
            // 
            // nudHistoryDocuments
            // 
            this.nudHistoryDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudHistoryDocuments.Location = new System.Drawing.Point(461, 98);
            this.nudHistoryDocuments.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudHistoryDocuments.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudHistoryDocuments.Name = "nudHistoryDocuments";
            this.nudHistoryDocuments.Size = new System.Drawing.Size(90, 20);
            this.nudHistoryDocuments.TabIndex = 10;
            this.nudHistoryDocuments.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lbHistoryDocuments
            // 
            this.lbHistoryDocuments.AutoSize = true;
            this.lbHistoryDocuments.Location = new System.Drawing.Point(3, 100);
            this.lbHistoryDocuments.Name = "lbHistoryDocuments";
            this.lbHistoryDocuments.Size = new System.Drawing.Size(232, 13);
            this.lbHistoryDocuments.TabIndex = 9;
            this.lbHistoryDocuments.Text = "How many documents to keep in the file history:";
            // 
            // gpDefaultEncoding
            // 
            this.gpDefaultEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpDefaultEncoding.Controls.Add(this.btUTF8);
            this.gpDefaultEncoding.Controls.Add(this.btSystemDefaultEncoding);
            this.gpDefaultEncoding.Controls.Add(this.cmbCharacterSet);
            this.gpDefaultEncoding.Controls.Add(this.cmbEncoding);
            this.gpDefaultEncoding.Controls.Add(this.lbCharacterSet);
            this.gpDefaultEncoding.Controls.Add(this.lbEncoding);
            this.gpDefaultEncoding.Location = new System.Drawing.Point(6, 6);
            this.gpDefaultEncoding.Name = "gpDefaultEncoding";
            this.gpDefaultEncoding.Size = new System.Drawing.Size(545, 86);
            this.gpDefaultEncoding.TabIndex = 8;
            this.gpDefaultEncoding.TabStop = false;
            this.gpDefaultEncoding.Text = "Default encoding";
            // 
            // btUTF8
            // 
            this.btUTF8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUTF8.Image = global::ScriptNotepad.Properties.Resources.unicode;
            this.btUTF8.Location = new System.Drawing.Point(513, 49);
            this.btUTF8.Name = "btUTF8";
            this.btUTF8.Size = new System.Drawing.Size(21, 21);
            this.btUTF8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btUTF8.TabIndex = 10;
            this.btUTF8.TabStop = false;
            this.ttMain.SetToolTip(this.btUTF8, "Set to unicode (UTF8)");
            this.btUTF8.Click += new System.EventHandler(this.btDefaultEncodings_Click);
            // 
            // btSystemDefaultEncoding
            // 
            this.btSystemDefaultEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSystemDefaultEncoding.Image = global::ScriptNotepad.Properties.Resources.default_image;
            this.btSystemDefaultEncoding.Location = new System.Drawing.Point(513, 22);
            this.btSystemDefaultEncoding.Name = "btSystemDefaultEncoding";
            this.btSystemDefaultEncoding.Size = new System.Drawing.Size(21, 21);
            this.btSystemDefaultEncoding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btSystemDefaultEncoding.TabIndex = 9;
            this.btSystemDefaultEncoding.TabStop = false;
            this.ttMain.SetToolTip(this.btSystemDefaultEncoding, "Set to system default");
            this.btSystemDefaultEncoding.Click += new System.EventHandler(this.btDefaultEncodings_Click);
            // 
            // cmbCharacterSet
            // 
            this.cmbCharacterSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCharacterSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCharacterSet.FormattingEnabled = true;
            this.cmbCharacterSet.Location = new System.Drawing.Point(147, 22);
            this.cmbCharacterSet.Name = "cmbCharacterSet";
            this.cmbCharacterSet.Size = new System.Drawing.Size(360, 21);
            this.cmbCharacterSet.TabIndex = 5;
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Location = new System.Drawing.Point(147, 49);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(360, 21);
            this.cmbEncoding.TabIndex = 7;
            // 
            // lbCharacterSet
            // 
            this.lbCharacterSet.AutoSize = true;
            this.lbCharacterSet.Location = new System.Drawing.Point(6, 25);
            this.lbCharacterSet.Name = "lbCharacterSet";
            this.lbCharacterSet.Size = new System.Drawing.Size(73, 13);
            this.lbCharacterSet.TabIndex = 4;
            this.lbCharacterSet.Text = "Character set:";
            // 
            // lbEncoding
            // 
            this.lbEncoding.AutoSize = true;
            this.lbEncoding.Location = new System.Drawing.Point(6, 52);
            this.lbEncoding.Name = "lbEncoding";
            this.lbEncoding.Size = new System.Drawing.Size(55, 13);
            this.lbEncoding.TabIndex = 6;
            this.lbEncoding.Text = "Encoding:";
            // 
            // tpgColorSettings
            // 
            this.tpgColorSettings.Controls.Add(this.btDefaults);
            this.tpgColorSettings.Controls.Add(this.btCurrentLineBackgroundColor);
            this.tpgColorSettings.Controls.Add(this.lbCurrentLineBackgroundColor);
            this.tpgColorSettings.Controls.Add(this.btMarkStyle5Color);
            this.tpgColorSettings.Controls.Add(this.lbMarkStyle5Color);
            this.tpgColorSettings.Controls.Add(this.btMarkStyle4Color);
            this.tpgColorSettings.Controls.Add(this.lbMarkStyle4Color);
            this.tpgColorSettings.Controls.Add(this.btMarkStyle3Color);
            this.tpgColorSettings.Controls.Add(this.lbMarkStyle3Color);
            this.tpgColorSettings.Controls.Add(this.btMarkStyle2Color);
            this.tpgColorSettings.Controls.Add(this.lbMarkStyle2Color);
            this.tpgColorSettings.Controls.Add(this.btMarkStyle1Color);
            this.tpgColorSettings.Controls.Add(this.lbMarkStyle1Color);
            this.tpgColorSettings.Controls.Add(this.btSmartHighlightColor);
            this.tpgColorSettings.Controls.Add(this.lbSmartHighlightColor);
            this.tpgColorSettings.Location = new System.Drawing.Point(4, 22);
            this.tpgColorSettings.Name = "tpgColorSettings";
            this.tpgColorSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpgColorSettings.Size = new System.Drawing.Size(557, 371);
            this.tpgColorSettings.TabIndex = 1;
            this.tpgColorSettings.Text = "Colors";
            this.tpgColorSettings.UseVisualStyleBackColor = true;
            // 
            // btDefaults
            // 
            this.btDefaults.Location = new System.Drawing.Point(9, 342);
            this.btDefaults.Name = "btDefaults";
            this.btDefaults.Size = new System.Drawing.Size(146, 23);
            this.btDefaults.TabIndex = 14;
            this.btDefaults.Text = "Defaults";
            this.btDefaults.UseVisualStyleBackColor = true;
            this.btDefaults.Click += new System.EventHandler(this.BtDefaults_Click);
            // 
            // btCurrentLineBackgroundColor
            // 
            this.btCurrentLineBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCurrentLineBackgroundColor.Location = new System.Drawing.Point(363, 180);
            this.btCurrentLineBackgroundColor.Name = "btCurrentLineBackgroundColor";
            this.btCurrentLineBackgroundColor.Size = new System.Drawing.Size(188, 23);
            this.btCurrentLineBackgroundColor.TabIndex = 13;
            this.btCurrentLineBackgroundColor.UseVisualStyleBackColor = true;
            // 
            // lbCurrentLineBackgroundColor
            // 
            this.lbCurrentLineBackgroundColor.AutoSize = true;
            this.lbCurrentLineBackgroundColor.Location = new System.Drawing.Point(6, 185);
            this.lbCurrentLineBackgroundColor.Name = "lbCurrentLineBackgroundColor";
            this.lbCurrentLineBackgroundColor.Size = new System.Drawing.Size(149, 13);
            this.lbCurrentLineBackgroundColor.TabIndex = 12;
            this.lbCurrentLineBackgroundColor.Text = "Current line background color:";
            // 
            // btMarkStyle5Color
            // 
            this.btMarkStyle5Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btMarkStyle5Color.Location = new System.Drawing.Point(363, 151);
            this.btMarkStyle5Color.Name = "btMarkStyle5Color";
            this.btMarkStyle5Color.Size = new System.Drawing.Size(188, 23);
            this.btMarkStyle5Color.TabIndex = 11;
            this.btMarkStyle5Color.UseVisualStyleBackColor = true;
            this.btMarkStyle5Color.Click += new System.EventHandler(this.BtSmartHighlightColor_Click);
            // 
            // lbMarkStyle5Color
            // 
            this.lbMarkStyle5Color.AutoSize = true;
            this.lbMarkStyle5Color.Location = new System.Drawing.Point(6, 156);
            this.lbMarkStyle5Color.Name = "lbMarkStyle5Color";
            this.lbMarkStyle5Color.Size = new System.Drawing.Size(93, 13);
            this.lbMarkStyle5Color.TabIndex = 10;
            this.lbMarkStyle5Color.Text = "Mark style 5 color:";
            // 
            // btMarkStyle4Color
            // 
            this.btMarkStyle4Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btMarkStyle4Color.Location = new System.Drawing.Point(363, 122);
            this.btMarkStyle4Color.Name = "btMarkStyle4Color";
            this.btMarkStyle4Color.Size = new System.Drawing.Size(188, 23);
            this.btMarkStyle4Color.TabIndex = 9;
            this.btMarkStyle4Color.UseVisualStyleBackColor = true;
            this.btMarkStyle4Color.Click += new System.EventHandler(this.BtSmartHighlightColor_Click);
            // 
            // lbMarkStyle4Color
            // 
            this.lbMarkStyle4Color.AutoSize = true;
            this.lbMarkStyle4Color.Location = new System.Drawing.Point(6, 127);
            this.lbMarkStyle4Color.Name = "lbMarkStyle4Color";
            this.lbMarkStyle4Color.Size = new System.Drawing.Size(93, 13);
            this.lbMarkStyle4Color.TabIndex = 8;
            this.lbMarkStyle4Color.Text = "Mark style 4 color:";
            // 
            // btMarkStyle3Color
            // 
            this.btMarkStyle3Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btMarkStyle3Color.Location = new System.Drawing.Point(363, 93);
            this.btMarkStyle3Color.Name = "btMarkStyle3Color";
            this.btMarkStyle3Color.Size = new System.Drawing.Size(188, 23);
            this.btMarkStyle3Color.TabIndex = 7;
            this.btMarkStyle3Color.UseVisualStyleBackColor = true;
            this.btMarkStyle3Color.Click += new System.EventHandler(this.BtSmartHighlightColor_Click);
            // 
            // lbMarkStyle3Color
            // 
            this.lbMarkStyle3Color.AutoSize = true;
            this.lbMarkStyle3Color.Location = new System.Drawing.Point(6, 98);
            this.lbMarkStyle3Color.Name = "lbMarkStyle3Color";
            this.lbMarkStyle3Color.Size = new System.Drawing.Size(93, 13);
            this.lbMarkStyle3Color.TabIndex = 6;
            this.lbMarkStyle3Color.Text = "Mark style 3 color:";
            // 
            // btMarkStyle2Color
            // 
            this.btMarkStyle2Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btMarkStyle2Color.Location = new System.Drawing.Point(363, 64);
            this.btMarkStyle2Color.Name = "btMarkStyle2Color";
            this.btMarkStyle2Color.Size = new System.Drawing.Size(188, 23);
            this.btMarkStyle2Color.TabIndex = 5;
            this.btMarkStyle2Color.UseVisualStyleBackColor = true;
            this.btMarkStyle2Color.Click += new System.EventHandler(this.BtSmartHighlightColor_Click);
            // 
            // lbMarkStyle2Color
            // 
            this.lbMarkStyle2Color.AutoSize = true;
            this.lbMarkStyle2Color.Location = new System.Drawing.Point(6, 69);
            this.lbMarkStyle2Color.Name = "lbMarkStyle2Color";
            this.lbMarkStyle2Color.Size = new System.Drawing.Size(93, 13);
            this.lbMarkStyle2Color.TabIndex = 4;
            this.lbMarkStyle2Color.Text = "Mark style 2 color:";
            // 
            // btMarkStyle1Color
            // 
            this.btMarkStyle1Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btMarkStyle1Color.Location = new System.Drawing.Point(363, 35);
            this.btMarkStyle1Color.Name = "btMarkStyle1Color";
            this.btMarkStyle1Color.Size = new System.Drawing.Size(188, 23);
            this.btMarkStyle1Color.TabIndex = 3;
            this.btMarkStyle1Color.UseVisualStyleBackColor = true;
            this.btMarkStyle1Color.Click += new System.EventHandler(this.BtSmartHighlightColor_Click);
            // 
            // lbMarkStyle1Color
            // 
            this.lbMarkStyle1Color.AutoSize = true;
            this.lbMarkStyle1Color.Location = new System.Drawing.Point(6, 40);
            this.lbMarkStyle1Color.Name = "lbMarkStyle1Color";
            this.lbMarkStyle1Color.Size = new System.Drawing.Size(93, 13);
            this.lbMarkStyle1Color.TabIndex = 2;
            this.lbMarkStyle1Color.Text = "Mark style 1 color:";
            // 
            // btSmartHighlightColor
            // 
            this.btSmartHighlightColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSmartHighlightColor.Location = new System.Drawing.Point(363, 6);
            this.btSmartHighlightColor.Name = "btSmartHighlightColor";
            this.btSmartHighlightColor.Size = new System.Drawing.Size(188, 23);
            this.btSmartHighlightColor.TabIndex = 1;
            this.btSmartHighlightColor.UseVisualStyleBackColor = true;
            this.btSmartHighlightColor.Click += new System.EventHandler(this.BtSmartHighlightColor_Click);
            // 
            // lbSmartHighlightColor
            // 
            this.lbSmartHighlightColor.AutoSize = true;
            this.lbSmartHighlightColor.Location = new System.Drawing.Point(6, 11);
            this.lbSmartHighlightColor.Name = "lbSmartHighlightColor";
            this.lbSmartHighlightColor.Size = new System.Drawing.Size(105, 13);
            this.lbSmartHighlightColor.TabIndex = 0;
            this.lbSmartHighlightColor.Text = "Smart highlight color:";
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(502, 415);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(12, 415);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // cdColors
            // 
            this.cdColors.AnyColor = true;
            this.cdColors.FullOpen = true;
            // 
            // lbHistoryAmount
            // 
            this.lbHistoryAmount.AutoSize = true;
            this.lbHistoryAmount.Location = new System.Drawing.Point(3, 275);
            this.lbHistoryAmount.Name = "lbHistoryAmount";
            this.lbHistoryAmount.Size = new System.Drawing.Size(211, 13);
            this.lbHistoryAmount.TabIndex = 36;
            this.lbHistoryAmount.Text = "Maximum amount of search history to keep:";
            // 
            // nudHistoryAmount
            // 
            this.nudHistoryAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudHistoryAmount.Location = new System.Drawing.Point(461, 273);
            this.nudHistoryAmount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudHistoryAmount.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudHistoryAmount.Name = "nudHistoryAmount";
            this.nudHistoryAmount.Size = new System.Drawing.Size(90, 20);
            this.nudHistoryAmount.TabIndex = 37;
            this.nudHistoryAmount.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // FormSettings
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(589, 450);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.tcMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Shown += new System.EventHandler(this.FormSettings_Shown);
            this.tcMain.ResumeLayout(false);
            this.tpgGeneralSettings.ResumeLayout(false);
            this.tpgGeneralSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaximumSearchFileSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefaultFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDocumentContentHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryDocuments)).EndInit();
            this.gpDefaultEncoding.ResumeLayout(false);
            this.gpDefaultEncoding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btUTF8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btSystemDefaultEncoding)).EndInit();
            this.tpgColorSettings.ResumeLayout(false);
            this.tpgColorSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryAmount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpgGeneralSettings;
        private System.Windows.Forms.GroupBox gpDefaultEncoding;
        private System.Windows.Forms.ComboBox cmbCharacterSet;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.Label lbCharacterSet;
        private System.Windows.Forms.Label lbEncoding;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.PictureBox btUTF8;
        private System.Windows.Forms.PictureBox btSystemDefaultEncoding;
        private System.Windows.Forms.NumericUpDown nudHistoryDocuments;
        private System.Windows.Forms.Label lbHistoryDocuments;
        private System.Windows.Forms.CheckBox cbDocumentContentHistory;
        private System.Windows.Forms.NumericUpDown nudDocumentContentHistory;
        private System.Windows.Forms.Label lbDocumentContentHistory;
        private System.Windows.Forms.Label lbSelectLanguageDescription;
        private System.Windows.Forms.ComboBox cmbSelectLanguageValue;
        private System.Windows.Forms.Button btSelectPluginFolder;
        private System.Windows.Forms.TextBox tbPluginFolder;
        private System.Windows.Forms.Label lbPluginFolder;
        private System.Windows.Forms.PictureBox pbDefaultFolder;
        private System.Windows.Forms.CheckBox cbDockSearchTree;
        private System.Windows.Forms.TabPage tpgColorSettings;
        private System.Windows.Forms.Button btSmartHighlightColor;
        private System.Windows.Forms.Label lbSmartHighlightColor;
        private System.Windows.Forms.ColorDialog cdColors;
        private System.Windows.Forms.Button btMarkStyle5Color;
        private System.Windows.Forms.Label lbMarkStyle5Color;
        private System.Windows.Forms.Button btMarkStyle4Color;
        private System.Windows.Forms.Label lbMarkStyle4Color;
        private System.Windows.Forms.Button btMarkStyle3Color;
        private System.Windows.Forms.Label lbMarkStyle3Color;
        private System.Windows.Forms.Button btMarkStyle2Color;
        private System.Windows.Forms.Label lbMarkStyle2Color;
        private System.Windows.Forms.Button btMarkStyle1Color;
        private System.Windows.Forms.Label lbMarkStyle1Color;
        private System.Windows.Forms.Button btCurrentLineBackgroundColor;
        private System.Windows.Forms.Label lbCurrentLineBackgroundColor;
        private System.Windows.Forms.Button btDefaults;
        private System.Windows.Forms.NumericUpDown nudMaximumSearchFileSize;
        private System.Windows.Forms.Label lbMaximumSearchFileSize;
        private System.Windows.Forms.NumericUpDown nudHistoryAmount;
        private System.Windows.Forms.Label lbHistoryAmount;
    }
}