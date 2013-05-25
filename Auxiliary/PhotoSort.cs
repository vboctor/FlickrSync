using System;
using System.Collections;
using System.Text;
using FlickrNet;

namespace FlickrSync
{
    public class PhotoSortDateTaken : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Photo p1 = (Photo)x;
            Photo p2 = (Photo)y;

            if (p1.DateTaken > p2.DateTaken)
                return 1;
            else if (p1.DateTaken < p2.DateTaken)
                return -1;
            else
                return 0;
        }
    }

    public class PhotoSortTitle : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Photo p1 = (Photo)x;
            Photo p2 = (Photo)y;

            return String.Compare(p1.Title, p2.Title, StringComparison.CurrentCulture);
        }
    }

    public class PhotoSortTag : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Photo p1 = (Photo)x;
            Photo p2 = (Photo)y;
            int order1 = Int32.MaxValue, order2 = Int32.MaxValue;

            String p1ct = p1.CleanTags.ToLower();
            p1ct = p1ct.Replace("flickrsync:order:", "flickrsync:order=");

            // FlickrNet is not yet supporting MachineTags fields so we use CleanTags
            int pos = p1ct.IndexOf("flickrsync:order=");
            if (pos >= 0)
            {
                string str = p1.CleanTags.Substring(pos + 17);
                str.Trim();
                pos = str.IndexOf(' ');
                if (pos > 0)
                    str = str.Remove(pos);
                str.Trim();

                order1 = Convert.ToInt32(str);
            }

            String p2ct = p2.CleanTags.ToLower();
            p2ct = p2ct.Replace("flickrsync:order:", "flickrsync:order=");

            pos = p2ct.IndexOf("flickrsync:order=");
            if (pos >= 0)
            {
                string str = p2.CleanTags.Substring(pos + 17);
                str.Trim();
                pos = str.IndexOf(' ');
                if (pos > 0)
                    str = str.Remove(pos);
                str.Trim();

                order2 = Convert.ToInt32(str);
            }

            if (order1 < order2)
                return -1;
            else if (order1 > order2)
                return 1;
            else
                return 0;
        }
    }

}
