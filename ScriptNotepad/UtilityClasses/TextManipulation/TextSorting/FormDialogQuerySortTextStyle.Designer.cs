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
            this.listCheckSortStyles = new System.Windows.Forms.CheckedListBox();
            this.btClose = new System.Windows.Forms.Button();
            this.btSort = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbCheckTooltip = new System.Windows.Forms.Label();
            this.btUndo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.tableLayoutPanel1.SetRowSpan(this.listSortStylesAvailable, 2);
            this.listSortStylesAvailable.Size = new System.Drawing.Size(304, 305);
            this.listSortStylesAvailable.TabIndex = 6;
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
            this.pictureBox2.Location = new System.Drawing.Point(313, 314);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormDialogQuerySortTextStyle_MouseUp);
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
            this.tableLayoutPanel1.SetRowSpan(this.listCheckSortStyles, 2);
            this.listCheckSortStyles.Size = new System.Drawing.Size(304, 305);
            this.listCheckSortStyles.TabIndex = 10;
            this.listCheckSortStyles.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listCheckSortStyles_ItemCheck);
            this.listCheckSortStyles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listCheckSortStyles_DragDrop);
            this.listCheckSortStyles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listCheckSortStyles_DragEnterOver);
            this.listCheckSortStyles.DragOver += new System.Windows.Forms.DragEventHandler(this.listCheckSortStyles_DragEnterOver);
            this.listCheckSortStyles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listCheckSortStyles_KeyDown);
            this.listCheckSortStyles.Leave += new System.EventHandler(this.listCheckSortStyles_Leave);
            this.listCheckSortStyles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listCheckSortStyles_MouseDown);
            this.listCheckSortStyles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listCheckSortStyles_MouseMove);
            this.listCheckSortStyles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormDialogQuerySortTextStyle_MouseUp);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lbSortStyles, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbSortStylesAvailable, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.listCheckSortStyles, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listSortStylesAvailable, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(642, 333);
            this.tableLayoutPanel1.TabIndex = 14;
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
            this.Controls.Add(this.tableLayoutPanel1);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbSortStyles;
        private System.Windows.Forms.ListBox listSortStylesAvailable;
        private System.Windows.Forms.Label lbSortStylesAvailable;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckedListBox listCheckSortStyles;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSort;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbCheckTooltip;
        private System.Windows.Forms.Button btUndo;
    }
}