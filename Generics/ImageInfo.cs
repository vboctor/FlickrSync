using System;
using System.Collections;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Forms;

namespace FlickrSync
{
    /// <summary>
    /// ImageInfo provides Metadata information for image files
    /// Currently it is only able to get meta information from Jpeg files
    /// </summary>
    class ImageInfo
    {
        public enum FileTypes {FileTypeUnknown=0, FileTypeJpeg, FileTypeGIF, FileTypePNG, FileTypeTiff, FileTypeBmp};

        ArrayList Tags;
        string Title;
        string Description;
        string City;
        string Sublocation;
        string Country;
        DateTime DateTaken;
        string FileName;
        double? GeoLat;
        double? GeoLong;
        int StarRating;

        public ImageInfo()
        {
            Tags = new ArrayList();
            Title = "";
            Description = "";
            City = "";
            Sublocation = "";
            Country = "";
            DateTaken = new DateTime(2000, 1, 1);
            FileName = "";
            GeoLat = GeoLong = null;
            StarRating = 0;
        }

        public void Load(string filename, FileTypes type)
        {
            Tags.Clear();
            Title = "";
            Description = "";
            City = "";
            Sublocation = "";
            Country = "";
            DateTaken = new DateTime(2000, 1, 1);
            FileName = filename;
            GeoLat = GeoLong = null;
            StarRating = 0;

            if (type == FileTypes.FileTypeUnknown)
            {
                if (filename.EndsWith(".jpg", System.StringComparison.CurrentCultureIgnoreCase))
                    type = FileTypes.FileTypeJpeg;
                else if (filename.EndsWith(".jpeg", System.StringComparison.CurrentCultureIgnoreCase))
                    type = FileTypes.FileTypeJpeg;
                else if (filename.EndsWith(".gif", System.StringComparison.CurrentCultureIgnoreCase))
                    type = FileTypes.FileTypeGIF;
                else if (filename.EndsWith(".png", System.StringComparison.CurrentCultureIgnoreCase))
                    type = FileTypes.FileTypePNG;
                else if (filename.EndsWith(".tif", System.StringComparison.CurrentCultureIgnoreCase))
                    type = FileTypes.FileTypeTiff;
                else if (filename.EndsWith(".tiff", System.StringComparison.CurrentCultureIgnoreCase))
                    type = FileTypes.FileTypeTiff;
                else if (filename.EndsWith(".bmp", System.StringComparison.CurrentCultureIgnoreCase))
                    type = FileTypes.FileTypeBmp;
            }

            try
            {
                Stream pictureStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BitmapMetadata bitmapMetadata;

                switch (type)
                {
                    case FileTypes.FileTypeJpeg:
                        JpegBitmapDecoder JpegDecoder = new JpegBitmapDecoder(pictureStream, BitmapCreateOptions.None, BitmapCacheOption.None);
                        bitmapMetadata = (BitmapMetadata)JpegDecoder.Frames[0].Metadata;

                        if (bitmapMetadata.Keywords != null)
                        {
                            foreach (string tag in bitmapMetadata.Keywords)
                                Tags.Add(tag);
                        }

                        Title = bitmapMetadata.Title;
                        Description = bitmapMetadata.Comment;
                        if (bitmapMetadata.DateTaken != null)
                            DateTaken = DateTime.Parse(bitmapMetadata.DateTaken);

                        //City = (string)bitmapMetadata.GetQuery(@"/xmp/<xmpbag>photoshop:City");
                        City = (string)bitmapMetadata.GetQuery(@"/app13/irb/8bimiptc/iptc/city");
                        if (City == null)
                            City = "";

                        Sublocation = (string)bitmapMetadata.GetQuery(@"/app13/irb/8bimiptc/iptc/sub-location");
                        if (Sublocation == null)
                            Sublocation = "";

                        //Country = (string)bitmapMetadata.GetQuery(@"/xmp/<xmpbag>photoshop:Country");
                        Country = (string)bitmapMetadata.GetQuery(@"/app13/irb/8bimiptc/iptc/country\/primary location name");
                        if (Country == null)
                            Country = "";

                        byte[] Version = (byte[])bitmapMetadata.GetQuery(@"/app1/ifd/gps/");
                        if (Version != null)
                        {
                            ulong[] GeoLatInfo = (ulong[])bitmapMetadata.GetQuery(@"/app1/ifd/gps/subifd:{ulong=2}");
                            string GeoLatDirection = (string)bitmapMetadata.GetQuery(@"/app1/ifd/gps/subifd:{char=1}");
                            ulong[] GeoLongInfo = (ulong[])bitmapMetadata.GetQuery(@"/app1/ifd/gps/subifd:{ulong=4}");
                            string GeoLongDirection = (string)bitmapMetadata.GetQuery(@"/app1/ifd/gps/subifd:{char=3}");

                            if (GeoLatInfo != null && GeoLatDirection != null)
                            {
                                GeoLat = ConvertCoordinate(GeoLatInfo);
                                if (GeoLatDirection == "S")
                                    GeoLat = -GeoLat;
                            }

                            if (GeoLongInfo != null && GeoLongDirection != null)
                            {
                                GeoLong = ConvertCoordinate(GeoLongInfo);
                                if (GeoLongDirection == "W")
                                    GeoLong = -GeoLong;
                            }
                        }

                        StarRating = bitmapMetadata.Rating;

                        break;

                    case FileTypes.FileTypeTiff:
                        TiffBitmapDecoder TiffDecoder = new TiffBitmapDecoder(pictureStream, BitmapCreateOptions.None, BitmapCacheOption.None);
                        bitmapMetadata = (BitmapMetadata)TiffDecoder.Frames[0].Metadata;

                        if (bitmapMetadata.Keywords != null)
                        {
                            foreach (string tag in bitmapMetadata.Keywords)
                                Tags.Add(tag);
                        }

                        Title = bitmapMetadata.Title;
                        Description = bitmapMetadata.Comment;
                        if (bitmapMetadata.DateTaken != null)
                            DateTaken = DateTime.Parse(bitmapMetadata.DateTaken);

                        StarRating = bitmapMetadata.Rating;

                        break;

                    case FileTypes.FileTypeGIF:
                        GifBitmapDecoder GifDecoder = new GifBitmapDecoder(pictureStream, BitmapCreateOptions.None, BitmapCacheOption.None);
                        bitmapMetadata = (BitmapMetadata)GifDecoder.Frames[0].Metadata;

                        if (bitmapMetadata.Keywords != null)
                        {
                            foreach (string tag in bitmapMetadata.Keywords)
                                Tags.Add(tag);
                        }

                        Title = bitmapMetadata.Title;
                        Description = bitmapMetadata.Comment;
                        if (bitmapMetadata.DateTaken != null)
                            DateTaken = DateTime.Parse(bitmapMetadata.DateTaken);

                        StarRating = bitmapMetadata.Rating;

                        break;

                    case FileTypes.FileTypePNG:
                        PngBitmapDecoder PngDecoder = new PngBitmapDecoder(pictureStream, BitmapCreateOptions.None, BitmapCacheOption.None);
                        bitmapMetadata = (BitmapMetadata)PngDecoder.Frames[0].Metadata;

                        if (bitmapMetadata.Keywords != null)
                        {
                            foreach (string tag in bitmapMetadata.Keywords)
                                Tags.Add(tag);
                        }

                        Title = bitmapMetadata.Title;
                        Description = bitmapMetadata.Comment;
                        if (bitmapMetadata.DateTaken != null)
                            DateTaken = DateTime.Parse(bitmapMetadata.DateTaken);

                        StarRating = bitmapMetadata.Rating;

                        break;

                    case FileTypes.FileTypeBmp:
                        BmpBitmapDecoder BmpDecoder = new BmpBitmapDecoder(pictureStream, BitmapCreateOptions.None, BitmapCacheOption.None);
                        bitmapMetadata = (BitmapMetadata)BmpDecoder.Frames[0].Metadata;

                        if (bitmapMetadata.Keywords != null)
                        {
                            foreach (string tag in bitmapMetadata.Keywords)
                                Tags.Add(tag);
                        }

                        Title = bitmapMetadata.Title;
                        Description = bitmapMetadata.Comment;
                        if (bitmapMetadata.DateTaken != null)
                            DateTaken = DateTime.Parse(bitmapMetadata.DateTaken);

                        StarRating = bitmapMetadata.Rating;

                        break;
                }

                pictureStream.Close();
            }
            catch (Exception)
            {
            }

            if (Title == null) Title = "";
            if (Description == null) Description = "";
        }

        private static double ConvertCoordinate(ulong[] coordinates)
        {
            int degrees;
            int minutes;
            double seconds;

            degrees = (int)splitLongAndDivide(coordinates[0]);
            minutes = (int)splitLongAndDivide(coordinates[1]);
            seconds = splitLongAndDivide(coordinates[2]);

            double coordinate = degrees + ((double)minutes / 60.0) + (seconds / 3600);

            double roundedCoordinate = Math.Floor((double)(coordinate * 1000000)) / 1000000;

            return roundedCoordinate;
        }

        private static double splitLongAndDivide(ulong number)
        {
            byte[] bytes = BitConverter.GetBytes(number);
            int int1 = BitConverter.ToInt32(bytes, 0);
            int int2 = BitConverter.ToInt32(bytes, 4);
            return ((double)int1 / (double)int2);
        }

        public string GetTitle()
        {
            return Title;
        }

        public string GetDescription()
        {
            return Description;
        }

        public string GetCity()
        {
            return City;
        }

        public string GetCountry()
        {
            return Country;
        }

        public ArrayList GetTagsArray()
        {
            return (ArrayList) Tags.Clone();
        }

        public DateTime GetDateTaken()
        {
            return DateTaken;
        }

        public string GetTagsString()
        {
            string taglist = "";

            foreach (string tag in Tags)
            {
                if (taglist == "")
                    taglist = tag;
                else
                    taglist+= "," + tag;
            }

            return taglist;
        }

        public string GetFileName()
        {
            return FileName;
        }

        public double? GetGeo(bool lat)
        {
            if (lat) return GeoLat;
            else return GeoLong;
        }

        public int GetStarRating()
        {
            return StarRating;
        }
    }
}
