namespace ScriptNotepad.DialogForms
{
    partial class FormDialogQueryJumpLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogQueryJumpLocation));
            this.lbEnterLineNumber = new System.Windows.Forms.Label();
            this.nudGoto = new System.Windows.Forms.NumericUpDown();
            this.btGo = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudGoto)).BeginInit();
            this.SuspendLayout();
            // 
            // lbEnterLineNumber
            // 
            this.lbEnterLineNumber.AutoSize = true;
            this.lbEnterLineNumber.Location = new System.Drawing.Point(12, 9);
            this.lbEnterLineNumber.Name = "lbEnterLineNumber";
            this.lbEnterLineNumber.Size = new System.Drawing.Size(131, 13);
            this.lbEnterLineNumber.TabIndex = 0;
            this.lbEnterLineNumber.Text = "Enter a line number (0 - 0):";
            // 
            // nudGoto
            // 
            this.nudGoto.Location = new System.Drawing.Point(12, 25);
            this.nudGoto.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudGoto.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGoto.Name = "nudGoto";
            this.nudGoto.Size = new System.Drawing.Size(120, 20);
            this.nudGoto.TabIndex = 1;
            this.nudGoto.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btGo
            // 
            this.btGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btGo.Location = new System.Drawing.Point(213, 25);
            this.btGo.Name = "btGo";
            this.btGo.Size = new System.Drawing.Size(75, 20);
            this.btGo.TabIndex = 2;
            this.btGo.Text = "Go";
            this.btGo.UseVisualStyleBackColor = true;
            this.btGo.Click += new System.EventHandler(this.BtGo_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(146, 11);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(0, 0);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "button1";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // FormDialogQueryJumpLocation
            // 
            this.AcceptButton = this.btGo;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(300, 57);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btGo);
            this.Controls.Add(this.nudGoto);
            this.Controls.Add(this.lbEnterLineNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDialogQueryJumpLocation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Goto";
            this.Shown += new System.EventHandler(this.FormDialogQueryJumpLocation_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.nudGoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbEnterLineNumber;
        private System.Windows.Forms.NumericUpDown nudGoto;
        private System.Windows.Forms.Button btGo;
        private System.Windows.Forms.Button btCancel;
    }
}