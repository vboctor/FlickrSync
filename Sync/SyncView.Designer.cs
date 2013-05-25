namespace FlickrSync
{
    partial class SyncView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncView));
            this.listViewToSync = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonSync = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.progressBarSync = new System.Windows.Forms.ProgressBar();
            this.progressBarPhoto = new System.Windows.Forms.ProgressBar();
            this.labelSync = new System.Windows.Forms.Label();
            this.webBrowserAds = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // listViewToSync
            // 
            this.listViewToSync.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewToSync.LargeImageList = this.imageList1;
            this.listViewToSync.Location = new System.Drawing.Point(3, 2);
            this.listViewToSync.Name = "listViewToSync";
            this.listViewToSync.ShowItemToolTips = true;
            this.listViewToSync.Size = new System.Drawing.Size(485, 288);
            this.listViewToSync.TabIndex = 0;
            this.listViewToSync.TileSize = new System.Drawing.Size(1, 1);
            this.listViewToSync.UseCompatibleStateImageBehavior = false;
            this.listViewToSync.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listViewToSync_DrawItem);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "default.gif");
            // 
            // buttonSync
            // 
            this.buttonSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSync.Location = new System.Drawing.Point(301, 343);
            this.buttonSync.Name = "buttonSync";
            this.buttonSync.Size = new System.Drawing.Size(75, 23);
            this.buttonSync.TabIndex = 1;
            this.buttonSync.Text = "Sync";
            this.buttonSync.UseVisualStyleBackColor = true;
            this.buttonSync.Click += new System.EventHandler(this.buttonSync_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(404, 343);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // progressBarSync
            // 
            this.progressBarSync.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarSync.Location = new System.Drawing.Point(3, 308);
            this.progressBarSync.Name = "progressBarSync";
            this.progressBarSync.Size = new System.Drawing.Size(485, 10);
            this.progressBarSync.TabIndex = 3;
            // 
            // progressBarPhoto
            // 
            this.progressBarPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarPhoto.Location = new System.Drawing.Point(3, 296);
            this.progressBarPhoto.Name = "progressBarPhoto";
            this.progressBarPhoto.Size = new System.Drawing.Size(485, 10);
            this.progressBarPhoto.TabIndex = 4;
            // 
            // labelSync
            // 
            this.labelSync.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSync.Location = new System.Drawing.Point(5, 321);
            this.labelSync.Name = "labelSync";
            this.labelSync.Size = new System.Drawing.Size(483, 19);
            this.labelSync.TabIndex = 5;
            // 
            // webBrowserAds
            // 
            this.webBrowserAds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserAds.Location = new System.Drawing.Point(3, 218);
            this.webBrowserAds.MaximumSize = new System.Drawing.Size(0, 70);
            this.webBrowserAds.MinimumSize = new System.Drawing.Size(20, 70);
            this.webBrowserAds.Name = "webBrowserAds";
            this.webBrowserAds.ScriptErrorsSuppressed = true;
            this.webBrowserAds.ScrollBarsEnabled = false;
            this.webBrowserAds.Size = new System.Drawing.Size(485, 70);
            this.webBrowserAds.TabIndex = 6;
            this.webBrowserAds.TabStop = false;
            this.webBrowserAds.Url = new System.Uri("http://flickrsync.freehostia.com/ad.htm", System.UriKind.Absolute);
            this.webBrowserAds.Visible = false;
            this.webBrowserAds.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowserAds_Navigating);
            // 
            // SyncView
            // 
            this.AcceptButton = this.buttonSync;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(491, 378);
            this.Controls.Add(this.webBrowserAds);
            this.Controls.Add(this.labelSync);
            this.Controls.Add(this.progressBarPhoto);
            this.Controls.Add(this.progressBarSync);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSync);
            this.Controls.Add(this.listViewToSync);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(231, 246);
            this.Name = "SyncView";
            this.Text = "FlickrSync Preview";
            this.Load += new System.EventHandler(this.SyncView_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SyncView_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewToSync;
        private System.Windows.Forms.Button buttonSync;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ProgressBar progressBarSync;
        private System.Windows.Forms.ProgressBar progressBarPhoto;
        private System.Windows.Forms.Label labelSync;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.WebBrowser webBrowserAds;
    }
}