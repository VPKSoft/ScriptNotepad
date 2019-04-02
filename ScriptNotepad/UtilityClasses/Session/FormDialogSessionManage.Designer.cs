namespace ScriptNotepad.UtilityClasses.Session
{
    partial class FormDialogSessionManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogSessionManage));
            this.lbSelectSession = new System.Windows.Forms.Label();
            this.cmbSessions = new System.Windows.Forms.ComboBox();
            this.lbRenameSession = new System.Windows.Forms.Label();
            this.tbRenameSession = new System.Windows.Forms.TextBox();
            this.pbRenameSelectedSession = new System.Windows.Forms.PictureBox();
            this.pbDeleteSelectedSession = new System.Windows.Forms.PictureBox();
            this.btOK = new System.Windows.Forms.Button();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.pbAddNewSessionWithName = new System.Windows.Forms.PictureBox();
            this.tbAddNewSessionWithName = new System.Windows.Forms.TextBox();
            this.lbAddNewSessionWithName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbRenameSelectedSession)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDeleteSelectedSession)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNewSessionWithName)).BeginInit();
            this.SuspendLayout();
            // 
            // lbSelectSession
            // 
            this.lbSelectSession.AutoSize = true;
            this.lbSelectSession.Location = new System.Drawing.Point(12, 12);
            this.lbSelectSession.Name = "lbSelectSession";
            this.lbSelectSession.Size = new System.Drawing.Size(87, 13);
            this.lbSelectSession.TabIndex = 0;
            this.lbSelectSession.Text = "Select a session:";
            // 
            // cmbSessions
            // 
            this.cmbSessions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSessions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSessions.FormattingEnabled = true;
            this.cmbSessions.Location = new System.Drawing.Point(210, 9);
            this.cmbSessions.Name = "cmbSessions";
            this.cmbSessions.Size = new System.Drawing.Size(357, 21);
            this.cmbSessions.TabIndex = 1;
            // 
            // lbRenameSession
            // 
            this.lbRenameSession.AutoSize = true;
            this.lbRenameSession.Location = new System.Drawing.Point(12, 39);
            this.lbRenameSession.Name = "lbRenameSession";
            this.lbRenameSession.Size = new System.Drawing.Size(161, 13);
            this.lbRenameSession.TabIndex = 2;
            this.lbRenameSession.Text = "Rename the selected session to:";
            // 
            // tbRenameSession
            // 
            this.tbRenameSession.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRenameSession.Location = new System.Drawing.Point(210, 36);
            this.tbRenameSession.Name = "tbRenameSession";
            this.tbRenameSession.Size = new System.Drawing.Size(357, 20);
            this.tbRenameSession.TabIndex = 3;
            // 
            // pbRenameSelectedSession
            // 
            this.pbRenameSelectedSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbRenameSelectedSession.Image = ((System.Drawing.Image)(resources.GetObject("pbRenameSelectedSession.Image")));
            this.pbRenameSelectedSession.Location = new System.Drawing.Point(573, 36);
            this.pbRenameSelectedSession.Name = "pbRenameSelectedSession";
            this.pbRenameSelectedSession.Size = new System.Drawing.Size(21, 21);
            this.pbRenameSelectedSession.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbRenameSelectedSession.TabIndex = 12;
            this.pbRenameSelectedSession.TabStop = false;
            this.ttMain.SetToolTip(this.pbRenameSelectedSession, "Rename selected session");
            this.pbRenameSelectedSession.Click += new System.EventHandler(this.pbRenameSelectedSession_Click);
            // 
            // pbDeleteSelectedSession
            // 
            this.pbDeleteSelectedSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDeleteSelectedSession.Image = global::ScriptNotepad.Properties.Resources.Erase;
            this.pbDeleteSelectedSession.Location = new System.Drawing.Point(573, 9);
            this.pbDeleteSelectedSession.Name = "pbDeleteSelectedSession";
            this.pbDeleteSelectedSession.Size = new System.Drawing.Size(21, 21);
            this.pbDeleteSelectedSession.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbDeleteSelectedSession.TabIndex = 13;
            this.pbDeleteSelectedSession.TabStop = false;
            this.ttMain.SetToolTip(this.pbDeleteSelectedSession, "Delete selected session");
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btOK.Location = new System.Drawing.Point(520, 92);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 14;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // pbAddNewSessionWithName
            // 
            this.pbAddNewSessionWithName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbAddNewSessionWithName.Image = global::ScriptNotepad.Properties.Resources.new_document;
            this.pbAddNewSessionWithName.Location = new System.Drawing.Point(573, 62);
            this.pbAddNewSessionWithName.Name = "pbAddNewSessionWithName";
            this.pbAddNewSessionWithName.Size = new System.Drawing.Size(21, 21);
            this.pbAddNewSessionWithName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbAddNewSessionWithName.TabIndex = 17;
            this.pbAddNewSessionWithName.TabStop = false;
            this.ttMain.SetToolTip(this.pbAddNewSessionWithName, "Add a new named session");
            this.pbAddNewSessionWithName.Click += new System.EventHandler(this.pbAddNewSessionWithName_Click);
            // 
            // tbAddNewSessionWithName
            // 
            this.tbAddNewSessionWithName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAddNewSessionWithName.Location = new System.Drawing.Point(210, 62);
            this.tbAddNewSessionWithName.Name = "tbAddNewSessionWithName";
            this.tbAddNewSessionWithName.Size = new System.Drawing.Size(357, 20);
            this.tbAddNewSessionWithName.TabIndex = 16;
            // 
            // lbAddNewSessionWithName
            // 
            this.lbAddNewSessionWithName.AutoSize = true;
            this.lbAddNewSessionWithName.Location = new System.Drawing.Point(12, 65);
            this.lbAddNewSessionWithName.Name = "lbAddNewSessionWithName";
            this.lbAddNewSessionWithName.Size = new System.Drawing.Size(134, 13);
            this.lbAddNewSessionWithName.TabIndex = 15;
            this.lbAddNewSessionWithName.Text = "Add a new session named:";
            // 
            // FormDialogSessionManage
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btOK;
            this.ClientSize = new System.Drawing.Size(607, 125);
            this.Controls.Add(this.pbAddNewSessionWithName);
            this.Controls.Add(this.tbAddNewSessionWithName);
            this.Controls.Add(this.lbAddNewSessionWithName);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.pbDeleteSelectedSession);
            this.Controls.Add(this.pbRenameSelectedSession);
            this.Controls.Add(this.tbRenameSession);
            this.Controls.Add(this.lbRenameSession);
            this.Controls.Add(this.cmbSessions);
            this.Controls.Add(this.lbSelectSession);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogSessionManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage sessions";
            this.Shown += new System.EventHandler(this.FormDialogSessionManage_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbRenameSelectedSession)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDeleteSelectedSession)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNewSessionWithName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSelectSession;
        private System.Windows.Forms.ComboBox cmbSessions;
        private System.Windows.Forms.Label lbRenameSession;
        private System.Windows.Forms.TextBox tbRenameSession;
        private System.Windows.Forms.PictureBox pbRenameSelectedSession;
        private System.Windows.Forms.PictureBox pbDeleteSelectedSession;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.ToolTip ttMain;
        private System.Windows.Forms.TextBox tbAddNewSessionWithName;
        private System.Windows.Forms.Label lbAddNewSessionWithName;
        private System.Windows.Forms.PictureBox pbAddNewSessionWithName;
    }
}