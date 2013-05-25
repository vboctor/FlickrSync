using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FlickrNet;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Net;

namespace FlickrSync
{
    public class RemoteInfo
    {
        Flickr f;
        Photoset[] sets;
        public delegate void SetProgressType(int value);
        SetProgressType progress = null;

        public RemoteInfo()
        {
            try
            {
                f = new Flickr(Properties.Settings.Default.FlickrApiKey, Properties.Settings.Default.FlickrShared, Properties.Settings.Default.FlickrToken);
                f.Proxy = FlickrSync.GetProxy(true);
                f.OnUploadProgress += new Flickr.UploadProgressHandler(Flickr_OnUploadProgress);
                sets = f.PhotosetsGetList().PhotosetCollection;
                if (sets == null)
                    sets = new Photoset[0];

                string user = User();  //force access to check connection
            }
            catch (Exception ex)
            {
                FlickrSync.Error("Error connecting to flickr", ex, FlickrSync.ErrorType.Connect);
            }
        }

        public string User()
        {
            return f.AuthCheckToken(Properties.Settings.Default.FlickrToken).User.Username;
        }

        public string UserId()
        {
            return f.AuthCheckToken(Properties.Settings.Default.FlickrToken).User.UserId;
        }

        public void Reload()
        {
            Flickr.CacheDisabled = true;
            sets = f.PhotosetsGetList().PhotosetCollection;
            Flickr.CacheDisabled = false;
        }

        public Photo[] SetPhotos(string SetId)
        {
            List <Photo> PhotoList = new List<Photo>();          
            int nPage = 1;
            Photo[] zPhotoPage;

            do {
                zPhotoPage = f.PhotosetsGetPhotos(SetId,nPage, 500).PhotoCollection;
                
                for (int i=0; i<zPhotoPage.GetLength(0); i++)
                    PhotoList.Add(zPhotoPage[i]);

                nPage++;
            } while (zPhotoPage.GetLength(0)==500);

            return PhotoList.ToArray();
        }

        public Photoset[] GetAllSets()
        {
            return sets;
        }

        public Photoset GetSet(string SetId)
        {
            try
            {
                return f.PhotosetsGetInfo(SetId);
            }
            catch
            {
                return null;
            }
        }

        public string GetPhotosetTitle(string SetId)
        {
            if (SetId == "")
                return "";

            Photoset set = GetPhotoset(SetId);
            if (set == null)
                return "";
            else
                return set.Title;
        }

        public string GetPhotosetURL(string SetId)
        {
            return f.UrlsGetUserPhotos() + "sets/" + SetId;
        }

        public string GetPhotosetEditURL(string SetId)
        {
            return "http://www.flickr.com/photos/organize/?start_tab=one_set" + SetId;
        }

        public Photoset GetPhotoset(string SetId)
        {
            foreach (Photoset ps in sets)
                if (ps.PhotosetId == SetId)
                    return ps;

            return null;
        }

        public Image PhotosetThumbnail(Photoset ps)
        {
            try
            {
                return Bitmap.FromStream(f.DownloadPicture(ps.PhotosetSquareThumbnailUrl));
            }
            catch (Exception)
            {
                return Properties.Resources.icon_replace;
            }
                
        }

        public Image PhotosetThumbnail(string SetId)
        {
            return PhotosetThumbnail(GetPhotoset(SetId));
        }

        public Stream PhotoThumbnail(string photoid)
        {
            return f.DownloadPicture(f.PhotosGetInfo(photoid).SquareThumbnailUrl);
        }

        public string UploadPicture(string filename,string title,string description,ArrayList tags,FlickrSync.Permissions permission)
        {
            ArrayList specialtags = new ArrayList();
            foreach (string tag in tags)
            {
                if (tag.ToLower().StartsWith("flickrsync:") || tag.ToLower().StartsWith(@"""flickrsync:"))
                    specialtags.Add(tag);
            }

            foreach (string stag in specialtags)
            {
                tags.Remove(stag);

                string stag2=stag.ToLower();
                // To support picasa replace colons with equal sign (to be able to use flickrsync:perm:friends instead of flickrsync:perm=friends)
                stag2=stag2.Replace(':', '=');

                if (stag2.Contains("flickrsync=perm=private")) permission = FlickrSync.Permissions.PermPrivate;
                else if (stag2.Contains("flickrsync=perm=default")) permission = FlickrSync.Permissions.PermDefault;
                else if (stag2.Contains("flickrsync=perm=familyfriends")) permission = FlickrSync.Permissions.PermFamilyFriends;
                else if (stag2.Contains("flickrsync=perm=friends")) permission = FlickrSync.Permissions.PermFriends;
                else if (stag2.Contains("flickrsync=perm=family")) permission = FlickrSync.Permissions.PermFamily;
                else if (stag2.Contains("flickrsync=perm=public")) permission = FlickrSync.Permissions.PermPublic;
            }

            string tags_str = "";
            foreach (string tag in tags) 
                if (tag.Length>0 && tag[0]=='"' && tag[tag.Length-1]=='"')
                    tags_str=tags_str+tag;
                else
                    tags_str = tags_str + @"""" + tag + @""" ";

            string id = "";
            if (permission==FlickrSync.Permissions.PermDefault)
                id=f.UploadPicture(filename,title,description,tags_str);
            else 
                id=f.UploadPicture(filename,title,description,tags_str,
                    permission==FlickrSync.Permissions.PermPublic,
                    permission==FlickrSync.Permissions.PermFamily || permission==FlickrSync.Permissions.PermFamilyFriends,
                    permission==FlickrSync.Permissions.PermFriends || permission==FlickrSync.Permissions.PermFamilyFriends);

            // remove special tags. If there is an error it will be ignored (not very relevant)
            // they were removed previously from tags_str but flickr ignores this on upload
            try
            {
                foreach (string stag in specialtags)
                {
                    PhotoInfoTag[] taglist=f.TagsGetListPhoto(id);
                    foreach (PhotoInfoTag pit in taglist)
                        if (pit.Raw == stag)
                            f.PhotosRemoveTag(pit.TagId);
                }   
            }
            catch (Exception)
            {
            }

            return id;
        }

        public string ReplacePicture(string filename,string photoid,string title,string caption,ArrayList tags,FlickrSync.Permissions permission,bool NoDeleteTags,double? GeoLat,double? GeoLong)
        {
            string id=f.ReplacePicture(filename, photoid);
            f.PhotosSetMeta(id, title, caption);

            ArrayList specialtags = new ArrayList();
            foreach (string tag in tags)
            {
                if (tag.ToLower().StartsWith("flickrsync:") || tag.ToLower().StartsWith(@"""flickrsync:"))
                    specialtags.Add(tag);
            }

            foreach (string stag in specialtags)
            {
                tags.Remove(stag);

                string stag2 = stag.ToLower();
                // To support picasa replace colons with equal sign (to be able to use flickrsync:perm:friends instead of flickrsync:perm=friends)
                stag2=stag2.Replace(':', '=');

                if (stag2.Contains("flickrsync=perm=private")) permission = FlickrSync.Permissions.PermPrivate;
                else if (stag2.Contains("flickrsync=perm=default")) permission = FlickrSync.Permissions.PermDefault;
                else if (stag2.Contains("flickrsync=perm=familyfriends")) permission = FlickrSync.Permissions.PermFamilyFriends;
                else if (stag2.Contains("flickrsync=perm=friends")) permission = FlickrSync.Permissions.PermFriends;
                else if (stag2.Contains("flickrsync=perm=family")) permission = FlickrSync.Permissions.PermFamily;
                else if (stag2.Contains("flickrsync=perm=public")) permission = FlickrSync.Permissions.PermPublic;
            }

            f.PhotosSetPerms(id,
                permission == FlickrSync.Permissions.PermPublic,
                permission == FlickrSync.Permissions.PermFriends || permission == FlickrSync.Permissions.PermFamilyFriends,
                permission == FlickrSync.Permissions.PermFamily || permission == FlickrSync.Permissions.PermFamilyFriends,
                PermissionComment.Everybody, PermissionAddMeta.Everybody);

            string user=f.AuthCheckToken(Properties.Settings.Default.FlickrToken).User.UserId;

            PhotoInfoTag[] ftags = f.TagsGetListPhoto(id); //.Tags.TagCollection;

            if (ftags != null)
            {
                foreach (PhotoInfoTag ftag in ftags)
                {
                    if (ftag.AuthorId == user)
                    {
                        bool found = false;
                        for (int i = 0; i < tags.Count; i++)
                        {
                            if ((string)tags[i] == ftag.Raw)
                            {
                                found = true;
                                tags.RemoveAt(i);
                                break;
                            }
                        }

                        if (!found && !NoDeleteTags)
                            f.PhotosRemoveTag(ftag.TagId);
                    }
                }
            }

            foreach (string tag in tags)
            {
                if (tag.Length > 0 && tag[0] == '"' && tag[tag.Length - 1] == '"')
                    f.PhotosAddTags(id, tag);
                else
                    f.PhotosAddTags(id, @"""" + tag + @"""");
            }

            if (GeoLat != null && GeoLong != null)
            {
                try
                {
                    f.PhotosGeoSetLocation(id, (double)GeoLat, (double)GeoLong);
                }
                catch (Exception ex)
                {
                    FlickrSync.Error("Error setting Geo location: " + filename + " Lat=" + GeoLat + " Long=" + GeoLong,  ex, FlickrSync.ErrorType.Normal);
                }
            }

            return id;
        }

        public void DeletePicture(string photoid)
        {
            f.PhotosDelete(photoid);
        }

        public void PhotosetsAddPhoto(string setid,string photoid)
        {
            f.PhotosetsAddPhoto(setid, photoid);
        }

        public Photoset CreateSet(string title, string description, string photoid)
        {
            return f.PhotosetsCreate(title,description,photoid);
        }

        public void SetProgress(SetProgressType p)
        {
            progress = p;
        }

        public void ClearProgress()
        {
            progress = null;
        }

        private void Flickr_OnUploadProgress(object sender, UploadProgressEventArgs e)
        {
            if (progress != null)
                progress(e.Bytes);
        }

        public long MaxFileSize()
        {
            try
            {
                return f.PeopleGetUploadStatus().FilesizeMax;
            }
            catch (Exception)
            {
                return long.MaxValue;
            }
        }

        public void SortSet(string setid,string[] ids)
        {
            f.PhotosetsEditPhotos(setid, f.PhotosetsGetInfo(setid).PrimaryPhotoId, ids);
        }

    }
}
