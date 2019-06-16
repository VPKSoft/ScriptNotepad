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
            this.cbSetThreadLocale = new System.Windows.Forms.CheckBox();
            this.cbUseRTL = new System.Windows.Forms.CheckBox();
            this.cbCategorizeProgrammingLanguages = new System.Windows.Forms.CheckBox();
            this.nudHistoryAmount = new System.Windows.Forms.NumericUpDown();
            this.lbHistoryAmount = new System.Windows.Forms.Label();
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
            this.tpgAdditionalSettings = new System.Windows.Forms.TabPage();
            this.tbNoteAutoSave = new System.Windows.Forms.TextBox();
            this.nudAutoSaveInterval = new System.Windows.Forms.NumericUpDown();
            this.cbUseAutoSave = new System.Windows.Forms.CheckBox();
            this.tabEncoding = new System.Windows.Forms.TabPage();
            this.cbDetectNoBomUnicode = new System.Windows.Forms.CheckBox();
            this.cbEncodingAutoDetect = new System.Windows.Forms.CheckBox();
            this.gpDefaultEncoding = new System.Windows.Forms.GroupBox();
            this.btUTF8 = new System.Windows.Forms.PictureBox();
            this.btSystemDefaultEncoding = new System.Windows.Forms.PictureBox();
            this.cmbCharacterSet = new System.Windows.Forms.ComboBox();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.lbCharacterSet = new System.Windows.Forms.Label();
            this.lbEncoding = new System.Windows.Forms.Label();
            this.tabEditorSettings = new System.Windows.Forms.TabPage();
            this.gbZoomSetting = new System.Windows.Forms.GroupBox();
            this.cbSaveDocumentZoom = new System.Windows.Forms.CheckBox();
            this.cbIndividualZoom = new System.Windows.Forms.CheckBox();
            this.lbTabWidth = new System.Windows.Forms.Label();
            this.nudTabWidth = new System.Windows.Forms.NumericUpDown();
            this.cbUseCodeIndentation = new System.Windows.Forms.CheckBox();
            this.cmbSimulateKeyboard = new System.Windows.Forms.ComboBox();
            this.cbSimulateKeyboard = new System.Windows.Forms.CheckBox();
            this.cbIndentGuideOn = new System.Windows.Forms.CheckBox();
            this.nudWhiteSpaceSize = new System.Windows.Forms.NumericUpDown();
            this.lbWhiteSpaceSize = new System.Windows.Forms.Label();
            this.gbTabSymbol = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rbTabSymbolStrikeout = new System.Windows.Forms.RadioButton();
            this.rbTabSymbolArrow = new System.Windows.Forms.RadioButton();
            this.cbUseTabs = new System.Windows.Forms.CheckBox();
            this.tabEditorFont = new System.Windows.Forms.TabPage();
            this.lbFontSample = new System.Windows.Forms.Label();
            this.scintilla = new ScintillaNET.Scintilla();
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.lbFontSize = new System.Windows.Forms.Label();
            this.cmbFont = new System.Windows.Forms.ComboBox();
            this.lbFont = new System.Windows.Forms.Label();
            this.tpgColorSettings = new System.Windows.Forms.TabPage();
            this.cbUseNotepadPlusPlusTheme = new System.Windows.Forms.CheckBox();
            this.cmbNotepadPlusPlusTheme = new System.Windows.Forms.ComboBox();
            this.btNotepadPlusPlusThemePath = new System.Windows.Forms.Button();
            this.tbNotepadPlusPlusThemePath = new System.Windows.Forms.TextBox();
            this.lbNotepadPlusPlusThemePath = new System.Windows.Forms.Label();
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
            this.tabAdditionalColors = new System.Windows.Forms.TabPage();
            this.cbUseBraceMatching = new System.Windows.Forms.CheckBox();
            this.gbUseBraceMatching = new System.Windows.Forms.GroupBox();
            this.gbBraceFontStyle = new System.Windows.Forms.GroupBox();
            this.rbBraceStyleItalic = new System.Windows.Forms.RadioButton();
            this.rbBraceStyleBold = new System.Windows.Forms.RadioButton();
            this.lbBraceHighlightForegroundColor = new System.Windows.Forms.Label();
            this.btBraceHighlightForegroundColor = new System.Windows.Forms.Button();
            this.btBadBraceColor = new System.Windows.Forms.Button();
            this.lbBraceHighlightBackgroundColor = new System.Windows.Forms.Label();
            this.lbBadBraceColor = new System.Windows.Forms.Label();
            this.btBraceHighlightBackgroundColor = new System.Windows.Forms.Button();
            this.tabSpellCheck = new System.Windows.Forms.TabPage();
            this.cmbInstalledDictionaries = new System.Windows.Forms.ComboBox();
            this.lbInstalledDictionaries = new System.Windows.Forms.Label();
            this.btDictionaryPath = new System.Windows.Forms.Button();
            this.tbDictionaryPath = new System.Windows.Forms.TextBox();
            this.lbDictionaryPath = new System.Windows.Forms.Label();
            this.lbEditorSpellRecheckInactivity = new System.Windows.Forms.Label();
            this.nudEditorSpellRecheckInactivity = new System.Windows.Forms.NumericUpDown();
            this.btSpellCheckMarkColor = new System.Windows.Forms.Button();
            this.lbSpellCheckMarkColor = new System.Windows.Forms.Label();
            this.btHunspellAffixFile = new System.Windows.Forms.Button();
            this.tbHunspellAffixFile = new System.Windows.Forms.TextBox();
            this.lbHunspellAffixFile = new System.Windows.Forms.Label();
            this.btHunspellDictionary = new System.Windows.Forms.Button();
            this.tbHunspellDictionary = new System.Windows.Forms.TextBox();
            this.lbHunspellDictionary = new System.Windows.Forms.Label();
            this.cbSpellCheckInUse = new System.Windows.Forms.CheckBox();
            this.tabDateAndTime = new System.Windows.Forms.TabPage();
            this.cbDateTimeUseInvarianCulture = new System.Windows.Forms.CheckBox();
            this.lbDateTimeFormatDescriptionValue = new System.Windows.Forms.Label();
            this.lbDateTimeFormatDescription = new System.Windows.Forms.Label();
            this.gbDate = new System.Windows.Forms.GroupBox();
            this.lbDateTimeFormat6 = new System.Windows.Forms.Label();
            this.tbDateTimeFormat6 = new System.Windows.Forms.TextBox();
            this.lbDateTimeFormat5 = new System.Windows.Forms.Label();
            this.tbDateTimeFormat5 = new System.Windows.Forms.TextBox();
            this.lbDateTimeFormat4 = new System.Windows.Forms.Label();
            this.tbDateTimeFormat4 = new System.Windows.Forms.TextBox();
            this.lbDateTimeFormat3 = new System.Windows.Forms.Label();
            this.tbDateTimeFormat3 = new System.Windows.Forms.TextBox();
            this.lbDateTimeFormat2 = new System.Windows.Forms.Label();
            this.tbDateTimeFormat2 = new System.Windows.Forms.TextBox();
            this.lbDateTimeFormat1 = new System.Windows.Forms.Label();
            this.tbDateTimeFormat1 = new System.Windows.Forms.TextBox();
            this.lbDateStyle = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.cdColors = new System.Windows.Forms.ColorDialog();
            this.odDictionaryFile = new System.Windows.Forms.OpenFileDialog();
            this.odAffixFile = new System.Windows.Forms.OpenFileDialog();
            this.fdEditorFont = new System.Windows.Forms.FontDialog();
            this.tbRestartNote = new System.Windows.Forms.TextBox();
            this.lbDateTimeInstructionLink = new System.Windows.Forms.Label();
            this.btDateTimeDefaults = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tpgGeneralSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaximumSearchFileSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefaultFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDocumentContentHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryDocuments)).BeginInit();
            this.tpgAdditionalSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAutoSaveInterval)).BeginInit();
            this.tabEncoding.SuspendLayout();
            this.gpDefaultEncoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btUTF8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btSystemDefaultEncoding)).BeginInit();
            this.tabEditorSettings.SuspendLayout();
            this.gbZoomSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTabWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWhiteSpaceSize)).BeginInit();
            this.gbTabSymbol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabEditorFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            this.tpgColorSettings.SuspendLayout();
            this.tabAdditionalColors.SuspendLayout();
            this.gbUseBraceMatching.SuspendLayout();
            this.gbBraceFontStyle.SuspendLayout();
            this.tabSpellCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditorSpellRecheckInactivity)).BeginInit();
            this.tabDateAndTime.SuspendLayout();
            this.gbDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpgGeneralSettings);
            this.tcMain.Controls.Add(this.tpgAdditionalSettings);
            this.tcMain.Controls.Add(this.tabEncoding);
            this.tcMain.Controls.Add(this.tabEditorSettings);
            this.tcMain.Controls.Add(this.tabEditorFont);
            this.tcMain.Controls.Add(this.tpgColorSettings);
            this.tcMain.Controls.Add(this.tabAdditionalColors);
            this.tcMain.Controls.Add(this.tabSpellCheck);
            this.tcMain.Controls.Add(this.tabDateAndTime);
            this.tcMain.Location = new System.Drawing.Point(12, 12);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(668, 397);
            this.tcMain.TabIndex = 0;
            // 
            // tpgGeneralSettings
            // 
            this.tpgGeneralSettings.Controls.Add(this.cbSetThreadLocale);
            this.tpgGeneralSettings.Controls.Add(this.cbUseRTL);
            this.tpgGeneralSettings.Controls.Add(this.cbCategorizeProgrammingLanguages);
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
            this.tpgGeneralSettings.Location = new System.Drawing.Point(4, 22);
            this.tpgGeneralSettings.Name = "tpgGeneralSettings";
            this.tpgGeneralSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpgGeneralSettings.Size = new System.Drawing.Size(660, 371);
            this.tpgGeneralSettings.TabIndex = 0;
            this.tpgGeneralSettings.Text = "General";
            this.tpgGeneralSettings.UseVisualStyleBackColor = true;
            // 
            // cbSetThreadLocale
            // 
            this.cbSetThreadLocale.AutoSize = true;
            this.cbSetThreadLocale.Location = new System.Drawing.Point(6, 104);
            this.cbSetThreadLocale.Name = "cbSetThreadLocale";
            this.cbSetThreadLocale.Size = new System.Drawing.Size(233, 17);
            this.cbSetThreadLocale.TabIndex = 9;
            this.cbSetThreadLocale.Text = "Set the thread locale to match the language";
            this.cbSetThreadLocale.UseVisualStyleBackColor = true;
            // 
            // cbUseRTL
            // 
            this.cbUseRTL.AutoSize = true;
            this.cbUseRTL.Location = new System.Drawing.Point(6, 284);
            this.cbUseRTL.Name = "cbUseRTL";
            this.cbUseRTL.Size = new System.Drawing.Size(200, 17);
            this.cbUseRTL.TabIndex = 39;
            this.cbUseRTL.Text = "Use RTL (Right-to-left) text alignment";
            this.cbUseRTL.UseVisualStyleBackColor = true;
            this.cbUseRTL.Visible = false;
            // 
            // cbCategorizeProgrammingLanguages
            // 
            this.cbCategorizeProgrammingLanguages.AutoSize = true;
            this.cbCategorizeProgrammingLanguages.Location = new System.Drawing.Point(6, 258);
            this.cbCategorizeProgrammingLanguages.Name = "cbCategorizeProgrammingLanguages";
            this.cbCategorizeProgrammingLanguages.Size = new System.Drawing.Size(434, 17);
            this.cbCategorizeProgrammingLanguages.TabIndex = 38;
            this.cbCategorizeProgrammingLanguages.Text = "Categorize the programming language menu with the language name starting characte" +
    "r";
            this.cbCategorizeProgrammingLanguages.UseVisualStyleBackColor = true;
            // 
            // nudHistoryAmount
            // 
            this.nudHistoryAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudHistoryAmount.Location = new System.Drawing.Point(564, 231);
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
            // lbHistoryAmount
            // 
            this.lbHistoryAmount.AutoSize = true;
            this.lbHistoryAmount.Location = new System.Drawing.Point(3, 233);
            this.lbHistoryAmount.Name = "lbHistoryAmount";
            this.lbHistoryAmount.Size = new System.Drawing.Size(211, 13);
            this.lbHistoryAmount.TabIndex = 36;
            this.lbHistoryAmount.Text = "Maximum amount of search history to keep:";
            // 
            // nudMaximumSearchFileSize
            // 
            this.nudMaximumSearchFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMaximumSearchFileSize.Location = new System.Drawing.Point(564, 205);
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
            this.lbMaximumSearchFileSize.Location = new System.Drawing.Point(3, 207);
            this.lbMaximumSearchFileSize.Name = "lbMaximumSearchFileSize";
            this.lbMaximumSearchFileSize.Size = new System.Drawing.Size(194, 13);
            this.lbMaximumSearchFileSize.TabIndex = 33;
            this.lbMaximumSearchFileSize.Text = "Maximum file size for searhing text (MB):";
            // 
            // cbDockSearchTree
            // 
            this.cbDockSearchTree.AutoSize = true;
            this.cbDockSearchTree.Location = new System.Drawing.Point(6, 181);
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
            this.pbDefaultFolder.Location = new System.Drawing.Point(597, 155);
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
            this.btSelectPluginFolder.Location = new System.Drawing.Point(623, 155);
            this.btSelectPluginFolder.Name = "btSelectPluginFolder";
            this.btSelectPluginFolder.Size = new System.Drawing.Size(31, 20);
            this.btSelectPluginFolder.TabIndex = 30;
            this.btSelectPluginFolder.Tag = "tbPluginFolder";
            this.btSelectPluginFolder.Text = "...";
            this.btSelectPluginFolder.UseVisualStyleBackColor = true;
            this.btSelectPluginFolder.Click += new System.EventHandler(this.btCommonSelectFolder_Click);
            // 
            // tbPluginFolder
            // 
            this.tbPluginFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPluginFolder.Location = new System.Drawing.Point(6, 155);
            this.tbPluginFolder.Name = "tbPluginFolder";
            this.tbPluginFolder.Size = new System.Drawing.Size(585, 20);
            this.tbPluginFolder.TabIndex = 29;
            this.tbPluginFolder.TextChanged += new System.EventHandler(this.tbPluginFolder_TextChanged);
            // 
            // lbPluginFolder
            // 
            this.lbPluginFolder.AutoSize = true;
            this.lbPluginFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbPluginFolder.Location = new System.Drawing.Point(3, 139);
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
            this.lbSelectLanguageDescription.Location = new System.Drawing.Point(3, 70);
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
            this.cmbSelectLanguageValue.Location = new System.Drawing.Point(225, 67);
            this.cmbSelectLanguageValue.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.cmbSelectLanguageValue.Name = "cmbSelectLanguageValue";
            this.cmbSelectLanguageValue.Size = new System.Drawing.Size(429, 21);
            this.cmbSelectLanguageValue.TabIndex = 27;
            // 
            // cbDocumentContentHistory
            // 
            this.cbDocumentContentHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDocumentContentHistory.AutoSize = true;
            this.cbDocumentContentHistory.Checked = true;
            this.cbDocumentContentHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDocumentContentHistory.Location = new System.Drawing.Point(543, 34);
            this.cbDocumentContentHistory.Name = "cbDocumentContentHistory";
            this.cbDocumentContentHistory.Size = new System.Drawing.Size(15, 14);
            this.cbDocumentContentHistory.TabIndex = 13;
            this.cbDocumentContentHistory.UseVisualStyleBackColor = true;
            this.cbDocumentContentHistory.CheckedChanged += new System.EventHandler(this.cbDocumentContentHistory_CheckedChanged);
            // 
            // nudDocumentContentHistory
            // 
            this.nudDocumentContentHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDocumentContentHistory.Location = new System.Drawing.Point(564, 32);
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
            this.lbDocumentContentHistory.Location = new System.Drawing.Point(3, 34);
            this.lbDocumentContentHistory.Name = "lbDocumentContentHistory";
            this.lbDocumentContentHistory.Size = new System.Drawing.Size(289, 13);
            this.lbDocumentContentHistory.TabIndex = 11;
            this.lbDocumentContentHistory.Text = "How many closed document contents to keep in the history:";
            // 
            // nudHistoryDocuments
            // 
            this.nudHistoryDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudHistoryDocuments.Location = new System.Drawing.Point(564, 6);
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
            this.lbHistoryDocuments.Location = new System.Drawing.Point(3, 8);
            this.lbHistoryDocuments.Name = "lbHistoryDocuments";
            this.lbHistoryDocuments.Size = new System.Drawing.Size(232, 13);
            this.lbHistoryDocuments.TabIndex = 9;
            this.lbHistoryDocuments.Text = "How many documents to keep in the file history:";
            // 
            // tpgAdditionalSettings
            // 
            this.tpgAdditionalSettings.Controls.Add(this.tbNoteAutoSave);
            this.tpgAdditionalSettings.Controls.Add(this.nudAutoSaveInterval);
            this.tpgAdditionalSettings.Controls.Add(this.cbUseAutoSave);
            this.tpgAdditionalSettings.Location = new System.Drawing.Point(4, 22);
            this.tpgAdditionalSettings.Name = "tpgAdditionalSettings";
            this.tpgAdditionalSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpgAdditionalSettings.Size = new System.Drawing.Size(660, 371);
            this.tpgAdditionalSettings.TabIndex = 5;
            this.tpgAdditionalSettings.Text = "Additional";
            this.tpgAdditionalSettings.UseVisualStyleBackColor = true;
            // 
            // tbNoteAutoSave
            // 
            this.tbNoteAutoSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNoteAutoSave.BackColor = System.Drawing.Color.White;
            this.tbNoteAutoSave.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbNoteAutoSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNoteAutoSave.ForeColor = System.Drawing.Color.DarkCyan;
            this.tbNoteAutoSave.Location = new System.Drawing.Point(6, 30);
            this.tbNoteAutoSave.Multiline = true;
            this.tbNoteAutoSave.Name = "tbNoteAutoSave";
            this.tbNoteAutoSave.ReadOnly = true;
            this.tbNoteAutoSave.Size = new System.Drawing.Size(603, 30);
            this.tbNoteAutoSave.TabIndex = 3;
            this.tbNoteAutoSave.TabStop = false;
            this.tbNoteAutoSave.Text = "NOTE: The auto-save doesn\'t use the file system.";
            // 
            // nudAutoSaveInterval
            // 
            this.nudAutoSaveInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudAutoSaveInterval.Location = new System.Drawing.Point(589, 6);
            this.nudAutoSaveInterval.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.nudAutoSaveInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAutoSaveInterval.Name = "nudAutoSaveInterval";
            this.nudAutoSaveInterval.Size = new System.Drawing.Size(65, 20);
            this.nudAutoSaveInterval.TabIndex = 1;
            this.nudAutoSaveInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // cbUseAutoSave
            // 
            this.cbUseAutoSave.AutoSize = true;
            this.cbUseAutoSave.Location = new System.Drawing.Point(6, 7);
            this.cbUseAutoSave.Name = "cbUseAutoSave";
            this.cbUseAutoSave.Size = new System.Drawing.Size(170, 17);
            this.cbUseAutoSave.TabIndex = 0;
            this.cbUseAutoSave.Text = "Auto-save (interval in minutes):";
            this.cbUseAutoSave.UseVisualStyleBackColor = true;
            // 
            // tabEncoding
            // 
            this.tabEncoding.Controls.Add(this.cbDetectNoBomUnicode);
            this.tabEncoding.Controls.Add(this.cbEncodingAutoDetect);
            this.tabEncoding.Controls.Add(this.gpDefaultEncoding);
            this.tabEncoding.Location = new System.Drawing.Point(4, 22);
            this.tabEncoding.Name = "tabEncoding";
            this.tabEncoding.Padding = new System.Windows.Forms.Padding(3);
            this.tabEncoding.Size = new System.Drawing.Size(660, 371);
            this.tabEncoding.TabIndex = 6;
            this.tabEncoding.Text = "Encoding";
            this.tabEncoding.UseVisualStyleBackColor = true;
            // 
            // cbDetectNoBomUnicode
            // 
            this.cbDetectNoBomUnicode.AutoSize = true;
            this.cbDetectNoBomUnicode.Location = new System.Drawing.Point(6, 121);
            this.cbDetectNoBomUnicode.Name = "cbDetectNoBomUnicode";
            this.cbDetectNoBomUnicode.Size = new System.Drawing.Size(307, 17);
            this.cbDetectNoBomUnicode.TabIndex = 44;
            this.cbDetectNoBomUnicode.Text = "Try to detect unicode files without a BOM (Byte Order Mark)";
            this.cbDetectNoBomUnicode.UseVisualStyleBackColor = true;
            // 
            // cbEncodingAutoDetect
            // 
            this.cbEncodingAutoDetect.AutoSize = true;
            this.cbEncodingAutoDetect.Location = new System.Drawing.Point(6, 95);
            this.cbEncodingAutoDetect.Name = "cbEncodingAutoDetect";
            this.cbEncodingAutoDetect.Size = new System.Drawing.Size(311, 17);
            this.cbEncodingAutoDetect.TabIndex = 43;
            this.cbEncodingAutoDetect.Text = "Auto-detect encoding (the default will be used as a fall back)";
            this.cbEncodingAutoDetect.UseVisualStyleBackColor = true;
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
            this.gpDefaultEncoding.Size = new System.Drawing.Size(648, 81);
            this.gpDefaultEncoding.TabIndex = 42;
            this.gpDefaultEncoding.TabStop = false;
            this.gpDefaultEncoding.Text = "Default encoding";
            // 
            // btUTF8
            // 
            this.btUTF8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUTF8.Image = global::ScriptNotepad.Properties.Resources.unicode;
            this.btUTF8.Location = new System.Drawing.Point(616, 49);
            this.btUTF8.Name = "btUTF8";
            this.btUTF8.Size = new System.Drawing.Size(21, 21);
            this.btUTF8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btUTF8.TabIndex = 10;
            this.btUTF8.TabStop = false;
            this.ttMain.SetToolTip(this.btUTF8, "Set to unicode (UTF8)");
            // 
            // btSystemDefaultEncoding
            // 
            this.btSystemDefaultEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSystemDefaultEncoding.Image = global::ScriptNotepad.Properties.Resources.default_image;
            this.btSystemDefaultEncoding.Location = new System.Drawing.Point(616, 22);
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
            this.cmbCharacterSet.Size = new System.Drawing.Size(463, 21);
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
            this.cmbEncoding.Size = new System.Drawing.Size(463, 21);
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
            // tabEditorSettings
            // 
            this.tabEditorSettings.Controls.Add(this.gbZoomSetting);
            this.tabEditorSettings.Controls.Add(this.lbTabWidth);
            this.tabEditorSettings.Controls.Add(this.nudTabWidth);
            this.tabEditorSettings.Controls.Add(this.cbUseCodeIndentation);
            this.tabEditorSettings.Controls.Add(this.cmbSimulateKeyboard);
            this.tabEditorSettings.Controls.Add(this.cbSimulateKeyboard);
            this.tabEditorSettings.Controls.Add(this.cbIndentGuideOn);
            this.tabEditorSettings.Controls.Add(this.nudWhiteSpaceSize);
            this.tabEditorSettings.Controls.Add(this.lbWhiteSpaceSize);
            this.tabEditorSettings.Controls.Add(this.gbTabSymbol);
            this.tabEditorSettings.Controls.Add(this.cbUseTabs);
            this.tabEditorSettings.Location = new System.Drawing.Point(4, 22);
            this.tabEditorSettings.Name = "tabEditorSettings";
            this.tabEditorSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabEditorSettings.Size = new System.Drawing.Size(660, 371);
            this.tabEditorSettings.TabIndex = 2;
            this.tabEditorSettings.Text = "Editor";
            this.tabEditorSettings.UseVisualStyleBackColor = true;
            // 
            // gbZoomSetting
            // 
            this.gbZoomSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbZoomSetting.Controls.Add(this.cbSaveDocumentZoom);
            this.gbZoomSetting.Controls.Add(this.cbIndividualZoom);
            this.gbZoomSetting.Location = new System.Drawing.Point(6, 238);
            this.gbZoomSetting.Name = "gbZoomSetting";
            this.gbZoomSetting.Size = new System.Drawing.Size(648, 70);
            this.gbZoomSetting.TabIndex = 12;
            this.gbZoomSetting.TabStop = false;
            this.gbZoomSetting.Text = "Zoom settings";
            // 
            // cbSaveDocumentZoom
            // 
            this.cbSaveDocumentZoom.AutoSize = true;
            this.cbSaveDocumentZoom.Location = new System.Drawing.Point(6, 42);
            this.cbSaveDocumentZoom.Name = "cbSaveDocumentZoom";
            this.cbSaveDocumentZoom.Size = new System.Drawing.Size(172, 17);
            this.cbSaveDocumentZoom.TabIndex = 1;
            this.cbSaveDocumentZoom.Text = "Save document zoom on close";
            this.cbSaveDocumentZoom.UseVisualStyleBackColor = true;
            // 
            // cbIndividualZoom
            // 
            this.cbIndividualZoom.AutoSize = true;
            this.cbIndividualZoom.Location = new System.Drawing.Point(6, 19);
            this.cbIndividualZoom.Name = "cbIndividualZoom";
            this.cbIndividualZoom.Size = new System.Drawing.Size(171, 17);
            this.cbIndividualZoom.TabIndex = 0;
            this.cbIndividualZoom.Text = "Individual zoon (per document)";
            this.cbIndividualZoom.UseVisualStyleBackColor = true;
            // 
            // lbTabWidth
            // 
            this.lbTabWidth.AutoSize = true;
            this.lbTabWidth.Location = new System.Drawing.Point(300, 214);
            this.lbTabWidth.Name = "lbTabWidth";
            this.lbTabWidth.Size = new System.Drawing.Size(57, 13);
            this.lbTabWidth.TabIndex = 11;
            this.lbTabWidth.Text = "Tab width:";
            // 
            // nudTabWidth
            // 
            this.nudTabWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTabWidth.Location = new System.Drawing.Point(564, 212);
            this.nudTabWidth.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudTabWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTabWidth.Name = "nudTabWidth";
            this.nudTabWidth.Size = new System.Drawing.Size(90, 20);
            this.nudTabWidth.TabIndex = 10;
            this.nudTabWidth.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // cbUseCodeIndentation
            // 
            this.cbUseCodeIndentation.AutoSize = true;
            this.cbUseCodeIndentation.Location = new System.Drawing.Point(6, 213);
            this.cbUseCodeIndentation.Name = "cbUseCodeIndentation";
            this.cbUseCodeIndentation.Size = new System.Drawing.Size(118, 17);
            this.cbUseCodeIndentation.TabIndex = 9;
            this.cbUseCodeIndentation.Text = "Use code intending";
            this.cbUseCodeIndentation.UseVisualStyleBackColor = true;
            // 
            // cmbSimulateKeyboard
            // 
            this.cmbSimulateKeyboard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSimulateKeyboard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSimulateKeyboard.FormattingEnabled = true;
            this.cmbSimulateKeyboard.Location = new System.Drawing.Point(6, 185);
            this.cmbSimulateKeyboard.Name = "cmbSimulateKeyboard";
            this.cmbSimulateKeyboard.Size = new System.Drawing.Size(648, 21);
            this.cmbSimulateKeyboard.TabIndex = 8;
            // 
            // cbSimulateKeyboard
            // 
            this.cbSimulateKeyboard.AutoSize = true;
            this.cbSimulateKeyboard.Location = new System.Drawing.Point(6, 160);
            this.cbSimulateKeyboard.Name = "cbSimulateKeyboard";
            this.cbSimulateKeyboard.Size = new System.Drawing.Size(201, 17);
            this.cbSimulateKeyboard.TabIndex = 5;
            this.cbSimulateKeyboard.Text = "AltGr (Alt Graph) keyboard simulation:";
            this.cbSimulateKeyboard.UseVisualStyleBackColor = true;
            // 
            // cbIndentGuideOn
            // 
            this.cbIndentGuideOn.AutoSize = true;
            this.cbIndentGuideOn.Location = new System.Drawing.Point(6, 134);
            this.cbIndentGuideOn.Name = "cbIndentGuideOn";
            this.cbIndentGuideOn.Size = new System.Drawing.Size(149, 17);
            this.cbIndentGuideOn.TabIndex = 4;
            this.cbIndentGuideOn.Text = "Indent guide on by default";
            this.cbIndentGuideOn.UseVisualStyleBackColor = true;
            // 
            // nudWhiteSpaceSize
            // 
            this.nudWhiteSpaceSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudWhiteSpaceSize.Location = new System.Drawing.Point(564, 107);
            this.nudWhiteSpaceSize.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudWhiteSpaceSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWhiteSpaceSize.Name = "nudWhiteSpaceSize";
            this.nudWhiteSpaceSize.Size = new System.Drawing.Size(90, 20);
            this.nudWhiteSpaceSize.TabIndex = 3;
            this.nudWhiteSpaceSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbWhiteSpaceSize
            // 
            this.lbWhiteSpaceSize.AutoSize = true;
            this.lbWhiteSpaceSize.Location = new System.Drawing.Point(6, 109);
            this.lbWhiteSpaceSize.Name = "lbWhiteSpaceSize";
            this.lbWhiteSpaceSize.Size = new System.Drawing.Size(88, 13);
            this.lbWhiteSpaceSize.TabIndex = 2;
            this.lbWhiteSpaceSize.Text = "White space size";
            // 
            // gbTabSymbol
            // 
            this.gbTabSymbol.Controls.Add(this.pictureBox2);
            this.gbTabSymbol.Controls.Add(this.pictureBox1);
            this.gbTabSymbol.Controls.Add(this.rbTabSymbolStrikeout);
            this.gbTabSymbol.Controls.Add(this.rbTabSymbolArrow);
            this.gbTabSymbol.Location = new System.Drawing.Point(6, 31);
            this.gbTabSymbol.Name = "gbTabSymbol";
            this.gbTabSymbol.Size = new System.Drawing.Size(201, 70);
            this.gbTabSymbol.TabIndex = 1;
            this.gbTabSymbol.TabStop = false;
            this.gbTabSymbol.Text = "Tab symbol";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ScriptNotepad.Properties.Resources.tab_strikethrough;
            this.pictureBox2.Location = new System.Drawing.Point(159, 52);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 7);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ScriptNotepad.Properties.Resources.tab_arrow;
            this.pictureBox1.Location = new System.Drawing.Point(159, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 7);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // rbTabSymbolStrikeout
            // 
            this.rbTabSymbolStrikeout.AutoSize = true;
            this.rbTabSymbolStrikeout.Location = new System.Drawing.Point(6, 42);
            this.rbTabSymbolStrikeout.Name = "rbTabSymbolStrikeout";
            this.rbTabSymbolStrikeout.Size = new System.Drawing.Size(67, 17);
            this.rbTabSymbolStrikeout.TabIndex = 1;
            this.rbTabSymbolStrikeout.Text = "Strikeout";
            this.rbTabSymbolStrikeout.UseVisualStyleBackColor = true;
            // 
            // rbTabSymbolArrow
            // 
            this.rbTabSymbolArrow.AutoSize = true;
            this.rbTabSymbolArrow.Checked = true;
            this.rbTabSymbolArrow.Location = new System.Drawing.Point(6, 19);
            this.rbTabSymbolArrow.Name = "rbTabSymbolArrow";
            this.rbTabSymbolArrow.Size = new System.Drawing.Size(52, 17);
            this.rbTabSymbolArrow.TabIndex = 0;
            this.rbTabSymbolArrow.TabStop = true;
            this.rbTabSymbolArrow.Text = "Arrow";
            this.rbTabSymbolArrow.UseVisualStyleBackColor = true;
            // 
            // cbUseTabs
            // 
            this.cbUseTabs.AutoSize = true;
            this.cbUseTabs.Location = new System.Drawing.Point(6, 8);
            this.cbUseTabs.Name = "cbUseTabs";
            this.cbUseTabs.Size = new System.Drawing.Size(68, 17);
            this.cbUseTabs.TabIndex = 0;
            this.cbUseTabs.Text = "Use tabs";
            this.cbUseTabs.UseVisualStyleBackColor = true;
            // 
            // tabEditorFont
            // 
            this.tabEditorFont.Controls.Add(this.lbFontSample);
            this.tabEditorFont.Controls.Add(this.scintilla);
            this.tabEditorFont.Controls.Add(this.nudFontSize);
            this.tabEditorFont.Controls.Add(this.lbFontSize);
            this.tabEditorFont.Controls.Add(this.cmbFont);
            this.tabEditorFont.Controls.Add(this.lbFont);
            this.tabEditorFont.Location = new System.Drawing.Point(4, 22);
            this.tabEditorFont.Name = "tabEditorFont";
            this.tabEditorFont.Padding = new System.Windows.Forms.Padding(3);
            this.tabEditorFont.Size = new System.Drawing.Size(660, 371);
            this.tabEditorFont.TabIndex = 4;
            this.tabEditorFont.Text = "Editor font";
            this.tabEditorFont.UseVisualStyleBackColor = true;
            // 
            // lbFontSample
            // 
            this.lbFontSample.AutoSize = true;
            this.lbFontSample.Location = new System.Drawing.Point(6, 84);
            this.lbFontSample.Name = "lbFontSample";
            this.lbFontSample.Size = new System.Drawing.Size(42, 13);
            this.lbFontSample.TabIndex = 18;
            this.lbFontSample.Text = "Sample";
            // 
            // scintilla
            // 
            this.scintilla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scintilla.Location = new System.Drawing.Point(6, 100);
            this.scintilla.Name = "scintilla";
            this.scintilla.ScrollWidth = 2421;
            this.scintilla.Size = new System.Drawing.Size(648, 265);
            this.scintilla.TabIndex = 17;
            this.scintilla.Text = resources.GetString("scintilla.Text");
            // 
            // nudFontSize
            // 
            this.nudFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFontSize.Location = new System.Drawing.Point(468, 33);
            this.nudFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new System.Drawing.Size(186, 20);
            this.nudFontSize.TabIndex = 16;
            this.nudFontSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFontSize.ValueChanged += new System.EventHandler(this.CmbFont_SelectedIndexChanged);
            // 
            // lbFontSize
            // 
            this.lbFontSize.AutoSize = true;
            this.lbFontSize.Location = new System.Drawing.Point(6, 35);
            this.lbFontSize.Name = "lbFontSize";
            this.lbFontSize.Size = new System.Drawing.Size(52, 13);
            this.lbFontSize.TabIndex = 15;
            this.lbFontSize.Text = "Font size:";
            // 
            // cmbFont
            // 
            this.cmbFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFont.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFont.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFont.FormattingEnabled = true;
            this.cmbFont.Location = new System.Drawing.Point(190, 6);
            this.cmbFont.Name = "cmbFont";
            this.cmbFont.Size = new System.Drawing.Size(464, 21);
            this.cmbFont.TabIndex = 14;
            this.cmbFont.SelectedIndexChanged += new System.EventHandler(this.CmbFont_SelectedIndexChanged);
            // 
            // lbFont
            // 
            this.lbFont.AutoSize = true;
            this.lbFont.Location = new System.Drawing.Point(6, 9);
            this.lbFont.Name = "lbFont";
            this.lbFont.Size = new System.Drawing.Size(31, 13);
            this.lbFont.TabIndex = 13;
            this.lbFont.Text = "Font:";
            // 
            // tpgColorSettings
            // 
            this.tpgColorSettings.Controls.Add(this.cbUseNotepadPlusPlusTheme);
            this.tpgColorSettings.Controls.Add(this.cmbNotepadPlusPlusTheme);
            this.tpgColorSettings.Controls.Add(this.btNotepadPlusPlusThemePath);
            this.tpgColorSettings.Controls.Add(this.tbNotepadPlusPlusThemePath);
            this.tpgColorSettings.Controls.Add(this.lbNotepadPlusPlusThemePath);
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
            this.tpgColorSettings.Size = new System.Drawing.Size(660, 371);
            this.tpgColorSettings.TabIndex = 1;
            this.tpgColorSettings.Text = "Colors & themes";
            this.tpgColorSettings.UseVisualStyleBackColor = true;
            // 
            // cbUseNotepadPlusPlusTheme
            // 
            this.cbUseNotepadPlusPlusTheme.AutoSize = true;
            this.cbUseNotepadPlusPlusTheme.Location = new System.Drawing.Point(9, 276);
            this.cbUseNotepadPlusPlusTheme.Name = "cbUseNotepadPlusPlusTheme";
            this.cbUseNotepadPlusPlusTheme.Size = new System.Drawing.Size(136, 17);
            this.cbUseNotepadPlusPlusTheme.TabIndex = 48;
            this.cbUseNotepadPlusPlusTheme.Text = "Use Notepad++ theme:";
            this.cbUseNotepadPlusPlusTheme.UseVisualStyleBackColor = true;
            // 
            // cmbNotepadPlusPlusTheme
            // 
            this.cmbNotepadPlusPlusTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbNotepadPlusPlusTheme.DisplayMember = "DisplayName";
            this.cmbNotepadPlusPlusTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNotepadPlusPlusTheme.FormattingEnabled = true;
            this.cmbNotepadPlusPlusTheme.Location = new System.Drawing.Point(9, 302);
            this.cmbNotepadPlusPlusTheme.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.cmbNotepadPlusPlusTheme.Name = "cmbNotepadPlusPlusTheme";
            this.cmbNotepadPlusPlusTheme.Size = new System.Drawing.Size(645, 21);
            this.cmbNotepadPlusPlusTheme.TabIndex = 47;
            this.cmbNotepadPlusPlusTheme.SelectedIndexChanged += new System.EventHandler(this.CmbNotepadPlusPlusTheme_SelectedIndexChanged);
            // 
            // btNotepadPlusPlusThemePath
            // 
            this.btNotepadPlusPlusThemePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btNotepadPlusPlusThemePath.Location = new System.Drawing.Point(623, 238);
            this.btNotepadPlusPlusThemePath.Name = "btNotepadPlusPlusThemePath";
            this.btNotepadPlusPlusThemePath.Size = new System.Drawing.Size(31, 20);
            this.btNotepadPlusPlusThemePath.TabIndex = 45;
            this.btNotepadPlusPlusThemePath.Tag = "tbNotepadPlusPlusThemePath";
            this.btNotepadPlusPlusThemePath.Text = "...";
            this.btNotepadPlusPlusThemePath.UseVisualStyleBackColor = true;
            this.btNotepadPlusPlusThemePath.Click += new System.EventHandler(this.btCommonSelectFolder_Click);
            // 
            // tbNotepadPlusPlusThemePath
            // 
            this.tbNotepadPlusPlusThemePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNotepadPlusPlusThemePath.Location = new System.Drawing.Point(9, 238);
            this.tbNotepadPlusPlusThemePath.Name = "tbNotepadPlusPlusThemePath";
            this.tbNotepadPlusPlusThemePath.Size = new System.Drawing.Size(608, 20);
            this.tbNotepadPlusPlusThemePath.TabIndex = 44;
            // 
            // lbNotepadPlusPlusThemePath
            // 
            this.lbNotepadPlusPlusThemePath.AutoSize = true;
            this.lbNotepadPlusPlusThemePath.Location = new System.Drawing.Point(6, 215);
            this.lbNotepadPlusPlusThemePath.Name = "lbNotepadPlusPlusThemePath";
            this.lbNotepadPlusPlusThemePath.Size = new System.Drawing.Size(119, 13);
            this.lbNotepadPlusPlusThemePath.TabIndex = 43;
            this.lbNotepadPlusPlusThemePath.Text = "Notepad++ theme path:";
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
            this.btCurrentLineBackgroundColor.Location = new System.Drawing.Point(466, 180);
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
            this.btMarkStyle5Color.Location = new System.Drawing.Point(466, 151);
            this.btMarkStyle5Color.Name = "btMarkStyle5Color";
            this.btMarkStyle5Color.Size = new System.Drawing.Size(188, 23);
            this.btMarkStyle5Color.TabIndex = 11;
            this.btMarkStyle5Color.UseVisualStyleBackColor = true;
            this.btMarkStyle5Color.Click += new System.EventHandler(this.ColorButton_Click);
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
            this.btMarkStyle4Color.Location = new System.Drawing.Point(466, 122);
            this.btMarkStyle4Color.Name = "btMarkStyle4Color";
            this.btMarkStyle4Color.Size = new System.Drawing.Size(188, 23);
            this.btMarkStyle4Color.TabIndex = 9;
            this.btMarkStyle4Color.UseVisualStyleBackColor = true;
            this.btMarkStyle4Color.Click += new System.EventHandler(this.ColorButton_Click);
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
            this.btMarkStyle3Color.Location = new System.Drawing.Point(466, 93);
            this.btMarkStyle3Color.Name = "btMarkStyle3Color";
            this.btMarkStyle3Color.Size = new System.Drawing.Size(188, 23);
            this.btMarkStyle3Color.TabIndex = 7;
            this.btMarkStyle3Color.UseVisualStyleBackColor = true;
            this.btMarkStyle3Color.Click += new System.EventHandler(this.ColorButton_Click);
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
            this.btMarkStyle2Color.Location = new System.Drawing.Point(466, 64);
            this.btMarkStyle2Color.Name = "btMarkStyle2Color";
            this.btMarkStyle2Color.Size = new System.Drawing.Size(188, 23);
            this.btMarkStyle2Color.TabIndex = 5;
            this.btMarkStyle2Color.UseVisualStyleBackColor = true;
            this.btMarkStyle2Color.Click += new System.EventHandler(this.ColorButton_Click);
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
            this.btMarkStyle1Color.Location = new System.Drawing.Point(466, 35);
            this.btMarkStyle1Color.Name = "btMarkStyle1Color";
            this.btMarkStyle1Color.Size = new System.Drawing.Size(188, 23);
            this.btMarkStyle1Color.TabIndex = 3;
            this.btMarkStyle1Color.UseVisualStyleBackColor = true;
            this.btMarkStyle1Color.Click += new System.EventHandler(this.ColorButton_Click);
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
            this.btSmartHighlightColor.Location = new System.Drawing.Point(466, 6);
            this.btSmartHighlightColor.Name = "btSmartHighlightColor";
            this.btSmartHighlightColor.Size = new System.Drawing.Size(188, 23);
            this.btSmartHighlightColor.TabIndex = 1;
            this.btSmartHighlightColor.UseVisualStyleBackColor = true;
            this.btSmartHighlightColor.Click += new System.EventHandler(this.ColorButton_Click);
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
            // tabAdditionalColors
            // 
            this.tabAdditionalColors.Controls.Add(this.cbUseBraceMatching);
            this.tabAdditionalColors.Controls.Add(this.gbUseBraceMatching);
            this.tabAdditionalColors.Location = new System.Drawing.Point(4, 22);
            this.tabAdditionalColors.Name = "tabAdditionalColors";
            this.tabAdditionalColors.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdditionalColors.Size = new System.Drawing.Size(660, 371);
            this.tabAdditionalColors.TabIndex = 7;
            this.tabAdditionalColors.Text = "Additional colors and styles";
            this.tabAdditionalColors.UseVisualStyleBackColor = true;
            // 
            // cbUseBraceMatching
            // 
            this.cbUseBraceMatching.AutoSize = true;
            this.cbUseBraceMatching.Checked = true;
            this.cbUseBraceMatching.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseBraceMatching.Location = new System.Drawing.Point(3, 6);
            this.cbUseBraceMatching.Name = "cbUseBraceMatching";
            this.cbUseBraceMatching.Size = new System.Drawing.Size(121, 17);
            this.cbUseBraceMatching.TabIndex = 16;
            this.cbUseBraceMatching.Text = "Use brace matching";
            this.cbUseBraceMatching.UseVisualStyleBackColor = true;
            // 
            // gbUseBraceMatching
            // 
            this.gbUseBraceMatching.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUseBraceMatching.Controls.Add(this.gbBraceFontStyle);
            this.gbUseBraceMatching.Controls.Add(this.lbBraceHighlightForegroundColor);
            this.gbUseBraceMatching.Controls.Add(this.btBraceHighlightForegroundColor);
            this.gbUseBraceMatching.Controls.Add(this.btBadBraceColor);
            this.gbUseBraceMatching.Controls.Add(this.lbBraceHighlightBackgroundColor);
            this.gbUseBraceMatching.Controls.Add(this.lbBadBraceColor);
            this.gbUseBraceMatching.Controls.Add(this.btBraceHighlightBackgroundColor);
            this.gbUseBraceMatching.Location = new System.Drawing.Point(9, 7);
            this.gbUseBraceMatching.Name = "gbUseBraceMatching";
            this.gbUseBraceMatching.Size = new System.Drawing.Size(645, 156);
            this.gbUseBraceMatching.TabIndex = 15;
            this.gbUseBraceMatching.TabStop = false;
            // 
            // gbBraceFontStyle
            // 
            this.gbBraceFontStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBraceFontStyle.Controls.Add(this.rbBraceStyleItalic);
            this.gbBraceFontStyle.Controls.Add(this.rbBraceStyleBold);
            this.gbBraceFontStyle.Location = new System.Drawing.Point(9, 98);
            this.gbBraceFontStyle.Name = "gbBraceFontStyle";
            this.gbBraceFontStyle.Size = new System.Drawing.Size(630, 52);
            this.gbBraceFontStyle.TabIndex = 8;
            this.gbBraceFontStyle.TabStop = false;
            this.gbBraceFontStyle.Text = "Font style";
            // 
            // rbBraceStyleItalic
            // 
            this.rbBraceStyleItalic.AutoSize = true;
            this.rbBraceStyleItalic.Location = new System.Drawing.Point(237, 19);
            this.rbBraceStyleItalic.Name = "rbBraceStyleItalic";
            this.rbBraceStyleItalic.Size = new System.Drawing.Size(47, 17);
            this.rbBraceStyleItalic.TabIndex = 1;
            this.rbBraceStyleItalic.TabStop = true;
            this.rbBraceStyleItalic.Text = "Italic";
            this.rbBraceStyleItalic.UseVisualStyleBackColor = true;
            // 
            // rbBraceStyleBold
            // 
            this.rbBraceStyleBold.AutoSize = true;
            this.rbBraceStyleBold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBraceStyleBold.Location = new System.Drawing.Point(6, 19);
            this.rbBraceStyleBold.Name = "rbBraceStyleBold";
            this.rbBraceStyleBold.Size = new System.Drawing.Size(46, 17);
            this.rbBraceStyleBold.TabIndex = 0;
            this.rbBraceStyleBold.TabStop = true;
            this.rbBraceStyleBold.Text = "Bold";
            this.rbBraceStyleBold.UseVisualStyleBackColor = true;
            // 
            // lbBraceHighlightForegroundColor
            // 
            this.lbBraceHighlightForegroundColor.AutoSize = true;
            this.lbBraceHighlightForegroundColor.Location = new System.Drawing.Point(6, 16);
            this.lbBraceHighlightForegroundColor.Name = "lbBraceHighlightForegroundColor";
            this.lbBraceHighlightForegroundColor.Size = new System.Drawing.Size(160, 13);
            this.lbBraceHighlightForegroundColor.TabIndex = 2;
            this.lbBraceHighlightForegroundColor.Text = "Brace highlight foreground color:";
            // 
            // btBraceHighlightForegroundColor
            // 
            this.btBraceHighlightForegroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBraceHighlightForegroundColor.Location = new System.Drawing.Point(451, 11);
            this.btBraceHighlightForegroundColor.Name = "btBraceHighlightForegroundColor";
            this.btBraceHighlightForegroundColor.Size = new System.Drawing.Size(188, 23);
            this.btBraceHighlightForegroundColor.TabIndex = 3;
            this.btBraceHighlightForegroundColor.UseVisualStyleBackColor = true;
            this.btBraceHighlightForegroundColor.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // btBadBraceColor
            // 
            this.btBadBraceColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBadBraceColor.Location = new System.Drawing.Point(451, 69);
            this.btBadBraceColor.Name = "btBadBraceColor";
            this.btBadBraceColor.Size = new System.Drawing.Size(188, 23);
            this.btBadBraceColor.TabIndex = 7;
            this.btBadBraceColor.UseVisualStyleBackColor = true;
            this.btBadBraceColor.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // lbBraceHighlightBackgroundColor
            // 
            this.lbBraceHighlightBackgroundColor.AutoSize = true;
            this.lbBraceHighlightBackgroundColor.Location = new System.Drawing.Point(6, 45);
            this.lbBraceHighlightBackgroundColor.Name = "lbBraceHighlightBackgroundColor";
            this.lbBraceHighlightBackgroundColor.Size = new System.Drawing.Size(166, 13);
            this.lbBraceHighlightBackgroundColor.TabIndex = 4;
            this.lbBraceHighlightBackgroundColor.Text = "Brace highlight background color:";
            // 
            // lbBadBraceColor
            // 
            this.lbBadBraceColor.AutoSize = true;
            this.lbBadBraceColor.Location = new System.Drawing.Point(6, 74);
            this.lbBadBraceColor.Name = "lbBadBraceColor";
            this.lbBadBraceColor.Size = new System.Drawing.Size(85, 13);
            this.lbBadBraceColor.TabIndex = 6;
            this.lbBadBraceColor.Text = "Bad brace color:";
            // 
            // btBraceHighlightBackgroundColor
            // 
            this.btBraceHighlightBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBraceHighlightBackgroundColor.Location = new System.Drawing.Point(451, 40);
            this.btBraceHighlightBackgroundColor.Name = "btBraceHighlightBackgroundColor";
            this.btBraceHighlightBackgroundColor.Size = new System.Drawing.Size(188, 23);
            this.btBraceHighlightBackgroundColor.TabIndex = 5;
            this.btBraceHighlightBackgroundColor.UseVisualStyleBackColor = true;
            this.btBraceHighlightBackgroundColor.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // tabSpellCheck
            // 
            this.tabSpellCheck.Controls.Add(this.cmbInstalledDictionaries);
            this.tabSpellCheck.Controls.Add(this.lbInstalledDictionaries);
            this.tabSpellCheck.Controls.Add(this.btDictionaryPath);
            this.tabSpellCheck.Controls.Add(this.tbDictionaryPath);
            this.tabSpellCheck.Controls.Add(this.lbDictionaryPath);
            this.tabSpellCheck.Controls.Add(this.lbEditorSpellRecheckInactivity);
            this.tabSpellCheck.Controls.Add(this.nudEditorSpellRecheckInactivity);
            this.tabSpellCheck.Controls.Add(this.btSpellCheckMarkColor);
            this.tabSpellCheck.Controls.Add(this.lbSpellCheckMarkColor);
            this.tabSpellCheck.Controls.Add(this.btHunspellAffixFile);
            this.tabSpellCheck.Controls.Add(this.tbHunspellAffixFile);
            this.tabSpellCheck.Controls.Add(this.lbHunspellAffixFile);
            this.tabSpellCheck.Controls.Add(this.btHunspellDictionary);
            this.tabSpellCheck.Controls.Add(this.tbHunspellDictionary);
            this.tabSpellCheck.Controls.Add(this.lbHunspellDictionary);
            this.tabSpellCheck.Controls.Add(this.cbSpellCheckInUse);
            this.tabSpellCheck.Location = new System.Drawing.Point(4, 22);
            this.tabSpellCheck.Name = "tabSpellCheck";
            this.tabSpellCheck.Padding = new System.Windows.Forms.Padding(3);
            this.tabSpellCheck.Size = new System.Drawing.Size(660, 371);
            this.tabSpellCheck.TabIndex = 3;
            this.tabSpellCheck.Text = "Spell checking";
            this.tabSpellCheck.UseVisualStyleBackColor = true;
            // 
            // cmbInstalledDictionaries
            // 
            this.cmbInstalledDictionaries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInstalledDictionaries.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbInstalledDictionaries.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbInstalledDictionaries.DisplayMember = "DisplayName";
            this.cmbInstalledDictionaries.FormattingEnabled = true;
            this.cmbInstalledDictionaries.Location = new System.Drawing.Point(9, 276);
            this.cmbInstalledDictionaries.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.cmbInstalledDictionaries.Name = "cmbInstalledDictionaries";
            this.cmbInstalledDictionaries.Size = new System.Drawing.Size(645, 21);
            this.cmbInstalledDictionaries.TabIndex = 44;
            this.cmbInstalledDictionaries.SelectedIndexChanged += new System.EventHandler(this.CmbInstalledDictionaries_SelectedIndexChanged);
            // 
            // lbInstalledDictionaries
            // 
            this.lbInstalledDictionaries.AutoSize = true;
            this.lbInstalledDictionaries.Location = new System.Drawing.Point(6, 251);
            this.lbInstalledDictionaries.Name = "lbInstalledDictionaries";
            this.lbInstalledDictionaries.Size = new System.Drawing.Size(149, 13);
            this.lbInstalledDictionaries.TabIndex = 43;
            this.lbInstalledDictionaries.Text = "Installed Hunspell dictionaries:";
            // 
            // btDictionaryPath
            // 
            this.btDictionaryPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDictionaryPath.Location = new System.Drawing.Point(623, 217);
            this.btDictionaryPath.Name = "btDictionaryPath";
            this.btDictionaryPath.Size = new System.Drawing.Size(31, 20);
            this.btDictionaryPath.TabIndex = 42;
            this.btDictionaryPath.Tag = "tbDictionaryPath";
            this.btDictionaryPath.Text = "...";
            this.btDictionaryPath.UseVisualStyleBackColor = true;
            this.btDictionaryPath.Click += new System.EventHandler(this.btCommonSelectFolder_Click);
            // 
            // tbDictionaryPath
            // 
            this.tbDictionaryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDictionaryPath.Location = new System.Drawing.Point(9, 217);
            this.tbDictionaryPath.Name = "tbDictionaryPath";
            this.tbDictionaryPath.Size = new System.Drawing.Size(608, 20);
            this.tbDictionaryPath.TabIndex = 41;
            // 
            // lbDictionaryPath
            // 
            this.lbDictionaryPath.AutoSize = true;
            this.lbDictionaryPath.Location = new System.Drawing.Point(6, 194);
            this.lbDictionaryPath.Name = "lbDictionaryPath";
            this.lbDictionaryPath.Size = new System.Drawing.Size(139, 13);
            this.lbDictionaryPath.TabIndex = 40;
            this.lbDictionaryPath.Text = "Hunspell dictionary file path:";
            // 
            // lbEditorSpellRecheckInactivity
            // 
            this.lbEditorSpellRecheckInactivity.AutoSize = true;
            this.lbEditorSpellRecheckInactivity.Location = new System.Drawing.Point(6, 167);
            this.lbEditorSpellRecheckInactivity.Name = "lbEditorSpellRecheckInactivity";
            this.lbEditorSpellRecheckInactivity.Size = new System.Drawing.Size(358, 13);
            this.lbEditorSpellRecheckInactivity.TabIndex = 38;
            this.lbEditorSpellRecheckInactivity.Text = "Recheck the spelling after text change after user inactivity for milliseconds:";
            // 
            // nudEditorSpellRecheckInactivity
            // 
            this.nudEditorSpellRecheckInactivity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudEditorSpellRecheckInactivity.Location = new System.Drawing.Point(564, 165);
            this.nudEditorSpellRecheckInactivity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudEditorSpellRecheckInactivity.Name = "nudEditorSpellRecheckInactivity";
            this.nudEditorSpellRecheckInactivity.Size = new System.Drawing.Size(90, 20);
            this.nudEditorSpellRecheckInactivity.TabIndex = 37;
            this.nudEditorSpellRecheckInactivity.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // btSpellCheckMarkColor
            // 
            this.btSpellCheckMarkColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSpellCheckMarkColor.Location = new System.Drawing.Point(368, 136);
            this.btSpellCheckMarkColor.Name = "btSpellCheckMarkColor";
            this.btSpellCheckMarkColor.Size = new System.Drawing.Size(286, 23);
            this.btSpellCheckMarkColor.TabIndex = 36;
            this.btSpellCheckMarkColor.UseVisualStyleBackColor = true;
            this.btSpellCheckMarkColor.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // lbSpellCheckMarkColor
            // 
            this.lbSpellCheckMarkColor.AutoSize = true;
            this.lbSpellCheckMarkColor.Location = new System.Drawing.Point(6, 141);
            this.lbSpellCheckMarkColor.Name = "lbSpellCheckMarkColor";
            this.lbSpellCheckMarkColor.Size = new System.Drawing.Size(118, 13);
            this.lbSpellCheckMarkColor.TabIndex = 35;
            this.lbSpellCheckMarkColor.Text = "Spell check mark color:";
            // 
            // btHunspellAffixFile
            // 
            this.btHunspellAffixFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btHunspellAffixFile.Location = new System.Drawing.Point(623, 110);
            this.btHunspellAffixFile.Name = "btHunspellAffixFile";
            this.btHunspellAffixFile.Size = new System.Drawing.Size(31, 20);
            this.btHunspellAffixFile.TabIndex = 34;
            this.btHunspellAffixFile.Text = "...";
            this.btHunspellAffixFile.UseVisualStyleBackColor = true;
            this.btHunspellAffixFile.Click += new System.EventHandler(this.BtHunspellAffixFile_Click);
            // 
            // tbHunspellAffixFile
            // 
            this.tbHunspellAffixFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHunspellAffixFile.Location = new System.Drawing.Point(9, 110);
            this.tbHunspellAffixFile.Name = "tbHunspellAffixFile";
            this.tbHunspellAffixFile.Size = new System.Drawing.Size(608, 20);
            this.tbHunspellAffixFile.TabIndex = 33;
            // 
            // lbHunspellAffixFile
            // 
            this.lbHunspellAffixFile.AutoSize = true;
            this.lbHunspellAffixFile.Location = new System.Drawing.Point(6, 87);
            this.lbHunspellAffixFile.Name = "lbHunspellAffixFile";
            this.lbHunspellAffixFile.Size = new System.Drawing.Size(128, 13);
            this.lbHunspellAffixFile.TabIndex = 32;
            this.lbHunspellAffixFile.Text = "Hunspell affix file (UTF-8):";
            // 
            // btHunspellDictionary
            // 
            this.btHunspellDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btHunspellDictionary.Location = new System.Drawing.Point(623, 57);
            this.btHunspellDictionary.Name = "btHunspellDictionary";
            this.btHunspellDictionary.Size = new System.Drawing.Size(31, 20);
            this.btHunspellDictionary.TabIndex = 31;
            this.btHunspellDictionary.Text = "...";
            this.btHunspellDictionary.UseVisualStyleBackColor = true;
            this.btHunspellDictionary.Click += new System.EventHandler(this.BtHunspellDictionary_Click);
            // 
            // tbHunspellDictionary
            // 
            this.tbHunspellDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHunspellDictionary.Location = new System.Drawing.Point(9, 58);
            this.tbHunspellDictionary.Name = "tbHunspellDictionary";
            this.tbHunspellDictionary.Size = new System.Drawing.Size(608, 20);
            this.tbHunspellDictionary.TabIndex = 4;
            // 
            // lbHunspellDictionary
            // 
            this.lbHunspellDictionary.AutoSize = true;
            this.lbHunspellDictionary.Location = new System.Drawing.Point(6, 35);
            this.lbHunspellDictionary.Name = "lbHunspellDictionary";
            this.lbHunspellDictionary.Size = new System.Drawing.Size(154, 13);
            this.lbHunspellDictionary.TabIndex = 2;
            this.lbHunspellDictionary.Text = "Hunspell dictionary file (UTF-8):";
            // 
            // cbSpellCheckInUse
            // 
            this.cbSpellCheckInUse.AutoSize = true;
            this.cbSpellCheckInUse.Location = new System.Drawing.Point(6, 8);
            this.cbSpellCheckInUse.Name = "cbSpellCheckInUse";
            this.cbSpellCheckInUse.Size = new System.Drawing.Size(116, 17);
            this.cbSpellCheckInUse.TabIndex = 1;
            this.cbSpellCheckInUse.Text = "Use spell checking";
            this.cbSpellCheckInUse.UseVisualStyleBackColor = true;
            this.cbSpellCheckInUse.Click += new System.EventHandler(this.CbSpellCheckInUse_Click);
            // 
            // tabDateAndTime
            // 
            this.tabDateAndTime.Controls.Add(this.btDateTimeDefaults);
            this.tabDateAndTime.Controls.Add(this.lbDateTimeInstructionLink);
            this.tabDateAndTime.Controls.Add(this.cbDateTimeUseInvarianCulture);
            this.tabDateAndTime.Controls.Add(this.lbDateTimeFormatDescriptionValue);
            this.tabDateAndTime.Controls.Add(this.lbDateTimeFormatDescription);
            this.tabDateAndTime.Controls.Add(this.gbDate);
            this.tabDateAndTime.Location = new System.Drawing.Point(4, 22);
            this.tabDateAndTime.Name = "tabDateAndTime";
            this.tabDateAndTime.Padding = new System.Windows.Forms.Padding(3);
            this.tabDateAndTime.Size = new System.Drawing.Size(660, 371);
            this.tabDateAndTime.TabIndex = 8;
            this.tabDateAndTime.Text = "Date and time";
            this.tabDateAndTime.UseVisualStyleBackColor = true;
            // 
            // cbDateTimeUseInvarianCulture
            // 
            this.cbDateTimeUseInvarianCulture.AutoSize = true;
            this.cbDateTimeUseInvarianCulture.Location = new System.Drawing.Point(9, 250);
            this.cbDateTimeUseInvarianCulture.Name = "cbDateTimeUseInvarianCulture";
            this.cbDateTimeUseInvarianCulture.Size = new System.Drawing.Size(310, 17);
            this.cbDateTimeUseInvarianCulture.TabIndex = 3;
            this.cbDateTimeUseInvarianCulture.Text = "Use invariant culture (will affect the date and time formatting)";
            this.cbDateTimeUseInvarianCulture.UseVisualStyleBackColor = true;
            this.cbDateTimeUseInvarianCulture.CheckedChanged += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            this.cbDateTimeUseInvarianCulture.Click += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            this.cbDateTimeUseInvarianCulture.Enter += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            // 
            // lbDateTimeFormatDescriptionValue
            // 
            this.lbDateTimeFormatDescriptionValue.AutoSize = true;
            this.lbDateTimeFormatDescriptionValue.Location = new System.Drawing.Point(12, 234);
            this.lbDateTimeFormatDescriptionValue.Name = "lbDateTimeFormatDescriptionValue";
            this.lbDateTimeFormatDescriptionValue.Size = new System.Drawing.Size(49, 13);
            this.lbDateTimeFormatDescriptionValue.TabIndex = 2;
            this.lbDateTimeFormatDescriptionValue.Text = "00:00:00";
            // 
            // lbDateTimeFormatDescription
            // 
            this.lbDateTimeFormatDescription.AutoSize = true;
            this.lbDateTimeFormatDescription.Location = new System.Drawing.Point(6, 211);
            this.lbDateTimeFormatDescription.Name = "lbDateTimeFormatDescription";
            this.lbDateTimeFormatDescription.Size = new System.Drawing.Size(58, 13);
            this.lbDateTimeFormatDescription.TabIndex = 1;
            this.lbDateTimeFormatDescription.Text = "Looks like:";
            // 
            // gbDate
            // 
            this.gbDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDate.Controls.Add(this.lbDateTimeFormat6);
            this.gbDate.Controls.Add(this.tbDateTimeFormat6);
            this.gbDate.Controls.Add(this.lbDateTimeFormat5);
            this.gbDate.Controls.Add(this.tbDateTimeFormat5);
            this.gbDate.Controls.Add(this.lbDateTimeFormat4);
            this.gbDate.Controls.Add(this.tbDateTimeFormat4);
            this.gbDate.Controls.Add(this.lbDateTimeFormat3);
            this.gbDate.Controls.Add(this.tbDateTimeFormat3);
            this.gbDate.Controls.Add(this.lbDateTimeFormat2);
            this.gbDate.Controls.Add(this.tbDateTimeFormat2);
            this.gbDate.Controls.Add(this.lbDateTimeFormat1);
            this.gbDate.Controls.Add(this.tbDateTimeFormat1);
            this.gbDate.Controls.Add(this.lbDateStyle);
            this.gbDate.Location = new System.Drawing.Point(6, 6);
            this.gbDate.Name = "gbDate";
            this.gbDate.Size = new System.Drawing.Size(648, 202);
            this.gbDate.TabIndex = 0;
            this.gbDate.TabStop = false;
            this.gbDate.Text = "Date and/or time formats";
            // 
            // lbDateTimeFormat6
            // 
            this.lbDateTimeFormat6.AutoSize = true;
            this.lbDateTimeFormat6.Location = new System.Drawing.Point(6, 172);
            this.lbDateTimeFormat6.Name = "lbDateTimeFormat6";
            this.lbDateTimeFormat6.Size = new System.Drawing.Size(131, 13);
            this.lbDateTimeFormat6.TabIndex = 16;
            this.lbDateTimeFormat6.Text = "Date and/or time format 6:";
            // 
            // tbDateTimeFormat6
            // 
            this.tbDateTimeFormat6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDateTimeFormat6.Location = new System.Drawing.Point(337, 169);
            this.tbDateTimeFormat6.Name = "tbDateTimeFormat6";
            this.tbDateTimeFormat6.Size = new System.Drawing.Size(305, 20);
            this.tbDateTimeFormat6.TabIndex = 15;
            this.tbDateTimeFormat6.TextChanged += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            this.tbDateTimeFormat6.Enter += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            // 
            // lbDateTimeFormat5
            // 
            this.lbDateTimeFormat5.AutoSize = true;
            this.lbDateTimeFormat5.Location = new System.Drawing.Point(6, 146);
            this.lbDateTimeFormat5.Name = "lbDateTimeFormat5";
            this.lbDateTimeFormat5.Size = new System.Drawing.Size(131, 13);
            this.lbDateTimeFormat5.TabIndex = 14;
            this.lbDateTimeFormat5.Text = "Date and/or time format 5:";
            // 
            // tbDateTimeFormat5
            // 
            this.tbDateTimeFormat5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDateTimeFormat5.Location = new System.Drawing.Point(337, 143);
            this.tbDateTimeFormat5.Name = "tbDateTimeFormat5";
            this.tbDateTimeFormat5.Size = new System.Drawing.Size(305, 20);
            this.tbDateTimeFormat5.TabIndex = 13;
            this.tbDateTimeFormat5.TextChanged += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            this.tbDateTimeFormat5.Enter += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            // 
            // lbDateTimeFormat4
            // 
            this.lbDateTimeFormat4.AutoSize = true;
            this.lbDateTimeFormat4.Location = new System.Drawing.Point(6, 120);
            this.lbDateTimeFormat4.Name = "lbDateTimeFormat4";
            this.lbDateTimeFormat4.Size = new System.Drawing.Size(131, 13);
            this.lbDateTimeFormat4.TabIndex = 12;
            this.lbDateTimeFormat4.Text = "Date and/or time format 4:";
            // 
            // tbDateTimeFormat4
            // 
            this.tbDateTimeFormat4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDateTimeFormat4.Location = new System.Drawing.Point(337, 117);
            this.tbDateTimeFormat4.Name = "tbDateTimeFormat4";
            this.tbDateTimeFormat4.Size = new System.Drawing.Size(305, 20);
            this.tbDateTimeFormat4.TabIndex = 11;
            this.tbDateTimeFormat4.TextChanged += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            this.tbDateTimeFormat4.Enter += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            // 
            // lbDateTimeFormat3
            // 
            this.lbDateTimeFormat3.AutoSize = true;
            this.lbDateTimeFormat3.Location = new System.Drawing.Point(6, 94);
            this.lbDateTimeFormat3.Name = "lbDateTimeFormat3";
            this.lbDateTimeFormat3.Size = new System.Drawing.Size(131, 13);
            this.lbDateTimeFormat3.TabIndex = 10;
            this.lbDateTimeFormat3.Text = "Date and/or time format 3:";
            // 
            // tbDateTimeFormat3
            // 
            this.tbDateTimeFormat3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDateTimeFormat3.Location = new System.Drawing.Point(337, 91);
            this.tbDateTimeFormat3.Name = "tbDateTimeFormat3";
            this.tbDateTimeFormat3.Size = new System.Drawing.Size(305, 20);
            this.tbDateTimeFormat3.TabIndex = 9;
            this.tbDateTimeFormat3.TextChanged += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            this.tbDateTimeFormat3.Enter += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            // 
            // lbDateTimeFormat2
            // 
            this.lbDateTimeFormat2.AutoSize = true;
            this.lbDateTimeFormat2.Location = new System.Drawing.Point(6, 68);
            this.lbDateTimeFormat2.Name = "lbDateTimeFormat2";
            this.lbDateTimeFormat2.Size = new System.Drawing.Size(131, 13);
            this.lbDateTimeFormat2.TabIndex = 8;
            this.lbDateTimeFormat2.Text = "Date and/or time format 2:";
            // 
            // tbDateTimeFormat2
            // 
            this.tbDateTimeFormat2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDateTimeFormat2.Location = new System.Drawing.Point(337, 65);
            this.tbDateTimeFormat2.Name = "tbDateTimeFormat2";
            this.tbDateTimeFormat2.Size = new System.Drawing.Size(305, 20);
            this.tbDateTimeFormat2.TabIndex = 7;
            this.tbDateTimeFormat2.TextChanged += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            this.tbDateTimeFormat2.Enter += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            // 
            // lbDateTimeFormat1
            // 
            this.lbDateTimeFormat1.AutoSize = true;
            this.lbDateTimeFormat1.Location = new System.Drawing.Point(6, 42);
            this.lbDateTimeFormat1.Name = "lbDateTimeFormat1";
            this.lbDateTimeFormat1.Size = new System.Drawing.Size(131, 13);
            this.lbDateTimeFormat1.TabIndex = 6;
            this.lbDateTimeFormat1.Text = "Date and/or time format 1:";
            // 
            // tbDateTimeFormat1
            // 
            this.tbDateTimeFormat1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDateTimeFormat1.Location = new System.Drawing.Point(337, 39);
            this.tbDateTimeFormat1.Name = "tbDateTimeFormat1";
            this.tbDateTimeFormat1.Size = new System.Drawing.Size(305, 20);
            this.tbDateTimeFormat1.TabIndex = 1;
            this.tbDateTimeFormat1.TextChanged += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            this.tbDateTimeFormat1.Enter += new System.EventHandler(this.TbDateTimeFormat1_TextChanged);
            // 
            // lbDateStyle
            // 
            this.lbDateStyle.AutoSize = true;
            this.lbDateStyle.Location = new System.Drawing.Point(6, 16);
            this.lbDateStyle.Name = "lbDateStyle";
            this.lbDateStyle.Size = new System.Drawing.Size(68, 13);
            this.lbDateStyle.TabIndex = 0;
            this.lbDateStyle.Text = "Styles 1 to 6:";
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(605, 415);
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
            // odDictionaryFile
            // 
            this.odDictionaryFile.Filter = "Hunspell dictionary file|*.dic";
            // 
            // odAffixFile
            // 
            this.odAffixFile.Filter = "Hunspell affix dictionary description file|*.aff";
            // 
            // tbRestartNote
            // 
            this.tbRestartNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRestartNote.BackColor = System.Drawing.Color.White;
            this.tbRestartNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbRestartNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRestartNote.ForeColor = System.Drawing.Color.DarkCyan;
            this.tbRestartNote.Location = new System.Drawing.Point(93, 415);
            this.tbRestartNote.Multiline = true;
            this.tbRestartNote.Name = "tbRestartNote";
            this.tbRestartNote.ReadOnly = true;
            this.tbRestartNote.Size = new System.Drawing.Size(506, 23);
            this.tbRestartNote.TabIndex = 8;
            this.tbRestartNote.TabStop = false;
            this.tbRestartNote.Text = "NOTE: Almost all settings require a restart of the software";
            // 
            // lbDateTimeInstructionLink
            // 
            this.lbDateTimeInstructionLink.AutoSize = true;
            this.lbDateTimeInstructionLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbDateTimeInstructionLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDateTimeInstructionLink.ForeColor = System.Drawing.Color.Navy;
            this.lbDateTimeInstructionLink.Location = new System.Drawing.Point(6, 270);
            this.lbDateTimeInstructionLink.Name = "lbDateTimeInstructionLink";
            this.lbDateTimeInstructionLink.Size = new System.Drawing.Size(357, 13);
            this.lbDateTimeInstructionLink.TabIndex = 4;
            this.lbDateTimeInstructionLink.Tag = "https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-" +
    "format-strings";
            this.lbDateTimeInstructionLink.Text = "A link to a Microsoft web site describing the date and time format specifiers";
            this.lbDateTimeInstructionLink.Click += new System.EventHandler(this.LbDateTimeInstructionLink_Click);
            // 
            // btDateTimeDefaults
            // 
            this.btDateTimeDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btDateTimeDefaults.Location = new System.Drawing.Point(6, 342);
            this.btDateTimeDefaults.Name = "btDateTimeDefaults";
            this.btDateTimeDefaults.Size = new System.Drawing.Size(134, 23);
            this.btDateTimeDefaults.TabIndex = 5;
            this.btDateTimeDefaults.Text = "Defaults";
            this.btDateTimeDefaults.UseVisualStyleBackColor = true;
            this.btDateTimeDefaults.Click += new System.EventHandler(this.BtDateTimeDefaults_Click);
            // 
            // FormSettings
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(692, 450);
            this.Controls.Add(this.tbRestartNote);
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
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaximumSearchFileSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefaultFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDocumentContentHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryDocuments)).EndInit();
            this.tpgAdditionalSettings.ResumeLayout(false);
            this.tpgAdditionalSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAutoSaveInterval)).EndInit();
            this.tabEncoding.ResumeLayout(false);
            this.tabEncoding.PerformLayout();
            this.gpDefaultEncoding.ResumeLayout(false);
            this.gpDefaultEncoding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btUTF8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btSystemDefaultEncoding)).EndInit();
            this.tabEditorSettings.ResumeLayout(false);
            this.tabEditorSettings.PerformLayout();
            this.gbZoomSetting.ResumeLayout(false);
            this.gbZoomSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTabWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWhiteSpaceSize)).EndInit();
            this.gbTabSymbol.ResumeLayout(false);
            this.gbTabSymbol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabEditorFont.ResumeLayout(false);
            this.tabEditorFont.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            this.tpgColorSettings.ResumeLayout(false);
            this.tpgColorSettings.PerformLayout();
            this.tabAdditionalColors.ResumeLayout(false);
            this.tabAdditionalColors.PerformLayout();
            this.gbUseBraceMatching.ResumeLayout(false);
            this.gbUseBraceMatching.PerformLayout();
            this.gbBraceFontStyle.ResumeLayout(false);
            this.gbBraceFontStyle.PerformLayout();
            this.tabSpellCheck.ResumeLayout(false);
            this.tabSpellCheck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditorSpellRecheckInactivity)).EndInit();
            this.tabDateAndTime.ResumeLayout(false);
            this.tabDateAndTime.PerformLayout();
            this.gbDate.ResumeLayout(false);
            this.gbDate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpgGeneralSettings;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.NumericUpDown nudHistoryDocuments;
        private System.Windows.Forms.Label lbHistoryDocuments;
        private System.Windows.Forms.CheckBox cbDocumentContentHistory;
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
        private System.Windows.Forms.TabPage tabEditorSettings;
        private System.Windows.Forms.GroupBox gbTabSymbol;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton rbTabSymbolStrikeout;
        private System.Windows.Forms.RadioButton rbTabSymbolArrow;
        private System.Windows.Forms.CheckBox cbUseTabs;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.NumericUpDown nudWhiteSpaceSize;
        private System.Windows.Forms.Label lbWhiteSpaceSize;
        private System.Windows.Forms.CheckBox cbIndentGuideOn;
        private System.Windows.Forms.TabPage tabSpellCheck;
        private System.Windows.Forms.Button btSpellCheckMarkColor;
        private System.Windows.Forms.Label lbSpellCheckMarkColor;
        private System.Windows.Forms.Button btHunspellAffixFile;
        private System.Windows.Forms.TextBox tbHunspellAffixFile;
        private System.Windows.Forms.Label lbHunspellAffixFile;
        private System.Windows.Forms.Button btHunspellDictionary;
        private System.Windows.Forms.TextBox tbHunspellDictionary;
        private System.Windows.Forms.Label lbHunspellDictionary;
        private System.Windows.Forms.CheckBox cbSpellCheckInUse;
        private System.Windows.Forms.OpenFileDialog odDictionaryFile;
        private System.Windows.Forms.OpenFileDialog odAffixFile;
        private System.Windows.Forms.Label lbEditorSpellRecheckInactivity;
        private System.Windows.Forms.NumericUpDown nudEditorSpellRecheckInactivity;
        private System.Windows.Forms.FontDialog fdEditorFont;
        private System.Windows.Forms.TabPage tabEditorFont;
        private System.Windows.Forms.Label lbFontSample;
        private ScintillaNET.Scintilla scintilla;
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private System.Windows.Forms.Label lbFontSize;
        private System.Windows.Forms.ComboBox cmbFont;
        private System.Windows.Forms.Label lbFont;
        private System.Windows.Forms.CheckBox cbCategorizeProgrammingLanguages;
        private System.Windows.Forms.Button btDictionaryPath;
        private System.Windows.Forms.TextBox tbDictionaryPath;
        private System.Windows.Forms.Label lbDictionaryPath;
        private System.Windows.Forms.ComboBox cmbInstalledDictionaries;
        private System.Windows.Forms.Label lbInstalledDictionaries;
        private System.Windows.Forms.Button btNotepadPlusPlusThemePath;
        private System.Windows.Forms.TextBox tbNotepadPlusPlusThemePath;
        private System.Windows.Forms.Label lbNotepadPlusPlusThemePath;
        private System.Windows.Forms.CheckBox cbUseNotepadPlusPlusTheme;
        private System.Windows.Forms.ComboBox cmbNotepadPlusPlusTheme;
        private System.Windows.Forms.ComboBox cmbSimulateKeyboard;
        private System.Windows.Forms.CheckBox cbSimulateKeyboard;
        private System.Windows.Forms.Label lbTabWidth;
        private System.Windows.Forms.NumericUpDown nudTabWidth;
        private System.Windows.Forms.CheckBox cbUseCodeIndentation;
        private System.Windows.Forms.GroupBox gbZoomSetting;
        private System.Windows.Forms.CheckBox cbSaveDocumentZoom;
        private System.Windows.Forms.CheckBox cbIndividualZoom;
        private System.Windows.Forms.TabPage tpgAdditionalSettings;
        private System.Windows.Forms.NumericUpDown nudAutoSaveInterval;
        private System.Windows.Forms.CheckBox cbUseAutoSave;
        private System.Windows.Forms.TextBox tbNoteAutoSave;
        private System.Windows.Forms.TabPage tabEncoding;
        private System.Windows.Forms.CheckBox cbEncodingAutoDetect;
        private System.Windows.Forms.GroupBox gpDefaultEncoding;
        private System.Windows.Forms.PictureBox btUTF8;
        private System.Windows.Forms.PictureBox btSystemDefaultEncoding;
        private System.Windows.Forms.ComboBox cmbCharacterSet;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.Label lbCharacterSet;
        private System.Windows.Forms.Label lbEncoding;
        private System.Windows.Forms.CheckBox cbDetectNoBomUnicode;
        private System.Windows.Forms.CheckBox cbUseRTL;
        private System.Windows.Forms.TabPage tabAdditionalColors;
        private System.Windows.Forms.Button btBadBraceColor;
        private System.Windows.Forms.Label lbBadBraceColor;
        private System.Windows.Forms.Button btBraceHighlightBackgroundColor;
        private System.Windows.Forms.Label lbBraceHighlightBackgroundColor;
        private System.Windows.Forms.Button btBraceHighlightForegroundColor;
        private System.Windows.Forms.Label lbBraceHighlightForegroundColor;
        private System.Windows.Forms.TextBox tbRestartNote;
        private System.Windows.Forms.CheckBox cbUseBraceMatching;
        private System.Windows.Forms.GroupBox gbUseBraceMatching;
        private System.Windows.Forms.GroupBox gbBraceFontStyle;
        private System.Windows.Forms.RadioButton rbBraceStyleBold;
        private System.Windows.Forms.RadioButton rbBraceStyleItalic;
        private System.Windows.Forms.CheckBox cbSetThreadLocale;
        private System.Windows.Forms.NumericUpDown nudDocumentContentHistory;
        private System.Windows.Forms.TabPage tabDateAndTime;
        private System.Windows.Forms.GroupBox gbDate;
        private System.Windows.Forms.TextBox tbDateTimeFormat1;
        private System.Windows.Forms.Label lbDateStyle;
        private System.Windows.Forms.Label lbDateTimeFormat1;
        private System.Windows.Forms.CheckBox cbDateTimeUseInvarianCulture;
        private System.Windows.Forms.Label lbDateTimeFormatDescriptionValue;
        private System.Windows.Forms.Label lbDateTimeFormatDescription;
        private System.Windows.Forms.Label lbDateTimeFormat6;
        private System.Windows.Forms.TextBox tbDateTimeFormat6;
        private System.Windows.Forms.Label lbDateTimeFormat5;
        private System.Windows.Forms.TextBox tbDateTimeFormat5;
        private System.Windows.Forms.Label lbDateTimeFormat4;
        private System.Windows.Forms.TextBox tbDateTimeFormat4;
        private System.Windows.Forms.Label lbDateTimeFormat3;
        private System.Windows.Forms.TextBox tbDateTimeFormat3;
        private System.Windows.Forms.Label lbDateTimeFormat2;
        private System.Windows.Forms.TextBox tbDateTimeFormat2;
        private System.Windows.Forms.Label lbDateTimeInstructionLink;
        private System.Windows.Forms.Button btDateTimeDefaults;
    }
}