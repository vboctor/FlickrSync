namespace FlickrSync
{
    partial class Preferences
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxOrderType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxShowThumbnailImages = new System.Windows.Forms.CheckBox();
            this.comboBoxMethod = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxNoDeleteTags = new System.Windows.Forms.CheckBox();
            this.checkBoxNoDelete = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxProxyPass = new System.Windows.Forms.MaskedTextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxProxyUser = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.textBoxProxyPort = new System.Windows.Forms.TextBox();
            this.textBoxProxyHost = new System.Windows.Forms.TextBox();
            this.checkBoxUseProxy = new System.Windows.Forms.CheckBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonLogFile = new System.Windows.Forms.Button();
            this.labelLogFile = new System.Windows.Forms.Label();
            this.comboBoxLogLevel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxMsgLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxOrderType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.checkBoxShowThumbnailImages);
            this.groupBox1.Controls.Add(this.comboBoxMethod);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.checkBoxNoDeleteTags);
            this.groupBox1.Controls.Add(this.checkBoxNoDelete);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Default Synchronization Properties";
            // 
            // comboBoxOrderType
            // 
            this.comboBoxOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrderType.FormattingEnabled = true;
            this.comboBoxOrderType.Items.AddRange(new object[] {
            "Default Order (Order By Local Date Modified)",
            "Order By Date Taken after each Sync",
            "Order By Title after each Sync",
            "Order By tag FlickrSync:Order after each Sync"});
            this.comboBoxOrderType.Location = new System.Drawing.Point(64, 54);
            this.comboBoxOrderType.Name = "comboBoxOrderType";
            this.comboBoxOrderType.Size = new System.Drawing.Size(341, 21);
            this.comboBoxOrderType.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Order:";
            // 
            // checkBoxShowThumbnailImages
            // 
            this.checkBoxShowThumbnailImages.AutoSize = true;
            this.checkBoxShowThumbnailImages.Location = new System.Drawing.Point(220, 85);
            this.checkBoxShowThumbnailImages.Name = "checkBoxShowThumbnailImages";
            this.checkBoxShowThumbnailImages.Size = new System.Drawing.Size(170, 17);
            this.checkBoxShowThumbnailImages.TabIndex = 3;
            this.checkBoxShowThumbnailImages.Text = "Show Images in Sync Window";
            this.checkBoxShowThumbnailImages.UseVisualStyleBackColor = true;
            // 
            // comboBoxMethod
            // 
            this.comboBoxMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMethod.FormattingEnabled = true;
            this.comboBoxMethod.Items.AddRange(new object[] {
            "Match local filename (no ext.) with flickr title",
            "Match by date taken",
            "Match image title or filename (no ext.) with flickr title"});
            this.comboBoxMethod.Location = new System.Drawing.Point(64, 24);
            this.comboBoxMethod.Name = "comboBoxMethod";
            this.comboBoxMethod.Size = new System.Drawing.Size(341, 21);
            this.comboBoxMethod.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Method:";
            // 
            // checkBoxNoDeleteTags
            // 
            this.checkBoxNoDeleteTags.AutoSize = true;
            this.checkBoxNoDeleteTags.Location = new System.Drawing.Point(12, 108);
            this.checkBoxNoDeleteTags.Name = "checkBoxNoDeleteTags";
            this.checkBoxNoDeleteTags.Size = new System.Drawing.Size(160, 17);
            this.checkBoxNoDeleteTags.TabIndex = 4;
            this.checkBoxNoDeleteTags.Text = "Never Delete tags from flickr";
            this.checkBoxNoDeleteTags.UseVisualStyleBackColor = true;
            // 
            // checkBoxNoDelete
            // 
            this.checkBoxNoDelete.AutoSize = true;
            this.checkBoxNoDelete.Location = new System.Drawing.Point(12, 85);
            this.checkBoxNoDelete.Name = "checkBoxNoDelete";
            this.checkBoxNoDelete.Size = new System.Drawing.Size(173, 17);
            this.checkBoxNoDelete.TabIndex = 2;
            this.checkBoxNoDelete.Text = "Never Delete images from flickr";
            this.checkBoxNoDelete.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(257, 426);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Location = new System.Drawing.Point(118, 426);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxProxyPass);
            this.groupBox3.Controls.Add(this.labelPassword);
            this.groupBox3.Controls.Add(this.textBoxProxyUser);
            this.groupBox3.Controls.Add(this.labelUser);
            this.groupBox3.Controls.Add(this.textBoxProxyPort);
            this.groupBox3.Controls.Add(this.textBoxProxyHost);
            this.groupBox3.Controls.Add(this.checkBoxUseProxy);
            this.groupBox3.Controls.Add(this.labelPort);
            this.groupBox3.Location = new System.Drawing.Point(13, 161);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(423, 129);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proxy Configuration";
            // 
            // textBoxProxyPass
            // 
            this.textBoxProxyPass.Location = new System.Drawing.Point(219, 90);
            this.textBoxProxyPass.Name = "textBoxProxyPass";
            this.textBoxProxyPass.PasswordChar = '*';
            this.textBoxProxyPass.Size = new System.Drawing.Size(185, 20);
            this.textBoxProxyPass.TabIndex = 4;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(8, 93);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(205, 13);
            this.labelPassword.TabIndex = 31;
            this.labelPassword.Text = "Password (Leave Blank to ask everytime):";
            // 
            // textBoxProxyUser
            // 
            this.textBoxProxyUser.Location = new System.Drawing.Point(164, 57);
            this.textBoxProxyUser.Name = "textBoxProxyUser";
            this.textBoxProxyUser.Size = new System.Drawing.Size(240, 20);
            this.textBoxProxyUser.TabIndex = 3;
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(8, 60);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(150, 13);
            this.labelUser.TabIndex = 29;
            this.labelUser.Text = "Authentication (Domain\\User):";
            // 
            // textBoxProxyPort
            // 
            this.textBoxProxyPort.Location = new System.Drawing.Point(335, 23);
            this.textBoxProxyPort.Name = "textBoxProxyPort";
            this.textBoxProxyPort.Size = new System.Drawing.Size(69, 20);
            this.textBoxProxyPort.TabIndex = 2;
            // 
            // textBoxProxyHost
            // 
            this.textBoxProxyHost.Location = new System.Drawing.Point(91, 23);
            this.textBoxProxyHost.Name = "textBoxProxyHost";
            this.textBoxProxyHost.Size = new System.Drawing.Size(189, 20);
            this.textBoxProxyHost.TabIndex = 1;
            // 
            // checkBoxUseProxy
            // 
            this.checkBoxUseProxy.AutoSize = true;
            this.checkBoxUseProxy.Location = new System.Drawing.Point(11, 25);
            this.checkBoxUseProxy.Name = "checkBoxUseProxy";
            this.checkBoxUseProxy.Size = new System.Drawing.Size(74, 17);
            this.checkBoxUseProxy.TabIndex = 0;
            this.checkBoxUseProxy.Text = "Use Proxy";
            this.checkBoxUseProxy.UseVisualStyleBackColor = true;
            this.checkBoxUseProxy.CheckedChanged += new System.EventHandler(this.checkBoxUseProxy_CheckedChanged);
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(300, 27);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 13);
            this.labelPort.TabIndex = 26;
            this.labelPort.Text = "Port:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonLogFile);
            this.groupBox2.Controls.Add(this.labelLogFile);
            this.groupBox2.Controls.Add(this.comboBoxLogLevel);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.comboBoxMsgLevel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(13, 297);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(422, 117);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information";
            // 
            // buttonLogFile
            // 
            this.buttonLogFile.Location = new System.Drawing.Point(11, 86);
            this.buttonLogFile.Name = "buttonLogFile";
            this.buttonLogFile.Size = new System.Drawing.Size(55, 21);
            this.buttonLogFile.TabIndex = 2;
            this.buttonLogFile.Text = "Log File";
            this.buttonLogFile.UseVisualStyleBackColor = true;
            this.buttonLogFile.Click += new System.EventHandler(this.buttonLogFile_Click);
            // 
            // labelLogFile
            // 
            this.labelLogFile.AutoEllipsis = true;
            this.labelLogFile.Location = new System.Drawing.Point(75, 90);
            this.labelLogFile.Name = "labelLogFile";
            this.labelLogFile.Size = new System.Drawing.Size(329, 13);
            this.labelLogFile.TabIndex = 26;
            this.labelLogFile.Visible = false;
            // 
            // comboBoxLogLevel
            // 
            this.comboBoxLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLogLevel.FormattingEnabled = true;
            this.comboBoxLogLevel.Items.AddRange(new object[] {
            "No log file information",
            "Log main messages to file",
            "Log detailed activity to file"});
            this.comboBoxLogLevel.Location = new System.Drawing.Point(75, 55);
            this.comboBoxLogLevel.Name = "comboBoxLogLevel";
            this.comboBoxLogLevel.Size = new System.Drawing.Size(329, 21);
            this.comboBoxLogLevel.TabIndex = 1;
            this.comboBoxLogLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogLevel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Logging:";
            // 
            // comboBoxMsgLevel
            // 
            this.comboBoxMsgLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMsgLevel.FormattingEnabled = true;
            this.comboBoxMsgLevel.Items.AddRange(new object[] {
            "Do not show any messages within application (not recommended)",
            "Show only main messages (no information messages during synchronization)",
            "Show all in-program messages"});
            this.comboBoxMsgLevel.Location = new System.Drawing.Point(75, 24);
            this.comboBoxMsgLevel.Name = "comboBoxMsgLevel";
            this.comboBoxMsgLevel.Size = new System.Drawing.Size(329, 21);
            this.comboBoxMsgLevel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Messages:";
            // 
            // Preferences
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(452, 457);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Preferences";
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.Preferences_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ComboBox comboBoxMethod;
        private System.Windows.Forms.CheckBox checkBoxNoDeleteTags;
        private System.Windows.Forms.CheckBox checkBoxNoDelete;
        private System.Windows.Forms.CheckBox checkBoxShowThumbnailImages;
        private System.Windows.Forms.TextBox textBoxProxyHost;
        private System.Windows.Forms.CheckBox checkBoxUseProxy;
        private System.Windows.Forms.TextBox textBoxProxyPort;
        private System.Windows.Forms.TextBox textBoxProxyUser;
        private System.Windows.Forms.MaskedTextBox textBoxProxyPass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelLogFile;
        private System.Windows.Forms.ComboBox comboBoxLogLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxMsgLevel;
        private System.Windows.Forms.Button buttonLogFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxOrderType;
    }
}