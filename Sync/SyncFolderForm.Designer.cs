namespace FlickrSync
{
    partial class SyncFolderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncFolderForm));
            this.listViewSet = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.labelFolderPath = new System.Windows.Forms.Label();
            this.labelSetTitle = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxMethod = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.textBoxTags = new System.Windows.Forms.TextBox();
            this.labelTags = new System.Windows.Forms.Label();
            this.buttonTagList = new System.Windows.Forms.Button();
            this.comboBoxPermissions = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxNoDelete = new System.Windows.Forms.CheckBox();
            this.checkBoxNoDeleteTags = new System.Windows.Forms.CheckBox();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonOKAll = new System.Windows.Forms.Button();
            this.buttonCancelAll = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxOrderType = new System.Windows.Forms.ComboBox();
            this.labelStarRating = new System.Windows.Forms.Label();
            this.pictureBoxStarRating5 = new System.Windows.Forms.PictureBox();
            this.pictureBoxStarRating4 = new System.Windows.Forms.PictureBox();
            this.pictureBoxStarRating3 = new System.Windows.Forms.PictureBox();
            this.pictureBoxStarRating2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxStarRating1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxStarRating0 = new System.Windows.Forms.PictureBox();
            this.checkBoxNoInitialReplace = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating0)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewSet
            // 
            this.listViewSet.HideSelection = false;
            this.listViewSet.LargeImageList = this.imageList1;
            this.listViewSet.Location = new System.Drawing.Point(12, 12);
            this.listViewSet.MultiSelect = false;
            this.listViewSet.Name = "listViewSet";
            this.listViewSet.Size = new System.Drawing.Size(399, 420);
            this.listViewSet.TabIndex = 0;
            this.listViewSet.UseCompatibleStateImageBehavior = false;
            this.listViewSet.SelectedIndexChanged += new System.EventHandler(this.listViewSet_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(75, 75);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(425, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Folder:";
            // 
            // labelFolderPath
            // 
            this.labelFolderPath.AutoSize = true;
            this.labelFolderPath.Location = new System.Drawing.Point(470, 12);
            this.labelFolderPath.Name = "labelFolderPath";
            this.labelFolderPath.Size = new System.Drawing.Size(30, 13);
            this.labelFolderPath.TabIndex = 1;
            this.labelFolderPath.Text = "c:\\...";
            this.labelFolderPath.UseMnemonic = false;
            // 
            // labelSetTitle
            // 
            this.labelSetTitle.AutoSize = true;
            this.labelSetTitle.Location = new System.Drawing.Point(470, 39);
            this.labelSetTitle.Name = "labelSetTitle";
            this.labelSetTitle.Size = new System.Drawing.Size(386, 13);
            this.labelSetTitle.TabIndex = 2;
            this.labelSetTitle.Text = "Choose one from the left or create a new one below (to be created on next sync)";
            this.labelSetTitle.UseMnemonic = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Title:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxDescription);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxTitle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(425, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 126);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Set   (type here to create a New Set)";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(73, 47);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(341, 73);
            this.textBoxDescription.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Description:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.HideSelection = false;
            this.textBoxTitle.Location = new System.Drawing.Point(73, 18);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(341, 20);
            this.textBoxTitle.TabIndex = 0;
            this.textBoxTitle.TextChanged += new System.EventHandler(this.textBoxTitle_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(431, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Method:";
            // 
            // comboBoxMethod
            // 
            this.comboBoxMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMethod.FormattingEnabled = true;
            this.comboBoxMethod.Items.AddRange(new object[] {
            "Match local filename (no ext.) with flickr title",
            "Match by date taken",
            "Match image title or filename (no ext.) with flickr title"});
            this.comboBoxMethod.Location = new System.Drawing.Point(498, 203);
            this.comboBoxMethod.Name = "comboBoxMethod";
            this.comboBoxMethod.Size = new System.Drawing.Size(341, 21);
            this.comboBoxMethod.TabIndex = 4;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(672, 409);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 16;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(764, 409);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(425, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Set:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(431, 297);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Filter:";
            // 
            // comboBoxFilterType
            // 
            this.comboBoxFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterType.FormattingEnabled = true;
            this.comboBoxFilterType.Items.AddRange(new object[] {
            "None",
            "Only photos with specified tags (IPTC Keywords). Semicolon for multiple tags",
            "Only photos with a minimal star rating"});
            this.comboBoxFilterType.Location = new System.Drawing.Point(498, 291);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(341, 21);
            this.comboBoxFilterType.TabIndex = 7;
            this.comboBoxFilterType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterType_SelectedIndexChanged);
            // 
            // textBoxTags
            // 
            this.textBoxTags.HideSelection = false;
            this.textBoxTags.Location = new System.Drawing.Point(498, 318);
            this.textBoxTags.Name = "textBoxTags";
            this.textBoxTags.Size = new System.Drawing.Size(301, 20);
            this.textBoxTags.TabIndex = 8;
            this.textBoxTags.Visible = false;
            // 
            // labelTags
            // 
            this.labelTags.AutoSize = true;
            this.labelTags.Location = new System.Drawing.Point(431, 323);
            this.labelTags.Name = "labelTags";
            this.labelTags.Size = new System.Drawing.Size(37, 13);
            this.labelTags.TabIndex = 15;
            this.labelTags.Text = "Tags: ";
            this.labelTags.Visible = false;
            // 
            // buttonTagList
            // 
            this.buttonTagList.Location = new System.Drawing.Point(815, 318);
            this.buttonTagList.Name = "buttonTagList";
            this.buttonTagList.Size = new System.Drawing.Size(24, 20);
            this.buttonTagList.TabIndex = 9;
            this.buttonTagList.Text = "...";
            this.buttonTagList.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonTagList.UseVisualStyleBackColor = true;
            this.buttonTagList.Visible = false;
            this.buttonTagList.Click += new System.EventHandler(this.buttonTagList_Click);
            // 
            // comboBoxPermissions
            // 
            this.comboBoxPermissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPermissions.FormattingEnabled = true;
            this.comboBoxPermissions.Items.AddRange(new object[] {
            "Flickr Default",
            "Public",
            "Family and Friends",
            "Only Friends",
            "Only Family",
            "Private"});
            this.comboBoxPermissions.Location = new System.Drawing.Point(498, 230);
            this.comboBoxPermissions.Name = "comboBoxPermissions";
            this.comboBoxPermissions.Size = new System.Drawing.Size(152, 21);
            this.comboBoxPermissions.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(431, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Permissions:";
            // 
            // checkBoxNoDelete
            // 
            this.checkBoxNoDelete.AutoSize = true;
            this.checkBoxNoDelete.Location = new System.Drawing.Point(434, 351);
            this.checkBoxNoDelete.Name = "checkBoxNoDelete";
            this.checkBoxNoDelete.Size = new System.Drawing.Size(173, 17);
            this.checkBoxNoDelete.TabIndex = 10;
            this.checkBoxNoDelete.Text = "Never Delete images from flickr";
            this.checkBoxNoDelete.UseVisualStyleBackColor = true;
            // 
            // checkBoxNoDeleteTags
            // 
            this.checkBoxNoDeleteTags.AutoSize = true;
            this.checkBoxNoDeleteTags.Location = new System.Drawing.Point(626, 351);
            this.checkBoxNoDeleteTags.Name = "checkBoxNoDeleteTags";
            this.checkBoxNoDeleteTags.Size = new System.Drawing.Size(160, 17);
            this.checkBoxNoDeleteTags.TabIndex = 11;
            this.checkBoxNoDeleteTags.Text = "Never Delete tags from flickr";
            this.checkBoxNoDeleteTags.UseVisualStyleBackColor = true;
            // 
            // buttonHelp
            // 
            this.buttonHelp.Location = new System.Drawing.Point(434, 409);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(75, 23);
            this.buttonHelp.TabIndex = 15;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // buttonOKAll
            // 
            this.buttonOKAll.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonOKAll.Location = new System.Drawing.Point(672, 380);
            this.buttonOKAll.Name = "buttonOKAll";
            this.buttonOKAll.Size = new System.Drawing.Size(75, 23);
            this.buttonOKAll.TabIndex = 13;
            this.buttonOKAll.Text = "OK to All";
            this.buttonOKAll.UseVisualStyleBackColor = true;
            this.buttonOKAll.Visible = false;
            // 
            // buttonCancelAll
            // 
            this.buttonCancelAll.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.buttonCancelAll.Location = new System.Drawing.Point(765, 380);
            this.buttonCancelAll.Name = "buttonCancelAll";
            this.buttonCancelAll.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelAll.TabIndex = 14;
            this.buttonCancelAll.Text = "Cancel All";
            this.buttonCancelAll.UseVisualStyleBackColor = true;
            this.buttonCancelAll.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(431, 264);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Order:";
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
            this.comboBoxOrderType.Location = new System.Drawing.Point(498, 261);
            this.comboBoxOrderType.Name = "comboBoxOrderType";
            this.comboBoxOrderType.Size = new System.Drawing.Size(341, 21);
            this.comboBoxOrderType.TabIndex = 6;
            // 
            // labelStarRating
            // 
            this.labelStarRating.AutoSize = true;
            this.labelStarRating.Location = new System.Drawing.Point(431, 322);
            this.labelStarRating.Name = "labelStarRating";
            this.labelStarRating.Size = new System.Drawing.Size(41, 13);
            this.labelStarRating.TabIndex = 20;
            this.labelStarRating.Text = "Rating:";
            this.labelStarRating.Visible = false;
            // 
            // pictureBoxStarRating5
            // 
            this.pictureBoxStarRating5.Image = global::FlickrSync.Properties.Resources.star_blue;
            this.pictureBoxStarRating5.Location = new System.Drawing.Point(642, 319);
            this.pictureBoxStarRating5.Name = "pictureBoxStarRating5";
            this.pictureBoxStarRating5.Size = new System.Drawing.Size(23, 19);
            this.pictureBoxStarRating5.TabIndex = 25;
            this.pictureBoxStarRating5.TabStop = false;
            this.pictureBoxStarRating5.Visible = false;
            this.pictureBoxStarRating5.Click += new System.EventHandler(this.pictureBoxStarRating5_Click);
            // 
            // pictureBoxStarRating4
            // 
            this.pictureBoxStarRating4.Image = global::FlickrSync.Properties.Resources.star_blue;
            this.pictureBoxStarRating4.Location = new System.Drawing.Point(613, 319);
            this.pictureBoxStarRating4.Name = "pictureBoxStarRating4";
            this.pictureBoxStarRating4.Size = new System.Drawing.Size(23, 19);
            this.pictureBoxStarRating4.TabIndex = 24;
            this.pictureBoxStarRating4.TabStop = false;
            this.pictureBoxStarRating4.Visible = false;
            this.pictureBoxStarRating4.Click += new System.EventHandler(this.pictureBoxStarRating4_Click);
            // 
            // pictureBoxStarRating3
            // 
            this.pictureBoxStarRating3.Image = global::FlickrSync.Properties.Resources.star_blue;
            this.pictureBoxStarRating3.Location = new System.Drawing.Point(584, 319);
            this.pictureBoxStarRating3.Name = "pictureBoxStarRating3";
            this.pictureBoxStarRating3.Size = new System.Drawing.Size(23, 19);
            this.pictureBoxStarRating3.TabIndex = 23;
            this.pictureBoxStarRating3.TabStop = false;
            this.pictureBoxStarRating3.Visible = false;
            this.pictureBoxStarRating3.Click += new System.EventHandler(this.pictureBoxStarRating3_Click);
            // 
            // pictureBoxStarRating2
            // 
            this.pictureBoxStarRating2.Image = global::FlickrSync.Properties.Resources.star_blue;
            this.pictureBoxStarRating2.Location = new System.Drawing.Point(555, 319);
            this.pictureBoxStarRating2.Name = "pictureBoxStarRating2";
            this.pictureBoxStarRating2.Size = new System.Drawing.Size(23, 19);
            this.pictureBoxStarRating2.TabIndex = 22;
            this.pictureBoxStarRating2.TabStop = false;
            this.pictureBoxStarRating2.Visible = false;
            this.pictureBoxStarRating2.Click += new System.EventHandler(this.pictureBoxStarRating2_Click);
            // 
            // pictureBoxStarRating1
            // 
            this.pictureBoxStarRating1.Image = global::FlickrSync.Properties.Resources.star_blue;
            this.pictureBoxStarRating1.Location = new System.Drawing.Point(526, 318);
            this.pictureBoxStarRating1.Name = "pictureBoxStarRating1";
            this.pictureBoxStarRating1.Size = new System.Drawing.Size(23, 19);
            this.pictureBoxStarRating1.TabIndex = 21;
            this.pictureBoxStarRating1.TabStop = false;
            this.pictureBoxStarRating1.Visible = false;
            this.pictureBoxStarRating1.Click += new System.EventHandler(this.pictureBoxStarRating1_Click);
            // 
            // pictureBoxStarRating0
            // 
            this.pictureBoxStarRating0.Image = global::FlickrSync.Properties.Resources.star_empty;
            this.pictureBoxStarRating0.Location = new System.Drawing.Point(497, 318);
            this.pictureBoxStarRating0.Name = "pictureBoxStarRating0";
            this.pictureBoxStarRating0.Size = new System.Drawing.Size(23, 19);
            this.pictureBoxStarRating0.TabIndex = 31;
            this.pictureBoxStarRating0.TabStop = false;
            this.pictureBoxStarRating0.Visible = false;
            this.pictureBoxStarRating0.Click += new System.EventHandler(this.pictureBoxStarRating0_Click);
            // 
            // checkBoxNoInitialReplace
            // 
            this.checkBoxNoInitialReplace.AutoSize = true;
            this.checkBoxNoInitialReplace.Location = new System.Drawing.Point(434, 374);
            this.checkBoxNoInitialReplace.Name = "checkBoxNoInitialReplace";
            this.checkBoxNoInitialReplace.Size = new System.Drawing.Size(184, 17);
            this.checkBoxNoInitialReplace.TabIndex = 12;
            this.checkBoxNoInitialReplace.Text = "Do not replace items on first Sync";
            this.checkBoxNoInitialReplace.UseVisualStyleBackColor = true;
            // 
            // SyncFolderForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(864, 444);
            this.Controls.Add(this.checkBoxNoInitialReplace);
            this.Controls.Add(this.pictureBoxStarRating0);
            this.Controls.Add(this.pictureBoxStarRating5);
            this.Controls.Add(this.pictureBoxStarRating4);
            this.Controls.Add(this.pictureBoxStarRating3);
            this.Controls.Add(this.pictureBoxStarRating2);
            this.Controls.Add(this.pictureBoxStarRating1);
            this.Controls.Add(this.labelStarRating);
            this.Controls.Add(this.comboBoxOrderType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.buttonCancelAll);
            this.Controls.Add(this.buttonOKAll);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.checkBoxNoDeleteTags);
            this.Controls.Add(this.checkBoxNoDelete);
            this.Controls.Add(this.comboBoxPermissions);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonTagList);
            this.Controls.Add(this.labelTags);
            this.Controls.Add(this.textBoxTags);
            this.Controls.Add(this.comboBoxFilterType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxMethod);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelSetTitle);
            this.Controls.Add(this.labelFolderPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SyncFolderForm";
            this.Text = "Synchronization Folder Properties";
            this.Load += new System.EventHandler(this.SyncFolderForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStarRating0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFolderPath;
        private System.Windows.Forms.Label labelSetTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxMethod;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxFilterType;
        private System.Windows.Forms.TextBox textBoxTags;
        private System.Windows.Forms.Label labelTags;
        private System.Windows.Forms.Button buttonTagList;
        private System.Windows.Forms.ComboBox comboBoxPermissions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxNoDelete;
        private System.Windows.Forms.CheckBox checkBoxNoDeleteTags;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button buttonOKAll;
        private System.Windows.Forms.Button buttonCancelAll;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxOrderType;
        private System.Windows.Forms.Label labelStarRating;
        private System.Windows.Forms.PictureBox pictureBoxStarRating1;
        private System.Windows.Forms.PictureBox pictureBoxStarRating2;
        private System.Windows.Forms.PictureBox pictureBoxStarRating3;
        private System.Windows.Forms.PictureBox pictureBoxStarRating4;
        private System.Windows.Forms.PictureBox pictureBoxStarRating5;
        private System.Windows.Forms.PictureBox pictureBoxStarRating0;
        private System.Windows.Forms.CheckBox checkBoxNoInitialReplace;
    }
}