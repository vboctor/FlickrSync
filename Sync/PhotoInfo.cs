using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrSync
{
    /// <summary>
    /// Class PhotoInfo is an auxiliary class used to store local picture information during synchronization process
    /// </summary>
    class PhotoInfo
    {
        public string Title;
        public DateTime DateTaken;
        public DateTime DateUploaded;
        public DateTime DateSync;
        public string PhotoId;
        public bool Found;
    }
}
