using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FlickrNet;
using System.Windows.Media.Imaging;
using System.Threading;
using ThumbDBLib;
using System.Diagnostics;

namespace FlickrSync
{
    public partial class SyncView : Form
    {
        const int ThumbnailSize = 75;

        struct ThumbnailTask
        {
            public string org;
            public string key;
            public bool local;
            public SyncItem.Actions action;
        };

        ArrayList SyncFolders;
        ArrayList SyncItems;
        static DateTime SyncDate;
        string AdsUrlPath;

        bool SyncStarted = false;
        bool SyncAborted = false;
        bool Finished = false;
        Thread SyncThread = null;
        Thread ThumbnailThread = null;

        ArrayList Tasks;
        ArrayList NewSets;

        private delegate void ChangeProgressBarCallBack(ProgressBars pb, ProgressValues type, int value, string status);
        private delegate void RefreshImagesCallBack(int index);
        private delegate void FinishCallBack();

        enum ProgressBars { PBSync = 0, PBPhoto };
        enum ProgressValues { PBValue = 0, PBMinimum, PBMaximum };

        public SyncView(ArrayList pSyncFolders)
        {
            InitializeComponent();
            SyncItems=new ArrayList();
            Tasks = new ArrayList();
            NewSets = new ArrayList();
            SyncFolders = pSyncFolders;
            AdsUrlPath = "";

            CalcSync();
        }

        private void UpdateThumbnails()

        {
            try
            {
                string thumb_path = "";
                ThumbDB th = null;

                foreach (ThumbnailTask tt in Tasks)
                {
                    int index = imageList1.Images.IndexOfKey(tt.key);
                    if (index < 0) continue;

                    if (tt.local)
                    {
                        Bitmap bm = null;
                        int bmSize = ThumbnailSize;

                        try
                        {
                            // NOTE: Specific Windows Code - Get thumbnail from Thumbs.db for faster update
                            string filepath = Path.GetDirectoryName(tt.org);
                            string thumb_path_new = filepath + Path.DirectorySeparatorChar + "Thumbs.db";
                            if (thumb_path_new != thumb_path)
                            {
                                th = new ThumbDB(thumb_path_new);
                                thumb_path = thumb_path_new;
                            }

                            if (th != null)
                                bm = (Bitmap)th.GetThumbnailImage(Path.GetFileName(tt.org));
                        }
                        catch (Exception)
                        {
                        }

                        if (bm == null)
                        {
                            bm = new Bitmap(tt.org);
                            //bmSize = ThumbnailSize*2; 

                            if (bm == null)
                                continue;
                        }

                        Rectangle rc = new Rectangle(0, 0, bmSize, bmSize);

                        Bitmap bm2;
                        if (bm.Width > bm.Height)
                        {
                            bm2 = new Bitmap(bm, bmSize * bm.Width / bm.Height, bmSize);
                            rc.X = (bm2.Width - bmSize) / 2;
                        }
                        else
                        {
                            bm2 = new Bitmap(bm, bmSize, bmSize * bm.Height / bm.Width);
                            rc.Y = (bm2.Height - bmSize) / 2;
                        }

                        bm2 = bm2.Clone(rc, bm2.PixelFormat);
                        imageList1.Images[index] = bm2;

                        bm.Dispose();
                        bm2.Dispose();
                    }
                    else
                    {
                        try
                        {
                            imageList1.Images[index] = Bitmap.FromStream(FlickrSync.ri.PhotoThumbnail(tt.org));
                        }
                        catch (Exception)
                        {
                        }
                    }

                    if (tt.action == SyncItem.Actions.ActionNone)
                    {
                        Image img = (Image) imageList1.Images[index].Clone();
                        Graphics g = Graphics.FromImage(img);
                        g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                        g.FillRectangle(new SolidBrush(Color.FromArgb(192, Color.White)), new Rectangle(0, 0, ThumbnailSize, ThumbnailSize));
                        imageList1.Images[index] = img;
                    }

                    this.Invoke(new RefreshImagesCallBack(RefreshImages), new object[] { index });
                }
            }
            catch(Exception)
            {
            }
        }

        private Image Thumbnail(string org,string key,bool local,SyncItem.Actions action)
        {
            ThumbnailTask tt;
            tt.org = org;
            tt.key = key;
            tt.local = local;
            tt.action = action;
            Tasks.Add(tt);

            switch (tt.action)
            {
                case SyncItem.Actions.ActionUpload:
                    return Properties.Resources.icon_new;
                case SyncItem.Actions.ActionReplace:
                    return Properties.Resources.icon_replace;
                case SyncItem.Actions.ActionDelete:
                    return Properties.Resources.icon_delete;
                case SyncItem.Actions.ActionNone:
                    return Properties.Resources.icon_none;
            }

            return Properties.Resources.flickrsync;
        }

        private void RefreshImages(int index)
        {
            try
            {
                listViewToSync.Invalidate(listViewToSync.Items[index].Bounds);
            }
            catch (Exception)
            {
            }
        }

        private void CalcSync()
        {
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();
            Flickr.CacheDisabled = true;
            SyncDate = DateTime.Now;
            int count_images = 0;
            long max_size = FlickrSync.ri.MaxFileSize();

            if (Properties.Settings.Default.UseThumbnailImages)
            {
                imageList1.Images.Clear();
                imageList1.ImageSize = new System.Drawing.Size(75, 75);
            }

            foreach (SyncFolder sf in SyncFolders)
            {
                Application.DoEvents();

                if (sf.SetId == "" && sf.SetTitle == "")
                {
                    this.Cursor = Cursors.Default;
                    if (FlickrSync.messages_level==FlickrSync.MessagesLevel.MessagesAll)
                        MessageBox.Show(sf.FolderPath + " has no associated Set", "Warning");
                    FlickrSync.Log(FlickrSync.LogLevel.LogAll, sf.FolderPath + " has no associated set");
                    this.Cursor = Cursors.WaitCursor;
                    continue;
                }

                ArrayList photos = new ArrayList();

                if (sf.SetId != "")
                {
                    try
                    {
                        foreach (Photo p in FlickrSync.ri.SetPhotos(sf.SetId))
                        {
                            // workaround since media type and p.MachineTags is not working on FlickrNet 2.1.5
                            if (p.CleanTags != null)
                            {
                                if (p.CleanTags.ToLower().Contains("flickrsync:type=video") ||
                                    p.CleanTags.ToLower().Contains("flickrsync:cmd=skip") ||
                                    p.CleanTags.ToLower().Contains("flickrsync:type:video") ||
                                    p.CleanTags.ToLower().Contains("flickrsync:cmd:skip"))
                                    continue;
                            }

                            PhotoInfo pi = new PhotoInfo();
                            pi.Title = p.Title;
                            pi.DateTaken = p.DateTaken;
                            pi.DateSync = sf.LastSync;
                            pi.DateUploaded = p.DateUploaded;
                            pi.PhotoId = p.PhotoId;
                            pi.Found = false;
                            photos.Add(pi);
                        }
                    }
                    catch (Exception ex)
                    {
                        FlickrSync.Error("Error loading information from Set " + sf.SetId, ex, FlickrSync.ErrorType.Normal);
                        Close();
                    }
                }

                FileInfo[] files ={ };

                try
                {
                    DirectoryInfo dir = new DirectoryInfo(sf.FolderPath);
                    if (!dir.Exists)
                    {
                        if (FlickrSync.messages_level != FlickrSync.MessagesLevel.MessagesNone)
                        {
                            if (MessageBox.Show("Folder " + sf.FolderPath + " no longer exists. Remove from configuration?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                FlickrSync.li.Remove(sf.FolderPath);
                                FlickrSync.Log(FlickrSync.LogLevel.LogAll, sf.FolderPath + " deleted from configuration");
                            }
                            else
                                FlickrSync.Log(FlickrSync.LogLevel.LogAll, sf.FolderPath + " does not exist");
                        }

                        continue;
                    }

                    int count = Properties.Settings.Default.Extensions.Count;
                    int total = 0;
                    FileInfo[][] files_ext = new FileInfo[count][];

                    foreach (string ext in Properties.Settings.Default.Extensions)
                    {
                        count--;
                        files_ext[count] = dir.GetFiles("*." + ext);
                        total += files_ext[count].GetLength(0);
                    }

                    files = new FileInfo[total];
                    total = 0;
                    foreach (string ext in Properties.Settings.Default.Extensions)
                    {
                        files_ext[count].CopyTo(files, total);
                        total += files_ext[count].GetLength(0);
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    FlickrSync.Error("Error accessing path: " + sf.FolderPath, ex, FlickrSync.ErrorType.Normal);
                    this.Close();
                }

                ImageInfo ii = new ImageInfo();
                string[] ftags = sf.FilterTags.Split(';');
                for (int i = 0; i < ftags.GetLength(0); i++)
                {
                    ftags[i] = ftags[i].Trim().ToLower();
                }

                ListViewGroup group;
                if (sf.SetId == "")
                    group = new ListViewGroup("Folder: " + sf.FolderPath + "; Set: " + sf.SetTitle);
                else
                {
                    try
                    {
                        group = new ListViewGroup("Folder: " + sf.FolderPath + "; Set: " + FlickrSync.ri.GetPhotoset(sf.SetId).Title);
                    }
                    catch (Exception)
                    {
                        group = new ListViewGroup("Folder: " + sf.FolderPath + "; Set: " + sf.SetId);
                    }
                }

                listViewToSync.Groups.Add(group);

                Array.Sort(files, new sortLastWriteHelper());

                foreach (FileInfo fi in files)
                {
                    bool include = true;

                    if (sf.FilterType == SyncFolder.FilterTypes.FilterIncludeTags ||
                        sf.FilterType == SyncFolder.FilterTypes.FilterStarRating || 
                        sf.SyncMethod == SyncFolder.Methods.SyncDateTaken || 
                        sf.SyncMethod == SyncFolder.Methods.SyncTitleOrFilename)
                        ii.Load(fi.FullName, ImageInfo.FileTypes.FileTypeUnknown);

                    if (sf.FilterType == SyncFolder.FilterTypes.FilterIncludeTags)
                    {
                        include = false;

                        foreach (string tag in ii.GetTagsArray())
                        {
                            foreach (string tag2 in ftags)
                                if (tag.ToLower() == tag2)
                                {
                                    include = true;
                                    break;
                                }

                            if (include)
                                break;
                        }
                    }

                    if (sf.FilterType == SyncFolder.FilterTypes.FilterStarRating) 
                        if (sf.FilterStarRating>ii.GetStarRating())
                            include=false;

                    /*if (fi.Length > max_size)
                        include = false;*/

                    if (!include)
                        continue;

                    string name = fi.Name;
                    foreach (string ext in Properties.Settings.Default.Extensions)
                        if (fi.Name.EndsWith("." + ext, StringComparison.CurrentCultureIgnoreCase))
                            name = name.Remove(name.Length - 4);

                    if (name.EndsWith(" "))  // flickr removes ending space - need to replace with something else
                        name=name.Remove(name.Length-1).Insert(name.Length-1,@"|");

                    // Matching photos
                    int pos = -1;
                    bool found = false;
                    foreach (PhotoInfo pi in photos)
                    {
                        pos++;

                        if (sf.SyncMethod == SyncFolder.Methods.SyncFilename && pi.Title == name)
                        {
                            found = true;
                            break;
                        }

                        if (sf.SyncMethod == SyncFolder.Methods.SyncDateTaken && pi.DateTaken == ii.GetDateTaken())
                        {
                            found = true;
                            break;
                        }

                        if (sf.SyncMethod == SyncFolder.Methods.SyncTitleOrFilename)
                        {
                            string title = ii.GetTitle();
                            if (title==null || title == "") title = name;

                            if (pi.Title == title)
                            {
                                found = true;
                                break;
                            }
                        }
                    }

                    if (!found)
                    {
                        SyncItem si = new SyncItem();
                        si.Action = SyncItem.Actions.ActionUpload;

                        if (fi.Length > max_size)
                            si.Action = SyncItem.Actions.ActionNone;
                        
                        si.Filename = fi.FullName;
                        si.SetId = sf.SetId;
                        si.SetTitle = sf.SetTitle;
                        si.SetDescription = sf.SetDescription;

                        if (ii.GetFileName() != fi.FullName)
                            ii.Load(fi.FullName, ImageInfo.FileTypes.FileTypeUnknown);

                        if (ii.GetTitle() != "" && sf.SyncMethod != SyncFolder.Methods.SyncFilename)
                            si.Title = ii.GetTitle();
                        else
                            si.Title = name;

                        si.Description = ii.GetDescription();
                        si.Tags = ii.GetTagsArray();
                        if (ii.GetCity() != "")
                            si.Tags.Add(ii.GetCity());
                        if (ii.GetCountry() != "")
                            si.Tags.Add(ii.GetCountry());
                        si.GeoLat = ii.GetGeo(true);
                        si.GeoLong = ii.GetGeo(false);

                        si.FolderPath = sf.FolderPath;
                        si.Permission = sf.Permission;

                        int position = 0;
                        if (Properties.Settings.Default.UseThumbnailImages)
                        {
                            imageList1.Images.Add(count_images.ToString(), Thumbnail(fi.FullName, count_images.ToString(), true, si.Action));
                            position = count_images;
                            count_images++;
                        }

                        ListViewItem lvi;
                        if (si.Action != SyncItem.Actions.ActionNone)
                        {
                            lvi = listViewToSync.Items.Add("NEW: " + fi.Name, position);
                            lvi.ForeColor = Color.Blue;
                        }
                        else
                        {
                            lvi = listViewToSync.Items.Add("SKIP: " + fi.Name, position);
                            lvi.ForeColor = Color.LightGray;
                        }

                        lvi.ToolTipText = lvi.Text + " " + group.Header;
                        group.Items.Add(lvi);

                        si.item_id = lvi.Index;
                        SyncItems.Add(si);
                    }
                    else
                    {
                        ((PhotoInfo)photos[pos]).Found = true;

                        // Compare time is based on local info DateSync because flickr clock could be misaligned with local clock
                        DateTime compare = ((PhotoInfo)photos[pos]).DateSync;
                        if (compare == new DateTime(2000, 1, 1))
                            compare = ((PhotoInfo)photos[pos]).DateUploaded;

                        if (compare < fi.LastWriteTime)
                        {
                            SyncItem si = new SyncItem();
                            si.Action = SyncItem.Actions.ActionReplace;

                            if (sf.LastSync == (new DateTime(2000, 1, 1)) && sf.NoInitialReplace)
                                si.Action = SyncItem.Actions.ActionNone;

                            si.Filename = fi.FullName;
                            si.PhotoId = ((PhotoInfo)photos[pos]).PhotoId;
                            si.SetId = sf.SetId;
                            si.SetTitle = "";
                            si.SetDescription = "";
                            si.Permission = sf.Permission;
                            si.NoDeleteTags = sf.NoDeleteTags;

                            if (ii.GetFileName() != fi.FullName)
                                ii.Load(fi.FullName, ImageInfo.FileTypes.FileTypeUnknown);

                            if (ii.GetTitle() != "" && sf.SyncMethod != SyncFolder.Methods.SyncFilename)
                                si.Title = ii.GetTitle();
                            else
                                si.Title = name;

                            si.Description = ii.GetDescription();
                            si.Tags = ii.GetTagsArray();
                            if (ii.GetCity() != "")
                                si.Tags.Add(ii.GetCity());
                            if (ii.GetCountry() != "")
                                si.Tags.Add(ii.GetCountry());
                            si.GeoLat = ii.GetGeo(true);
                            si.GeoLong = ii.GetGeo(false);

                            int position = 0;
                            if (Properties.Settings.Default.UseThumbnailImages)
                            {
                                imageList1.Images.Add(count_images.ToString(), Thumbnail(fi.FullName, count_images.ToString(), true, SyncItem.Actions.ActionReplace));
                                position = count_images;
                                count_images++;
                            }

                            ListViewItem lvi;
                            if (si.Action != SyncItem.Actions.ActionNone)
                            {
                                lvi = listViewToSync.Items.Add("REPLACE: " + fi.Name, position);
                                lvi.ForeColor = Color.Black;
                            }
                            else
                            {
                                lvi = listViewToSync.Items.Add("SKIP: " + fi.Name, position);
                                lvi.ForeColor = Color.LightGray;
                            }
                            lvi.ToolTipText = lvi.Text+" "+group.Header;
                            group.Items.Add(lvi);

                            si.item_id = lvi.Index;
                            SyncItems.Add(si);
                        }
                    }
                }

                if (!sf.NoDelete)
                {
                    foreach (PhotoInfo pi in photos)
                    {
                        if (!pi.Found)
                        {
                            SyncItem si = new SyncItem();
                            si.Action = SyncItem.Actions.ActionDelete;
                            si.PhotoId = pi.PhotoId;
                            si.Title = pi.Title;
                            si.SetId = sf.SetId;

                            int position = 0;
                            if (Properties.Settings.Default.UseThumbnailImages)
                            {
                                imageList1.Images.Add(count_images.ToString(), Thumbnail(pi.PhotoId, count_images.ToString(), false, SyncItem.Actions.ActionDelete));
                                position = count_images;
                                count_images++;
                            }

                            ListViewItem lvi = listViewToSync.Items.Add("DELETE: " + pi.Title,position);
                            lvi.ForeColor = Color.Red;
                            lvi.ToolTipText = lvi.Text + " " + group.Header;
                            group.Items.Add(lvi);

                            si.item_id = lvi.Index;
                            SyncItems.Add(si);
                        }
                    }
                }
            }

            FlickrSync.Log(FlickrSync.LogLevel.LogAll, "Prepared Synchronization successful");

            Flickr.CacheDisabled = false;
            this.Cursor = Cursors.Default;
        }

        private void SyncView_Load(object sender, EventArgs e)
        {
            if (SyncItems.Count == 0)
            {
                if (FlickrSync.messages_level != FlickrSync.MessagesLevel.MessagesNone) 
                    MessageBox.Show("Nothing to do", "FlickrSync Information");

                FlickrSync.Log(FlickrSync.LogLevel.LogBasic, "Nothing to do");

                this.Close();
                return;
            }

            Program.MainFlickrSync.Visible = false;
            WindowState = FormWindowState.Maximized;          

            ThreadStart ts = new ThreadStart(UpdateThumbnails);
            ThumbnailThread = new Thread(ts);
            ThumbnailThread.IsBackground = true;
            ThumbnailThread.Start();
        }

        private void ChangeProgressBar(ProgressBars pb, ProgressValues type, int value)
        {
            ChangeProgressBar(pb, type, value, null);
        }

        private void ChangeProgressBar(ProgressBars pb, ProgressValues type, int value, string status)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new ChangeProgressBarCallBack(ChangeProgressBar), new object[] { pb, type, value, status });
                }
                else
                {
                    if ((status != null) && (status.Length > 0))
                    {
                        labelSync.Text = "Synchronizing: " + status;
                    }
                    ProgressBar p;
                    if (pb == ProgressBars.PBSync)
                        p = progressBarSync;
                    else
                        p = progressBarPhoto;

                    switch (type)
                    {
                        case ProgressValues.PBValue:
                            if (value < p.Minimum)
                                value = p.Minimum;
                            if (value > p.Maximum)
                                value = p.Maximum;

                            p.Value = value;
                            break;
                        case ProgressValues.PBMinimum:
                            p.Minimum = value;
                            break;
                        case ProgressValues.PBMaximum:
                            p.Maximum = value;
                            break;
                    }

                    if (pb == ProgressBars.PBSync)
                    {
                        int percent = 0;
                        if (p.Maximum != 0)
                            percent = p.Value * 100 / p.Maximum;

                        if (percent < 0)
                            percent = 0;
                        if (percent > 100)
                            percent = 100;

                        this.Text = "FlickrSync Synchronizing..." + percent.ToString() + @"%";
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void FlickrProgress(int value)
        {
            ChangeProgressBar(ProgressBars.PBPhoto, ProgressValues.PBValue, value);
        }

        private void MarkCompleted(int index)
        {
            try
            {
                ListViewItem lvi=listViewToSync.Items[index];

                lvi.ForeColor = SystemColors.WindowText;
                lvi.BackColor = Color.FromArgb(220, 250, 220);

                if (Properties.Settings.Default.UseThumbnailImages)
                {
                    Image img = (Image)lvi.ImageList.Images[lvi.ImageIndex].Clone();
                    Graphics g = Graphics.FromImage(img);
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    g.FillRectangle(new SolidBrush(Color.FromArgb(192, Color.White)), new Rectangle(0, 0, ThumbnailSize, ThumbnailSize));
                    g.DrawImage(Properties.Resources.check, ThumbnailSize - 25, ThumbnailSize - 25);
                    lvi.ImageList.Images[lvi.ImageIndex] = img;
                }

                listViewToSync.Invalidate(listViewToSync.Items[index].Bounds);
            }
            catch (Exception)
            {
            }
        }

        #region - Execute Sync -
        private SyncFolder GetSyncFolder(string SetId)
        {
            for (int i = 0; i < SyncFolders.Count; i++)
                if (((SyncFolder)SyncFolders[i]).SetId == SetId)
                    return (SyncFolder)SyncFolders[i];

            return null;
        }

        private FileInfo GetFileInfo(string filename)
        {
            FileInfo fi = null;

            try
            {
                fi = new FileInfo(filename);
            }
            catch (Exception)
            {
            }

            return fi;
        }

        private void Sort(SyncFolder sf)
        {
            if (sf.OrderType==SyncFolder.OrderTypes.OrderDefault)
                return;

            try
            {
                Photo[] photolist = FlickrSync.ri.SetPhotos(sf.SetId);
                ArrayList photo_array=new ArrayList();
                foreach (Photo p in photolist)
                    photo_array.Add(p);

                switch (sf.OrderType)
                {
                    case SyncFolder.OrderTypes.OrderDateTaken:
                        photo_array.Sort(new PhotoSortDateTaken());
                        break;
                    case SyncFolder.OrderTypes.OrderTitle:
                        photo_array.Sort(new PhotoSortTitle());
                        break;
                    case SyncFolder.OrderTypes.OrderTag:
                        photo_array.Sort(new PhotoSortTag());
                        break;
                }

                string[] ids = new string[photo_array.Count];
                for (int i = 0; i < photo_array.Count; i++)
                    ids[i] = ((Photo)photo_array[i]).PhotoId;

                FlickrSync.ri.SortSet(sf.SetId, ids);
            }
            catch (Exception ex)
            {
                FlickrSync.Error("Error sorting set "+sf.SetTitle+" ("+sf.SetId+")", ex, FlickrSync.ErrorType.Info);
            }
        }

        public void ExecuteSync()
        {
            try
            {
                ChangeProgressBar(ProgressBars.PBSync, ProgressValues.PBMinimum, 0);
                ChangeProgressBar(ProgressBars.PBSync, ProgressValues.PBMaximum, SyncItems.Count);
                int pos = 0;

                string CurrentSetId = "";
                bool ReplaceError = false;

                DateTime SyncProgressDate = new DateTime(2000, 1, 1);
                if (SyncFolders.Count>0)
                    SyncProgressDate=((SyncFolder)SyncFolders[0]).LastSync;

                foreach (SyncItem si in SyncItems)
                {
                    if (SyncAborted)
                    {
                        Finish();
                        return;
                    }

                    string logmsg="";
                    switch(si.Action) {
                        case SyncItem.Actions.ActionDelete:
                            logmsg = "To Execute: Delete " + si.Title;
                            break;
                        case SyncItem.Actions.ActionNone:
                            logmsg="To Execute: Skip "+si.Filename;
                            break;
                        case SyncItem.Actions.ActionReplace:
                            logmsg="To Execute: Replace "+si.Filename;
                            break;
                        case SyncItem.Actions.ActionUpload:
                            logmsg="To Execute: New "+si.Filename;
                            break;
                    }
                    FlickrSync.Log(FlickrSync.LogLevel.LogAll, logmsg);

                    FileInfo fi = null;
                    if (si.Action != SyncItem.Actions.ActionDelete)
                        fi = GetFileInfo(si.Filename);

                    if (si.SetId != CurrentSetId)
                    {
                        if (ReplaceError)
                            ReplaceError = false;
                        else
                        {
                            // CurrentSetId synchronization is finished on previous set
                            SyncFolder sfCurrent = GetSyncFolder(CurrentSetId);
                            if (sfCurrent != null)
                            {
                                sfCurrent.LastSync = SyncDate;
                                Sort(sfCurrent);
                            }

                            SyncFolder sfNew = GetSyncFolder(si.SetId);
                            if (sfNew != null)
                                SyncProgressDate = sfNew.LastSync;
                        }

                        CurrentSetId = si.SetId;
                    }
                    else
                    {
                        // Since replaces are controlled from the date of last synchronization, when there is an error on replace, 
                        // sync should not continue. Otherwise the file in error would never get updated
                        if (ReplaceError)
                        {
                            continue;
                        }
                        else if (si.Action != SyncItem.Actions.ActionDelete)
                        {
                            SyncFolder sfCurrent = GetSyncFolder(CurrentSetId);

                            if (sfCurrent!=null && fi!=null && SyncProgressDate < fi.LastWriteTime) //Only updates LastSync to the last date if the new file is more recent than the last one
                                sfCurrent.LastSync = SyncProgressDate;
                        }
                    }

                    ChangeProgressBar(ProgressBars.PBPhoto, ProgressValues.PBMinimum, 0, si.SetTitle + " " + si.Filename);
                    if (fi != null)
                        ChangeProgressBar(ProgressBars.PBPhoto, ProgressValues.PBMaximum, (int)fi.Length);

                    FlickrSync.ri.SetProgress(new RemoteInfo.SetProgressType(FlickrProgress));

                    switch (si.Action)
                    {
                        case SyncItem.Actions.ActionUpload:
                            string PhotoId = "";

                            bool retry = true;
                            int retrycount=0;

                            //sometimes upload may fail, so retry a few times before giving up
                            while (retry)
                            {
                                try
                                {
                                    PhotoId = FlickrSync.ri.UploadPicture(si.Filename, si.Title, si.Description, si.Tags, si.Permission);
                                    retry = false;
                                }
                                catch (Exception ex)
                                {
                                    if (retrycount <= 1)
                                        retrycount++;
                                    else
                                    {
                                        FlickrSync.Error("Error uploading picture to flickr: " + si.Filename, ex, FlickrSync.ErrorType.Normal);
                                        SyncAborted = true;
                                        retry = false;
                                        break;
                                    }
                                }
                            }

                            if (SyncAborted)
                                break;

                            if (si.SetId != "")
                            {
                                retry = true; //associating the photo might fail if it's done right after uploading
                                retrycount = 0;

                                while (retry)
                                {
                                    try
                                    {
                                        FlickrSync.ri.PhotosetsAddPhoto(si.SetId, PhotoId);
                                        retry = false;
                                    }
                                    catch (FlickrApiException ex)
                                    {
                                        if (ex.Code == 3)    // Code 3 means Photo already in set - not really a problem
                                        {
                                            retry = false;
                                        }
                                        else
                                        {                       //only ask for user confirmation after retrying (retry several times if messages are disabled).
                                            if ((retrycount>0 && FlickrSync.messages_level == FlickrSync.MessagesLevel.MessagesAll) ||
                                                (retrycount>3 && FlickrSync.messages_level!=FlickrSync.MessagesLevel.MessagesAll))
                                            {
                                                DialogResult resp = DialogResult.Abort;
                                                if (FlickrSync.messages_level == FlickrSync.MessagesLevel.MessagesAll)
                                                    resp=MessageBox.Show("Error associating photo to set: " + si.Filename + "\n" + ex.Message + "\nDo you want to continue?", "Error", MessageBoxButtons.AbortRetryIgnore);
                                                FlickrSync.Log(FlickrSync.LogLevel.LogBasic, "Error associating photo to set: " + si.Filename + "; " + ex.Message);

                                                if (resp == DialogResult.Abort)
                                                {
                                                    SyncAborted = true;
                                                    break;
                                                }

                                                if (resp == DialogResult.Ignore)
                                                    retry = false;
                                            }
                                            else
                                                Thread.Sleep(5000);

                                            retrycount++;
                                        }
                                    }
                                    catch (Exception ex2) // all other exceptions do the same
                                    {                     //only ask for user confirmation after retrying (retry several times if messages are disabled).
                                        if ((retrycount>0 && FlickrSync.messages_level == FlickrSync.MessagesLevel.MessagesAll) ||
                                            (retrycount>3 && FlickrSync.messages_level!=FlickrSync.MessagesLevel.MessagesAll))
                                        {
                                            DialogResult resp = DialogResult.Abort;
                                            if (FlickrSync.messages_level == FlickrSync.MessagesLevel.MessagesAll)
                                                resp=MessageBox.Show("Error associating photo to set: " + si.Filename + "\n" + ex2.Message + "\nDo you want to continue?", "Error", MessageBoxButtons.AbortRetryIgnore);
                                            FlickrSync.Log(FlickrSync.LogLevel.LogBasic, "Error associating photo to set: " + si.Filename + "; " + ex2.Message);

                                            if (resp == DialogResult.Abort)
                                            {
                                                SyncAborted = true;
                                                break;
                                            }

                                            if (resp == DialogResult.Ignore)
                                                retry = false;
                                        }
                                        else
                                            Thread.Sleep(5000);

                                        retrycount++;
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    Photoset ps = FlickrSync.ri.CreateSet(si.SetTitle, si.SetDescription, PhotoId);
                                    if (ps != null)
                                    {
                                        CurrentSetId = ps.PhotosetId;
                                        string Title = si.SetTitle;
                                        string Description = si.SetDescription;

                                        for (int i = 0; i < SyncItems.Count; i++)
                                        {
                                            SyncItem si2 = (SyncItem)SyncItems[i];

                                            if (si2.SetTitle == Title && si2.SetDescription == Description)
                                            {
                                                si2.SetId = ps.PhotosetId;
                                                si2.SetTitle = "";
                                                si2.SetDescription = "";
                                            }
                                        }

                                        FlickrSync.li.Associate(si.FolderPath, ps.PhotosetId);
                                        NewSets.Add(ps.PhotosetId);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    FlickrSync.Log(FlickrSync.LogLevel.LogBasic, "Error creating set: " + si.SetTitle + "; " + ex.Message);
                                    if (FlickrSync.messages_level!=FlickrSync.MessagesLevel.MessagesAll || MessageBox.Show("Error creating set: " + si.SetTitle + "\n" + ex.Message + "\nDo you want to continue?", "Error", MessageBoxButtons.OKCancel) != DialogResult.OK)
                                    {
                                        SyncAborted = true;
                                        break;
                                    }
                                }

                            }

                            break;

                        case SyncItem.Actions.ActionReplace:
                            try
                            {
                                Flickr.CacheDisabled = true;
                                FlickrSync.ri.ReplacePicture(si.Filename, si.PhotoId, si.Title, si.Description, si.Tags, si.Permission, si.NoDeleteTags, si.GeoLat, si.GeoLong);
                                Flickr.CacheDisabled = false;
                            }
                            catch (Exception ex)
                            {
                                FlickrSync.Error("Error replacing picture: " + si.Filename + " - Skipping this Set", ex, FlickrSync.ErrorType.Normal);
                                ReplaceError = true;
                            }

                            break;

                        case SyncItem.Actions.ActionDelete:
                            try
                            {
                                FlickrSync.ri.DeletePicture(si.PhotoId);
                            }
                            catch (Exception ex)
                            {
                                FlickrSync.Error("Error deleting picture: " + si.PhotoId, ex, FlickrSync.ErrorType.Normal);
                            }
                            break;

                    } // end switch on action

                    FlickrSync.ri.ClearProgress();

                    if (!SyncAborted)
                    {
                        try
                        {
                            pos++;
                            ChangeProgressBar(ProgressBars.PBSync, ProgressValues.PBValue, pos);

                            //if (Properties.Settings.Default.UseThumbnailImages) {
                            this.Invoke(new RefreshImagesCallBack(MarkCompleted), new object[] { si.item_id });
                            //}
                        }
                        catch (Exception)
                        {
                        }

                        if (!ReplaceError && si.Action!=SyncItem.Actions.ActionDelete && fi!=null)
                            SyncProgressDate = fi.LastWriteTime;
                    }
                } // end for each syncItem

                if (!ReplaceError)
                {
                    // CurrentSetId synchronization is finished
                    SyncFolder sfCurrent = GetSyncFolder(CurrentSetId);
                    if (sfCurrent != null)
                    {
                        sfCurrent.LastSync = SyncDate;
                        Sort(sfCurrent);
                    }
                }

                Finish();
            }
            catch (Exception ex)
            {
                FlickrSync.Error("Error during synchronization", ex, FlickrSync.ErrorType.Normal);
                SyncAborted = true;
                Finish();
            }
        }
        #endregion

        private bool ViewLog(int id)
        {
            Process.Start(FlickrSync.LogFile());
            return false;
        }

        private void Finish()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new FinishCallBack(Finish));
            }
            else
            {
                if (Finished)
                    return;

                Finished = true;

                if (SyncStarted)
                {
                    if (ThumbnailThread != null && ThumbnailThread.IsAlive)
                        ThumbnailThread.Abort();

                    string msg;
                    if (SyncAborted)
                        msg = "Synchronization aborted.";
                    else
                        msg = "Synchronization finished.";

                    try
                    {
                        FlickrSync.li.SaveToXML();
                        labelSync.Text = "";

                        FlickrSync.Log(FlickrSync.LogLevel.LogBasic, msg + " Updated configuration saved");

                        if (FlickrSync.messages_level != FlickrSync.MessagesLevel.MessagesNone)
                        {
                            //MessageBox.Show(msg + " Updated configuration saved.", "Sync");
                            CustomMsgBox msgbox = new CustomMsgBox(msg + " Updated configuration saved.","FlickrSync");
                            msgbox.AddButton("OK", 75, 1,msgbox.ButtonCallOK);
                            if (FlickrSync.log_level!=FlickrSync.LogLevel.LogNone)
                                msgbox.AddButton("View Log", 75, 2, ViewLog);
                            msgbox.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        FlickrSync.Error(msg + " Error saving configuration", ex, FlickrSync.ErrorType.Normal);
                    }

                    if (NewSets.Count > 0)
                        Program.MainFlickrSync.AddNewSets(NewSets);
                }

                this.Close();
            }
        }

        private void buttonSync_Click(object sender, EventArgs e)
        {
            labelSync.Text = "Synchronizing. Please Wait...";
            buttonSync.Visible=false;

            //show ads
            bool show=true;
            string hash = FlickrSync.ri.UserId().GetHashCode().ToString("X");
            foreach (string str in FlickrSync.HashUsers)
            {
                if (str == hash)
                {
                    show = false;
                    break;
                }

                if (str == "VERSION" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version)
                {
                    show = false;
                    break;
                }
            }

            if (show)
            {
                webBrowserAds.Height = 70;
                webBrowserAds.Width = listViewToSync.Width;
                webBrowserAds.Visible = true;
                listViewToSync.Height = listViewToSync.Height - webBrowserAds.Height - 5;
            }

            this.Text = "FlickrSync Synchronizing...0%";

            ThreadStart ts = new ThreadStart(ExecuteSync);
            SyncThread = new Thread(ts);
            SyncStarted = true;
            SyncThread.Start();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SyncAborted = true;
            Finish();
        }

        private void SyncView_FormClosed(object sender, FormClosedEventArgs e)
        {
            SyncAborted = true;
            Finish();
        }

        // listViewToSync_DrawItem is not currently used since listViewToSync is not ownerDraw
        // this is maintained for future extension
        private void listViewToSync_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Rectangle r=listViewToSync.GetItemRect(e.ItemIndex, ItemBoundsPortion.Icon);
            r.X = r.X + (r.Width - ThumbnailSize) / 2;
            r.Y = r.Y + (r.Height - ThumbnailSize) / 2;
            r.Width = ThumbnailSize;
            r.Height = ThumbnailSize;

            e.Graphics.DrawImage(e.Item.ImageList.Images[e.Item.ImageIndex], r);

            if (listViewToSync.View != View.Details)
            {
                StringFormat fmt = new StringFormat();
                fmt.Trimming = StringTrimming.EllipsisCharacter;
                fmt.FormatFlags = StringFormatFlags.LineLimit;
                fmt.Alignment = StringAlignment.Center;
 
                Rectangle r2 = e.Item.Bounds;
                r2.Y = r.Y + r.Height + 1;
                r2.Height = e.Item.Bounds.Bottom - r2.Y;

                e.Graphics.DrawString(e.Item.Text, listViewToSync.Font, new SolidBrush(e.Item.ForeColor), r2, fmt);
            }
        }

        private void webBrowserAds_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (webBrowserAds.Visible && e.Url.ToString()!=Properties.Settings.Default.AdUrl && e.Url.LocalPath!=AdsUrlPath)
            {
                e.Cancel = true;
                System.Diagnostics.Process.Start(e.Url.ToString());
            }

            if (AdsUrlPath == "")
                AdsUrlPath = e.Url.LocalPath;
        }
    }
}
