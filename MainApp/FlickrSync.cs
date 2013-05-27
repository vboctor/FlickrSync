using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using FlickrNet;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Windows.Forms.Design;

namespace FlickrSync
{
    public partial class FlickrSync : Form
    {
        public enum Permissions { PermDefault = 0, PermPublic, PermFamilyFriends, PermFriends, PermFamily, PermPrivate};
        public enum SyncPropertiesStatus { Default = 0, MultipleUndef, OKAll, CancelAll };
        public enum ErrorType { Normal = 0, FatalError, Connect, Info, Debug };
        public enum MessagesLevel { MessagesNone = 0, MessagesBasic, MessagesAll };
        public enum LogLevel { LogNone = 0, LogBasic, LogAll, LogDebug };

        static public LocalInfo li;
        static public RemoteInfo ri;
        static public string user;
        static public bool autorun = false;
        static public MessagesLevel messages_level = MessagesLevel.MessagesAll;
        static public LogLevel log_level = LogLevel.LogNone;
        Point MouseDownPos;

        static private SyncPropertiesStatus syncprop_status=SyncPropertiesStatus.Default;
        static private string proxy_password="";

        #region GeneralStuff
        public FlickrSync()
        {
            InitializeComponent();
        }

        private static bool ButtonCallPref(int id)
        {
            Preferences pref = new Preferences();
            pref.ShowDialog();
            return false;
        }

        public static void Error(string msg, Exception e, ErrorType type)
        {
            string exception_message = "";
            if (e != null)
                exception_message = "Error: " + e.Message;

            if (messages_level != MessagesLevel.MessagesNone &&
                !(messages_level == MessagesLevel.MessagesBasic && type != ErrorType.FatalError && type != ErrorType.Connect))
            {
                if (type == ErrorType.Connect)
                {
                    //Use specific Error dialog to allow changing of preferences
                    CustomMsgBox msgbox=new CustomMsgBox(msg + "\n" + exception_message,"FlickrSync Error");
                    msgbox.AddButton("OK", 75, 1, msgbox.ButtonCallOK);
                    msgbox.AddButton("Preferences",75,2,ButtonCallPref);
                    msgbox.ShowDialog();
                }
                else
                    MessageBox.Show(msg + "\n" + exception_message, "Error");
            }

            string logmsg=msg+"; "+exception_message;
            if (type == ErrorType.FatalError || type == ErrorType.Connect)
                Log(LogLevel.LogBasic, logmsg);
            else
                Log(LogLevel.LogAll, logmsg);

            if (type==ErrorType.FatalError || type==ErrorType.Connect) 
                Application.Exit();
        }

        public static string LogFile()
        {
            string logfile = Properties.Settings.Default.LogFile;
            if (logfile == "")
                logfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "FlickrSync_Log.txt";

            return logfile;
        }

        public static void Log(LogLevel level,string msg)
        {
            if (level <= log_level)
            {
                string logmsg;
                logmsg = String.Format("{0:s} {1}", DateTime.Now, msg);

                StreamWriter SW;
                SW = File.AppendText(LogFile());
                SW.WriteLine(logmsg);
                SW.Close();
            }
        }

        public static MessagesLevel StringToMsgLevel(string str)
        {
            if (str == "MessagesNone")
                return MessagesLevel.MessagesNone;
            if (str == "MessagesBasic")
                return MessagesLevel.MessagesBasic;
            if (str == "MessagesAll")
                return MessagesLevel.MessagesAll;

            return MessagesLevel.MessagesAll;
        }

        public static LogLevel StringToLogLevel(string str)
        {
            if (str == "LogNone")
                return LogLevel.LogNone;
            if (str == "LogBasic")
                return LogLevel.LogBasic;
            if (str == "LogAll")
                return LogLevel.LogAll;
            if (str == "LogDebug")
                return LogLevel.LogDebug;

            return LogLevel.LogNone;
        }

        #endregion

        #region Initial Load
        private void FlickrSync_Load(object sender, EventArgs e)
        {
            string token="";
            try {
                token=Properties.Settings.Default.FlickrToken;
            }
            catch(Exception)
            {
            }

            if (token == "")
                FlickrGetToken();

            messages_level = FlickrSync.StringToMsgLevel(Properties.Settings.Default.MessageLevel);
            log_level = FlickrSync.StringToLogLevel(Properties.Settings.Default.LogLevel);
            if (autorun)
                messages_level = MessagesLevel.MessagesNone;

            // ri is needed to get the user. TODO: Get User without using ri
            ri = new RemoteInfo();

            UpdateUser();
            LoadConfig();
            Reload();
            FlickrSync.Log(LogLevel.LogBasic, "Application loaded");

            if (autorun)
                WindowState = FormWindowState.Minimized;
            else
                WindowState = FormWindowState.Maximized;
            
            if (autorun)
            {
                ViewAndSync();
                Close();
            }
        }

        public void FillListSet(ProgressBar progress)
        {
            int photos = 0;
            Photoset[] allsets = ri.GetAllSets();
            if (progress != null)
            {
                progress.Minimum = 0;
                progress.Maximum = allsets.Length;
                progress.Value = 0;
            }

            // Setup the image list first to speed things up.
            Image[] tempimgs = new Image[allsets.Length];
            Image[] tempimgsmall = new Image[allsets.Length];

            for (int i = 0; i < allsets.Length; i++)
            {
                // use a fixed temporary image
                tempimgs[i] = global::FlickrSync.Properties.Resources.icon_default;
                tempimgsmall[i] = global::FlickrSync.Properties.Resources.icon_default;
            }
            imageListLarge.Images.AddRange(tempimgs);
            imageListSmall.Images.AddRange(tempimgsmall);

            for (int i = 0; i < allsets.Length; i++)
            {
                Photoset psi = allsets[i];
                Image img = ri.PhotosetThumbnail(psi);
                imageListLarge.Images[i] = img;
                imageListLarge.Images.SetKeyName(i, psi.PhotosetId);
                imageListSmall.Images[i] = img.GetThumbnailImage(16, 16, null, IntPtr.Zero);
                imageListSmall.Images.SetKeyName(i, psi.PhotosetId);

                ListViewItem lvi = listSets.Items.Add(psi.PhotosetId, psi.Title, psi.PhotosetId);
                lvi.SubItems.Add(psi.NumberOfPhotos.ToString());
                lvi.SubItems.Add(psi.Description);

                if (progress != null)
                {
                    progress.Value = i;
                }
                photos += psi.NumberOfPhotos;
            }

            labelStatus.Text = string.Format("Sets on Flickr: {0}       Photos on Flickr: {1}", allsets.Length, photos);
        }

        public void ValidateSets()
        {
            ArrayList ToRemove=new ArrayList();

            foreach (SyncFolder sf in li.GetSyncFolders())
            {
                bool found = false;
                foreach (Photoset psi in ri.GetAllSets())
                {
                    if (sf.SetId == psi.PhotosetId)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found && sf.SetId!="")
                {
                    if (messages_level != MessagesLevel.MessagesNone && MessageBox.Show("Folder " + sf.FolderPath + " is configured to synchronize with a Set that does not exist (ID " +
                        sf.SetId + "). Delete from configuration?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ToRemove.Add(sf.FolderPath);
                        Log(LogLevel.LogAll, sf.FolderPath + " deleted from configuration");
                    }
                    else
                        Log(LogLevel.LogAll, sf.FolderPath + " configured to synchronize with a set that does not exists");
                }
            }

            foreach (string path in ToRemove) 
                li.Remove(path);

            if (ToRemove.Count > 0)
                li.SaveToXML();
        }

        private string FlickrGetToken()
        {
            try
            {
                Flickr f = new Flickr(Properties.Settings.Default.FlickrApiKey, Properties.Settings.Default.FlickrShared);
                f.Proxy = GetProxy(true);

                string Frob = f.AuthGetFrob();
                string url = f.AuthCalcUrl(Frob, AuthLevel.Read | AuthLevel.Write | AuthLevel.Delete);
                System.Diagnostics.Process.Start(url);

                if (messages_level==MessagesLevel.MessagesNone || MessageBox.Show("Please confirm that you've authorized FlickrSync to access your flickr account", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Auth auth = f.AuthGetToken(Frob);
                    Properties.Settings.Default.FlickrToken = auth.Token;
                    SaveConfig();
                    return Properties.Settings.Default.FlickrToken;
                }
            }
            catch (Exception e)
            {
                if (Properties.Settings.Default.FlickrToken == "")
                    Error("Unable to obtain Flickr Token", e, ErrorType.Connect);
                else
                    Error("Error obtaining Flickr Token", e, ErrorType.Normal);
            }

            return "";
        }


        #endregion

        #region Methods
        public static void LoadConfig()
        {
            string xml = "<FlickrSync></FlickrSync>";

            try
            {
                string xmlfilepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar+"FlickrSync.Config."+user+".XML";
                if (!File.Exists(xmlfilepath)) //to maintain compatibility with versions 0.7 and below
                    xmlfilepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar+"FlickrSync.Config.XML";

                if (File.Exists(xmlfilepath))
                {
                    StreamReader s = new StreamReader(xmlfilepath);
                    xml = s.ReadToEnd();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                Error("Problem loading configuration file.",ex,ErrorType.FatalError);
            }

            Properties.Settings.Default.LocalInfoXml = xml;
        }

        public static void SaveConfig()
        {
            try
            {
                Properties.Settings.Default.Save();
                if (user==null || user== "") return;

                StreamWriter s = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar+"FlickrSync.Config."+user+".XML");
                s.Write(Properties.Settings.Default.LocalInfoXml);
                s.Close();
            }
            catch (Exception ex)
            {
                Error("Error saving configuration", ex, ErrorType.Normal);
            }
        }

        private void UpdateUser()
        {
            try
            {
                user = ri.User();
                Text = "FlickrSync (" + user + ")";
            }
            catch (Exception ex)
            {
                Error("Error obtaining user name", ex, ErrorType.Normal);
            }
        }

        public void Reload()
        {
            try
            {
                li = new LocalInfo();
                li.LoadFromXML(Properties.Settings.Default.LocalInfoXml);
                
                treeFolders.Initialize();
                foreach (PathInfo pi in li.GetPathInfoList())
                    if (pi.ManualAdd)
                        treeFolders.AddBaseNode(pi.Path);

                ExpandOpenFolders();
            }
            catch (Exception ex)
            {
                Error("Error initializing local folders Window", ex, ErrorType.FatalError);
            }

            try
            {
                AboutBox1 about = new AboutBox1("PLEASE WAIT... LOADING YOUR SETS");
                if (!autorun)
                    about.Show();

                Application.DoEvents();

                Flickr.CacheDisabled = true;
                ri = new RemoteInfo();
                Flickr.CacheDisabled = false; // ri already has the sets lists. Cache can be used for thumbnails
                
                listSets.Items.Clear();
                imageListLarge.Images.Clear();
                imageListSmall.Images.Clear();

                if (autorun)
                    FillListSet(null);
                else
                    FillListSet(about.GetProgressBar());

                ValidateSets();

                Application.DoEvents();
                about.Close();
            }
            catch (Exception ex)
            {
                Error("Error obtaining flickr sets", ex, ErrorType.FatalError);
            }

            CalcTooltips();
            UpdateStatusBar();
        }

        public void CalcTooltips()
        {
            for (int i = 0; i < listSets.Items.Count; i++)
            {
                string ttt = "";
                SyncFolder sf = FlickrSync.li.GetSyncFolderBySet(listSets.Items[i].Name);
                if (sf!=null) 
                    ttt = sf.FolderPath;

                listSets.Items[i].ToolTipText = ttt;
            }
        }

        public void UpdateStatusBar()
        {
            int photos = 0;
            Photoset[] allsets = ri.GetAllSets();

            for (int i = 0; i < allsets.Length; i++)
                photos += allsets[i].NumberOfPhotos;

            labelStatus.Text = string.Format("Sets on Flickr: {0}       Photos on Flickr: {1}", allsets.Length, photos);
        }

        public void AddNewSets(ArrayList SetIds)
        {

            foreach (string SetId in SetIds)
            {
                Flickr.CacheDisabled = true;
                Photoset psi = ri.GetSet(SetId);
                Flickr.CacheDisabled = false;
                if (psi != null)
                {
                    Image img = ri.PhotosetThumbnail(psi);
                    imageListLarge.Images.Add(SetId, img);
                    imageListSmall.Images.Add(SetId, img.GetThumbnailImage(16, 16, null, IntPtr.Zero));
                    
                    ListViewItem lvi = listSets.Items.Insert(0, SetId, psi.Title, SetId);
                    lvi.SubItems.Add(psi.NumberOfPhotos.ToString());
                    lvi.SubItems.Add(psi.Description);
                }
            }

            listSets.EnsureVisible(0);

            ri.Reload();
            CalcTooltips();
            UpdateStatusBar();
        }

        public ImageList GetImageList()
        {
            return imageListLarge;
        }

        public void ViewAndSync(ArrayList sf_col)
        {
            if (sf_col.Count > 0)
            {
                labelCalc.Visible = true;
                labelCalc.BringToFront();
                SyncView sv = new SyncView(sf_col);
                labelCalc.Visible = false;

                if (sv!=null && !sv.IsDisposed)
                    sv.ShowDialog();

                this.Visible = true;
            }
        }

        public void ViewAndSync()
        {
            labelCalc.Visible = true;
            labelCalc.BringToFront();
            SyncView sv = new SyncView(li.GetSyncFolders());
            labelCalc.Visible = false;

            if (sv != null && !sv.IsDisposed)
                sv.ShowDialog(this);
            this.Visible = true;
        }

        static public WebProxy GetProxy(bool interactive)
        {
            if (!Properties.Settings.Default.ProxyUse)
                return null;

            string domain_user = Properties.Settings.Default.ProxyUser;
            string pass = Properties.Settings.Default.ProxyPass;
            if (pass=="")
                pass = proxy_password;

            if (pass=="") {
                if (interactive)
                {
                    Login l = new Login(domain_user, pass);
                    if (l.ShowDialog() != DialogResult.OK)
                        return null;

                    if (!Properties.Settings.Default.ProxyUse)
                        return null;

                    domain_user = l.GetUser();
                    pass = l.GetPass();
                }

                Properties.Settings.Default.ProxyUser = domain_user;
                Properties.Settings.Default.Save();

                proxy_password = pass;
            }

            string domain;
            string proxyuser;

            if (domain_user.Contains(@"\")) {
                int pos=domain_user.IndexOf('\\');
                domain = domain_user.Substring(0,pos);
                proxyuser = domain_user.Substring(pos + 1, domain_user.Length - pos - 1);
            }
            else 
            {
                domain="";
                proxyuser=domain_user;
            }

            try {
                WebProxy proxyObject = new WebProxy(Properties.Settings.Default.ProxyHost, Int16.Parse(Properties.Settings.Default.ProxyPort));
                proxyObject.Credentials = new NetworkCredential(proxyuser, pass, domain);
                return proxyObject;
            } 
            catch(Exception ex) 
            {
                FlickrSync.Error("Error connecting to Proxy", ex, ErrorType.Connect);
                return null;
            }
        }

        private void CalcOpenFolders()
        {
            ArrayList to_remove = new ArrayList();

            // reset all information about open folders to current view
            foreach (PathInfo pi in li.GetPathInfoList())
            {
                pi.Open = false;
                if (pi.IsEmpty())
                    to_remove.Add(pi.Path);
            }

            foreach (string str in to_remove)
                li.RemovePath(str);

            foreach (TreeNode node in treeFolders.Nodes)
                if (node.IsExpanded)
                    SetOpenFolders(node);
        }

        private void SetOpenFolders(TreeNode node)
        {
            PathInfo pi = li.GetPathInfo(node.Name);
            if (pi == null)
                li.AddPath(node.Name, true, false);
            else
                pi.Open = true;

            foreach (TreeNode subnode in node.Nodes)
                if (subnode.IsExpanded)
                    SetOpenFolders(subnode);
        }

        private void ExpandOpenFolders()
        {
            foreach (PathInfo pi in li.GetPathInfoList())
            {
                if (pi.Open)
                {
                    foreach (TreeNode node in treeFolders.Nodes.Find(pi.Path, true))
                        node.Expand();
                }
            }

            if (treeFolders.Nodes.Count > 0)
                treeFolders.Nodes[0].EnsureVisible();
        }

        #endregion

        #region Events
        private void treeFolders_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (e.Node.Checked)
            {
                if (syncprop_status == SyncPropertiesStatus.CancelAll)
                {
                    e.Node.Checked = false;

                    treeFolders.RefreshTreeNodes(treeFolders.Nodes);
                    CalcTooltips();
                    Cursor = Cursors.Default;
                    return;
                }

                SyncFolder sf = new SyncFolder(e.Node.Name);
                
                //try to match Set name
                foreach (Photoset ps in FlickrSync.ri.GetAllSets())
                {
                    if (ps.Title.Equals(Path.GetFileName(e.Node.Name),StringComparison.CurrentCultureIgnoreCase))
                    {
                        sf.SetId = ps.PhotosetId;
                        break;
                    }
                }
                if (sf.SetId == "")
                    sf.SetTitle = Path.GetFileName(e.Node.Name);

                if (syncprop_status == SyncPropertiesStatus.OKAll)
                {
                    li.Add(sf);
                }
                else
                {
                    SyncFolderForm sff = new SyncFolderForm(sf);
                    if (syncprop_status == SyncPropertiesStatus.MultipleUndef)
                        sff.SetMultiple(true);

                    DialogResult dr=sff.ShowDialog();

                    if (dr == DialogResult.OK || dr==DialogResult.Yes)  //Yes means OK to All
                        li.Add(sf);
                    else
                        e.Node.Checked = false;

                    if (dr == DialogResult.Yes && syncprop_status == SyncPropertiesStatus.MultipleUndef)
                        syncprop_status = SyncPropertiesStatus.OKAll;

                    if (dr == DialogResult.Abort) //CancelAll
                    {
                        syncprop_status = SyncPropertiesStatus.CancelAll;

                        treeFolders.RefreshTreeNodes(treeFolders.Nodes);
                        CalcTooltips();
                        Cursor = Cursors.Default;
                        return;
                    }
                }
            }
            else
                li.Remove(e.Node.Name);

            treeFolders.RefreshTreeNodes(treeFolders.Nodes);
            CalcTooltips();
            Cursor = Cursors.Default;
        }

        private void treeFolders_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = e.Node.Name;
            proc.Start();
        }

        private void treeFolders_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void listSets_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listSets_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point p = listSets.PointToClient(new Point(e.X, e.Y));
                ListViewItem lv = listSets.GetItemAt(p.X, p.Y);
                TreeNode node=(TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                if (lv != null && node != null)
                {
                    li.Associate(node.Name, lv.Name);
                    string setname = ri.GetSet(lv.Name).Title;
                    MessageBox.Show(node.Name + " associated to set " + setname, "Information");
                }
            }
        }

        private void listSets_DoubleClick(object sender, EventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = ri.GetPhotosetURL(listSets.FocusedItem.Name);
            proc.Start();
        }

        private void FlickrSync_Resize(object sender, EventArgs e)
        {
            labelCalc.Left = this.Width / 2 - labelCalc.Width/2;
            labelCalc.Top = this.Height / 2 - labelCalc.Height/2;
        }

        private void ViewLogMenuItem_Paint(object sender, PaintEventArgs e)
        {
            ViewLogMenuItem.Enabled = (log_level != LogLevel.LogNone);
        }

        private void treeFolders_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                MouseDownPos = new Point(e.X, e.Y);
        }

        #endregion

        #region Menu Options
        private void newUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlickrGetToken();

            // ri is needed to get the user. TODO: Get User without using ri
            ri = new RemoteInfo();
            
            UpdateUser();
            LoadConfig();
            Reload();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalcOpenFolders();
            li.SaveToXML();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewAndSync();
        }

        private void reloadSetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1("");
            about.Show();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node;
            if (MouseDownPos == null)
            {
                Point org = new Point(contextMenuLocalFolder.Left, contextMenuLocalFolder.Top);
                Point p = treeFolders.PointToClient(org);
                node = treeFolders.GetNodeAt(p.X, p.Y);
            }
            else
                node = treeFolders.GetNodeAt(MouseDownPos.X, MouseDownPos.Y);

            SyncFolder sf = null;

            if (node != null)
                sf = li.GetSyncFolder(node.Name);

            if (sf != null)
            {
                SyncFolderForm sff = new SyncFolderForm(sf);
                sff.ShowDialog();
            }
        }

        private void configSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point org = new Point(contextMenuSets.Left, contextMenuSets.Top);
            Point p = listSets.PointToClient(org);
            ListViewItem lvi = listSets.GetItemAt(p.X, p.Y);
            SyncFolder sf = null;

            if (lvi != null)
                sf = FlickrSync.li.GetSyncFolderBySet(lvi.Name);

            if (sf != null)
            {
                SyncFolderForm sff = new SyncFolderForm(sf);
                sff.ShowDialog();
            }
        }

        private void viewAndSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList sf_col = new ArrayList();
            foreach (TreeNode tn in treeFolders.SelectedNodes)
            {
                SyncFolder sf = li.GetSyncFolder(tn.Name);
                if (sf != null)
                    sf_col.Add(sf);
            }

            ViewAndSync(sf_col);
        }

        private void viewAndSyncSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList sf_col=new ArrayList();

            foreach(ListViewItem lvi in listSets.SelectedItems) {
                SyncFolder sf=FlickrSync.li.GetSyncFolderBySet(lvi.Name);
                if (sf != null)
                    sf_col.Add(sf);
            }

            ViewAndSync(sf_col);
        }

        private void menuitemSyncChildren_Click(object sender, EventArgs e)
        {
            ArrayList sf_col = new ArrayList();
            foreach (TreeNode tn in treeFolders.SelectedNodes)
                BuildSyncChildrenList(sf_col, tn);

            ViewAndSync(sf_col);
        }

        private void AddSyncFolder(ArrayList list, String path)
        {
            SyncFolder sf = li.GetSyncFolder(path);
            if (sf != null)
            {
                bool new_item=true;
                foreach(SyncFolder sf_org in list)
                    if (sf.Equals(sf_org))
                    {
                        new_item = false;
                        break;
                    }

                if (new_item)
                    list.Add(sf);
            }
        }

        private void BuildSyncChildrenList(ArrayList list, TreeNode tn)
        {
            treeFolders.ExpandNode(tn);
            AddSyncFolder(list, tn.Name);

            for (int i = 0; i < tn.Nodes.Count; i++) {
                AddSyncFolder(list, tn.Nodes[i].Name);

                treeFolders.ExpandNode(tn.Nodes[i]);
                if (tn.Nodes[i].Nodes.Count > 0 && tn.Nodes[i].Nodes[0].Text!="") 
                    BuildSyncChildrenList(list, tn.Nodes[i]);
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preferences pref = new Preferences();
            if (pref.ShowDialog() == DialogResult.OK)
            {
                messages_level = FlickrSync.StringToMsgLevel(Properties.Settings.Default.MessageLevel);
                log_level = FlickrSync.StringToLogLevel(Properties.Settings.Default.LogLevel);
                if (autorun)
                    messages_level = MessagesLevel.MessagesNone;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.HelpMain);
        }

        private void menuitemViews_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            menuitemDetails.Checked = false;
            menuitemLargeIcons.Checked = false;
            menuitemSmallIcons.Checked = false;
            menuitemList.Checked = false;
            menuitemTile.Checked = false;

            ToolStripMenuItem tsmi = e.ClickedItem as ToolStripMenuItem;
            if (tsmi != null) {
                tsmi.Checked = true;
                listSets.View = (View)Enum.Parse(typeof(View), (string)tsmi.Tag);
            }

        }

        private void menuitemViews_ButtonClick(object sender, EventArgs e)
        {
            int nNewView = (int)listSets.View;
            nNewView++;
            nNewView %= ((int)View.Tile) + 1;
            listSets.View = (View)nNewView;

            menuitemDetails.Checked = listSets.View == View.Details;
            menuitemLargeIcons.Checked = listSets.View == View.LargeIcon;
            menuitemSmallIcons.Checked = listSets.View == View.SmallIcon;
            menuitemList.Checked = listSets.View == View.List;
            menuitemTile.Checked = listSets.View == View.Tile;
        }


        private void checkAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int maxlevel = 0;

            syncprop_status = SyncPropertiesStatus.MultipleUndef;

            if (treeFolders.SelectedNode != null) 
                foreach(TreeNode tn in treeFolders.SelectedNodes) 
                    CheckAllChildren(tn, true, 0, ref maxlevel);

            syncprop_status = SyncPropertiesStatus.Default;
        }

        private void uncheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int maxlevel = 0;

            syncprop_status = SyncPropertiesStatus.MultipleUndef;

            if (treeFolders.SelectedNode != null) 
                if (MessageBox.Show("Are you sure you want to uncheck all children folders?","Confirmation",MessageBoxButtons.OKCancel)==DialogResult.OK)
                    foreach (TreeNode tn in treeFolders.SelectedNodes)
                        CheckAllChildren(tn, false, 0, ref maxlevel);

            syncprop_status = SyncPropertiesStatus.Default;
        }

        // maxlevel<=0 - ask if you want to check just the 1st level
        private void CheckAllChildren(TreeNode tn, bool bValue, int level, ref int maxlevel)
        {
            treeFolders.ExpandNode(tn);
            level++;

            for (int i = 0; i < tn.Nodes.Count; i++) {
                if (tn.Nodes[i].Checked!=bValue)
                    tn.Nodes[i].Checked = bValue;

                if (level < maxlevel || maxlevel <= 0)
                {
                    treeFolders.ExpandNode(tn.Nodes[i]);

                    if (tn.Nodes[i].Nodes.Count > 0 && tn.Nodes[i].Nodes[0].Text != "")
                    {
                        bool syncnext = true;
                        if (level == 1 && maxlevel <= 0)
                        {
                            string msg = String.Format("Do you want to {0}check all folder levels (folders within folders)?", bValue ? "" : "un");

                            if (MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                syncnext = false;
                                maxlevel = 1;
                            }
                            else
                                maxlevel = System.Int32.MaxValue;
                        }

                        if (syncnext)
                            CheckAllChildren(tn.Nodes[i], bValue, level, ref maxlevel);
                    }
                }
            }
        }

        // Automatically select the right node on right click
        private void contextMenuLocalFolder_Opening(object sender, CancelEventArgs e)
        {
            TreeNode tn = treeFolders.GetNodeAt(treeFolders.PointToClient(Cursor.Position));
            if (tn != null)
                treeFolders.SelectedNode = tn;

            // Enable Properties only if node is checked
            ToolStripItem[] tsi=contextMenuLocalFolder.Items.Find("configToolStripMenuItem", false);
            if (tsi.GetLength(0)>0 && tn!=null)
                tsi[0].Enabled = tn.Checked;

            // Enable View And Sync if any selected node is checked
            tsi = contextMenuLocalFolder.Items.Find("viewAndSyncToolStripMenuItem", false);
            if (tsi.GetLength(0) > 0)
            {
                bool isselected = false;
                foreach (TreeNode tn2 in treeFolders.SelectedNodes)
                    if (tn2.Checked)
                    {
                        isselected = true;
                        break;
                    }

                tsi[0].Enabled = isselected;
            }

            // Hide "Remove Path" menu is this is not a manually added network path
            tsi = contextMenuLocalFolder.Items.Find("removePathToolStripMenuItem", false);
            PathInfo pi=li.GetPathInfo(tn.Name);
            tsi[0].Visible = (pi != null && pi.ManualAdd);
        }

        private void contextMenuSets_Opening(object sender, CancelEventArgs e)
        {
            Point org = new Point(contextMenuSets.Left, contextMenuSets.Top);
            Point p = listSets.PointToClient(org);
            ListViewItem lvi = listSets.GetItemAt(p.X, p.Y);
            SyncFolder sf = null;

            if (lvi != null)
                sf = FlickrSync.li.GetSyncFolderBySet(lvi.Name);

            // Enable Properties only if set is under FlickrSync configuration
            ToolStripItem[] tsi=contextMenuSets.Items.Find("ConfigSetToolStripMenuItem", false);
            if (tsi.GetLength(0)>0)
                tsi[0].Enabled = (sf!=null);

            // Enable View and Sync only if set is under FlickrSync configuration
            tsi = contextMenuSets.Items.Find("ViewAndSyncSetToolStripMenuItem", false);
            if (tsi.GetLength(0) > 0)
                tsi[0].Enabled = (sf != null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            li.SaveToXML();
        }

        private void btnViewSync_Click(object sender, EventArgs e)
        {
            ViewAndSync();
        }

        private void addNetworkPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select path to include on folder list";
            fbd.ShowNewFolderButton = false;
            fbd.ShowDialog();*/

            FolderBrowser fb = new FolderBrowser();
            fb.SetDescription("Select path to include on folder list");
            fb.SetNetworkSelect();

            if (fb.ShowDialog() == DialogResult.OK)
                if (fb.DirectoryPath == "")
                    Error("This network path is not valid for FlickrSync", null, ErrorType.Normal);
                else
                {
                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(fb.DirectoryPath);
                        if (!di.Exists)
                            Error("Network path is not accesible", null, ErrorType.Normal);
                        else
                        {
                            PathInfo pi = li.GetPathInfo(fb.DirectoryPath);
                            if (pi == null)
                                li.AddPath(fb.DirectoryPath, false, true);
                            else
                                pi.ManualAdd = true;

                            treeFolders.AddBaseNode(fb.DirectoryPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        Error("Invalid network path", ex, ErrorType.Normal);
                    }
                }
        }

        private void removePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node;
            if (MouseDownPos == null)
            {
                Point org = new Point(contextMenuLocalFolder.Left, contextMenuLocalFolder.Top);
                Point p = treeFolders.PointToClient(org);
                node = treeFolders.GetNodeAt(p.X, p.Y);
            }
            else
                node = treeFolders.GetNodeAt(MouseDownPos.X, MouseDownPos.Y);

            if (node != null)
            {
                li.RemovePath(node.Name);
                treeFolders.RemoveBaseNode(node.Name);

                ArrayList to_remove=new ArrayList();

                foreach (SyncFolder sf in li.GetSyncFolders())
                {
                    bool still_linked = false;

                    foreach (TreeNode bnode in treeFolders.Nodes)
                    {
                        if (sf.FolderPath.StartsWith(bnode.Name, StringComparison.CurrentCultureIgnoreCase)) {
                            still_linked = true;
                            break;
                        }
                    }

                    if (!still_linked)
                    {
                        if (MessageBox.Show("Folder " + sf.FolderPath + " is no longer visible. It should be removed from configuration. Do you want to remove it?", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            to_remove.Add(sf);
                    }
                }

                foreach (SyncFolder sf in to_remove)
                    li.Remove(sf.FolderPath);

                if (to_remove.Count > 0)
                    li.SaveToXML();
            }
        }

        private void AutorunMenuItem_Click(object sender, EventArgs e)
        {
            string cmd;
            cmd = "\"" + Application.ExecutablePath + "\" /auto";

            MessageBox.Show("To run automatically with no user interface, execute the following command:\n"+cmd+"\nThis information is already copied to the clipboard. Just paste it where needed","Information");
            Clipboard.SetText(cmd);
        }

        private void ViewLogMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(LogFile());
        }

        private void organizeSetOnFlickrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point org = new Point(contextMenuSets.Left, contextMenuSets.Top);
            Point p = listSets.PointToClient(org);
            ListViewItem lvi = listSets.GetItemAt(p.X, p.Y);

            if (lvi != null)
                Process.Start(ri.GetPhotosetEditURL(lvi.Name));
        }
        #endregion
    }
}
