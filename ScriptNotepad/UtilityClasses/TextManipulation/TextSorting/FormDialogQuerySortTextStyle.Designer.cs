namespace ScriptNotepad.UtilityClasses.TextManipulation.TextSorting
{
    partial class FormDialogQuerySortTextStyle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogQuerySortTextStyle));
            this.lbSortStyles = new System.Windows.Forms.Label();
            this.listSortStylesAvailable = new System.Windows.Forms.ListBox();
            this.lbSortStylesAvailable = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btClose = new System.Windows.Forms.Button();
            this.btSort = new System.Windows.Forms.Button();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpSubStringRange = new System.Windows.Forms.TableLayoutPanel();
            this.lbSubStringRange = new System.Windows.Forms.Label();
            this.lbSpan1 = new System.Windows.Forms.Label();
            this.nudSubStringRange1 = new System.Windows.Forms.NumericUpDown();
            this.nudSubStringRange2 = new System.Windows.Forms.NumericUpDown();
            this.tlpRegex = new System.Windows.Forms.TableLayoutPanel();
            this.lbRegex = new System.Windows.Forms.Label();
            this.tbRegex = new System.Windows.Forms.ComboBox();
            this.lbCheckTooltip = new System.Windows.Forms.Label();
            this.btUndo = new System.Windows.Forms.Button();
            this.listCheckSortStyles = new ScriptNotepad.UtilityClasses.TextManipulation.TextSorting.RefreshCheckListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.tlpSubStringRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubStringRange1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubStringRange2)).BeginInit();
            this.tlpRegex.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbSortStyles
            // 
            this.lbSortStyles.AllowDrop = true;
            this.lbSortStyles.AutoSize = true;
            this.lbSortStyles.Location = new System.Drawing.Point(3, 0);
            this.lbSortStyles.Name = "lbSortStyles";
            this.lbSortStyles.Size = new System.Drawing.Size(122, 13);
            this.lbSortStyles.TabIndex = 5;
            this.lbSortStyles.Text = "Sort styles (Drag && Drop)";
            this.lbSortStyles.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.lbSortStyles.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.lbSortStyles.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.lbSortStyles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormDialogQuerySortTextStyle_MouseUp);
            // 
            // listSortStylesAvailable
            // 
            this.listSortStylesAvailable.AllowDrop = true;
            this.listSortStylesAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSortStylesAvailable.FormattingEnabled = true;
            this.listSortStylesAvailable.Location = new System.Drawing.Point(335, 25);
            this.listSortStylesAvailable.Name = "listSortStylesAvailable";
            this.tlpMain.SetRowSpan(this.listSortStylesAvailable, 2);
            this.listSortStylesAvailable.Size = new System.Drawing.Size(304, 252);
            this.listSortStylesAvailable.TabIndex = 6;
            this.listSortStylesAvailable.SelectedValueChanged += new System.EventHandler(this.listSortStylesAvailable_SelectedValueChanged);
            this.listSortStylesAvailable.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.listSortStylesAvailable.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.listSortStylesAvailable.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.listSortStylesAvailable.DoubleClick += new System.EventHandler(this.listSortStylesAvailable_DoubleClick);
            this.listSortStylesAvailable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listSortStylesAvailable_KeyDown);
            this.listSortStylesAvailable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listSortStylesAvailable_MouseDown);
            this.listSortStylesAvailable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listSortStylesAvailable_MouseMove);
            this.listSortStylesAvailable.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormDialogQuerySortTextStyle_MouseUp);
            // 
            // lbSortStylesAvailable
            // 
            this.lbSortStylesAvailable.AllowDrop = true;
            this.lbSortStylesAvailable.AutoSize = true;
            this.lbSortStylesAvailable.Location = new System.Drawing.Point(335, 0);
            this.lbSortStylesAvailable.Name = "lbSortStylesAvailable";
            this.lbSortStylesAvailable.Size = new System.Drawing.Size(166, 13);
            this.lbSortStylesAvailable.TabIndex = 7;
            this.lbSortStylesAvailable.Text = "Available sort styles (Drag && Drop)";
            this.lbSortStylesAvailable.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.lbSortStylesAvailable.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.lbSortStylesAvailable.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.lbSortStylesAvailable.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormDialogQuerySortTextStyle_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ScriptNotepad.Properties.Resources.Fast_rewind;
            this.pictureBox1.Location = new System.Drawing.Point(313, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.pictureBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.pictureBox1.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormDialogQuerySortTextStyle_MouseUp);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ScriptNotepad.Properties.Resources.Fast_rewind;
            this.pictureBox2.Location = new System.Drawing.Point(313, 261);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormDialogQuerySortTextStyle_MouseUp);
            // 
            // btClose
            // 
            this.btClose.AllowDrop = true;
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btClose.Location = new System.Drawing.Point(579, 351);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 13;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.btClose.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.btClose.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            // 
            // btSort
            // 
            this.btSort.AllowDrop = true;
            this.btSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSort.Location = new System.Drawing.Point(12, 351);
            this.btSort.Name = "btSort";
            this.btSort.Size = new System.Drawing.Size(75, 23);
            this.btSort.TabIndex = 12;
            this.btSort.Text = "Sort";
            this.btSort.UseVisualStyleBackColor = true;
            this.btSort.Click += new System.EventHandler(this.btSort_Click);
            this.btSort.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.btSort.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.btSort.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            // 
            // tlpMain
            // 
            this.tlpMain.AllowDrop = true;
            this.tlpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.lbSortStyles, 0, 0);
            this.tlpMain.Controls.Add(this.pictureBox1, 1, 0);
            this.tlpMain.Controls.Add(this.lbSortStylesAvailable, 2, 0);
            this.tlpMain.Controls.Add(this.pictureBox2, 1, 2);
            this.tlpMain.Controls.Add(this.listCheckSortStyles, 0, 1);
            this.tlpMain.Controls.Add(this.listSortStylesAvailable, 2, 1);
            this.tlpMain.Controls.Add(this.tlpSubStringRange, 0, 4);
            this.tlpMain.Controls.Add(this.tlpRegex, 0, 3);
            this.tlpMain.Location = new System.Drawing.Point(12, 12);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(642, 333);
            this.tlpMain.TabIndex = 14;
            this.tlpMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.tlpMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.tlpMain.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            // 
            // tlpSubStringRange
            // 
            this.tlpSubStringRange.AllowDrop = true;
            this.tlpSubStringRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpSubStringRange.AutoSize = true;
            this.tlpSubStringRange.ColumnCount = 4;
            this.tlpMain.SetColumnSpan(this.tlpSubStringRange, 3);
            this.tlpSubStringRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSubStringRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSubStringRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSubStringRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSubStringRange.Controls.Add(this.lbSubStringRange, 0, 0);
            this.tlpSubStringRange.Controls.Add(this.lbSpan1, 2, 0);
            this.tlpSubStringRange.Controls.Add(this.nudSubStringRange1, 1, 0);
            this.tlpSubStringRange.Controls.Add(this.nudSubStringRange2, 3, 0);
            this.tlpSubStringRange.Location = new System.Drawing.Point(0, 307);
            this.tlpSubStringRange.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSubStringRange.Name = "tlpSubStringRange";
            this.tlpSubStringRange.RowCount = 1;
            this.tlpSubStringRange.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSubStringRange.Size = new System.Drawing.Size(642, 26);
            this.tlpSubStringRange.TabIndex = 11;
            this.tlpSubStringRange.Visible = false;
            this.tlpSubStringRange.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.tlpSubStringRange.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.tlpSubStringRange.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            // 
            // lbSubStringRange
            // 
            this.lbSubStringRange.AllowDrop = true;
            this.lbSubStringRange.AutoSize = true;
            this.lbSubStringRange.Location = new System.Drawing.Point(3, 0);
            this.lbSubStringRange.Name = "lbSubStringRange";
            this.lbSubStringRange.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.lbSubStringRange.Size = new System.Drawing.Size(90, 19);
            this.lbSubStringRange.TabIndex = 0;
            this.lbSubStringRange.Text = "Substring range:";
            this.lbSubStringRange.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.lbSubStringRange.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.lbSubStringRange.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            // 
            // lbSpan1
            // 
            this.lbSpan1.AllowDrop = true;
            this.lbSpan1.AutoSize = true;
            this.lbSpan1.Location = new System.Drawing.Point(546, 0);
            this.lbSpan1.Name = "lbSpan1";
            this.lbSpan1.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.lbSpan1.Size = new System.Drawing.Size(16, 19);
            this.lbSpan1.TabIndex = 2;
            this.lbSpan1.Text = "-";
            this.lbSpan1.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.lbSpan1.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.lbSpan1.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            // 
            // nudSubStringRange1
            // 
            this.nudSubStringRange1.AllowDrop = true;
            this.nudSubStringRange1.Location = new System.Drawing.Point(467, 3);
            this.nudSubStringRange1.Name = "nudSubStringRange1";
            this.nudSubStringRange1.Size = new System.Drawing.Size(73, 20);
            this.nudSubStringRange1.TabIndex = 3;
            this.nudSubStringRange1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSubStringRange1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSubStringRange1.ValueChanged += new System.EventHandler(this.nudSubStringRange2_ValueChanged);
            this.nudSubStringRange1.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.nudSubStringRange1.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.nudSubStringRange1.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            // 
            // nudSubStringRange2
            // 
            this.nudSubStringRange2.AllowDrop = true;
            this.nudSubStringRange2.Location = new System.Drawing.Point(568, 3);
            this.nudSubStringRange2.Name = "nudSubStringRange2";
            this.nudSubStringRange2.Size = new System.Drawing.Size(71, 20);
            this.nudSubStringRange2.TabIndex = 1;
            this.nudSubStringRange2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSubStringRange2.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudSubStringRange2.ValueChanged += new System.EventHandler(this.nudSubStringRange2_ValueChanged);
            this.nudSubStringRange2.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.nudSubStringRange2.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.nudSubStringRange2.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            // 
            // tlpRegex
            // 
            this.tlpRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpRegex.AutoSize = true;
            this.tlpRegex.ColumnCount = 2;
            this.tlpMain.SetColumnSpan(this.tlpRegex, 3);
            this.tlpRegex.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRegex.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRegex.Controls.Add(this.lbRegex, 0, 0);
            this.tlpRegex.Controls.Add(this.tbRegex, 1, 0);
            this.tlpRegex.Location = new System.Drawing.Point(0, 280);
            this.tlpRegex.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRegex.Name = "tlpRegex";
            this.tlpRegex.RowCount = 1;
            this.tlpRegex.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRegex.Size = new System.Drawing.Size(642, 27);
            this.tlpRegex.TabIndex = 12;
            this.tlpRegex.Visible = false;
            // 
            // lbRegex
            // 
            this.lbRegex.AutoSize = true;
            this.lbRegex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbRegex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRegex.Location = new System.Drawing.Point(3, 0);
            this.lbRegex.Name = "lbRegex";
            this.lbRegex.Padding = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.lbRegex.Size = new System.Drawing.Size(106, 19);
            this.lbRegex.TabIndex = 0;
            this.lbRegex.Text = "Regular expression:";
            this.lbRegex.Click += new System.EventHandler(this.lbRegex_Click);
            // 
            // tbRegex
            // 
            this.tbRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRegex.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbRegex.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tbRegex.FormattingEnabled = true;
            this.tbRegex.Location = new System.Drawing.Point(115, 3);
            this.tbRegex.Name = "tbRegex";
            this.tbRegex.Size = new System.Drawing.Size(524, 21);
            this.tbRegex.TabIndex = 1;
            this.tbRegex.TextChanged += new System.EventHandler(this.tbRegex_TextChanged);
            // 
            // lbCheckTooltip
            // 
            this.lbCheckTooltip.AllowDrop = true;
            this.lbCheckTooltip.AutoSize = true;
            this.lbCheckTooltip.Location = new System.Drawing.Point(102, 356);
            this.lbCheckTooltip.Name = "lbCheckTooltip";
            this.lbCheckTooltip.Size = new System.Drawing.Size(208, 13);
            this.lbCheckTooltip.TabIndex = 15;
            this.lbCheckTooltip.Text = "Check the style to sort in descending order";
            this.lbCheckTooltip.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.lbCheckTooltip.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.lbCheckTooltip.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            // 
            // btUndo
            // 
            this.btUndo.AllowDrop = true;
            this.btUndo.Image = global::ScriptNotepad.Properties.Resources.Undo;
            this.btUndo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btUndo.Location = new System.Drawing.Point(461, 351);
            this.btUndo.Name = "btUndo";
            this.btUndo.Size = new System.Drawing.Size(112, 23);
            this.btUndo.TabIndex = 16;
            this.btUndo.Text = "Undo";
            this.btUndo.UseVisualStyleBackColor = true;
            this.btUndo.Click += new System.EventHandler(this.btUndo_Click);
            this.btUndo.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.btUndo.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.btUndo.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            // 
            // listCheckSortStyles
            // 
            this.listCheckSortStyles.AllowDrop = true;
            this.listCheckSortStyles.CheckOnClick = true;
            this.listCheckSortStyles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCheckSortStyles.FormattingEnabled = true;
            this.listCheckSortStyles.IntegralHeight = false;
            this.listCheckSortStyles.Location = new System.Drawing.Point(3, 25);
            this.listCheckSortStyles.Name = "listCheckSortStyles";
            this.tlpMain.SetRowSpan(this.listCheckSortStyles, 2);
            this.listCheckSortStyles.Size = new System.Drawing.Size(304, 252);
            this.listCheckSortStyles.TabIndex = 10;
            this.listCheckSortStyles.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listCheckSortStyles_ItemCheck);
            this.listCheckSortStyles.SelectedValueChanged += new System.EventHandler(this.listSortStylesAvailable_SelectedValueChanged);
            this.listCheckSortStyles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listCheckSortStyles_DragDrop);
            this.listCheckSortStyles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listCheckSortStyles_DragEnterOver);
            this.listCheckSortStyles.DragOver += new System.Windows.Forms.DragEventHandler(this.listCheckSortStyles_DragEnterOver);
            this.listCheckSortStyles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listCheckSortStyles_KeyDown);
            this.listCheckSortStyles.Leave += new System.EventHandler(this.listCheckSortStyles_Leave);
            this.listCheckSortStyles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listCheckSortStyles_MouseDown);
            this.listCheckSortStyles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listCheckSortStyles_MouseMove);
            this.listCheckSortStyles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormDialogQuerySortTextStyle_MouseUp);
            // 
            // FormDialogQuerySortTextStyle
            // 
            this.AcceptButton = this.btSort;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btClose;
            this.ClientSize = new System.Drawing.Size(666, 386);
            this.Controls.Add(this.btUndo);
            this.Controls.Add(this.lbCheckTooltip);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btSort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogQuerySortTextStyle";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced text sorting";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDialogQuerySortTextStyle_DragEnterOver);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormDialogQuerySortTextStyle_KeyDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormDialogQuerySortTextStyle_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpSubStringRange.ResumeLayout(false);
            this.tlpSubStringRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubStringRange1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubStringRange2)).EndInit();
            this.tlpRegex.ResumeLayout(false);
            this.tlpRegex.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbSortStyles;
        private System.Windows.Forms.ListBox listSortStylesAvailable;
        private System.Windows.Forms.Label lbSortStylesAvailable;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private RefreshCheckListBox listCheckSortStyles;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSort;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbCheckTooltip;
        private System.Windows.Forms.Button btUndo;
        private System.Windows.Forms.TableLayoutPanel tlpSubStringRange;
        private System.Windows.Forms.Label lbSubStringRange;
        private System.Windows.Forms.NumericUpDown nudSubStringRange2;
        private System.Windows.Forms.Label lbSpan1;
        private System.Windows.Forms.NumericUpDown nudSubStringRange1;
        private System.Windows.Forms.TableLayoutPanel tlpRegex;
        private System.Windows.Forms.Label lbRegex;
        private System.Windows.Forms.ComboBox tbRegex;
    }
}