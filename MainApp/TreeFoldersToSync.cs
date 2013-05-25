using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Controls
{
    class TreeFoldersToSync : TreeFolders
    {
        public override bool Exists(string path)
        {
            return FlickrSync.FlickrSync.li.Exists(path);
        }

        public override bool Includes(string path)
        {
            return FlickrSync.FlickrSync.li.Includes(path);
        }
        public override string ToolTipText(string path)
        {
            try
            {
                return FlickrSync.FlickrSync.ri.GetPhotosetTitle(FlickrSync.FlickrSync.li.GetSetId(path));
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
