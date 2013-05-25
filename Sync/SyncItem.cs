using System;
using System.Collections;
using System.Text;

namespace FlickrSync
{
    class SyncItem
    {
        public enum Actions
        {
            ActionUpload=1,
            ActionReplace,
            ActionDelete,
            ActionNone
        }

        public Actions Action;
        public string Filename;
        public string FolderPath;
        public string Title;
        public string SetId;
        public string SetTitle;
        public string SetDescription;
        public string PhotoId;
        public FlickrSync.Permissions Permission;
        public bool NoDeleteTags;
        
        public string Description;
        public ArrayList Tags;
        public double? GeoLat;
        public double? GeoLong;

        public int item_id;
    }
}
