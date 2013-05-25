namespace FlickrSync
{
    partial class FlickrSync
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlickrSync));
            this.contextMenuLocalFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAndSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemSyncChildren = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutorunMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DonateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadSetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNetworkPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.labelCalc = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeFolders = new Common.Controls.TreeFoldersToSync();
            this.listSets = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.contextMenuSets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ConfigSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewAndSyncSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.organizeSetOnFlickrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnViewSync = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemViews = new System.Windows.Forms.ToolStripSplitButton();
            this.menuitemLargeIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemSmallIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemTile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.contextMenuLocalFolder.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuSets.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuLocalFolder
            // 
            this.contextMenuLocalFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem,
            this.removePathToolStripMenuItem,
            this.viewAndSyncToolStripMenuItem,
            this.menuitemSyncChildren,
            this.toolStripMenuItem3,
            this.checkAllToolStripMenuItem,
            this.uncheckAllToolStripMenuItem});
            this.contextMenuLocalFolder.Name = "contextMenuLocalFolder";
            this.contextMenuLocalFolder.Size = new System.Drawing.Size(232, 142);
            this.contextMenuLocalFolder.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuLocalFolder_Opening);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.configToolStripMenuItem.Text = "&Properties...";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // removePathToolStripMenuItem
            // 
            this.removePathToolStripMenuItem.Name = "removePathToolStripMenuItem";
            this.removePathToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.removePathToolStripMenuItem.Text = "&Remove Path";
            this.removePathToolStripMenuItem.Click += new System.EventHandler(this.removePathToolStripMenuItem_Click);
            // 
            // viewAndSyncToolStripMenuItem
            // 
            this.viewAndSyncToolStripMenuItem.Name = "viewAndSyncToolStripMenuItem";
            this.viewAndSyncToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.viewAndSyncToolStripMenuItem.Text = "&View and Sync...";
            this.viewAndSyncToolStripMenuItem.Click += new System.EventHandler(this.viewAndSyncToolStripMenuItem_Click);
            // 
            // menuitemSyncChildren
            // 
            this.menuitemSyncChildren.Name = "menuitemSyncChildren";
            this.menuitemSyncChildren.Size = new System.Drawing.Size(231, 22);
            this.menuitemSyncChildren.Text = "View and Sync with Children...";
            this.menuitemSyncChildren.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.menuitemSyncChildren.Click += new System.EventHandler(this.menuitemSyncChildren_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(228, 6);
            // 
            // checkAllToolStripMenuItem
            // 
            this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
            this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.checkAllToolStripMenuItem.Text = "Check All Children";
            this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
            // 
            // uncheckAllToolStripMenuItem
            // 
            this.uncheckAllToolStripMenuItem.Name = "uncheckAllToolStripMenuItem";
            this.uncheckAllToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.uncheckAllToolStripMenuItem.Text = "Uncheck All Children";
            this.uncheckAllToolStripMenuItem.Click += new System.EventHandler(this.uncheckAllToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.syncToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(933, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newUserToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.AutorunMenuItem,
            this.ViewLogMenuItem,
            this.DonateMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newUserToolStripMenuItem
            // 
            this.newUserToolStripMenuItem.Name = "newUserToolStripMenuItem";
            this.newUserToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.newUserToolStripMenuItem.Text = "&New User";
            this.newUserToolStripMenuItem.Click += new System.EventHandler(this.newUserToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.saveToolStripMenuItem.Text = "&Save Config";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.preferencesToolStripMenuItem.Text = "&Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // AutorunMenuItem
            // 
            this.AutorunMenuItem.Name = "AutorunMenuItem";
            this.AutorunMenuItem.Size = new System.Drawing.Size(143, 22);
            this.AutorunMenuItem.Text = "&Autorun";
            this.AutorunMenuItem.Click += new System.EventHandler(this.AutorunMenuItem_Click);
            // 
            // ViewLogMenuItem
            // 
            this.ViewLogMenuItem.Name = "ViewLogMenuItem";
            this.ViewLogMenuItem.Size = new System.Drawing.Size(143, 22);
            this.ViewLogMenuItem.Text = "View &Log";
            this.ViewLogMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewLogMenuItem_Paint);
            this.ViewLogMenuItem.Click += new System.EventHandler(this.ViewLogMenuItem_Click);
            // 
            // DonateMenuItem
            // 
            this.DonateMenuItem.Name = "DonateMenuItem";
            this.DonateMenuItem.Size = new System.Drawing.Size(143, 22);
            this.DonateMenuItem.Text = "&Donate";
            this.DonateMenuItem.Click += new System.EventHandler(this.DonateMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // syncToolStripMenuItem
            // 
            this.syncToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.reloadSetsToolStripMenuItem,
            this.addNetworkPathToolStripMenuItem});
            this.syncToolStripMenuItem.Name = "syncToolStripMenuItem";
            this.syncToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.syncToolStripMenuItem.Text = "&Sync";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.viewToolStripMenuItem.Text = "&View and Sync All";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // reloadSetsToolStripMenuItem
            // 
            this.reloadSetsToolStripMenuItem.Name = "reloadSetsToolStripMenuItem";
            this.reloadSetsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.reloadSetsToolStripMenuItem.Text = "&Reload Sets";
            this.reloadSetsToolStripMenuItem.Click += new System.EventHandler(this.reloadSetsToolStripMenuItem_Click);
            // 
            // addNetworkPathToolStripMenuItem
            // 
            this.addNetworkPathToolStripMenuItem.Name = "addNetworkPathToolStripMenuItem";
            this.addNetworkPathToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.addNetworkPathToolStripMenuItem.Text = "Add &Network Path";
            this.addNetworkPathToolStripMenuItem.Click += new System.EventHandler(this.addNetworkPathToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.aboutToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // imageListLarge
            // 
            this.imageListLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListLarge.ImageSize = new System.Drawing.Size(75, 75);
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // labelCalc
            // 
            this.labelCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCalc.AutoSize = true;
            this.labelCalc.BackColor = System.Drawing.SystemColors.Control;
            this.labelCalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCalc.Location = new System.Drawing.Point(264, 248);
            this.labelCalc.Name = "labelCalc";
            this.labelCalc.Padding = new System.Windows.Forms.Padding(10);
            this.labelCalc.Size = new System.Drawing.Size(413, 57);
            this.labelCalc.TabIndex = 3;
            this.labelCalc.Text = "Preparing Synchronization";
            this.labelCalc.Visible = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(-200, -200);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(100, 85);
            this.webBrowser1.TabIndex = 4;
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 496);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(933, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(109, 17);
            this.labelStatus.Text = "toolStripStatusLabel1";
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(933, 469);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeFolders);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listSets);
            this.splitContainer1.Size = new System.Drawing.Size(933, 447);
            this.splitContainer1.SplitterDistance = 310;
            this.splitContainer1.TabIndex = 7;
            // 
            // treeFolders
            // 
            this.treeFolders.AllowDrop = true;
            this.treeFolders.CheckBoxes = true;
            this.treeFolders.ContextMenuStrip = this.contextMenuLocalFolder;
            this.treeFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFolders.Location = new System.Drawing.Point(0, 0);
            this.treeFolders.Name = "treeFolders";
            this.treeFolders.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("treeFolders.SelectedNodes")));
            this.treeFolders.ShowNodeToolTips = true;
            this.treeFolders.Size = new System.Drawing.Size(310, 447);
            this.treeFolders.TabIndex = 0;
            this.treeFolders.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeFolders_NodeMouseDoubleClick);
            this.treeFolders.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeFolders_AfterCheck);
            this.treeFolders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeFolders_MouseDown);
            this.treeFolders.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeFolders_ItemDrag);
            // 
            // listSets
            // 
            this.listSets.AllowDrop = true;
            this.listSets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listSets.ContextMenuStrip = this.contextMenuSets;
            this.listSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSets.FullRowSelect = true;
            this.listSets.LargeImageList = this.imageListLarge;
            this.listSets.Location = new System.Drawing.Point(0, 0);
            this.listSets.Name = "listSets";
            this.listSets.ShowGroups = false;
            this.listSets.ShowItemToolTips = true;
            this.listSets.Size = new System.Drawing.Size(619, 447);
            this.listSets.SmallImageList = this.imageListSmall;
            this.listSets.TabIndex = 2;
            this.listSets.UseCompatibleStateImageBehavior = false;
            this.listSets.DoubleClick += new System.EventHandler(this.listSets_DoubleClick);
            this.listSets.DragDrop += new System.Windows.Forms.DragEventHandler(this.listSets_DragDrop);
            this.listSets.DragEnter += new System.Windows.Forms.DragEventHandler(this.listSets_DragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Set";
            this.columnHeader1.Width = 183;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Photos";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Description";
            this.columnHeader3.Width = 350;
            // 
            // contextMenuSets
            // 
            this.contextMenuSets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigSetToolStripMenuItem,
            this.ViewAndSyncSetToolStripMenuItem,
            this.organizeSetOnFlickrToolStripMenuItem});
            this.contextMenuSets.Name = "contextMenuLocalFolder";
            this.contextMenuSets.Size = new System.Drawing.Size(210, 70);
            this.contextMenuSets.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuSets_Opening);
            // 
            // ConfigSetToolStripMenuItem
            // 
            this.ConfigSetToolStripMenuItem.Name = "ConfigSetToolStripMenuItem";
            this.ConfigSetToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.ConfigSetToolStripMenuItem.Text = "&Properties...";
            this.ConfigSetToolStripMenuItem.Click += new System.EventHandler(this.configSetToolStripMenuItem_Click);
            // 
            // ViewAndSyncSetToolStripMenuItem
            // 
            this.ViewAndSyncSetToolStripMenuItem.Name = "ViewAndSyncSetToolStripMenuItem";
            this.ViewAndSyncSetToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.ViewAndSyncSetToolStripMenuItem.Text = "&View and Sync selected...";
            this.ViewAndSyncSetToolStripMenuItem.Click += new System.EventHandler(this.viewAndSyncSetToolStripMenuItem_Click);
            // 
            // organizeSetOnFlickrToolStripMenuItem
            // 
            this.organizeSetOnFlickrToolStripMenuItem.Name = "organizeSetOnFlickrToolStripMenuItem";
            this.organizeSetOnFlickrToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.organizeSetOnFlickrToolStripMenuItem.Text = "&Organize Set on flickr";
            this.organizeSetOnFlickrToolStripMenuItem.Click += new System.EventHandler(this.organizeSetOnFlickrToolStripMenuItem_Click);
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "default.gif");
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnViewSync,
            this.toolStripSeparator2,
            this.btnSave,
            this.toolStripSeparator3,
            this.menuitemViews,
            this.toolStripSeparator4,
            this.btnHelp});
            this.toolStrip2.Location = new System.Drawing.Point(0, 24);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(933, 25);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 7;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnViewSync
            // 
            this.btnViewSync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnViewSync.Image = global::FlickrSync.Properties.Resources.toolbar_sync;
            this.btnViewSync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewSync.Name = "btnViewSync";
            this.btnViewSync.Size = new System.Drawing.Size(23, 22);
            this.btnViewSync.Text = "View and Sync All";
            this.btnViewSync.Click += new System.EventHandler(this.btnViewSync_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::FlickrSync.Properties.Resources.toolbar_save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save Config";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // menuitemViews
            // 
            this.menuitemViews.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuitemViews.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemLargeIcons,
            this.menuitemSmallIcons,
            this.menuitemDetails,
            this.menuitemList,
            this.menuitemTile});
            this.menuitemViews.Image = global::FlickrSync.Properties.Resources.toolbar_view;
            this.menuitemViews.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuitemViews.Name = "menuitemViews";
            this.menuitemViews.Size = new System.Drawing.Size(32, 22);
            this.menuitemViews.Text = "Views";
            this.menuitemViews.ButtonClick += new System.EventHandler(this.menuitemViews_ButtonClick);
            this.menuitemViews.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuitemViews_DropDownItemClicked);
            // 
            // menuitemLargeIcons
            // 
            this.menuitemLargeIcons.Checked = true;
            this.menuitemLargeIcons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuitemLargeIcons.Name = "menuitemLargeIcons";
            this.menuitemLargeIcons.Size = new System.Drawing.Size(141, 22);
            this.menuitemLargeIcons.Tag = "LargeIcon";
            this.menuitemLargeIcons.Text = "Large Icons";
            // 
            // menuitemSmallIcons
            // 
            this.menuitemSmallIcons.Name = "menuitemSmallIcons";
            this.menuitemSmallIcons.Size = new System.Drawing.Size(141, 22);
            this.menuitemSmallIcons.Tag = "SmallIcon";
            this.menuitemSmallIcons.Text = "Small Icons";
            // 
            // menuitemDetails
            // 
            this.menuitemDetails.Name = "menuitemDetails";
            this.menuitemDetails.Size = new System.Drawing.Size(141, 22);
            this.menuitemDetails.Tag = "Details";
            this.menuitemDetails.Text = "Details";
            // 
            // menuitemList
            // 
            this.menuitemList.Name = "menuitemList";
            this.menuitemList.Size = new System.Drawing.Size(141, 22);
            this.menuitemList.Tag = "List";
            this.menuitemList.Text = "List";
            // 
            // menuitemTile
            // 
            this.menuitemTile.Name = "menuitemTile";
            this.menuitemTile.Size = new System.Drawing.Size(141, 22);
            this.menuitemTile.Tag = "Tile";
            this.menuitemTile.Text = "Tile";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnHelp
            // 
            this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHelp.Image = global::FlickrSync.Properties.Resources.toolbar_help;
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(23, 22);
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(933, 447);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(933, 496);
            this.toolStripContainer1.TabIndex = 8;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // FlickrSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 518);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.labelCalc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FlickrSync";
            this.Text = "FlickrSync";
            this.Load += new System.EventHandler(this.FlickrSync_Load);
            this.Resize += new System.EventHandler(this.FlickrSync_Resize);
            this.contextMenuLocalFolder.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuSets.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ImageList imageListLarge;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newUserToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuLocalFolder;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadSetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewAndSyncToolStripMenuItem;
        private System.Windows.Forms.Label labelCalc;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnViewSync;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSave;
        private Common.Controls.TreeFoldersToSync treeFolders;
        private System.Windows.Forms.ListView listSets;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSplitButton menuitemViews;
        private System.Windows.Forms.ToolStripMenuItem menuitemLargeIcons;
        private System.Windows.Forms.ToolStripMenuItem menuitemSmallIcons;
        private System.Windows.Forms.ToolStripMenuItem menuitemDetails;
        private System.Windows.Forms.ToolStripMenuItem menuitemList;
        private System.Windows.Forms.ToolStripMenuItem menuitemTile;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uncheckAllToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuSets;
        private System.Windows.Forms.ToolStripMenuItem ConfigSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewAndSyncSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuitemSyncChildren;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.ToolStripMenuItem addNetworkPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutorunMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewLogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem organizeSetOnFlickrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DonateMenuItem;


    }
}

